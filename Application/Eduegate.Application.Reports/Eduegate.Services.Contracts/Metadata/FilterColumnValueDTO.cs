using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Metadata
{
     [DataContract]
   public  class FilterColumnValueDTO
    {
        [DataMember]
        public long FilterColumnID { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string Value2 { get; set; }
        [DataMember]
        public Enums.Conditions FilterCondition { get; set; }
    }
}
