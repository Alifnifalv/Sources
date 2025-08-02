using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.DataAccess.Interfaces;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts.Banner;

namespace Eduegate.Domain
{
    public class BannerManagementBL
    {
        private static IBannerManagementDA bannerManagement = new BannerMangementRepository();

        public static List<BannerConfigurationDTO> GetBannerConfigurations()
        {
            return BannerConfigurationDTO.ToDTO(bannerManagement.GetBannerConfigurations());
        }
        public static BannerConfigurationDTO GetBannerConfiguration(long configurationIID)
        {
            return BannerConfigurationDTO.ToDTO(bannerManagement.GetBannerConfiguration(configurationIID));
        }
        public static BannerConfigurationDTO GetBannerConfiguration(string configurationName)
        {
            return BannerConfigurationDTO.ToDTO(bannerManagement.GetBannerConfiguration(configurationName));
        }

        public static List<BannerConfigurationDTO> GetBannerConfigurationsByProductID(long productID)
        {
            return BannerConfigurationDTO.ToDTO(bannerManagement.GetBannerConfigurationsByProductID(productID));
        }

        public static BannerConfigurationDTO UpdateBannerConfiguration(BannerConfigurationDTO configurationDTO)
        {
            var bannerConfiguration = BannerConfigurationDTO.ToDTO(bannerManagement.UpdateBannerConfiguration(
                BannerConfigurationDTO.ToEntity(configurationDTO),
                BannerPropertyDTO.ToEntity(configurationDTO.Properties.Where(a => a is BannerWatermarkImageDTO).Cast<BannerWatermarkImageDTO>().ToList()),
                BannerPropertyDTO.ToEntity(configurationDTO.Properties.Where(a => a is BannerWatermarkTextDTO).Cast<BannerWatermarkTextDTO>().ToList()),
                BannerConfigurationDTO.ToEntity(configurationDTO.ContextParameters, configurationDTO.BannerConfigurationIID)));
            UpdateBannerVersion(configurationDTO.BannerID);
            return bannerConfiguration;
        }

        public static void UpdateBannerVersion(long bannerID)
        {
            var categoryBanner = bannerManagement.GetCategoryBanner(int.Parse(bannerID.ToString()));
            var indexQueryString = categoryBanner.Link.LastIndexOf('?');
            var newVersionLink = indexQueryString <= 0 ? categoryBanner.Link + "?1" :
                categoryBanner.Link + "?" +
                (int.Parse(categoryBanner.Link.Substring(indexQueryString + 1, categoryBanner.Link.Length)) + 1).ToString();
            categoryBanner.Link = newVersionLink;
            bannerManagement.UpdateCategoryBanner(categoryBanner);
        }
    }
}
