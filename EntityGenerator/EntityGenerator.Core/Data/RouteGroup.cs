using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("RouteGroups", Schema = "schools")]
    public partial class RouteGroup
    {
        public RouteGroup()
        {
            Route1 = new HashSet<Route1>();
        }

        [Key]
        public int RouteGroupID { get; set; }
        public string Description { get; set; }
        public int? AcademicYearID { get; set; }
        public bool? IsActive { get; set; }
        public byte? SchoolID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("RouteGroups")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("RouteGroups")]
        public virtual School School { get; set; }
        [InverseProperty("RouteGroup")]
        public virtual ICollection<Route1> Route1 { get; set; }
    }
}
