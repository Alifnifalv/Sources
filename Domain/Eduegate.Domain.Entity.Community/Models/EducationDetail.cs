using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Community.Models
{
    [Table("EducationDetails", Schema = "communities")]
    public partial class EducationDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long EducationDetailIID { get; set; }

        public long? MemberID { get; set; }

        public byte? EducationTypeID { get; set; }

        [StringLength(500)]
        public string Subject { get; set; }

        [StringLength(2000)]
        public string OtherQualitications { get; set; }

        [StringLength(2000)]
        public string ReligiousEducation { get; set; }

        [StringLength(2000)]
        public string Skills { get; set; }

        public virtual EducationType EducationType { get; set; }

        public virtual Member Member { get; set; }
    }
}