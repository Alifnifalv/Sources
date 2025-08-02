using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AlbumTypes", Schema = "collaboration")]
    public partial class AlbumType
    {
        public AlbumType()
        {
            Albums = new HashSet<Album>();
        }

        [Key]
        public byte AlbumTypeID { get; set; }
        [StringLength(50)]
        public string AlbumTypeName { get; set; }

        [InverseProperty("AlbumType")]
        public virtual ICollection<Album> Albums { get; set; }
    }
}
