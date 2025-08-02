using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eduegate.Web.Library.ViewModels
{
    public class PointOfSaleViewModel
    {
        public long TransactionIID { get; set; }

        public string Product { get; set; }

        public decimal ProductIID { get; set; }

        public string Description { get; set; }

        public string Customer { get; set; }

        public string SettingCode { get; set; }

        public decimal CustomerIID { get; set; }

        public decimal ProductSKUMapIID { get; set; }

        public string ProductSKU { get; set; }

        public decimal Quantity { get; set; } //Quantity count

        public decimal QuantityText { get; set; } //Binded to the textbox in grid

        public decimal UnitPrice { get; set; }

        public decimal DiscountPercentage { get; set; }

        public decimal Price { get; set; } // Caluculating price based on the quantity entered by the textbox in the grid

        public decimal ProductPrice { get; set; } //Product price of one item

        public decimal TotalPrice { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TotalDiscount { get; set; }

        public int TotalDiscountPercentage { get; set; }

        public decimal TotalProductDiscount { get; set; }

        public CustomerViewModel CustomerModel { get; set; }

        public CommentViewModel Comment { get; set; }

        public PaymentViewModel Payment { get; set; }
    }
}