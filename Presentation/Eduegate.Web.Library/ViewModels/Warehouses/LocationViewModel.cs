using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Services.Contracts.Warehouses;

namespace Eduegate.Web.Library.ViewModels.Warehouses
{
    public class LocationViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Location ID")]
        public long LocationIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Code")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string LocationCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Description")]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        public string Description { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Branch")]
        [LookUp("LookUps.Branch")]
        [HasClaim(HasClaims.HASMULTIBRANCH)]
        public string Branch { get; set; }

        public long BranchID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Type")]
        [LookUp("LookUps.LocationType")]
        public string LocationType { get; set; }
        public long LocationTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Bar Code")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string BarCode { get; set; }

        public static LocationViewModel FromDTO(LocationDTO dto)
        {
            Mapper<LocationDTO, LocationViewModel>.CreateMap();
            var mapper = Mapper<LocationDTO, LocationViewModel>.Map(dto);
            mapper.Branch = dto.BranchID.ToString();
            mapper.LocationType = dto.LocationTypeID.ToString();
            return mapper;
        }

        public static LocationDTO ToDTO(LocationViewModel vm)
        {
            Mapper<LocationViewModel, LocationDTO>.CreateMap();
            var mapper = Mapper<LocationViewModel, LocationDTO>.Map(vm);
            mapper.BranchID = vm.Branch == null ? (long?)null : long.Parse(vm.Branch);
            mapper.LocationTypeID = byte.Parse(vm.LocationType);
            return mapper;
        }
    }
}
