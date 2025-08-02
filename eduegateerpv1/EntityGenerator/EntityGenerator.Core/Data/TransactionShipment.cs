using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TransactionShipments", Schema = "inventory")]
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
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? FreightCharges { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? BrokerCharges { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? AdditionalCharges { get; set; }
        public double? Weight { get; set; }
        public int? NoOfBoxes { get; set; }
        [StringLength(50)]
        public string BrokerAccount { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("SupplierIDFrom")]
        [InverseProperty("TransactionShipmentSupplierIDFromNavigations")]
        public virtual Supplier SupplierIDFromNavigation { get; set; }
        [ForeignKey("SupplierIDTo")]
        [InverseProperty("TransactionShipmentSupplierIDToNavigations")]
        public virtual Supplier SupplierIDToNavigation { get; set; }
        [ForeignKey("TransactionHeadID")]
        [InverseProperty("TransactionShipments")]
        public virtual TransactionHead TransactionHead { get; set; }
    }
}
