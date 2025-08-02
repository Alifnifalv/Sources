using Eduegate.Services.Contracts.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Framework
{
    [DataContract]
    public class ScreenMetadataDTO
    {
        public ScreenMetadataDTO()
        {
            ScreenFieldSettings = new List<ScreenFieldSettingDTO>();
            Urls = new List<ScreenLookupDTO>();
            Claims = new List<ClaimDTO>();
        }

        [DataMember]
        public string ListActionName { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string ViewFullPath { get; set; }
        [DataMember]
        public string View { get; set; }
        [DataMember]
        public string DisplayName { get; set; }
        [DataMember]
        public string ListButtonDisplayName { get; set; }
        [DataMember]
        public long? IID { get; set; }
        [DataMember]
        public string IID2 { get; set; }
        [DataMember]
        public string JsControllerName { get; set; }
        [DataMember]
        public List<ScreenLookupDTO> Urls { get; set; }
        [DataMember]
        public bool? IsCacheable { get; set; }
        [DataMember]
        public string CacheType { get; set; }
        [DataMember]
        public bool? IsSavePanelRequired { get; set; }
        [DataMember]
        public string SaveCRUDMethod { get; set; }        
        [DataMember]
        public bool? IsGenericCRUDSave { get; set; }
        [DataMember]
        public string ModelAssembly { get; set; }
        [DataMember]
        public string ModelNamespace { get; set; }
        [DataMember]
        public string MasterViewModel { get; set; }
        [DataMember]
        public string DetailViewModel { get; set; }
        [DataMember]
        public string SummaryViewModel { get; set; }
        [DataMember]
        public string ModelViewModel { get; set; }
        [DataMember]
        public string EntityMapperViewModel { get; set; }
        [DataMember]
        public string EntityMapperAssembly { get; set; }
        [DataMember]
        public int ScreenTypeID { get; set; }
        [DataMember]
        public List<ScreenFieldSettingDTO> ScreenFieldSettings { get; set; }
        [DataMember]
        public List<ClaimDTO> Claims { get; set; }
        [DataMember]
        public string EntityType { get; set; }
        [DataMember]
        public string PrintPreviewReportName { get; set; }

    }
}
