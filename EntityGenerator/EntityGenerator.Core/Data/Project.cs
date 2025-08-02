using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("Projects", Schema = "account")]
    public partial class Project
    {
        public long ProjectIID { get; set; }
        [StringLength(200)]
        public string ProjectName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ProjectStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ProjectEndDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
