using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;


namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "CategoryMarketPlaceViewModel", "CRUDModel.ViewModel.CategoryMarketPlace")]
    public class CategoryMarketPlaceViewModel : BaseMasterViewModel
    {
        public CategoryMarketPlaceViewModel()
        {
            Profit = 10;
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        [DisplayName("Profit %")]
        public Nullable<decimal> Profit { get; set; }
    }
}
