namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.AdditionalExpenseProvisionalAccountMaps")]
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

        public bool? Isdefault { get; set; }
    }
}
