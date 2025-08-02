namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("marketing.CampaignEmployeeMaps")]
    public partial class CampaignEmployeeMap
    {
        [Key]
        public long CompaignEmployeeMapIID { get; set; }

        public long? EmployeeID { get; set; }

        public long? CompaignID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual SocailMediaCampaign SocailMediaCampaign { get; set; }
    }
}
