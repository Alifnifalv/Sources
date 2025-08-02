using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class StudentPassportDetailDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public StudentPassportDetailDTO()
        {
            National = new KeyValueDTO();
            CountryofBirth = new KeyValueDTO();
            CountryofIssue = new KeyValueDTO();
        }

        [DataMember]
        public long StudentPassportDetailsIID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public int? NationalityID { get; set; }

        [DataMember]
        public string PassportNo { get; set; }

        [DataMember]
        public int? CountryofIssueID { get; set; }

        [DataMember]
        public DateTime? PassportNoExpiry { get; set; }

        [DataMember]
        public int? CountryofBirthID { get; set; }

        [DataMember]
        public string VisaNo { get; set; }

        [DataMember]
        public DateTime? VisaExpiry { get; set; }

        [DataMember]
        public string NationalIDNo { get; set; }

        [DataMember]
        public DateTime? NationalIDNoExpiry { get; set; }

        [DataMember]
        public KeyValueDTO National { get; set; }

        [DataMember]
        public KeyValueDTO CountryofBirth { get; set; }

        [DataMember]
        public KeyValueDTO CountryofIssue { get; set; }

        [DataMember]
        public string AdhaarCardNo { get; set; }

        [DataMember]
        public string PassportExpiryStringDate { get; set; }

        [DataMember]
        public string VisaExpiryStringDate { get; set; }

        [DataMember]
        public string NationalIDExpiryStringDate { get; set; }
    }
}