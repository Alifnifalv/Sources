using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TeachingAids", Schema = "schools")]
    public partial class TeachingAid
    {
        public TeachingAid()
        {
            Agenda = new HashSet<Agenda>();
            LessonPlans = new HashSet<LessonPlan>();
        }

        [Key]
        public byte TeachingAidID { get; set; }
        [StringLength(50)]
        public string TeachingAidName { get; set; }

        [InverseProperty("TeachingAid")]
        public virtual ICollection<Agenda> Agenda { get; set; }
        [InverseProperty("TeachingAid")]
        public virtual ICollection<LessonPlan> LessonPlans { get; set; }
    }
}
