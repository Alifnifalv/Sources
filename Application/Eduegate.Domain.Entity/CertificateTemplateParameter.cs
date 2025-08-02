namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.CertificateTemplateParameters")]
    public partial class CertificateTemplateParameter
    {
        [Key]
        public long CertificateTemplateParameterIID { get; set; }

        public long CertificateTemplateID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        [StringLength(250)]
        public string ParameterID { get; set; }

        [StringLength(250)]
        public string ParameterValue { get; set; }
    }
}
