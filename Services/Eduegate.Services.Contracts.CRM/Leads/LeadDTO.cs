using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Leads
{
    [DataContract]
    public class LeadDTO : BaseMasterDTO
    {
        public LeadDTO()
        {
            LeadCommunication = new List<LeadCommunicationDTO>();
            LeadContact = new LeadContactDTO();
            LeadEmailCommunication = new LeadCommunicationDTO();
            Nationality = new KeyValueDTO();
        }

        [DataMember]
        public long LeadIID { get; set; }

        [DataMember]
        [StringLength(200)]
        public string LeadName { get; set; }

        [DataMember]
        [StringLength(100)]
        public string LeadCode { get; set; }

        [DataMember]
        public byte? GenderID { get; set; }

        [DataMember]
        [StringLength(200)]
        public string OrgnanizationName { get; set; }

        [DataMember]
        public int? LeadSourceID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string EmailAddress { get; set; }

        [DataMember]
        public long? ContactID { get; set; }

        [DataMember]
        public byte? LeadTypeID { get; set; }

        [DataMember]
        public byte? MarketSegmentID { get; set; }

        [DataMember]
        public int? IndustryTypeID { get; set; }

        [DataMember]
        public byte? RequestTypeID { get; set; }

        [DataMember]
        public int? CompanyID { get; set; }

        [DataMember]
        public bool? IsOrganization { get; set; }

        [DataMember]
        [StringLength(50)]
        public string StudentName { get; set; }

        [DataMember]
        [StringLength(50)]
        public string ParentName { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public int? NationalityID { get; set; }

        [DataMember]
        public string AcademicYear { get; set; }

        [DataMember]
        public string AcademicYearCode { get; set; }

        [DataMember]
        public byte? LeadStatusID { get; set; }

        [DataMember]
        [StringLength(20)]
        public string MobileNumber { get; set; }

        [DataMember]
        public DateTime? DateOfBirth { get; set; }

        [DataMember]
        public List<LeadCommunicationDTO> LeadCommunication { get; set; }

        [DataMember]
        public LeadContactDTO LeadContact { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public string ReferalCode { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public LeadCommunicationDTO LeadEmailCommunication { get; set; }

        [DataMember]
        public byte? CurriculamID { get; set; }
        [DataMember]
        public string Curriculam { get; set; }

        [DataMember]
        public string AgeCriteriaWarningMsg { get; set; }

        [DataMember]
        public KeyValueDTO Nationality { get; set; }

    }
}


