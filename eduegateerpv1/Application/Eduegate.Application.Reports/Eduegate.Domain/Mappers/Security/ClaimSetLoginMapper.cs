using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Security;

namespace Eduegate.Domain.Mappers.Security
{
    public class ClaimSetLoginMapper : IDTOEntityMapper<ClaimSetDTO, Eduegate.Domain.Entity.Models.ClaimSetLoginMap>
    {
        private CallContext _context;

        public static ClaimSetLoginMapper Mapper(CallContext context)
        {
            var mapper = new ClaimSetLoginMapper();
            mapper._context = context;
            return mapper;

        }
        public ClaimSetDTO ToDTO(Entity.Models.ClaimSetLoginMap entity)
        {
            throw new NotImplementedException();
        }

        public Entity.Models.ClaimSetLoginMap ToEntity(ClaimSetDTO dto)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> ToKeyValueDTO(List<Entity.Models.ClaimSetLoginMap> entities)
        {
            var dto = new List<KeyValueDTO>();

            foreach (var entity in entities)
            {
                dto.Add(ToKeyValueDTO(entity));
            }

            return dto;
        }

        public KeyValueDTO ToKeyValueDTO(Entity.Models.ClaimSetLoginMap entity)
        {
            return new KeyValueDTO() { Key = entity.ClaimSetID.ToString(), Value = (entity.ClaimSet == null ? null : entity.ClaimSet.ClaimSetName) };
        }

        public List<ClaimSetLoginMap> FromKeyValueDTO(List<KeyValueDTO> dtos, long loginID)
        {
            var entity = new List<ClaimSetLoginMap>();

            if (dtos != null)
            {
                foreach (var dto in dtos)
                {
                    entity.Add(FromKeyValueDTO(dto, loginID));
                }
            }

            return entity;
        }

        public ClaimSetLoginMap FromKeyValueDTO(KeyValueDTO dto, long loginID)
        {
            return new ClaimSetLoginMap()
            {
                ClaimSetID = long.Parse(dto.Key),
                LoginID = loginID,
            };
        }
    }
}
