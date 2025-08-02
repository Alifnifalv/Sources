namespace Eduegate.Domain.Entity.HR.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PassportVisaDetails", Schema = "schools")]
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

        public DateTime? PassportNoIssueDate { get; set; }

        public DateTime? PassportNoExpiry { get; set; }

        [StringLength(20)]
        public string VisaNo { get; set; }

        public DateTime? VisaExpiry { get; set; }

        [StringLength(20)]
        public string NationalIDNo { get; set; }

        public DateTime? NationalIDNoIssueDate { get; set; }

        public DateTime? NationalIDNoExpiry { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        [StringLength(10)]
        public string UserType { get; set; }

        [StringLength(250)]
        public string SponceredBy { get; set; }

        public virtual Country Country { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
