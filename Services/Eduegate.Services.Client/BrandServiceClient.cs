using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Service.Client
{
    public class BrandServiceClient : BaseClient, IBrandService
    {
        private static string _serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string _brandService = string.Concat(_serviceHost, Eduegate.Framework.Helper.Constants.BRAND_SERVICE_NAME);
        public BrandServiceClient(CallContext callContext = null, Action<string> logger = null)
            :base(callContext, logger)
        {
        }
        public BrandDTO GetBrand(long brandID)
        {
            var uri = _brandService + "GetBrand?brandID=" + brandID;
            return ServiceHelper.HttpGetRequest<BrandDTO>(uri, _callContext);
        }

        public BrandDTO SaveBrand(BrandDTO brandDetail)
        {
            var uri = _brandService + "SaveBrand";
            return ServiceHelper.HttpPostGetRequest<BrandDTO>(uri, brandDetail, _callContext);
        }

        public bool BrandNameAvailibility(string brandName, long brandIID)
        {
            var uri = _brandService + "BrandNameAvailibility?brandName=" + brandName+ "&&brandIID="+brandIID;
            return ServiceHelper.HttpGetRequest<bool>(uri, _callContext);
        }

        public List<KeyValueDTO> GetBrandTags()
        {
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(_brandService + "GetBrandTags", _callContext);
        }
    }
}
