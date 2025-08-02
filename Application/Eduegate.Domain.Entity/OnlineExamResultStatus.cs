namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.OnlineExamResultStatuses")]
    public partial class OnlineExamResultStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OnlineExamResultStatus()
        {
            OnlineExamResults = new HashSet<OnlineExamResult>();
        }

        public byte OnlineExamResultStatusID { get; set; }

        [StringLength(50)]
        public string StatusNameEn { get; set; }

        [StringLength(50)]
        public string StatusNameAr { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OnlineExamResult> OnlineExamResults { get; set; }
    }
}
