using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Domain.Repository
{
    public class ProductSKUTagRepository
    {
        public List<ProductSKUTag> GetProductSKUTagMaps(List<long> skuIDs)
        {
            using (var db = new dbEduegateERPContext())
            {
                var result = from pstm in db.ProductSKUTagMaps
                             join pst in db.ProductSKUTags on pstm.ProductSKUTagID equals pst.ProductSKUTagIID
                             join sku in skuIDs on pstm.ProductSKuMapID equals sku
                             select pst;
                return result.ToList();
            }
        }

        public List<ProductSKUTag> GetProductSKUTags()
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.ProductSKUTags.ToList();
            }
        }

         
    }
}
