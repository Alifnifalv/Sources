namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.EmployeeAdditionalInfos")]
    public partial class EmployeeAdditionalInfo
    {
        [Key]
        public long EmployeeAdditionalInfoIID { get; set; }

        public long? EmployeeID { get; set; }

        [StringLength(1000)]
        public string HighestAcademicQualitication { get; set; }

        [StringLength(1000)]
        public string HighestPrefessionalQualitication { get; set; }

        [StringLength(1000)]
        public string ClassessTaught { get; set; }

        [StringLength(1000)]
        public string AppointedSubject { get; set; }

        [StringLength(1000)]
        public string MainSubjectTought { get; set; }

        [StringLength(1000)]
        public string AdditioanalSubjectTought { get; set; }

        public bool? IsComputerTrained { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
