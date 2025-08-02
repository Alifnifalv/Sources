using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Fines;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Fines
{
    public class FineMasterViewModel : BaseMasterViewModel
    {
        public int FineMasterID { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("FineCode")]
        public string FineCode { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("FineName")]
        public string FineName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("FineType")]
        [LookUp("LookUps.FeeFineType")]
        public string FeeFineType { get; set; }
        public short? FeeFineTypeID { get; set; }

        [Required]
        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Amount")]
        public decimal? Amount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine01 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("LedgerAccount")]
        [LookUp("LookUps.LedgerAccount")]
        public string LedgerAccount { get; set; }
        public long? LedgerAccountID { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as FineMasterDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<FineMasterViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<FineMasterDTO, FineMasterViewModel>.CreateMap();
            var fmDto = dto as FineMasterDTO;
            var vm = Mapper<FineMasterDTO, FineMasterViewModel>.Map(fmDto);
          
            FeeFineType = fmDto.FeeFineTypeID.ToString();
            LedgerAccount = fmDto.LedgerAccountID.ToString();
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<FineMasterViewModel, FineMasterDTO>.CreateMap();

            var dto = Mapper<FineMasterViewModel, FineMasterDTO>.Map(this);
            dto.FeeFineTypeID = short.Parse(FeeFineType);
            dto.LedgerAccountID = long.Parse(LedgerAccount);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<FineMasterDTO>(jsonString);
        }
    }
}