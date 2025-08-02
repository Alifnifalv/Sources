namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schedule.Despatches")]
    public partial class Despatch
    {
        [Key]
        public long DespatchIdty { get; set; }

        public DateTime? DespatchDate { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; }

        public long? ParentDespatchID { get; set; }

        public long? EmployeeID { get; set; }

        public long? CustomerID { get; set; }

        public long? ScheduleID { get; set; }

        [StringLength(20)]
        public string TimeFrom { get; set; }

        [StringLength(20)]
        public string TimeTo { get; set; }

        public double? TotalHours { get; set; }

        public decimal? Rate { get; set; }

        public decimal? ReceivedAmount { get; set; }

        public DateTime? ReceivedDate { get; set; }

        public decimal? Amount { get; set; }

        public decimal? TaxAmount { get; set; }

        public decimal? TaxPercentage { get; set; }

        public int? StatusID { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public long? ExternalReferenceID1 { get; set; }

        public long? ExternalReferenceID2 { get; set; }
    }
}
