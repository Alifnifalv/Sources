using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using System.Collections.Generic;
using Eduegate.Domain;
using Microsoft.VisualBasic.FileIO;
using Eduegate.Framework.Enums;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    public class OnlineQuestionsViewModel : BaseMasterViewModel
    {
        public OnlineQuestionsViewModel()
        {
            TextAnswerTypeID = new Domain.Setting.SettingBL(null).GetSettingValue<byte?>("ANSWER_TYPEID_TEXTANSWER");
            MultipleChoiceTypeID = new Domain.Setting.SettingBL(null).GetSettingValue<byte?>("ANSWER_TYPEID_MULTIPLECHOICE");
            MultiSelectTypeID = new Domain.Setting.SettingBL(null).GetSettingValue<byte?>("ANSWER_TYPEID_MULTISELECT");

            QuestionOptionMaps = new List<QuestionOptionMapViewModel>() { new QuestionOptionMapViewModel() };
        }

        public long QuestionIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='AnswerTypeChanges(CRUDModel.ViewModel)'")]
        [LookUp("LookUps.AnswerTypes")]
        [DisplayName("Answer type")]
        public string AnswerTypes { get; set; }
        public long? AnswerTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("QuestionGroups", "Numeric", false, "QuestionGroupChanges(CRUDModel.ViewModel)")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=QuestionGroups", "LookUps.QuestionGroups")]
        [DisplayName("Question group")]
        public KeyValueViewModel QuestionGroup { get; set; }
        public long? QuestionGroupID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Subject")]
        public string SubjectName { get; set; }
        public KeyValueViewModel Subject { get; set; }
        public int? SubjectID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        [DisplayName("Score")]
        public decimal Points { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='PassageQuestionChanges(CRUDModel.ViewModel)'")]
        [LookUp("LookUps.PassageQuestions")]
        [DisplayName("Passage Question")]
        public string PassageQuestion { get; set; }
        public long? PassageQuestionID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "alignleft")]
        [DisplayName("Passage Question Description")]
        public string PassageQuestionName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.RichTextEditor, "w-1000px")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        [CustomDisplay("")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.UserProfile, "ProfileUrl", "")]
        public string ProfileUrl { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Question options")]
        public List<QuestionOptionMapViewModel> QuestionOptionMaps { get; set; }

        public byte? TextAnswerTypeID { get; set; }
        public byte? MultipleChoiceTypeID { get; set; }
        public byte? MultiSelectTypeID { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as OnlineQuestionsDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<OnlineQuestionsViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<OnlineQuestionsDTO, OnlineQuestionsViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<QuestionOptionMapDTO, QuestionOptionMapViewModel>.CreateMap();
            var qnDto = dto as OnlineQuestionsDTO;
            var vm = Mapper<OnlineQuestionsDTO, OnlineQuestionsViewModel>.Map(qnDto);

            vm.AnswerTypes = qnDto.AnswerTypeID.HasValue ? qnDto.AnswerTypeID.ToString() : null;
            vm.Subject = qnDto.SubjectID.HasValue ? new KeyValueViewModel() { Key = qnDto.SubjectID.ToString(), Value = qnDto.SubjectName } : new KeyValueViewModel();
            vm.QuestionGroup = qnDto.QuestionGroupID.HasValue ? new KeyValueViewModel() { Key = qnDto.QuestionGroupID.ToString(), Value = qnDto.QuestionGroupName } : new KeyValueViewModel();
            vm.SubjectName = qnDto.SubjectName != null ? qnDto.SubjectName : null;
            vm.ProfileUrl = qnDto.Docfile;
            vm.PassageQuestion = qnDto.PassageQuestionID.HasValue ? qnDto.PassageQuestionID.ToString() : null;
            vm.PassageQuestionName = qnDto.PassageQuestionName;

            vm.QuestionOptionMaps = new List<QuestionOptionMapViewModel>();
            foreach (var qnOption in qnDto.QuestionOptionMaps)
            {
                if (qnOption.OptionText != null)
                {
                    vm.QuestionOptionMaps.Add(new QuestionOptionMapViewModel()
                    {
                        QuestionOptionMapIID = qnOption.QuestionOptionMapIID,
                        QuestionID = qnOption.QuestionID,
                        ContentFileName = qnOption.ImageName,
                        ContentFileIID = qnOption.ContentID,
                        OptionText = qnOption.OptionText,
                        IsCorrectAnswer = qnOption.IsCorrectAnswer,
                        OrderNo = qnOption.OrderNo,
                    });
                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<OnlineQuestionsViewModel, OnlineQuestionsDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<QuestionOptionMapViewModel, QuestionOptionMapDTO>.CreateMap();
            var dto = Mapper<OnlineQuestionsViewModel, OnlineQuestionsDTO>.Map(this);

            dto.AnswerTypeID = string.IsNullOrEmpty(this.AnswerTypes) ? (byte?)null : byte.Parse(this.AnswerTypes);
            dto.SubjectID = this.SubjectID != null ? this.SubjectID : null;
            dto.QuestionGroupID = this.QuestionGroup == null || string.IsNullOrEmpty(this.QuestionGroup.Key) ? (int?)null : int.Parse(this.QuestionGroup.Key);
            dto.QuestionIID = this.QuestionIID;
            dto.Docfile = this.ProfileUrl;
            dto.PassageQuestionID = string.IsNullOrEmpty(this.PassageQuestion) ? (byte?)null : byte.Parse(this.PassageQuestion);
            dto.QuestionOptionMaps = new List<QuestionOptionMapDTO>();

            var optionOrderNo = 0;
            foreach (var qnOptionMap in this.QuestionOptionMaps)
            {
                if (qnOptionMap.OptionText != null)
                {
                    optionOrderNo ++;

                    dto.QuestionOptionMaps.Add(new QuestionOptionMapDTO()
                    {
                        QuestionOptionMapIID = qnOptionMap.QuestionOptionMapIID,
                        QuestionID = qnOptionMap.QuestionID,
                        OptionText = qnOptionMap.OptionText,
                        ContentID = qnOptionMap.ContentFileIID,
                        ImageName = qnOptionMap.ContentFileName,
                        IsCorrectAnswer = qnOptionMap.IsCorrectAnswer,
                        OrderNo = qnOptionMap.OrderNo.HasValue ? qnOptionMap.OrderNo : optionOrderNo,
                    });
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<OnlineQuestionsDTO>(jsonString);
        }

    }
}