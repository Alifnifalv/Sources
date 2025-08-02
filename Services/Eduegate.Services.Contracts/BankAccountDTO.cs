using Eduegate.Domain.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class BankAccountDTO
    {
        [DataMember]
        public string BankName { get; set; } 
        
        [DataMember]
        public string BankAddress { get; set; }

        [DataMember]
        public string AccountNumber { get; set; }

        [DataMember]
        public string IBAN { get; set; }

        [DataMember]
        public string SwiftCode { get; set; }

        [DataMember]
        public string SupplierID { get; set; }  
        
        [DataMember]
        public bool? IsCreditReference { get; set; }  
        
        [DataMember]
        public int? PaymentMaxNoOfDaysAllowed { get; set; }

        [DataMember]
        public string AccountHolderName { get; set; }

        [DataMember]
        public string BankShortName { get; set; }

        //Content Files
        [DataMember]
        public string LetterConfirmationFromBank { get; set; }

        [DataMember]
        public string LatestAuditedFinancialStatements { get; set; }

        [DataMember]
        public string LiabilityInsurance { get; set; }

        [DataMember]
        public string WorkersCompensationInsurance { get; set; }
    }
}
