using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.SearchData;
using Eduegate.Services;

namespace Eduegate.Service.Client.Direct
{
    public class BannerServiceClient : IBannerService
    {
        BannerService service = new BannerService();

        public BannerServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }

        public List<BannerMasterDTO> GetBanners()
        {
            return service.GetBanners();
        }

        public List<BannerMasterDTO> GetBannersByType(Eduegate.Services.Contracts.Enums.BannerTypes bannerType, Eduegate.Services.Contracts.Enums.BannerStatuses status)
        {
            return service.GetBannersByType(bannerType, status);
        }

        public BannerMasterDTO GetBanner(string bannerID)
        {
            return service.GetBanner(bannerID);
        }

        public BannerMasterDTO SaveBanner(BannerMasterDTO bannerDTO)
        {
            return service.SaveBanner(bannerDTO);
        }

    }
}
