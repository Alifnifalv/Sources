using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Framework.Security.Secured;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Domain.Mappers.Catalog
{
    public class ProductStatusMapper : IDTOEntityMapper<ProductStatusDTO, ProductStatu>
    {
        private CallContext _context;

        public static ProductStatusMapper Mapper(CallContext context)
        {
            var mapper = new ProductStatusMapper();
            mapper._context = context;
            return mapper;
        }

        public List<ProductStatusDTO> ToDTO(List<ProductStatu> entities)
        {
            var secured = new SecuredData(new Eduegate.Domain.Repository.Security.SecurityRepository().GetUserClaimKey(_context.LoginID.Value));
            var dtos = new List<ProductStatusDTO>();

            foreach (var entity in entities)
            {
                if (secured.HasAccess(entity.GetIID(), (Eduegate.Framework.Security.Enums.ClaimType)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.ClaimType), 
                    Eduegate.Services.Contracts.Enums.ClaimType.ProductStatus.ToString())))
                {
                    dtos.Add(ToDTO(entity));
                }
            }

            return dtos;
        }

        public ProductStatusDTO ToDTO(ProductStatu entity)
        {
            return new ProductStatusDTO()
            {
                StatusIID = entity.ProductStatusID,
                StatusName = entity.StatusName
            };
        }

        public ProductStatu ToEntity(ProductStatusDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
