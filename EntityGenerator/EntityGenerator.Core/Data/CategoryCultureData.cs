using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CategoryCultureDatas", Schema = "catalog")]
    public partial class CategoryCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        [Key]
        public long CategoryID { get; set; }
        [StringLength(100)]
        public string CategoryName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CategoryID")]
        [InverseProperty("CategoryCultureDatas")]
        public virtual Category Category { get; set; }
        [ForeignKey("CultureID")]
        [InverseProperty("CategoryCultureDatas")]
        public virtual Culture Culture { get; set; }
    }
}
