using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.CustomEntity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.ProductDetail;

namespace Eduegate.Domain.Mappers
{
    public class ProductSKUDetailMapper : IDTOEntityMapper<ProductSKUDetailDTO, ProductSKUDetail>
    {
        private CallContext _context;
        public static ProductSKUDetailMapper Mapper(CallContext context)
        {
            var mapper = new ProductSKUDetailMapper();
            mapper._context = context;
            return mapper;
        }

        public List<ProductSKUDetailDTO> ToDTO(List<ProductSKUDetail> entities)
        {
            var dtos = new List<ProductSKUDetailDTO>();

            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }
         
            return dtos;
        }

        public ProductSKUDetailDTO ToDTO(ProductSKUDetail entity)
        {
            if (entity != null)
            {
                return new ProductSKUDetailDTO()
                {
                    ProductID = entity.ProductIID,
                    ProductPartNo = entity.PartNo,
                    ProductName = entity.ProductName,
                    ProductPrice = entity.ProductPrice.HasValue ? (decimal)entity.ProductPrice : 0,
                    SKUID = entity.ProductSKUMapIID,
                    SKUName = entity.SKU,
                    ProductCode = entity.ProductSKUCode,
                    BrandName = entity.BrandName,
                    BrandCode = entity.BrandCode,
                    Calorie = entity.Calorie,
                    Weight= entity.Weight,
                    Allergies = getAllergyList(entity.ProductIID),
                };
            }
            else return new ProductSKUDetailDTO();
        }

        public ProductSKUDetail ToEntity(ProductSKUDetailDTO dto)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> getAllergyList(long ProductIID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var allergyList = new List<KeyValueDTO>();
                var allergy = dbContext.ProductAllergyMaps.Where(a => a.ProductID == ProductIID).ToList();
                if (allergy != null)
                {
                    foreach (var item in allergy)
                    {
                        allergyList.Add(new KeyValueDTO() { Key = item.AllergyID.ToString(), Value = item.Allergy.AllergyName });
                    }
                }
                return allergyList;
            }
        }
    }
}
