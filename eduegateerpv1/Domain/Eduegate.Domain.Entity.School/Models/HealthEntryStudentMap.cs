using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("HealthEntryStudentMaps", Schema = "schools")]
    public partial class HealthEntryStudentMap
    {
        [Key]
        public long HealthEntryStudentMapIID { get; set; }

        public long? HealthEntryID { get; set; }

        public long? StudentID { get; set; }

        public decimal? Height { get; set; }

        public decimal? Weight { get; set; }

        public decimal? BMS { get; set; }

        public string Vision { get; set; }

        public string Remarks { get; set; }

        public decimal? BMI { get; set; }

        public virtual HealthEntry HealthEntry { get; set; }

        public virtual Student Student { get; set; }
    }
}