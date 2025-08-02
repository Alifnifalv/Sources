using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "TicketReferenceTypes")]
    public enum TicketReferenceTypes
    {
        [EnumMember]
        FeeDue = 1,
        [EnumMember]
        FeeCollection = 2,
        [EnumMember]
        CreditNote = 3,
        [EnumMember]
        Attendance = 4,
        [EnumMember]
        CustomerSupport = 5,
        [EnumMember]
        Complaint = 6,
        [EnumMember]
        Enquiry = 7,
    }
}