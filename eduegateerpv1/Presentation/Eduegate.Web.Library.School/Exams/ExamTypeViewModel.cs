using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Exams
{
    public class ExamTypeViewModel : BaseMasterViewModel
    {

      //  [Required]
       // [ControlType(Framework.Enums.ControlTypes.Label)]
       // [DisplayName("Exam TypeID")]
        public byte ExamTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ExamTypeDescription")]
        public string ExamTypeDescription { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ExamTypeDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ExamTypeViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ExamTypeDTO, ExamTypeViewModel>.CreateMap();
            var vm = Mapper<ExamTypeDTO, ExamTypeViewModel>.Map(dto as ExamTypeDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ExamTypeViewModel, ExamTypeDTO>.CreateMap();
            var dto = Mapper<ExamTypeViewModel, ExamTypeDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ExamTypeDTO>(jsonString);
        }
    }
}
