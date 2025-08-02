using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Web.Library.ViewModels;

namespace Eduegate.Web.Library
{
    public partial class SalesRelationshipTypeViewModel
    {
        public byte SalesRelationTypeID { get; set; }
        public string RelationName { get; set; }
        public List<ProductMapViewModel> ToProduct { get; set; }


        public static SalesRelationshipTypeViewModel FromDTO(SalesRelationshipTypeDTO dto)
        {
            return new SalesRelationshipTypeViewModel()
            {
                SalesRelationTypeID = dto.SalesRelationTypeID,
                RelationName = dto.RelationName,
                ToProduct = dto.ToProducts == null ? null : dto.ToProducts.Select(x => ProductMapViewModel.FromDTO(x)).ToList()
            };
        }


        public static SalesRelationshipTypeDTO ToDTO(SalesRelationshipTypeViewModel obj)
        {
            return new SalesRelationshipTypeDTO()
            {
                SalesRelationTypeID = obj.SalesRelationTypeID,
                RelationName = obj.RelationName,
                ToProducts = obj.ToProduct == null ? null : obj.ToProduct.Select(x => x == null ? null : ProductMapViewModel.ToDTO(x)).ToList()
            };
        }
    }


}
