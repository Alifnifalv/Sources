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
using System;
using System.Linq;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "OnlineExams", "CRUDModel.ViewModel")]
    [DisplayName("Online Exams")]
    public class OnlineExamsViewModel : BaseMasterViewModel
    {
        public OnlineExamsViewModel()
        {
            //ExamQuestionGroupMaps = new List<ExamQuestionGroupMapViewModel>() { new ExamQuestionGroupMapViewModel() };
            //ObjectiveQuestionMarkMaps = new List<OnlineExamObjectiveMapViewModel>() { new OnlineExamObjectiveMapViewModel() };
            //SubjectiveQuestionMarkMaps = new List<OnlineExamSubjectiveMapViewModel>() { new OnlineExamSubjectiveMapViewModel() };
            SubjectiveQuestionMaps = new List<OnlineExamSubjectiveMarksMapViewModel>() { new OnlineExamSubjectiveMarksMapViewModel() };
            ObjectiveQuestionMaps = new List<OnlineExamObjectiveMarksMapViewModel>() { new OnlineExamObjectiveMarksMapViewModel() };
            //Subjects = new List<KeyValueViewModel>();
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

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false)]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public KeyValueViewModel StudentClass { get; set; }
        public int? ClassID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Subject", "Numeric", false, "SubjectChanges(CRUDModel.ViewModel)")]
        [LookUp("LookUps.Subject")]
        [CustomDisplay("Subject")]
        public KeyValueViewModel Subjects { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("QuestionSelections", "Numeric", false, "")]
        //[LookUp("LookUps.QuestionSelections")]
        //[CustomDisplay("QuestionSelections")]
        //public KeyValueViewModel QuestionSelection { get; set; }
        //public long? QuestionSelectionID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("TotalMarks")]
        public decimal? MaximumMarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine6 { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("ObjectiveQuestionMarks")]
        //public decimal? ObjectiveQuestionMarks { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", attribs: "ng-change=ChangeMarks(CRUDModel.ViewModel)")]
        [CustomDisplay("ObjectiveQuestionMarks")]
        public decimal? ObjectiveQuestionMarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "htmllabelinfo")]
        [CustomDisplay("TotalQuestions")]
        public string ObjTotalQuestions { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine8 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid, "fullwidth alignleft")]
        [DisplayName("")]
        public List<OnlineExamObjectiveMarksMapViewModel> ObjectiveQuestionMaps { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", attribs: "ng-change=ChangeMarks(CRUDModel.ViewModel)")]
        [CustomDisplay("SubjectiveQuestionMarks")]
        public decimal? SubjectiveQuestionMarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "htmllabelinfo")]
        [CustomDisplay("TotalQuestions")]
        public string SubTotalQuestions { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine7 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid, "fullwidth", Attributes4 = "colspan=8")]
        [DisplayName("")]
        public List<OnlineExamSubjectiveMarksMapViewModel> SubjectiveQuestionMaps { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

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
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", attribs: "ng-change=PercentageCalcuation('percentage')")]
        [CustomDisplay("MinimumPassPercentage%")]
        public double? PassPercentage { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", attribs: "ng-change=PercentageCalcuation('minMarks')")]
        [CustomDisplay("PassMarks")]
        public decimal? MinimumMarks { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("PassNos")]
        //public int? PassNos { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine0 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("MarkGrade", "Numeric", false)]
        [LookUp("LookUps.MarkGrade")]
        [CustomDisplay("MarkGrade")]
        public KeyValueViewModel MarkGrade { get; set; }
        public int? MarkGradeID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[LookUp("LookUps.OnlineExamTypes")]
        //[CustomDisplay("Type")]
        //public string OnlineExamType { get; set; }
        //public byte? OnlineExamTypeID { get; set; }

        //[Required]

        //[ControlType(Framework.Enums.ControlTypes.Grid)]
        //[DisplayName("Exam Question Groups")]
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
            //vm.QuestionSelection = new KeyValueViewModel()
            //{
            //    Key = onlineExamsDTO.QuestionSelectionID.ToString(),
            //    Value = onlineExamsDTO.QuestionSelectionName
            //};
            vm.StudentClass = onlineExamsDTO.Classes != null ? new KeyValueViewModel()
            {
                Key = onlineExamsDTO.Classes.Key,
                Value = onlineExamsDTO.Classes.Value
            } : new KeyValueViewModel();

            //vm.Subjects = new List<KeyValueViewModel>();
            vm.Subjects = onlineExamsDTO.Subject != null ? new KeyValueViewModel()
            {
                Key = onlineExamsDTO.Subject.Key,
                Value = onlineExamsDTO.Subject.Value
            } : new KeyValueViewModel();

            vm.ObjectiveQuestionMarks = onlineExamsDTO.ObjectiveMarks;
            vm.SubjectiveQuestionMarks = onlineExamsDTO.SubjectiveMarks;

            vm.ObjTotalQuestions = onlineExamsDTO.ObjTotalQuestions;
            vm.SubTotalQuestions = onlineExamsDTO.SubTotalQuestions;

            vm.MarkGrade = onlineExamsDTO.MarkGrade != null ? new KeyValueViewModel()
            {
                Key = onlineExamsDTO.MarkGrade.Key,
                Value = onlineExamsDTO.MarkGrade.Value
            } : new KeyValueViewModel();

            //vm.OnlineExamType = onlineExamsDTO.OnlineExamTypeID.HasValue ? onlineExamsDTO.OnlineExamTypeID.ToString() : null;

            //vm.ExamQuestionGroupMaps = new List<ExamQuestionGroupMapViewModel>();
            //if (onlineExamsDTO.OnlineExamQuestionGroupMaps != null)
            //{
            //    foreach (var examGroup in onlineExamsDTO.OnlineExamQuestionGroupMaps)
            //    {
            //        //vm.ExamQuestionGroupMaps.Add(new ExamQuestionGroupMapViewModel()
            //        //{
            //        //    ExamQuestionGroupMapIID = examGroup.ExamQuestionGroupMapIID,
            //        //    NumberOfQuestions = examGroup.NumberOfQuestions,
            //        //    QuestionGroup = examGroup.QuestionGroupID.HasValue ? new KeyValueViewModel()
            //        //    {
            //        //        Key = examGroup.QuestionGroupID.ToString(),
            //        //        Value = examGroup.GroupName
            //        //    } : new KeyValueViewModel(),
            //        //    MaximumMarks = examGroup.MaximumMarks,
            //        //    GroupTotalQnCount = examGroup.GroupTotalQnCount,
            //        //    NumberOfPassageQuestions = examGroup.NoOfPassageQuestions,
            //        //    TotalNoOfQuestions = Convert.ToInt64(examGroup.TotalQuestions),
            //        //    TotalNoOfPassageQuestions = Convert.ToInt64(examGroup.TotalPassageQuestions),
            //        //});
            //    }
            //}

            vm.ObjectiveQuestionMaps = new List<OnlineExamObjectiveMarksMapViewModel>();
            if (onlineExamsDTO.OnlineExamQuestionGroupMaps != null)
            {
                foreach (var examGroup in onlineExamsDTO.OnlineExamQuestionGroupMaps.Where(a => a.OnlineExamTypeID == 1))
                {
                    vm.ObjectiveQuestionMaps.Add(new OnlineExamObjectiveMarksMapViewModel()
                    {
                        ExamQuestionGroupMapIID = examGroup.ExamQuestionGroupMapIID,
                        ObjNoOfQuestions = examGroup.NumberOfQuestions,
                        ObjMarkGroup = Convert.ToInt64(examGroup.MaximumMarks),
                        //GroupTotalQnCount = examGroup.GroupTotalQnCount,
                        //NumberOfPassageQuestions = examGroup.NoOfPassageQuestions,
                        TotalNoOfQuestions = Convert.ToInt64(examGroup.TotalQuestions),
                        //TotalNoOfPassageQuestions = Convert.ToInt64(examGroup.TotalPassageQuestions),
                        TotalMarks = Convert.ToInt64(examGroup.TotalMarks),
                    });
                }
            }

            vm.SubjectiveQuestionMaps = new List<OnlineExamSubjectiveMarksMapViewModel>();
            if (onlineExamsDTO.OnlineExamQuestionGroupMaps != null)
            {
                foreach (var examGroup in onlineExamsDTO.OnlineExamQuestionGroupMaps.Where(a => a.OnlineExamTypeID == 2))
                {
                    vm.SubjectiveQuestionMaps.Add(new OnlineExamSubjectiveMarksMapViewModel()
                    {
                        ExamQuestionGroupMapIID = examGroup.ExamQuestionGroupMapIID,
                        SubNoOfQuestions = examGroup.NumberOfQuestions,
                        SubMarkGroup = Convert.ToInt64(examGroup.MaximumMarks),
                        //GroupTotalQnCount = examGroup.GroupTotalQnCount,
                        //NumberOfPassageQuestions = examGroup.NoOfPassageQuestions,
                        TotalNoOfQuestions = Convert.ToInt64(examGroup.TotalQuestions),
                        //TotalNoOfPassageQuestions = Convert.ToInt64(examGroup.TotalPassageQuestions),
                        TotalMarks = Convert.ToInt64(examGroup.TotalMarks),
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

            //dto.QuestionSelectionID = byte.Parse(this.QuestionSelection.Key);
            dto.Classes = this.StudentClass != null ? new KeyValueDTO()
            {
                Key = this.StudentClass.Key,
                Value = this.StudentClass.Value
            } : new KeyValueDTO();
            dto.MinimumMarks = this.MinimumMarks;
            dto.MaximumMarks = this.MaximumMarks;
            dto.MaximumDuration = this.MaximumDuration;
            dto.MinimumDuration = this.MinimumDuration;
            //dto.OnlineExamTypeID = string.IsNullOrEmpty(this.OnlineExamType) ? (byte?)null : byte.Parse(this.OnlineExamType);
            dto.ObjectiveMarks = this.ObjectiveQuestionMarks;
            dto.SubjectiveMarks = this.SubjectiveQuestionMarks;

            //dto.Subjects = new List<KeyValueDTO>();
            //if (this.Subjects != null)
            //{
            //    foreach (KeyValueViewModel vm in this.Subjects)
            //    {
            //        dto.Subjects.Add(new KeyValueDTO
            //        {
            //            Key = vm.Key,
            //            Value = vm.Value
            //        });
            //    }
            //}

            dto.Subject = this.Subjects != null ? new KeyValueDTO()
            {
                Key = this.Subjects.Key,
                Value = this.Subjects.Value
            } : new KeyValueDTO();

            dto.MarkGrade = this.MarkGrade != null ? new KeyValueDTO()
            {
                Key = this.MarkGrade.Key,
                Value = this.MarkGrade.Value
            } : new KeyValueDTO();


            dto.OnlineExamQuestionGroupMaps = new List<OnlineExamQuestionGroupMapDTO>();

            //if (this.ExamQuestionGroupMaps != null)
            //{
            //    //foreach (var exmGroupMap in this.ExamQuestionGroupMaps)
            //    //{
            //    //    if (exmGroupMap.QuestionGroup != null)
            //    //    {
            //    //        if (!string.IsNullOrEmpty(exmGroupMap.QuestionGroup.Key))
            //    //        {
            //    //            dto.OnlineExamQuestionGroupMaps.Add(new OnlineExamQuestionGroupMapDTO()
            //    //            {
            //    //                ExamQuestionGroupMapIID = exmGroupMap.ExamQuestionGroupMapIID,
            //    //                QuestionGroupID = int.Parse(exmGroupMap.QuestionGroup.Key),
            //    //                NumberOfQuestions = exmGroupMap.NumberOfQuestions,
            //    //                MaximumMarks = exmGroupMap.MaximumMarks,
            //    //                NoOfPassageQuestions = exmGroupMap.NumberOfPassageQuestions,
            //    //            });
            //    //        }
            //    //    }
            //    //}
            //}

            if (this.ObjectiveQuestionMaps != null)
            {
                foreach (var exmGroupMap in this.ObjectiveQuestionMaps)
                {
                    if (exmGroupMap.ObjMarkGroup != null)
                    {
                        dto.OnlineExamQuestionGroupMaps.Add(new OnlineExamQuestionGroupMapDTO()
                        {
                            ExamQuestionGroupMapIID = exmGroupMap.ExamQuestionGroupMapIID,
                            MaximumMarks = exmGroupMap.ObjMarkGroup,
                            NumberOfQuestions = exmGroupMap.ObjNoOfQuestions,
                            OnlineExamTypeID = 1,
                            TotalMarks = exmGroupMap.TotalMarks,
                        });
                    }
                }
            }

            if (this.SubjectiveQuestionMaps != null)
            {
                foreach (var exmGroupMap in this.SubjectiveQuestionMaps)
                {
                    if (exmGroupMap.SubMarkGroup != null)
                    {
                        dto.OnlineExamQuestionGroupMaps.Add(new OnlineExamQuestionGroupMapDTO()
                        {
                            ExamQuestionGroupMapIID = exmGroupMap.ExamQuestionGroupMapIID,
                            MaximumMarks = exmGroupMap.SubMarkGroup,
                            NumberOfQuestions = exmGroupMap.SubNoOfQuestions,
                            OnlineExamTypeID = 2,
                            TotalMarks = exmGroupMap.TotalMarks,
                        });
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