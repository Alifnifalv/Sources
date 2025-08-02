namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.EntityTypePaymentMethodMaps")]
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual EntityPropertyType EntityPropertyType { get; set; }

        public virtual EntityType EntityType { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
