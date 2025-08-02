using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeRelationsDetails", Schema = "payroll")]
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
        [Column(TypeName = "datetime")]
        public DateTime? PassportNoIssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PassportNoExpiry { get; set; }
        [StringLength(20)]
        public string VisaNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? VisaExpiry { get; set; }
        [StringLength(20)]
        public string NationalIDNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? NationalIDNoIssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? NationalIDNoExpiry { get; set; }
        [StringLength(50)]
        public string ContactNo { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(250)]
        public string SponceredBy { get; set; }
        public long? SponsorID { get; set; }

        [ForeignKey("CountryofIssueID")]
        [InverseProperty("EmployeeRelationsDetails")]
        public virtual Country CountryofIssue { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("EmployeeRelationsDetails")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("EmployeeRelationTypeID")]
        [InverseProperty("EmployeeRelationsDetails")]
        public virtual EmployeeRelationType EmployeeRelationType { get; set; }
        [ForeignKey("SponsorID")]
        [InverseProperty("EmployeeRelationsDetails")]
        public virtual Sponsor Sponsor { get; set; }
    }
}
