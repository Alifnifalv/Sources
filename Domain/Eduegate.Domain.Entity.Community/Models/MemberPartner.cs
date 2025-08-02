using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Community.Models
{
    [Table("AssignmentMemberPartnersTypes", Schema = "communities")]
    public partial class MemberPartner
    {
        [Key]
        public long MemberPartnerIID { get; set; }

        public byte? MaritalStatusID { get; set; }

        public DateTime? MarriageDate { get; set; }

        [StringLength(500)]
        public string SpousePlace { get; set; }

        [StringLength(500)]
        public string SpouseMahalName { get; set; }

        public long? SpouseMemberID { get; set; }

        public virtual MaritalStatus MaritalStatus { get; set; }

        public virtual Member Member { get; set; }
    }
}