using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Eduegates;

namespace Eduegate.Domain.Repository
{
    public class BannerDataRepository
    {
         public List<BannerDTO> GetBanners()
         {
             List<BannerDTO> banners = new List<BannerDTO>();

             BannerDTO banner = new BannerDTO();
             banner.FileName = "Banner1.jpg";
             banners.Add(banner);

             banner = new BannerDTO();
             banner.FileName = "Banner2.jpg";
             banners.Add(banner);

             banner = new BannerDTO();
             banner.FileName = "Banner3.jpg";
             banners.Add(banner);

             banner = new BannerDTO();
             banner.FileName = "Banner4.jpg";
             banners.Add(banner);

             banner = new BannerDTO();
             banner.FileName = "Banner5.jpg";
             banners.Add(banner);

             return banners;
         }
    }
}
