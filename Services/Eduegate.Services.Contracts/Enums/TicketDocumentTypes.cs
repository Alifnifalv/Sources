using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "TicketDocumentTypes")]
    public enum TicketDocumentTypes
    {
        [EnumMember]
        CustomerSupport = 150,
        [EnumMember]
        FeeOutStandingSupport = 151,
        [EnumMember]
        FeeCollectionSupport = 152,
        [EnumMember]
        ComplaintsSupport = 153,
        [EnumMember]
        EnquirySupport = 154,
    }
}