﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grad.Core.Entities.Academic_regulation;

namespace Grad.Repository.Data.Configrations.ProgramTheGradeConfig
{
    internal class ProgramTheGradeConfig : IEntityTypeConfiguration<Program_TheGrades>
    {
        public void Configure(EntityTypeBuilder<Program_TheGrades> builder)
        {
            builder.HasOne(pi => pi.prog_Info)
                  .WithMany(pi=>pi.Program_TheGrades)
                  .HasForeignKey(pi => pi.prog_InfoId).IsRequired().OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(pi => pi.TheGrade)
                  .WithMany(pi=>pi.program_TheGrades)
                  .HasForeignKey(pi => pi.TheGradeId).IsRequired().OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(pi => pi.EquivalentEstimate)
                  .WithMany(pi=>pi.EquivalentEstimate)
                  .HasForeignKey(pi => pi.EquivalentEstimateId).IsRequired().OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(pi => pi.GraduationEstimate)
                  .WithMany(pi=>pi.GraduationEstimate)
                  .HasForeignKey(pi => pi.GraduationEstimateId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        }

    }
}
