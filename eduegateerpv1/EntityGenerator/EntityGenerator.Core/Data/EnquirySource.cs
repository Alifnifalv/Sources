using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EnquirySources", Schema = "schools")]
    public partial class EnquirySource
    {
        [Key]
        public byte EnquirySourceID { get; set; }
        [StringLength(50)]
        public string SourceName { get; set; }
    }
}
