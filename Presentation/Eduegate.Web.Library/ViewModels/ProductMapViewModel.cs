using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Web.Library.ViewModels
{
    public class ProductMapViewModel
    {
        public long ProductToProductMapID { get; set; }
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public int SalesRelationShipType { get; set; }
        public long ProductSKUMapID { get; set; }
         

        public static ProductMapViewModel FromDTO(ProductMapDTO dto)
        {
            return new ProductMapViewModel()
            {
                ProductToProductMapID = dto.ProductToProductMapID,
                ProductID = dto.ProductID,
                ProductName = dto.ProductName,
                SalesRelationShipType = dto.SalesRelationShipType,
            };
        }


        public static ProductMapDTO ToDTO(ProductMapViewModel obj)
        {
            return new ProductMapDTO()
            {
                ProductToProductMapID = obj.ProductToProductMapID,
                ProductID = obj.ProductID,
                ProductName = obj.ProductName,
                SalesRelationShipType = obj.SalesRelationShipType,
            };
        }

        public static List<ProductMapViewModel> FromDTOToViewModel(List<ProductMapDTO> productDTOList)
        {
            List<ProductMapViewModel> productVMList = new List<ProductMapViewModel>();

            if (productDTOList.IsNotNull() && productDTOList.Count > 0) 
            {
                foreach (ProductMapDTO product in productDTOList)
                {
                    productVMList.Add(new ProductMapViewModel()
                    {
                        ProductID = product.ProductID,
                        ProductName = product.ProductName,
                        ProductSKUMapID = product.ProductSKUMapID,
                    });
                }
            }

            return productVMList;
        }
    }
}
