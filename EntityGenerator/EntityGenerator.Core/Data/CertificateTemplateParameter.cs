using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CertificateTemplateParameters", Schema = "schools")]
    public partial class CertificateTemplateParameter
    {
        [Key]
        public long CertificateTemplateParameterIID { get; set; }
        public long CertificateTemplateID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(250)]
        public string ParameterID { get; set; }
        [StringLength(250)]
        public string ParameterValue { get; set; }
    }
}
