using System.Collections.Generic;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBannerService" in both code and config file together.
    public interface IBannerService
    {
        List<BannerMasterDTO> GetBanners();

        List<BannerMasterDTO> GetBannersByType(BannerTypes bannerType, BannerStatuses status);

        BannerMasterDTO GetBanner(string bannerID);

        BannerMasterDTO SaveBanner(BannerMasterDTO bannerDTO);
    }
}