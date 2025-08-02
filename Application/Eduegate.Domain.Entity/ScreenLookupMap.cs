namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("setting.ScreenLookupMaps")]
    public partial class ScreenLookupMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ScreenLookupMap()
        {
            LookupColumnConditionMaps = new HashSet<LookupColumnConditionMap>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ScreenLookupMapID { get; set; }

        public long? ScreenID { get; set; }

        public bool? IsOnInit { get; set; }

        [StringLength(50)]
        public string LookUpName { get; set; }

        [StringLength(1000)]
        public string Url { get; set; }

        [StringLength(50)]
        public string CallBack { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LookupColumnConditionMap> LookupColumnConditionMaps { get; set; }

        public virtual ScreenMetadata ScreenMetadata { get; set; }
    }
}
