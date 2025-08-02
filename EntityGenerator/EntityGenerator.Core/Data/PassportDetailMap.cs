using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PassportDetailMaps", Schema = "schools")]
    public partial class PassportDetailMap
    {
        public PassportDetailMap()
        {
            StudentApplicationFatherPassportDetailNoes = new HashSet<StudentApplication>();
            StudentApplicationGuardianPassportDetailNoes = new HashSet<StudentApplication>();
            StudentApplicationMotherPassportDetailNoes = new HashSet<StudentApplication>();
            StudentApplicationStudentPassportDetailNoes = new HashSet<StudentApplication>();
        }

        [Key]
        public long PassportDetailsIID { get; set; }
        [StringLength(20)]
        public string PassportNo { get; set; }
        public int? CountryofIssueID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PassportNoIssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PassportNoExpiryDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CountryofIssueID")]
        [InverseProperty("PassportDetailMaps")]
        public virtual Country CountryofIssue { get; set; }
        [InverseProperty("FatherPassportDetailNo")]
        public virtual ICollection<StudentApplication> StudentApplicationFatherPassportDetailNoes { get; set; }
        [InverseProperty("GuardianPassportDetailNo")]
        public virtual ICollection<StudentApplication> StudentApplicationGuardianPassportDetailNoes { get; set; }
        [InverseProperty("MotherPassportDetailNo")]
        public virtual ICollection<StudentApplication> StudentApplicationMotherPassportDetailNoes { get; set; }
        [InverseProperty("StudentPassportDetailNo")]
        public virtual ICollection<StudentApplication> StudentApplicationStudentPassportDetailNoes { get; set; }
    }
}
