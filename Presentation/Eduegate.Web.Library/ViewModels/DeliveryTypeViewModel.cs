using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Web.Library.ViewModels
{
    public partial class DeliveryTypeViewModel : BaseMasterViewModel
    {
        public DeliveryTypeViewModel()
        {
            DeliveryDetails = new List<ProductDeliveryTypeViewModel>();
            ZoneDeliveryDetails = new List<ZoneDeliveryTypeViewModel>();
            AreaDeliveryDetails = new List<AreaDeliveryTypeViewModel>();
            CustomerGroupDeliveryDetails = new List<CustomerGroupDeliveryTypeViewModel>();
        }

        // adding this property to handle ZoneDeliveryTypeViewModel
        public Nullable<bool> IsDeliveryAvailable { get; set; }

        public short DeliveryTypeID { get; set; }
        public string DeliveryMethod { get; set; }
        public string Description { get; set; }
        public List<ProductDeliveryTypeViewModel> DeliveryDetails { get; set; }
        public List<ZoneDeliveryTypeViewModel> ZoneDeliveryDetails { get; set; }
        public List<AreaDeliveryTypeViewModel> AreaDeliveryDetails { get; set; }
        public List<CustomerGroupDeliveryTypeViewModel> CustomerGroupDeliveryDetails { get; set; }

        public static DeliveryTypeViewModel ToViewModel(DeliveryTypeDTO dto)
        {
            if (dto != null)
            {
                return new DeliveryTypeViewModel()
                {
                    DeliveryTypeID = dto.DeliveryTypeID,
                    DeliveryMethod = dto.DeliveryMethod,
                    Description = dto.Description,
                };
            }
            else return new DeliveryTypeViewModel();
        }

        public static DeliveryTypeDTO ToDto(DeliveryTypeViewModel vm)
        {
            if (vm != null)
            {
                return new DeliveryTypeDTO()
                {
                    DeliveryTypeID = vm.DeliveryTypeID,
                    DeliveryMethod = vm.DeliveryMethod,
                    Description = vm.Description,
                };
            }
            else return new DeliveryTypeDTO();
        }

        public static KeyValueViewModel ToKeyValueViewModel(DeliveryTypeDTO dto)
        {
            if (dto != null)
            {
                return new KeyValueViewModel()
                {
                    Key = dto.DeliveryTypeID.ToString(),
                    Value = dto.DeliveryMethod 
                };
            }
            else return new KeyValueViewModel();
        }

    }
}



