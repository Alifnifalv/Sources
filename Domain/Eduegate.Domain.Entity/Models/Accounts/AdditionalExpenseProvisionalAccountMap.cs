namespace Eduegate.Domain.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AdditionalExpenseProvisionalAccountMaps", Schema = "account")]
    public partial class AdditionalExpenseProvisionalAccountMap
    {
        [Key]
        public long AdditionalExpProvAccountMapIID { get; set; }

        public long? AdditionalExpenseID { get; set; }

        public long? AccountID { get; set; }

        public long? ProvisionalAccountID { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool? IsDefault { get; set; }
    }
}
