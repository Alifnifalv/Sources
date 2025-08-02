namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.TransactionShipments")]
    public partial class TransactionShipment
    {
        [Key]
        public long TransactionShipmentIID { get; set; }

        public long? TransactionHeadID { get; set; }

        public long? SupplierIDFrom { get; set; }

        public long? SupplierIDTo { get; set; }

        [StringLength(50)]
        public string ShipmentReference { get; set; }

        [StringLength(50)]
        public string FreightCarrier { get; set; }

        public short? ClearanceTypeID { get; set; }

        [StringLength(50)]
        public string AirWayBillNo { get; set; }

        public decimal? FreightCharges { get; set; }

        public decimal? BrokerCharges { get; set; }

        public decimal? AdditionalCharges { get; set; }

        public double? Weight { get; set; }

        public int? NoOfBoxes { get; set; }

        [StringLength(50)]
        public string BrokerAccount { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual TransactionHead TransactionHead { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual Supplier Supplier1 { get; set; }
    }
}
