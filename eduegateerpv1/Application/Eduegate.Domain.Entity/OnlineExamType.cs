namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.OnlineExamTypes")]
    public partial class OnlineExamType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OnlineExamType()
        {
            OnlineExams = new HashSet<OnlineExam>();
        }

        [Key]
        public byte ExamTypeID { get; set; }

        [StringLength(50)]
        public string ExamTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OnlineExam> OnlineExams { get; set; }
    }
}
