using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PropertyCultureDatas", Schema = "catalog")]
    public partial class PropertyCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        [Key]
        public long PropertyID { get; set; }
        [StringLength(50)]
        public string PropertyName { get; set; }
        [StringLength(100)]
        public string PropertyDescription { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("PropertyCultureDatas")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("PropertyID")]
        [InverseProperty("PropertyCultureDatas")]
        public virtual Property Property { get; set; }
    }
}
