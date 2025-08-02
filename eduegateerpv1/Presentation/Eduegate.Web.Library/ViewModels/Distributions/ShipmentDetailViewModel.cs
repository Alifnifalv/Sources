using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.ExternalServices;

namespace Eduegate.Web.Library.ViewModels.Distributions
{
    public class ShipmentDetailViewModel : BaseMasterViewModel
    {
        public string ReferenceNo { get; set; }
        public string CustomerName { get; set; }
        public string City { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string NoOfPcs { get; set; }
        public string Weight { get; set; }
        public string CODAmount { get; set; }
        public string ItemDescription { get; set; }

        public static ServiceProviderShipmentDetailDTO ToDTO(ShipmentDetailViewModel vm)
        {
            Mapper<ShipmentDetailViewModel, ServiceProviderShipmentDetailDTO>.CreateMap();
            return Mapper<ShipmentDetailViewModel, ServiceProviderShipmentDetailDTO>.Map(vm);
        }

        public static ShipmentDetailViewModel ToVM(ServiceProviderShipmentDetailDTO vm)
        {
            Mapper<ServiceProviderShipmentDetailDTO, ShipmentDetailViewModel>.CreateMap();
            return Mapper<ServiceProviderShipmentDetailDTO, ShipmentDetailViewModel>.Map(vm);
        }
    }
}
