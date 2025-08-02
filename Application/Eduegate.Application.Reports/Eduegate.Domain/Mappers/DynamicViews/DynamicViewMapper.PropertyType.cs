using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers.DynamicViews
{
    public partial class DynamicViewMapper
    {
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetPropertyType(PropertyType proptype,List<Property> prop)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (proptype != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PropertyTypeID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PropertyTypeName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PropertyName" }); 
                              
                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                    {
                      proptype.PropertyTypeID,proptype.PropertyTypeName,
                       proptype.PropertyTypeID.IsNotNull()? string.Join(",", prop.Select(x=>x.PropertyName)):""
                    }
                });
            }
            return searchDTO;
      }
   }
}