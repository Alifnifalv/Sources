namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cs.TicketTags")]
    public partial class TicketTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TicketTagsID { get; set; }

        [StringLength(50)]
        public string TagName { get; set; }
    }
}
