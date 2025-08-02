using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class IP2CountryDTO
    {
        [DataMember]
        public Nullable<long> BeginningIP { get; set; }
        [DataMember]
        public Nullable<long> EndingIP { get; set; }
        [DataMember]
        public Nullable<long> AssignedIP { get; set; }
        [DataMember]
        public string TwoCountryCode { get; set; }
        [DataMember]
        public string ThreeCountryCode { get; set; }
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public int IP2CountryID { get; set; }

    }

    
}
