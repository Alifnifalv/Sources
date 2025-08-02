using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Members", Schema = "communities")]
    public partial class Member
    {
        public Member()
        {
            EducationDetails = new HashSet<EducationDetail>();
            MemberHealths = new HashSet<MemberHealth>();
            MemberPartners = new HashSet<MemberPartner>();
            MemberQuestionnaireAnswerMaps = new HashSet<MemberQuestionnaireAnswerMap>();
            SocialServices = new HashSet<SocialService>();
        }

        [Key]
        public long MemberIID { get; set; }
        public long? FamilyID { get; set; }
        public byte? RelationWithHeadOfFamilyID { get; set; }
        [StringLength(500)]
        public string MemberName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Sex { get; set; }
        [StringLength(50)]
        public string Relation { get; set; }
        [StringLength(50)]
        public string MembershipNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DOB { get; set; }
        [StringLength(50)]
        public string MobileNumber { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(10)]
        public string BloogGroup { get; set; }
        [StringLength(500)]
        public string Father { get; set; }
        [StringLength(500)]
        public string Mother { get; set; }
        public long? PartnerID { get; set; }
        [StringLength(100)]
        public string Ambition { get; set; }
        [StringLength(500)]
        public string RegligiousEducation { get; set; }
        [StringLength(500)]
        public string ReligiousEducationDetails { get; set; }
        [StringLength(500)]
        public string NonRelgiousEdication { get; set; }
        [StringLength(2000)]
        public string NonRelgiousEdicationDetails { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string MaritalStatus { get; set; }
        public int? NoOfChildrens { get; set; }
        public byte? Status { get; set; }
        public string Description { get; set; }
        public string Informer { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RegistrationDate { get; set; }
        public int? MemberGrade { get; set; }
        public string Extra2 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExtraDate { get; set; }
        public byte? GenderID { get; set; }
        public byte? CreatedBy { get; set; }
        public long? FatherMemberID { get; set; }
        public long? MotherMemberID { get; set; }

        [ForeignKey("CreatedBy")]
        [InverseProperty("Members")]
        public virtual CreatedByType CreatedByNavigation { get; set; }
        [ForeignKey("FamilyID")]
        [InverseProperty("Members")]
        public virtual Family Family { get; set; }
        [ForeignKey("GenderID")]
        [InverseProperty("Members")]
        public virtual Gender Gender { get; set; }
        [ForeignKey("RelationWithHeadOfFamilyID")]
        [InverseProperty("Members")]
        public virtual RelationWithHeadOfFamily RelationWithHeadOfFamily { get; set; }
        [InverseProperty("Member")]
        public virtual ICollection<EducationDetail> EducationDetails { get; set; }
        [InverseProperty("Member")]
        public virtual ICollection<MemberHealth> MemberHealths { get; set; }
        [InverseProperty("SpouseMember")]
        public virtual ICollection<MemberPartner> MemberPartners { get; set; }
        [InverseProperty("Member")]
        public virtual ICollection<MemberQuestionnaireAnswerMap> MemberQuestionnaireAnswerMaps { get; set; }
        [InverseProperty("Member")]
        public virtual ICollection<SocialService> SocialServices { get; set; }
    }
}
