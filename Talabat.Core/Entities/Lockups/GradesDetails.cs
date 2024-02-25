﻿

using Grad.Core.Entities.CoursesInfo;

namespace Talabat.Core.Entities.Lockups
{
    [Table("LU_GradesDetails")]
    public class GradesDetails:BaseEntity
    {
        public string TheDetails { get; set; }
        public int? UniversityId { get; set; }
        [ForeignKey(nameof(UniversityId))]
        public University University { get; set; }
        public ICollection<CoursesandGradesDetails> coursesandGradesDetails { get; set; } = new HashSet<CoursesandGradesDetails>();
        public ICollection<DetailsOfFailingGrades> detailsOfFailingGrades { get; set; } = new HashSet<DetailsOfFailingGrades>();

    }
}