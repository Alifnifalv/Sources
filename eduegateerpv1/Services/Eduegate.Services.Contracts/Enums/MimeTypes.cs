using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "MimeType")]
    public enum MimeType
    {
        [EnumMember]
        XML,
        [EnumMember]
        CSV,
        [EnumMember]
        IMAGE,
        [EnumMember]
        PDF,
        [EnumMember]
        MHTML,
        [EnumMember]
        EXCEL,
        [EnumMember]
        WORD,
    }
}
