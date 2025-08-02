namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.StudentStreamOptionalSubjectMaps")]
    public partial class StudentStreamOptionalSubjectMap
    {
        [Key]
        public long StudentStreamOptionalSubjectMapIID { get; set; }

        public byte? StreamID { get; set; }

        public int? SubjectID { get; set; }

        public long? StudentID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Stream Stream { get; set; }

        public virtual Student Student { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
