using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.PageRender;

namespace Eduegate.Domain.Mappers.PageRender
{
    public class BoilerPlateMapper : IDTOEntityMapper<BoilerPlateDTO, BoilerPlate>
    {
        private CallContext _context;
        public static BoilerPlateMapper Mapper(CallContext context)
        {
            var mapper = new BoilerPlateMapper();
            mapper._context = context;
            return mapper;
        }

        public BoilerPlateDTO ToDTO(BoilerPlate boilerPlate)
        {
            return new BoilerPlateDTO
            {
                BoilerPlateID = boilerPlate.BoilerPlateID,
                Description = boilerPlate.Description,
                Name = boilerPlate.Name,
                Template = boilerPlate.Template,
                ReferenceIDName = boilerPlate.ReferenceIDName,
                ReferenceIDRequired = boilerPlate.ReferenceIDRequired,
                //TimeStamps = Convert.ToBase64String(boilerPlate.TimeStamps),
                BoilerPlateParameters = ToBoilerPlateParameterDTO(boilerPlate.BoilerPlateParameters.ToList())
            };
        }

        public BoilerPlate ToEntity(BoilerPlateDTO dto, CallContext callContext)
        {
            var entity = ToEntity(dto);

            if (dto.BoilerPlateID == 0)
            {
                entity.CreatedBy = int.Parse(callContext.LoginID.ToString());
                entity.CreatedDate = DateTime.Now;
            }
            return entity;
        }

        public BoilerPlate ToEntity(BoilerPlateDTO dto)
        {
            var entity = new BoilerPlate()
            {
                BoilerPlateID = dto.BoilerPlateID,
                Description = dto.Description,
                Name = dto.Name,
                Template = dto.Template,
                //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
            };
            return entity;
        }

        public List<KeyValueDTO> ToParameterDTO(List<PageBoilerplateMapParameter> parameters)
        {
            var parameterDtos = new List<KeyValueDTO>();

            foreach (var parameter in parameters)
            {
                parameterDtos.Add(ToParameterDTO(parameter));
            }

            return parameterDtos;
        }

        public KeyValueDTO ToParameterDTO(PageBoilerplateMapParameter parameter)
        {
            return new KeyValueDTO()
            {
                Key = parameter.ParameterName,
                Value = parameter.ParameterValue
            };
        }

        public List<BoilerPlateParameterDTO> ToBoilerPlateParameterDTO(List<BoilerPlateParameter> parameters)
        {
            var parameterDTOs = new List<BoilerPlateParameterDTO>();
            foreach(var parameter in parameters)
            {
                parameterDTOs.Add(ToBoilerPlateParameterDTO(parameter));
            }
            return parameterDTOs;
        }
        public BoilerPlateParameterDTO ToBoilerPlateParameterDTO(BoilerPlateParameter parameter)
        {
            return new BoilerPlateParameterDTO()
            {
                ParameterName = parameter.ParameterName,
                BoilerPlateMapID = Convert.ToInt32(parameter.BoilerPlateID),
                BoilerPlateMapParameterIID = parameter.BoilerPlateParameterID,
                Description = parameter.Description,
            };
        }
    }
}
