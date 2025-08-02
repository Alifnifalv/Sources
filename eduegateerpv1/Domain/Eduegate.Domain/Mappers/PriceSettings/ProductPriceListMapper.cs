using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Security.Secured;

namespace Eduegate.Domain.Mappers.PriceSettings
{
    public class ProductPriceListMapper : IDTOEntityMapper<ProductPriceDTO, ProductPriceList>
    {
        private CallContext _context;

        public static ProductPriceListMapper Mapper(CallContext context)
        {
            var mapper = new ProductPriceListMapper();
            mapper._context = context;
            return mapper;
        }

        public List<ProductPriceDTO> ToDTO(List<ProductPriceList> entities)
        {
            var secured = new SecuredData(new Eduegate.Domain.Repository.Security.SecurityRepository().GetUserClaimKey(_context.LoginID.Value, (int)Eduegate.Services.Contracts.Enums.ClaimType.ProductPrice));
            var priceLists = new List<ProductPriceDTO>();

            foreach (var entity in entities)
            {
                if (secured.HasAccess(entity.GetIID(), (Eduegate.Framework.Security.Enums.ClaimType)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.ClaimType), Eduegate.Services.Contracts.Enums.ClaimType.ProductPrice.ToString())))
                {
                    priceLists.Add(ToDTO(entity));
                }
            }

            return priceLists;
        }

        public ProductPriceDTO ToDTO(ProductPriceList entity)
        {
            return new ProductPriceDTO()
            {
                ProductPriceListIID = entity.ProductPriceListIID,
                PriceDescription = entity.PriceDescription,
                CompanyID = entity.CompanyID.IsNotNull()? entity.CompanyID : _context.CompanyID,
                EndDate = entity.EndDate,
                Price = entity.Price,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                StartDate = entity.StartDate,
                PricePercentage = entity.PricePercentage,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
                //TimeStamps = entity.TimeStamps.IsNotNull() ? Convert.ToBase64String(entity.TimeStamps) : null,
                ProductPriceListLevelID = entity.ProductPriceListLevelID,
                ProductPriceListTypeID = entity.ProductPriceListTypeID,
            };
        }

        public ProductPriceList ToEntity(ProductPriceDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
