using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Distributions;

namespace Eduegate.Domain.Mappers.Distributions
{
    public class ServiceProviderSettingMapper : IDTOEntityMapper<ServiceProviderDTO, ServiceProviderSetting>
    {
        private CallContext _context;

        public static ServiceProviderSettingMapper Mapper(CallContext context)
        {
            var mapper = new ServiceProviderSettingMapper();
            mapper._context = context;
            return mapper;
        }

        public ServiceProviderDTO ToDTO(ServiceProviderSetting entity)
        {
            throw new NotImplementedException();
        }

        public ServiceProviderSetting ToEntity(ServiceProviderDTO dto)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> ToKeyValueDTO(List<ServiceProviderSetting> values)
        {
            var dtos = new List<KeyValueDTO>();

            foreach (var value in values)
            {
                dtos.Add(ToKeyValueDTO(value));
            }

            return dtos;
        }

        public KeyValueDTO ToKeyValueDTO(ServiceProviderSetting value)
        {
            return new KeyValueDTO() { Key = value.SettingCode, Value = value.SettingValue };
        }
    }
}
