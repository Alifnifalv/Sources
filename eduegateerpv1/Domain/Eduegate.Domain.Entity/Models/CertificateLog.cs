namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CertificateLogs", Schema = "schools")]
    public partial class CertificateLog
    {
        [Key]
        public long CertificateLogIID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }


        public string ParameterValue { get; set; }

        public long? CertificateTemplateIID { get; set; }


        public virtual CertificateTemplate CertificateTemplate { get; set; }
    }
}
