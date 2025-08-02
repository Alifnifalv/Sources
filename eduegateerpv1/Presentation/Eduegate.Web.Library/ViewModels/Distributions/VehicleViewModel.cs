using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Web.Library.ViewModels.Distributions
{
    public class VehicleViewModel : BaseMasterViewModel
    {
        public long VehicleID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.VehicleType")]
        [DisplayName("Vehicle Type")]
        public string VehicleTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.VehicleOwnershipType")]
        [DisplayName("Vehicle Ownership Type")]
        public string VehicleOwnershipTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Registration Name")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string RegistrationName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Vehicle Code")]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        public string VehicleCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Description")]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        public string Description { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Registration No")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string RegistrationNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Purchase Date")]
        public string PurchaseDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Registration Expire")]
        public string RegistrationExpire { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Insurance Expire")]
        public string InsuranceExpire { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, attribs: "ng-change='GetCityByCountryId($event, $element")]
        [Select2("Registration Country", "Numeric", false)]
        [LookUp("LookUps.Countries")]
        [DisplayName("Registration Country")]
        public KeyValueViewModel RigistrationCountry { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Rigistration City", "Numeric", false)]
        [LookUp("LookUps.Cities")]
        [DisplayName("Rigistration City")]
        public KeyValueViewModel RigistrationCity { get; set; } 
        //public int RigistrationCountryID { get; set; }


        public static VehicleViewModel FromDTO(VehicleDTO dto)
        {
            VehicleViewModel vehicleVM = new VehicleViewModel();
            Mapper<VehicleDTO, VehicleViewModel>.CreateMap();
            var mapper = Mapper<VehicleDTO, VehicleViewModel>.Map(dto);
            mapper.RigistrationCountry = new KeyValueViewModel();
            mapper.RigistrationCountry.Key = dto.RigistrationCountryID.ToString();
            mapper.RigistrationCountry.Value = dto.CountryName;
            mapper.RigistrationCity = new KeyValueViewModel();
            mapper.RigistrationCity.Key = dto.RigistrationCityID.ToString();
            mapper.RigistrationCity.Value = dto.CityName;
            return mapper;
        }

        public static VehicleDTO ToDTO(VehicleViewModel vm)
        {
            Mapper<VehicleViewModel, VehicleDTO>.CreateMap();
            VehicleDTO dto = new VehicleDTO();
            dto.RigistrationCountryID = vm.RigistrationCountry.IsNotNull() && !string.IsNullOrEmpty(vm.RigistrationCountry.Key) ? Convert.ToInt32(vm.RigistrationCountry.Key) : (int?)null;
            dto.CountryName = vm.RigistrationCountry.IsNotNull() && !string.IsNullOrEmpty(vm.RigistrationCountry.Value) ? vm.RigistrationCountry.Value : null;
            dto.RigistrationCityID = vm.RigistrationCity.IsNotNull() && !string.IsNullOrEmpty(vm.RigistrationCity.Key) ? Convert.ToInt32(vm.RigistrationCity.Key) : (int?)null;
            dto.CityName = vm.RigistrationCity.IsNotNull() && !string.IsNullOrEmpty(vm.RigistrationCity.Value) ? vm.RigistrationCity.Value : null;
            var mapper = Mapper<VehicleViewModel, VehicleDTO>.Map(vm);
            mapper.RigistrationCountryID = dto.RigistrationCountryID;
            mapper.CountryName = dto.CountryName;
            mapper.RigistrationCityID = dto.RigistrationCityID;
            mapper.CityName = dto.CityName;
            return mapper;
        }
    }
}
