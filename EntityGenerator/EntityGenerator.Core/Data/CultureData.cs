using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CultureDatas", Schema = "setting")]
    public partial class CultureData
    {
        [Key]
        public long CultureDataIID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TableName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ColumnName { get; set; }
        public byte? CultureID { get; set; }
        [StringLength(500)]
        public string Data { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("CultureDatas")]
        public virtual Culture Culture { get; set; }
    }
}
