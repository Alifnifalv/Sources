using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class UnitsView
    {
        public long UnitID { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string UnitCode { get; set; }
        [StringLength(50)]
        public string UnitName { get; set; }
        public double? Fraction { get; set; }
        public int? CreatedBy { get; set; }
        [StringLength(100)]
        public string CreatedUserName { get; set; }
        public int? UpdatedBy { get; set; }
        [StringLength(100)]
        public string UpdatedUserName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(20)]
        public string ShortName { get; set; }
        public long? UnitGroupID { get; set; }
        [StringLength(50)]
        public string UnitGroupName { get; set; }
    }
}
