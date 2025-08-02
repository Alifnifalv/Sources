using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Settings
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "CustomerSettings", "CRUDModel.ViewModel.Settings")]
    [DisplayName("Customer Settings")]
    public class CustomerSettingsViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Settings ID")]
        public long CustomerSettingIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Is Verified")]
        public Nullable<bool> IsVerified { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Is Confirmed")]
        public Nullable<bool> IsConfirmed { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Is Blocked")]
        public Nullable<bool> IsBlocked { get; set; } 

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Current loyalty points")]
        public decimal? CurrentLoyaltyPoints { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Total loyalty points")]
        public decimal? TotalLoyaltyPoints { get; set; }
    }
}
