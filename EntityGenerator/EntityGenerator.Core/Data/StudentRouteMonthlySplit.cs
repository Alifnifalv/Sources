using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentRouteMonthlySplit", Schema = "schools")]
    [Index("StudentRouteStopMapID", "FeePeriodID", Name = "IDX_StudentRouteMonthlySplit_StudentRouteStopMapID__FeePeriodID_MonthID")]
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

        [ForeignKey("FeePeriodID")]
        [InverseProperty("StudentRouteMonthlySplits")]
        public virtual FeePeriod FeePeriod { get; set; }
        [ForeignKey("StudentRouteStopMapID")]
        [InverseProperty("StudentRouteMonthlySplits")]
        public virtual StudentRouteStopMap StudentRouteStopMap { get; set; }
    }
}
