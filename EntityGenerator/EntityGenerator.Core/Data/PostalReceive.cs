using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PostalReceives", Schema = "schools")]
    public partial class PostalReceive
    {
        [Key]
        public long PostalReceiveIID { get; set; }
        [StringLength(50)]
        public string FromTitle { get; set; }
        [StringLength(50)]
        public string ReferenceNumber { get; set; }
        [StringLength(1000)]
        public string Address { get; set; }
        [StringLength(1000)]
        public string Note { get; set; }
        [StringLength(50)]
        public string ToTitle { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
