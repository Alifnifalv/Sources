namespace Eduegate.Domain.Entity.Models.Settings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("setting.TextTransformTypes")]
    public partial class TextTransformType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TextTransformType()
        {
            ScreenFieldSettings = new HashSet<ScreenFieldSetting>();
        }

        public byte TextTransformTypeId { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScreenFieldSetting> ScreenFieldSettings { get; set; }
    }
}
