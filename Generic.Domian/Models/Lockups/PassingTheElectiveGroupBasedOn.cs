﻿using Generic.Domian.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Domian.Models.Lockups
{
    [Table("LU_PassingTheElectiveGroupBasedOn")]
    public class PassingTheElectiveGroupBasedOn: BaseEntity
    {
        public string PassingTheElectiveGroup { get; set; }

         public int? UniversityId { get; set; }
        [ForeignKey(nameof(UniversityId))]
        public University University { get; set; }

    }
}