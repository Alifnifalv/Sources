using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ScreenMetadata
    {
        public ScreenMetadata()
        {
            this.ScreenLookupMaps = new List<ScreenLookupMap>();
        }

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
        public string DisplayName { get; set; }
        public string JsControllerName { get; set; }
        public Nullable<bool> IsCacheable { get; set; }
        public Nullable<bool> IsSavePanelRequired { get; set; }
        public virtual ICollection<ScreenLookupMap> ScreenLookupMaps { get; set; }
        public virtual View View { get; set; }
    }
}
