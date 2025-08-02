using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Library
{
    [DataContract]
    public class LibraryTransactionTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public byte  LibraryTransactionTypeID { get; set; }
        [DataMember]
        public string  TransactionTypeName { get; set; }
    }
}


