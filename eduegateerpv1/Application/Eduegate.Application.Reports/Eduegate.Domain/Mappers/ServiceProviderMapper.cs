using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Interfaces;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;

namespace Eduegate.Domain.Mappers
{
    public class ServiceProviderMapper : IDTOEntityMapper<ServiceProviderDTO, ServiceProvider>
    {
        private CallContext _context;

        public static ServiceProviderMapper Mapper(CallContext context)
        {
            var mapper = new ServiceProviderMapper();
            mapper._context = context;
            return mapper;
        }

        public ServiceProvider ToEntity(ServiceProviderDTO dto)
        {
            if (dto.IsNotNull())
            {
                var sp = new ServiceProvider()
                {
                    ServiceProviderID = dto.ServiceProviderID,
                    ProviderCode = dto.ProviderCode,
                    ProviderName = dto.ProviderName,
                    CountryID = dto.CountryID,
                    IsActive = dto.IsActive,
                    CreatedBy = dto.CreatedBy.IsNotNull() ? dto.CreatedBy : (int)_context.LoginID,
                    CreatedDate = dto.CreatedDate.IsNotNull() ? dto.CreatedDate : DateTime.Now,
                    UpdatedBy = (int)_context.LoginID,
                    UpdatedDate = DateTime.Now,
                    TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                    ServiceProviderLink = dto.ServiceProviderLink
                };

                return sp;
            }
            else
            {
                return new ServiceProvider();
            }
        }

        public ServiceProviderDTO ToDTO(ServiceProvider entity)
        {
            if (entity.IsNotNull())
            {
                var dto = new ServiceProviderDTO()
                {
                    ServiceProviderID = entity.ServiceProviderID,
                    ProviderCode = entity.ProviderCode,
                    ProviderName = entity.ProviderName,
                    CountryID = entity.CountryID,
                    IsActive = entity.IsActive,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                    TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                    ServiceProviderLink = entity.ServiceProviderLink,
                };

                return dto;
            }
            else
            {
                return new ServiceProviderDTO();
            }
        }

    }
}
