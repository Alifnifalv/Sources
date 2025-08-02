using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;

namespace Eduegate.Services.Contracts.Checkout
{
    [DataContract]
    public class CartScheduleDateDTO
    {
        [DataMember]
        public string ScheduleDate { get; set; }
    }
}
