using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("CustomerSupportTicket", Schema = "cms")]
    public partial class CustomerSupportTicket
    {
        [Key]
        public long TicketIID { get; set; }
        public string Name { get; set; }
        public string EmailID { get; set; }
        public string Telephone { get; set; }
        public string Subject { get; set; }
        public string TransactionNo { get; set; }
        public string Comments { get; set; }
        public string IPAddress { get; set; }
        public byte CultureID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public virtual Culture Culture { get; set; }
    }
}
