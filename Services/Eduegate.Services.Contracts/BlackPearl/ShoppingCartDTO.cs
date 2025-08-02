using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class ShoppingCartDTO
    {
        public ShoppingCartDTO()
        {
            DaysID = new List<byte>();
        }

        [DataMember]
        public List<ProductDetailDTO> Products { get; set; }

        [DataMember]
        public decimal SubTotal { get; set; }

        [DataMember]
        public decimal Total { get; set; }

        [DataMember]
        public long? ShoppingCartIID { get; set; }

        [DataMember]
        public string CartID { get; set; }

        


        [DataMember]
        public short? SubscriptionTypeID { get; set; }

        //[DataMember]
        //public DateTime? StartDate { get; set; }

        //[DataMember]
        //public DateTime? EndDate { get; set; }

        [DataMember]
        public string StartDate { get; set; }

        [DataMember]
        public string EndDate { get; set; }

        [DataMember]
        public List<byte> DaysID { get; set; }

        [DataMember]
        public int DeliveryDaysCount { get; set; }

    }


    [DataContract]
    public class CartDTO
    {
        [DataMember]
        public List<CartProductDTO> Products { get; set; }

        [DataMember]
        public long ShoppingCartID { get; set; }

        [DataMember]
        public string SubTotal { get; set; }

        [DataMember]
        public string Total { get; set; }

        [DataMember]
        public string UserMessage { get; set; }

        [DataMember]
        public bool IsVoucherApplied { get; set; }

        [DataMember]
        public string VoucherValue { get; set; }

        [DataMember]
        public long? VoucherID { get; set; }

        [DataMember]
        public string PaymentMethod { get; set; }

        [DataMember]
        public Nullable<long> ShippingAddressID { get; set; }

        [DataMember]
        public Nullable<long> BillingAddressID { get; set; }

        [DataMember]
        public bool IsDeliveryCharge { get; set; }

        [DataMember]
        public decimal DeliveryCharge { get; set; }


        [DataMember]
        public List<DeliveryDTO> DeliveryTypes { get; set; }

        [DataMember]
        public bool IsCartItemDeleted { get; set; }
        [DataMember]
        public bool IsCartItemOutOfStock { get; set; }
        [DataMember]
        public bool IsCartItemQuantityAdjusted { get; set; }

        [DataMember]
        public bool IsOnlineBranchPhysicalCartItems { get; set; }

        [DataMember]
        public bool IsDeliveryChargeAssigned { get; set; }

        [DataMember]
        public bool IsEmailDeliveryInCart { get; set; }

        [DataMember]
        public bool IsStorePickUpInCart { get; set; }

        [DataMember]
        public List<PaymentMethodDTO> PaymentMethods { get; set; }


        [DataMember]
        public decimal CartDigitalAmount { get; set; }


        [DataMember]

        public long? CustomerID { get; set; }

        [DataMember]

        public string Currency { get; set; }

        [DataMember]

        public bool isDigitalCart { get; set; }
        [DataMember]
        public Nullable<int> CompanyID { get; set; }

        [DataMember]
        public bool IsIntlCart { get; set; }

        [DataMember]
        public bool IsProceedToPayment { get; set; }

        [DataMember]
        public string DisplayText { get; set; }
        [DataMember]
        public decimal? DeliveryDistance { get; set; }
        [DataMember]
        public long? BranchID { get; set; }

        [DataMember]
        public decimal Discount { get; set; }

        [DataMember]
        public int? DeliveryTimeslotID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }
    }
}
