using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.School.Academics;
using System;
using System.Globalization;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Enums;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "LessonPlan", "CRUDModel.ViewModel")]
    [DisplayName("LessonPlan")]
    public class LessonPlanViewModel_OLD : BaseMasterViewModel
    {
        public LessonPlanViewModel_OLD()
        {
            StudentClass = new KeyValueViewModel();
            Section = new KeyValueViewModel();
            Subject = new KeyValueViewModel();
            LessonPlanStatus = new KeyValueViewModel();
            TeachingAid = new List<KeyValueViewModel>();
            LessonPlanTopics = new List<LessonPlanTopicMapsViewModel>() { new LessonPlanTopicMapsViewModel() };
            LessonPlanTasks = new List<LessonPlanTaskViewModel>() { new LessonPlanTaskViewModel() };
        }

        public long LessonPlanIID { get; set; }

        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Lesson Plan Title")]
        public string Title { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.MonthNames")]
        [DisplayName("Month")]
        public string MonthName { get; set; }
        public int? MonthID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Start Date")]
        public string Date1String { get; set; }
        public DateTime? Date1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("End Date")]
        public string Date2String { get; set; }
        public DateTime? Date2 { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Maximum Extended Date")]
        public string Date3String { get; set; }
        public DateTime? Date3 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false, "ClassChanges($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.Classes")]
        [DisplayName("Class")]
        public KeyValueViewModel StudentClass { get; set; }
        public int? ClassID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Section", "Numeric", false, "")]
        [DisplayName("Section")]
        [LookUp("LookUps.Section")]
        public KeyValueViewModel Section { get; set; }
        public int? SectionID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Subject", "Numeric", false, "")]
        [DisplayName("Subject")]
        [LookUp("LookUps.Subject")]
        public KeyValueViewModel Subject { get; set; }
        public int? SubjectID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
        [DisplayName("Total Hours")]
        public decimal? TotalHours { get; set; }

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

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Activities (Specify The Activity (s) To Be Completed)")]
        public int? NumberOfActivityCompleted { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("TeachingAid", "String", true, "")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=TeachingAids", "LookUps.TeachingAids")]
        [DisplayName("Teaching Aids To Be Used")]
        public int? TeachingAidID { get; set; }
        public List<KeyValueViewModel> TeachingAid { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("LessonPlanStatus", "Numeric", false, "")]
        [DisplayName("Lesson Plan Status")]
        [LookUp("LookUps.LessonPlanStatus")]
        public KeyValueViewModel LessonPlanStatus { get; set; }
        public int? LessonPlanStatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        [MaxLength(1000, ErrorMessage = "Maximum Length should be within 1000!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Learning Experiences")]
        public string LearningExperiences { get; set; }

        [MaxLength(1000, ErrorMessage = "Maximum Length should be within 1000!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Home Work")]
        public string HomeWorks { get; set; }


        [MaxLength(1000, ErrorMessage = "Maximum Length should be within 1000!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Activity")]
        public string Activity { get; set; }

        //[Required]
        public string ProfileFile { get; set; }

        public string ProfileUrl { get; set; }

        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        [DisplayName("Attachment")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "ProfileUrl", "")]
        public string ProfileUploadFile { get; set; }
        public long? AttachmentReferenceID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine6 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Lesson Plan Topics")]
        public List<LessonPlanTopicMapsViewModel> LessonPlanTopics { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine7 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Lesson Plan Task")]
        public List<LessonPlanTaskViewModel> LessonPlanTasks { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LessonPlanDTO_OLD);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LessonPlanViewModel_OLD>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LessonPlanDTO_OLD, LessonPlanViewModel_OLD>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<LessonPlanTopicMapDTO, LessonPlanTopicMapsViewModel>.CreateMap();
            Mapper<LessonPlanTaskMapDTO, LessonPlanTopicTaskViewModel>.CreateMap();
            Mapper<LessonPlanTaskMapDTO, LessonPlanTaskViewModel>.CreateMap();
            var mpdto = dto as LessonPlanDTO_OLD;
            var vm = Mapper<LessonPlanDTO_OLD, LessonPlanViewModel_OLD>.Map(dto as LessonPlanDTO_OLD);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.LessonPlanIID = mpdto.LessonPlanIID;
            vm.ClassID = string.IsNullOrEmpty(mpdto.Class.Key) ? (int?)null : int.Parse(mpdto.Class.Key);
            vm.SubjectID = string.IsNullOrEmpty(mpdto.Subject.Key) ? (int?)null : int.Parse(mpdto.Subject.Key);
            vm.SectionID = string.IsNullOrEmpty(mpdto.Section.Key) ? (int?)null : int.Parse(mpdto.Section.Key);
            vm.Subject = new KeyValueViewModel() { Key = mpdto.Subject.Key.ToString(), Value = mpdto.Subject.Value };
            vm.Section = new KeyValueViewModel() { Key = mpdto.Section.Key.ToString(), Value = mpdto.Section.Value };
            vm.StudentClass = new KeyValueViewModel() { Key = vm.ClassID.ToString(), Value = mpdto.Class.Value  };
            vm.LessonPlanStatus = new KeyValueViewModel() { Key = mpdto.LessonPlanStatus.Key.ToString(), Value = mpdto.LessonPlanStatus.Value };
            vm.LessonPlanStatusID = string.IsNullOrEmpty(mpdto.LessonPlanStatus.Key) ? (byte?)null : byte.Parse(mpdto.LessonPlanStatus.Key);
            vm.Date1String = mpdto.Date1.HasValue ? mpdto.Date1.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.Date2String = mpdto.Date2.HasValue ? mpdto.Date2.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.Date3String = mpdto.Date3.HasValue ? mpdto.Date3.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.TotalHours = mpdto.TotalHours;
            vm.MonthID = mpdto.MonthID;
            vm.MonthName = mpdto.MonthID.ToString();
            vm.NumberOfPeriods = mpdto.NumberOfPeriods;
            vm.NumberOfClassTests = mpdto.NumberOfClassTests;
            vm.NumberOfActivityCompleted = mpdto.NumberOfActivityCompleted;
            vm.Activity = mpdto.Activity;
            vm.HomeWorks = mpdto.HomeWorks;
            vm.Title = mpdto.Title;

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
                                                LessonPlanTopicMapID = tpTaskAttach.LessonPlancTaskAttachmentMapIID,
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
                                        //TaskTypeID = string.IsNullOrEmpty(taskData.TaskType.Key) ? (int?)null : int.Parse(taskData.TaskType.Key),
                                        LessonPlanTaskMapIID = taskData.LessonPlanTaskMapIID,
                                        LessonPlanID = taskData.LessonPlanID,
                                        StartDate = taskData.StartDate,
                                        EndDate = taskData.EndDate,
                                        TaskType = new KeyValueViewModel() { Key = taskData.TaskType.ToString(), Value = taskData.TaskType.Value },
                                });
                                }
                            }
                        }
                        vm.LessonPlanTopics.Add(
                        new LessonPlanTopicMapsViewModel()
                        {
                            Topic = topic.Topic,
                            LectureCode = topic.LectureCode,
                            LessonPlanID = topic.LessonPlanID,
                            LessonPlanTopicMapIID = topic.LessonPlanTopicMapsIID
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
                        vm.LessonPlanTasks.Add(
                        new LessonPlanTaskViewModel()
                        {
                            Task = task.Task,
                            //TaskTypeID = string.IsNullOrEmpty(task.TaskType.Key) ? (int?)null : int.Parse(task.TaskType.Key),
                            LessonPlanTaskMapIID = task.LessonPlanTaskMapIID,
                            LessonPlanID = task.LessonPlanID,
                            StartDate = task.StartDate,
                            EndDate = task.EndDate,
                            TaskType = new KeyValueViewModel() { Key = task.TaskType.ToString(), Value = task.TaskType.Value },
                        }
                        );
                    }
                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LessonPlanViewModel_OLD, LessonPlanDTO_OLD>.CreateMap();
            Mapper<LessonPlanTopicMapsViewModel, LessonPlanTopicMapDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<LessonPlanTopicTaskViewModel, LessonPlanTaskMapDTO>.CreateMap();
            Mapper<LessonPlanTaskViewModel, LessonPlanTaskMapDTO>.CreateMap();
            var dto = Mapper<LessonPlanViewModel_OLD, LessonPlanDTO_OLD>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.LessonPlanIID = this.LessonPlanIID;
            dto.ClassID = string.IsNullOrEmpty(this.StudentClass.Key) ? (byte?)null : byte.Parse(this.StudentClass.Key);
            dto.SubjectID = string.IsNullOrEmpty(this.Subject.Key) ? (byte?)null : byte.Parse(this.Subject.Key);
            dto.SectionID = string.IsNullOrEmpty(this.Section.Key) ? (byte?)null : byte.Parse(this.Section.Key);
            dto.LessonPlanStatusID = string.IsNullOrEmpty(this.LessonPlanStatus.Key) ? (byte?)null : byte.Parse(this.LessonPlanStatus.Key);
            dto.Date1 = string.IsNullOrEmpty(this.Date1String) ? (DateTime?)null : DateTime.ParseExact(this.Date1String, dateFormat, CultureInfo.InvariantCulture);
            dto.Date2 = string.IsNullOrEmpty(this.Date2String) ? (DateTime?)null : DateTime.ParseExact(this.Date2String, dateFormat, CultureInfo.InvariantCulture);
            dto.Date3 = string.IsNullOrEmpty(this.Date3String) ? (DateTime?)null : DateTime.ParseExact(this.Date3String, dateFormat, CultureInfo.InvariantCulture);
            dto.TotalHours = this.TotalHours;
            dto.Title = this.Title;
            dto.NumberOfActivityCompleted = (byte?)this.NumberOfActivityCompleted;
            dto.NumberOfClassTests = (byte?)this.NumberOfClassTests;
            dto.NumberOfPeriods = (byte?)this.NumberOfPeriods;
            dto.MonthID = string.IsNullOrEmpty(this.MonthName) ? (byte?)null : byte.Parse(this.MonthName);
            dto.Activity = this.Activity;
            dto.HomeWorks = this.HomeWorks;
            dto.LearningExperiences = this.LearningExperiences;

            List<KeyValueDTO> TeachingAidList = new List<KeyValueDTO>();

            foreach (KeyValueViewModel vm in this.TeachingAid)
            {
                TeachingAidList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                    );
            }
            dto.TeachingAid = TeachingAidList;
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
                                TaskTypeID = string.IsNullOrEmpty(taskData.TaskType.Key) ? (byte?)null : byte.Parse(taskData.TaskType.Key),
                                LessonPlanTaskMapIID = taskData.LessonPlanTaskMapIID,
                                LessonPlanID = taskData.LessonPlanID,
                                StartDate = taskData.StartDate,
                                EndDate = taskData.EndDate,
                                LessonPlanTaskAttachment = topicTaskAttach,
                            });
                        }
                    }
                    dto.LessonPlanTopicMap.Add(
                    new LessonPlanTopicMapDTO()
                    {
                        Topic = topic.Topic,
                        LectureCode = topic.LectureCode,
                        LessonPlanID = topic.LessonPlanID,
                        LessonPlanTopicMapsIID = topic.LessonPlanTopicMapIID,
                        LessonPlanTopicAttachments = topicAttach,
                        LessonPlanTopicTask = topicTask,
                    }
                    );
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
                    dto.LessonPlanTask.Add(
                    new LessonPlanTaskMapDTO()
                    {
                        Task = task.Task,
                        TaskTypeID = string.IsNullOrEmpty(task.TaskType.Key) ? (byte?)null : byte.Parse(task.TaskType.Key),
                        LessonPlanTaskMapIID = task.LessonPlanTaskMapIID,
                        LessonPlanID = task.LessonPlanID,
                        StartDate = task.StartDate,
                        EndDate = task.EndDate,
                        LessonPlanTaskAttachment = taskAttach,
                    }
                    );
                }
            }
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LessonPlanDTO_OLD>(jsonString);
        }
    }
}