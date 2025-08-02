using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Framework.Contracts.Common.PageRender;

namespace Eduegate.Domain.Mappers.PageRender
{
    public class BoilerPlateParameterMapper : IDTOEntityMapper<BoilerPlateParameterDTO, BoilerPlateParameter>
    {
        private CallContext _context;
        public static BoilerPlateParameterMapper Mapper(CallContext context)
        {
            var mapper = new BoilerPlateParameterMapper();
            mapper._context = context;
            return mapper;
        }

        public BoilerPlateParameterDTO ToDTO(BoilerPlateParameter boilerPlate)
        {
            return new BoilerPlateParameterDTO
            {
                BoilerPlateMapID = Convert.ToInt32(boilerPlate.BoilerPlateID),
                BoilerPlateMapParameterIID = boilerPlate.BoilerPlateParameterID,
                ParameterName = boilerPlate.ParameterName,
                Description = boilerPlate.Description,
            };
        }

        public BoilerPlateParameter ToEntity(BoilerPlateParameterDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
