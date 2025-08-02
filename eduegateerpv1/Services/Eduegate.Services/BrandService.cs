using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services
{
    public class BrandService : BaseService, IBrandService
    {
        public BrandDTO GetBrand(long brandID)
        {
            try
            {
                return new BrandBL(CallContext).GetBrand(brandID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<BrandService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public BrandDTO SaveBrand(BrandDTO brandDetail)
        {
            try
            {
                return new BrandBL(CallContext).SaveBrand(brandDetail);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<BrandService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool BrandNameAvailibility(string brandName, long brandIID)
        {
            try
            {
                return new BrandBL(CallContext).BrandNameAvailibility(brandName,brandIID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<BrandService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<KeyValueDTO> GetBrandTags()
        {
            try
            {
                return new BrandBL(CallContext).GetBrandTags();
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<BrandService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
    }
}
