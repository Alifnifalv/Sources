using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("MeshafLibraryBookUpdateRef", Schema = "schools")]
    public partial class MeshafLibraryBookUpdateRef
    {
        public short LibraryBookIID { get; set; }
        public string Subject { get; set; }
        public string BookTitle { get; set; }
        [StringLength(50)]
        public string Call_No { get; set; }
        [StringLength(600)]
        public string Acc_No { get; set; }
        [StringLength(50)]
        public string LibraryBookType { get; set; }
        [StringLength(50)]
        public string RackNumber { get; set; }
        public double? BookPrice { get; set; }
        public double? QatarPrice { get; set; }
        public string New_Shelf_Number { get; set; }
        public string Year { get; set; }
    }
}
