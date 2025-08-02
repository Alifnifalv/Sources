namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LessonReferenceBooks", Schema = "schools")]
    public partial class LessonReferenceBook
    {
     
        [Key]
        public int LessonReferenceBookID { get; set; }

        public int LessonID { get; set; }

        public string BookTitle { get; set; }

        public string Description { get; set; }

        public int? CreatedBy { get; set; }

        public int? PublicationYear { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
