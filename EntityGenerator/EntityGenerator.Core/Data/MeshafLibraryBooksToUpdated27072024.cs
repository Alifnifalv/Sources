using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("MeshafLibraryBooksToUpdated27072024", Schema = "schools")]
    public partial class MeshafLibraryBooksToUpdated27072024
    {
        public string TITLE { get; set; }
        [StringLength(50)]
        public string CALL_NO { get; set; }
        [StringLength(50)]
        public string ACC_NO { get; set; }
        [StringLength(50)]
        public string Book_TyPE { get; set; }
        [StringLength(50)]
        public string PRICE { get; set; }
    }
}
