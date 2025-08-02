namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.QuestionAnswerMaps")]
    public partial class QuestionAnswerMap
    {
        [Key]
        public long QuestionAnswerMapIID { get; set; }

        public long? QuestionID { get; set; }

        public long? QuestionOptionMapID { get; set; }

        public virtual QuestionOptionMap QuestionOptionMap { get; set; }

        public virtual Question Question { get; set; }
    }
}
