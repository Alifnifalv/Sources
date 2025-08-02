using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Salon
{
    [DataContract]
    public class ServiceDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ServiceDTO()
        {
            PriceSettings = new List<ServicePriceDTO>();
            Staffs = new List<KeyValueDTO>();
        }

        [DataMember]
        public Nullable<long> LoginID { get; set; }
        [DataMember]
        public long ServiceIID { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public KeyValueDTO TreatmentType { get; set; }
        [DataMember]
        public KeyValueDTO AvailableFor { get; set; }
        [DataMember]
        public KeyValueDTO PricingType { get; set; }
        [DataMember]
        public KeyValueDTO ExtraTimeType { get; set; }
        [DataMember]
        public KeyValueDTO Duration { get; set; }
        [DataMember]
        public List<ServicePriceDTO> PriceSettings { get; set; }
        [DataMember]
        public List<KeyValueDTO> Staffs { get; set; }
    }
}
