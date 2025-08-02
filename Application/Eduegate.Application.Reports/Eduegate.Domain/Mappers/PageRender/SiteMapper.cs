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
    public class SiteMapper : IDTOEntityMapper<SiteDTO, Site>
    {
        private CallContext _context;
        public static SiteMapper Mapper(CallContext context)
        {
            var mapper = new SiteMapper();
            mapper._context = context;
            return mapper;
        }

        public SiteDTO ToDTO(Site site)
        {
            return new SiteDTO
            {
                SiteID=  site.SiteID,
                SiteName = site.SiteName,
                MasterPageID = site.MasterPageID,
                HomePageID = site.HomePageID,
                Pages = new List<PageDTO>()
            };
        }

        public Site ToEntity(SiteDTO dto)
        {
            throw new NotImplementedException();
        }

    }
}
