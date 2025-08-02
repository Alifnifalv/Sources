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
using Eduegate.Framework.Extensions;

namespace Eduegate.Web.Library.ViewModels.Distributions
{
    public class RouteViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Route ID")]
        public int RouteID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, attribs: "ng-change='GetCityByCountryId($event, $element)'")]
        [Select2("Country", "Numeric", false)]
        [LookUp("LookUps.Countries")]
        [DisplayName("Country")]
        public KeyValueViewModel Country { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Warehouse")]
        [DisplayName("Warehouse")]
        public string WarehouseID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [DisplayName("Description")]
        public string Description { get; set; }
        public Nullable<int> CompanyID { get; set; }


        public static RouteViewModel FromDTO(RouteDTO dto)
        {
            Mapper<RouteDTO, RouteViewModel>.CreateMap();            
            var mapper = Mapper<RouteDTO, RouteViewModel>.Map(dto);
            mapper.Country = new KeyValueViewModel();
            mapper.Country.Key = dto.CountryID.ToString();
            mapper.Country.Value = dto.CountryName;
            return mapper;
        }

        public static RouteDTO ToDTO(RouteViewModel vm)
        {
            Mapper<RouteViewModel, RouteDTO>.CreateMap();
            RouteDTO dto = new RouteDTO();
            dto.CountryID = vm.Country.IsNotNull() ? Convert.ToInt32(vm.Country.Key) : (int?)null;
            dto.CountryName = vm.Country.IsNotNull() ? vm.Country.Value : null;
            var mapper = Mapper<RouteViewModel, RouteDTO>.Map(vm);
            mapper.CountryID = dto.CountryID;
            mapper.CountryName = dto.CountryName;
            return mapper;
        }

        public static RouteViewModel FromRouteViewModelToVM(RouteViewModel vm)
        {
            if (vm.IsNotNull())
            {
                var routeVM = new RouteViewModel()
                {
                    Description = vm.Description,
                    WarehouseID = vm.WarehouseID,
                    CreatedBy = vm.CreatedBy,
                    UpdatedBy = vm.UpdatedBy,
                    CreatedDate = vm.CreatedDate,
                    UpdatedDate = vm.UpdatedDate,
                    TimeStamps = vm.TimeStamps,
                };

                return routeVM;
            }
            else
            {
                return new RouteViewModel();
            }
        }

    }
}
