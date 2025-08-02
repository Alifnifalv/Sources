using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers.DynamicViews
{
    public partial class DynamicViewMapper
    {
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetModifyShoppingCarts(Eduegate.Domain.Entity.Models.ShoppingCart cart)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (cart.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CartID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CustomerID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CartStatus" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          cart.ShoppingCartIID , cart.CartID, ((Eduegate.Framework.Helper.Enums.ShoppingCartStatus) cart.CartStatusID).ToString()                    
                        }
                });
            }


            return searchDTO;
        }
    }
}
