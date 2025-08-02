namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("collaboration.QuestionnaireTypes")]
    public partial class QuestionnaireType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionnaireTypeID { get; set; }

        [StringLength(500)]
        public string TypeName { get; set; }
    }
}
