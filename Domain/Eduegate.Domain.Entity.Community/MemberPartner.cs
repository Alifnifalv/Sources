namespace Eduegate.Domain.Entity.Community
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("communities.MemberPartners")]
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
