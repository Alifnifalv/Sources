using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Web.Library.ViewModels.Distributions
{
    public class CityViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("City ID")]
        public long CityID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Countries")]
        [DisplayName("Country")]
        public string CountryID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("City Name")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string CityName { get; set; }
        

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("IsActive")]
        public Nullable<bool> IsActive { get; set; }

        public static CityViewModel FromDTO(CityDTO dto)
        {
            Mapper<CityDTO, CityViewModel>.CreateMap();
            var mapper = Mapper<CityDTO, CityViewModel>.Map(dto);
            return mapper;
        }

        public static CityDTO ToDTO(CityViewModel vm)
        {
            Mapper<CityViewModel, CityDTO>.CreateMap();
            var mapper = Mapper<CityViewModel, CityDTO>.Map(vm);
            return mapper;
        }
    }
}
