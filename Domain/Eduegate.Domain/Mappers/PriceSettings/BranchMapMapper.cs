using System;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using System.Collections.Generic;
using Eduegate.Framework.Security.Secured;

namespace Eduegate.Domain.Mappers
{
    public class BranchMapMapper : IDTOEntityMapper<BranchMapDTO, ProductPriceListBranchMap>
    {
        private CallContext _context;

        public static BranchMapMapper Mapper(CallContext context)
        {
            var mapper = new BranchMapMapper();
            mapper._context = context;
            return mapper;
        }
        public BranchMapDTO ToDTO(ProductPriceListBranchMap entity)
        {
            return new BranchMapDTO()
            {
                ProductPriceListBranchMapIID = entity.ProductPriceListBranchMapIID,
                BranchID = entity.BranchID,
                BranchName = entity.Branch.BranchName,
                ProductPriceListID = entity.ProductPriceListID,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
                //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
            };
        }

        public ProductPriceListBranchMap ToEntity(BranchMapDTO dto)
        {
            return new ProductPriceListBranchMap()
            {
                ProductPriceListBranchMapIID = dto.ProductPriceListBranchMapIID,
                BranchID = dto.BranchID,
                ProductPriceListID = dto.ProductPriceListID,
                CreatedDate = dto.ProductPriceListBranchMapIID == default(long) ? DateTime.Now : dto.CreatedDate,
                CreatedBy = dto.ProductPriceListBranchMapIID == default(long) ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedDate = dto.ProductPriceListBranchMapIID == default(long) ? DateTime.Now : dto.UpdatedDate,
                UpdatedBy = dto.ProductPriceListBranchMapIID == default(long) ? (int)_context.LoginID : dto.UpdatedBy,
                //TimeStamps = dto.TimeStamps.IsNotNull() ? Convert.FromBase64String(dto.TimeStamps) : null,
            };

        }
        public System.Collections.Generic.List<BranchMapDTO> ToDTO(List<Branch> entities)
        {
            var branchLists = new List<BranchMapDTO>();
            var secured = new SecuredData(new Eduegate.Domain.Repository.Security.SecurityRepository().GetUserClaimKey(_context.LoginID.Value, (int)Eduegate.Services.Contracts.Enums.ClaimType.Branches));
            foreach (var entity in entities)
            {
                // Only those brach will return which has permission
                if (secured.HasAccess(entity.GetIID(), (Eduegate.Framework.Security.Enums.ClaimType)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.ClaimType), Eduegate.Services.Contracts.Enums.ClaimType.Branches.ToString())))
                {
                    branchLists.Add(ToDTO(entity));
                }
            }

            return branchLists;
        }

        public BranchMapDTO ToDTO(Branch entity) 
        {
            return new BranchMapDTO() 
            {
               BranchID = entity.BranchIID,
               BranchName = entity.BranchName,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
                //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
            };
        }
      
    }
}
