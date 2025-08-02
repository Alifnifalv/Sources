using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Framework.Contracts.Common.PageRender;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.PageRender;

namespace Eduegate.Domain.Mappers.PageRender
{
    public class PageMapper : IDTOEntityMapper<PageDTO, Page>
    {
        private CallContext _context;
        public static PageMapper Mapper(CallContext context)
        {
            var mapper = new PageMapper();
            mapper._context = context;
            return mapper;
        }

        public PageDTO ToDTO(Page page, long? referenceID)
        {
            var pageDTO = new PageDTO();
            pageDTO.PageBoilerPlateMaps = new List<PageBoilerPlateMapDTO>();

            if (page.IsNotNull())
            {
                pageDTO.MasterPageID = page.MasterPageID;
                pageDTO.PageID = page.PageID;
                pageDTO.PageName = page.PageName;
                pageDTO.PageTypeID = page.PageTypeID;
                pageDTO.ParentPageID = page.ParentPageID;
                pageDTO.PlaceHolder = page.PlaceHolder;
                pageDTO.SiteID = page.SiteID;
                pageDTO.TemplateName = page.TemplateName;
                pageDTO.Title = page.Title;
                pageDTO.CreatedBy = page.CreatedBy;
                pageDTO.UpdatedBy = page.UpdatedBy;      
                pageDTO.CreatedDate = page.CreatedDate;
                pageDTO.UpdatedDate = page.UpdatedDate;
                pageDTO.IsCache = page.IsCache;
                //pageDTO.TimeStamps = Convert.ToBase64String(page.TimeStamps);
                pageDTO.BoilerPlates = new List<BoilerPlateDTO>();
                pageDTO.CompanyID = page.CompanyID.IsNotNull()? page.CompanyID : _context.CompanyID;

                // Map ReferenceID at page level if page has boilerplates
                pageDTO.ReferenceID = referenceID;

                if (page.PageBoilerplateMaps.IsNotNull() && page.PageBoilerplateMaps.Count > 0)
                {
                    var pageMapper = PageBoilerPlateMapMapper.Mapper(_context);

                    foreach(var pbMap in page.PageBoilerplateMaps)
                    {
                        pageDTO.PageBoilerPlateMaps.Add(pageMapper.ToDTO(pbMap));
                    }
                }

                var mapper = Mappers.PageRender.BoilerPlateMapper.Mapper(_context);

                foreach (var map in page.PageBoilerplateMaps.OrderBy(a => a.SerialNumber).ToList())
                {
                    var boilerplateDto = mapper.ToDTO(map.BoilerPlate);
                    boilerplateDto.BoilerplateMapIID = map.PageBoilerplateMapIID;
                    boilerplateDto.ReferenceID = map.ReferenceID;
                    boilerplateDto.RuntimeParameters = mapper.ToParameterDTO(map.PageBoilerplateMapParameters.ToList());
                    pageDTO.BoilerPlates.Add(boilerplateDto);
                }
            }

            return pageDTO;
        }

        public Page ToEntity(PageDTO dto)
        {
            Page page = new Page();
            page.PageBoilerplateMaps = new List<PageBoilerplateMap>();

            if (dto.IsNotNull())
            {
                page.PageID = dto.PageID;
                page.SiteID = dto.SiteID;
                page.PageName = dto.PageName;
                page.PageTypeID = dto.PageTypeID;
                page.Title = dto.Title;
                page.TemplateName = dto.TemplateName;
                page.PlaceHolder = dto.PlaceHolder;
                page.ParentPageID = dto.ParentPageID;
                page.MasterPageID = dto.MasterPageID;
                page.CreatedBy = dto.PageID > 0 ? dto.CreatedBy : (int)_context.LoginID;
                page.UpdatedBy = dto.PageID > 0 ? (int)_context.LoginID : dto.UpdatedBy;
                page.CreatedDate = dto.PageID > 0 ? dto.CreatedDate : DateTime.Now;
                page.UpdatedDate = dto.PageID > 0 ? DateTime.Now : dto.UpdatedDate;
                //page.TimeStamps = dto.TimeStamps.IsNotNull() ? Convert.FromBase64String(dto.TimeStamps) : null;
                page.IsCache = dto.IsCache;
                if(dto.PageBoilerPlateMaps.IsNotNull() && dto.PageBoilerPlateMaps.Count > 0)
                {
                    var mapper = PageBoilerPlateMapMapper.Mapper(_context);
                    foreach (var pageBPMap in dto.PageBoilerPlateMaps)
                    {
                        page.PageBoilerplateMaps.Add(mapper.ToEntity(pageBPMap));
                    }
                }
                page.CompanyID = dto.CompanyID.IsNotNull() ? dto.CompanyID : _context.CompanyID;
            }

            return page;
        }

        public PageDTO ToDTO(Page entity)
        {
            throw new NotImplementedException();
        }
    }
}
