using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Enums.Notifications
{
    //notification.EmailTemplates
    public enum EmailTemplates
    {
        [EnumMember]
        DefaultMail = 1,
        [EnumMember]
        TCMail = 2,
        [EnumMember]
        StudentTransportMail = 3,
        [EnumMember]
        FeeDueStatementMail = 4,
        [EnumMember]
        TicketingMail = 5,
        [EnumMember]
        CanteenConfirmationMail = 6,
        [EnumMember]
        ProformaMail = 7,
        [EnumMember]
        MeetingRequestMail = 8,
        [EnumMember]
        CounselingMail = 9,
        [EnumMember]
        AdminListingMail = 10,
        [EnumMember]
        SalarySlipGeneration = 11,
        [EnumMember]
        SupplierWelcomeMail = 12,  
        [EnumMember]
        TransactionMailToSupplier = 13,  
        [EnumMember]
        QuotationSubmission = 14,   
        [EnumMember]
        BidOpeningCredential = 15,
        [EnumMember]
        AdmissionWelcomeMail = 16, 
        [EnumMember]
        FeeReceiptMail = 17,   
        [EnumMember]
        JobSeekerRegistration = 18,  
        [EnumMember]
        OTPMail = 19,   
        [EnumMember]
        JobShortListedMail = 20,    
        [EnumMember]
        StudentApplicationAppliedMail = 21,
        [EnumMember]
        JobInterviewMail = 22,
        [EnumMember]
        TicketingGenerateMailToEmployee = 23,
        [EnumMember]
        MeetingRequestMailToFaculty = 24,
        [EnumMember]
        TransportCancellationRequest = 25,  
        [EnumMember]
        TransportCancellationRequestStatus = 26,
        [EnumMember]
        Circular = 27, 
    }
}