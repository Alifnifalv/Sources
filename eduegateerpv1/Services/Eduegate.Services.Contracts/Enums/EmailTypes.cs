using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "EmailTypes")]
    public enum EmailTypes
    {
        [EnumMember]
        AutoFeeReceipt = 1,
        [EnumMember]
        ResendFeeReceipt = 2,
        [EnumMember]
        FeeDueStatement = 3,
        [EnumMember]
        SalesOrderConfirmation = 4,
        [EnumMember]
        SalesInvoiceGeneration = 5,
        [EnumMember]
        SalarySlipGeneration = 6,
        [EnumMember]
        LeadCreation = 7,
        [EnumMember]
        LeadCommunication = 8,
        [EnumMember]
        CanteenOrderConfirmation = 9,
        [EnumMember]
        CircularPublish = 10,
        [EnumMember]
        Attendance = 11,
        [EnumMember]
        OTPGeneration = 12,
        [EnumMember]
        ShoppingCartOrder = 13,
        [EnumMember]
        StudentApplicaton = 14,
        [EnumMember]
        StudentAdmission = 15,
        [EnumMember]
        TCProcess = 16,
        [EnumMember]
        TransportCreation = 17,
        [EnumMember]
        WorkFlowCreation = 18,
        [EnumMember]
        Ticketing = 19,
        [EnumMember]
        ParentCommunication = 20,
        [EnumMember]
        PushNotification = 21,
        [EnumMember]
        SignupMeeting = 22,
        [EnumMember]
        MeetingRequest = 23,
        [EnumMember]
        CounselingMail = 24,
        [EnumMember]
        MeetingUpdate = 25,
        [EnumMember]
        TenderCommittieeCredentails = 26, 
        [EnumMember]
        SupplierCredentails = 27, 
        [EnumMember]
        TransactionMailToSupplier = 28, 
        [EnumMember]
        QuotationSubmission = 29, 
        [EnumMember]
        VendorPortal = 30,
    }
}