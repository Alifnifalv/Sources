using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AlbumImageMaps", Schema = "collaboration")]
    public partial class AlbumImageMap
    {
        [Key]
        public long AlbumImageMapIID { get; set; }
        public int? AlbumID { get; set; }
        [StringLength(1000)]
        public string FileName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AlbumID")]
        [InverseProperty("AlbumImageMaps")]
        public virtual Album Album { get; set; }
    }
}
