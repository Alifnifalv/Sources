using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.School.Students;

namespace Eduegate.Services.Contracts.OrderHistory
{
    [DataContract]
    public class OrderHistoryDTO
    {
        public OrderHistoryDTO()
        {
            StudentDetails = new StudentDTO();
        }

        [DataMember]
        public long TransactionOrderIID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string TransactionNo { get; set; }

        [DataMember]
        public Nullable<long> ParentTransactionOrderIID { get; set; }
        
        [DataMember]
        public string ParentTransactionNo { get; set; }

        [DataMember]
        public Nullable<System.DateTime> TransactionDate { get; set; }

        [DataMember]
        public Nullable<int> DocumentTypeID { get; set; }

        [DataMember]
        public Nullable<long> CustomerID { get; set; }

        [DataMember]
        public Nullable<long> SupplierID { get; set; }

        [DataMember]
        public List<OrderDetailDTO> OrderDetails { get; set; }

        [DataMember]
        public UserDTO UserDetail { get; set; }

        [DataMember]
        public string PaymentMethod { get; set; }

        [DataMember]
        public decimal Total { get; set; }

        [DataMember]
        public decimal SubTotal { get; set; }

        [DataMember]
        public decimal VoucherAmount { get; set; }
        [DataMember]
        public Nullable<decimal> DeliveryCharge { get; set; }
        [DataMember]
        public Nullable<decimal> DiscountAmount { get; set; }
        [DataMember]
        public Nullable<decimal> DiscountPercentage { get; set; }
        [DataMember]
        public Nullable<decimal> Discount { get; set; }
        [DataMember]
        public TransactionStatus TransactionStatus { get; set; }
        [DataMember]
        public ActualOrderStatus ActualOrderStatus { get; set; }

        [DataMember]
        public string StatusTransaction { get; set; }

        [DataMember]
        public string VoucherNo { get; set; }

        [DataMember]
        public OrderContactMapDTO DeliveryAddress { get; set; }


        [DataMember]
        public OrderContactMapDTO BillingAddress { get; set; }

        [DataMember]
        public long LoyaltyPoints { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public string DeliveryText { get; set; }

        [DataMember]
        public long DeliveryTypeID { get; set; }

        [DataMember]
        public bool ResendMail { get; set; }

        [DataMember]
        public Nullable<int> CompanyID { get; set; }

        [DataMember]
        public string DeliveryDisplayText { get; set; }

        [DataMember] 
        public Nullable<long> DocumentStatusID { get; set; }

        [DataMember]
        public List<KeyValueDTO> ReplacementActions { get; set; }

        [DataMember]
        public string TimeSlotText { get; set; }

        [DataMember]
        public List<OrderEntitlementDTO> EntitlementMaps { get; set; }

        [DataMember]
        public Nullable<long> CartID { get; set; }

        [DataMember]
        public string CartPaymentMethod { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public long? BranchID { get; set; }

        [DataMember]
        public string BranchCode { get; set; }

        [DataMember]
        public string BranchName { get; set; }

        [DataMember]
        public int JobStatusID { get; set; }

        [DataMember]
        public string TransactionDateString { get; set; }

        [DataMember]
        public string DocumentStatus { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public StudentDTO StudentDetails { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public string SectionName { get; set; }

        [DataMember]
        public string DeliveryTypeName { get; set; }

        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        public DateTime? EndDate { get; set; }

        [DataMember]
        public string SubscriptionType { get; set; }

        [DataMember]
        public string DeliveryTimeSlots { get; set; }

        [DataMember]
        public List<KeyValueDTO> SubciptionDeliveryDays { get; set; }
    }
}
