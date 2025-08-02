using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Fees
{
    public class FeePaymentModesViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Payment Mode ID")]
        public byte PaymentModeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Payment Mode Name")]
        public string PaymentModeName { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as FeePaymentModesDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeePaymentModesViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<FeePaymentModesDTO, FeePaymentModesViewModel>.CreateMap();
            var feeDto = dto as FeePaymentModesDTO;
            var vm = Mapper<FeePaymentModesDTO, FeePaymentModesViewModel>.Map(feeDto);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<FeePaymentModesViewModel, FeePaymentModesDTO>.CreateMap();
            var dto = Mapper<FeePaymentModesViewModel, FeePaymentModesDTO>.Map(this);
            return dto;
        }                                                                                                                                                                           

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeePaymentModesDTO>(jsonString);
        }

    }
}
