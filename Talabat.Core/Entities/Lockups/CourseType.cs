﻿
using Grad.Core.Entities.CoursesInfo;

namespace Talabat.Core.Entities.Lockups
{
    [Table("LU_CourseType")]
    public class CourseType:BaseEntity
    {
        public string courseType { get; set; }
        public int? UniversityId { get; set; }
        [ForeignKey(nameof(UniversityId))]
        public University University { get; set; }
        public ICollection<CourseInformation> CourseInformation { get; set; } = new HashSet<CourseInformation>();

    }
}
