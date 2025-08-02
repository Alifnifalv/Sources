using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class FeeDiscountDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public int  FeeDiscountID { get; set; }
        [DataMember]
        public string  DiscountCode { get; set; }
        [DataMember]
        public decimal?  DiscountPercentage { get; set; }
        [DataMember]
        public decimal?  Amount { get; set; }
        [DataMember]
        public string  Description { get; set; }
     
    }
}



