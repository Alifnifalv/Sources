namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.Accounts_Reference")]
    public partial class Accounts_Reference
    {
        [Key]
        public int Ref_ID { get; set; }

        [Required]
        [StringLength(2500)]
        public string Ref_No { get; set; }

        [Required]
        [StringLength(2500)]
        public string Ref_No1 { get; set; }

        [Required]
        [StringLength(2500)]
        public string Ref_No2 { get; set; }
    }
}
