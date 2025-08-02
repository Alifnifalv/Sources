using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ChartMetadatas", Schema = "setting")]
    public partial class ChartMetadata
    {
        [Key]
        public int ChartMetadataID { get; set; }
        [StringLength(100)]
        public string ChartName { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string ChartType { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string ChartPhysicalEntiy { get; set; }
    }
}
