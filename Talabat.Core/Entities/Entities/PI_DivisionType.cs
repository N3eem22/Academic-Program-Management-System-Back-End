﻿using Grad.Core.Entities.Lockups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grad.Core.Entities.Entities
{
    public class PI_DivisionType
    {
        [ForeignKey("DivisionType")]
        public int DivisionTypeId { get; set; }
        [ForeignKey("ProgramInformation")]
        public int ProgramInformationId { get; set; }
        public DivisionType DivisionType { get; set; }
        public ProgramInformation ProgramInformation { get; set; }
    }
}