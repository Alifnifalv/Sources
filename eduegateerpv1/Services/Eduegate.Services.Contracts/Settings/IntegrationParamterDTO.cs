using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Settings
{
    [DataContract]
    public class IntegrationParamterDTO
    {
        [DataMember]
        public long IntegrationParameterId { get; set; }
        [DataMember]
        public string ParameterType { get; set; }
        [DataMember]
        public string ParameterName { get; set; }
        [DataMember]
        public string ParameterValue { get; set; }
        [DataMember]
        public string ParameterDataType { get; set; }
    }
}
