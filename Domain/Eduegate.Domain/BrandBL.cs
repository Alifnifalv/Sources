using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Mappers.Catalog;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Domain
{
    public class BrandBL
    {
        private CallContext _callContext;

        public BrandBL(CallContext context)
        {
            _callContext = context;
        }

        public BrandDTO GetBrand(long brandID)
        {
            return BrandMapper.Mapper(_callContext).ToDTO(new BrandRepository().GetBrand(brandID));
        }

        public BrandDTO SaveBrand(BrandDTO brandDetails)
        {
            var mapper = BrandMapper.Mapper(_callContext);
            var IsBrandExist = BrandNameAvailibility(brandDetails.BrandName, brandDetails.BrandIID);
            if (IsBrandExist)
            {
                brandDetails.IsError = true;
                brandDetails.ErrorCode = ErrorCodes.Brand.B001;
                return brandDetails;
            }
            else
            {
                brandDetails.IsError = false;
            }
            var brand = new BrandRepository().SaveBrand(mapper.ToEntity(brandDetails), _callContext);
            var brandmapper = mapper.ToDTO(brand);
            if(brandDetails.Status == true)
            {
                var brandstatusID = brand.StatusID == (byte)BrandStatuses.Active ? (byte)ProductStatuses.Active : (byte)ProductStatuses.Inactive;
                var updatestatus = new ProductDetailRepository().UpdateProductSKUStatus(brandstatusID, brand.BrandIID, _callContext);

                brandmapper.Status = true;
            }
            return brandmapper;
        }

        public bool BrandNameAvailibility(string brandName,long brandIID)
        {
            return new BrandRepository().BrandNameAvailibility(brandName, brandIID);
        }

        public List<KeyValueDTO> GetBrandTags()
        {
            return BrandTagMapper.Mapper(_callContext).ToDTO(new BrandRepository().GetBrandTags());
        }
    }
}
