using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Domain.Mappers
{
    public class EntitlementMapMapper : IDTOEntityMapper<EntitlementMapDTO, EntitlementMap>
    {
        public static EntitlementMapMapper Mapper { get { return new EntitlementMapMapper(); } }

        public EntitlementMapDTO ToDTO(EntitlementMap entity)
        {
            if (entity != null)
            {
                return new EntitlementMapDTO()
                {
                    EntitlementMapID = entity.EntitlementMapIID,
                    ReferenceID = entity.ReferenceID,
                    IsLocked = entity.IsLocked,
                    EntitlementAmount = entity.EntitlementAmount,
                    EntitlementDays = entity.EntitlementDays,
                    EntitlementID = entity.EntitlementID,
                    EntitlementName = entity.EntityTypeEntitlement != null ? entity.EntityTypeEntitlement.EntitlementName : null
                };
            }
            else
            {
                return new EntitlementMapDTO();
            }
        }


        public EntitlementMap ToEntity(EntitlementMapDTO dto)
        {
            if (dto != null)
            {
                return new EntitlementMap()
                {
                    EntitlementMapIID = dto.EntitlementMapID,
                    ReferenceID = dto.ReferenceID,
                    IsLocked = dto.IsLocked,
                    EntitlementAmount = dto.EntitlementAmount,
                    EntitlementDays = dto.EntitlementDays,
                    EntitlementID = dto.EntitlementID,
                };
            }
            else
            {
                return new EntitlementMap();
            }
        }
    }
}
