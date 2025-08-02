using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;
using Eduegate.Domain.Entity.Accounts.Models.Assets;

namespace Eduegate.Domain.Entity.Accounts.Models.Mutual
{
    [Table("Departments", Schema = "mutual")]
    public partial class Department1
    {
        public Department1()
        {
            AccountTransactionDetails = new HashSet<AccountTransactionDetail>();
            //Budget1 = new HashSet<Budget1>();
            //CircularMaps = new HashSet<CircularMap>();
            //DocumentDepartmentMaps = new HashSet<DocumentDepartmentMap>();
            //Employees = new HashSet<Employee>();
            //TransactionHeads = new HashSet<TransactionHead>();
        }

        [Key]
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

        //public byte[] TimeStamps { get; set; }

        public byte? StatusID { get; set; }

        [StringLength(4)]
        public string DepartmentNumber { get; set; }

        public int? AcademicYearID { get; set; }

        [StringLength(40)]
        public string DepartmentCode { get; set; }

        public byte? SchoolID { get; set; }

        //public virtual AcademicYear AcademicYear { get; set; }

        //public virtual School School { get; set; }

        public virtual ICollection<AccountTransactionDetail> AccountTransactionDetails { get; set; }

        //public virtual ICollection<Budget1> Budget1 { get; set; }

        //public virtual ICollection<CircularMap> CircularMaps { get; set; }

        //public virtual ICollection<DocumentDepartmentMap> DocumentDepartmentMaps { get; set; }

        //public virtual ICollection<Employee> Employees { get; set; }

        //public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}