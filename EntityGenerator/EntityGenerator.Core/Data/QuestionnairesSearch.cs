using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class QuestionnairesSearch
    {
        public long QuestionnaireIID { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        public int? QuestionnaireAnswerTypeID { get; set; }
        [StringLength(2000)]
        public string MoreInfo { get; set; }
        [StringLength(500)]
        public string TypeName { get; set; }
    }
}
