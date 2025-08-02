using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract]
    public enum DocumentReferenceTypes
    {
        [EnumMember]
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
        AssetEntryManual = 400,
        [EnumMember]
        AssetDepreciation = 401,
        [EnumMember]
        AssetRemoval = 402,
        [EnumMember]
        AssetEntryPurchase = 403,
        [EnumMember]
        AssetTransferRequest = 404,
        [EnumMember]
        AssetTransferIssue = 405,
        [EnumMember]
        AssetTransferReceipt = 406,
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
        [EnumMember]
        PurchaseRequest = 64,
        [EnumMember]
        RFQ = 65,
        [EnumMember]
        StockVerification = 66,
        [EnumMember]
        StockUpdation = 67,
        [EnumMember]
        ReceiptVoucherMission = 501,
        [EnumMember]
        ReceiptVoucherInvoice = 505,
        [EnumMember]
        ReceiptVoucherRegularReceipt = 506,
        [EnumMember]
        PaymentVoucherInvoice = 507,
        [EnumMember]
        PaymentVoucherRegularReceipt = 508,
        [EnumMember]
        CreditNoteRegular = 509,
        [EnumMember]
        DebitNoteProduct = 510,
        [EnumMember]
        DebitNoteRegular = 511,
        [EnumMember]
        JournalVoucher = 512,
        [EnumMember]
        TicketsExternal = 300,
        [EnumMember]
        TicketsInHouse = 301,
    }
}