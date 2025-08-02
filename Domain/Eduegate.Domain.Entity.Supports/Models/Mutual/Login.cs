using Eduegate.Domain.Entity.Supports.Models.Mutual;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models
{
    [Table("Logins", Schema = "admin")]
    public partial class Login
    {
        public Login()
        {
            Customers = new HashSet<Customer>();
            Employees = new HashSet<Employee>();
            Suppliers = new HashSet<Supplier>();
            TicketCommunications = new HashSet<TicketCommunication>();
            Tickets = new HashSet<Ticket>();
        }

        [Key]
        public long LoginIID { get; set; }

        [StringLength(100)]
        public string LoginUserID { get; set; }

        [StringLength(100)]
        public string LoginEmailID { get; set; }

        [StringLength(200)]
        public string UserName { get; set; }

        [StringLength(1000)]
        public string ProfileFile { get; set; }

        public long? CustomerID { get; set; }

        public long? SupplierID { get; set; }

        public long? EmployeeID { get; set; }

        [StringLength(50)]
        public string PasswordHint { get; set; }

        [StringLength(128)]
        public string PasswordSalt { get; set; }

        [StringLength(128)]
        public string Password { get; set; }

        public byte? StatusID { get; set; }

        public int? RegisteredCountryID { get; set; }

        [StringLength(20)]
        public string RegisteredIP { get; set; }

        [StringLength(20)]
        public string RegisteredIPCountry { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public bool? RequirePasswordReset { get; set; }

        public int? SiteID { get; set; }

        [StringLength(50)]
        public string LastOTP { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public byte? SchoolID { get; set; }

        public bool? IsForceLogout { get; set; }

        public bool? EnableForceLogout { get; set; }

        public bool? ExternalReference { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }

        public virtual ICollection<TicketCommunication> TicketCommunications { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}