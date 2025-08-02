using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ServiceProviderLogs", Schema = "distribution")]
    public partial class ServiceProviderLog
    {
        [Key]
        public long ServiceProviderLogIID { get; set; }
        public int? ServiceProviderID { get; set; }
        public long? ReferenceDocumentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LogDateTime { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ServiceProviderID")]
        [InverseProperty("ServiceProviderLogs")]
        public virtual ServiceProvider ServiceProvider { get; set; }
    }
}
