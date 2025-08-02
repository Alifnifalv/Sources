using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("StudentRouteMonthlySplit_20231203", Schema = "schools")]
    public partial class StudentRouteMonthlySplit_20231203
    {
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
    }
}
