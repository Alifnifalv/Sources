namespace  Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StudentSiblingMaps", Schema = "schools")]
    public partial class StudentSiblingMap
    {
        [Key]
        public long StudentSiblingMapIID { get; set; }

        public long StudentID { get; set; }

        public long? SiblingID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual Student Student { get; set; }

        public virtual Student Student1 { get; set; }
    }
}
