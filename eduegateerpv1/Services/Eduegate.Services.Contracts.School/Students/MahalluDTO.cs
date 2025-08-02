using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class MahalluDTO : BaseMasterDTO
    {
         [DataMember]
        public long  MahalluIID { get; set; }
        [DataMember]
        public string  MahalluName { get; set; }
        [DataMember]
        public string  Place { get; set; }
        [DataMember]
        public string  Post { get; set; }
        [DataMember]
        public string  Pincode { get; set; }
        [DataMember]
        public string  District { get; set; }
        [DataMember]
        public string  State { get; set; }
        [DataMember]
        public string  Phone { get; set; }
        [DataMember]
        public string  Email { get; set; }
        [DataMember]
        public string  Fax { get; set; }
        [DataMember]
        public string  WaqafNumber { get; set; }
        [DataMember]
        public System.DateTime?  EstablishedOn { get; set; }
        [DataMember]
        public string  MahalluArea { get; set; }
        [DataMember]
        public string  Description { get; set; }
        [DataMember]
        public string  Logo { get; set; }
        [DataMember]
        public System.DateTime?  CurrentDate { get; set; }
        [DataMember]
        public string  Extra1 { get; set; }
        [DataMember]
        public System.DateTime?  ExtraDate { get; set; }
    }
}


