using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.OnlineExam.Models
{

    [Table("OnlineExamOperationStatuses", Schema = "exam")]
    public partial class OnlineExamOperationStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OnlineExamOperationStatus()
        {
            CandidateOnlineExamMaps = new HashSet<CandidateOnlineExamMap>();
        }
        [Key]
        public byte OnlineExamOperationStatusID { get; set; }

        [StringLength(50)]
        public string OperationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateOnlineExamMap> CandidateOnlineExamMaps { get; set; }
    }
}