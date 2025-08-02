using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SiteCountryMap
    {
        [Key]
        public int SiteCountryMapIID { get; set; }
        public int SiteID { get; set; }
        public int CountryID { get; set; }
        public bool IsLocal { get; set; }
        public virtual Country Country { get; set; }
        public virtual Site Site { get; set; }
    }
}
