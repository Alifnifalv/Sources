using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Despatches", Schema = "schedule")]
    public partial class Despatch
    {
        [Key]
        public long DespatchIdty { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DespatchDate { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }
        public long? ParentDespatchID { get; set; }
        public long? EmployeeID { get; set; }
        public long? CustomerID { get; set; }
        public long? ScheduleID { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string TimeFrom { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string TimeTo { get; set; }
        public double? TotalHours { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Rate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ReceivedAmount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ReceivedDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TaxAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TaxPercentage { get; set; }
        public int? StatusID { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? ExternalReferenceID1 { get; set; }
        public long? ExternalReferenceID2 { get; set; }
    }
}
