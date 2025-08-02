using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("QuestionnaireSets", Schema = "collaboration")]
    public partial class QuestionnaireSet
    {
        [Key]
        public int QuestionnaireSetID { get; set; }
        [StringLength(100)]
        public string QuestionnaireSetName { get; set; }
    }
}
