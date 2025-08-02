using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Leads", Schema = "crm")]
    [Index("ClassID", Name = "IDX_Leads_ClassID_LeadName__LeadCode__GenderID__OrgnanizationName__LeadSourceID__EmailAddress__Cont")]
    [Index("LeadCode", Name = "IDX_Leads_LeadCode_")]
    [Index("LeadStatusID", Name = "idx_LeadsLeadStatusID")]
    public partial class Lead
    {
        public Lead()
        {
            Communications = new HashSet<Communication>();
            LeadEmailMaps = new HashSet<LeadEmailMap>();
            Opportunities = new HashSet<Opportunity>();
        }

        [Key]
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
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        public byte? LeadStatusID { get; set; }
        [StringLength(20)]
        public string MobileNumber { get; set; }
        [StringLength(100)]
        public string ClassName { get; set; }
        [StringLength(25)]
        public string AcademicYear { get; set; }
        [StringLength(100)]
        public string ReferalCode { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string Remarks { get; set; }
        public byte? SchoolID { get; set; }
        public byte? CurriculamID { get; set; }
        public int? NationalityID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("Leads")]
        public virtual AcademicYear AcademicYearNavigation { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("Leads")]
        public virtual Class Class { get; set; }
        [ForeignKey("CompanyID")]
        [InverseProperty("Leads")]
        public virtual CRMCompany Company { get; set; }
        [ForeignKey("ContactID")]
        [InverseProperty("Leads")]
        public virtual Contact Contact { get; set; }
        [ForeignKey("CurriculamID")]
        [InverseProperty("Leads")]
        public virtual Syllabu Curriculam { get; set; }
        [ForeignKey("GenderID")]
        [InverseProperty("Leads")]
        public virtual Gender Gender { get; set; }
        [ForeignKey("IndustryTypeID")]
        [InverseProperty("Leads")]
        public virtual IndustryType IndustryType { get; set; }
        [ForeignKey("LeadSourceID")]
        [InverseProperty("Leads")]
        public virtual Source LeadSource { get; set; }
        [ForeignKey("LeadStatusID")]
        [InverseProperty("Leads")]
        public virtual LeadStatus LeadStatus { get; set; }
        [ForeignKey("LeadTypeID")]
        [InverseProperty("Leads")]
        public virtual LeadType LeadType { get; set; }
        [ForeignKey("MarketSegmentID")]
        [InverseProperty("Leads")]
        public virtual MarketSegment MarketSegment { get; set; }
        [ForeignKey("NationalityID")]
        [InverseProperty("Leads")]
        public virtual Nationality Nationality { get; set; }
        [ForeignKey("RequestTypeID")]
        [InverseProperty("Leads")]
        public virtual RequestType RequestType { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("Leads")]
        public virtual School School { get; set; }
        [InverseProperty("Lead")]
        public virtual ICollection<Communication> Communications { get; set; }
        [InverseProperty("Lead")]
        public virtual ICollection<LeadEmailMap> LeadEmailMaps { get; set; }
        [InverseProperty("Lead")]
        public virtual ICollection<Opportunity> Opportunities { get; set; }
    }
}
