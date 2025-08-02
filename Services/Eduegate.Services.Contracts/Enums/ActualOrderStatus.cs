using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "ActualOrderStatus")]
    public enum ActualOrderStatus
    {
        [EnumMember]
        [Display(Name = "OrderPlaced")]
        OrderPlaced = 1,
        [EnumMember]
        [Display(Name = "InProcess")]
        Picking = 3,
        [EnumMember]
        [Display(Name = "Processed")]
        Stockout = 4,
        [EnumMember]
        [Display(Name = "Processed")]
        Packing = 5,
        [EnumMember]
        [Display(Name = "ReadyForDelivery")]
        ReadyForDelivery = 6,
        [EnumMember]
        [Display(Name = "Dispatched")]
        Mission = 7,
        [EnumMember]
        [Display(Name = "Delivered")]
        Delivered = 10,
        [EnumMember]
        [Display(Name = "Failed")]
        Failed = 11,
        [EnumMember]
        [Display(Name = "Cancelled")]
        Cancelled = 13,

    }
}
