namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Leads", Schema = "crm")]
    public partial class Lead
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lead()
        {
            //Communications = new HashSet<Communication>();
            //Opportunities = new HashSet<Opportunity>();
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

        public DateTime? CreatedDate { get; set; }

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

        public DateTime? DateOfBirth { get; set; }

        [StringLength(100)]
        public string ClassName { get; set; }

        [StringLength(25)]
        public string AcademicYear { get; set; }

        [StringLength(100)]
        public string ReferalCode { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; }

        public byte? SchoolID { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Communication> Communications { get; set; }

        //public virtual CRMCompany CRMCompany { get; set; }

        //public virtual IndustryType IndustryType { get; set; }

        public virtual AcademicYear AcademicYear1 { get; set; }

        public virtual Schools School { get; set; }

        public virtual Class Class { get; set; }

        //public virtual Contact Contact { get; set; }

        public virtual Gender Gender { get; set; }

        //public virtual Source Source { get; set; }

        //public virtual LeadStatus LeadStatus { get; set; }

        //public virtual LeadType LeadType { get; set; }

        //public virtual MarketSegment MarketSegment { get; set; }

        //public virtual RequestType RequestType { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Opportunity> Opportunities { get; set; }
    }
}
