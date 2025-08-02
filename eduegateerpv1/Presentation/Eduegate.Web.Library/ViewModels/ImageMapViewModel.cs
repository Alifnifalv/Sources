using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels
{
    public class ImageMapViewModel : BaseMasterViewModel
    {
        public ImageMapViewModel()
        {
            Maps = new List<ImageTypeMapViewModel>();
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)] //"", "", "", "ng-click='InsertGridRow($index, ModelStructure.ViewModel.PriceListsMap.Maps[0], CRUDModel.ViewModel.PriceListsMap.Maps)'"
        [DisplayName("")]
        public List<ImageTypeMapViewModel> Maps { get; set; }
    }
}
