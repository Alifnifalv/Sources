using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentPassportDetails", Schema = "schools")]
    [Index("StudentID", Name = "IDX_StudentPassportDetails_PassportNo_NationalIDNo")]
    public partial class StudentPassportDetail
    {
        [Key]
        public long StudentPassportDetailsIID { get; set; }
        public long? StudentID { get; set; }
        public int? NationalityID { get; set; }
        [StringLength(20)]
        public string PassportNo { get; set; }
        public int? CountryofIssueID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PassportNoExpiry { get; set; }
        public int? CountryofBirthID { get; set; }
        [StringLength(20)]
        public string VisaNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? VisaExpiry { get; set; }
        [StringLength(20)]
        public string NationalIDNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? NationalIDNoExpiry { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(12)]
        public string AdhaarCardNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StudentNationalIDNoIssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StudentPassportNoIssueDate { get; set; }

        [ForeignKey("CountryofBirthID")]
        [InverseProperty("StudentPassportDetailCountryofBirths")]
        public virtual Country CountryofBirth { get; set; }
        [ForeignKey("CountryofIssueID")]
        [InverseProperty("StudentPassportDetailCountryofIssues")]
        public virtual Country CountryofIssue { get; set; }
        [ForeignKey("NationalityID")]
        [InverseProperty("StudentPassportDetails")]
        public virtual Nationality Nationality { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("StudentPassportDetails")]
        public virtual Student Student { get; set; }
    }
}
