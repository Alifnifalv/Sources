namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ExamGroupCategoryMaps", Schema = "schools")]
    public partial class ExamGroupCategoryMap
    {
        [Key]
        public long ExamGroupCategoryMapIID { get; set; }

        public int? ExamGroupID { get; set; }

        public int? StudentCategoryID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual StudentCategory StudentCategory { get; set; }
    }
}
