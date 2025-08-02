using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Settings
{
    [DataContract]
    public class ViewDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ViewID { get; set; }
        [DataMember]
        public byte? ViewTypeID { get; set; }
        [DataMember]
        public string ViewName { get; set; }
        [DataMember]
        public string ViewFullPath { get; set; }
        [DataMember]
        public bool? IsMultiLine { get; set; }
        [DataMember]
        public bool? IsRowCategory { get; set; }
        [DataMember]
        public string PhysicalSchemaName { get; set; }
        [DataMember]
        public bool? HasChild { get; set; }
        [DataMember]
        public bool? IsRowClickForMultiSelect { get; set; }
        [DataMember]
        public long? ChildViewID { get; set; }
        [DataMember]
        public string ChildFilterField { get; set; }
        [DataMember]
        public string ControllerName { get; set; }
        [DataMember]
        public bool? IsMasterDetail { get; set; }
        [DataMember]
        public bool? IsEditable { get; set; }
        [DataMember]
        public bool? IsGenericCRUDSave { get; set; }
        [DataMember]
        public bool? IsReloadSummarySmartViewAlways { get; set; }
        [DataMember]
        public string JsControllerName { get; set; }
        [DataMember]
        public string ViewDescription { get; set; }
        [DataMember]
        public string ViewTitle { get; set; }
    }
}


