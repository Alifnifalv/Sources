namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cs.Tickets")]
    public partial class Ticket
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ticket()
        {
            TicketActionDetailMaps = new HashSet<TicketActionDetailMap>();
            TicketProductMaps = new HashSet<TicketProductMap>();
            Tickets1 = new HashSet<Ticket>();
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public DateTime? DueDateFrom { get; set; }

        public DateTime? DueDateTo { get; set; }

        public long? HeadID { get; set; }

        public bool? CustomerNotification { get; set; }

        public int? CompanyID { get; set; }

        public long? ReferenceTicketID { get; set; }

        public byte? TicketProcessingStatusID { get; set; }

        [StringLength(250)]
        public string ProcessingDescription { get; set; }

        public virtual SupportAction SupportAction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketActionDetailMap> TicketActionDetailMaps { get; set; }

        public virtual TicketProcessingStatus TicketProcessingStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketProductMap> TicketProductMaps { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee Employee1 { get; set; }

        public virtual Employee Employee2 { get; set; }

        public virtual Supplier Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets1 { get; set; }

        public virtual Ticket Ticket1 { get; set; }

        public virtual TicketStatus TicketStatus { get; set; }
    }
}
