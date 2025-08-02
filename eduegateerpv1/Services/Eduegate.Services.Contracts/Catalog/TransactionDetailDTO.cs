using Eduegate.Framework.Contracts.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class TransactionDetailDTO
    {

        [DataMember]
        public long DetailIID { get; set; }

        [DataMember]
        public Nullable<long> HeadID { get; set; }

        [DataMember]
        public Nullable<long> ProductID { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public Nullable<long> ProductSKUMapID { get; set; }

        [DataMember]
        public string ImageFile { get; set; }

        [DataMember]
        public string Barcode { get; set; }

        [DataMember]
        public string PartNo { get; set; }

        [DataMember]
        public string SKU { get; set; }

        [DataMember]
        public Nullable<decimal> Quantity { get; set; }

        [DataMember]
        public Nullable<decimal> SellingQuantityLimit { get; set; }

        [DataMember]
        public Nullable<long> UnitID { get; set; }

        [DataMember]
        public Nullable<decimal> DiscountPercentage { get; set; }

        [DataMember]
        public Nullable<decimal> UnitPrice { get; set; }

        [DataMember]
        public decimal? CostPrice { get; set; }

        [DataMember]
        public int? DocumentTypeID { get; set; }

        [DataMember]
        public Nullable<decimal> Amount { get; set; }

        [DataMember]
        public Nullable<decimal> ExchangeRate { get; set; }

        [DataMember]
        public Nullable<System.DateTime> WarrantyDate { get; set; }

        [DataMember]
        public Nullable<long> CreatedBy { get; set; }

        [DataMember]
        public Nullable<long> UpdatedBy { get; set; }

        [DataMember]
        public string CreatedDate { get; set; }

        [DataMember]
        public string UpdatedDate { get; set; }

        [DataMember]
        public List<ProductSerialMapDTO> SKUDetails { get; set;}

        [DataMember]
        public List<TransactionAllocationDTO> TransactionAllocations { get; set; }

        [DataMember]
        public bool IsSerialNumberOnPurchase { get; set; }

        [DataMember]
        public bool IsSerialNumber { get; set; }

        [DataMember]
        public Nullable<long> BatchID { get; set; }

        [DataMember]
        public string SerialNumber { get; set; }

        [DataMember]
        public int ProductLength { get; set; }

        [DataMember]
        public string ProductTypeName { get; set; }

        [DataMember]
        public bool IsError { get; set; }

        [DataMember]
        public long? ParentDetailID { get; set; }

        [DataMember]
        public int? Action { get; set; }

        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public decimal? TaxAmount1 { get; set; }

        [DataMember]
        public decimal? TaxAmount2 { get; set; }

        [DataMember]
        public int? TaxTemplateID { get; set; }

        [DataMember]
        public decimal? TaxPercentage { get; set; }

        [DataMember]
        public bool? HasTaxInclusive { get; set; }

        [DataMember]
        public decimal? InclusiveTaxAmount { get; set; }

        [DataMember]
        public decimal? ExclusiveTaxAmount { get; set; }

        [DataMember]
        public DateTime? WarrantyStartDate { get; set; }

        [DataMember]
        public DateTime? WarrantyEndDate { get; set; }

        [DataMember]
        public int? CostCenterID { get; set; }

        [DataMember]
        public KeyValueDTO CostCenter { get; set; }

        [DataMember]
        public Nullable<decimal> LandingCost { get; set; }

        [DataMember]
        public Nullable<decimal> LastCostPrice { get; set; }

        [DataMember]
        public Nullable<decimal> Fraction { get; set; }

        [DataMember]
        public string Unit { get; set; }
        [DataMember]
        public Nullable<decimal> ForeignAmount { get; set; }
        [DataMember]
        public Nullable<decimal> ForeignRate { get; set; }

        [DataMember]
        public Nullable<long> UnitGroupID { get; set; }

        [DataMember]
        public List<KeyValueDTO> UnitDTO { get; set; }

        [DataMember]
        public List<UnitsDTO> UnitList { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public decimal? DiscountAmount { get; set; }

        [DataMember]
        public long? CartItemID { get; set; }

        [DataMember]
        public decimal? ActualUnitPrice { get; set; }

        [DataMember]
        public decimal? AvailableQuantity { get; set; }

        //For Purchase Quotation
        [DataMember]
        public KeyValueDTO SKUID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string QuotationNo { get; set; }

        [DataMember]
        public long? SupplierID { get; set; }

        [DataMember]
        public string Supplier { get; set; } 
        
        [DataMember]
        public string SupplierCode { get; set; }

        //For Bid Opening
        [DataMember]
        public string SubmittedDateString { get; set; }  
        
        [DataMember]
        public decimal? GrossAmount { get; set; } 
        
        [DataMember]
        public decimal? QTDiscount { get; set; }
    }
}
