using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "LessonPlan", "CRUDModel.ViewModel")]
    [DisplayName("LessonPlan")]
    public class LessonPlanViewModel : BaseMasterViewModel
    {
        public LessonPlanViewModel()
        {
            IsSendPushNotification = true;
            HideActionPlan = false;
            Class = new List<KeyValueViewModel>();
            //Section = new List<KeyValueViewModel>();
            //Subject = new KeyValueViewModel();
            LessonPlanAttachments = new List<LessonPlanAttachmentMapViewModel>() { new LessonPlanAttachmentMapViewModel() };
        }

        public long LessonPlanIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.AcademicYear")]
        [CustomDisplay("Academic Year")]
        public string AcademicYear { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine0 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", true, "")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public List<KeyValueViewModel> Class { get; set; }
        public int? ClassID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2(" Section", "Numeric", true, "")]
        [LookUp("LookUps.Section")]
        [CustomDisplay("Section")]
        public List<KeyValueViewModel> Section { get; set; }
        public int? SectionID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Subject", "Numeric", false, "")]
        [CustomDisplay("Subject")]
        [LookUp("LookUps.Subject")]
        public KeyValueViewModel Subject { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.MonthNames")]
        [CustomDisplay("Month")]
        public string Month { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("StartDate")]
        public string Date1String { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("EndDate")]
        public string Date2String { get; set; }
        public DateTime? Date2 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("LessonPlanTitle")]
        public string Title { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Content")]
        public string Content { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("LessonPlanStatus", "Numeric", false, "")]
        [CustomDisplay("LessonPlanStatus")]
        [LookUp("LookUps.LessonPlanStatus")]
        public KeyValueViewModel LessonPlanStatus { get; set; }


        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsSendPushNotification")]
        public bool? IsSendPushNotification { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.CheckBox, attribs: "ng-change='SyllabusCompleteCheckBox($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("Is Syllabus Completed")]
        public bool? IsSyllabusCompleted { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled=CRUDModel.ViewModel.HideActionPlan")]
        [CustomDisplay("Action Plan")]
        public string ActionPlan { get; set; }

        public bool? HideActionPlan { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Attachment")]
        public List<LessonPlanAttachmentMapViewModel> LessonPlanAttachments { get; set; }


        #region Unwanted fields to hide

        //[ControlType(Framework.Enums.ControlTypes.DatePicker)]
        //[CustomDisplay("MaximumExtendedDate")]
        //public string Date3String { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine3 { get; set; }

        //[Required]
        //[MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
        //[CustomDisplay("TotalHours")]
        //public decimal? TotalHours { get; set; }

        //[MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
        //[CustomDisplay("NoOfPeriods")]
        //public int? NumberOfPeriods { get; set; }

        //[MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
        //[CustomDisplay("No.OfClassTestPlanned(ForThisLesson)")]
        //public int? NumberOfClassTests { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine4 { get; set; }

        //[MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("Activities(SpecifyTheActivity(s)ToBCompleted)")]
        //public int? NumberOfActivityCompleted { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[LookUp("LookUps.ExamType")]
        //[CustomDisplay("ExpectedLearningOutcome")]
        //public string ExpectedLearningOutcome { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine5 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("SkillFocused")]
        //public string SkillFocused { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("AllignmentToVisionandMission")]
        //public string AllignmentToVisionAndMission { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("LocalConnectivityorGlobalConnectivity")]
        //public string Connectivity { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine6 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("Cross-DisciplinaryConnection")]
        //public string CrossDisciplinaryConnection { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("Introduction")]
        //public string Introduction { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("Closure")]
        //public string Closure { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine7 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("ForSEN")]
        //public string SEN { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("ForHighAchievers")]
        //public string HighAchievers { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("ForStudentsWhoNeedImprovement")]
        //public string StudentsWhoNeedImprovement { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine8 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("PostLessonEvaluation")]
        //public string PostLessonEvaluation { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("Reflections")]
        //public string Reflections { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextArea)]
        //[CustomDisplay("Resources")]
        //public string Resourses { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextArea)]
        //[CustomDisplay("TeachingMethodology")]
        //public string TeachingMethodology { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine11 { get; set; }

        //[MaxLength(1000, ErrorMessage = "Maximum Length should be within 1000!")]
        //[ControlType(Framework.Enums.ControlTypes.TextArea)]
        //[CustomDisplay("LearningExperiences")]
        //public string LearningExperiences { get; set; }

        //[MaxLength(1000, ErrorMessage = "Maximum Length should be within 1000!")]
        //[ControlType(Framework.Enums.ControlTypes.TextArea)]
        //[CustomDisplay("Activity")]
        //public string Activity { get; set; }

        //[MaxLength(1000, ErrorMessage = "Maximum Length should be within 1000!")]
        //[ControlType(Framework.Enums.ControlTypes.TextArea)]
        //[CustomDisplay("ExtendedActivityForStudents")]
        //public string HomeWorks { get; set; }

        #endregion

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LessonPlanDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LessonPlanViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LessonPlanDTO, LessonPlanViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var mpdto = dto as LessonPlanDTO;
            var vm = Mapper<LessonPlanDTO, LessonPlanViewModel>.Map(mpdto);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.LessonPlanIID = mpdto.LessonPlanIID;
            vm.Subject = mpdto.SubjectID.HasValue ? new KeyValueViewModel() { Key = mpdto.Subject.Key.ToString(), Value = mpdto.Subject.Value } : new KeyValueViewModel();
            vm.LessonPlanStatus = new KeyValueViewModel() { Key = mpdto.LessonPlanStatus.Key.ToString(), Value = mpdto.LessonPlanStatus.Value };
            vm.Date1String = mpdto.Date1.HasValue ? mpdto.Date1.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.Date2String = mpdto.Date2.HasValue ? mpdto.Date2.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.Month = mpdto.MonthID.HasValue ? mpdto.MonthID.ToString() : null;
            vm.AcademicYear = mpdto.AcademicYearID.HasValue ? mpdto.AcademicYearID.ToString() : null;
            vm.IsSendPushNotification = mpdto.IsSendPushNotification;
            vm.IsSyllabusCompleted = mpdto.IsSyllabusCompleted;
            vm.HideActionPlan = mpdto.IsSyllabusCompleted == true ? true : false;
            vm.ActionPlan = mpdto.ActionPlan;

            foreach (var map in mpdto.LessonPlanClassSectionMaps)
            {
                if (map.ClassID.HasValue && !vm.Class.Any(x => x.Key == map.ClassID.Value.ToString()))
                {
                    vm.Class.Add(new KeyValueViewModel()
                    {
                        Key = map.Class.Key,
                        Value = map.Class.Value
                    });
                }

                if (map.SectionID.HasValue && !vm.Section.Any(x => x.Key == map.SectionID.Value.ToString()))
                {
                    vm.Section.Add(new KeyValueViewModel()
                    {
                        Key = map.Section.Key,
                        Value = map.Section.Value
                    });
                }
            }

            #region removed fields from screen

            //vm.ExpectedLearningOutcome = mpdto.ExpectedLearningOutcomeID.HasValue ? mpdto.ExpectedLearningOutcomeID.ToString() : null;
            //vm.Date3String = mpdto.Date3.HasValue ? mpdto.Date3.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            //vm.Resourses = mpdto.Resourses;
            //vm.TeachingAid = mpdto.TeachingAidID.HasValue ? mpdto.TeachingAidID.ToString() : null;

            #endregion


            vm.LessonPlanAttachments = new List<LessonPlanAttachmentMapViewModel>();

            if (mpdto.LessonPlanAttachmentMap != null)
            {
                foreach (var attach in mpdto.LessonPlanAttachmentMap)
                {
                    if (attach.AttachmentReferenceID.HasValue || !string.IsNullOrEmpty(attach.AttachmentName))
                    {
                        vm.LessonPlanAttachments.Add(new LessonPlanAttachmentMapViewModel
                        {
                            LessonPlanAttachmentMapIID = attach.LessonPlanAttachmentMapIID,
                            LessonPlanID = attach.LessonPlanID,
                            ContentFileName = attach.AttachmentName,
                            ContentFileIID = attach.AttachmentReferenceID,
                            AttachmentDescription = attach.AttachmentDescription,
                        });
                    }
                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LessonPlanViewModel, LessonPlanDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<LessonPlanViewModel, LessonPlanDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.LessonPlanIID = this.LessonPlanIID;
            dto.AcademicYearID = string.IsNullOrEmpty(this.AcademicYear) ? (int?)null : int.Parse(this.AcademicYear);
            //dto.ClassID = string.IsNullOrEmpty(this.Class.Key) ? (int?)null : int.Parse(this.Class.Key);
            dto.SubjectID = string.IsNullOrEmpty(this.Subject.Key) ? (int?)null : int.Parse(this.Subject.Key);
            //dto.SectionID = string.IsNullOrEmpty(this.Section.Key) ? (int?)null : int.Parse(this.Section.Key);
            dto.LessonPlanStatusID = string.IsNullOrEmpty(this.LessonPlanStatus.Key) ? (byte?)null : byte.Parse(this.LessonPlanStatus.Key);
            dto.Date1 = string.IsNullOrEmpty(this.Date1String) ? (DateTime?)null : DateTime.ParseExact(this.Date1String, dateFormat, CultureInfo.InvariantCulture);
            dto.Date2 = string.IsNullOrEmpty(this.Date2String) ? (DateTime?)null : DateTime.ParseExact(this.Date2String, dateFormat, CultureInfo.InvariantCulture);
            dto.MonthID = string.IsNullOrEmpty(this.Month) ? (byte?)null : byte.Parse(this.Month);
            //dto.TeachingAidID = string.IsNullOrEmpty(this.TeachingAid) ? (byte?)null : byte.Parse(this.TeachingAid);
            dto.IsSendPushNotification = this.IsSendPushNotification;
            dto.IsSyllabusCompleted = this.IsSyllabusCompleted;
            dto.ActionPlan = this.ActionPlan;

            if (this.Class.Count == 0)
            {
                throw new Exception("Please select atlest one class!");
            }

            if (this.Section.Count == 0)
            {
                throw new Exception("Please select atlest one class!");
            }

            if (this.Class != null)
            {
                foreach (var splitCls in Class)
                {
                    foreach (var splitSec in Section)
                    {
                        dto.LessonPlanClassSectionMaps.Add(new LessonPlanClassSectionMapDTO()
                        {
                            SectionID = int.Parse(splitSec.Key),
                            ClassID = int.Parse(splitCls.Key),
                        });
                    }
                }
            }

            #region removed fields from screen

            //dto.Resourses = this.Resourses;
            //dto.ExpectedLearningOutcomeID = string.IsNullOrEmpty(this.ExpectedLearningOutcome) ? (byte?)null : byte.Parse(this.ExpectedLearningOutcome);
            //dto.Date3 = string.IsNullOrEmpty(this.Date3String) ? (DateTime?)null : DateTime.ParseExact(this.Date3String, dateFormat, CultureInfo.InvariantCulture);

            //dto.TeachingAid = new List<KeyValueDTO>();
            //foreach (KeyValueViewModel vm in this.TeachingAid)
            //{
            //    dto.TeachingAid.Add(new KeyValueDTO
            //    {
            //        Key = vm.Key,
            //        Value = vm.Value
            //    });
            //}

            #endregion

            dto.LessonPlanAttachmentMap = new List<LessonPlanAttachmentMapDTO>();

            if (this.LessonPlanAttachments.Count > 0)
            {
                foreach (var attachment in this.LessonPlanAttachments)
                {
                    if (attachment.ContentFileIID.HasValue || !string.IsNullOrEmpty(attachment.ContentFileName))
                    {
                        dto.LessonPlanAttachmentMap.Add(new LessonPlanAttachmentMapDTO
                        {
                            LessonPlanAttachmentMapIID = attachment.LessonPlanAttachmentMapIID,
                            LessonPlanID = attachment.LessonPlanID.HasValue ? attachment.LessonPlanID : null,
                            AttachmentReferenceID = attachment.ContentFileIID,
                            AttachmentName = attachment.ContentFileName,
                            AttachmentDescription = attachment.AttachmentDescription,
                            CreatedBy = attachment.CreatedBy,
                            UpdatedBy = attachment.UpdatedBy,
                            CreatedDate = attachment.CreatedDate,
                            UpdatedDate = attachment.UpdatedDate,
                        });
                    }
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LessonPlanDTO>(jsonString);
        }
    }
}