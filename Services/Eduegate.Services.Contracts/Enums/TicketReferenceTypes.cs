using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "TicketReferenceTypes")]
    public enum TicketReferenceTypes
    {
        [EnumMember]
        Complaint = 1,
        [EnumMember]
        Enquiry = 2,
        [EnumMember]
        CustomerSupport = 3,
        [EnumMember]
        Attendance = 4,
        [EnumMember]
        FeeDue = 5,
        [EnumMember]
        FeeCollection = 6,
        [EnumMember]
        CreditNote = 7,        
    }
}