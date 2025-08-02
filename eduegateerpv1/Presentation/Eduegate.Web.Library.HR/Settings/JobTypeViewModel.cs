using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.HR.Designation;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Eduegate.Web.Library.HR.Settings
{
    public class JobTypeViewModel : BaseMasterViewModel
    {
     //   [Required]
      //  [ControlType(Framework.Enums.ControlTypes.Label)]
       // [DisplayName("Job Type ID")]
        public int JobTypeID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("JobTypeName")]
        public string JobTypeName { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as JobTypeDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<JobTypeViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<JobTypeDTO, JobTypeViewModel>.CreateMap();
            var vm = Mapper<JobTypeDTO, JobTypeViewModel>.Map(dto as JobTypeDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<JobTypeViewModel, JobTypeDTO>.CreateMap();
            var dto = Mapper<JobTypeViewModel, JobTypeDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<JobTypeDTO>(jsonString);
        }
    }
}
