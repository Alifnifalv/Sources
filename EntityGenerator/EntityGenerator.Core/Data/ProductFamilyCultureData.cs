using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductFamilyCultureDatas", Schema = "catalog")]
    public partial class ProductFamilyCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        [Key]
        public long ProductFamilyID { get; set; }
        [StringLength(100)]
        public string FamilyName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("ProductFamilyCultureDatas")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("ProductFamilyID")]
        [InverseProperty("ProductFamilyCultureDatas")]
        public virtual ProductFamily ProductFamily { get; set; }
    }
}
