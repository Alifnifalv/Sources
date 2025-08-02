using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums.Schedulers
{
    [DataContract]
    public enum SchedulerEntityTypes
    {
        [EnumMember]
        Job = 1,
        [EnumMember]
        PurchaseOrder = 2,
        [EnumMember]
        PurchaseInvoice = 3,
        [EnumMember]
        SalesInvoice = 4,
        [EnumMember]
        SalesOrder = 5,
        [EnumMember]
        EmailNotification = 6,
    }
}
