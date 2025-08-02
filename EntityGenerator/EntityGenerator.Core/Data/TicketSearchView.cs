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
        public long TicketIID { get; set; }
        [StringLength(50)]
        public string TicketNo { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        public int? DocumentTypeID { get; set; }
        [StringLength(50)]
        public string TransactionTypeName { get; set; }
        [StringLength(100)]
        public string TicketSubject { get; set; }
        public string ProblemDescription { get; set; }
        public string ActionDescription { get; set; }
        public int? TicketSource { get; set; }
        public byte? PriorityID { get; set; }
        [StringLength(50)]
        public string PriorityName { get; set; }
        public byte? ActionID { get; set; }
        [StringLength(50)]
        public string ActionName { get; set; }
        public byte? TicketStatusID { get; set; }
        [StringLength(50)]
        public string TicketStatus { get; set; }
        public long? AssignedEmployeeID { get; set; }
        [StringLength(555)]
        public string AssignedEmployeeName { get; set; }
        public long? AssignedEmployeeLoginID { get; set; }
        public long? CustomerID { get; set; }
        [StringLength(767)]
        public string CustomerName { get; set; }
        public long? SupplierID { get; set; }
        [StringLength(152)]
        public string SupplierName { get; set; }
        public long? EmployeeID { get; set; }
        [StringLength(502)]
        public string EmployeeName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDateFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDateTo { get; set; }
        public bool? IsSendCustomerNotification { get; set; }
        public long? ReferenceTicketID { get; set; }
        public long? ReferenceID { get; set; }
        public byte? ReferenceTypeID { get; set; }
        public long? HeadID { get; set; }
        public string SKUDetails { get; set; }
        public int? CompanyID { get; set; }
        [StringLength(50)]
        public string GroupName { get; set; }
        public long? LoginID { get; set; }
        [StringLength(100)]
        public string LoginEmailID { get; set; }
        public string ShoppingCartIID { get; set; }
        public int CommentCounts { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        public int? UpdatedBy { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
