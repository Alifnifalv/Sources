using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Settings;

namespace Eduegate.Web.Library.ViewModels
{
    public class SettingConfigureGroupViewModel : BaseMasterViewModel
    {
        public SettingConfigureGroupViewModel()
        {
            Settings = new List<SettingConfigureViewModel>();
        }

        public string GroupName { get; set; }
        public List<SettingConfigureViewModel> Settings { get; set; }       

        public static List<SettingConfigureGroupViewModel> ToGroupVM(List<SettingDTO> settings)
        {
            var groupVMs = new List<SettingConfigureGroupViewModel>();

            foreach(var group in settings.Select(a=> a.GroupName).Distinct().OrderBy(a=> a))
            {
                var groupVM = new SettingConfigureGroupViewModel();
                groupVM.GroupName = group == null ? "Default" : group;

                foreach (var dto in (group == null ? settings.Where(a => group == null).OrderBy(a => a.CompanyID).ThenBy(a=> a.SiteID).ThenBy(a=> a.SettingCode)
                    : settings.Where(a => a.GroupName == group).OrderBy(a => a.CompanyID).ThenBy(a => a.SiteID).ThenBy(a => a.SettingCode)))
                {
                    groupVM.Settings.Add(new SettingConfigureViewModel()
                    {
                        SettingCode = dto.SettingCode,
                        SettingValue = dto.SettingValue,
                        Description = dto.Description,
                        SiteID = dto.SiteID,
                        CompanyID = dto.CompanyID,
                        LookupTypeID = dto.LookupTypeID,
                        ValueType = dto.ValueType
                    });
                }

                groupVMs.Add(groupVM);
            }

            return groupVMs;
        }
    }
}
