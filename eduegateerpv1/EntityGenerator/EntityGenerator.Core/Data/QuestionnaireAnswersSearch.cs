using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class QuestionnaireAnswersSearch
    {
        public long QuestionnaireAnswerIID { get; set; }
        public int? QuestionnaireAnswerTypeID { get; set; }
        public long? QuestionnaireID { get; set; }
        public string Answer { get; set; }
        public string MoreInfo { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        [StringLength(500)]
        public string TypeName { get; set; }
    }
}
