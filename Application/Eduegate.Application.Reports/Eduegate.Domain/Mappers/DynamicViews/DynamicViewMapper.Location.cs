using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers.DynamicViews
{
    public partial class DynamicViewMapper
    {
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetLocation(LocationDTO location)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (location.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "LocationID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          location.LocationIID , location.CreatedDate, location.UpdatedDate                     
                        }
                });
            }


            return searchDTO;
        }
    }
}
