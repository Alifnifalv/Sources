using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Library
{
    [DataContract]
    public class LibraryBookCategoryMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public LibraryBookCategoryMapDTO()
        {
            BookCategory = new KeyValueDTO();
        }
        [DataMember]
        public long LibraryBookCategoryMapIID { get; set; }
        [DataMember]
        public long? LibraryBookID { get; set; }
        [DataMember]
        public long? BookCategoryID { get; set; }

        [DataMember]
        public KeyValueDTO BookCategory { get; set; }
    }
}
