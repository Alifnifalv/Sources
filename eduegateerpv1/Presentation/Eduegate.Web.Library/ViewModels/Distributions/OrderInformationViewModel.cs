using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;

namespace Eduegate.Web.Library.ViewModels.Distributions
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "OrderInfo", "Model.MasterViewModel.OrderInfo")]
    [DisplayName("Transaction Info")]
    public class OrderInformationViewModel : BaseMasterViewModel
    {
        public OrderInformationViewModel()
        {
            CustomerDetail = new CustomerDetailViewModel();
            DeliveryAddress = new DeliveryAddressDetailViewModel();
            PaymentDetail = new PaymentDetailViewModel();
        }

        [ControlType(Framework.Enums.ControlTypes.GroupInfo)]
        [DisplayName("Customer Details")]
        public CustomerDetailViewModel CustomerDetail { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GroupInfo)]
        [DisplayName("Delivery Address")]
        public DeliveryAddressDetailViewModel DeliveryAddress { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GroupInfo)]
        [DisplayName("Payment Details")]
        public PaymentDetailViewModel PaymentDetail { get; set; }

        public static CustomerDetailViewModel GetCustomerDetail(CustomerDTO customerDTO)
        {
            CustomerDetailViewModel customerVM = new CustomerDetailViewModel();

            if (customerDTO.IsNotNull())
            {
                customerVM.CustomerName = string.Concat(customerDTO.FirstName, customerDTO.MiddleName, "  " , customerDTO.LastName);
                customerVM.CustomerEmail = customerDTO.CustomerEmail;
                customerVM.CustomerNumber = customerDTO.CustomerNumber;
            }

            return customerVM;
        }

        public static DeliveryAddressDetailViewModel GetDeliveryAddress(MissionProcessingAddressViewModel address)
        {
            DeliveryAddressDetailViewModel dadVM = new DeliveryAddressDetailViewModel();

            if (address.IsNotNull())
            {
                dadVM.ContactPerson = address.ContactPerson;
                dadVM.DeliveryAddress = address.DeliveryAddress;
                dadVM.MobileNo = address.MobileNo;
                dadVM.LandLineNo = address.LandLineNo;
                dadVM.SpecialInstructions = address.SpecialInstructions;
            }

            return dadVM;
        }

    }
}
