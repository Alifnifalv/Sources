using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.HR.Payroll
{
    public class SalaryMethodViewModel : BaseMasterViewModel
    {
       /// [Required]
       /// [ControlType(Framework.Enums.ControlTypes.Label)]
       /// [DisplayName("Salary Method ID")]
        public int SalaryMethodID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20,ErrorMessage = "Maximum Length should be within 20!")]
        [CustomDisplay("SalaryMethodName")]
        public string SalaryMethodName { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SalaryMethodDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SalaryMethodViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SalaryMethodDTO, SalaryMethodViewModel>.CreateMap();
            var vm = Mapper<SalaryMethodDTO, SalaryMethodViewModel>.Map(dto as SalaryMethodDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SalaryMethodViewModel, SalaryMethodDTO>.CreateMap();
            var dto = Mapper<SalaryMethodViewModel, SalaryMethodDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SalaryMethodDTO>(jsonString);
        }

    }
}
