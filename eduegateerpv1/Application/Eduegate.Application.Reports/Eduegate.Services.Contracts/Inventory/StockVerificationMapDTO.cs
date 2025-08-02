using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Inventory
{
    public class StockVerificationMapDTO
    {

        public StockVerificationMapDTO()
        {
            ProductSKU = new KeyValueDTO();
        }

        [DataMember]
        public KeyValueDTO ProductSKU { get; set; }


        [DataMember]
        public KeyValueDTO SKUID { get; set; }


        [DataMember]
        public long? ProductID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public decimal? AvailableQuantity { get; set; }

        [DataMember]
        public Nullable<decimal> Quantity { get; set; }


        [DataMember]
        public long DetailIID { get; set; }
        [DataMember]
        public long? HeadID { get; set; }
        [DataMember]
        public long? ReferenceHeadID { get; set; }
        [DataMember]
        public long? ProductSKUMapID { get; set; }
        [DataMember]
        public decimal? Fraction { get; set; }
        [DataMember]
        public long? UnitGroupID { get; set; }
        [DataMember]
        public long? UnitID { get; set; }
        [DataMember]
        public decimal? PhysicalUnitPrice { get; set; }
        [DataMember]
        public decimal? PhysicalQuantity { get; set; }
        [DataMember]
        public decimal? PhysicalAmount { get; set; }
        [DataMember]
        public decimal? ActualUnitPrice { get; set; }
        [DataMember]
        public decimal? ActualQuantity { get; set; }
        [DataMember]
        public decimal? ActualAmount { get; set; }
        [DataMember]
        public decimal? DifferUnitPrice { get; set; }
        [DataMember]
        public decimal? DifferQuantity { get; set; }
        [DataMember]
        public decimal? DifferAmount { get; set; }
        [DataMember]
        public decimal? ExchangeRate { get; set; }
        [DataMember]
        public DateTime? WarrantyDate { get; set; }
        [DataMember]
        public long? CreatedBy { get; set; }
        [DataMember]
        public long? UpdatedBy { get; set; }
        [DataMember]
        public DateTime? CreatedDate { get; set; }
        [DataMember]
        public DateTime? UpdatedDate { get; set; }
        [DataMember]
        public byte[] TimeStamps { get; set; }
        [DataMember]
        public string SerialNumber { get; set; }
        [DataMember]
        public long? ParentDetailID { get; set; }
        [DataMember]
        public string Remark { get; set; }
        [DataMember]
        public DateTime? WarrantyStartDate { get; set; }
        [DataMember]
        public DateTime? WarrantyEndDate { get; set; }
        [DataMember]
        public int? CostCenterID { get; set; }
        [DataMember]
        public string BarCode { get; set; }
        [DataMember]
        public long? CartItemID { get; set; }
        [DataMember]
        public int? ProductOptionID { get; set; }
        [DataMember]
        public decimal? LastCostPrice { get; set; }

        [DataMember]
        public decimal? CorrectedQuantity { get; set; }

        [DataMember]
        public long InventoryTransactionIID { get; set; }

        //for update screen 
        [DataMember]
        public string PhysicalVerTransNo { get; set; }

        [DataMember]
        public long StockVerficationDetailIID { get; set; }

        [DataMember]
        public decimal? BookStock { get; set; }
    }
}
