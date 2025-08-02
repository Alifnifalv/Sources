using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SiteCountryMaps", Schema = "mutual")]
    public partial class SiteCountryMap1
    {
        [Key]
        public int SiteCountryMapIID { get; set; }
        public int SiteID { get; set; }
        public int CountryID { get; set; }
    }
}
