namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("setting.ViewActions")]
    public partial class ViewAction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ViewActionID { get; set; }

        public long? ViewID { get; set; }

        [StringLength(100)]
        public string ActionCaption { get; set; }

        [StringLength(1000)]
        public string ActionAttribute { get; set; }

        [StringLength(1000)]
        public string ActionAttribute2 { get; set; }

        public virtual View View { get; set; }
    }
}
