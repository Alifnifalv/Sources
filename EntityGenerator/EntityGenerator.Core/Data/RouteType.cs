using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("RouteTypes", Schema = "schools")]
    public partial class RouteType
    {
        public RouteType()
        {
            Route1 = new HashSet<Route1>();
        }

        [Key]
        public byte RouteTypeID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public bool? IsVisible { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("RouteTypes")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("RouteTypes")]
        public virtual School School { get; set; }
        [InverseProperty("RouteType")]
        public virtual ICollection<Route1> Route1 { get; set; }
    }
}
