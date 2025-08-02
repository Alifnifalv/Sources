namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.StudentApplicationDocumentMaps")]
    public partial class StudentApplicationDocumentMap
    {
        [Key]
        public long ApplicationDocumentIID { get; set; }

        public long? ApplicationID { get; set; }

        public long? BirthCertificateReferenceID { get; set; }

        [StringLength(50)]
        public string BirthCertificateAttach { get; set; }

        public long? StudentPassportReferenceID { get; set; }

        [StringLength(50)]
        public string StudentPassportAttach { get; set; }

        public long? TCReferenceID { get; set; }

        [StringLength(50)]
        public string TCAttach { get; set; }

        public long? FatherQIDReferenceID { get; set; }

        [StringLength(50)]
        public string FatherQIDAttach { get; set; }

        public long? MotherQIDReferenceID { get; set; }

        [StringLength(50)]
        public string MotherQIDAttach { get; set; }

        public long? StudentQIDReferenceID { get; set; }

        [StringLength(50)]
        public string StudentQIDAttach { get; set; }

        public string Notes { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public long? StudentID { get; set; }

        public virtual StudentApplication StudentApplication { get; set; }

        public virtual Student Student { get; set; }
    }
}
