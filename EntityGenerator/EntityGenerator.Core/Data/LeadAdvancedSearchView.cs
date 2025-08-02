using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class LeadAdvancedSearchView
    {
        public long LeadIID { get; set; }
        [StringLength(200)]
        public string LeadName { get; set; }
        [StringLength(100)]
        public string LeadCode { get; set; }
        public byte? GenderID { get; set; }
        [StringLength(200)]
        public string OrgnanizationName { get; set; }
        public int? LeadSourceID { get; set; }
        [StringLength(50)]
        public string EmailAddress { get; set; }
        public long? ContactID { get; set; }
        public byte? LeadTypeID { get; set; }
        public byte? MarketSegmentID { get; set; }
        public int? IndustryTypeID { get; set; }
        public byte? RequestTypeID { get; set; }
        public int? CompanyID { get; set; }
        public bool? IsOrganization { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(50)]
        public string StudentName { get; set; }
        [StringLength(50)]
        public string ParentName { get; set; }
        public int? ClassID { get; set; }
        public int? AcademicYearID { get; set; }
        public byte? LeadStatusID { get; set; }
        [StringLength(20)]
        public string MobileNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        [StringLength(50)]
        public string LeadTypeName { get; set; }
        [StringLength(50)]
        public string LeadStatusName { get; set; }
        [StringLength(50)]
        public string SourceName { get; set; }
        [StringLength(50)]
        public string IndustryTypeName { get; set; }
        [StringLength(50)]
        public string MarketSegmentName { get; set; }
        [StringLength(50)]
        public string CompanyName { get; set; }
        [StringLength(50)]
        public string RequestTypeName { get; set; }
        [StringLength(50)]
        public string GenderName { get; set; }
        [StringLength(20)]
        public string AcademicYearCode { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
    }
}
