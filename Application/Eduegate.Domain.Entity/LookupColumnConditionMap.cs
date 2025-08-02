namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("setting.LookupColumnConditionMaps")]
    public partial class LookupColumnConditionMap
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long LookupColumnConditionMapID { get; set; }

        public long? ScreenLookupMapID { get; set; }

        public int? DesignationID { get; set; }

        public string Condition { get; set; }

        public virtual Designation Designation { get; set; }

        public virtual ScreenLookupMap ScreenLookupMap { get; set; }
    }
}
