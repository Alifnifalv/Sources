using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CertificateLogs", Schema = "schools")]
    public partial class CertificateLog
    {
        [Key]
        public long CertificateLogIID { get; set; }
        public string ParameterValue { get; set; }
        public long? CertificateTemplateIID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CertificateTemplateIID")]
        [InverseProperty("CertificateLogs")]
        public virtual CertificateTemplate CertificateTemplateI { get; set; }
    }
}
