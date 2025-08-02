namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.StudentApplicationOptionalSubjectMaps")]
    public partial class StudentApplicationOptionalSubjectMap
    {
        [Key]
        public long StudentApplicationOptionalSubjectMapIID { get; set; }

        public byte? StreamID { get; set; }

        public int? SubjectID { get; set; }

        public long? ApplicationID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Stream Stream { get; set; }

        public virtual StudentApplication StudentApplication { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
