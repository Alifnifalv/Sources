using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class LeadReportView
    {
        public long LeadIID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string DateofEnquiry { get; set; }
        [StringLength(100)]
        public string LeadCode { get; set; }
        [StringLength(50)]
        public string StudentName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string DateOfBirth { get; set; }
        [StringLength(1)]
        [Unicode(false)]
        public string Gender { get; set; }
        [StringLength(123)]
        public string AcademicYear { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [StringLength(20)]
        public string MobileNumber { get; set; }
        [StringLength(50)]
        public string ParentName { get; set; }
        [StringLength(50)]
        public string EmailAddress { get; set; }
        [StringLength(50)]
        public string SourceOfEnquiry { get; set; }
        [StringLength(502)]
        public string AttendedBy { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        public byte? GenderID { get; set; }
        [StringLength(200)]
        public string LeadName { get; set; }
        [StringLength(200)]
        public string OrgnanizationName { get; set; }
        public int? LeadSourceID { get; set; }
        public long? ContactID { get; set; }
        public byte? LeadTypeID { get; set; }
        public byte? MarketSegmentID { get; set; }
        public int? IndustryTypeID { get; set; }
        public byte? RequestTypeID { get; set; }
        public int? CompanyID { get; set; }
        public bool? IsOrganization { get; set; }
        public long? UpdatedBy { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string UpdatedDate { get; set; }
        public int? ClassID { get; set; }
        public int? AcademicYearID { get; set; }
        public byte? LeadStatusID { get; set; }
        public byte? CurriculamID { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string Remarks { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string FollowUpDate { get; set; }
        public string EmailContent { get; set; }
        public long? CommunicationIID { get; set; }
        public byte? CommunicationTypeID { get; set; }
        [StringLength(1000)]
        public string Notes { get; set; }
        [StringLength(50)]
        public string CommunicationType { get; set; }
    }
}
