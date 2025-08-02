using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Enums.Schedulers;
using Eduegate.Web.Library.ViewModels.Scheduler;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AssetCategory", "CRUDModel.ViewModel")]
    [DisplayName("Asset Category ")]
    public class AssetCategoryViewModel : BaseMasterViewModel
    {
        public AssetCategoryViewModel()
        {
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("AssetCategory ID")]
        public long AssetCategoryID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Name")]
        public string CategoryName { get; set; }

        public static AssetCategoryViewModel ToViewModel(AssetCategoryDTO dto)
        {
            if (dto != null)
            {
                return new AssetCategoryViewModel()
                {
                    AssetCategoryID = dto.AssetCategoryID,
                    CategoryName = dto.CategoryName
                };
            }
            else return new AssetCategoryViewModel();
        }

        public static AssetCategoryDTO ToDto(AssetCategoryViewModel vm)
        {
            if (vm != null)
            {
                return new AssetCategoryDTO()
                {
                    AssetCategoryID = vm.AssetCategoryID,
                    CategoryName = vm.CategoryName,
                };
            }
            else return new AssetCategoryDTO();
        }

        public static KeyValueViewModel ToKeyValueViewModel(AssetCategoryDTO dto)
        {
            if (dto != null)
            {
                return new KeyValueViewModel()
                {
                    Key = dto.AssetCategoryID.ToString(),
                    Value = dto.CategoryName
                };
            }
            else return new KeyValueViewModel();
        }
    }
}
