using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentApplicationDocumentMaps", Schema = "schools")]
    [Index("StudentID", Name = "IDX_StudentApplicationDocumentMaps_StudentID_")]
    [Index("StudentID", Name = "IDX_StudentApplicationDocumentMaps_StudentID_ApplicationID__BirthCertificateReferenceID__BirthCerti")]
    public partial class StudentApplicationDocumentMap
    {
        [Key]
        public long ApplicationDocumentIID { get; set; }
        public long? ApplicationID { get; set; }
        public long? BirthCertificateReferenceID { get; set; }
        [StringLength(255)]
        public string BirthCertificateAttach { get; set; }
        public long? StudentPassportReferenceID { get; set; }
        [StringLength(255)]
        public string StudentPassportAttach { get; set; }
        public long? TCReferenceID { get; set; }
        [StringLength(255)]
        public string TCAttach { get; set; }
        public long? FatherQIDReferenceID { get; set; }
        [StringLength(255)]
        public string FatherQIDAttach { get; set; }
        public long? MotherQIDReferenceID { get; set; }
        [StringLength(255)]
        public string MotherQIDAttach { get; set; }
        public long? StudentQIDReferenceID { get; set; }
        [StringLength(255)]
        public string StudentQIDAttach { get; set; }
        public string Notes { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? StudentID { get; set; }

        [ForeignKey("ApplicationID")]
        [InverseProperty("StudentApplicationDocumentMaps")]
        public virtual StudentApplication Application { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("StudentApplicationDocumentMaps")]
        public virtual Student Student { get; set; }
    }
}
