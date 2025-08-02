using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Web.Library.ViewModels
{
    public class ProductStatusViewModel
    {
        public int CultureID { get; set; }
        public long StatusIID { get; set; }
        public string StatusName { get; set; }

        public static List<ProductStatusViewModel> ToViewModel(List<ProductStatusDTO> dtos)
        {
            var vms = new List<ProductStatusViewModel>();

            foreach (var dto in dtos)
            {
                vms.Add(new ProductStatusViewModel()
                {
                    CultureID = dto.CultureID,
                    StatusIID = dto.StatusIID,
                    StatusName = dto.StatusName
                });
            }

            return vms;
        }
        public static ProductStatusViewModel ToViewModel(ProductStatusDTO dto)
        {
            return new ProductStatusViewModel()
            {
                CultureID = dto.CultureID,
                StatusIID = dto.StatusIID,
                StatusName = dto.StatusName
            };
        }
    }
}