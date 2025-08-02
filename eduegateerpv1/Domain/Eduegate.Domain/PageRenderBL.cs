using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Mappers.PageRender;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common.PageRender;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.School.Mutual;

namespace Eduegate.Domain
{
    public class PageRenderBL
    {
        private CallContext _callContext;

        public PageRenderBL(CallContext context)
        {
            _callContext = context;
        }

        public PageDTO GetPageInfo(long pageID, string parameter)
        {
            long? referenceID = null;
            var parameters = parameter != null ? parameter.Split(',').ToList() : null;
            var referenceParameter = parameters != null ? parameters.FirstOrDefault(a => a.ToLower().Contains("referenceid")) : null;

            if (referenceParameter != null)
            {
                referenceID = long.Parse(referenceParameter.Split('=')[1]);
            }

            var pageInfo = new PageRenderRepository().GetPageInfo(pageID, referenceID, _callContext.CompanyID.HasValue? (int)_callContext.CompanyID.Value : default(int));
            return Mappers.PageRender.PageMapper.Mapper(_callContext).ToDTO(pageInfo, referenceID);
        }

        public List<SiteDTO> GetSites()
        {
            var mapper = Mappers.PageRender.SiteMapper.Mapper(_callContext);
            var sites = new List<SiteDTO>();
            foreach (var site in new PageRenderRepository().GetSites())
            {
                sites.Add(mapper.ToDTO(site));
            }
            return sites;
        }

        public List<PageTypeDTO> GetPageTypes()
        {
            var mapper = Mappers.PageRender.PageTypeMapper.Mapper(_callContext);
            var pageTypes = new List<PageTypeDTO>();
            foreach (var pageType in new PageRenderRepository().GetPageTypes())
            {
                pageTypes.Add(mapper.ToDTO(pageType));
            }
            return pageTypes;
        }

        public PageDTO SavePage(PageDTO dto)
        {
            var pageDTO = new PageDTO();
            var mapper = Mappers.PageRender.PageMapper.Mapper(_callContext);
            return mapper.ToDTO(new PageRenderRepository().SavePage(mapper.ToEntity(dto), dto.ReferenceID), dto.ReferenceID);
        }

        public PowerBiDashBoardDTO GetPowerBIDataUsingPageID(long? pageID)
        {
            var result = new PageRenderRepository().GetPowerBIDataUsingPageID(pageID);
            return result;
        }
    }
}
