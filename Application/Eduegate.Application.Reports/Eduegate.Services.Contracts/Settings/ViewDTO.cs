using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Settings
{
    [DataContract]
    public class ViewDTO
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
    }
}
