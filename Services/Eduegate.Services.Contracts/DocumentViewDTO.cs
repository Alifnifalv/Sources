using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class DocumentViewDTO
    {
        [DataMember]
        public List<DocumentFileDTO> Documents { get; set; }
        //[DataMember]
        //public string ReferenceParameterName { get { return "referenceID"; } }
        //[DataMember]
        //public string EntityParameterName { get { return "entityType"; } }


        //For Supplier / vendor attachment
        [DataMember]
        public long SupplierContentID { get; set; }
        [DataMember]
        public long? SupplierID { get; set; }
        [DataMember]
        public long? LetterConfirmationFromBank { get; set; }
        [DataMember]
        public long? LatestAuditedFinancialStatements { get; set; }
        [DataMember]
        public long? LiabilityInsurance { get; set; }
        [DataMember]
        public long? WorkersCompensationInsurance { get; set; }
        [DataMember]
        public long? PrdctCategories { get; set; }
        [DataMember]
        public long? ISO9001 { get; set; }
        [DataMember]
        public long? OtherRelevantISOCertifications { get; set; }
        [DataMember]
        public long? ISO14001 { get; set; }
        [DataMember]
        public long? OtherEnviStandards { get; set; }
        [DataMember]
        public long? SA8000 { get; set; }
        [DataMember]
        public long? OtherSocialRespoStandards { get; set; }
        [DataMember]
        public long? OHSAS18001 { get; set; }
        [DataMember]
        public long? OtherRelevantHealthSafetyStandards { get; set; }
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
