namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.MarkRegisters20230227")]
    public partial class MarkRegisters20230227
    {
        [Key]
        public long MarkRegisterIID { get; set; }

        public long? ExamID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public long? StudentId { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public byte? MarkEntryStatusID { get; set; }

        public int? ExamGroupID { get; set; }

        public byte? PresentStatusID { get; set; }
    }
}
