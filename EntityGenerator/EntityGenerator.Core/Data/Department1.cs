using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Departments", Schema = "mutual")]
    public partial class Department1
    {
        public Department1()
        {
            AccountTransactionDetails = new HashSet<AccountTransactionDetail>();
            AvailableJobs = new HashSet<AvailableJob>();
            Budget1 = new HashSet<Budget1>();
            CircularMaps = new HashSet<CircularMap>();
            DocumentDepartmentMaps = new HashSet<DocumentDepartmentMap>();
            EmployeeDepartmentAccountMaps = new HashSet<EmployeeDepartmentAccountMap>();
            Employees = new HashSet<Employee>();
            JobDescriptions = new HashSet<JobDescription>();
            SectorTicketAirfares = new HashSet<SectorTicketAirfare>();
            TicketDepartments = new HashSet<TicketDepartment>();
            Tickets = new HashSet<Ticket>();
            TransactionHeads = new HashSet<TransactionHead>();
        }

        [Key]
        public long DepartmentID { get; set; }
        public int? CompanyID { get; set; }
        [StringLength(50)]
        public string DepartmentName { get; set; }
        [StringLength(500)]
        public string Logo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? StatusID { get; set; }
        [StringLength(4)]
        public string DepartmentNumber { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(40)]
        public string DepartmentCode { get; set; }
        public byte? SchoolID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("Department1")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("Department1")]
        public virtual School School { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<AccountTransactionDetail> AccountTransactionDetails { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<AvailableJob> AvailableJobs { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<Budget1> Budget1 { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<CircularMap> CircularMaps { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<DocumentDepartmentMap> DocumentDepartmentMaps { get; set; }
        [InverseProperty("DepartMent")]
        public virtual ICollection<EmployeeDepartmentAccountMap> EmployeeDepartmentAccountMaps { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<JobDescription> JobDescriptions { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<SectorTicketAirfare> SectorTicketAirfares { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<TicketDepartment> TicketDepartments { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<Ticket> Tickets { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}
