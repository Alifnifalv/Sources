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
    public class UnitGroupViewModel : BaseMasterViewModel
    {

        public long UnitGroupID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public int NewLine1 { get; set; }

        [Required]
        [ControlType(Eduegate.Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("Unit Group Code")]
        public string UnitGroupCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public int NewLine2 { get; set; }

        [Required]
        [ControlType(Eduegate.Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Unit Group Name")]
        public string UnitGroupName { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public int NewLine3 { get; set; }

        //[Required]
        //[ControlType(Eduegate.Framework.Enums.ControlTypes.TextBox)]
        //[MaxLength(16, ErrorMessage = "Maximum Length should be within 16!")]
        //[DisplayName("Fraction")]
        //public double? Fraction { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as UnitGroupDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<UnitGroupViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<UnitGroupDTO, UnitGroupViewModel>.CreateMap();
            var vm = Mapper<UnitGroupDTO, UnitGroupViewModel>.Map(dto as UnitGroupDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<UnitGroupViewModel, UnitGroupDTO>.CreateMap();
            var dto = Mapper<UnitGroupViewModel, UnitGroupDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<UnitGroupDTO>(jsonString);
        }
    }
}
