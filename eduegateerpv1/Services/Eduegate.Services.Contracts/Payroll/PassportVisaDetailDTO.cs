using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Payroll
{
    [DataContract]
    public class PassportVisaDetailDTO : BaseMasterDTO
    {
        [DataMember]
        public long PassportVisaIID { get; set; }
        
        [DataMember]
        public long? ReferenceID { get; set; }
        
        [DataMember]
        public string PassportNo { get; set; }
        
        [DataMember]
        public int? CountryofIssueID { get; set; }
        
        [DataMember]
        public string PlaceOfIssue { get; set; }
        
        [DataMember]
        public DateTime? PassportNoIssueDate { get; set; }
       
        [DataMember]
        public DateTime? PassportNoExpiry { get; set; }
        
        [DataMember]
        public string VisaNo { get; set; }
        
        [DataMember]
        public DateTime? VisaExpiry { get; set; }
        
        [DataMember]
        public string NationalIDNo { get; set; }
        
        [DataMember]
        public DateTime? NationalIDNoIssueDate { get; set; }
        
        [DataMember]
        public DateTime? NationalIDNoExpiry { get; set; }
        
        [DataMember]
        public string UserType { get; set; }
        
        [DataMember]
        public string SponceredBy { get; set; }

        [DataMember]
        public string CountryofIssueName { get; set; }

        [DataMember]
        public long? SponsorID { get; set; }

        [DataMember]
        public string MOIID { get; set; }

        [DataMember]
        public string LabourCardNo { get; set; }

        [DataMember]
        public string HealthCardNo { get; set; }

    }
}