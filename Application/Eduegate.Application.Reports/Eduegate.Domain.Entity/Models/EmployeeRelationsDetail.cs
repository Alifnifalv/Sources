using Eduegate.Domain.Entity.Models.HR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    [Table("payroll.EmployeeRelationsDetails")]
    public partial class EmployeeRelationsDetail
    {
        [Key]
        public long EmployeeRelationsDetailIID { get; set; }

        public byte? EmployeeRelationTypeID { get; set; }

        public long? EmployeeID { get; set; }

        [StringLength(200)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [StringLength(200)]
        public string LastName { get; set; }

        [StringLength(20)]
        public string PassportNo { get; set; }

        public int? CountryofIssueID { get; set; }

        [StringLength(100)]
        public string PlaceOfIssue { get; set; }

        public DateTime? PassportNoIssueDate { get; set; }

        public DateTime? PassportNoExpiry { get; set; }

        [StringLength(20)]
        public string VisaNo { get; set; }

        public DateTime? VisaExpiry { get; set; }

        [StringLength(20)]
        public string NationalIDNo { get; set; }

        public DateTime? NationalIDNoIssueDate { get; set; }

        public DateTime? NationalIDNoExpiry { get; set; }

        [StringLength(50)]
        public string ContactNo { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        [StringLength(250)]
        public string SponceredBy { get; set; }

        public long? SponsorID { get; set; }

        //public virtual Country Country { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual EmployeeRelationType EmployeeRelationType { get; set; }

        //public virtual Sponsor Sponsor { get; set; }
    }
}
