using Eduegate.Domain.Entity.School.Models;
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
using ServiceStack;
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
            LessonPlanTopics = new List<LessonPlanTopicMapsViewModel>() { new LessonPlanTopicMapsViewModel() };
            LessonPlanTasks = new List<LessonPlanTaskViewModel>() { new LessonPlanTaskViewModel() };
            TeachingAids = new List<KeyValueViewModel>();
            LessonPlanLearningOutcomes = new List<KeyValueViewModel>();
            LessonPlanLearningObjectives = new List<KeyValueViewModel>();
            SubjectUnit = new KeyValueViewModel();

        }

        public long LessonPlanIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-click='SaveUploadedFiles($event, $element, CRUDModel.ViewModel)'")]
        [CustomDisplay("Upload lesson plan pdf")]
        public string LessonplanUploadButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("pdf")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "", "")]
        public string pdf { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.AcademicYear")]
        [CustomDisplay("Academic Year")]
        public string AcademicYear { get; set; }


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

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("SubjectUnit", "Numeric", false, "")]
        [CustomDisplay("Unit")]
        [LookUp("LookUps.SubjectUnits")]
        public KeyValueViewModel SubjectUnit { get; set; }

        public long? UnitID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }


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
        public string NewLine1 { get; set; }

        [Required]
        //[MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("LessonPlanTitle")]
        public string Title { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }



        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("LessonPlanLearningObjective", "Numeric", true, "")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=LessonPlanLearningObjectives", "LookUps.LessonPlanLearningObjective")]
        [DisplayName("Lesson Plan Learning Objectives")]
        public List<KeyValueViewModel> LessonPlanLearningObjectives { get; set; }
        public long? LessonLearningObjectiveID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("LessonPlanLearningOutcome", "Numeric", true, "")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=LessonPlanLearningOutcomes", "LookUps.LessonPlanLearningOutcome")]
        [DisplayName("Lesson Plan Learning Outcomes")]
        public List<KeyValueViewModel> LessonPlanLearningOutcomes { get; set; }
        public long? LessonLearningOutcomeID { get; set; }



        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }


      

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
        [DisplayName("No Of Periods")]
        public int? NumberOfPeriods { get; set; }

        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
        [DisplayName("No.Of Class Test Planned (For This Lesson)")]
        public int? NumberOfClassTests { get; set; }

        [Required]
        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
        [DisplayName("Total Hours")]
        public decimal? TotalHours { get; set; }



        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("LessonPlanStatus", "Numeric", false, "")]
        [CustomDisplay("LessonPlanStatus")]
        [LookUp("LookUps.LessonPlanStatus")]
        public KeyValueViewModel LessonPlanStatus { get; set; }


        [ControlType(Framework.Enums.ControlTypes.CheckBox, attribs: "ng-change='SyllabusCompleteCheckBox($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("Is Syllabus Completed")]
        public bool? IsSyllabusCompleted { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine9 { get; set; }



        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled=CRUDModel.ViewModel.HideActionPlan")]
        [CustomDisplay("Action Plan")]
        public string ActionPlan { get; set; }

        public bool? HideActionPlan { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("TeachingAid", "String", true, "")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=TeachingAids", "LookUps.TeachingAids")]
        [DisplayName("Teaching Aids To Be Used")]
        public List<KeyValueViewModel> TeachingAids { get; set; }
        public int? TeachingAidID { get; set; }



        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsSendPushNotification")]
        public bool? IsSendPushNotification { get; set; }

     

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine11 { get; set; }


        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Lesson Plan Topics")]
        public List<LessonPlanTopicMapsViewModel> LessonPlanTopics { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine8 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Lesson Plan Task")]
        public List<LessonPlanTaskViewModel> LessonPlanTasks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Attachment")]
        public List<LessonPlanAttachmentMapViewModel> LessonPlanAttachments { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine7 { get; set; }

        [MaxLength(1000, ErrorMessage = "Maximum Length should be within 1000!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Cross-disciplinary connection")]
        public string CrossDisciplinaryConnection { get; set; }


        [MaxLength(1000, ErrorMessage = "Maximum Length should be within 1000!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Alignment to Vision and Mission")]
        public string AllignmentToVisionAndMission { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine10 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Content")]
        public string Content { get; set; }

        [MaxLength(1000, ErrorMessage = "Maximum Length should be within 1000!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Learning Experiences")]
        public string LearningExperiences { get; set; }

        [MaxLength(1000, ErrorMessage = "Maximum Length should be within 1000!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Activity")]
        public string Activity { get; set; }

        [MaxLength(1000, ErrorMessage = "Maximum Length should be within 1000!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Home Work")]
        public string HomeWorks { get; set; }




        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine6 { get; set; }



        [MaxLength(1000, ErrorMessage = "Maximum Length should be within 1000!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("SEN")]
        public string SEN { get; set; }


        [MaxLength(1000, ErrorMessage = "Maximum Length should be within 1000!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("High Achievers")]
        public string HighAchievers { get; set; }


        [MaxLength(1000, ErrorMessage = "Maximum Length should be within 1000!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("For Students who need improvement")]
        public string StudentsWhoNeedImprovement { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine12 { get; set; }


        [MaxLength(1000, ErrorMessage = "Maximum Length should be within 1000!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Post Class Reflection")]
        public string PostLessonEvaluation { get; set; }

       

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
            Mapper<LessonPlanTopicMapDTO, LessonPlanTopicMapsViewModel>.CreateMap();
            Mapper<LessonPlanTaskMapDTO, LessonPlanTopicTaskViewModel>.CreateMap();
            Mapper<LessonPlanTaskMapDTO, LessonPlanTaskViewModel>.CreateMap();
            Mapper<LessonPlanOutcomeMapDTO, LessonPlanLearningOutcomeViewModel>.CreateMap();


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
            vm.TotalHours = mpdto.TotalHours;
            vm.NumberOfPeriods = mpdto.NumberOfPeriods;
            vm.NumberOfClassTests = mpdto.NumberOfClassTests;
            vm.LearningExperiences = mpdto.LearningExperiences;
            vm.HomeWorks = mpdto.HomeWorks;
            vm.AllignmentToVisionAndMission = mpdto.AllignmentToVisionAndMission;
            vm.CrossDisciplinaryConnection = mpdto.CrossDisciplinaryConnection;
            vm.SEN = mpdto.SEN;
            vm.HighAchievers = mpdto.HighAchievers;
            vm.StudentsWhoNeedImprovement = mpdto.StudentsWhoNeedImprovement;
            vm.PostLessonEvaluation = mpdto.PostLessonEvaluation;
            vm.Activity = mpdto.Activity;
            vm.SubjectUnit =  mpdto.UnitID.HasValue ? new KeyValueViewModel() { Key = mpdto.SubjectUnit.Key.ToString(), Value = mpdto.SubjectUnit.Value }: new KeyValueViewModel();
            vm.UnitID = mpdto.UnitID;

            vm.TeachingAids = new List<KeyValueViewModel>();
            foreach (var aid in mpdto.TeachingAids)
            {
                vm.TeachingAids.Add(new KeyValueViewModel
                {
                    Key = aid.Key,
                    Value = aid.Value

                });
            }

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

            vm.LessonPlanTopics = new List<LessonPlanTopicMapsViewModel>();

            if (mpdto.LessonPlanTopicMap != null)
            {
                foreach (var topic in mpdto.LessonPlanTopicMap)
                {
                    if (!string.IsNullOrEmpty(topic.LectureCode))
                    {
                        var topicAttach = new List<LessonPlanTopicAttachmentViewModel>();
                        if (topic.LessonPlanTopicAttachments != null)
                        {
                            foreach (var topicFile in topic.LessonPlanTopicAttachments)
                            {
                                if (!string.IsNullOrEmpty(topicFile.AttachmentName))
                                {
                                    topicAttach.Add(new LessonPlanTopicAttachmentViewModel
                                    {
                                        LessonPlanTopicAttachmentMapIID = topicFile.LessonPlanTopicAttachmentMapIID,
                                        LessonPlanTopicMapID = topicFile.LessonPlanTopicMapID,
                                        AttachmentName = topicFile.AttachmentName,
                                        AttachmentReferenceID = topicFile.AttachmentReferenceID,
                                        ProfileUrl = Convert.ToString(topicFile.AttachmentReferenceID),
                                        ProfileFile = Convert.ToString(topicFile.AttachmentReferenceID),

                                    });
                                }
                            }
                        }
                        var topicTask = new List<LessonPlanTopicTaskViewModel>();
                        if (topic.LessonPlanTopicTask != null)
                        {
                            foreach (var taskData in topic.LessonPlanTopicTask)
                            {
                                if (!string.IsNullOrEmpty(taskData.Task))
                                {
                                    List<LessonPlanTopicAttachmentViewModel> topicTaskAttach = new List<LessonPlanTopicAttachmentViewModel>();
                                    foreach (var tpTaskAttach in taskData.LessonPlanTaskAttachment)
                                    {
                                        if (!string.IsNullOrEmpty(tpTaskAttach.AttachmentName))
                                        {
                                            topicTaskAttach.Add(new LessonPlanTopicAttachmentViewModel
                                            {
                                                LessonPlanTopicAttachmentMapIID = tpTaskAttach.LessonPlancTaskAttachmentMapIID,
                                                //LessonPlanTopicMapID = topic.LessonPlanTopicMapsIID,
                                                AttachmentName = tpTaskAttach.AttachmentName,
                                                AttachmentReferenceID = tpTaskAttach.AttachmentReferenceID,
                                                ProfileUrl = Convert.ToString(tpTaskAttach.AttachmentReferenceID),
                                                ProfileFile = Convert.ToString(tpTaskAttach.AttachmentReferenceID),
                                            });
                                        }
                                    }
                                    topicTask.Add(new LessonPlanTopicTaskViewModel
                                    {
                                        Task = taskData.Task,
                                        LessonPlanTaskMapIID = taskData.LessonPlanTaskMapIID,
                                        LessonPlanID = taskData.LessonPlanID,
                                        TimeDuration = taskData.TimeDuration,

                                        // Safely check if TaskType is null before accessing its properties
                                        TaskType = taskData.TaskType != null && !string.IsNullOrEmpty(taskData.TaskType.Key)
                                            ? new KeyValueViewModel() { Key = taskData.TaskType.Key, Value = taskData.TaskType.Value }
                                            : null
                                                });

                                }
                            }
                        }
                        vm.LessonPlanTopics.Add(
                        new LessonPlanTopicMapsViewModel()
                        {
                            Topic = topic.Topic,
                            LectureCode = topic.LectureCode,
                            Period = topic.Period,
                            LessonPlanID = topic.LessonPlanID,
                            LessonPlanTopicMapIID = topic.LessonPlanTopicMapsIID,
                            LessonPlanFileUpload = topicAttach,
                            LessonPlanTopicTaskMap = topicTask //
                        }
                        );
                    }
                }
            }

            if (mpdto.LessonPlanTask != null)
            {
                foreach (var task in mpdto.LessonPlanTask)
                {
                    if (!string.IsNullOrEmpty(task.Task))
                    {
                        List<LessonPlanTaskAttachmentViewModel> taskAttach = new List<LessonPlanTaskAttachmentViewModel>();

                        // Check if LessonPlanTaskAttachment is null before iterating
                        if (task.LessonPlanTaskAttachment != null)
                        {
                            foreach (var taskFile in task.LessonPlanTaskAttachment)
                            {
                                if (!string.IsNullOrEmpty(taskFile.AttachmentName))
                                {
                                    taskAttach.Add(new LessonPlanTaskAttachmentViewModel
                                    {
                                        LessonPlancTaskAttachmentMapIID = taskFile.LessonPlancTaskAttachmentMapIID,
                                        LessonPlanTaskMapID = taskFile.LessonPlanTaskMapID,
                                        AttachmentReferenceID = taskFile.AttachmentReferenceID,
                                        AttachmentName = taskFile.AttachmentName,
                                        ProfileUrl = Convert.ToString(taskFile.AttachmentReferenceID),
                                        ProfileFile = Convert.ToString(taskFile.AttachmentReferenceID),
                                    });
                                }
                            }
                        }

                        vm.LessonPlanTasks.Add(new LessonPlanTaskViewModel()
                        {
                            Task = task.Task,
                            LessonPlanTaskMapIID = task.LessonPlanTaskMapIID,
                            LessonPlanID = task.LessonPlanID,
                            TimeDuration = task.TimeDuration,
                            LessonPlanTaskAttach = taskAttach,

                            TaskType = task.TaskType != null && !string.IsNullOrEmpty(task.TaskType.Key)
                                ? new KeyValueViewModel() { Key = task.TaskType.Key, Value = task.TaskType.Value }
                                : null
                        });
                    }
                }
            }


            foreach (var map in mpdto.LessonPlanLearningOutcomeMap)
            {
                if (map.LessonLearningOutcomeID != 0
                    && !vm.LessonPlanLearningOutcomes.Any(x => x.Key == map.LessonLearningOutcomeID.ToString()))
                {
                        vm.LessonPlanLearningOutcomes.Add(new KeyValueViewModel()
                        {
                            Key = map.LessonLearningOutcomeID.ToString(), // Use LessonLearningOutcomeID as Key
                            Value = map.LessonLearningOutcomeName // Ensure Value exists
                        });
                    
                }
            }

            foreach (var map in mpdto.LessonPlanLearningObjectiveMap)
            {
                if (map.LessonLearningObjectiveID != 0
                    && !vm.LessonPlanLearningObjectives.Any(x => x.Key == map.LessonLearningObjectiveID.ToString()))
                {
                    vm.LessonPlanLearningObjectives.Add(new KeyValueViewModel()
                    {
                        Key = map.LessonLearningObjectiveID.ToString(), // Use LessonLearningObjectiveID as Key
                        Value = map.LessonLearningObjectiveName // Ensure Value exists
                    });

                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LessonPlanViewModel, LessonPlanDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<LessonPlanTopicMapsViewModel, LessonPlanTopicMapDTO>.CreateMap();
            Mapper<LessonPlanTopicTaskViewModel, LessonPlanTaskMapDTO>.CreateMap();
            Mapper<LessonPlanTaskViewModel, LessonPlanTaskMapDTO>.CreateMap();

            var dto = Mapper<LessonPlanViewModel, LessonPlanDTO>.Map(this);


            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

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
            dto.TotalHours = this.TotalHours;
            dto.NumberOfPeriods = (byte?)this.NumberOfPeriods;
            dto.NumberOfClassTests = (byte?)this.NumberOfClassTests;
            dto.LearningExperiences = this.LearningExperiences;
            dto.AllignmentToVisionAndMission = this.AllignmentToVisionAndMission;
            dto.CrossDisciplinaryConnection = this.CrossDisciplinaryConnection;
            dto.SEN = this.SEN;
            dto.HighAchievers = this.HighAchievers;
            dto.StudentsWhoNeedImprovement = this.StudentsWhoNeedImprovement;
            dto.PostLessonEvaluation = this.PostLessonEvaluation;
            dto.HomeWorks = this.HomeWorks;
            dto.Activity = this.Activity;
            dto.UnitID = string.IsNullOrEmpty(this.SubjectUnit.Key) ? (int?)null : int.Parse(this.SubjectUnit.Key);

            dto.TeachingAids = new List<KeyValueDTO>();
            foreach (var vm in this.TeachingAids)
            {
                dto.TeachingAids.Add(new KeyValueDTO
                {
                    Key = vm.Key,
                    Value = vm.Value
                });
            }

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

            dto.LessonPlanTopicMap = new List<LessonPlanTopicMapDTO>();

            foreach (var topic in this.LessonPlanTopics)
            {
                if (!string.IsNullOrEmpty(topic.LectureCode))
                {
                    var topicAttach = new List<LessonPlanTopicAttachmentMapDTO>();
                    foreach (var topicFile in topic.LessonPlanFileUpload)
                    {
                        if (!string.IsNullOrEmpty(topicFile.AttachmentName))
                        {
                            topicAttach.Add(new LessonPlanTopicAttachmentMapDTO
                            {
                                LessonPlanTopicAttachmentMapIID = topicFile.LessonPlanTopicAttachmentMapIID,
                                LessonPlanTopicMapID = topicFile.LessonPlanTopicMapID,
                                AttachmentReferenceID = string.IsNullOrEmpty(topicFile.ProfileUrl) ? (long?)null : long.Parse(topicFile.ProfileUrl),
                                AttachmentName = topicFile.AttachmentName,
                                CreatedBy = topicFile.CreatedBy,
                                UpdatedBy = topicFile.UpdatedBy,
                                CreatedDate = topicFile.CreatedDate,
                                UpdatedDate = topicFile.UpdatedDate,
                            });
                        }
                    }

                    var topicTask = new List<LessonPlanTaskMapDTO>();
                    foreach (var taskData in topic.LessonPlanTopicTaskMap)
                    {
                        if (!string.IsNullOrEmpty(taskData.Task))
                        {
                            List<LessonPlanTaskAttachmentMapDTO> topicTaskAttach = new List<LessonPlanTaskAttachmentMapDTO>();
                            foreach (var tpTaskAttach in taskData.LessonPlanTopicTaskAttach)
                            {
                                if (!string.IsNullOrEmpty(tpTaskAttach.AttachmentName))
                                {
                                    topicTaskAttach.Add(new LessonPlanTaskAttachmentMapDTO
                                    {
                                        LessonPlancTaskAttachmentMapIID = tpTaskAttach.LessonPlancTaskAttachmentMapIID,
                                        LessonPlanTaskMapID = tpTaskAttach.LessonPlanTaskMapID,
                                        AttachmentReferenceID = string.IsNullOrEmpty(tpTaskAttach.ProfileUrl) ? (long?)null : long.Parse(tpTaskAttach.ProfileUrl),
                                        AttachmentName = tpTaskAttach.AttachmentName,
                                        CreatedBy = tpTaskAttach.CreatedBy,
                                        UpdatedBy = tpTaskAttach.UpdatedBy,
                                        CreatedDate = tpTaskAttach.CreatedDate,
                                        UpdatedDate = tpTaskAttach.UpdatedDate,
                                    });
                                }
                            }
                            topicTask.Add(new LessonPlanTaskMapDTO
                            {
                                Task = taskData.Task,
                                TaskTypeID = taskData.TaskType != null && !string.IsNullOrEmpty(taskData.TaskType.Key)
                                    ? byte.Parse(taskData.TaskType.Key) // Convert safely
                                    : (byte?)null, // Assign null if TaskType is missing
                                LessonPlanTaskMapIID = taskData.LessonPlanTaskMapIID,
                                LessonPlanID = taskData.LessonPlanID,
                                LessonPlanTopicMapID = taskData.LessonPlanTopicMapID,
                                TimeDuration = taskData.TimeDuration,

                                LessonPlanTaskAttachment = topicTaskAttach,
                            });
                        }
                    }

                    dto.LessonPlanTopicMap.Add(new LessonPlanTopicMapDTO()
                    {
                        Topic = topic.Topic,
                        LectureCode = topic.LectureCode,
                        Period = topic.Period,
                        LessonPlanID = topic.LessonPlanID,
                        LessonPlanTopicMapsIID = topic.LessonPlanTopicMapIID,
                        LessonPlanTopicAttachments = topicAttach,
                        LessonPlanTopicTask = topicTask,
                    });
                }
            }
            dto.LessonPlanTask = new List<LessonPlanTaskMapDTO>();
            foreach (var task in this.LessonPlanTasks)
            {
                if (!string.IsNullOrEmpty(task.Task))
                {
                    List<LessonPlanTaskAttachmentMapDTO> taskAttach = new List<LessonPlanTaskAttachmentMapDTO>();
                    foreach (var taskFile in task.LessonPlanTaskAttach)
                    {
                        if (!string.IsNullOrEmpty(taskFile.AttachmentName))
                        {
                            taskAttach.Add(new LessonPlanTaskAttachmentMapDTO
                            {
                                LessonPlancTaskAttachmentMapIID = taskFile.LessonPlancTaskAttachmentMapIID,
                                LessonPlanTaskMapID = taskFile.LessonPlanTaskMapID,
                                AttachmentReferenceID = string.IsNullOrEmpty(taskFile.ProfileUrl) ? (long?)null : long.Parse(taskFile.ProfileUrl),
                                AttachmentName = taskFile.AttachmentName,
                                CreatedBy = taskFile.CreatedBy,
                                UpdatedBy = taskFile.UpdatedBy,
                                CreatedDate = taskFile.CreatedDate,
                                UpdatedDate = taskFile.UpdatedDate,
                            });
                        }
                    }

                    dto.LessonPlanTask.Add(new LessonPlanTaskMapDTO()
                    {
                        Task = task.Task,
                        TaskTypeID = task.TaskType != null && !string.IsNullOrEmpty(task.TaskType.Key)
                            ? byte.Parse(task.TaskType.Key) // Convert safely
                            : (byte?)null, // Assign null if TaskType is missing
                        LessonPlanTaskMapIID = task.LessonPlanTaskMapIID,
                        LessonPlanID = task.LessonPlanID,
                        TimeDuration = task.TimeDuration,

                        LessonPlanTaskAttachment = taskAttach,
                    });
                }
            }

            dto.LessonPlanLearningOutcomeMap = new List<LessonPlanOutcomeMapDTO>();
            foreach (var learningOutcome in this.LessonPlanLearningOutcomes)
            {
                if (learningOutcome !=  null && !string.IsNullOrEmpty(learningOutcome.Key))
                {
                    dto.LessonPlanLearningOutcomeMap.Add(new LessonPlanOutcomeMapDTO()
                    {
                        LessonPlanID = dto.LessonPlanID,
                        LessonLearningOutcomeID = byte.Parse(learningOutcome.Key),  
                    });
                }
            }

            dto.LessonPlanLearningObjectiveMap = new List<LessonPlanObjectiveMapDTO>();
            foreach (var learningObjective in this.LessonPlanLearningObjectives)
            {
                if (learningObjective !=  null && !string.IsNullOrEmpty(learningObjective.Key))
                {
                    dto.LessonPlanLearningObjectiveMap.Add(new LessonPlanObjectiveMapDTO()
                    {
                        LessonPlanID = dto.LessonPlanID,
                        LessonLearningObjectiveID = byte.Parse(learningObjective.Key),
                    });
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