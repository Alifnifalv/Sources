using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class CompanyDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int CompanyID { get; set; }
        [DataMember]
        public string CompanyName { get; set; }
        [DataMember]
        public Nullable<int> CountryID { get; set; }
        [DataMember]
        public Nullable<Byte> StatusID { get; set; }
        [DataMember]
        public Nullable<int> BaseCurrencyID { get; set; }
        [DataMember]
        public Nullable<int> LanguageID { get; set; }
        [DataMember]
        public string RegistraionNo { get; set; }
        [DataMember]
        public Nullable<System.DateTime> RegistrationDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Logo { get; set; }
        [DataMember]
        public int? CurrencyID { get; set; }
        [DataMember]
        public string CurrencyCode { get; set; }
        [DataMember]
        public int DecimalPrecision { get; set; }
    }
}
