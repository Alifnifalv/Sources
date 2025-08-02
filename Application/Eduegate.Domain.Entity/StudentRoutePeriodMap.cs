namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.StudentRoutePeriodMaps")]
    public partial class StudentRoutePeriodMap
    {
        [Key]
        public long StudentRoutePeriodMapIID { get; set; }

        public long? StudentRouteStopMapID { get; set; }

        public int? FeePeriodID { get; set; }

        public virtual FeePeriod FeePeriod { get; set; }

        public virtual StudentRouteStopMap StudentRouteStopMap { get; set; }
    }
}
