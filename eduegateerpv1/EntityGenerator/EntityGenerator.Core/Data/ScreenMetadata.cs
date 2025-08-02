using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ScreenMetadatas", Schema = "setting")]
    public partial class ScreenMetadata
    {
        public ScreenMetadata()
        {
            ScreenFieldSettings = new HashSet<ScreenFieldSetting>();
            ScreenLookupMaps = new HashSet<ScreenLookupMap>();
            ScreenMetadataCultureDatas = new HashSet<ScreenMetadataCultureData>();
            ScreenShortCuts = new HashSet<ScreenShortCut>();
            UserScreenFieldSettings = new HashSet<UserScreenFieldSetting>();
        }

        [Key]
        public long ScreenID { get; set; }
        public long? ViewID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string ListActionName { get; set; }
        [StringLength(50)]
        public string ListButtonDisplayName { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string ModelAssembly { get; set; }
        [StringLength(1000)]
        public string ModelNamespace { get; set; }
        [StringLength(1000)]
        public string ModelViewModel { get; set; }
        [StringLength(1000)]
        [Unicode(false)]
        public string MasterViewModel { get; set; }
        [StringLength(1000)]
        public string DetailViewModel { get; set; }
        [StringLength(1000)]
        public string SummaryViewModel { get; set; }
        [StringLength(50)]
        public string DisplayName { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string JsControllerName { get; set; }
        public bool? IsCacheable { get; set; }
        public bool? IsSavePanelRequired { get; set; }
        public bool? IsGenericCRUDSave { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string EntityMapperAssembly { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string EntityMapperViewModel { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string SaveCRUDMethod { get; set; }
        public int? ScreenTypeID { get; set; }
        [StringLength(50)]
        public string EntityType { get; set; }
        [StringLength(500)]
        public string PrintPreviewReportName { get; set; }
        public int? IsCustomizationEnabled { get; set; }
        public int? IsAIEnabled { get; set; }

        [ForeignKey("ViewID")]
        [InverseProperty("ScreenMetadatas")]
        public virtual View View { get; set; }
        [InverseProperty("Screen")]
        public virtual ICollection<ScreenFieldSetting> ScreenFieldSettings { get; set; }
        [InverseProperty("Screen")]
        public virtual ICollection<ScreenLookupMap> ScreenLookupMaps { get; set; }
        [InverseProperty("Screen")]
        public virtual ICollection<ScreenMetadataCultureData> ScreenMetadataCultureDatas { get; set; }
        [InverseProperty("Screen")]
        public virtual ICollection<ScreenShortCut> ScreenShortCuts { get; set; }
        [InverseProperty("Screen")]
        public virtual ICollection<UserScreenFieldSetting> UserScreenFieldSettings { get; set; }
    }
}
