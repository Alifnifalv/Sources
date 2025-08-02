using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Students
{
    public class StudentAddressDTO
    {
        [DataMember]
        public bool IsCurrentAddress { get; set; }
        [DataMember]
        public string CurrentAddress { get; set; }
        [DataMember]
        public string IsPermanentAddress { get; set; }
        [DataMember]
        public string PermanentAddress { get; set; }
    }
}
