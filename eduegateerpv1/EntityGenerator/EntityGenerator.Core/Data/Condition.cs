using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Conditions", Schema = "setting")]
    public partial class Condition
    {
        public Condition()
        {
            FilterColumnConditionMaps = new HashSet<FilterColumnConditionMap>();
            FilterColumnUserValues = new HashSet<FilterColumnUserValue>();
        }

        [Key]
        public byte ConditionID { get; set; }
        [StringLength(50)]
        public string ConditionName { get; set; }

        [InverseProperty("Conidtion")]
        public virtual ICollection<FilterColumnConditionMap> FilterColumnConditionMaps { get; set; }
        [InverseProperty("Condition")]
        public virtual ICollection<FilterColumnUserValue> FilterColumnUserValues { get; set; }
    }
}
