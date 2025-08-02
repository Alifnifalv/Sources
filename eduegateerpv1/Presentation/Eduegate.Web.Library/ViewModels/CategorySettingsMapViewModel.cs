using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels
{
    public class CategorySettingsMapViewModel : BaseMasterViewModel
    {
        public CategorySettingsMapViewModel()
        {
            Maps = new List<CategorySettingsViewModel>();
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)] 
        [DisplayName("")]
        public List<CategorySettingsViewModel> Maps { get; set; }
    }
}
