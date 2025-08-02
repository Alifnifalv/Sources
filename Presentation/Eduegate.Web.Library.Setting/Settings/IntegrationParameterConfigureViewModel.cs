using Eduegate.Services.Contracts.Setting.Settings;

namespace Eduegate.Web.Library.Setting.Settings
{
    public class IntegrationParameterConfigureViewModel
    {
        public long IntegrationParameterId { get; set; }
        public string ParameterType { get; set; }
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }
        public string ParameterDataType { get; set; }

        public static IntegrationParamterDTO ToDTO(IntegrationParameterConfigureViewModel vm)
        {
            return new IntegrationParamterDTO()
            {
                IntegrationParameterId = vm.IntegrationParameterId,
                ParameterDataType = vm.ParameterDataType,
                ParameterName = vm.ParameterName,
                ParameterType = vm.ParameterType,
                ParameterValue = vm.ParameterValue
            };
        }

    }
}