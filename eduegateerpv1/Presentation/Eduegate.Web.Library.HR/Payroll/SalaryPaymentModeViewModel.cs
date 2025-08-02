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
   public class SalaryPaymentModeViewModel : BaseMasterViewModel
    {
       /// [Required]
       /// [ControlType(Framework.Enums.ControlTypes.Label)]
        ///[DisplayName("Salary Payment Mode ID")]
        public int SalaryPaymentModeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(8,ErrorMessage = "Maximum Length should be within 8!")]
        [CustomDisplay("PaymentModeTypeCode")]
        public byte? PyamentModeTypeID { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50,ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("PaymentName")]
        public string PaymentName { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SalaryPaymentModeDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SalaryPaymentModeViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SalaryPaymentModeDTO, SalaryPaymentModeViewModel>.CreateMap();
            var vm = Mapper<SalaryPaymentModeDTO, SalaryPaymentModeViewModel>.Map(dto as SalaryPaymentModeDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SalaryPaymentModeViewModel, SalaryPaymentModeDTO>.CreateMap();
            var dto = Mapper<SalaryPaymentModeViewModel, SalaryPaymentModeDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SalaryPaymentModeDTO>(jsonString);
        }

    }
}
