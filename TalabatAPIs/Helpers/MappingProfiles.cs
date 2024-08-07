﻿using AutoMapper;
using Grad.APIs.DTO;
using Grad.APIs.DTO.Entities_Dto;
using Grad.APIs.DTO.Entities_Dto.AcademicLoadAccordingToLevel;
using Grad.APIs.DTO.Entities_Dto.Cumulative_Average;
using Grad.APIs.DTO.Entities_Dto.Graduation;
using Grad.APIs.DTO.Entities_Dto.Program_TheGrade;
using Grad.APIs.DTO.Entities_Dto.ProgramLEvelsDTO;
using Grad.APIs.DTO.Lockups_Dto;
using Grad.APIs.DTO.ProgrmInformation;
using Grad.Core.Entities.Academic_regulation;
using Grad.Core.Entities.Control;
using Grad.Core.Entities.Control;
using Grad.Core.Entities.Control;
using Grad.Core.Entities.Control;
using Grad.Core.Entities.CoursesInfo;
using Grad.Core.Entities.CumulativeAverage;
using Grad.Core.Entities.Entities;
using Grad.Core.Entities.Graduation;
using Grad.Core.Entities.Lockups;
using Talabat.APIs.DTO;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Academic_regulation;
using Talabat.Core.Entities.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Lockups;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {

            CreateMap<AppUser , UsersReturn>().ReverseMap();

            CreateMap<CollegeCourses, CollegeCoursesDTO>()
          .ForMember(D => D.Faculty, O => O.MapFrom(s => s.Faculty.FacultyName)).ReverseMap();

            CreateMap<CollegeCoursesReq, CollegeCourses>();

            CreateMap<University,UniversityDTO>();
            CreateMap<UniversityReq, University>();
            // Configuration
            #region Grduation
            CreateMap<GraduationReq, Graduation>();
            CreateMap<AverageValueReq, AverageValue>();
            CreateMap<GraduationLevelsReq, GraduationLevels>();
            CreateMap<GraduationSemestersReq, GraduationSemesters>();
            
            CreateMap<Graduation, GraduationDTO>()
                .ForMember(D => D.TheMinimumGradeForTheCourse, O => O.MapFrom(s => s.Grades.TheGrade))
                .ForMember(D => D.GraduationLevels , o => o.MapFrom(s => string.Join(",", s.LevelsTobePassed.Select(gl => gl.Level.levels))))
                .ForMember(D => D.GraduationSemesters, o => o.MapFrom(s => string.Join(",", s.SemestersTobePssed.Select(gl => gl.semesters.semesters))))
                .ReverseMap()
                ;

            CreateMap<AverageValue, AverageValueDTO>()
                .ForMember(D => D.EquivalentGrade, O => O.MapFrom(s => s.EquivalentGrade.equivalentGrade))
                .ForMember(D => D.Grades, O => O.MapFrom(s =>  s.AllGrades.TheGrade));

            CreateMap<GraduationLevels, GraduationLevelsDTO>()
                .ForMember(D => D.Level, O => O.MapFrom(s => s.Level.levels)).ReverseMap();
            CreateMap<GraduationSemesters, GraduationSemestersDTO>()
          .ForMember(D => D.Semester, O => O.MapFrom(s => s.semesters.semesters)).ReverseMap();
            #endregion
            #region CumulativeAverage

            CreateMap<CumulativeAverage, CumulativeAverageDTO>()
                .ForMember(D => D.UtmostGrade, O => O.MapFrom(s => s.Grades.TheGrade))
                .ForMember(D => D.GadesOfEstimatesThatDoesNotCount, o => o.MapFrom(s => string.Join(",", s.GadesOfEstimatesThatDoesNotCount.Select(gl => gl.Grades.TheGrade))))

                .ReverseMap();

            CreateMap<GadesOfEstimatesThatDoesNotCount, GadesOfEstimatesThatDoesNotCountDTO>()
                .ForMember(D => D.Grade, O => O.MapFrom(s => s.Grades.TheGrade)).ReverseMap();

            CreateMap<GadesOfEstimatesThatDoesNotCountReq, GadesOfEstimatesThatDoesNotCount>();

            CreateMap<CumulativeAverageReq, CumulativeAverage>();

            #endregion



            #region programLevels

            CreateMap<programLevels, ProgramLevelResponseDto>()
                .ForMember(d => d.TheLevel, o => o.MapFrom(s => s.TheLevel.levels));
            CreateMap<ProgramLevelRequestDto, programLevels>();
            #endregion

            #region Programs
            CreateMap<Programs, ProgramDTO>()
                   .ForMember(d => d.Faculty, o => o.MapFrom(s => s.Faculty.FacultyName));

            CreateMap<ProgramReqDTO, Programs>();
            #endregion



            #region AcademicLoad
            CreateMap<AcademicLoadAccordingToLevel, AcademicLoadAccordingToLevelDTO>()
                    .ForMember(d => d.AcademicLevel, o => o.MapFrom(s => s.AcademicLevel.levels))
                    .ForMember(d => d.AL_Semesters, o => o.MapFrom(s => s.AL_Semesters.semesters));

            CreateMap<AcademicLoadAccordingToLevelReqDTO, AcademicLoadAccordingToLevel>();
            #endregion

            #region ProgramInformation
            CreateMap<ProgramInformation, ProgramInformationDTO>()
                .ForMember(d => d.AcademicDegreeId, O => O.MapFrom(s => s.AcademicDegree.AcademicDegreeName))
                .ForMember(d => d.SystemTypeId, O => O.MapFrom(s => s.SystemType.SystemName))
                .ForMember(d => d.BurdanCalculationId, O => O.MapFrom(s => s.BurdenCalculation.BurdenCalculationAS))
                .ForMember(d => d.PassingTheElectiveGroupBasedOnId, O => O.MapFrom(s => s.PassingTheElectiveGroupBasedOn.PassingTheElectiveGroup))
                .ForMember(d => d.PrerequisitesProgramsId, o => o.MapFrom(s => s.PrerequisitesPrograms.ProgramNameInArabic))
                .ForMember(d => d.EditTheStudentLevelId, O => O.MapFrom(s => s.EditTheStudentLevel.editTheStudentLevel))
                .ForMember(d => d.BlockingProofOfRegistrationId, O => O.MapFrom(s => s.BlockingProofOfRegistration.ReasonsOfBlocking))
                .ForMember(d => d.TypeOfFinancialStatementInTheProgramId, O => O.MapFrom(s => s.TypeOfFinancialStatementInTheProgram.TheType))
                .ForMember(d => d.TypeOfProgramFeesId, O => O.MapFrom(s => s.TypeOfProgramFees.TypeOfFees))
                .ForMember(d => d.TypeOfSummerFeesId, O => O.MapFrom(s => s.TypeOfSummerFees.TheTypeOfSummerFees))
                .ForMember(d => d.TheResultAppearsId, O => O.MapFrom(s => s.TheResultAppears.ResultAppears))
                .ForMember(d => d.TheResultToTheGuidId, O => O.MapFrom(s => s.TheResultToTheGuid.ResultAppears))
                .ForMember(d => d.ReasonForBlockingRegistrationId, O => O.MapFrom(s => s.ReasonForBlockingRegistration.TheReasonForBlockingRegistration))
                .ForMember(d => d.TheReasonForHiddingTheResultId, O => O.MapFrom(s => s.TheReasonForHiddingTheResult.TheReasonForBlockingAcademicResult))
                .ForMember(d => d.FacultyId, o => o.MapFrom(s => s.Institue.FacultyName))
                .ForMember(D => D.pI_DivisionTypes, o => o.MapFrom(s => string.Join(",", s.pI_DivisionTypes.Select(gl => gl.DivisionType.Division_Type))))
                .ForMember(D => D.PI_EstimatesOfCourseFeeExemptions, o => o.MapFrom(s => string.Join(",", s.PI_EstimatesOfCourseFeeExemptions.Select(gl => gl.AllGrades.TheGrade))))
                .ForMember(D => D.pI_DetailedGradesToBeAnnounced, o => o.MapFrom(s => string.Join(",", s.pI_DetailedGradesToBeAnnounced.Select(gl => gl.GradesDetails.TheDetails))))
                .ForMember(D => D.pI_AllGradesSummerEstimates, o => o.MapFrom(s => string.Join(",", s.pI_AllGradesSummerEstimates.Select(gl => gl.AllGrades.TheGrade)))).ReverseMap();




            CreateMap<PI_DivisionType, PI_DivisionTypeDTO>()
                .ForMember(d => d.DivisionType, O => O.MapFrom(s => s.DivisionType.Division_Type));

            CreateMap<PI_AllGradesSummerEstimate, PI_AllGradesSummerEstimateDTO>()
                .ForMember(d => d.AllGrades, O => O.MapFrom(s => s.AllGrades.TheGrade));

            CreateMap<PI_DetailedGradesToBeAnnounced, PI_DetailedGradesToBeAnnouncedDTO>()
                .ForMember(d => d.GradesDetails, O => O.MapFrom(s => s.GradesDetails.TheDetails));

            CreateMap<PI_EstimatesOfCourseFeeExemption, PI_EstimatesOfCourseFeeExemptionDTO>()
                .ForMember(d => d.AllGrades_Etomate, O => O.MapFrom(s => s.AllGrades.TheGrade));

            CreateMap<ProgramInformationReqDTO, ProgramInformation>();
            CreateMap<PI_DivisionTypeReqDTO, PI_DivisionType>();
            CreateMap<PI_EstimatesOfCourseFeeExemptionReqDTO, PI_EstimatesOfCourseFeeExemption>();
            CreateMap<PI_AllGradesSummerEstimateReqDTO, PI_AllGradesSummerEstimate>();
            CreateMap<PI_DetailedGradesToBeAnnouncedReqDTO,PI_DetailedGradesToBeAnnounced>();



            #endregion

            #region Program the grade
            CreateMap<Program_TheGrades, Program_TheGradeDTO>()
                .ForMember(d => d.TheGrade, o=>o.MapFrom(s=>s.TheGrade.TheGrade))
                .ForMember(d=>d.EquivalentEstimate, o=>o.MapFrom(s=>s.EquivalentEstimate.equivalentGrade))
                .ForMember(d=>d.GraduationEstimate,o=>o.MapFrom(s=>s.GraduationEstimate.equivalentGrade));

            CreateMap<Program_TheGradeReqDTO, Program_TheGrades>().ReverseMap();

            #endregion


            #region Control
            CreateMap<ControlReq, Control>();
            CreateMap<ACaseOfAbsenceInTheDetailedGradesReq, ACaseOfAbsenceInTheDetailedGrades>();
            CreateMap<ASuccessRatingDoesNotAddHoursOrAverageReq, ASuccessRatingDoesNotAddHoursOrAverage>();
            CreateMap<DetailsOfExceptionalLettersReq, DetailsOfExceptionalLetters>();
            CreateMap<DetailsOfTheoreticalFailingGradesReq, DetailsOfTheoreticalFailingGrades>();
            CreateMap<EstimatesNotDefinedInTheListReq, EstimatesNotDefinedInTheList>();
            CreateMap<ExceptionalLetterGradesReq, ExceptionalLetterGrades>();
            CreateMap<FailureEstimatesInTheListReq, FailureEstimatesInTheList>();


            CreateMap<Control, ControlDTO>()
                  .ForMember(c => c.FirstReductionEstimatesForFailureTimes, O => O.MapFrom(s => s.FirstGrades.TheGrade))
                  .ForMember(c => c.SecondReductionEstimatesForFailureTimes, O => O.MapFrom(s => s.SecondGrades.TheGrade))
                  .ForMember(c => c.ThirdReductionEstimatesForFailureTimes, O => O.MapFrom(s => s.ThirdGrades.TheGrade))
                  .ForMember(c => c.EstimatingTheTheoreticalFailure, O => O.MapFrom(s => s.TheoriticalFailure.TheGrade))
                  .ForMember(c => c.EstimateDeprivationBeforeTheExam, O => O.MapFrom(s => s.EstimateDeprivationBeforeTheExam.TheGrade))
                  .ForMember(c => c.EstimateDeprivationAfterTheExam, O => O.MapFrom(s => s.EstimateDeprivationAfterTheExam.TheGrade))
                  .ForMember(D => D.FailureEstimatesInTheLists, o => o.MapFrom(s => string.Join(",", s.FailureEstimatesInTheLists.Select(gl => gl.grades.TheGrade))))
                  .ForMember(D => D.ACaseOfAbsenceInTheDetailedGrades, o => o.MapFrom(s => string.Join(",", s.ACaseOfAbsenceInTheDetailedGrades.Select(gl => gl.GradesDetails.TheDetails))))
                  .ForMember(D => D.DetailsOfExceptionalLetters, o => o.MapFrom(s => string.Join(",", s.DetailsOfExceptionalLetters.Select(gl => gl.GradesDetails.TheDetails))))
                  .ForMember(D => D.EstimatesNotDefinedInTheLists, o => o.MapFrom(s => string.Join(",", s.EstimatesNotDefinedInTheLists.Select(gl => gl.Grades.TheGrade))))
                  .ForMember(D => D.ASuccessRatingDoesNotAddHours, o => o.MapFrom(s => string.Join(",", s.ASuccessRatingDoesNotAddHours.Select(gl => gl.Grades.TheGrade))))
                  .ReverseMap();
            CreateMap<ACaseOfAbsenceInTheDetailedGrades, ACaseOfAbsenceInTheDetailedGradesDTO>()
                .ForMember(c => c.GradeGetail, O => O.MapFrom(s => s.GradesDetails.TheDetails)).ReverseMap();
            CreateMap<ASuccessRatingDoesNotAddHoursOrAverage, ASuccessRatingDoesNotAddHoursOrAverageDTO>()
             .ForMember(c => c.Grade, O => O.MapFrom(s => s.Grades.TheGrade)).ReverseMap();
            CreateMap<DetailsOfExceptionalLetters, DetailsOfExceptionalLettersDTO>()
             .ForMember(c => c.GradeDetail, O => O.MapFrom(s => s.GradesDetails.TheDetails)).ReverseMap();
            CreateMap<DetailsOfTheoreticalFailingGrades, DetailsOfTheoreticalFailingGradesDTO>()
             .ForMember(c => c.GradeDetail, O => O.MapFrom(s => s.GradesDetails.TheDetails)).ReverseMap();
            CreateMap<EstimatesNotDefinedInTheList, EstimatesNotDefinedInTheListDTO>()
             .ForMember(c => c.Grade, O => O.MapFrom(s => s.Grades.TheGrade)).ReverseMap();
            CreateMap<ExceptionalLetterGrades, ExceptionalLetterGradesDTO>()
             .ForMember(c => c.Grade, O => O.MapFrom(s => s.Grades.TheGrade)).ReverseMap();
            CreateMap<FailureEstimatesInTheList, FailureEstimatesInTheListDTO>()
             .ForMember(c => c.Grade, O => O.MapFrom(s => s.grades.TheGrade)).ReverseMap();
            #endregion


            #region CourseInfo
            CreateMap<CourseInfoDTO, CourseInformation>();
            CreateMap<CoursesAndGradesDetailsDTO, CoursesandGradesDetails>();
            CreateMap<PreRequisiteCoursesDTO, PreRequisiteCourses>();

            CreateMap<DetailsOfFailingGradesDTO, DetailsOfFailingGrades>();
            CreateMap<CoursesAndHoursDTO, CoursesAndHours>();

            #endregion



            #region  Maps For Lockups with UNI

            CreateMap<Faculty, FacultyDTO>()
             .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();


            CreateMap<AllGrades, AllGradesDTO>()
            .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();

         

            CreateMap<Level, LevelsDTO>()
            .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();

            CreateMap<Hours, HoursDTO>()
           .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();

            CreateMap<TypeOfSummerFees, TypeOfSummerFeesDTO>()
             .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();

            CreateMap<TypeOfStudySection, TypeOfStudySectionDTO>()
             .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();

            CreateMap<TypeOfProgramFees, TypeOfProgramFeesDTO>()
             .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();

            CreateMap<TheResultAppears, TheResultAppearsDTO>()
             .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();

            CreateMap<Semesters, SemestersDTO>()
             .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();


            CreateMap<ReasonForBlockingRegistration, ReasonForBlockingRegistrationDTO>()
             .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();


            CreateMap<ReasonForBlockingAcademicResult, ReasonForBlockingAcademicResultDTO>()
             .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();

            CreateMap<PreviousQualification, PreviousQualificationDTO>()
             .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();


            CreateMap<Prerequisites, PrerequisitesDTO>()
             .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();

            CreateMap<AbsenteeEstimateCalculation, AbsenteeEstimateCalculationDTO>()
             .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();

            CreateMap<CourseRequirement, CourseRequirementDTO>()
              .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();

            CreateMap<CourseType, CourseTypeDTO>()
              .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();

            CreateMap<EquivalentGrade, EquivalentGradeDTO>()
            .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();

            CreateMap<BlockingProofOfRegistration, BlockingProofOfRegistrationDTO>()
             .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();

            CreateMap<BurdenCalculation, BurdenCalculationDTO>()
             .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();

            CreateMap<DivisionType, DivisionTypeDTO>()
             .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();


            CreateMap<EditTheStudentLevel, EditTheStudentLevelDTO>()
             .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();

            CreateMap<GradesDetails, GradesDetailsDTO>()
              .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();

            CreateMap<PassingTheElectiveGroupBasedOn, PassingTheElectiveGroupBasedOnDTO>()
               .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();

            CreateMap<SystemType, SystemTypeDTO>()
             .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();

            CreateMap<TheAcademicDegree, TheAcademicDegreeDTO>()
            .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();

            CreateMap<TypeOfFinancialStatementInTheProgram, TypeOfFinancialStatementInTheProgramDTO>()
             .ForMember(D => D.University, O => O.MapFrom(s => s.University.Name)).ReverseMap();

        
            #endregion


            #region Maps For Lockups Request

            CreateMap<AllGradesReq, AllGrades>();
            CreateMap<HoursReq, Hours>();
            CreateMap<LevelsReq, Level>();
            CreateMap<TypeOfSummerFeesReq, TypeOfSummerFees>();
            CreateMap<TypeOfStudySectionReq, TypeOfStudySection>();
            CreateMap<TypeOfFeesReq, TypeOfProgramFees>();
            CreateMap<TheResultAppearsReq, TheResultAppears>();
            CreateMap<SemestersReq, Semesters>();
            CreateMap<ReasonForBlockingRegistrationReq, ReasonForBlockingRegistration>();
            CreateMap<ReasonForBlockingAcademicResultReq, ReasonForBlockingAcademicResult>();
            CreateMap<PreviousQualificationReq, PreviousQualification>();
            CreateMap<PrerequisitesReq, Prerequisites>();
            CreateMap<AbsenteeEstimateCalculationReq, AbsenteeEstimateCalculation>();
            CreateMap<CourseRequirementReq, CourseRequirement>();
            CreateMap<CourseTypeReq, CourseType>();
            CreateMap<CourseTypeReq, CourseType>();
            CreateMap<EquivalentGradeReq, EquivalentGrade>();
            CreateMap<BlockingProofOfRegistrationReq, BlockingProofOfRegistration>();
            CreateMap<BurdenCalculationReq, BurdenCalculation > ();
            CreateMap<DivisionTypeReq, DivisionType>();
            CreateMap<EditTheStudentLevelReq, EditTheStudentLevel>();
            CreateMap<GradesDetailsReq, GradesDetails>();
            CreateMap<PassingTheElectiveGroupBasedOnReq, PassingTheElectiveGroupBasedOn>();
            CreateMap<SystemTypeReq,SystemType>();
            CreateMap<TheAcademicDegreeReq, TheAcademicDegree>();
            CreateMap<TypeOfFinancialStatementInTheProgramReq, TypeOfFinancialStatementInTheProgram>();

            CreateMap<FacultyReq, Faculty>();














            #endregion

            


        }
    }
}
