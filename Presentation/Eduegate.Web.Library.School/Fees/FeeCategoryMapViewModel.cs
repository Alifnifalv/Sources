using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "FeeCategoryMap", "CRUDModel.ViewModel")]
    [DisplayName("Fee Category Map")]
    public class FeeCategoryMapViewModel : BaseMasterViewModel
    {
        public FeeCategoryMapViewModel()
        {
            IsPrimary = false;
        }

        public long CategoryFeeMapIID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.FeeMaster")]
        [CustomDisplay("FeeMaster")]
        public string FeeMaster { get; set; }
        public long? FeeMasterID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Categories")]
        [CustomDisplay("Categories")]
        public string Categories { get; set; }
        public long? CategoryID { get; set; }

        [ControlType(Eduegate.Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsPrimary")]
        public bool? IsPrimary { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as FeeCategoryMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeCategoryMapViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<FeeCategoryMapDTO, FeeCategoryMapViewModel>.CreateMap();
            var feeDto = dto as FeeCategoryMapDTO;
            var vm = Mapper<FeeCategoryMapDTO, FeeCategoryMapViewModel>.Map(feeDto);
            vm.FeeMaster = feeDto.FeeMasterID.HasValue ? feeDto.FeeMasterID.ToString() : null;
            vm.Categories = feeDto.CategoryID.HasValue ? feeDto.CategoryID.ToString() : null;
            vm.CategoryFeeMapIID = feeDto.CategoryFeeMapIID;
            //vm.FeeMaster = feeDto.FeeMaster.Key == null ? new KeyValueViewModel() { Key = null, Value = null } : new KeyValueViewModel() { Key = feeDto.FeeMaster.Key.ToString(), Value = feeDto.FeeMaster.Value };
            //vm.Categories = feeDto.FeeMaster.Key == null ? new KeyValueViewModel() { Key = null, Value = null } : new KeyValueViewModel() { Key = feeDto.Categories.Key.ToString(), Value = feeDto.Categories.Value };
            vm.IsPrimary = feeDto.IsPrimary;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<FeeCategoryMapViewModel, FeeCategoryMapDTO>.CreateMap();
            var dto = Mapper<FeeCategoryMapViewModel, FeeCategoryMapDTO>.Map(this);
            dto.IsPrimary = this.IsPrimary;
            dto.FeeMasterID = string.IsNullOrEmpty(this.FeeMaster) ? (long?)null : long.Parse(this.FeeMaster);
            dto.CategoryID = string.IsNullOrEmpty(this.Categories) ? (long?)null : long.Parse(this.Categories);
            //dto.FeeMaster = this.FeeMaster.Key == null ? new KeyValueDTO() { Key = null, Value = null } : new KeyValueDTO() { Key = this.FeeMaster.Key.ToString(), Value = this.FeeMaster.Value };
            //dto.Categories = this.Categories.Key == null ? new KeyValueDTO() { Key = null, Value = null } : new KeyValueDTO() { Key = this.Categories.Key.ToString(), Value = this.FeeMaster.Value };
            dto.CategoryFeeMapIID = this.CategoryFeeMapIID;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeCategoryMapDTO>(jsonString);
        }

    }
}