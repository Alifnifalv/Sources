namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("setting.ScreenShortCuts")]
    public partial class ScreenShortCut
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ScreenShortCutID { get; set; }

        public long? ScreenID { get; set; }

        [StringLength(25)]
        public string KeyCode { get; set; }

        [StringLength(50)]
        public string ShortCutKey { get; set; }

        [StringLength(100)]
        public string Action { get; set; }

        public virtual ScreenMetadata ScreenMetadata { get; set; }
    }
}
