using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;

namespace Eduegate.Web.Library
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "Maps", "CRUDModel.ViewModel.CategorySettingMap.Maps")]
    [DisplayName("Category Settings")]
    public class CategorySettingsViewModel : BaseMasterViewModel
    {
        public long CategorySettingsID { get; set; }
        public long CategoryID { get; set; }
        public string SettingCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown, "large-col-width")]
        [DisplayName("Setting Value")]
        [LookUp("GetSubLookUpData('CategorySettings',gridModel.SettingCode)")]
        public string SettingValue { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.CategorySettingMap.Maps[0], CRUDModel.ViewModel.CategorySettingMap.Maps)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.CategorySettingMap.Maps[0], CRUDModel.ViewModel.CategorySettingMap.Maps)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}
