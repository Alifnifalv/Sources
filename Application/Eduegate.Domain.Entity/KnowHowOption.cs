namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cms.KnowHowOption")]
    public partial class KnowHowOption
    {
        [Key]
        public long KnowHowOptionIID { get; set; }

        [Required]
        [StringLength(100)]
        public string KnowHowOptionText { get; set; }

        public bool IsEditable { get; set; }
    }
}
