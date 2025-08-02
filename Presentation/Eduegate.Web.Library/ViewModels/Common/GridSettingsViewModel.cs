using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Common
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "Settings", "CRUDModel.ViewModel.Settings")]
    [DisplayName("Settings")]
    public class GridSettingsViewModel : BaseMasterViewModel
    {
        public GridSettingsViewModel()
        {
            SettingCode = new KeyValueViewModel();
        }

        public long? ReferenceID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "ex-large-col-width")]
        [DisplayName("Setting Value")]
        [LookUp("LookUps.UserSettingsMaster")]
        [Select2("SettingCode", "String", false, "OnSelect2ChangeOverrides(gridModel, $select)", false, "ng-click=LoadUniqueSettings('UserSettings',CRUDModel.ViewModel.Settings)")]
        public KeyValueViewModel SettingCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [LazyLoad("", "Mutual/GetLazyDBLookUpData?lookType={{gridModel.SettingCode.Key}}", "LookUps.DataLookUp_{{gridModel.SettingCode.Key}}", true, dynamicDataSource: "GetSettingValue(gridModel.SettingCode.Key, 'DataLookUp')")]
        [DisplayName("Value")]
        [Select2("SettingValue", "String", false, "", false, "", false)]
        public KeyValueViewModel SettingValue { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.ViewModel.Settings[0], CRUDModel.ViewModel.Settings)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.ViewModel.Settings[0], CRUDModel.ViewModel.Settings)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        public static List<GridSettingsViewModel> FromDTO(List<SettingDTO> dtos, long? loginID)
        {
            var vms = new List<GridSettingsViewModel>();

            foreach (var dto in dtos)
            {
                vms.Add(FromDTO(dto, loginID));
            }

            return vms;
        }

        public static GridSettingsViewModel FromDTO(SettingDTO dto, long? loginID)
        {
            return new GridSettingsViewModel()
            {
                 Description = dto.Description,
                 SettingCode = new KeyValueViewModel() { Key = dto.SettingCode, Value = dto.Description },
                 SettingValue = new KeyValueViewModel() { Key = dto.SettingValue, Value = dto.SettingValue },
                 ReferenceID = loginID.HasValue ? loginID.Value : (long?)null
            };
        }

        public static List<GridSettingsViewModel> GetMergedSettingVM(List<SettingDTO> userSettings, long? loginID)
        {
            var vms = new List<GridSettingsViewModel>();

            if (userSettings != null)
            {              
                foreach (var setting in userSettings)
                {                  
                    var settingCode = new GridSettingsViewModel()
                    {
                        SettingCode = new KeyValueViewModel() { Key = setting.SettingCode, Value = string.IsNullOrEmpty(setting.Description) ? setting.SettingCode : setting.Description },
                        Description = setting.Description,
                        SettingValue = new KeyValueViewModel() { Key = setting.SettingValue, Value = setting.SettingValue },
                        ReferenceID = loginID
                    };

                    vms.Add(settingCode);
                }
            }

            if(vms.Count == 0)
            {
                vms.Add(new GridSettingsViewModel());
            }

            return vms;
        }
    }
}
