using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Checkout;

namespace Eduegate.Web.Library.ViewModels.Checkout
{
    public class PaymentMethodViewModel : BaseMasterViewModel
    {
        public int PaymentMethodID { get; set; }
        public string PaymentMethodName { get; set; }
        public string ImageName { get; set; }

        public static List<PaymentMethodViewModel> ToDTO(List<PaymentMethodDTO> dtos)
        {
            var vms = new List<PaymentMethodViewModel>();

            foreach (var dto in dtos)
            {
                vms.Add(ToDTO(dto));
            }

            return vms;
        }

        public static PaymentMethodViewModel ToDTO(PaymentMethodDTO dto)
        {
            Mapper<PaymentMethodDTO, PaymentMethodViewModel>.CreateMap();
            return Mapper<PaymentMethodDTO, PaymentMethodViewModel>.Map(dto);
        }

        public static List<PaymentMethodDTO> FromDTO(List<PaymentMethodViewModel> vms)
        {
            var dtos = new List<PaymentMethodDTO>();

            foreach (var vm in vms)
            {
                dtos.Add(FromDTO(vm));
            }

            return dtos;
        }

        public static PaymentMethodDTO FromDTO(PaymentMethodViewModel vm)
        {
            Mapper<PaymentMethodViewModel, PaymentMethodDTO>.CreateMap();
            return Mapper<PaymentMethodViewModel, PaymentMethodDTO>.Map(vm);
        }
    }
}
