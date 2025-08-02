using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Settings
{
    [DataContract]
    public class ScreenMetadataDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ScreenID { get; set; }
        [DataMember]
        public long? ViewID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string ListActionName { get; set; }
        [DataMember]
        public string ListButtonDisplayName { get; set; }
        [DataMember]
        public string ModelAssembly { get; set; }
        [DataMember]
        public string ModelNamespace { get; set; }
        [DataMember]
        public string ModelViewModel { get; set; }
        [DataMember]
        public string MasterViewModel { get; set; }
        [DataMember]
        public string DetailViewModel { get; set; }
        [DataMember]
        public string SummaryViewModel { get; set; }
        [DataMember]
        public string DisplayName { get; set; }
        [DataMember]
        public string JsControllerName { get; set; }
        [DataMember]
        public bool? IsCacheable { get; set; }
        [DataMember]
        public bool? IsSavePanelRequired { get; set; }
        [DataMember]
        public bool? IsGenericCRUDSave { get; set; }
        [DataMember]
        public string EntityMapperAssembly { get; set; }
        [DataMember]
        public string EntityMapperViewModel { get; set; }
        [DataMember]
        public string SaveCRUDMethod { get; set; }
        [DataMember]
        public int? ScreenTypeID { get; set; }
    }
}


