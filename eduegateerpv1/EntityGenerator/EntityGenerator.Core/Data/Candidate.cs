using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Candidates", Schema = "exam")]
    [Index("StudentApplicationID", Name = "IDX_Candidates_StudentApplicationID_")]
    public partial class Candidate
    {
        public Candidate()
        {
            CandidateAnswers = new HashSet<CandidateAnswer>();
            CandidateOnlineExamMaps = new HashSet<CandidateOnlineExamMap>();
            OnlineExamResults = new HashSet<OnlineExamResult>();
        }

        [Key]
        public long CandidateIID { get; set; }
        [StringLength(50)]
        public string CandidateName { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(20)]
        public string Telephone { get; set; }
        [StringLength(20)]
        public string MobileNumber { get; set; }
        public string Notes { get; set; }
        public long? StudentID { get; set; }
        [StringLength(30)]
        public string UserName { get; set; }
        [StringLength(20)]
        public string Password { get; set; }
        [StringLength(50)]
        public string NationalID { get; set; }
        public int? ClassID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? CandidateStatusID { get; set; }
        public long? StudentApplicationID { get; set; }

        [ForeignKey("CandidateStatusID")]
        [InverseProperty("Candidates")]
        public virtual CandidateStatus CandidateStatus { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("Candidates")]
        public virtual Class Class { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("Candidates")]
        public virtual Student Student { get; set; }
        [ForeignKey("StudentApplicationID")]
        [InverseProperty("Candidates")]
        public virtual StudentApplication StudentApplication { get; set; }
        [InverseProperty("Candidate")]
        public virtual ICollection<CandidateAnswer> CandidateAnswers { get; set; }
        [InverseProperty("Candidate")]
        public virtual ICollection<CandidateOnlineExamMap> CandidateOnlineExamMaps { get; set; }
        [InverseProperty("Candidate")]
        public virtual ICollection<OnlineExamResult> OnlineExamResults { get; set; }
    }
}
