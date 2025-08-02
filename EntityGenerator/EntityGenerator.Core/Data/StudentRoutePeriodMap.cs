using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentRoutePeriodMaps", Schema = "schools")]
    [Index("StudentRouteStopMapID", Name = "IDX_StudentRoutePeriodMaps_StudentRouteStopMapID_")]
    public partial class StudentRoutePeriodMap
    {
        [Key]
        public long StudentRoutePeriodMapIID { get; set; }
        public long? StudentRouteStopMapID { get; set; }
        public int? FeePeriodID { get; set; }

        [ForeignKey("FeePeriodID")]
        [InverseProperty("StudentRoutePeriodMaps")]
        public virtual FeePeriod FeePeriod { get; set; }
        [ForeignKey("StudentRouteStopMapID")]
        [InverseProperty("StudentRoutePeriodMaps")]
        public virtual StudentRouteStopMap StudentRouteStopMap { get; set; }
    }
}
