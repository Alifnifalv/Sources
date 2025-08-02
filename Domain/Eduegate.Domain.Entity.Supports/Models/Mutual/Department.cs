using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models.Mutual
{
    [Table("Departments", Schema = "mutual")]
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
            TicketDepartments = new HashSet<TicketDepartment>();
            Tickets = new HashSet<Ticket>();
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

        public virtual ICollection<DocumentDepartmentMap> DocumentDepartmentMaps { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<TicketDepartment> TicketDepartments { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}