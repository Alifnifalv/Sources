using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "BillingAddress", "CRUDModel.Model.MasterViewModel.BillingDetails")]
    [DisplayName("Billing Details")]
    public class BillingAddressViewModel : BaseMasterViewModel
    {
        public BillingAddressViewModel()
        {
            Country = new KeyValueViewModel();
            City = new KeyValueViewModel();
            Area = new KeyValueViewModel();
        }

        [DataPicker("CustomerContacts")]
        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [DisplayName("Customer Contact")]
        public string ContactID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Country", "Numeric", false,
        onSelectChangeEvent: "onCountryChangeSelect2($select, CRUDModel.Model.MasterViewModel.BillingDetails)")]
        [LookUp("LookUps.Country")]
        [DisplayName("Country")]
        public KeyValueViewModel Country { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("City")]
        [Select2("City", "Numeric", false, optionalAttribute1: "ng-click=onCityClickSelect2($select,CRUDModel.Model.MasterViewModel.DeliveryDetails)",
            onSelectChangeEvent: "onCityChangeSelect2($select, CRUDModel.Model.MasterViewModel.BillingDetails)")]
        [LookUp("LookUps.Cities")]
        public KeyValueViewModel City { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Area", "Numeric", false, optionalAttribute1: "ng-click=onAreaClickSelect2($select,CRUDModel.Model.MasterViewModel.BillingDetails)")]
        [LookUp("LookUps.Areas")]
        [DisplayName("Area")]
        public KeyValueViewModel Area { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Billing Address")]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        public string BillingAddress { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "", "ng-disabled= 'CRUDModel.Model.MasterViewModel.DeliveryMethod.Key==2'")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Contact Person")]
        public string ContactPerson { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Mobile No")]
        [MaxLength(13, ErrorMessage = "Maximum Length should be within 13!")]
        public string bMobileNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Landline No")]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        public string LandLineNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Special Instructions")]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        public string SpecialInstructions { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Flat No")]
        [MaxLength(75, ErrorMessage = "Maximum Length should be within 75!")]
        public string FlatNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Floor")]
        [MaxLength(75, ErrorMessage = "Maximum Length should be within 75!")]
        public string Floor { get; set; } 

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Avenue")]
        [MaxLength(75, ErrorMessage = "Maximum Length should be within 75!")]
        public string Avenue { get; set; }

        // PK of OrderContactMap table
        public long OrderContactMapID { get; set; }

        public Nullable<long> OrderID { get; set; }

        public static BillingAddressViewModel ToViewModelFromContactDTO(ContactDTO dto)
        {
            if (dto != null)
            {
                return new BillingAddressViewModel()
                {
                    ContactID = dto.ContactID.ToString(),
                    ContactPerson = string.Concat(dto.FirstName, " ", dto.LastName),
                    BillingAddress = dto.AddressName,
                    bMobileNo = dto.MobileNo1,
                    LandLineNo = dto.PhoneNo1,
                    SpecialInstructions = "NA"
                };
            }
            else return new BillingAddressViewModel();
        }

        public static BillingAddressViewModel FromOrderContactDTOToVM(OrderContactMapDTO dto)
        {
            if (dto.IsNotNull())
            {
                BillingAddressViewModel vm = new BillingAddressViewModel();

                vm.OrderContactMapID = dto.OrderContactMapID;
                vm.OrderID = dto.OrderID;
                vm.ContactID = dto.ContactID.ToString();
                vm.ContactPerson = dto.FirstName;
                vm.BillingAddress = dto.AddressName;
                vm.LandLineNo = dto.PhoneNo1;
                vm.bMobileNo = dto.MobileNo1;
                vm.SpecialInstructions = dto.SpecialInstruction;
                //vm.AreaID = dto.AreaID.ToString();
                vm.UpdatedDate = dto.UpdatedDate;
                vm.UpdatedBy = dto.UpdatedBy;
                vm.CreatedDate = dto.CreatedDate;
                vm.CreatedBy = dto.CreatedBy;
                vm.FlatNo = dto.Flat;
                vm.Floor = dto.Floor;
                vm.Avenue = dto.Avenue;
                vm.TimeStamps = dto.TimeStamps;

                vm.Country.Key = dto.CountryID.ToString();
                vm.Country.Value = dto.CountryName;

                vm.City.Key = dto.CityId.ToString();
                vm.City.Value = dto.CityName;

                vm.Area.Key = dto.AreaID.ToString();
                vm.Area.Value = dto.AreaName;

                return vm;
            }
            else
            {
                return new BillingAddressViewModel();
            }
        }

        public static OrderContactMapDTO ToDTO(BillingAddressViewModel vm)
        {
            if (vm.IsNotNull())
            {
                OrderContactMapDTO dto = new OrderContactMapDTO();

                dto.OrderContactMapID = vm.OrderContactMapID;
                dto.OrderID = Convert.ToInt32(vm.OrderID);
                dto.ContactID = vm.ContactID.IsNotNull() ? Convert.ToInt32(vm.ContactID) : (long?)null;
                dto.FirstName = vm.ContactPerson;
                dto.AddressName = vm.BillingAddress;
                dto.PhoneNo1 = vm.LandLineNo;
                dto.MobileNo1 = vm.bMobileNo;
                dto.Flat = vm.FlatNo;
                dto.Floor = vm.Floor;
                dto.Avenue = vm.Avenue;
                dto.SpecialInstruction = vm.SpecialInstructions;
                //dto.AreaID = vm.AreaID.IsNotNull() ? Convert.ToInt16(vm.AreaID) : (int?)null;
                dto.IsShippingAddress = false;
                dto.IsBillingAddress = true;
                dto.UpdatedDate = vm.UpdatedDate;
                dto.UpdatedBy = vm.UpdatedBy;
                dto.CreatedDate = vm.CreatedDate;
                dto.CreatedBy = vm.CreatedBy;
                dto.TimeStamps = string.IsNullOrEmpty(vm.TimeStamps) ? null : vm.TimeStamps.ToString();


                dto.CountryID = vm.Country.IsNull() || string.IsNullOrWhiteSpace(vm.Country.Key) ? (long?)null : long.Parse(vm.Country.Key);
                dto.CityId = vm.City.IsNull() || string.IsNullOrWhiteSpace(vm.City.Key) ? 0 : int.Parse(vm.City.Key);
                dto.AreaID = vm.Area.IsNull() || string.IsNullOrWhiteSpace(vm.Area.Key) ? (int?)null : int.Parse(vm.Area.Key);

                return dto;
            }
            else
            {
                return new OrderContactMapDTO();
            }
        }

    }
}
