using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract]
    public enum DocumentReferenceTypes
    {
        All = 0,
        [EnumMember]
        SalesOrder = 1,
        [EnumMember]
        PurchaseInvoice = 2,
        [EnumMember]
        SalesReturn = 3,
        [EnumMember]
        PurchaseReturn = 4,
        [EnumMember]
        SalesInvoice = 5,
        [EnumMember]
        PurchaseOrder = 6,
        [EnumMember]
        SalesQuote = 7,
        [EnumMember]
        PurchaseQuote = 8,
        [EnumMember]
        BranchTransfer = 9,
        [EnumMember]
        BranchTransferRequest = 10,
        [EnumMember]
        SalesReturnRequest = 11,
        [EnumMember]
        PurchaseReturnRequest = 12,
        [EnumMember]
        OrderChangeRequest = 13,
        [EnumMember]
        PurchaseTender = 14,
        [EnumMember]
        GoodsReceivedNotes = 15,
        [EnumMember]
        GoodsDeliveryNotes = 16,
        [EnumMember]
        SalesDeliveryNotes = 17,
        [EnumMember]
        AccountPurchaseInvoice = 50,
        [EnumMember]
        AccountSalesInvoice = 51,
        [EnumMember]
        Journal = 52,
        [EnumMember]
        Payments = 53,
        [EnumMember]
        Receipts = 54,
        [EnumMember]
        DebitNote = 57,
        [EnumMember]
        CreditNote = 58,
        [EnumMember]
        Expenses = 59,
        [EnumMember]
        CreditProduct = 60,
        [EnumMember]
        DebitProduct = 61,

        [EnumMember]
        InboundOperations = 100,
        [EnumMember]
        OutboundOperations = 101,
        [EnumMember]
        FailedOperations = 102,
        [EnumMember]
        MarketplaceOperations = 103,
        [EnumMember]
        DistributionJobs = 200,
        [EnumMember]
        ServiceJobs = 201,
        [EnumMember]
        WarehouseOperations = -100,

        [EnumMember]
        AssetEntry = 400,
        [EnumMember]
        AssetDepreciation = 401,
        [EnumMember]
        AssetRemoval = 402,
        [EnumMember]
        AssetMaster = 403,
        [EnumMember]
        BundleWrap = 18,
        [EnumMember]
        BundleUnWrap = 19,
        [EnumMember]
        ServiceEntry = 20,
        [EnumMember]
        AdditionalExpense= 62,
        [EnumMember]
        FOCSales = 63,
    }
}
