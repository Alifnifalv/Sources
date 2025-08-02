using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class TreatmentGroup
    {
        public TreatmentGroup()
        {
            this.TreatmentTypes = new List<TreatmentType>();
        }

        public int TreatmentGroupID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<TreatmentType> TreatmentTypes { get; set; }
    }
}
