namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductFamilyCultureDatas")]
    public partial class ProductFamilyCultureData
    {
        [Key]
        [Column(Order = 0)]
        public byte CultureID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ProductFamilyID { get; set; }

        [StringLength(100)]
        public string FamilyName { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual ProductFamily ProductFamily { get; set; }

        public virtual Culture Culture { get; set; }
    }
}
