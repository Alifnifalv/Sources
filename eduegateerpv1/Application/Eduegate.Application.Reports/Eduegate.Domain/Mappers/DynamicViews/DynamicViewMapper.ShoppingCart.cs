using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers.DynamicViews
{
    public partial class DynamicViewMapper
    {
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetShoppingCartDetails(ShoppingCart cart)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (cart.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ShoppingCartIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CartID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PaymentMethod" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CartStatusID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CartStatus" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedBy" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Description" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PaymentGateWayID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Message" });

                var cartStatus = new ShoppingCartRepository().GetCartStatus((int)cart.CartStatusID);
                
                
                     var LoginEmail = new ShoppingCartRepository().GetShoppingCartLogin(cart.ShoppingCartIID);
               

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          cart.ShoppingCartIID, cart.CartID, cart.PaymentMethod,cartStatus.StatusID, cartStatus.StatusName,cart.CreatedBy.IsNotNull() ? LoginEmail.LoginEmailID : null,cart.CreatedDate,cart.UpdatedDate,cart.Description!=null?cart.Description:string.Empty,cart.PaymentGateWayID.HasValue?cart.PaymentGateWayID.Value:0,""
                        }
                });
            }


            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetShoppingCartItems(List<ShoppingCartItem> cartitems)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (cartitems.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SKU" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Quantity" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductPrice" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductDiscountPrice" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Barcode" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PartNo" });

                foreach (var item in cartitems) 
                {
                    var sku = new ProductDetailRepository().GetProductSkuDetails((long)item.ProductSKUMapID);
                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO() 
                    {

                       DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          sku.SKUName,item.Quantity,item.ProductPrice,item.ProductDiscountPrice,sku.BarCode,sku.PartNo
                        }
                    });
                }
            }
            return searchDTO;
        }
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetShoppingCartDescription(ShoppingCart cart)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (cart.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ShoppingCartIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Description" });
                var cartStatus = new ShoppingCartRepository().GetCartStatus((int)cart.CartStatusID);

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          cart.ShoppingCartIID, cart.CartID, cart.PaymentMethod,cartStatus.StatusName,cart.CreatedDate,cart.UpdatedDate
                        }
                });
            }


            return searchDTO;
        }
    }
}
