using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Inventory;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Inventory
{
    public class UnitViewModel : BaseMasterViewModel
    {
        ///[Required]
        ///[ControlType(Eduegate.Framework.Enums.ControlTypes.Label)]
        ///[DisplayName("Unit ID")]
        public long UnitID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public int NewLine1 { get; set; }

        [Required]
        [ControlType(Eduegate.Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [CustomDisplay("UnitCode")]
        public string UnitCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public int NewLine2 { get; set; }

        [Required]
        [ControlType(Eduegate.Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("UnitName")]
        public string UnitName { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public int NewLine3 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.UnitGroup")]
        [DisplayName("Unit Group")]
        public string UnitGroup { get; set; }

        public long UnitGroupID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public int NewLine4 { get; set; }

        //[Required]
        [ControlType(Eduegate.Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(16, ErrorMessage = "Maximum Length should be within 16!")]
        [CustomDisplay("Fraction")]
        public double? Fraction { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as UnitDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<UnitViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<UnitDTO, UnitViewModel>.CreateMap();
            var vm = Mapper<UnitDTO, UnitViewModel>.Map(dto as UnitDTO);
            var toDTO = dto as UnitDTO;

            vm.UnitGroup = toDTO.UnitGroupID.ToString();

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<UnitViewModel, UnitDTO>.CreateMap();
            var dto = Mapper<UnitViewModel, UnitDTO>.Map(this);

            dto.UnitGroupID = string.IsNullOrEmpty(this.UnitGroup) ? (byte?)null : byte.Parse(this.UnitGroup);

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<UnitDTO>(jsonString);
        }
    }
}
