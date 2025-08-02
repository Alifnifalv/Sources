using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AvailableJobTags", Schema = "hr")]
    public partial class AvailableJobTag
    {
        [Key]
        public long AvailableJobTagIID { get; set; }
        public long? JobID { get; set; }
        [StringLength(500)]
        public string TagName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("JobID")]
        [InverseProperty("AvailableJobTags")]
        public virtual AvailableJob Job { get; set; }
    }
}
