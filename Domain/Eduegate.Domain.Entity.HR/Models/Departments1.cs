using Eduegate.Domain.Entity.HR.Payroll;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.HR.Models
{
    [Table("Departments", Schema = "mutual")]
    public partial class Departments1
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Departments1()
        {
            Employees = new HashSet<Employee>();
            SectorTicketAirfares = new HashSet<SectorTicketAirfare>();
            EmployeeDepartmentAccountMaps = new HashSet<EmployeeDepartmentAccountMap>();
            JobDescriptions = new HashSet<JobDescription>();
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

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public byte? StatusID { get; set; }

        public virtual Company Company { get; set; }

        //public virtual DepartmentStatus DepartmentStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<SectorTicketAirfare> SectorTicketAirfares { get; set; }
       
        public virtual ICollection<EmployeeDepartmentAccountMap> EmployeeDepartmentAccountMaps { get; set; }

        public virtual ICollection<JobDescription> JobDescriptions { get; set; }

    }
}