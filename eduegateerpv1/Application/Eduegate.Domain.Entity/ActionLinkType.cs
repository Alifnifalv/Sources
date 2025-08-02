namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.ActionLinkTypes")]
    public partial class ActionLinkType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActionLinkTypeID { get; set; }

        [StringLength(100)]
        public string ActionLinkTypeName { get; set; }
    }
}
