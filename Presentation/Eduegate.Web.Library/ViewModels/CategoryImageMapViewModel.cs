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
    public partial class CategoryImageMapViewModel : BaseMasterViewModel
    {
        public long CategoryImageMapIID { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public Nullable<byte> ImageTypeID { get; set; }
        public string ImageFile { get; set; }
        public string ImageTitle { get; set; }

        public static CategoryImageMapViewModel FromDTO(CategoryImageMapDTO dto)
        {
            return new CategoryImageMapViewModel()
            {
               CategoryImageMapIID = dto.CategoryImageMapIID,
               CategoryID = dto.CategoryID,
               ImageTypeID = dto.ImageTypeID,
               ImageFile = string.Format("{0}/{1}/{2}", new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl"), EduegateImageTypes.Category.ToString(), dto.ImageFile),//dto.ImageFile,
               ImageTitle = dto.ImageTitle
            };
        }
    }
}
