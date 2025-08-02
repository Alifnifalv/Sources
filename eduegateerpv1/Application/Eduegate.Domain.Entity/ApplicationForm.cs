namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("hr.ApplicationForms")]
    public partial class ApplicationForm
    {
        [Key]
        public long ApplicationFormIID { get; set; }

        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string ContactNo { get; set; }

        [StringLength(100)]
        public string CountryOfResidence { get; set; }

        [StringLength(50)]
        public string YearsOfExperience { get; set; }

        [StringLength(100)]
        public string PositionAppliedFor { get; set; }

        [StringLength(500)]
        public string CV { get; set; }

        [StringLength(500)]
        public string Nationality { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(50)]
        public string Gender { get; set; }

        [StringLength(500)]
        public string Qualification { get; set; }

        [StringLength(50)]
        public string Status { get; set; }
    }
}
