using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts.Eduegates;

namespace Eduegate.Domain
{
    public class BannerDataBL
    {
        BannerDataRepository bannerDataRepository = new BannerDataRepository();
        public List<BannerDTO> GetBanners()
        {
            var banners = bannerDataRepository.GetBanners();
            return banners;
        }
    }
}
