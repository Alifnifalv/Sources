using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Notifications
{
    [DataContract]
    public class NotificationAlertsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long NotificationAlertIID { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public long? FromLoginID { get; set; }
        [DataMember]
        public string FromLogin { get; set; }
        [DataMember]
        public long? ToLoginID { get; set; }
        [DataMember]
        public string ToLogin { get; set; }
        [DataMember]
        public DateTime? NotificationDate { get; set; }
        [DataMember]
        public int? AlertStatusID { get; set; }
        [DataMember]
        public string AlertStatus { get; set; }
        [DataMember]
        public int? AlertTypeID { get; set; }
        [DataMember]
        public string AlertType { get; set; }
        [DataMember]
        public long? ReferenceID { get; set; }
        [DataMember]
        public long? ReferenceScreenID { get; set; }

        [DataMember]
        public string NotificationDateString { get; set; }

        [DataMember]
        public string FromEmployeeName { get; set; }
    }
}
