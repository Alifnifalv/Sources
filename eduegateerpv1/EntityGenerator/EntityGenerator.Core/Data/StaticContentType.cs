using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StaticContentTypes", Schema = "cms")]
    public partial class StaticContentType
    {
        [Key]
        public int ContentTypeID { get; set; }
        [StringLength(50)]
        public string ContentTypeName { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(500)]
        public string ContentTemplateFilePath { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Required]
        public byte[] TimeStamps { get; set; }
    }
}
