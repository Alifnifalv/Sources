using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.ShoppingCart;

namespace Eduegate.Web.Library.ViewModels.ShoppingCart
{
   public class ProcessOrderViewModel
    {
       
        public long CartID { get; set; }
        public int CartStatus { get; set; }
        public string Description { get; set; }
        public string PaymentMethod { get; set; }
        public long ShoppingCartIID { get; set; }
        public Int16 PaymentgateWayID { get; set; }
        public string Message { get; set; }
        public int CartStatusID { get; set; }
        public bool IsForceCreation { get; set; }

        public static ProcessOrderDTO ToDTO(ProcessOrderViewModel vm)
        {
            return new ProcessOrderDTO()
            {
                CartID = vm.CartID,
                CartStatus = vm.CartStatus,
                Description = vm.Description,
                PaymentMethod = vm.PaymentMethod,
                ShoppingCartIID = vm.ShoppingCartIID,
                PaymentgateWayID = vm.PaymentgateWayID,
                Message = vm.Message,
            };
        }

        public static ProcessOrderViewModel ToVM(ProcessOrderDTO dto)
        {
            return new ProcessOrderViewModel()
            {
                CartID = dto.CartID,
                CartStatus = dto.CartStatus,
                Description = dto.Description,
                PaymentMethod = dto.PaymentMethod,
                ShoppingCartIID = dto.ShoppingCartIID,
                PaymentgateWayID = dto.PaymentgateWayID,
                Message = dto.Message,
            };
        }
       
    }
}
