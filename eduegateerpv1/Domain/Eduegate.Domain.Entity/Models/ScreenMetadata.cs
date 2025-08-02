using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ScreenMetadatas", Schema = "setting")]
    public partial class ScreenMetadata
    {
        public ScreenMetadata()
        {
            this.ScreenLookupMaps = new List<ScreenLookupMap>();
            ScreenFieldSettings = new HashSet<ScreenFieldSetting>();
            UserScreenFieldSettings = new HashSet<UserScreenFieldSetting>();
            ScreenMetadataCultureDatas = new HashSet<ScreenMetadataCultureData>();
        }

        [Key]
        public long ScreenID { get; set; }

        public Nullable<long> ViewID { get; set; }

        public string Name { get; set; }

        public string ListActionName { get; set; }

        public string ListButtonDisplayName { get; set; }

        public string ModelNamespace { get; set; }

        public string ModelViewModel { get; set; }

        public string MasterViewModel { get; set; }

        public string DetailViewModel { get; set; }

        public string SummaryViewModel { get; set; }

        public string EntityMapperViewModel { get; set; }

        public string EntityMapperAssembly { get; set; }

        public string DisplayName { get; set; }

        public string JsControllerName { get; set; }

        public Nullable<bool> IsCacheable { get; set; }

        public Nullable<bool> IsSavePanelRequired { get; set; }

        public Nullable<bool> IsGenericCRUDSave { get; set; }

        public Nullable<int> ScreenTypeID { get; set; }

        public string SaveCRUDMethod { get; set; }

        public string ModelAssembly { get; set; }

        public string EntityType { get; set; }

        public string PrintPreviewReportName { get; set; }

        public virtual ICollection<ScreenLookupMap> ScreenLookupMaps { get; set; }

        public virtual View View { get; set; }

        public virtual ICollection<UserScreenFieldSetting> UserScreenFieldSettings { get; set; }

        public virtual ICollection<ScreenFieldSetting> ScreenFieldSettings { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScreenMetadataCultureData> ScreenMetadataCultureDatas { get; set; }
    }
}