using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.CustomEntity
{
    public class HistoryHeader
    {
        public Nullable<int> DocumentTypeID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public Nullable<long> SupplierID { get; set; }
        public long TransactionOrderIID { get; set; }
        public string TransactionNo { get; set; }
        public Nullable<long> ParentTransactionOrderIID { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public string Description { get; set; }
        public string PaymentMethod { get; set; }
        public decimal? VoucherAmount { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public List<HistoryDetail> OrderDetails { get; set; }
        public long TransactionStatus { get; set; }
        public long ActualOrderStatus { get; set; }
        public string VoucherNo { get; set; }

        public Eduegate.Domain.Entity.Models.OrderContactMap DeliveryAddress { get; set; }

        public Eduegate.Domain.Entity.Models.OrderContactMap BillingAddress { get; set; }

        public string DeliveryCountryName { get; set; }
        public string DeliveryAreaName { get; set; }

        public string DeliveryCityName { get; set; }

        public string BillingCountryName { get; set; }
        public string BillingAreaName { get; set; }

        public string BillingCityName { get; set; }

        public long LoyaltyPoints { get; set; }

        public decimal DeliveryCharge { get; set; }

        public int DeliveryTypeID { get; set; }
        public int DeliveryDays { get; set; }
        public string DeliveryText { get; set; }
        public Nullable<int> CompanyID { get; set; } 
        public Nullable<long> DocumentStatusID { get; set; }

        public string BranchCode { get; set; }

        public string BranchName { get; set; }

        public string CartPaymentMethod { get; set; }

        public int? LocationId { get; set; }

        public long? StudentID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string SubscriptionType { get; set; }

        public string DeliveryTimeSlots { get; set; }

        public List<Eduegate.Domain.Entity.Models.ShoppingCartWeekDayMap> SubciptionDeliveryDays { get; set; }
    }
}
