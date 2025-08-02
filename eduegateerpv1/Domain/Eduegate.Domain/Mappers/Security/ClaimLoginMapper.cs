using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Admin;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Security;

namespace Eduegate.Domain.Mappers.Security
{
    public class ClaimLoginMapper : IDTOEntityMapper<ClaimDTO, Eduegate.Domain.Entity.Models.ClaimLoginMap>
    {
        private CallContext _context;

        public static ClaimLoginMapper Mapper(CallContext context)
        {
            var mapper = new ClaimLoginMapper();
            mapper._context = context;
            return mapper;

        }

        public ClaimDTO ToDTO(Entity.Models.ClaimLoginMap entity)
        {
            throw new NotImplementedException();
        }

        public Entity.Models.ClaimLoginMap ToEntity(ClaimDTO dto)
        {
            throw new NotImplementedException();
        }
        public List<KeyValueDTO> ToKeyValueDTO(List<Entity.Models.ClaimLoginMap> entities)
        {
            var dto = new List<KeyValueDTO>();

            foreach (var entity in entities)
            {
                dto.Add(ToKeyValueDTO(entity));
            }

            return dto;
        }

        public List<ClaimDetailDTO> ToClaimDetailDTO(List<Entity.Models.ClaimLoginMap> entities)
        {
            var dto = new List<ClaimDetailDTO>();

            foreach (var entity in entities)
            {
                dto.Add(new ClaimDetailDTO() { ClaimIID = entity.ClaimID.Value, ClaimName = entity.Claim.ClaimName, 
                    ClaimType = (Eduegate.Services.Contracts.Enums.ClaimType)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.ClaimType), entity.Claim.ClaimTypeID.Value.ToString()) });
            }

            return dto;
        }

        public KeyValueDTO ToKeyValueDTO(Entity.Models.ClaimLoginMap entity)
        {
            return new KeyValueDTO() { Key = entity.ClaimID.ToString(), Value = entity.Claim.ClaimName };
        }

        public List<ClaimLoginMap> FromKeyValueDTO(List<KeyValueDTO> dtos, long loginID)
        {
            var entity = new List<ClaimLoginMap>();
            if(dtos != null){
                foreach (var dto in dtos)
                {
                    entity.Add(FromKeyValueDTO(dto, loginID));
                }
            }

            return entity;
        }

        public ClaimLoginMap FromKeyValueDTO(KeyValueDTO dto, long loginID)
        {
            return new ClaimLoginMap()
            {
                ClaimID = long.Parse(dto.Key),
                LoginID = loginID,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                UpdatedBy = int.Parse(_context.LoginID.ToString()),
                CreatedBy = int.Parse(_context.LoginID.ToString()),
            };
        }

        public ClaimLoginMap FromClaimDetailDTO(Eduegate.Services.Contracts.Admin.ClaimDetailDTO dto, long loginID)
        {
            return new ClaimLoginMap()
            {
                ClaimID = dto.ClaimIID,
                LoginID = loginID,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                UpdatedBy = int.Parse(_context.LoginID.ToString()),
                CreatedBy = int.Parse(_context.LoginID.ToString()),
            };
        }

        public List<ClaimLoginMap> FromClaimDetailDTO(List<Eduegate.Services.Contracts.Admin.ClaimDetailDTO> dtos, long loginID)
        {
            var entity = new List<ClaimLoginMap>();
            if (dtos != null)
            {
                foreach (var dto in dtos)
                {
                    entity.Add(FromClaimDetailDTO(dto, loginID));
                }
            }

            return entity;
        }
    }
}
