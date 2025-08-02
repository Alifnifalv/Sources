using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AlbumSearch
    {
        public int AlbumID { get; set; }
        [StringLength(500)]
        public string AlbumName { get; set; }
        public byte? AlbumTypeID { get; set; }
        [StringLength(50)]
        public string AlbumTypeName { get; set; }
    }
}
