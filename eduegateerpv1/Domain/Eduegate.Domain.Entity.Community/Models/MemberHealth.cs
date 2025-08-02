using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Community.Models
{
    [Table("MemberHealths", Schema = "communities")]
    public partial class MemberHealth
    {
        [Key]
        public long MemberHealthIID { get; set; }

        public long? MemberID { get; set; }

        public int? BloodGroupID { get; set; }

        [StringLength(500)]
        public string HealthIssues { get; set; }

        [StringLength(2000)]
        public string DisabledDetails { get; set; }

        public virtual BloodGroup BloodGroup { get; set; }

        public virtual Member Member { get; set; }
    }
}