using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public partial class BusinessDetailDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        public BusinessDetailDTO() {

            BusinessType = new KeyValueDTO();
            TaxJurisdictionCountry = new KeyValueDTO();
        }

        [DataMember]
        public long? BusinessTypeID { get; set; } 
        
        [DataMember]
        public KeyValueDTO BusinessType { get; set; }

        [DataMember]
        public string YearEstablished { get; set; }

        //Commercial/Business Registration Number :
        [DataMember]
        public string VendorCR { get; set; }       
      
        [DataMember]
        public DateTime? CRStartDate { get; set; } 

        [DataMember]
        public DateTime? CRExpiry { get; set; }

        [DataMember]
        public string CRStartDateString { get; set; }

        [DataMember]
        public string CRExpiryDateString { get; set; } 

        [DataMember]
        public string TINNumber { get; set; }         
        
        [DataMember]
        public long? TaxJurisdictionCountryID { get; set; }

        [DataMember]
        public KeyValueDTO TaxJurisdictionCountry { get; set; }

        [DataMember]
        public string DUNSNumber { get; set; }

        //Trade/Business license :

        [DataMember]
        public string LicenseNumber { get; set; }

        [DataMember]
        public DateTime? LicenseStartDate { get; set; }

        [DataMember]
        public string LicenseStartDateString { get; set; }

        [DataMember]
        public DateTime? LicenseExpiryDate { get; set; }

        [DataMember]
        public string LicenseExpiryDateString { get; set; }

        [DataMember]
        public string EstIDNumber { get; set; }

        [DataMember]
        public DateTime? EstFirstIssueDate { get; set; }

        [DataMember]
        public string EstFirstIssueDateString { get; set; }

        [DataMember]
        public DateTime? EstExpiryDate { get; set; }

        [DataMember]
        public string EstExpiryDateString { get; set; }


        [DataMember]
        public long? BusinessRegistration { get; set; }

        [DataMember]
        public long? TaxIdentificationNumber { get; set; }

        [DataMember]
        public long? DUNSNumberUpload { get; set; }

        [DataMember]
        public long? TradeLicense { get; set; }

        [DataMember]
        public long? EstablishmentLicense { get; set; }

    }
}
