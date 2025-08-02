using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AvailableJobCultureDatas", Schema = "hr")]
    public partial class AvailableJobCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        [Key]
        public long JobIID { get; set; }
        [StringLength(100)]
        public string JobTitle { get; set; }
        [StringLength(1000)]
        public string JobDescription { get; set; }
        public string JobDetails { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
