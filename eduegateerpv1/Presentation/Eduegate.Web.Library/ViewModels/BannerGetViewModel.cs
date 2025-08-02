using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;

namespace Eduegate.Web.Library.ViewModels
{
    public class BannerGetViewModel : BaseMasterViewModel
    {
        public long BannerID { get; set; }

        public string BannerName { get; set; }

        public string BannerFile { get; set; }

        public string BannerUrl { get; set; }

        public string Target { get; set; }

        public static BannerGetViewModel FromDTO(BannerMasterDTO dto)
        {
            return new BannerGetViewModel()
            {
                BannerID = dto.BannerIID,
                BannerName = dto.BannerName,
                BannerFile = string.Format("{0}/{1}/{2}", new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl"), EduegateImageTypes.Banners.ToString(), dto.BannerFile),
                BannerUrl = dto.Link,
                Target = dto.Target
            };
        }

        
    }
}
