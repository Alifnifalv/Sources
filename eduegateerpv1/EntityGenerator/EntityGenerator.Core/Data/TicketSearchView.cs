using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TicketSearchView
    {
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        public long TicketIID { get; set; }
        [StringLength(50)]
        public string TicketNo { get; set; }
        public int? DocumentTypeID { get; set; }
        [StringLength(50)]
        public string TransactionTypeName { get; set; }
        [StringLength(100)]
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Description2 { get; set; }
        public byte ActionID { get; set; }
        [StringLength(50)]
        public string ActionName { get; set; }
        public byte? TicketStatusID { get; set; }
        [StringLength(50)]
        public string TicketStatus { get; set; }
        public long? AssingedEmployeeID { get; set; }
        [StringLength(100)]
        public string AssingedEmployeeName { get; set; }
        public long? ManagerEmployeeID { get; set; }
        [StringLength(100)]
        public string EmployeeName { get; set; }
        public long? CustomerID { get; set; }
        [Required]
        [StringLength(767)]
        public string CustomerName { get; set; }
        public long? SupplierID { get; set; }
        [Required]
        [StringLength(152)]
        public string SupplierName { get; set; }
        public long? EmployeeID { get; set; }
        [StringLength(100)]
        public string ManagerName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public long? HeadId { get; set; }
        public int? CreatedBy { get; set; }
        public string skudetails { get; set; }
        [StringLength(100)]
        public string LoginEmailID { get; set; }
        public int? CompanyID { get; set; }
        [StringLength(50)]
        public string GroupName { get; set; }
        public string ShoppingCartIID { get; set; }
        public int CommentCounts { get; set; }
    }
}
