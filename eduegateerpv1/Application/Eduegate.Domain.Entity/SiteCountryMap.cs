namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cms.SiteCountryMaps")]
    public partial class SiteCountryMap
    {
        [Key]
        public int SiteCountryMapIID { get; set; }

        public int SiteID { get; set; }

        public int CountryID { get; set; }

        public bool? IsLocal { get; set; }

        public virtual Country Country { get; set; }

        public virtual Site Site { get; set; }
    }
}
