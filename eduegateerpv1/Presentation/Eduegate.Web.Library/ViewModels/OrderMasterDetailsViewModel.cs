using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Helper;
using Eduegate.Services.Contracts.OrderHistory;
using Eduegate.Framework.Translator;

namespace Eduegate.Web.Library.ViewModels
{
    public  class OrderMasterDetailsViewModel
    {
        public long HeadID { get; set; }
        public List<OrderDetailViewModel> OrderDetails { get; set; }

        public OrderMasterDetailsViewModel()
        {
            OrderDetails = new List<OrderDetailViewModel>();
        }

        public OrderMasterDetailsViewModel(long OrderID)
        {
            HeadID = OrderID;
            OrderDetails = new List<OrderDetailViewModel>();
        }

        public static List<OrderMasterDetailsViewModel> ToViewModel(List<OrderHistoryDTO> orderHistoryDTOList, string _dateFormat,long headIID)
        {
            List<OrderMasterDetailsViewModel> orderHistoryList = new List<OrderMasterDetailsViewModel>();
            OrderMasterDetailsViewModel orderMasterDetailHistory = null;
            if (orderHistoryDTOList != null && orderHistoryDTOList.Count > 0)
            {
                foreach (OrderHistoryDTO orderHistoryDTO in orderHistoryDTOList)
                {
                    orderMasterDetailHistory = new OrderMasterDetailsViewModel();
                    orderMasterDetailHistory.HeadID = headIID;
                    orderMasterDetailHistory.OrderDetails = new List<OrderDetailViewModel>();

                    if (orderHistoryDTO.OrderDetails != null && orderHistoryDTO.OrderDetails.Count > 0)
                    {
                        foreach (OrderDetailDTO orderDetailDTO in orderHistoryDTO.OrderDetails)
                        {
                            orderMasterDetailHistory.OrderDetails.Add(FromDTO(orderDetailDTO));
                        }
                    }
                    orderHistoryList.Add(orderMasterDetailHistory);
                }
            }
            return orderHistoryList;
        }

        public static OrderDetailViewModel FromDTO(OrderDetailDTO orderDetailDTO)
        {
            Mapper<OrderDetailDTO, OrderDetailViewModel>.CreateMap();
            return Mapper<OrderDetailDTO, OrderDetailViewModel>.Map(orderDetailDTO);
        }
    }
}
