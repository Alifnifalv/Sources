namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Stops", Schema = "schools")]
    public partial class Stop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StopID { get; set; }

        [StringLength(50)]
        public string StopCode { get; set; }

        [StringLength(50)]
        public string StopName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Schools School { get; set; }
    }
}
