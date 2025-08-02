using Eduegate.Domain.Entity.Supports.Models.Mutual;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models
{
    [Table("Tickets", Schema = "cs")]
    public partial class Ticket
    {
        public Ticket()
        {
            InverseReferenceTicket = new HashSet<Ticket>();
            TicketActionDetailMaps = new HashSet<TicketActionDetailMap>();
            TicketCommunications = new HashSet<TicketCommunication>();
            TicketFeeDueMaps = new HashSet<TicketFeeDueMap>();
            TicketProductMaps = new HashSet<TicketProductMap>();
        }

        [Key]
        public long TicketIID { get; set; }

        [StringLength(50)]
        public string TicketNo { get; set; }

        public int? DocumentTypeID { get; set; }

        [StringLength(100)]
        public string Subject { get; set; }

        public string Description { get; set; }

        public string Description2 { get; set; }

        public int? Source { get; set; }

        public byte? PriorityID { get; set; }

        public byte? ActionID { get; set; }

        public byte? TicketStatusID { get; set; }

        public long? AssignedEmployeeID { get; set; }

        public long? CustomerID { get; set; }

        public long? SupplierID { get; set; }

        public long? EmployeeID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public DateTime? DueDateFrom { get; set; }

        public DateTime? DueDateTo { get; set; }

        public bool? IsSendCustomerNotification { get; set; }

        public int? CompanyID { get; set; }

        public long? ReferenceTicketID { get; set; }

        public long? LoginID { get; set; }

        public long? ReferenceID { get; set; }

        public byte? ReferenceTypeID { get; set; }

        public int? TicketTypeID { get; set; }

        public int? SupportCategoryID { get; set; }

        public int? SupportSubCategoryID { get; set; }

        public int? FacultyTypeID { get; set; }

        public long? StudentID { get; set; }

        public long? DepartmentID { get; set; }

        public long? HeadID { get; set; }

        public virtual SupportAction Action { get; set; }

        public virtual Employee AssignedEmployee { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Department Department { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual FacultyType FacultyType { get; set; }

        public virtual Login Login { get; set; }

        public virtual Ticket ReferenceTicket { get; set; }

        public virtual TicketReferenceType ReferenceType { get; set; }

        public virtual Student Student { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual SupportCategory SupportCategory { get; set; }

        public virtual SupportCategory SupportSubCategory { get; set; }

        public virtual TicketStatus TicketStatus { get; set; }

        public virtual TicketType TicketType { get; set; }

        public virtual ICollection<Ticket> InverseReferenceTicket { get; set; }

        public virtual ICollection<TicketActionDetailMap> TicketActionDetailMaps { get; set; }

        public virtual ICollection<TicketCommunication> TicketCommunications { get; set; }

        public virtual ICollection<TicketFeeDueMap> TicketFeeDueMaps { get; set; }

        public virtual ICollection<TicketProductMap> TicketProductMaps { get; set; }
    }
}