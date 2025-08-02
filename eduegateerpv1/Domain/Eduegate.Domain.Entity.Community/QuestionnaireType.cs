namespace Eduegate.Domain.Entity.Community
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("collaboration.QuestionnaireTypes")]
    public partial class QuestionnaireType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionnaireTypeID { get; set; }

        [StringLength(500)]
        public string TypeName { get; set; }
    }
}
