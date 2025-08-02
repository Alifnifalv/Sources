using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Contracts.Common.PageRender;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Framework;

namespace Eduegate.Domain.Mappers.PageRender
{
    public class PageBoilerPlateMapParameterMapper : IDTOEntityMapper<PageBoilerPlateMapParameterDTO, PageBoilerplateMapParameter>
    {
        private CallContext _context;

        public static PageBoilerPlateMapParameterMapper Mapper(CallContext context)
        {
            var mapper = new PageBoilerPlateMapParameterMapper();
            mapper._context = context;
            return mapper;
        }

        public PageBoilerPlateMapParameterDTO ToDTO(PageBoilerplateMapParameter entity)
        {
            if (entity.IsNotNull())
            {
                var dto = new PageBoilerPlateMapParameterDTO();

                dto.PageBoilerplateMapParameterIID = entity.PageBoilerplateMapParameterIID;
                dto.PageBoilerplateMapID = entity.PageBoilerplateMapID;
                dto.ParameterName = entity.ParameterName;
                dto.ParameterValue = entity.ParameterValue;
                dto.CreatedBy = entity.CreatedBy;
                dto.UpdatedBy = entity.UpdatedBy;
                dto.CreatedDate = entity.CreatedDate;
                dto.UpdatedDate = entity.UpdatedDate;
                //dto.TimeStamps = Convert.ToBase64String(entity.TimeStamps);

                return dto;
            }
            else
            {
                return new PageBoilerPlateMapParameterDTO();
            }
        }

        public PageBoilerplateMapParameter ToEntity(PageBoilerPlateMapParameterDTO dto)
        {
            if (dto.IsNotNull())
            {
                var entity = new PageBoilerplateMapParameter();

                entity.PageBoilerplateMapParameterIID = dto.PageBoilerplateMapParameterIID;
                entity.PageBoilerplateMapID = dto.PageBoilerplateMapID;
                entity.ParameterName = dto.ParameterName;
                entity.ParameterValue = dto.ParameterValue;
                entity.CreatedBy = dto.PageBoilerplateMapParameterIID > 0 ? dto.CreatedBy : (int)_context.LoginID;
                entity.UpdatedBy = dto.PageBoilerplateMapParameterIID > 0 ? (int)_context.LoginID : dto.UpdatedBy;
                entity.CreatedDate = dto.PageBoilerplateMapParameterIID > 0 ? dto.CreatedDate : DateTime.Now;
                entity.UpdatedDate = dto.PageBoilerplateMapParameterIID > 0 ? DateTime.Now : dto.UpdatedDate;
                //entity.TimeStamps = dto.TimeStamps.IsNotNull() ? Convert.FromBase64String(dto.TimeStamps) : null;

                return entity;
            }
            else
            {
                return new PageBoilerplateMapParameter();
            }
        }

    }
}
