namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.HealthEntryStudentMaps")]
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
