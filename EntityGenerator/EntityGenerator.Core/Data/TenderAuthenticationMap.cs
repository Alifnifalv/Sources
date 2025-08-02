using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TenderAuthenticationMap", Schema = "inventory")]
    public partial class TenderAuthenticationMap
    {
        [Key]
        public long MapIID { get; set; }
        public long? TenderID { get; set; }
        public long? AuthenticationID { get; set; }
        public bool? IsTenderApproved { get; set; }
        public bool? IsTenderOpened { get; set; }
        public string LastOTP { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? OpenedDate { get; set; }
        public string Remarks { get; set; }

        [ForeignKey("AuthenticationID")]
        [InverseProperty("TenderAuthenticationMaps")]
        public virtual TenderAuthentication Authentication { get; set; }
        [ForeignKey("TenderID")]
        [InverseProperty("TenderAuthenticationMaps")]
        public virtual Tender Tender { get; set; }
    }
}
