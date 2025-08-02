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
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Web.Library.ViewModels.Distributions
{
    public class AreaViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Area ID")]
        public int AreaID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, attribs: "ng-change='GetCityByCountryId($event, $element)'")]
        [Select2("Country", "Numeric", false)]
        [LookUp("LookUps.Countries")]
        [DisplayName("Country")]
        public KeyValueViewModel Country { get; set; }
        
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Area Name")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string AreaName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Route")]
        [DisplayName("Route")]
        public int RouteID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Zone")]
        [DisplayName("Zone")]        
        public string ZoneID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("City", "Numeric", false, optionalAttribute1: "ng-click=onCityClickSelect2($select,CRUDModel.Model.MasterViewModel.DeliveryDetails)")]
        [DisplayName("City")]
        [LookUp("LookUps.Cities")]
        public KeyValueViewModel City { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Active")]
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> CompanyID { get; set; }

        public static AreaViewModel FromDTO(AreaDTO dto)
        {
            //AreaViewModel areaVM = new AreaViewModel();
            Mapper<AreaDTO, AreaViewModel>.CreateMap();
           
            var mapper = Mapper<AreaDTO, AreaViewModel>.Map(dto);
            mapper.Country = new KeyValueViewModel();
            mapper.Country.Key = dto.CountryID.ToString();
            mapper.Country.Value = dto.CountryName;
            mapper.City = new KeyValueViewModel();
            mapper.City.Key = dto.CityID.ToString();
            mapper.City.Value = dto.CityName;
            return mapper;
        }

        public static AreaDTO ToDTO(AreaViewModel vm)
        {
            Mapper<AreaViewModel, AreaDTO>.CreateMap();
            AreaDTO dto = new AreaDTO();
            dto.CountryID = vm.Country.IsNotNull() ? Convert.ToInt32(vm.Country.Key) : (int?)null;
            dto.CountryName = vm.Country.IsNotNull() ? vm.Country.Value : null;
            dto.CityID = vm.City.IsNotNull() ? Convert.ToInt32(vm.City.Key) : (int?)null;
            dto.CountryName = vm.City.IsNotNull() ? vm.City.Value : null;
            var mapper = Mapper<AreaViewModel, AreaDTO>.Map(vm);
            mapper.CountryID = dto.CountryID;
            mapper.CountryName = dto.CountryName;
            mapper.CityID = dto.CityID;
            mapper.CityName = dto.CityName;
            return mapper;
        }
    }
}
