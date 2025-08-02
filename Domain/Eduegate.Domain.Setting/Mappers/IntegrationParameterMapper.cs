using Eduegate.Domain.Entity.Setting.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Settings;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Setting.Mappers
{
    public class IntegrationParameterMapper : IDTOEntityMapper<IntegrationParamterDTO, IntegrationParameter>
    {
        private CallContext _callContext;

        public static IntegrationParameterMapper Mapper(CallContext _context = null)
        {
            var mapper = new IntegrationParameterMapper();
            mapper._callContext = _context;
            return mapper;
        }

        public List<IntegrationParamterDTO> ToDTO(List<IntegrationParameter> entities)
        {
            var dtos = new List<IntegrationParamterDTO>();

            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public IntegrationParamterDTO ToDTO(IntegrationParameter entity)
        {
            return new IntegrationParamterDTO()
            {
                IntegrationParameterId = entity.IntegrationParameterId,
                ParameterDataType = entity.ParameterDataType,
                ParameterName = entity.ParameterName,
                ParameterType = entity.ParameterType,
                ParameterValue = entity.ParameterValue
            };
        }

        public IntegrationParameter ToEntity(IntegrationParamterDTO dto)
        {
            return new IntegrationParameter()
            {
                IntegrationParameterId = dto.IntegrationParameterId,
                ParameterDataType = dto.ParameterDataType,
                ParameterName = dto.ParameterName,
                ParameterType = dto.ParameterType,
                ParameterValue = dto.ParameterValue
            };
        }
    }
}
