using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "CustomerGroup", "CRUDModel.ViewModel")]
    [DisplayName("Details")]
    public class CustomerGroupViewModel : BaseMasterViewModel
    {
        public CustomerGroupViewModel()
        {
            PriceListsMap = new PirceListsMap();
        }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Group ID")]
        public long CustomerGroupIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Group No")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string GroupName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Point Limit")]
        public Nullable<decimal> PointLimit { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "PriceList", "PriceListsMap")]
        [MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        [DisplayName("Price List")]
        public PirceListsMap PriceListsMap { get; set; }

        public List<ProductSKUPriceCustomerGroupViewModel> CustomerGroupPrices { get; set; }

        public static CustomerGroupViewModel FromDTO(CustomerGroupDTO dto)
        {
            return new CustomerGroupViewModel()
            {
                GroupName = dto.GroupName,
                PointLimit = dto.PointLimit,
                CustomerGroupIID = dto.CustomerGroupIID
            };
        }

        public static CustomerGroupDTO ToDTO(CustomerGroupViewModel vm)
        {
            return new CustomerGroupDTO()
            {
                CustomerGroupIID = vm.CustomerGroupIID,
                GroupName = vm.GroupName,
                PointLimit = vm.PointLimit
            };
        }
    }
}