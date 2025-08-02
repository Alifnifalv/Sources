using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Condition
    {
        public Condition()
        {
            this.FilterColumnConditionMaps = new List<FilterColumnConditionMap>();
            this.FilterColumnUserValues = new List<FilterColumnUserValue>();
        }

        public byte ConditionID { get; set; }
        public string ConditionName { get; set; }
        public virtual ICollection<FilterColumnConditionMap> FilterColumnConditionMaps { get; set; }
        public virtual ICollection<FilterColumnUserValue> FilterColumnUserValues { get; set; }
    }
}
