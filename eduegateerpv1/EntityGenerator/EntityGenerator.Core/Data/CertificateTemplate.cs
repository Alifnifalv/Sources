using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CertificateTemplates", Schema = "schools")]
    public partial class CertificateTemplate
    {
        public CertificateTemplate()
        {
            CertificateLogs = new HashSet<CertificateLog>();
        }

        [Key]
        public long CertificateTemplateIID { get; set; }
        [StringLength(200)]
        public string ReportName { get; set; }
        [StringLength(200)]
        public string CertificateName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("CertificateTemplateI")]
        public virtual ICollection<CertificateLog> CertificateLogs { get; set; }
    }
}
