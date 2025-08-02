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
    public class DeliveryAddressViewViewModel : BaseMasterViewModel
    {
        public DeliveryAddressViewViewModel()
        {

        }
        [ControlType(Framework.Enums.ControlTypes.Button,"", "ng-click ='EditCustomer(\"Customer\",CRUDModel.Model.MasterViewModel.Customer.Key)'")]
        [DisplayName("Customer Edit")]
        public string CustomerDetails { get; set; }  

        //[Required]
        [DataPicker("CustomerContacts", invoker: "ShoppingCartController", runtimeFilter: "'CustomerIID='+CRUDModel.Model.MasterViewModel.Customer.Key")]
        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [DisplayName("Customer Contact")]
        public string ContactID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        //[Select2("Area", "Numeric", false, optionalAttribute1: "ng-click=onAreaClickSelect2($select,CRUDModel.Model.MasterViewModel.DeliveryDetails)")]
        //[LookUp("LookUps.Areas")]
        [DisplayName("Area")]
        public string Area { get; set; } 

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Delivery Address")]
        public string DeliveryAddress { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Building")]
        public string BuildingNo { get; set; } 


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Block")]
        public string Block { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Street")]
        public string Street { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        //[Select2("Country", "Numeric", false,
        //onSelectChangeEvent: "onCountryChangeSelect2($select, CRUDModel.Model.MasterViewModel.DeliveryDetails)")]
        //[LookUp("LookUps.Country")]
        [DisplayName("Country")]
        public string Country { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Contact Person")]
        public string ContactPerson { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Mobile No")]
        public string MobileNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Landline No")]
        public string LandLineNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Special Instructions")]
        public string SpecialInstructions { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Flat No")]
        public string FlatNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Floor")]
        public string Floor { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Avenue")]
        public string Avenue { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("City")]
        //[Select2("City", "Numeric", false, optionalAttribute1: "ng-click=onCityClickSelect2($select,CRUDModel.Model.MasterViewModel.DeliveryDetails)",
        //    onSelectChangeEvent: "onCityChangeSelect2($select, CRUDModel.Model.MasterViewModel.DeliveryDetails)")]
        //[LookUp("LookUps.Cities")]
        public string City { get; set; }
         
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("District")]
        public string District { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("LandMark")]
        public string LandMark { get; set; }

        // PK of OrderContactMap table
        public long OrderContactMapID { get; set; }

        public Nullable<long> OrderID { get; set; }

        public Nullable<long> CustomerID { get; set; }  


        public static DeliveryAddressViewViewModel ToViewModelFromContactDTO(ContactDTO dto)
        {
            if (dto != null)
            {
                return new DeliveryAddressViewViewModel()
                {
                    ContactID = dto.ContactID == 0 ? null : dto.ContactID.ToString(),
                    ContactPerson = string.Concat(dto.FirstName, " ", dto.LastName),
                    DeliveryAddress = dto.AddressName,
                    MobileNo = dto.MobileNo1,
                    LandLineNo = dto.PhoneNo1,
                    SpecialInstructions = "NA"
                };
            }
            else return new DeliveryAddressViewViewModel();
        }

        public static DeliveryAddressViewViewModel FromOrderContactDTOToVM(OrderContactMapDTO dto)
        {
            if (dto.IsNotNull())
            {
                DeliveryAddressViewViewModel vm = new DeliveryAddressViewViewModel();

                vm.OrderContactMapID = dto.OrderContactMapID;
                vm.OrderID = dto.OrderID;
                vm.ContactID = dto.ContactID.ToString();
                vm.ContactPerson = dto.FirstName;
                vm.DeliveryAddress = dto.AddressName;
                vm.BuildingNo = dto.BuildingNo;
                vm.Block = dto.Block;
                vm.Street = dto.Street;
                vm.LandLineNo = dto.PhoneNo1;
                vm.MobileNo = dto.MobileNo1;
                vm.SpecialInstructions = dto.SpecialInstruction;
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

                vm.Country = dto.CountryName; 

                //vm.City = dto.CityId.ToString();
                vm.City = dto.CityName;

                //vm.Area.Key = dto.AreaID.ToString();
                vm.Area = dto.AreaName;
                vm.ContactID = dto.ContactID.ToString();

                return vm;
            }
            else
            {
                return new DeliveryAddressViewViewModel();
            }
        }

        public static OrderContactMapDTO ToDTO(DeliveryAddressViewViewModel vm)
        {
            if (vm.IsNotNull())
            {
                OrderContactMapDTO dto = new OrderContactMapDTO();

                dto.OrderContactMapID = vm.OrderContactMapID;
                dto.OrderID = Convert.ToInt32(vm.OrderID);
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
                dto.District = vm.District;
                dto.LandMark = vm.LandMark;
                //dto.AreaID = vm.Area.IsNotNull() ? Convert.ToInt16(vm.Area.Key) : (int?)null;
                dto.IsShippingAddress = true;
                dto.IsBillingAddress = false;
                dto.UpdatedDate = vm.UpdatedDate;
                dto.UpdatedBy = vm.UpdatedBy;
                dto.CreatedDate = vm.CreatedDate;
                dto.CreatedBy = vm.CreatedBy;
                dto.TimeStamps = string.IsNullOrEmpty(vm.TimeStamps) ? null : vm.TimeStamps.ToString();
                //dto.CountryID = vm.Country.Key.IsNull() || string.IsNullOrWhiteSpace(vm.Country.Key) ? (long?)null : long.Parse(vm.Country.Key);
                //dto.CityId = vm.City.IsNull() || vm.City.Key.IsNull() || string.IsNullOrWhiteSpace(vm.City.Key) ? 0 : int.Parse(vm.City.Key);
                //dto.AreaID = vm.Area.IsNull() || vm.Area.Key.IsNull() || string.IsNullOrWhiteSpace(vm.Area.Key) ? (int?)null : int.Parse(vm.Area.Key); 
                dto.City = vm.City;
                dto.CountryName = vm.Country;
                dto.AreaName = vm.Area;
                return dto;
            }
            else
            {
                return new OrderContactMapDTO();
            }
        }


        public static DeliveryAddressViewViewModel FromDTOToVM(ContactDTO dto)
        {
            if (dto.IsNotNull())   
            {
                DeliveryAddressViewViewModel vm = new DeliveryAddressViewViewModel();

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
                vm.Country = dto.CountryName.IsNotNull () ? dto.CountryName : string.Empty;

                vm.City = dto.City.IsNotNull() ? dto.City : string.Empty;

                //vm.Area.Key = dto.AreaID.ToString();
                vm.Area = dto.AreaName.IsNotNull() ? dto.AreaName : string.Empty;
                vm.CustomerID = dto.CustomerID.IsNotNull() ? dto.CustomerID : null;

                return vm;
            }
            else
            {
                return new DeliveryAddressViewViewModel();
            }
        }

    }
}
