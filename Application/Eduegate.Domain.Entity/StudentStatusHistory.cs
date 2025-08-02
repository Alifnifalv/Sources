namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.StudentStatusHistory")]
    public partial class StudentStatusHistory
    {
        [Key]
        public long SSH_ID { get; set; }

        public long? StudentID { get; set; }

        public DateTime? ActiveDate { get; set; }

        public DateTime? DeActiveDate { get; set; }

        public DateTime? CampusTransferInDate { get; set; }

        public DateTime? CampusTransferOutDate { get; set; }

        public int? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public int? StudentStatusID { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamps { get; set; }
    }
}
