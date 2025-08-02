using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LessonLearningObjectives", Schema = "schools")]
    public partial class LessonLearningObjective
    {
        public LessonLearningObjective()
        {
            LessonPlanLearningObjectiveMaps = new HashSet<LessonPlanLearningObjectiveMap>();
        }

        [Key]
        public byte LessonLearningObjectiveID { get; set; }
        [StringLength(50)]
        public string LessonLearningObjectiveName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("LessonLearningObjective")]
        public virtual ICollection<LessonPlanLearningObjectiveMap> LessonPlanLearningObjectiveMaps { get; set; }
    }
}
