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
    public class PayrollFrequenciesViewModel : BaseMasterViewModel
    {
        ///[Required]
        ///[ControlType(Framework.Enums.ControlTypes.Label)]
       /// [DisplayName("Payroll Frequency ID")]
        public byte PayrollFrequencyID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50,ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("FrequencyName")]
        public string FrequencyName { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as PayrollFrequenciesDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<PayrollFrequenciesViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<PayrollFrequenciesDTO, PayrollFrequenciesViewModel>.CreateMap();
            var vm = Mapper<PayrollFrequenciesDTO, PayrollFrequenciesViewModel>.Map(dto as PayrollFrequenciesDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<PayrollFrequenciesViewModel, PayrollFrequenciesDTO>.CreateMap();
            var dto = Mapper<PayrollFrequenciesViewModel, PayrollFrequenciesDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<PayrollFrequenciesDTO>(jsonString);
        }
    }
}
