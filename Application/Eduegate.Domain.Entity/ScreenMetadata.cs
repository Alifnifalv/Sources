namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("setting.ScreenMetadatas")]
    public partial class ScreenMetadata
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ScreenMetadata()
        {
            ScreenFieldSettings = new HashSet<ScreenFieldSetting>();
            ScreenLookupMaps = new HashSet<ScreenLookupMap>();
            ScreenMetadataCultureDatas = new HashSet<ScreenMetadataCultureData>();
            ScreenShortCuts = new HashSet<ScreenShortCut>();
            UserScreenFieldSettings = new HashSet<UserScreenFieldSetting>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ScreenID { get; set; }

        public long? ViewID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string ListActionName { get; set; }

        [StringLength(50)]
        public string ListButtonDisplayName { get; set; }

        [StringLength(500)]
        public string ModelAssembly { get; set; }

        [StringLength(1000)]
        public string ModelNamespace { get; set; }

        [StringLength(1000)]
        public string ModelViewModel { get; set; }

        [StringLength(1000)]
        public string MasterViewModel { get; set; }

        [StringLength(1000)]
        public string DetailViewModel { get; set; }

        [StringLength(1000)]
        public string SummaryViewModel { get; set; }

        [StringLength(50)]
        public string DisplayName { get; set; }

        [StringLength(200)]
        public string JsControllerName { get; set; }

        public bool? IsCacheable { get; set; }

        public bool? IsSavePanelRequired { get; set; }

        public bool? IsGenericCRUDSave { get; set; }

        [StringLength(500)]
        public string EntityMapperAssembly { get; set; }

        [StringLength(500)]
        public string EntityMapperViewModel { get; set; }

        [StringLength(50)]
        public string SaveCRUDMethod { get; set; }

        public int? ScreenTypeID { get; set; }

        [StringLength(50)]
        public string EntityType { get; set; }

        [StringLength(500)]
        public string PrintPreviewReportName { get; set; }

        public int? IsCustomizationEnabled { get; set; }

        public int? IsAIEnabled { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScreenFieldSetting> ScreenFieldSettings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScreenLookupMap> ScreenLookupMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScreenMetadataCultureData> ScreenMetadataCultureDatas { get; set; }

        public virtual View View { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScreenShortCut> ScreenShortCuts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserScreenFieldSetting> UserScreenFieldSettings { get; set; }
    }
}
