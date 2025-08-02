using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Lms;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "LessonObjective", "CRUDModel.ViewModel")]
    [DisplayName("Lesson Objective")]
    public class LessonObjectiveViewModel : BaseMasterViewModel
    {
        public LessonObjectiveViewModel()
        {
        }

        public byte LessonLearningObjectiveID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("Lesson Objective")]
        public string LessonLearningObjectiveName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }



        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LessonObjectiveDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LessonObjectiveViewModel>(jsonString);
        }

        // Convert ViewModel to DTO

        // Convert DTO to ViewModel
        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LessonObjectiveDTO, LessonObjectiveViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();

            var mpdto = dto as LessonObjectiveDTO;
            var vm = Mapper<LessonObjectiveDTO, LessonObjectiveViewModel>.Map(mpdto);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            vm.LessonLearningObjectiveID = mpdto.LessonLearningObjectiveID;
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LessonObjectiveViewModel, LessonObjectiveDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();

            var dto = Mapper<LessonObjectiveViewModel, LessonObjectiveDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            dto.LessonLearningObjectiveID = this.LessonLearningObjectiveID;
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LessonObjectiveDTO>(jsonString);
        }
    }

}