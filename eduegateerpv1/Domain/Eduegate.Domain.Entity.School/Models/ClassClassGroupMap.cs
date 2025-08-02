namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ClassClassGroupMaps", Schema = "schools")]
    public partial class ClassClassGroupMap
    {
        [Key]
        public long ClassClassGroupMapIID { get; set; }

        public long? ClassGroupID { get; set; }

        public int? ClassID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual Class Class { get; set; }

        public virtual ClassGroup ClassGroup { get; set; }
    }
}
