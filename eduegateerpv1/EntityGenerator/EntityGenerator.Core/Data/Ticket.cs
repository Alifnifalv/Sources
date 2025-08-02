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
        public byte ActionID { get; set; }
        public byte? TicketStatusID { get; set; }
        public long? AssingedEmployeeID { get; set; }
        public long? ManagerEmployeeID { get; set; }
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
        public long? HeadID { get; set; }
        public bool? CustomerNotification { get; set; }
        public int? CompanyID { get; set; }
        public long? ReferenceTicketID { get; set; }
        public byte? TicketProcessingStatusID { get; set; }
        [StringLength(250)]
        public string ProcessingDescription { get; set; }
        public long? LoginID { get; set; }
        public long? ReferenceID { get; set; }
        public byte? ReferenceTypeID { get; set; }

        [ForeignKey("ActionID")]
        [InverseProperty("Tickets")]
        public virtual SupportAction Action { get; set; }
        [ForeignKey("AssingedEmployeeID")]
        [InverseProperty("TicketAssingedEmployees")]
        public virtual Employee AssingedEmployee { get; set; }
        [ForeignKey("CustomerID")]
        [InverseProperty("Tickets")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("DocumentTypeID")]
        [InverseProperty("Tickets")]
        public virtual DocumentType DocumentType { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("TicketEmployees")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("LoginID")]
        [InverseProperty("Tickets")]
        public virtual Login Login { get; set; }
        [ForeignKey("ManagerEmployeeID")]
        [InverseProperty("TicketManagerEmployees")]
        public virtual Employee ManagerEmployee { get; set; }
        [ForeignKey("ReferenceTicketID")]
        [InverseProperty("InverseReferenceTicket")]
        public virtual Ticket ReferenceTicket { get; set; }
        [ForeignKey("ReferenceTypeID")]
        [InverseProperty("Tickets")]
        public virtual TicketReferenceType ReferenceType { get; set; }
        [ForeignKey("SupplierID")]
        [InverseProperty("Tickets")]
        public virtual Supplier Supplier { get; set; }
        [ForeignKey("TicketProcessingStatusID")]
        [InverseProperty("Tickets")]
        public virtual TicketProcessingStatus TicketProcessingStatus { get; set; }
        [ForeignKey("TicketStatusID")]
        [InverseProperty("Tickets")]
        public virtual TicketStatus TicketStatus { get; set; }
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
