using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CustomerCards", Schema = "mutual")]
    public partial class CustomerCard
    {
        [Key]
        public long CustomerCardIID { get; set; }
        public int? CardTypeID { get; set; }
        public long? LoginID { get; set; }
        public long? CustomerID { get; set; }
        [StringLength(50)]
        public string CardNumber { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(50)]
        public string ExternalCode1 { get; set; }
        [StringLength(50)]
        public string ExternalCode2 { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey("CardTypeID")]
        [InverseProperty("CustomerCards")]
        public virtual CardType CardType { get; set; }
        [ForeignKey("CustomerID")]
        [InverseProperty("CustomerCards")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("LoginID")]
        [InverseProperty("CustomerCards")]
        public virtual Login Login { get; set; }
    }
}
