using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("VisaDetailMaps", Schema = "schools")]
    public partial class VisaDetailMap
    {
        public VisaDetailMap()
        {
            StudentApplicationFatherVisaDetailNoes = new HashSet<StudentApplication>();
            StudentApplicationGuardianVisaDetailNoes = new HashSet<StudentApplication>();
            StudentApplicationMotherVisaDetailNoes = new HashSet<StudentApplication>();
            StudentApplicationStudentVisaDetailNoes = new HashSet<StudentApplication>();
        }

        [Key]
        public long VisaDetailsIID { get; set; }
        [StringLength(20)]
        public string VisaNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? VisaIssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? VisaExpiryDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("FatherVisaDetailNo")]
        public virtual ICollection<StudentApplication> StudentApplicationFatherVisaDetailNoes { get; set; }
        [InverseProperty("GuardianVisaDetailNo")]
        public virtual ICollection<StudentApplication> StudentApplicationGuardianVisaDetailNoes { get; set; }
        [InverseProperty("MotherVisaDetailNo")]
        public virtual ICollection<StudentApplication> StudentApplicationMotherVisaDetailNoes { get; set; }
        [InverseProperty("StudentVisaDetailNo")]
        public virtual ICollection<StudentApplication> StudentApplicationStudentVisaDetailNoes { get; set; }
    }
}
