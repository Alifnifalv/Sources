using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Budgeting.Models
{
    [Table("Departments", Schema = "mutual")]
    public partial class Departments1
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Departments1()
        {
            Budget1 = new HashSet<Budget1>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DepartmentID { get; set; }

        public int? CompanyID { get; set; }

        [StringLength(50)]
        public string DepartmentName { get; set; }

        [StringLength(500)]
        public string Logo { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        public virtual ICollection<Budget1> Budget1 { get; set; }

        public byte? StatusID { get; set; }

        [StringLength(4)]
        public string DepartmentNumber { get; set; }

        public int? AcademicYearID { get; set; }

        [StringLength(40)]
        public string DepartmentCode { get; set; }
    }
}