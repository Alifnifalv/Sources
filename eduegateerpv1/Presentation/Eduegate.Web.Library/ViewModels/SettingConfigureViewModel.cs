using Eduegate.Services.Contracts.Settings;

namespace Eduegate.Web.Library.ViewModels
{
    public class SettingConfigureViewModel : BaseMasterViewModel
    {
        public SettingConfigureViewModel()
        {
            IsDirty = false;
        }

        public int? CompanyID { get; set; }
        public int? SiteID { get; set; }
        public string SettingCode { get; set; }
        
        //TODO: Need to check if this is required
        //[AllowHtml]
        public string SettingValue { get; set; }

        public string Description { get; set; }
        public string ValueType { get; set; }
        public int? LookupTypeID { get; set; }
        public bool IsDirty { get; set; }

        public static SettingDTO ToDTO(SettingConfigureViewModel vm)
        {
            return new SettingDTO()
            {
                CompanyID = vm.CompanyID,
                Description = vm.Description,
                SettingCode = vm.SettingCode,
                SettingValue = vm.SettingValue
            };
        }
    }
}
