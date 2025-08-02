using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SchoolSaleExcelExport", Schema = "schools")]
    public partial class SchoolSaleExcelExport
    {
        [Key]
        public long SchoolSaleDataIID { get; set; }
        [StringLength(100)]
        public string GroupSet { get; set; }
        [StringLength(100)]
        public string Classes { get; set; }
        [StringLength(100)]
        public string Opening { get; set; }
        [StringLength(100)]
        public string Sale { get; set; }
        [StringLength(100)]
        public string PhysicalBalance { get; set; }
        [StringLength(100)]
        public string LandedCost { get; set; }
        [StringLength(100)]
        public string Sellingprice { get; set; }
        [StringLength(100)]
        public string ClosingstockValue { get; set; }
        [StringLength(100)]
        public string Campus { get; set; }
    }
}
