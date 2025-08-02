using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Albums", Schema = "collaboration")]
    public partial class Album
    {
        public Album()
        {
            AlbumImageMaps = new HashSet<AlbumImageMap>();
        }

        [Key]
        public int AlbumID { get; set; }
        [StringLength(500)]
        public string AlbumName { get; set; }
        public byte? AlbumTypeID { get; set; }

        [ForeignKey("AlbumTypeID")]
        [InverseProperty("Albums")]
        public virtual AlbumType AlbumType { get; set; }
        [InverseProperty("Album")]
        public virtual ICollection<AlbumImageMap> AlbumImageMaps { get; set; }
    }
}
