using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EntityTypePaymentMethodMaps", Schema = "mutual")]
    public partial class EntityTypePaymentMethodMap
    {
        [Key]
        public long EntityTypePaymentMethodMapIID { get; set; }
        public int? EntityTypeID { get; set; }
        public short? PaymentMethodID { get; set; }
        public long? ReferenceID { get; set; }
        public long? EntityPropertyID { get; set; }
        public int? EntityPropertyTypeID { get; set; }
        [StringLength(50)]
        public string AccountName { get; set; }
        [StringLength(50)]
        public string AccountID { get; set; }
        [StringLength(50)]
        public string BankName { get; set; }
        [StringLength(50)]
        public string BankBranch { get; set; }
        [StringLength(50)]
        public string IBANCode { get; set; }
        [StringLength(50)]
        public string SWIFTCode { get; set; }
        [StringLength(50)]
        public string IFSCCode { get; set; }
        [StringLength(50)]
        public string NameOnCheque { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("EntityPropertyTypeID")]
        [InverseProperty("EntityTypePaymentMethodMaps")]
        public virtual EntityPropertyType EntityPropertyType { get; set; }
        [ForeignKey("EntityTypeID")]
        [InverseProperty("EntityTypePaymentMethodMaps")]
        public virtual EntityType EntityType { get; set; }
        [ForeignKey("PaymentMethodID")]
        [InverseProperty("EntityTypePaymentMethodMaps")]
        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
