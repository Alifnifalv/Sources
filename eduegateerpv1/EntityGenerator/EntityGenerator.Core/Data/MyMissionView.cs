using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class MyMissionView
    {
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        [StringLength(9)]
        [Unicode(false)]
        public string MissionStatus { get; set; }
        public long JobEntryHeadIID { get; set; }
        [StringLength(50)]
        public string JobNumber { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? JobStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? JobEndDate { get; set; }
        public int? ReferenceDocumentTypeID { get; set; }
        [StringLength(82)]
        public string TransactionTypeName { get; set; }
        public long? TransactionHeadID { get; set; }
        public byte? PriorityID { get; set; }
        [StringLength(50)]
        public string PriorityDescription { get; set; }
        public int? JobStatusID { get; set; }
        [StringLength(50)]
        public string JobStatusName { get; set; }
        [StringLength(500)]
        public string TransDescription { get; set; }
        public int? TransDocumentTypeID { get; set; }
        [StringLength(50)]
        public string TransTransactionTypeName { get; set; }
        [StringLength(20)]
        public string VehicleCode { get; set; }
        public long? TransHeadID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNo { get; set; }
        [Required]
        public string PartNumber { get; set; }
        public string ShoppingCartIds { get; set; }
        public string AreaName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransDeliveryDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Required]
        [StringLength(1)]
        [Unicode(false)]
        public string RemainingHours { get; set; }
        public long? EmployeeID { get; set; }
        [StringLength(100)]
        public string EmployeeName { get; set; }
        public int? NumberOfOrders { get; set; }
        public long? LoginID { get; set; }
    }
}
