using System.Collections.Generic;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBrandService" in both code and config file together.
    public interface IBrandService
    {
        public BrandDTO SaveBrand(BrandDTO brandDetail);

        public BrandDTO GetBrand(long brandID);

        public bool BrandNameAvailibility(string brandName, long brandIID);

        public List<KeyValueDTO> GetBrandTags();
    }    
}