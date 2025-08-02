using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("MaritalStatuses", Schema = "communities")]
    public partial class MaritalStatus
    {
        public MaritalStatus()
        {
            MemberPartners = new HashSet<MemberPartner>();
        }

        [Key]
        public byte MaritalStatusID { get; set; }
        [StringLength(100)]
        public string StatusDescription { get; set; }

        [InverseProperty("MaritalStatus")]
        public virtual ICollection<MemberPartner> MemberPartners { get; set; }
    }
}
