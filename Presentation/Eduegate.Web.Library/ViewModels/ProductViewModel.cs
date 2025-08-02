using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eduegate.Web.Library.ViewModels
{
    public class ProductViewModel
    {
        public long TotalProduct { get; set; }

        public string RecentlyAdded { get; set; }

        public string MostSellingProduct { get; set; }

        public long OutOfStocks { get; set; }

        public long PendingCreate { get; set; }

        public List<ProductItemViewModel> ProductItems { get; set; }
    }
}