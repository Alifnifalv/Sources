using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.OnlineExam.Models
{
    [Table("StudentPassportDetails", Schema = "schools")]
    public partial class StudentPassportDetail
    {
        [Key]
        public long StudentPassportDetailsIID { get; set; }

        public long? StudentID { get; set; }

        public int? NationalityID { get; set; }

        [StringLength(20)]
        public string PassportNo { get; set; }

        public int? CountryofIssueID { get; set; }

        public DateTime? PassportNoExpiry { get; set; }

        public int? CountryofBirthID { get; set; }

        [StringLength(20)]
        public string VisaNo { get; set; }

        public DateTime? VisaExpiry { get; set; }

        [StringLength(20)]
        public string NationalIDNo { get; set; }

        public DateTime? NationalIDNoExpiry { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        [StringLength(12)]
        public string AdhaarCardNo { get; set; }

        public DateTime? StudentNationalIDNoIssueDate { get; set; }

        public DateTime? StudentPassportNoIssueDate { get; set; }

        public virtual Student Student { get; set; }
    }
}