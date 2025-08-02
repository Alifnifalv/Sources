using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwCustomerOrder
    {
        [Key]
        public long OrderID { get; set; }
        public long RefCustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<long> RefCountryID { get; set; }
        public Nullable<long> RefAreaID { get; set; }
        public string Block { get; set; }
        public string Street { get; set; }
        public string BuildingNo { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string OtherDetails { get; set; }
        public string BillingFirstName { get; set; }
        public string BillingLastName { get; set; }
        public string BillingBlock { get; set; }
        public string BillingStreet { get; set; }
        public string BillingBuildingNo { get; set; }
        public string BillingTelephone { get; set; }
        public long BillingRefAreaID { get; set; }
        public long BillingCountryID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public byte RefOrderStatusID { get; set; }
        public byte DeliveryPrefrence { get; set; }
        public byte DeliveryMethod { get; set; }
        public decimal DeliveryCharges { get; set; }
        public decimal DeliveryChargesCancellation { get; set; }
        public Nullable<System.DateTime> SpecificDeliveryDate { get; set; }
        public Nullable<bool> Cancelled { get; set; }
        public string CancelledBy { get; set; }
        public Nullable<System.DateTime> CancelledOn { get; set; }
        public string CancellationRemarks { get; set; }
        public Nullable<decimal> CancellRefundAmount { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public Nullable<byte> PaymentMethod { get; set; }
        public string PaymentRemarks { get; set; }
        public string DeliveryDetails { get; set; }
        public Nullable<decimal> AdditionalCharges { get; set; }
        public Nullable<decimal> PromotionalDiscount { get; set; }
        public decimal ItemAmount { get; set; }
        public decimal OrderAmount { get; set; }
        public string OrderIP { get; set; }
        public string OrderCountry { get; set; }
        public long TransID { get; set; }
        public long TrackID { get; set; }
        public long TrackKey { get; set; }
        public string SessionID { get; set; }
        public long PaymentID { get; set; }
        public string TransRef { get; set; }
        public Nullable<System.DateTime> TransOn { get; set; }
        public string ReceiptNumber { get; set; }
        public Nullable<System.DateTime> DispatchDate { get; set; }
        public string DispatchRemarks { get; set; }
        public string DeliveryRemarks { get; set; }
        public Nullable<bool> Returned { get; set; }
        public string ReturnedBy { get; set; }
        public Nullable<System.DateTime> ReturnedOn { get; set; }
        public Nullable<System.DateTime> ReturnedApprovedOn { get; set; }
        public string ReturnRemarks { get; set; }
        public Nullable<decimal> ReturnAmount { get; set; }
        public Nullable<decimal> ReturnAmountApprove { get; set; }
        public Nullable<bool> OrderClosed { get; set; }
        public Nullable<bool> LoyaltyPointAssigned { get; set; }
        public Nullable<int> LotaltyPoints { get; set; }
        public string OrderClosedBy { get; set; }
        public Nullable<decimal> FinalTotal { get; set; }
        public Nullable<bool> CategorizationPointAssigned { get; set; }
        public Nullable<int> CategorizationPoints { get; set; }
        public bool IsVoucherPayment { get; set; }
        public Nullable<decimal> ActualDeliveryCost { get; set; }
        public Nullable<decimal> DeliveryDiscount { get; set; }
        public Nullable<decimal> cnActualDeliveryCost { get; set; }
        public Nullable<decimal> cnDeliveryDiscount { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string EmailID { get; set; }
        public string CountryNameEn { get; set; }
        public string Status { get; set; }
        public string AreaNameEn { get; set; }
        public string BillCountry { get; set; }
        public string BillArea { get; set; }
        public decimal VoucherAmount { get; set; }
        public bool MultiPrice { get; set; }
        public Nullable<bool> ResendEmail { get; set; }
        public string DeliveryDriver { get; set; }
        public byte RouteID { get; set; }
        public string OrderLang { get; set; }
        public short OrderSizeID { get; set; }
    }
}
