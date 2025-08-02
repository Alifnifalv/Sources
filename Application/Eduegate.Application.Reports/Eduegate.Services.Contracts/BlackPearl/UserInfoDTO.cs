using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
   public class UserInfoDTO
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]        
        public string LastName { get; set; }

        [DataMember]
        public decimal IID { get; set; }

        [DataMember]
        public string ProfileImageURL { get; set; }
    }
}
