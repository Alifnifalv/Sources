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
    public class PageTypeMapper : IDTOEntityMapper<PageTypeDTO, PageType>
    {
        private CallContext _context;
        public static PageTypeMapper Mapper(CallContext context)
        {
            var mapper = new PageTypeMapper();
            mapper._context = context;
            return mapper;
        }
        public PageTypeDTO ToDTO(PageType pageType)
        {
            return new PageTypeDTO
            {
                PageTypeID = pageType.PageTypeID,
                TypeName = pageType.TypeName,
                Pages = new List<PageDTO>()
            };
        }

        public PageType ToEntity(PageTypeDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
