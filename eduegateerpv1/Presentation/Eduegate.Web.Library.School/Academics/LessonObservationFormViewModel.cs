using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.ViewModels;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;
using System.Collections.Generic;
using Eduegate.Web.Library.School.Students;
using Eduegate.Web.Library.School.Gallery;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "LessonObservationForm", "CRUDModel.ViewModel")]
    [DisplayName("Details")]
    public class LessonObservationFormViewModel : BaseMasterViewModel
    {
        public LessonObservationFormViewModel()
        {
            //SubjectKnowledge = new List<SubjectKnowledgeViewModel>();
            SubjectKnowledge = new List<SubjectKnowledgeViewModel>() { new SubjectKnowledgeViewModel() };
        }

        public byte LessonObservationFormID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "")]
        [LookUp("LookUps.School")]
        [CustomDisplay("School")]
        public string School { get; set; }
        public int? SchoolID { get; set; }



        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "")]
        [LookUp("LookUps.Employee")]
        [CustomDisplay("Reviewer")]
        public string Reviewer { get; set; }
        public int? ReviewerID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Form No.")]
        public string FormNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine0 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "")]
        [LookUp("LookUps.Teacher")]
        [CustomDisplay("Teacher")]
        public string Teacher { get; set; }
        public int? TeacherID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Date")]
        public string DateString { get; set; }
        public DateTime? Date { get; set; }




        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]

        public int? ClassID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "")]
        [LookUp("LookUps.Section")]
        [CustomDisplay("Section")]
        public int? SectionID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "")]
        [LookUp("LookUps.Subject")]
        [CustomDisplay("Subject")]
        public int? SubjectID { get; set; }


        [ContainerType(Framework.Enums.ContainerTypes.Tab, "SubjectKnowledge", "CRUDModel.ViewModel.SubjectKnowledge")]
        [CustomDisplay("SubjectKnowledge")]
        public List<SubjectKnowledgeViewModel> SubjectKnowledge { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StreamDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LessonObservationFormViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<StreamDTO, LessonObservationFormViewModel>.CreateMap();
            var streamDTO = dto as StreamDTO;
            var vm = Mapper<StreamDTO, LessonObservationFormViewModel>.Map(streamDTO);
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();

            //vm.School = streamDTO.SchoolID.HasValue ? streamDTO.SchoolID.ToString() : null;
            //vm.AcademicYear = streamDTO.AcademicYearID.HasValue ? new KeyValueViewModel() { Key = streamDTO.AcademicYearID.ToString(), Value = streamDTO.AcademicYearName } : new KeyValueViewModel();
            //vm.StreamGroup = streamDTO.StreamGroupID.HasValue ? streamDTO.StreamGroupID.ToString() : null;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<LessonObservationFormViewModel, StreamDTO>.CreateMap();
            var dto = Mapper<LessonObservationFormViewModel, StreamDTO>.Map(this);

            //dto.SchoolID = string.IsNullOrEmpty(this.School) ? (byte?)null : byte.Parse(this.School);
            //dto.AcademicYearID = string.IsNullOrEmpty(this.AcademicYear.Key) ? (int?)null : int.Parse(this.AcademicYear.Key);
            //dto.StreamGroupID = string.IsNullOrEmpty(this.StreamGroup) ? (byte?)null : byte.Parse(this.StreamGroup);

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StreamDTO>(jsonString);
        }
    }
}