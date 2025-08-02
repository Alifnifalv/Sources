using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Web.Library.ViewModels
{
    public class OrderTrackingViewModel
    {
        
        // Tracking properties
        public long OrderTrackingIID { get; set; }
        public Nullable<long> OrderID { get; set; }
        public long StatusID { get; set; }
        public string StatusValue
        {
            get
            {
                return this.TransactionStatus.ToString();
            }
        }
        public TransactionStatus TransactionStatus { get; set; }
        public string Description { get; set; }
        
        public Nullable<System.DateTime> StatusDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public PaymentViewModel PaymentDetail { get; set; }

        // OrderHistory properties
        public OrderHistoryViewModel Order { get; set; }

        // Address properties
        public ContactsViewModel ShippingContact { get; set; }
        public ContactsViewModel BillingContact { get; set; }

        public static OrderTrackingDTO ToDTO(OrderTrackingViewModel vm)
        {
            Mapper<OrderTrackingViewModel, OrderTrackingDTO>.CreateMap();
            return Mapper<OrderTrackingViewModel, OrderTrackingDTO>.Map(vm);
        }

        public static OrderTrackingViewModel ToVM(OrderTrackingDTO dto)
        {
            Mapper<OrderTrackingDTO, OrderTrackingViewModel>.CreateMap();
            return Mapper<OrderTrackingDTO, OrderTrackingViewModel>.Map(dto);
        }

    }
}
