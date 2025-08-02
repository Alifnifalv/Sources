using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("QuestionAnswerMaps", Schema = "exam")]
    public partial class QuestionAnswerMap
    {
        [Key]
        public long QuestionAnswerMapIID { get; set; }
        public long? QuestionID { get; set; }
        public long? QuestionOptionMapID { get; set; }
    }
}
