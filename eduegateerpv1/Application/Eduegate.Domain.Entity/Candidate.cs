namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.Candidates")]
    public partial class Candidate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public byte? CandidateStatusID { get; set; }

        public long? StudentApplicationID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateAnswer> CandidateAnswers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateOnlineExamMap> CandidateOnlineExamMaps { get; set; }

        public virtual CandidateStatus CandidateStatus { get; set; }

        public virtual Class Class { get; set; }

        public virtual StudentApplication StudentApplication { get; set; }

        public virtual Student Student { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OnlineExamResult> OnlineExamResults { get; set; }
    }
}
