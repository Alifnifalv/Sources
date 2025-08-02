namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.SiteCountryMaps")]
    public partial class SiteCountryMaps1
    {
        [Key]
        public int SiteCountryMapIID { get; set; }

        public int SiteID { get; set; }

        public int CountryID { get; set; }
    }
}
