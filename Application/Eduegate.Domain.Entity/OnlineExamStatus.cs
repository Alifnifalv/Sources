namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.OnlineExamStatuses")]
    public partial class OnlineExamStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OnlineExamStatus()
        {
            CandidateOnlineExamMaps = new HashSet<CandidateOnlineExamMap>();
        }

        [Key]
        public byte ExamStatusID { get; set; }

        [StringLength(50)]
        public string ExamStatusName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateOnlineExamMap> CandidateOnlineExamMaps { get; set; }
    }
}
