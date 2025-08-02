using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Interfaces;
using Eduegate.Framework.Contracts.Common.PageRender;
using Eduegate.Framework.Extensions;
using Eduegate.Framework;

namespace Eduegate.Domain.Mappers.PageRender
{
    public class PageBoilerPlateMapMapper : IDTOEntityMapper<PageBoilerPlateMapDTO, PageBoilerplateMap>
    {
        private CallContext _context;

        public static PageBoilerPlateMapMapper Mapper(CallContext context)
        {
            var mapper = new PageBoilerPlateMapMapper();
            mapper._context = context;
            return mapper;
        }

        public PageBoilerPlateMapDTO ToDTO(PageBoilerplateMap entity)
        {
            if (entity.IsNotNull())
            {
                var pbpmDTO = new PageBoilerPlateMapDTO();
                pbpmDTO.PageBoilerPlateMapParameters = new List<PageBoilerPlateMapParameterDTO>();

                if (entity.BoilerPlate.IsNotNull())
                {
                    pbpmDTO.BoilerplateID = entity.BoilerPlate.BoilerPlateID;
                    pbpmDTO.Description = entity.BoilerPlate.Description;
                    pbpmDTO.Name = entity.BoilerPlate.Name;
                }

                pbpmDTO.PageBoilerplateMapIID = entity.PageBoilerplateMapIID;
                pbpmDTO.PageID = Convert.ToInt32(entity.PageID);
                pbpmDTO.ReferenceID = entity.ReferenceID;
                pbpmDTO.SerialNumber = Convert.ToInt16(entity.SerialNumber);
                pbpmDTO.CreatedBy = entity.CreatedBy;
                pbpmDTO.UpdatedBy = entity.UpdatedBy;
                pbpmDTO.CreatedDate = entity.CreatedDate;
                pbpmDTO.UpdatedDate = entity.UpdatedDate;
                //pbpmDTO.TimeStamps = Convert.ToBase64String(entity.TimeStamps);

                if (entity.PageBoilerplateMapParameters.IsNotNull() && entity.PageBoilerplateMapParameters.Count > 0)
                {
                    var mapper = PageBoilerPlateMapParameterMapper.Mapper(_context);

                    foreach (var pbmParameter in entity.PageBoilerplateMapParameters)
                    {
                        var dto = mapper.ToDTO(pbmParameter);
                        var parameter = entity.BoilerPlate.BoilerPlateParameters.Where(a => a.ParameterName == pbmParameter.ParameterName).FirstOrDefault();
                        if(parameter != null)
                            dto.Description = parameter.Description;

                        pbpmDTO.PageBoilerPlateMapParameters.Add(dto);
                    }

                    //add newly added parameter (missing ones)
                    foreach (var masterParameter in entity.BoilerPlate.BoilerPlateParameters)
                    {
                        var parameter = pbpmDTO.PageBoilerPlateMapParameters.Where(a => a.ParameterName == masterParameter.ParameterName).FirstOrDefault();

                        if (parameter == null)
                        {
                            pbpmDTO.PageBoilerPlateMapParameters.Add(new PageBoilerPlateMapParameterDTO()
                            {
                                Description = masterParameter.Description,
                                ParameterName = masterParameter.ParameterName,
                                ParameterValue = string.Empty,
                            });
                        }
                    }
                }

                return pbpmDTO;
            }
            else
            {
                return new PageBoilerPlateMapDTO();
            }
        }

        public PageBoilerplateMap ToEntity(PageBoilerPlateMapDTO dto)
        {
            
            if (dto.IsNotNull())
            {
                PageBoilerplateMap pbMap = new PageBoilerplateMap();
                pbMap.PageBoilerplateMapParameters = new List<PageBoilerplateMapParameter>();
                pbMap.PageBoilerplateMapIID = dto.PageBoilerplateMapIID;
                pbMap.BoilerplateID = dto.BoilerplateID;
                pbMap.PageID = dto.PageID;
                pbMap.ReferenceID = dto.ReferenceID;
                pbMap.SerialNumber = dto.SerialNumber;
                pbMap.CreatedBy = dto.PageBoilerplateMapIID > 0 ? dto.CreatedBy : (int)_context.LoginID;
                pbMap.UpdatedBy = dto.PageBoilerplateMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy;
                pbMap.CreatedDate = dto.PageBoilerplateMapIID > 0 ? dto.CreatedDate : DateTime.Now;
                pbMap.UpdatedDate = dto.PageBoilerplateMapIID > 0 ? DateTime.Now : dto.UpdatedDate;
                //pbMap.TimeStamps = dto.TimeStamps.IsNotNull() ? Convert.FromBase64String(dto.TimeStamps) : null;

                if (dto.PageBoilerPlateMapParameters.IsNotNull() && dto.PageBoilerPlateMapParameters.Count > 0)
                {
                    var mapper = PageBoilerPlateMapParameterMapper.Mapper(_context);

                    foreach (var pbmParameter in dto.PageBoilerPlateMapParameters)
                    {
                        pbMap.PageBoilerplateMapParameters.Add(mapper.ToEntity(pbmParameter));
                    }
                }

                return pbMap;
            }
            else
            {
                return new PageBoilerplateMap();
            }
        }

    }
}
