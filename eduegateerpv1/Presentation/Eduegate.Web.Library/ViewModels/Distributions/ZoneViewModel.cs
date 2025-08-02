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
    public class ZoneViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        [DisplayName("ZoneID")]
        public short ZoneID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, attribs: "ng-change='GetCityByCountryId($event, $element)'")]
        [Select2("Country", "Numeric", false)]
        [LookUp("LookUps.Countries")]
        [DisplayName("Country")]
        public KeyValueViewModel Country { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ZoneName")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string ZoneName { get; set; }



        public static ZoneViewModel ToViewModel(ZoneDTO dto)
        {
            ZoneViewModel zoneVM = new ZoneViewModel();
            zoneVM.Country = new KeyValueViewModel();
            zoneVM.Country.Key = dto.CompanyID.ToString();
            zoneVM.Country.Value = dto.CountryName;
            Mapper<ZoneDTO, ZoneViewModel>.CreateMap();
            var mapper = Mapper<ZoneDTO, ZoneViewModel>.Map(dto);
            mapper.Country = new KeyValueViewModel();
            mapper.Country.Key = dto.CountryID.ToString();
            mapper.Country.Value = dto.CountryName;
            //mapper.ZoneName = dto.ZoneName;
            return mapper;
        }

        public static ZoneDTO ToDTO(ZoneViewModel vm)
        {
            Mapper<ZoneViewModel, ZoneDTO>.CreateMap();
            ZoneDTO dto = new ZoneDTO();
            dto.CountryID = vm.Country.IsNotNull() ? Convert.ToInt32(vm.Country.Key) : (int?)null;
            dto.CountryName = vm.Country.IsNotNull() ? vm.Country.Value : null;
            var mapper = Mapper<ZoneViewModel, ZoneDTO>.Map(vm);
            mapper.CountryID = dto.CountryID;
            mapper.CountryName = dto.CountryName;
            return mapper;
        }
    }
}
