using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{

    [DataContract]
    public class CustomerMembershipDTO
    {
        [DataMember]
        public string CustomerFirstName { get; set; }
        [DataMember]
        public string CustomerLastName { get; set; }
        [DataMember]
        public string CustomerEmailId { get; set; }
        [DataMember]
        public int CategorizationPoints { get; set; }
        [DataMember]
        public long CustomerId { get; set; }
        [DataMember]
        public string ApplicableCategory { get; set; }
        [DataMember]
        public string NextApplicableCategory { get; set; }
        [DataMember]
        public int LoyaltyPoints { get; set; }
        [DataMember]
        public int PointsNeededForNextCategory { get; set; }
        [DataMember]
        public string PointsNeededText { get; set; }
    }
}
