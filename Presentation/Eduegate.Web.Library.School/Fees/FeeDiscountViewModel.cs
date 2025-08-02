using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Fees
{
    public class FeeDiscountViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Fee Discount ID")]
        public int  FeeDiscountID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("Discount Code")]
         public string  DiscountCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        //[RegularExpression("^[0-9]{2}", ErrorMessage ="Invalid Format")]
        [RegularExpression("[0-9]{0,1}.[0-9]{0,1}", ErrorMessage = "Invalid Format")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [DisplayName("Discount Percentage")]
        public decimal?  DiscountPercentage { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        [DisplayName("Amount")]
        public decimal?  Amount { get; set; }       
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as FeeDiscountDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeDiscountViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<FeeDiscountDTO, FeeDiscountViewModel>.CreateMap();
            var vm = Mapper<FeeDiscountDTO, FeeDiscountViewModel>.Map(dto as FeeDiscountDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<FeeDiscountViewModel, FeeDiscountDTO>.CreateMap();
            var dto = Mapper<FeeDiscountViewModel, FeeDiscountDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeDiscountDTO>(jsonString);
        }
    }
}

