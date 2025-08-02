using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Library
{
    [DataContract]
    public class LibraryBookConditionDTO : BaseMasterDTO
    {
         [DataMember]
        public byte  BookConditionID { get; set; }
        [DataMember]
        public string  BookConditionName { get; set; }
    }
}


