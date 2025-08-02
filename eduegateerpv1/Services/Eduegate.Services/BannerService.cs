using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services
{
    public class BannerService : BaseService, IBannerService
    {
        public List<BannerMasterDTO> GetBanners()
        {
            try
            {
                return new BannerBL(base.CallContext).GetBanners();
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<BannerService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<BannerMasterDTO> GetBannersByType(BannerTypes bannerType, BannerStatuses status)
        {
            try
            {
                return new BannerBL(base.CallContext).GetBanners(bannerType, status);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<BannerService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public BannerMasterDTO GetBanner(string bannerID)
        {
            try
            {
                return new BannerBL(this.CallContext).GetBanner(long.Parse(bannerID));
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<BannerService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public BannerMasterDTO SaveBanner(BannerMasterDTO bannerDTO)
        {
            try
            {
                return new BannerBL(this.CallContext).SaveBanner(bannerDTO);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<BannerService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
    }
}
