using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Web.Library.ViewModels
{
    public partial class CategoryViewModel : BaseMasterViewModel
    {
        public long CategoryIID { get; set; }
        public Nullable<long> ParentCategoryID { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string ImageName { get; set; }
        public string ThumbnailImageName { get; set; }
        public Nullable<bool> IsAcive { get; set; }
        public List<CategoryViewModel> CategoryList { get; set; }
        public List<CategoryImageMapViewModel> CategoryImageMapList { get; set; }

        public static CategoryViewModel FromDTO(CategoryDTO dto)
        {
            return new CategoryViewModel()
            {
                CategoryIID = dto.CategoryIID,
                ParentCategoryID = dto.ParentCategoryID,
                CategoryCode = dto.CategoryCode,
                CategoryName = dto.CategoryName,
                ImageName = string.Format("{0}/{1}/{2}", new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl"), EduegateImageTypes.Category.ToString(), dto.ImageName),//dto.ImageName,
                ThumbnailImageName = string.Format("{0}/{1}/{2}", new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl"), EduegateImageTypes.Category.ToString(), dto.ThumbnailImageName),// dto.ThumbnailImageName,
                IsAcive = dto.IsActive
            };
        }
    }
}
