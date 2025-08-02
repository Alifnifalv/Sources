using System;
using System.Runtime.Serialization;


namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class CategoryMarketPlaceDTO
    {
        [DataMember]
        public Nullable<decimal> Profit { get; set; }
    }
}
