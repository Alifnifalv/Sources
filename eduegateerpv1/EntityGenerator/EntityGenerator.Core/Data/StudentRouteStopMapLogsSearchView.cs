using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentRouteStopMapLogsSearchView
    {
        [StringLength(100)]
        public string RouteCode { get; set; }
        public int RouteID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? Stud_Count { get; set; }
        public int? Staff_Count { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedDate { get; set; }
        public int? UpdatedDate { get; set; }
        public int? CreatedUserName { get; set; }
        public int? UpdatedUserName { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        [StringLength(123)]
        public string AcademicYear { get; set; }
    }
}
