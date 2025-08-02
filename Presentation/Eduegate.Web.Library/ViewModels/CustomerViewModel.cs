using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "CustomerDtails", "CRUDModel.ViewModel.Customer", "class='alignleft two-column-header'")]
    [DisplayName("Customer")]
    public class CustomerViewModel : BaseMasterViewModel
    {
        public CustomerViewModel()
        {
            Customer = new KeyValueViewModel();
        }

        [DataPicker("Customer")]
        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [CustomDisplay("CustomerID")]
        public long CustomerIID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("CustomerName")]
        public string CustomerName { get; set; }

        public KeyValueViewModel Customer { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("CustomerEmail")]
        public string CustomerEmail { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("CustomerNumber")]
        public decimal CustomerNumber { get; set; }

        public decimal? CurrentLoyaltyPoints { get; set; }
        public decimal? TotalLoyaltyPoints { get; set; }
        public string CustomerGroup { get; set;}

        public static CustomerViewModel FromCustomerDTO(CustomerDTO dto)
        {
            var primaryContact = dto.Contacts == null ? null : (dto.Contacts.Count == 1 ? dto.Contacts.FirstOrDefault() :
                dto.Contacts.Where(x => x.IsBillingAddress.HasValue && x.IsBillingAddress.Value == true).FirstOrDefault());

            return new CustomerViewModel()
            {
                CustomerIID = dto.CustomerIID,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                CustomerEmail = primaryContact != null ? primaryContact.AlternateEmailID1 : string.Empty,
                //CustomerNumber = dto.TelephoneNumber,
            };
        }

        public static CustomerViewModel FromCustomerDTOReference(CustomerDTO dto)
        {
            return new CustomerViewModel()
            {
                CustomerIID = dto.CustomerIID ,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                CurrentLoyaltyPoints = dto.Settings != null ? dto.Settings.CurrentLoyaltyPoints : 0,
                TotalLoyaltyPoints = dto.Settings != null ?  dto.Settings.TotalLoyaltyPoints : 0,
                CustomerGroup = dto.Settings != null ?  dto.Settings.CustomerGroup : ""
                //CustomerNumber = dto.TelephoneNumber,
            };
        }
    }
}