using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Departments", Schema = "mutual")]
    public partial class Department1
    {
        public Department1()
        {
            AccountTransactionDetails = new HashSet<AccountTransactionDetail>();
            //CircularMaps = new HashSet<CircularMap>();
            Employees = new HashSet<Employee>();
            TransactionHeads = new HashSet<TransactionHead>();
            AvailableJobs = new HashSet<AvailableJob>();
        }

        [Key]
        public long DepartmentID { get; set; }
        public int? CompanyID { get; set; }
        public string DepartmentName { get; set; }
        public string Logo { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public byte? StatusID { get; set; }
        public string DepartmentNumber { get; set; }
        public int? AcademicYearID { get; set; }
        public string DepartmentCode { get; set; }
        public byte? SchoolID { get; set; }
        public virtual AcademicYear AcademicYear { get; set; }
        public virtual Schools School { get; set; }
        public virtual ICollection<AvailableJob> AvailableJobs { get; set; }

        public virtual ICollection<AccountTransactionDetail> AccountTransactionDetails { get; set; }
        //public virtual ICollection<CircularMap> CircularMaps { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}
