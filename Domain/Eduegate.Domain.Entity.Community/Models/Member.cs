using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Community.Models
{
    [Table("Members", Schema = "communities")]
    public partial class Member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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
        public string Sex { get; set; }

        [StringLength(50)]
        public string Relation { get; set; }

        [StringLength(50)]
        public string MembershipNumber { get; set; }

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
        public string MaritalStatus { get; set; }

        public int? NoOfChildrens { get; set; }

        public byte? Status { get; set; }

        public string Description { get; set; }

        public string Informer { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public int? MemberGrade { get; set; }

        public string Extra2 { get; set; }

        public DateTime? ExtraDate { get; set; }

        public byte? GenderID { get; set; }

        public byte? CreatedBy { get; set; }

        public long? FatherMemberID { get; set; }

        public long? MotherMemberID { get; set; }

        public virtual CreatedByType CreatedByType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EducationDetail> EducationDetails { get; set; }

        public virtual Family Family { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemberHealth> MemberHealths { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemberPartner> MemberPartners { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemberQuestionnaireAnswerMap> MemberQuestionnaireAnswerMaps { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual RelationWithHeadOfFamily RelationWithHeadOfFamily { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SocialService> SocialServices { get; set; }
    }
}