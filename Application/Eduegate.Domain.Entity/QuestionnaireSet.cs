namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("collaboration.QuestionnaireSets")]
    public partial class QuestionnaireSet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionnaireSetID { get; set; }

        [StringLength(100)]
        public string QuestionnaireSetName { get; set; }
    }
}
