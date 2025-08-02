using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.MobileAppWrapper
{
    [DataContract]
    public class NewsletterSubscriptionDTO
    {
        [DataMember]
        public string emailID { get; set; }
        [DataMember]
        public short cultureID { get; set; }
    }
}
