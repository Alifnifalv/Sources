using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eduegate.Web.Library.ViewModels
{
    public class TransactionDetailViewModel
    {
        public TransactionDetailViewModel()
        {
            TaxTemplate = "none";
        }

        public long DetailIID { get; set; }
        public Nullable<long> HeadID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<long> UnitID { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> CostPrice { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public string TaxTemplate { get; set; }

        public int? TaxTemplateID { get; set; }
        public decimal? TaxPercentage { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? InclusiveTaxAmount { get; set; }
        public decimal? ExclusiveTaxAmount { get; set; }
        public bool? HasTaxInclusive { get; set; }

        //Extra property from entity which is using in the view

        public string ProductName { get; set; }
        public string ProductSKU { get; set; }
        public decimal QuantityText { get; set; } //Binded to the textbox in grid
        public string ImageFile { get; set; }
        public string BarCode { get; set; }
        //public string ProductTypeName { get; set; }
        //public string ProductLength { get; set; }

        public KeyValueViewModel SKUID { get; set; }
    }
}