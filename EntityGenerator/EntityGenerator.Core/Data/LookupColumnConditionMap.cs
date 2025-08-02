using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LookupColumnConditionMaps", Schema = "setting")]
    public partial class LookupColumnConditionMap
    {
        [Key]
        public long LookupColumnConditionMapID { get; set; }
        public long? ScreenLookupMapID { get; set; }
        public int? DesignationID { get; set; }
        public string Condition { get; set; }

        [ForeignKey("DesignationID")]
        [InverseProperty("LookupColumnConditionMaps")]
        public virtual Designation Designation { get; set; }
        [ForeignKey("ScreenLookupMapID")]
        [InverseProperty("LookupColumnConditionMaps")]
        public virtual ScreenLookupMap ScreenLookupMap { get; set; }
    }
}
