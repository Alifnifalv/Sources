using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Globalization;
using Eduegate.Web.Library.Common;


namespace Eduegate.Web.Library.School.Fees
{
    public class FeeTypeViewModel : BaseMasterViewModel
    {
        public FeeTypeViewModel()
        {
            IsRefundable = false;
        }

        public int  FeeTypeID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [CustomDisplay("FeeCode")]
        public string  FeeCode { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [CustomDisplay("TypeName")]
        public string  TypeName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("FeeGroup")]
        [LookUp("LookUps.FeeGroup")]
        public string FeeGroup { get; set; }
        public int? FeeGroupId { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("Fee Cycle")]
        //[LookUp("LookUps.FeeCycle")]
        //public string FeeCycle { get; set; }
        //public byte? FeeCycleId { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsRefundable")]
        public bool? IsRefundable { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as FeeTypeDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeTypeViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<FeeTypeDTO, FeeTypeViewModel>.CreateMap();
            var feeDto = dto as FeeTypeDTO;
            var vm = Mapper<FeeTypeDTO, FeeTypeViewModel>.Map(feeDto);
            vm.FeeGroup = feeDto.FeeGroupId.ToString();
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<FeeTypeViewModel, FeeTypeDTO>.CreateMap();
            var dto = Mapper<FeeTypeViewModel, FeeTypeDTO>.Map(this);
            dto.FeeGroupId= Convert.ToInt32(this.FeeGroup);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeTypeDTO>(jsonString);
        }
    }
}

