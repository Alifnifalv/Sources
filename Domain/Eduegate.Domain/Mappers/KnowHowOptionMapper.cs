using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain.Mappers
{
    public class KnowHowOptionMapper : IDTOEntityMapper<KnowHowOptionDTO, KnowHowOption>
    {
        private CallContext _context;

        public static KnowHowOptionMapper Mapper(CallContext context)
        {
            var mapper = new KnowHowOptionMapper();
            mapper._context = context;
            return mapper;
        }

        public KnowHowOptionDTO ToDTO(KnowHowOption entity)
        {
            if (entity != null)
            {
                return new KnowHowOptionDTO()
                {
                    KnowHowOptionIID = entity.KnowHowOptionIID,
                    KnowHowOptionText = entity.KnowHowOptionText,
                    IsEditable = entity.IsEditable
                };
            }
            else return new KnowHowOptionDTO();
        }
        public KnowHowOption ToEntity(KnowHowOptionDTO dto)
        {
            throw new NotImplementedException();
        }

    }
}
