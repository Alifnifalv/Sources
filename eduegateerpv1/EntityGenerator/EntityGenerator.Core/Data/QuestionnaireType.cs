using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("QuestionnaireTypes", Schema = "collaboration")]
    public partial class QuestionnaireType
    {
        [Key]
        public int QuestionnaireTypeID { get; set; }
        [StringLength(500)]
        public string TypeName { get; set; }
    }
}
