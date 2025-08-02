using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    public class OnlineExamsViewModel : BaseMasterViewModel
    {
        public OnlineExamsViewModel()
        {
            ExamQuestionGroupMaps = new List<ExamQuestionGroupMapViewModel>() { new ExamQuestionGroupMapViewModel() };
            Subjects = new List<KeyValueViewModel>();
        }

        public long OnlineExamIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Name")]
        public string Name { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MinimumMarks")]
        public decimal? MinimumMarks { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MaximumMarks")]
        public decimal? MaximumMarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false)]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public KeyValueViewModel StudentClass { get; set; }
        public int? ClassID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("QuestionSelections", "Numeric", false, "")]
        [LookUp("LookUps.QuestionSelections")]
        [CustomDisplay("QuestionSelections")]
        public KeyValueViewModel QuestionSelection { get; set; }
        public long? QuestionSelectionID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MinimumDurationInMinutes")]
        public float MinimumDuration { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MaximumDurationInMinutes")]
        public float MaximumDuration { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("PassPercentage")]
        public double? PassPercentage { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("PassNos")]
        public int? PassNos { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.OnlineExamTypes")]
        [CustomDisplay("Type")]
        public string OnlineExamType { get; set; }
        public byte? OnlineExamTypeID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Subject", "Numeric", true)]
        [LookUp("LookUps.Subject")]
        [CustomDisplay("Subject")]
        public List<KeyValueViewModel> Subjects { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Exam Question Groups")]
        public List<ExamQuestionGroupMapViewModel> ExamQuestionGroupMaps { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as OnlineExamsDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<OnlineExamsViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<OnlineExamsDTO, OnlineExamsViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var onlineExamsDTO = dto as OnlineExamsDTO;
            var vm = Mapper<OnlineExamsDTO, OnlineExamsViewModel>.Map(onlineExamsDTO);

            vm.MinimumMarks = onlineExamsDTO.MinimumMarks;
            vm.MaximumMarks = onlineExamsDTO.MaximumMarks;
            vm.QuestionSelection = new KeyValueViewModel()
            {
                Key = onlineExamsDTO.QuestionSelectionID.ToString(),
                Value = onlineExamsDTO.QuestionSelectionName
            };
            vm.StudentClass = onlineExamsDTO.Classes != null ? new KeyValueViewModel()
            {
                Key = onlineExamsDTO.Classes.Key,
                Value = onlineExamsDTO.Classes.Value
            } : new KeyValueViewModel();
            vm.OnlineExamType = onlineExamsDTO.OnlineExamTypeID.HasValue ? onlineExamsDTO.OnlineExamTypeID.ToString() : null;

            vm.ExamQuestionGroupMaps = new List<ExamQuestionGroupMapViewModel>();
            if (onlineExamsDTO.OnlineExamQuestionGroupMaps != null)
            {
                foreach (var examGroup in onlineExamsDTO.OnlineExamQuestionGroupMaps)
                {
                    vm.ExamQuestionGroupMaps.Add(new ExamQuestionGroupMapViewModel()
                    {
                        ExamQuestionGroupMapIID = examGroup.ExamQuestionGroupMapIID,
                        NumberOfQuestions = examGroup.NumberOfQuestions,
                        QuestionGroup = examGroup.QuestionGroupID.HasValue ? new KeyValueViewModel()
                        {
                            Key = examGroup.QuestionGroupID.ToString(),
                            Value = examGroup.GroupName
                        } : new KeyValueViewModel(),
                        MaximumMarks = examGroup.MaximumMarks,
                        GroupTotalQnCount = examGroup.GroupTotalQnCount,
                    });
                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<OnlineExamsViewModel, OnlineExamsDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<OnlineExamsViewModel, OnlineExamsDTO>.Map(this);

            dto.QuestionSelectionID = byte.Parse(this.QuestionSelection.Key);
            dto.Classes = this.StudentClass != null ? new KeyValueDTO()
            {
                Key = this.StudentClass.Key,
                Value = this.StudentClass.Value
            } : new KeyValueDTO();
            dto.MinimumMarks = this.MinimumMarks;
            dto.MaximumMarks = this.MaximumMarks;
            dto.MaximumDuration = this.MaximumDuration;
            dto.MinimumDuration = this.MinimumDuration;
            dto.OnlineExamTypeID = string.IsNullOrEmpty(this.OnlineExamType) ? (byte?)null : byte.Parse(this.OnlineExamType);

            dto.Subjects = new List<KeyValueDTO>();
            if (this.Subjects != null)
            {
                foreach (KeyValueViewModel vm in this.Subjects)
                {
                    dto.Subjects.Add(new KeyValueDTO
                    {
                        Key = vm.Key,
                        Value = vm.Value
                    });
                }
            }

            dto.OnlineExamQuestionGroupMaps = new List<OnlineExamQuestionGroupMapDTO>();

            if (this.ExamQuestionGroupMaps != null)
            {
                foreach (var exmGroupMap in this.ExamQuestionGroupMaps)
                {
                    if (exmGroupMap.QuestionGroup != null)
                    {
                        if (!string.IsNullOrEmpty(exmGroupMap.QuestionGroup.Key))
                        {
                            dto.OnlineExamQuestionGroupMaps.Add(new OnlineExamQuestionGroupMapDTO()
                            {
                                ExamQuestionGroupMapIID = exmGroupMap.ExamQuestionGroupMapIID,
                                QuestionGroupID = int.Parse(exmGroupMap.QuestionGroup.Key),
                                NumberOfQuestions = exmGroupMap.NumberOfQuestions,
                                MaximumMarks = exmGroupMap.MaximumMarks,
                            });
                        }
                    }
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<OnlineExamsDTO>(jsonString);
        }

    }
}