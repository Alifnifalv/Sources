using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.ExternalServices
{
    [DataContract]
    public class ServiceProviderShipmentDetailDTO
    {
        [DataMember]
        public string ReferenceNo { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string Mobile { get; set; }
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string NoOfPcs { get; set; }
        [DataMember]
        public string Weight { get; set; }
        [DataMember]
        public string CODAmount { get; set; }
        [DataMember]
        public string ItemDescription { get; set; }
    }
}
