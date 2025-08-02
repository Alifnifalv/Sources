using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Mutual
{
    [DataContract]
    public class CompanyDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public int  CompanyID { get; set; }
        [DataMember]
        public string  CompanyName { get; set; }
        [DataMember]
        public int?  CompanyGroupID { get; set; }
        [DataMember]
        public int?  CountryID { get; set; }
        [DataMember]
        public int?  BaseCurrencyID { get; set; }
        [DataMember]
        public int?  LanguageID { get; set; }
        [DataMember]
        public string  RegistraionNo { get; set; }
        [DataMember]
        public System.DateTime?  RegistrationDate { get; set; }
        [DataMember]
        public System.DateTime?  ExpiryDate { get; set; }
        [DataMember]
        public string  Address { get; set; }
        [DataMember]
        public byte?  StatusID { get; set; }
    }
}


