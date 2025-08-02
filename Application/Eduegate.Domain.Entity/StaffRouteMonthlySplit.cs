namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.StaffRouteMonthlySplit")]
    public partial class StaffRouteMonthlySplit
    {
        [Key]
        public long StaffRouteMonthlySplitIID { get; set; }

        public long? StaffRouteStopMapID { get; set; }

        public long? PickupStopMapID { get; set; }

        public long? DropStopMapID { get; set; }

        public int? AcademicYearID { get; set; }

        public int? MonthID { get; set; }

        public int? Year { get; set; }

        public bool? Status { get; set; }

        public bool? IsExcluded { get; set; }

        public virtual StaffRouteStopMap StaffRouteStopMap { get; set; }
    }
}
