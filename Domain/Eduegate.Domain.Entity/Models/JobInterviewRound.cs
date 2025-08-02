using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("JobInterviewRounds", Schema = "hr")]
    public partial class JobInterviewRound
    {
        public JobInterviewRound()
        {
            JobInterviewMaps = new HashSet<JobInterviewMap>();
            JobInterviewRoundMaps = new HashSet<JobInterviewRoundMap>();
        }

        [Key]
        public int RoundID { get; set; }
        [StringLength(250)]
        public string Description { get; set; }

        public int? MaximumRating { get; set; }

        [InverseProperty("CompletedRound")]
        public virtual ICollection<JobInterviewMap> JobInterviewMaps { get; set; }
        [InverseProperty("Round")]
        public virtual ICollection<JobInterviewRoundMap> JobInterviewRoundMaps { get; set; }
    }
}
