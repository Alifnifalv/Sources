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

namespace Eduegate.Service.Client
{
    public class BannerServiceClient : BaseClient, IBannerService
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string productService = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.BANNER_SERVICE_NAME);
        public BannerServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public List<BannerMasterDTO> GetBanners()
        {
            var bannerUri = string.Format("{0}/{1}", productService, "GetBanners");
            return ServiceHelper.HttpGetRequest<List<Eduegate.Services.Contracts.BannerMasterDTO>>(bannerUri, _callContext, _logger);
        }

        public List<BannerMasterDTO> GetBannersByType(Eduegate.Services.Contracts.Enums.BannerTypes bannerType, Eduegate.Services.Contracts.Enums.BannerStatuses status)
        {
            var bannerUri = string.Format("{0}/{1}?bannerType={2}&status={3}", productService, "GetBannersByType", bannerType, status);
            return ServiceHelper.HttpGetRequest<List<Eduegate.Services.Contracts.BannerMasterDTO>>(bannerUri, _callContext, _logger);
        }

        public BannerMasterDTO GetBanner(string bannerID)
        {
            var bannerUri = string.Format("{0}/{1}?bannerID={2}", productService, "GetBanner", bannerID);
            return ServiceHelper.HttpGetRequest<BannerMasterDTO>(bannerUri, _callContext, _logger);
        }

        public BannerMasterDTO SaveBanner(BannerMasterDTO bannerDTO)
        {
            var bannerUri = string.Format("{0}/{1}", productService, "SaveBanner");
            return ServiceHelper.HttpPostGetRequest<BannerMasterDTO>(bannerUri, bannerDTO, _callContext, _logger);
        }

    }
}
