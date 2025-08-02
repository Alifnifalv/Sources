using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductFamilyTypes", Schema = "catalog")]
    public partial class ProductFamilyType
    {
        [Key]
        public int FamilyTypeID { get; set; }
        [Key]
        public byte CultureID { get; set; }
        [StringLength(50)]
        public string FamilyTypeName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("ProductFamilyTypes")]
        public virtual Culture Culture { get; set; }
    }
}
