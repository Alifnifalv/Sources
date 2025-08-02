using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PropertyTypeCultureDatas", Schema = "catalog")]
    public partial class PropertyTypeCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        [Key]
        public byte PropertyTypeID { get; set; }
        [StringLength(50)]
        public string PropertyTypeName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
