using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CustomerSupportTicket", Schema = "cms")]
    public partial class CustomerSupportTicket
    {
        [Key]
        public long TicketIID { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string EmailID { get; set; }
        [StringLength(100)]
        public string Telephone { get; set; }
        [Required]
        [StringLength(250)]
        public string Subject { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNo { get; set; }
        [Required]
        [StringLength(300)]
        public string Comments { get; set; }
        [Required]
        [StringLength(250)]
        public string IPAddress { get; set; }
        public byte CultureID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? CompanyID { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("CustomerSupportTickets")]
        public virtual Culture Culture { get; set; }
    }
}
