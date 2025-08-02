namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.StudentRouteMonthlySplit")]
    public partial class StudentRouteMonthlySplit
    {
        [Key]
        public long StudentRouteMonthlySplitIID { get; set; }

        public long? StudentRouteStopMapID { get; set; }

        public long? PickupStopMapID { get; set; }

        public long? DropStopMapID { get; set; }

        public int? AcademicYearID { get; set; }

        public int? MonthID { get; set; }

        public int? Year { get; set; }

        public bool? Status { get; set; }

        public bool? IsExcluded { get; set; }

        public bool IsCollected { get; set; }

        public int? FeePeriodID { get; set; }

        public virtual FeePeriod FeePeriod { get; set; }

        public virtual StudentRouteStopMap StudentRouteStopMap { get; set; }
    }
}
