using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Web.Library.ViewModels.Checkout
{
    public class OrderViewModel
    {
        public string OrderNo { get; set; }

        public string  DeliveryType { get; set; }

        public List<OrderDetailViewModel> OrderDetails { get; set; }
        public string DeliveryDisplayText { get; set; }

        //public static OrderViewModel ToViewModel(TransactionHeadDTO dto)
        //{
        //    return new OrderViewModel()
        //    {
        //        OrderNo = dto.TransactionNo,
        //    };
        //}

    }
}
