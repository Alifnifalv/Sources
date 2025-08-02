using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Domain.Mappers.DynamicViews
{
    public partial class DynamicViewMapper
    {
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetPageDetails(Page page)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (page != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PageID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PageName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PageTypeID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Title" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TemplateName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PlaceHolder" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ParentPageID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "MasterPageID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedBy" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedBy" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PageType" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ParentPage" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Site" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          page.PageID, page.PageName,page.PageTypeID, page.Title, page.TemplateName, page.PlaceHolder,
                          page.ParentPageID, page.MasterPageID,  page.CreatedDate, page.UpdatedDate, page.CreatedBy, page.UpdatedBy,
                          page.PageType.TypeName, page.Page1 == null ? string.Empty :  page.Page1.PageName, page.Site == null ? string.Empty : page.Site.SiteName
                        }
                });
            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetPageBoilerplateDetails(BoilerPlate boilerplates)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (boilerplates != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BoilerPlateID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Name" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Description" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Template" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ReferenceIDName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedBy" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedBy" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                   DataCells = new Services.Contracts.Commons.DataCellListDTO()
                    {
                       boilerplates.BoilerPlateID, boilerplates.Name,boilerplates.Description,
                       boilerplates.Template, boilerplates.ReferenceIDName, boilerplates.CreatedDate, boilerplates.UpdatedDate,
                       boilerplates.CreatedBy, boilerplates.UpdatedBy
                    }
               });
            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetNewsDetails(News news)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO(); 

            if (news != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "NewsIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Title" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });


                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                    {
                      news.NewsIID,news.Title,news.CreatedDate,news.UpdatedDate
                      
                    }
                });
            }

            return searchDTO;
        }
    }
}
