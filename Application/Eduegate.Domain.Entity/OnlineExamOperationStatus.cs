namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.OnlineExamOperationStatuses")]
    public partial class OnlineExamOperationStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OnlineExamOperationStatus()
        {
            CandidateOnlineExamMaps = new HashSet<CandidateOnlineExamMap>();
        }

        public byte OnlineExamOperationStatusID { get; set; }

        [StringLength(50)]
        public string OperationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateOnlineExamMap> CandidateOnlineExamMaps { get; set; }
    }
}
