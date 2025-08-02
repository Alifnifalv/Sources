using Eduegate.Services.Contracts.Setting.Settings;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Web.Library.Setting.Settings
{
    public class IntegrationParameterGroupViewModel : BaseMasterViewModel
    {
        public IntegrationParameterGroupViewModel()
        {
            Settings = new List<IntegrationParameterConfigureViewModel>();
        }

        public string GroupName { get; set; }

        public List<IntegrationParameterConfigureViewModel> Settings { get; set; }

        public static List<IntegrationParameterGroupViewModel> ToGroupVM(List<IntegrationParamterDTO> settings)
        {
            var groupVMs = new List<IntegrationParameterGroupViewModel>();

            foreach (var group in settings.Select(a => a.ParameterType).Distinct().OrderBy(a => a))
            {
                var groupVM = new IntegrationParameterGroupViewModel();
                groupVM.GroupName = group == null ? "Default" : group;

                foreach (var dto in (group == null ? settings.Where(a => group == null).OrderBy(a => a.ParameterType).ThenBy(a => a.ParameterName)
                    : settings.Where(a => a.ParameterType == group).OrderBy(a => a.ParameterName).ThenBy(a => a.ParameterName)))
                {
                    groupVM.Settings.Add(new IntegrationParameterConfigureViewModel()
                    {
                        IntegrationParameterId = dto.IntegrationParameterId,
                        ParameterDataType = dto.ParameterDataType,
                        ParameterName = dto.ParameterName,
                        ParameterType = dto.ParameterType,
                        ParameterValue = dto.ParameterValue
                    });
                }

                groupVMs.Add(groupVM);
            }

            return groupVMs;
        }

    }
}