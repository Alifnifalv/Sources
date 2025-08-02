using Eduegate.Framework.Contracts.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Mutual
{
    [DataContract]
    public class SchoolPayerBankDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public SchoolPayerBankDTO() {

            Bank = new KeyValueDTO();
        }

        [DataMember]
        public long PayerBankDetailIID { get; set; }
        [DataMember]
        public byte? SchoolID { get; set; }
        [DataMember]
        public long? BankID { get; set; }
        [DataMember]
        public bool? IsMainOperating { get; set; }
        [DataMember]
        public string PayerIBAN { get; set; }
        [DataMember]
        public string PayerBankShortName { get; set; }

        [DataMember]
        public KeyValueDTO Bank { get; set; }

    }
}


