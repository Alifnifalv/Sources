using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDateFrom { get; set; }
        [Column(TypeName = "datetime")]
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

        [ForeignKey("ActionID")]
        [InverseProperty("Tickets")]
        public virtual SupportAction Action { get; set; }
        [ForeignKey("AssignedEmployeeID")]
        [InverseProperty("TicketAssignedEmployees")]
        public virtual Employee AssignedEmployee { get; set; }
        [ForeignKey("CustomerID")]
        [InverseProperty("Tickets")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("DepartmentID")]
        [InverseProperty("Tickets")]
        public virtual Department1 Department { get; set; }
        [ForeignKey("DocumentTypeID")]
        [InverseProperty("Tickets")]
        public virtual DocumentType DocumentType { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("TicketEmployees")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("FacultyTypeID")]
        [InverseProperty("Tickets")]
        public virtual FacultyType FacultyType { get; set; }
        [ForeignKey("LoginID")]
        [InverseProperty("Tickets")]
        public virtual Login Login { get; set; }
        [ForeignKey("ReferenceTicketID")]
        [InverseProperty("InverseReferenceTicket")]
        public virtual Ticket ReferenceTicket { get; set; }
        [ForeignKey("ReferenceTypeID")]
        [InverseProperty("Tickets")]
        public virtual TicketReferenceType ReferenceType { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("Tickets")]
        public virtual Student Student { get; set; }
        [ForeignKey("SupplierID")]
        [InverseProperty("Tickets")]
        public virtual Supplier Supplier { get; set; }
        [ForeignKey("SupportCategoryID")]
        [InverseProperty("TicketSupportCategories")]
        public virtual SupportCategory SupportCategory { get; set; }
        [ForeignKey("SupportSubCategoryID")]
        [InverseProperty("TicketSupportSubCategories")]
        public virtual SupportCategory SupportSubCategory { get; set; }
        [ForeignKey("TicketStatusID")]
        [InverseProperty("Tickets")]
        public virtual TicketStatus TicketStatus { get; set; }
        [ForeignKey("TicketTypeID")]
        [InverseProperty("Tickets")]
        public virtual TicketType TicketType { get; set; }
        [InverseProperty("ReferenceTicket")]
        public virtual ICollection<Ticket> InverseReferenceTicket { get; set; }
        [InverseProperty("Ticket")]
        public virtual ICollection<TicketActionDetailMap> TicketActionDetailMaps { get; set; }
        [InverseProperty("Ticket")]
        public virtual ICollection<TicketCommunication> TicketCommunications { get; set; }
        [InverseProperty("Ticket")]
        public virtual ICollection<TicketFeeDueMap> TicketFeeDueMaps { get; set; }
        [InverseProperty("Ticket")]
        public virtual ICollection<TicketProductMap> TicketProductMaps { get; set; }
    }
}
