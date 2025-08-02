using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Banner;

namespace Eduegate.Services.Contracts
{
    public interface IBannerManagement
    {
        List<BannerConfigurationDTO> GetBannerConfigurations();
        BannerConfigurationDTO GetBannerConfiguration(long bannerConfigurationIID);
        BannerConfigurationDTO UpdateBannerConfiguration(BannerConfigurationDTO bannerConfiguration);
    }
}
