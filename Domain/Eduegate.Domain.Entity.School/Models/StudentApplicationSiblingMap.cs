namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StudentApplicationSiblingMaps", Schema = "schools")]
    public partial class StudentApplicationSiblingMap
    {
        [Key]
        public long StudentApplicationSiblingMapIID { get; set; }

        public long SiblingID { get; set; }

        public long? ApplicationID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual StudentApplication Application { get; set; }

        public virtual Student Sibling { get; set; }
    }
}
