using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Tickets", Schema = "cs")]
    public partial class Ticket
    {
        public Ticket()
        {
            this.TicketActionDetailMaps = new List<TicketActionDetailMap>();
            this.TicketProductMaps = new List<TicketProductMap>();
        }

        [Key]
        public long TicketIID { get; set; }
        public string TicketNo { get; set; }
        public Nullable<int> DocumentTypeID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Description2 { get; set; }
        public Nullable<int> Source { get; set; }
        public Nullable<byte> PriorityID { get; set; }
        public byte ActionID { get; set; }
        public Nullable<byte> TicketStatusID { get; set; }
        public Nullable<long> AssingedEmployeeID { get; set; }
        public Nullable<long> ManagerEmployeeID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public Nullable<long> SupplierID { get; set; }
        public Nullable<long> EmployeeID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> DueDateFrom { get; set; }
        public Nullable<System.DateTime> DueDateTo { get; set; }
        public Nullable<long> HeadID { get; set; }
        public Nullable<bool> CustomerNotification { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual SupportAction SupportAction { get; set; }
        public virtual ICollection<TicketProductMap> TicketProductMaps { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Employee Employee1 { get; set; }
        public virtual Employee Employee2 { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }
        public virtual ICollection<TicketActionDetailMap> TicketActionDetailMaps { get; set; }
        public int CompanyID { get; set; }
    }
}
