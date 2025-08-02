namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.QuestionOptionMaps")]
    public partial class QuestionOptionMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuestionOptionMap()
        {
            CandidateAnswers = new HashSet<CandidateAnswer>();
            CandidateAssesments = new HashSet<CandidateAssesment>();
            CandidateAssesments1 = new HashSet<CandidateAssesment>();
            QuestionAnswerMaps = new HashSet<QuestionAnswerMap>();
        }

        [Key]
        public long QuestionOptionMapIID { get; set; }

        public string OptionText { get; set; }

        public long? QuestionID { get; set; }

        public string ImageName { get; set; }

        public long? ContentID { get; set; }

        public int? OrderNo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateAnswer> CandidateAnswers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateAssesment> CandidateAssesments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateAssesment> CandidateAssesments1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionAnswerMap> QuestionAnswerMaps { get; set; }

        public virtual Question Question { get; set; }
    }
}
