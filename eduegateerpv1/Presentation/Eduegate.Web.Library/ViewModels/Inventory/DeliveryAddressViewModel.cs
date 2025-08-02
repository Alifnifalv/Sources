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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "DeliveryAddress", "CRUDModel.Model.MasterViewModel.DeliveryDetails")]
    [DisplayName("Delivery Details")]
    public class DeliveryAddressViewModel : BaseMasterViewModel
    {
        public DeliveryAddressViewModel()
        {
            Country = new KeyValueViewModel();
            City = new KeyValueViewModel();
            Area = new KeyValueViewModel();
        }
        [Required]
        [DataPicker("CustomerContacts")]
        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [DisplayName("Customer Contact")]
        public string ContactID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Area", "Numeric", false, optionalAttribute1: "ng-click=onAreaClickSelect2($select,CRUDModel.Model.MasterViewModel.DeliveryDetails)")]
        [LookUp("LookUps.Areas")]
        [DisplayName("Area")]
        public KeyValueViewModel Area { get; set; }


        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Delivery Address")]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        public string DeliveryAddress { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Building")]
        [MaxLength(75, ErrorMessage = "Maximum Length should be within 75!")]
        public string BuildingNo { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Block")]
        [MaxLength(75, ErrorMessage = "Maximum Length should be within 75!")]
        public string Block { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Street")]
        [MaxLength(75, ErrorMessage = "Maximum Length should be within 75!")]
        public string Street { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Country", "Numeric", false,
    onSelectChangeEvent: "onCountryChangeSelect2($select, CRUDModel.Model.MasterViewModel.DeliveryDetails)")]
        [LookUp("LookUps.Country")]
        [DisplayName("Country")]
        public KeyValueViewModel Country { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", "ng-disabled ='CRUDModel.Model.MasterViewModel.DeliveryMethod.Key==2'")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Contact Person")]
        public string ContactPerson { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", "ng-disabled ='CRUDModel.Model.MasterViewModel.DeliveryMethod.Key==2'")]
        [DisplayName("Mobile No")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string MobileNo { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Landline No")]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        public string LandLineNo { get; set; }

        //[Required]
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

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("City")]
        [Select2("City", "Numeric", false, optionalAttribute1: "ng-click=onCityClickSelect2($select,CRUDModel.Model.MasterViewModel.DeliveryDetails)",
            onSelectChangeEvent: "onCityChangeSelect2($select, CRUDModel.Model.MasterViewModel.DeliveryDetails)")]
        [LookUp("LookUps.Cities")]
        public KeyValueViewModel City { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("District")]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        public string District { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("LandMark")]
        [MaxLength(200, ErrorMessage = "Maximum Length should be within 200!")]
        public string LandMark { get; set; }

        // PK of OrderContactMap table
        public long OrderContactMapID { get; set; }

        public Nullable<long> OrderID { get; set; }


        public static DeliveryAddressViewModel ToViewModelFromContactDTO(ContactDTO dto)
        {
            if (dto != null)
            {
                return new DeliveryAddressViewModel()
                {
                    ContactID = dto.ContactID == 0 ? null : dto.ContactID.ToString(),
                    ContactPerson = string.Concat(dto.FirstName, " ", dto.LastName),
                    DeliveryAddress = dto.AddressName,
                    MobileNo = dto.MobileNo1,
                    LandLineNo = dto.PhoneNo1,
                    SpecialInstructions = "NA"
                };
            }
            else return new DeliveryAddressViewModel();
        }

        public static DeliveryAddressViewModel FromOrderContactDTOToVM(OrderContactMapDTO dto)
        {
            if (dto.IsNotNull())
            {
                var vm = new DeliveryAddressViewModel();
                vm.OrderContactMapID = dto.OrderContactMapID;
                vm.OrderID = dto.OrderID;
                vm.ContactID = dto.ContactID.ToString();
                vm.ContactPerson = dto.FirstName;
                vm.DeliveryAddress = dto.AddressName;
                vm.BuildingNo = dto.BuildingNo;
                vm.Block = dto.Block;
                vm.Street = dto.Street;
                //vm.CountryID = dto.CountryID.ToString();
                vm.LandLineNo = dto.PhoneNo1;
                vm.MobileNo = dto.MobileNo1;
                vm.SpecialInstructions = dto.SpecialInstruction;
                //vm.AreaID = dto.AreaID.ToString();
                vm.UpdatedDate = dto.UpdatedDate;
                vm.UpdatedBy = dto.UpdatedBy;
                vm.CreatedDate = dto.CreatedDate;
                vm.CreatedBy = dto.CreatedBy;
                vm.TimeStamps = dto.TimeStamps;
                vm.FlatNo = dto.Flat;
                vm.Floor = dto.Floor;
                vm.Avenue = dto.Avenue;
                //vm.City = dto.City;
                vm.District = dto.District;
                vm.LandMark = dto.LandMark;

                vm.Country.Key = dto.CountryID.ToString();
                vm.Country.Value = dto.CountryName;

                vm.City.Key = dto.CityId.ToString();
                vm.City.Value = dto.CityName;

                vm.Area.Key = dto.AreaID.HasValue? dto.AreaID.Value.ToString() : null;
                vm.Area.Value = dto.AreaName;
                vm.ContactID = dto.ContactID.ToString();

                return vm;
            }
            else
            {
                return new DeliveryAddressViewModel();
            }
        }

        public static OrderContactMapDTO ToDTO(DeliveryAddressViewModel vm)
        {
            if (vm.IsNotNull())
            {
                var dto = new OrderContactMapDTO();
                dto.OrderContactMapID = vm.OrderContactMapID;
                dto.OrderID = vm.OrderID;
                dto.ContactID = vm.ContactID.IsNotNull() ? Convert.ToInt32(vm.ContactID) : (long?)null;
                dto.FirstName = vm.ContactPerson;
                dto.AddressName = vm.DeliveryAddress;
                dto.BuildingNo = vm.BuildingNo;
                dto.Block = vm.Block;
                dto.Street = vm.Street;
                //dto.CountryID = string.IsNullOrEmpty(vm.CountryID) ? (long?)null : long.Parse(vm.CountryID);
                dto.PhoneNo1 = vm.LandLineNo;
                dto.MobileNo1 = vm.MobileNo;
                dto.SpecialInstruction = vm.SpecialInstructions;
                dto.Avenue = vm.Avenue;
                dto.Flat = vm.FlatNo;
                dto.Floor = vm.Floor;
                //dto.City = vm.City;
                dto.District = vm.District;
                dto.LandMark = vm.LandMark;
                //dto.AreaID = vm.AreaID.IsNotNull() ? Convert.ToInt16(vm.AreaID) : (int?)null;
                dto.IsShippingAddress = true;
                dto.IsBillingAddress = false;
                dto.UpdatedDate = vm.UpdatedDate;
                dto.UpdatedBy = vm.UpdatedBy;
                dto.CreatedDate = vm.CreatedDate;
                dto.CreatedBy = vm.CreatedBy;
                dto.TimeStamps = string.IsNullOrEmpty(vm.TimeStamps) ? null : vm.TimeStamps.ToString();

                dto.CountryID = vm.Country.IsNull() || vm.Country.Key.IsNull() || string.IsNullOrWhiteSpace(vm.Country.Key) ? (long?)null : long.Parse(vm.Country.Key);
                dto.CityId = vm.City.IsNull() || vm.City.Key.IsNull() || string.IsNullOrWhiteSpace(vm.City.Key) ? 0 : int.Parse(vm.City.Key);
                dto.AreaID = vm.Area.IsNull() || vm.Area.Key.IsNull() || string.IsNullOrWhiteSpace(vm.Area.Key) ? (int?)null : int.Parse(vm.Area.Key); 

                return dto;
            }
            else
            {
                return new OrderContactMapDTO();
            }
        }

        public static DeliveryAddressViewModel FromDTOToVM(ContactDTO dto)
        {
            if (dto.IsNotNull())
            {
                DeliveryAddressViewModel vm = new DeliveryAddressViewModel();

                vm.ContactID = dto.ContactID.ToString();
                vm.ContactPerson = dto.FirstName;
                vm.DeliveryAddress = dto.AddressName;
                vm.BuildingNo = dto.BuildingNo;
                vm.Block = dto.Block;
                vm.Street = dto.Street;
                vm.LandLineNo = dto.PhoneNo1;
                vm.MobileNo = dto.MobileNo1;
                vm.UpdatedDate = dto.UpdatedDate;
                vm.UpdatedBy = dto.UpdatedBy;
                vm.CreatedDate = dto.CreatedDate;
                vm.CreatedBy = dto.CreatedBy;
                vm.TimeStamps = dto.TimeStamps;
                vm.FlatNo = dto.Flat;
                vm.Floor = dto.Floor;
                vm.Avenue = dto.Avenue;
                vm.District = dto.District;
                vm.LandMark = dto.LandMark;

                vm.Country.Key = dto.CountryID.HasValue? dto.CountryID.Value.ToString() : null;
                vm.Country.Value = dto.CountryName.IsNotNull() ? dto.CountryName : string.Empty;

                vm.City.Key = dto.CityID.IsNotNull() ? Convert.ToString(dto.CityID) : string.Empty;
                vm.City.Value = dto.City.IsNotNull() ? dto.City : string.Empty;

                vm.Area.Key = dto.AreaID.HasValue ? dto.AreaID.Value.ToString() : null;
                vm.Area.Value = dto.AreaName.IsNotNull() ? dto.AreaName : string.Empty;
                return vm;
            }
            else
            {
                return new DeliveryAddressViewModel();
            }
        }
    }
}
