namespace Eduegate.Domain.Entity.HR.Payroll
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AcademicYears", Schema = "schools")]
    public partial class AcademicYear
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AcademicYear()
        {
            
            FunctionalPeriods = new HashSet<FunctionalPeriod>();
          
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AcademicYearID { get; set; }

        [StringLength(20)]
        public string AcademicYearCode { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public byte? SchoolID { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        public byte? AcademicYearStatusID { get; set; }

        public int? ORDERNO { get; set; }

        //public virtual Schools School { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FunctionalPeriod> FunctionalPeriods { get; set; }
    }
}
