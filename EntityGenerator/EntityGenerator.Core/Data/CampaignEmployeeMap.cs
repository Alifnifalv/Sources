using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CampaignEmployeeMaps", Schema = "marketing")]
    public partial class CampaignEmployeeMap
    {
        [Key]
        public long CompaignEmployeeMapIID { get; set; }
        public long? EmployeeID { get; set; }
        public long? CompaignID { get; set; }

        [ForeignKey("CompaignID")]
        [InverseProperty("CampaignEmployeeMaps")]
        public virtual SocailMediaCampaign Compaign { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("CampaignEmployeeMaps")]
        public virtual Employee Employee { get; set; }
    }
}
