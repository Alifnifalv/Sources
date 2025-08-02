using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "SecuritySettings", "CRUDModel.ViewModel.SecuritySettings")]
    [DisplayName("Security")]
    public class GrandAccessSettingsViewModel : BaseMasterViewModel
    {
        public long ClaimIID { get; set; }

        public long? LoginClaimsetID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft")]
        [CustomDisplay("Select")]
        public bool? IsRowSelected { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Menus")]
        public string ClaimName { get; set; }


    }
}
