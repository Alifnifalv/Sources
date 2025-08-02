using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Fees;
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

namespace Eduegate.Web.Library.School.Fees
{
    public class BudgetViewModel : BaseMasterViewModel
    {
        public BudgetViewModel()
        {   }
        public int BudgetID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [CustomDisplay("Budget Code")]
        public string BudgetCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("Budget Name")]
        public string BudgetName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

       

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as BudgetDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<BudgetViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<BudgetDTO, BudgetViewModel>.CreateMap();
          
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var stDtO = dto as BudgetDTO;
            var vm = Mapper<BudgetDTO, BudgetViewModel>.Map(stDtO);

            vm.BudgetID = stDtO.BudgetID;
            return vm;
           
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<BudgetViewModel, BudgetDTO>.CreateMap();
            var dto = Mapper<BudgetViewModel, BudgetDTO>.Map(this);

            dto.BudgetID = this.BudgetID;
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<BudgetDTO>(jsonString);
        }
    }
}
