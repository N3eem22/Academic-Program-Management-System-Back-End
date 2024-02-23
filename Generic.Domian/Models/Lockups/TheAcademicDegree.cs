﻿using Generic.Domian.Models.Academic_regulation;
using Generic.Domian.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Domian.Models.Lockups
{
    [Table("LU_TheAcademicDegree")]
    public class TheAcademicDegree : BaseEntity
    {
        
        public string AcademicDegreeName { get; set; }

        public int? UniversityId { get; set; }
        [ForeignKey(nameof(UniversityId))]

        public University University { get; set; }

         public int? ProgramInformationId { get; set; }
        [ForeignKey(nameof(ProgramInformationId))]

        public ProgramInformation ProgramInformation { get; set; }


    }
}
