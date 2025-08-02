namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("saloon.Saloons")]
    public partial class Saloon
    {
        [Key]
        public long SaloonIID { get; set; }

        [StringLength(500)]
        public string SaloonName { get; set; }

        public long? BranchID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Branch Branch { get; set; }
    }
}
