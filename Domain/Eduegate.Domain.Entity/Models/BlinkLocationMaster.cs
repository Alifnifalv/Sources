using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BlinkLocationMaster
    {
        public BlinkLocationMaster()
        {
            this.BlinkPoGrnMasters = new List<BlinkPoGrnMaster>();
        }

        [Key]
        public short BlinkLocationMasterID { get; set; }
        public string BlinkLocationMasterCode { get; set; }
        public string BlinkLocationMasterName { get; set; }
        public long RefCountryID { get; set; }
        public bool BlinkLocationMasterActive { get; set; }
        public short UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public virtual CountryMaster CountryMaster { get; set; }
        public virtual ICollection<BlinkPoGrnMaster> BlinkPoGrnMasters { get; set; }
    }
}
