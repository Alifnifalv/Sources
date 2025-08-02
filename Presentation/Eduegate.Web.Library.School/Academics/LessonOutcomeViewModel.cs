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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Chapter", "CRUDModel.ViewModel")]
    [DisplayName("Chapter")]
    public class LessonOutcomeViewModel : BaseMasterViewModel
    {
        public LessonOutcomeViewModel()
        {
        }

        public byte LessonLearningOutcomeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("Lesson Outcome")]
        public string LessonLearningOutcomeName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LessonOutcomeDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ChapterViewModel>(jsonString);
        }

        // Convert ViewModel to DTO

        // Convert DTO to ViewModel
        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LessonOutcomeDTO, ChapterViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();

            var mpdto = dto as LessonOutcomeDTO;
            var vm = Mapper<LessonOutcomeDTO, LessonOutcomeViewModel>.Map(mpdto);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.LessonLearningOutcomeID = mpdto.LessonLearningOutcomeID;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LessonOutcomeViewModel, LessonOutcomeDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();

            var dto = Mapper<LessonOutcomeViewModel, LessonOutcomeDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            dto.LessonLearningOutcomeID = this.LessonLearningOutcomeID;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LessonOutcomeDTO>(jsonString);
        }
    }

}