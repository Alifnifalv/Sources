using Eduegate.Domain.Entity.Models.Settings;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Frameworks.Enums;
using Eduegate.Services.Contracts.Metadata;
using System;
using System.Collections.Generic;


namespace Eduegate.Domain.Mappers.Mutual
{
    public class ScreenShortCutMapper : IDTOEntityMapper<ScreenShortCutDTO, ScreenShortCut>
    {
        private CallContext _context;

        public static ScreenShortCutMapper Mapper(CallContext context)
        {
            var mapper = new ScreenShortCutMapper();
            mapper._context = context;
            return mapper;
        }

        public List<ScreenShortCutDTO> ToDTO(List<ScreenShortCut> entities)
        {
            var dtos = new List<ScreenShortCutDTO>();

            foreach(var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public ScreenShortCutDTO ToDTO(ScreenShortCut entity)
        {
            return new ScreenShortCutDTO()
            {
                Action = entity.Action,
                KeyCode = entity.KeyCode,
                ScreenID = entity.ScreenID,
                ScreenShortCutID = entity.ScreenShortCutID,
                ShortCutKey = entity.ShortCutKey,
            };
        }

        public ScreenShortCut ToEntity(ScreenShortCutDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
