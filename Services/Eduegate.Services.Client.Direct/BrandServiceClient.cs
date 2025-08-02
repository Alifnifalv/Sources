using System;
using System.Collections.Generic;
using Eduegate.Framework;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Service.Client.Direct
{
    public class BrandServiceClient : IBrandService
    {
        BrandService service = new BrandService();
        public BrandServiceClient(CallContext callContext = null, Action<string> logger = null)
        {
            service.CallContext = callContext;
        }

        public BrandDTO GetBrand(long brandID)
        {
            return service.GetBrand(brandID);
        }

        public BrandDTO SaveBrand(BrandDTO brandDetail)
        {
            return service.SaveBrand(brandDetail);
        }

        public bool BrandNameAvailibility(string brandName, long brandIID)
        {
            return service.BrandNameAvailibility(brandName,brandIID);
        }

        public List<KeyValueDTO> GetBrandTags()
        {
            return service.GetBrandTags();
        }

    }
}