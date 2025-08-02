using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ClaimLoginMaps", Schema = "admin")]
    public partial class ClaimLoginMap
    {
        [Key]
        public long ClaimLoginMapIID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<long> LoginID { get; set; }
        public Nullable<long> ClaimID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual Claim Claim { get; set; }
        public virtual Company Company { get; set; }
        public virtual Login Login { get; set; }
    }
}
