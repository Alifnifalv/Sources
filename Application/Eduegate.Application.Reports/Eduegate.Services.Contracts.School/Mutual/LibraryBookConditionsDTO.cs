using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Mutual
{
    [DataContract]
    public class LibraryBookConditionsDTO : BaseMasterDTO
    {
         [DataMember]
        public byte  BookConditionID { get; set; }
        [DataMember]
        public string  BookConditionName { get; set; }
    }
}


