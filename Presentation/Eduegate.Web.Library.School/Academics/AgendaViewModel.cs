using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.School.Academics;
using System;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Agenda", "CRUDModel.ViewModel")]
    [DisplayName("Notes")]
    public class AgendaViewModel : BaseMasterViewModel
    {
        public AgendaViewModel()
        {
            IsSendPushNotification = true;
            AgendaTopics = new List<AgendaTopicViewModel>() { new AgendaTopicViewModel() }; ;
            AgendaTaskAttachMap = new List<AgendaTastAttachmentViewModel>() { new AgendaTastAttachmentViewModel() };
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            Date1String = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture);
        }

        public long AgendaIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Date")]
        public string Date1String { get; set; }
        public DateTime? Date1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AcademicYear", "Numeric", false, "")]
        [CustomDisplay("Academic Year")]
        [LookUp("LookUps.AcademicYear")]
        public KeyValueViewModel AcademicYear { get; set; }
        public int? AcademicYearID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false, "ClassChanges($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public KeyValueViewModel StudentClass { get; set; }
        public int? ClassID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Section", "Numeric", true, "")]
        [CustomDisplay("Section")]
        [LookUp("LookUps.Section")]
        public List<KeyValueViewModel> Sections { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }


        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Title")]
        public string Title { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Subject", "Numeric", false, "")]
        [CustomDisplay("Subject")]
        [LookUp("LookUps.Subject")]
        public KeyValueViewModel Subject { get; set; }
        public int? SubjectID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }



        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Status", "Numeric", false, "")]
        [CustomDisplay("Status")]
        [LookUp("LookUps.AgendaStatus")]
        public KeyValueViewModel AgendaStatus { get; set; }
        public byte? AgendaStatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsSendPushNotification")]
        public bool? IsSendPushNotification { get; set; }


        

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Notes")]
        public List<AgendaTopicViewModel> AgendaTopics { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Types")]
        public List<AgendaTastAttachmentViewModel> AgendaTaskAttachMap { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as AgendaDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<AgendaViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<AgendaDTO, AgendaViewModel>.CreateMap();
            Mapper<AgendaTopicMapDTO, AgendaTopicViewModel>.CreateMap();
            Mapper<AgendaTaskMapDTO, AgendaTopicViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var mpdto = dto as AgendaDTO;
            var vm = Mapper<AgendaDTO, AgendaViewModel>.Map(dto as AgendaDTO);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.AgendaIID = mpdto.AgendaIID;
            vm.ClassID = string.IsNullOrEmpty(mpdto.Class.Key) ? (int?)null : int.Parse(mpdto.Class.Key);
            vm.SubjectID = string.IsNullOrEmpty(mpdto.Subject.Key) ? (int?)null : int.Parse(mpdto.Subject.Key);
            vm.AcademicYearID = string.IsNullOrEmpty(mpdto.AcademicYear.Key) ? (int?)null : int.Parse(mpdto.AcademicYear.Key);
            vm.Subject = new KeyValueViewModel() { Key = mpdto.Subject.Key.ToString(), Value = mpdto.Subject.Value };
            vm.AgendaStatus = new KeyValueViewModel() { Key = mpdto.AgendaStatus.Key.ToString(), Value = mpdto.AgendaStatus.Value };
            vm.StudentClass = new KeyValueViewModel() { Key = vm.ClassID.ToString(), Value = mpdto.Class.Value };
            vm.Title = mpdto.Title;
            vm.IsSendPushNotification = mpdto.IsSendPushNotification;
            vm.AcademicYear = mpdto.AcademicYearID.HasValue ? new KeyValueViewModel()
            {
                Key = mpdto.AcademicYearID.ToString(),
                Value = mpdto.AcademicYear.Value
            } : null;
            vm.Date1String = mpdto.Date1.HasValue ? mpdto.Date1.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;

            vm.Sections = new List<KeyValueViewModel>();
            foreach (var sec in mpdto.SectionList)
            {
                vm.Sections.Add(new KeyValueViewModel()
                {
                    Key = sec.Key,
                    Value = sec.Value
                });
            }

            vm.AgendaTopics = new List<AgendaTopicViewModel>();
            foreach (var topic in mpdto.AgendaTopicMap)
            {
                if (topic.LectureCode != null)
                {
                    vm.AgendaTopics.Add(new AgendaTopicViewModel()
                    {
                        Topic = topic.Topic,
                        LectureCode = topic.LectureCode,
                        AgendaID = topic.AgendaID,
                        AgendaTopicMapIID = topic.AgendaTopicMapIID
                    });
                }
            }

            vm.AgendaTaskAttachMap = new List<AgendaTastAttachmentViewModel>();
            foreach (var task in mpdto.AgendaTaskMap)
            {
                if (task.TaskTypeID != null)
                {
                    vm.AgendaTaskAttachMap.Add(new AgendaTastAttachmentViewModel
                    {
                        AgendacTaskAttachmentMapIID = task.AgendacTaskAttachmentMapIID,
                        ContentFileIID = task.AttachmentReferenceID,
                        ContentFileName = task.AttachmentName,
                        //Task = task.Task,
                        AgendaTaskMapIID = task.AgendaTaskMapIID,
                        AgendaTaskMapID = task.AgendaTaskMapIID,
                        AgendaTopicMapID = task.AgendaTopicMapID,
                        Date1String = task.StartDate.HasValue ? task.StartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        Date2String = task.EndDate.HasValue ? task.EndDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        TaskType = task.TaskTypeID.HasValue ? new KeyValueViewModel() { Key = task.TaskTypeID.ToString(), Value = task.TaskType.Value } : new KeyValueViewModel(),
                    });
                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<AgendaViewModel, AgendaDTO>.CreateMap();
            Mapper<AgendaTopicViewModel, AgendaTopicMapDTO>.CreateMap();
            Mapper<AgendaTastAttachmentViewModel, AgendaTaskMapDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<AgendaViewModel, AgendaDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.AgendaIID = this.AgendaIID;
            dto.Title = this.Title;
            dto.ClassID = this.StudentClass != null && string.IsNullOrEmpty(this.StudentClass.Key) ? (int?)null : int.Parse(this.StudentClass.Key);
            dto.SubjectID = this.Subject != null && string.IsNullOrEmpty(this.Subject.Key) ? (int?)null : int.Parse(this.Subject.Key);
            dto.AcademicYearID = this.AcademicYear != null && string.IsNullOrEmpty(this.AcademicYear.Key) ? (int?)null : int.Parse(this.AcademicYear.Key);
            dto.AgendaStatusID = this.AgendaStatus != null && string.IsNullOrEmpty(this.AgendaStatus.Key) ? (byte?)null : byte.Parse(this.AgendaStatus.Key);
            dto.Date1 = string.IsNullOrEmpty(this.Date1String) ? (DateTime?)null : DateTime.ParseExact(this.Date1String, dateFormat, CultureInfo.InvariantCulture);

            dto.SectionList = new List<KeyValueDTO>();
            foreach (var sec in Sections)
            {
                dto.SectionList.Add(new KeyValueDTO()
                {
                    Key = sec.Key,
                    Value = sec.Value
                });
            }

            dto.AgendaTopicMap = new List<AgendaTopicMapDTO>();
            foreach (var topic in this.AgendaTopics)
            {
                if (topic.Topic != null)
                {
                    dto.AgendaTopicMap.Add(new AgendaTopicMapDTO()
                    {
                        Topic = topic.Topic,
                        LectureCode = topic.LectureCode,
                        AgendaID = topic.AgendaID.HasValue ? topic.AgendaID : this.AgendaIID,
                        AgendaTopicMapIID = topic.AgendaTopicMapIID,
                    });
                }
            }

            dto.AgendaTaskMap = new List<AgendaTaskMapDTO>();
            foreach (var task in this.AgendaTaskAttachMap)
            {
                if (!string.IsNullOrEmpty(task.TaskType.Value) || task.ContentFileIID != null)
                {
                    dto.AgendaTaskMap.Add(new AgendaTaskMapDTO()
                    {
                        //Task = task.Task,
                        TaskTypeID = task.TaskType == null || string.IsNullOrEmpty(task.TaskType.Key) ? (byte?)null : byte.Parse(task.TaskType.Key),
                        StartDate = string.IsNullOrEmpty(task.Date1String) ? (DateTime?)null : DateTime.ParseExact(task.Date1String, dateFormat, CultureInfo.InvariantCulture),
                        EndDate = string.IsNullOrEmpty(task.Date2String) ? (DateTime?)null : DateTime.ParseExact(task.Date2String, dateFormat, CultureInfo.InvariantCulture),
                        AgendaTaskMapIID = task.AgendaTaskMapIID,
                        AgendaID = task.AgendaID,
                        AgendacTaskAttachmentMapIID = task.AgendacTaskAttachmentMapIID,
                        AgendaTaskMapID = task.AgendaTaskMapID,
                        AttachmentReferenceID = task.ContentFileIID,
                        AttachmentName = task.ContentFileName,
                        CreatedBy = task.CreatedBy,
                        UpdatedBy = task.UpdatedBy,
                        CreatedDate = task.CreatedDate,
                        UpdatedDate = task.UpdatedDate,
                    });
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AgendaDTO>(jsonString);
        }
    }
}