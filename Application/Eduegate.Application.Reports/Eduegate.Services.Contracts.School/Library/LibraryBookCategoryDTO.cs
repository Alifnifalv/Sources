using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Library
{
    [DataContract]
    public class LibraryBookCategoryDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public long  LibraryBookCategoryID { get; set; }
        [DataMember]
        public string  BookCategoryName { get; set; }

        [DataMember]
        public string CategoryCode { get; set; }
    }
}


