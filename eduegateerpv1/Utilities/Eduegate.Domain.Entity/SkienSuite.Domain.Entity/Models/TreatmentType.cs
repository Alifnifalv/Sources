using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class TreatmentType
    {
        public TreatmentType()
        {
            this.Services = new List<Service>();
        }

        public int TreatmentTypeID { get; set; }
        public string TreatmentName { get; set; }
        public int TreatmentGroupID { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual TreatmentGroup TreatmentGroup { get; set; }
    }
}
