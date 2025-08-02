using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Inventory;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class TransactionHeadDTO
    {
        public TransactionHeadDTO()
        {
            this.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapDTO>();
            this.AdditionalExpensesTransactionsMaps = new List<AdditionalExpensesTransactionsMapDTO>();
            TaxDetails = new List<TaxDetailsDTO>();
            SupplierList = new List<KeyValueDTO>();
            PurchaseRequests = new List<KeyValueDTO>();
            Quotations = new List<KeyValueDTO>();
            Tender = new KeyValueDTO();
        }

        [DataMember]
        public long HeadIID { get; set; }

        [DataMember]
        public Nullable<long> BranchID { get; set; }

        [DataMember]
        public string BranchName { get; set; }

        [DataMember]
        public Nullable<long> ToBranchID { get; set; }

        [DataMember]
        public string ToBranchName { get; set; }

        [DataMember]
        public Nullable<int> DocumentTypeID { get; set; }

        [DataMember]
        public string DocumentTypeName { get; set; }

        [DataMember]
        public Nullable<DocumentReferenceTypes> DocumentReferenceTypeID { get; set; }

        [DataMember]
        public string TransactionNo { get; set; }

        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Reference { get; set; }

        [DataMember]
        public Nullable<long> CustomerID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public string StudentClassSectionDescription { get; set; }

        [DataMember]
        public string CustomerEmailID { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public Nullable<long> SupplierID { get; set; }

        [DataMember]
        public string SupplierName { get; set; }

        [DataMember]
        public Nullable<DateTime> TransactionDate { get; set; }

        [DataMember]
        public Nullable<byte> TransactionStatusID { get; set; }

        [DataMember]
        public Nullable<int> JobStatusID { get; set; } 

        //[DataMember]
        //public TransactionStatus TransactionStatus { get; set; }

        [DataMember]
        public Nullable<decimal> DiscountAmount { get; set; }

        [DataMember]
        public Nullable<decimal> DiscountPercentage { get; set; }

        [DataMember]
        public Nullable<int> CreatedBy { get; set; }

        [DataMember]
        public Nullable<int> UpdatedBy { get; set; }

        [DataMember]
        public string CreatedDate { get; set; }

        [DataMember]
        public string UpdatedDate { get; set; }

        //[DataMember]
        //public byte[] TimeStamps { get; set; }

        [DataMember]
        public List<TransactionDetailDTO> TransactionDetails { get; set; }

        [DataMember]
        public string PaymentMethod { get; set; }

        [DataMember]
        public decimal TotalPrice { get; set; }

        [DataMember]
        public Nullable<DateTime> DueDate { get; set;}

        [DataMember]
        public Nullable<int> CurrencyID { get; set; }

        [DataMember]
        public string CurrencyName { get; set; }

        [DataMember]
        public Nullable<int> DeliveryTypeID { get; set; }

        [DataMember]
        public string DeliveryTypeName { get; set; }

        [DataMember]
        public bool IsShipment { get; set; }

        [DataMember]
        public long EmployeeID { get; set;}

        [DataMember]
        public long? StaffID { get; set; }

        [DataMember]
        public string StaffName { get; set; } 

        [DataMember]
        public string EmployeeName { get; set; }

        [DataMember]
        public Nullable<byte> EntitlementID { get; set; }

        [DataMember]
        public Nullable<DateTime> DeliveryDate { get; set; }

        [DataMember]
        public Nullable<long> ReferenceHeadID { get; set; }

        [DataMember]
        public Nullable<int> CompanyID { get; set; }

        [DataMember]
        public Nullable<long> JobEntryHeadID { get; set; }

        [DataMember]
        public Nullable<decimal> DeliveryCharge { get; set; }

        [DataMember]
        public Nullable<decimal> ForeignAmount { get; set; }
        [DataMember]
        public Nullable<decimal> ExchangeRate { get; set; }

        [DataMember]
        public string InvoiceStatus { get; set; }

        [DataMember]
        public string DeliveryOption { get; set; }

        [DataMember]
        public short? DeliveryMethodID { get; set; }

        [DataMember]
        public List<TransactionHeadEntitlementMapDTO> TransactionHeadEntitlementMaps { get; set; }

        [DataMember]
        public List<AdditionalExpensesTransactionsMapDTO> AdditionalExpensesTransactionsMaps { get; set; }        

        [DataMember]
        public Nullable<int> DeliveryDays { get; set; }

        [DataMember]
        public List<KeyValueDTO> Entitlements { get; set; }

        [DataMember]
        public bool IsTransactionCompleted { get; set; }
        [DataMember]
        public string TransactionStatusName { get; set; }
        [DataMember]
        public Nullable<long> DocumentStatusID { get; set; }
        [DataMember]
        public string DocumentStatusName { get; set; }
        [DataMember]
        public string ReferenceTransactionNo { get; set; }

        [DataMember]
        public string ErrorCode { get; set; }
        [DataMember]
        public bool IsError { get; set; }

        [DataMember]
        public string DocumentReferenceType { get; set; }

        [DataMember]
        public Nullable<int> TransactionRole { get; set; }


        [DataMember]
        public decimal TransactionLoyaltyPoints { get; set; }

        [DataMember]
        public Nullable<DateTime> DocumentCancelledDate { get; set; }

        [DataMember] 
        public Nullable<int> ReceivingMethodID { get; set; }

        [DataMember]
        public string ReceivingMethodName { get; set; }

        [DataMember]
        public Nullable<int> ReturnMethodID { get; set; }

        [DataMember]
        public string ReturnMethodName { get; set; }

        [DataMember]
        public List<TaxDetailsDTO> TaxDetails { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string EmailID { get; set; }

        [DataMember]
        public Nullable<long> FeeReceiptID { get; set; }
   
        [DataMember]
        public decimal? TotalLandingCost { get; set; }
        [DataMember]
        public decimal? LocalDiscount { get; set; }

        [DataMember]
        public int? DeliveryTimeslotID { get; set; }
        [DataMember]
        public decimal? InvoiceLocalAmount { get; set; }
        [DataMember]
        public decimal? InvoiceForeignAmount { get; set; }
        [DataMember]
        public decimal? PaidAmount { get; set; }

        [DataMember]
        public long AgainstReferenceHeadID { get; set; }


        //For purchase Request Screen
        [DataMember]
        public long? RequestedByID { get; set; }

        [DataMember]
        public long? DepartmentID { get; set; } //RequestedDepartmentID

        [DataMember]
        public long? ApproverID { get; set; }
        
        [DataMember]
        public long? BidID { get; set; }  
        
        [DataMember]
        public string Bid { get; set; }

        [DataMember]
        public string ApproverName { get ; set; }

        [DataMember]
        public string Department { get; set; }

        //For RFQ

        [DataMember]
        public long? RFQID { get; set; }

        [DataMember]
        public string RFQ { get; set; }

        [DataMember]
        public List<KeyValueDTO> SupplierList { get; set; }

        [DataMember]
        public List<KeyValueDTO> PurchaseRequests { get; set; }

        [DataMember]
        public List<KeyValueDTO> Quotations { get; set; }

        [DataMember] 
        public string Validity { get; set; }

        [DataMember]
        public bool IsQuotation { get; set; }

        [DataMember]
        public long? TenderID { get; set; }  
        
        [DataMember]
        public bool SendMailNotification { get; set; }   
        
        [DataMember]
        public KeyValueDTO Tender { get; set; }

        //For Bid Opening
        [DataMember]
        public string TenderName { get; set; }
        
        [DataMember]
        public string TenderDescription { get; set; }

        [DataMember]
        public long? SupplierLogiID { get; set; }

    }

    public class MarketPlaceTransactionActionDTO : TransactionHeadDTO
    {
        public string Reason { get; set; }
    }
}
