using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.OrderHistory
{
    [DataContract]
    public class OrderEntitlementDTO
    {
        [DataMember]
        public long EntitlementID { get; set; }
        [DataMember]
        public string EntitlementName { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
    }
}
