﻿using AutoMapper;
using Grad.APIs.DTO.Entities_Dto;
using Grad.APIs.DTO.Entities_Dto.Graduation;
using Grad.APIs.Helpers;
using Grad.Core.Entities.Control;
using Grad.Core.Entities.Graduation;
using Grad.Core.Specifications.Enities_Spec;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.APIs.Controllers;
using Talabat.APIs.Errors;
using Talabat.Core;
using Talabat.Core.Entities.Academic_regulation;
using Talabat.Core.Entities.Lockups;
using Talabat.Repository.Data;
using Talabat.Repository.Data.Talabat.Repository.Data;

namespace Grad.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraduationController : APIBaseController
    {
        private readonly GradContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GraduationController(IMapper mapper, IUnitOfWork unitOfWork, GradContext dbcontext)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GraduationDTO>>> GetAllGraduations(int? GradId)
        {
            var spec = new GraduationSpec(GradId);
            var Grads = await _unitOfWork.Repository<Graduation>().GetAllWithSpecAsync(spec);
            await _dbcontext.Set<AverageValue>().Include(G => G.AllGrades).Include(f => f.EquivalentGrade).ToListAsync();
            await _dbcontext.Set<GraduationSemesters>().Include(G => G.semesters).ToListAsync();
            await _dbcontext.Set<GraduationLevels>().Include(G => G.Level).ToListAsync();
            await _dbcontext.Set<AverageValue>().Include(G => G.AllGrades).Include(f => f.EquivalentGrade).ToListAsync();
            var GradDTO = _mapper.Map<IEnumerable<Graduation>, IEnumerable<GraduationDTO>>(Grads);
            return Ok(GradDTO);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GraduationDTO), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<ActionResult<GraduationDTO>> GetGraduationById(int id)
        {

            var spec = new GraduationSpec(id);
            var Grad = await _unitOfWork.Repository<Graduation>().GetEntityWithSpecAsync(spec);
            if (Grad == null)
                return NotFound(new ApiResponse(404));
            await _dbcontext.Set<AverageValue>().Include(G => G.AllGrades).Include(f => f.EquivalentGrade).ToListAsync();
            await _dbcontext.Set<GraduationSemesters>().Include(G => G.semesters).ToListAsync();
            await _dbcontext.Set<GraduationLevels>().Include(G => G.Level).ToListAsync();
            await _dbcontext.Set<AverageValue>().Include(G => G.AllGrades).Include(f => f.EquivalentGrade).ToListAsync();
            var GradDTO = _mapper.Map<Graduation, GraduationDTO>(Grad);
            return Ok(GradDTO);
        }
  
        [HttpPost]
        public async Task<ActionResult<GraduationReq>> AddGraduation( GraduationReq graduationReq)
        {
            bool exists = await _unitOfWork.Repository<Graduation>().ExistAsync(
                 x => x.ProgramId == graduationReq.ProgramId && x.IsDeleted == false);
            await Console.Out.WriteLineAsync("Program  :  " + graduationReq.ProgramId + " < ");
            if (exists)
            {
                return StatusCode(409, new ApiResponse(409));
            }
            var preValidationResult = await ValidateForeignKeyExistence(graduationReq);
            if (preValidationResult != null) return preValidationResult;

            try
            {
                
                var Grad = _unitOfWork.Repository<Graduation>().Add(_mapper.Map<GraduationReq, Graduation>(graduationReq));
                var result = await _unitOfWork.CompleteAsync(User) > 0;
                var message = result ? AppMessage.Done : AppMessage.Error;
                return result ? Ok(new { Message = message }) : BadRequest(new ApiResponse(500, message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(500, ex.Message));
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGraduation(int id, GraduationReq graduationReq)
        {

            var GraduationToUpdate = await _unitOfWork.Repository<Graduation>()
                .GetByIdAsync(id);

            if (GraduationToUpdate == null)
            {
                return NotFound(new ApiResponse(404, $"Graduation with ID {id} not found."));
            }

            var preValidationResult = await ValidateForeignKeyExistence(graduationReq);
            if (preValidationResult != null) return preValidationResult;

            try
            {
                await UpdateRelations(id);
                _mapper.Map(graduationReq, GraduationToUpdate);
                _unitOfWork.Repository<Graduation>().Update(GraduationToUpdate);
                var result = await _unitOfWork.CompleteAsync() > 0;
                var message = result ? AppMessage.Updated : AppMessage.Error;
                return result ? Ok(new { Message = message }) : BadRequest(new ApiResponse(500, message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(500, ex.Message));
            }
        }
        private async Task UpdateRelations(int id)
        {
            var AverageValues = await _unitOfWork.Repository<AverageValue>()
                                .GetAllAsync();
            var g = AverageValues.Where(g => g.Id == id);

            foreach (var grade in g)
            {
                _unitOfWork.Repository<AverageValue>().Delete(grade);
            }
            var Semesters = await _unitOfWork.Repository<GraduationSemesters>()
                                .GetAllAsync();
            var s = Semesters.Where(g => g.Id == id);

            foreach (var semester in s)
            {
                _unitOfWork.Repository<GraduationSemesters>().Delete(semester);
            }
            var levels = await _unitOfWork.Repository<GraduationLevels>()
                                .GetAllAsync();
            var L = levels.Where(g => g.Id == id);

            foreach (var Levels in L)
            {
                _unitOfWork.Repository<GraduationLevels>().Delete(Levels);
            }
            await _unitOfWork.CompleteAsync();

        }

        private async Task<ActionResult> ValidateForeignKeyExistence(GraduationReq graduationReq)
        {
            // Program existence check
            var programExists = await _unitOfWork.Repository<ProgramInformation>().GetByIdAsync(graduationReq.ProgramId) != null;
            if (!programExists)
            {
                return NotFound(new ApiResponse(404, $"Program with ID {graduationReq.ProgramId} not found."));
            }
            // Grades existence check
            if (graduationReq.TheMinimumGradeForTheCourseId.HasValue)
            {

                var GradeExists = await _unitOfWork.Repository<AllGrades>().GetByIdAsync(graduationReq.TheMinimumGradeForTheCourseId.Value) != null;
                if (!GradeExists)
                {
                    return NotFound(new ApiResponse(404, $"Grade with ID {graduationReq.TheMinimumGradeForTheCourseId.Value} not found."));
                }
            }
            

            foreach (var gradeDetail in graduationReq.AverageValues)
            {

                if (gradeDetail.AllGradesId != null) // Check if Grades is not null
                {
                    var gradeExists = await _unitOfWork.Repository<AllGrades>().GetByIdAsync(gradeDetail.AllGradesId) != null;
                    if (!gradeExists)
                    {
                        return NotFound(new ApiResponse(404, $"Grade  with ID {gradeDetail.AllGradesId} not found."));
                    }
                }

            }
            foreach (var gradeDetail in graduationReq.LevelsTobePassed)

            {

                if (gradeDetail.LevelId != null) // Check if Grades is not null
                {
                    var gradeExists = await _unitOfWork.Repository<Level>().GetByIdAsync(gradeDetail.LevelId) != null;
                    if (!gradeExists)
                    {
                        return NotFound(new ApiResponse(404, $"Leverl  with ID {gradeDetail.LevelId} not found."));
                    }
                }

            }
            foreach (var gradeDetail in graduationReq.SemestersTobePssed)

            {

                if (gradeDetail != null) // Check if Grades is not null
                {
                    var gradeExists = await _unitOfWork.Repository<Semesters>().GetByIdAsync(gradeDetail.SemesterId) != null;
                    if (!gradeExists)
                    {
                        return NotFound(new ApiResponse(404, $"Semester  with ID {gradeDetail.SemesterId} not found."));
                    }
                }

            }
            return null;
        }
    }
}
