using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ClaimSetLoginMaps", Schema = "admin")]
    public partial class ClaimSetLoginMap
    {
        [Key]
        public long ClaimSetLoginMapIID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<long> LoginID { get; set; }
        public Nullable<long> ClaimSetID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual ClaimSet ClaimSet { get; set; }
        public virtual Company Company { get; set; }
        public virtual Login Login { get; set; }
    }
}
