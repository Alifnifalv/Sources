using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SiteCountryMaps", Schema = "cms")]
    public partial class SiteCountryMap
    {
        [Key]
        public int SiteCountryMapIID { get; set; }
        public int SiteID { get; set; }
        public int CountryID { get; set; }
        public bool? IsLocal { get; set; }

        [ForeignKey("CountryID")]
        [InverseProperty("SiteCountryMaps")]
        public virtual Country Country { get; set; }
        [ForeignKey("SiteID")]
        [InverseProperty("SiteCountryMaps")]
        public virtual Site Site { get; set; }
    }
}
