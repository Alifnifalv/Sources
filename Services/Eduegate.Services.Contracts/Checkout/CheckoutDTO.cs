using Eduegate.Framework;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Checkout
{
    [DataContract]
    public class CheckoutDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public ContactDTO contactDTO { get; set; }

        [DataMember]

        public CallContext callContext { get; set; }
    }
}