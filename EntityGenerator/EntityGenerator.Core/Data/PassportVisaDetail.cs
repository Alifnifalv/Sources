using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PassportVisaDetails", Schema = "schools")]
    [Index("ReferenceID", Name = "IDX_PassportVisaDetails_ReferenceID_")]
    [Index("ReferenceID", Name = "IDX_PassportVisaDetails_ReferenceID_PassportNo__CountryofIssueID__PlaceOfIssue__PassportNoExpiry__V")]
    public partial class PassportVisaDetail
    {
        [Key]
        public long PassportVisaIID { get; set; }
        public long? ReferenceID { get; set; }
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
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(10)]
        public string UserType { get; set; }
        [StringLength(250)]
        public string SponceredBy { get; set; }
        public long? SponsorID { get; set; }

        [ForeignKey("CountryofIssueID")]
        [InverseProperty("PassportVisaDetails")]
        public virtual Country CountryofIssue { get; set; }
        [ForeignKey("ReferenceID")]
        [InverseProperty("PassportVisaDetails")]
        public virtual Employee Reference { get; set; }
        [ForeignKey("SponsorID")]
        [InverseProperty("PassportVisaDetails")]
        public virtual Sponsor Sponsor { get; set; }
    }
}
