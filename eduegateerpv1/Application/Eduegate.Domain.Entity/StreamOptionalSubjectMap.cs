namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.StreamOptionalSubjectMaps")]
    public partial class StreamOptionalSubjectMap
    {
        [Key]
        public long StreamOptionalSubjectIID { get; set; }

        public byte? StreamID { get; set; }

        public long? StreamSubjectMapID { get; set; }

        public int? SubjectID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? OrderBy { get; set; }

        public virtual Stream Stream { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
