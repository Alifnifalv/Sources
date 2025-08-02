using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("MemberPartners", Schema = "communities")]
    public partial class MemberPartner
    {
        [Key]
        public long MemberPartnerIID { get; set; }
        public byte? MaritalStatusID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MarriageDate { get; set; }
        [StringLength(500)]
        public string SpousePlace { get; set; }
        [StringLength(500)]
        public string SpouseMahalName { get; set; }
        public long? SpouseMemberID { get; set; }

        [ForeignKey("MaritalStatusID")]
        [InverseProperty("MemberPartners")]
        public virtual MaritalStatus MaritalStatus { get; set; }
        [ForeignKey("SpouseMemberID")]
        [InverseProperty("MemberPartners")]
        public virtual Member SpouseMember { get; set; }
    }
}
