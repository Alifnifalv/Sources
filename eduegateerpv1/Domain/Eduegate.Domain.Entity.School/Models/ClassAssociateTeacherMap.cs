namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ClassAssociateTeacherMaps", Schema = "schools")]
    public partial class ClassAssociateTeacherMap
    {
        [Key]
        public long ClassAssociateTeacherMapIID { get; set; }

        public long? TeacherID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public long? ClassClassTeacherMapID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ClassClassTeacherMap ClassClassTeacherMap { get; set; }
    }
}
