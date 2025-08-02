using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Exams
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "ExamSchedule", "CRUDModel.ViewModel")]
    [DisplayName("Exam GeneralInfo")]
    public class ExamViewModel : BaseMasterViewModel
    {
        public ExamViewModel()
        {
            //ExamSchedule = new ExamScheduleViewModel();
            ExamSubject = new ExamSubjectViewModel();
            ExamClass = new ExamClassViewModel();
            IsActive = true;
            IncludeInFinalReport = true;
            SkillSets = new List<KeyValueViewModel>();
            //SkillGroups = new List<KeyValueViewModel>();
        }

        //[Required]
       // [ControlType(Framework.Enums.ControlTypes.Label)]
       // [DisplayName("Exam ID")]
        public long ExamIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ExamDescription")]
        public string ExamDescription { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.ExamType")]
        [CustomDisplay("ExamType")]
        public string ExamTypes { get; set; }
        public byte? ExamTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.ExamGroups")]
        [CustomDisplay("AssesmentTerm")]
        public string ExamGroupName { get; set; }
        public int? ExamGroupID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Skill", "Numeric", true, "")]
        //[LookUp("LookUps.Skills")]
        //[DisplayName("Skill Group")]
        //public List<KeyValueViewModel> SkillGroups { get; set; }
        //public int? SkillGroupMasterID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("SkillSets", "Numeric", true, "")]
        [LookUp("LookUps.SkillSets")]
        [CustomDisplay("SkillSets")]
        public List<KeyValueViewModel> SkillSets { get; set; }
        public int? ClassSubjectSkillGroupMapID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[LookUp("LookUps.MarkGrade")]
        //[DisplayName("Mark Grade")]
        //public string MarkGrade { get; set; }
        //public int? MarkGradeID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "ExamSchedule", "ExamSchedule")]
        //[DisplayName("Exam Schedule")]
        //public ExamScheduleViewModel ExamSchedule { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "ExamClass", "ExamClass")]
        [CustomDisplay("Class")]
        public ExamClassViewModel ExamClass { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "ExamSubject", "ExamSubject")]
        [CustomDisplay("Subject")]
        public ExamSubjectViewModel ExamSubject { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }


        //[Required]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ProgressCardHeader")]
        public string ProgressCardHeader { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("Include in Final report")]
        public bool? IncludeInFinalReport { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("Include in Interim report")]
        public bool? IncludeInInterimReport { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ExamDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ExamViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ExamDTO, ExamViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            //Mapper<ExamScheduleDTO, ExamScheduleViewModel>.CreateMap();
            var exmdto = dto as ExamDTO;
            var vm = Mapper<ExamDTO, ExamViewModel>.Map(exmdto);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            vm.ExamIID = exmdto.ExamIID;
            vm.ExamDescription = exmdto.ExamDescription;
            vm.IsActive = exmdto.IsActive;
            vm.ExamTypes = exmdto.ExamTypeID.ToString();
            vm.ExamGroupName = exmdto.ExamGroupID.ToString();
            vm.IncludeInFinalReport = exmdto.IncludeInFinalReport;
            vm.IncludeInInterimReport = exmdto.IncludeInInterimReport;
      

            //vm.MarkGrade = exmdto.MarkGradeID.ToString();
            //vm.ExamSchedule = new ExamScheduleViewModel()
            //{
            //    ExamScheduleIID = exmdto.ExamSchedules.ExamScheduleIID,
            //    //ScheduleDateString = Convert.ToDateTime(exmdto.ExamSchedules.Date).ToShortDateString(),
            //    ExamStartDateString = exmdto.ExamSchedules.ExamStartDate.HasValue ? Convert.ToDateTime(exmdto.ExamSchedules.ExamStartDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null,
            //    ExamEndDateString = exmdto.ExamSchedules.ExamEndDate.HasValue ? Convert.ToDateTime(exmdto.ExamSchedules.ExamEndDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null,
            //    ExamID = exmdto.ExamIID,
            //    Starttime= exmdto.ExamSchedules.StartTime.HasValue ? DateTime.Today.Add(exmdto.ExamSchedules.StartTime.Value).ToString("hh:mm tt") : null,
            //    Endtime = exmdto.ExamSchedules.EndTime.HasValue ? DateTime.Today.Add(exmdto.ExamSchedules.EndTime.Value).ToString("hh:mm tt") : null,
            //    Room = exmdto.ExamSchedules.Room,
            //    FullMarks = exmdto.ExamSchedules.FullMarks,
            //    PassingMarks = exmdto.ExamSchedules.PassingMarks
            //};

            foreach (var Map in exmdto.ExamSkillMaps)
            {
                if (Map.SkillSet != null )
                { 
                vm.SkillSets.Add(new KeyValueViewModel()
                {
                    Key = Map.SkillSet.Key,
                    Value = Map.SkillSet.Value
                });
                }

                //if (Map.SkillGroup != null)
                //{
                //vm.SkillGroups.Add(new KeyValueViewModel()
                //{
                //    Key = Map.SkillGroup.Key,
                //    Value = Map.SkillGroup.Value
                //});
                //}
            }


            vm.ExamClass.ExamClasses = new List<ExamClassMapsViewModel>();
            foreach (var exclass in exmdto.ExamClasses)
            {
                vm.ExamClass.ExamClasses.Add(new ExamClassMapsViewModel()
                {
                    ExamClassMapIID = exclass.ExamClassMapIID,
                    ExamScheduleID = exmdto.ExamSchedules.ExamScheduleIID,
                    ExamID = exmdto.ExamIID,
                    StudentClass = new KeyValueViewModel() { Key = exclass.ClassID.ToString(), Value = exclass.Class },
                    Section = new KeyValueViewModel() { Key = exclass.SectionID.ToString(), Value = exclass.Section }

                });
            }

            vm.ExamSubject = new ExamSubjectViewModel();
            vm.ExamSubject.ExamSubjects = new List<ExamSubjectMapsViewModel>();

            foreach (var exsub in exmdto.ExamSubjects)
            {
                vm.ExamSubject.ExamSubjects.Add(new ExamSubjectMapsViewModel()
                {
                    ExamSubjectMapIID = exsub.ExamSubjectMapIID,
                    ExamID = exmdto.ExamIID,
                    Subject = new KeyValueViewModel() { Key = exsub.SubjectID.ToString(), Value = exsub.Subject },
                    //ExamDateString = Convert.ToDateTime(exsub.ExamDate).ToString(dateFormat, CultureInfo.InvariantCulture),
                    MarkGrade = new KeyValueViewModel() { Key = exsub.MarkGradeID.ToString(), Value = exsub.MarkGrade },
                    SubjectType = exsub.SubjectType != null ? new KeyValueViewModel() { Key = exsub.SubjectType.Key, Value = exsub.SubjectType.Value} : null,
                    
                    MinimumMarks = exsub.MinimumMarks,
                    MaximumMarks = exsub.MaximumMarks,
                    //StartTimeString = exsub.StartTime.HasValue ? DateTime.Today.Add(exsub.StartTime.Value).ToString("hh:mm tt") : null,
                    //EndTimeString = exsub.EndTime.HasValue ? DateTime.Today.Add(exsub.EndTime.Value).ToString("hh:mm tt") : null,
                    ConversionFactor = exsub.ConversionFactor,
            });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ExamViewModel, ExamDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<ExamScheduleViewModel, ExamScheduleDTO>.CreateMap();
            var dto = Mapper<ExamViewModel, ExamDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            dto.ExamTypeID = string.IsNullOrEmpty(this.ExamTypes) ? (byte?)null : byte.Parse(this.ExamTypes);
            dto.ExamGroupID = string.IsNullOrEmpty(this.ExamGroupName) ? (int?)null : int.Parse(this.ExamGroupName);
            //dto.MarkGradeID = string.IsNullOrEmpty(this.MarkGrade) ? (int?)null : int.Parse(this.MarkGrade);
            dto.ExamDescription = this.ExamDescription;
            dto.ProgressCardHeader = this.ProgressCardHeader;
            dto.IsActive = this.IsActive;
            dto.IncludeInFinalReport = this.IncludeInFinalReport;
            dto.IncludeInInterimReport = this.IncludeInInterimReport;
           


            //foreach (var sklgrp in this.SkillGroups)
            //{
            //    if (!string.IsNullOrEmpty(sklgrp.Key))
            //    {
            //        dto.ExamSkillMaps.Add(new ExamSkillMapDTO()
            //        {
            //            SkillGroupMasterID = string.IsNullOrEmpty(sklgrp.Key) ? (int?)null : int.Parse(sklgrp.Key),
            //        });
            //    }
            //}

            foreach (var sklset in this.SkillSets)
            {
                if (!string.IsNullOrEmpty(sklset.Key))
                {
                    dto.ExamSkillMaps.Add(new ExamSkillMapDTO()
                    {
                        ClassSubjectSkillGroupMapID = string.IsNullOrEmpty(sklset.Key) ? (long?)null : long.Parse(sklset.Key),
                    });
                }
            }

            //dto.ExamSchedules = new ExamScheduleDTO()
            //{
            //    ExamScheduleIID = this.ExamSchedule.ExamScheduleIID,
            //    ExamID = this.ExamSchedule.ExamID,
            //    //Date = Convert.ToDateTime(this.ExamSchedule.ScheduleDateString),
            //    ExamStartDate = string.IsNullOrEmpty(this.ExamSchedule.ExamStartDateString) ? (DateTime?)null : DateTime.ParseExact(this.ExamSchedule.ExamStartDateString, dateFormat, CultureInfo.InvariantCulture),
            //    ExamEndDate = string.IsNullOrEmpty(this.ExamSchedule.ExamEndDateString) ? (DateTime?)null : DateTime.ParseExact(this.ExamSchedule.ExamEndDateString, dateFormat, CultureInfo.InvariantCulture),
            //    StartTime = this.ExamSchedule.StartTime != null ? this.ExamSchedule.Starttime == "" ? (TimeSpan?) null : DateTime.Parse(this.ExamSchedule.Starttime).TimeOfDay : null,
            //    EndTime = this.ExamSchedule.EndTime != null ? this.ExamSchedule.Endtime == "" ? (TimeSpan?) null : DateTime.Parse(this.ExamSchedule.Endtime).TimeOfDay : null,
            //    Room = this == null || this.ExamSchedule == null || this.ExamSchedule.Room == null ? null : this.ExamSchedule.Room,
            //    FullMarks = this.ExamSchedule.FullMarks,
            //    PassingMarks = this.ExamSchedule.PassingMarks
            //};

            dto.ExamClasses = new List<ExamClassDTO>();

            foreach (var exclassdto in this.ExamClass.ExamClasses)
            {
                if (exclassdto.StudentClass != null && !string.IsNullOrEmpty(exclassdto.StudentClass.Key))
                {
                    dto.ExamClasses.Add(new ExamClassDTO()
                    {
                        ExamClassMapIID = exclassdto.ExamClassMapIID,
                        ExamID = this.ExamIID,
                        ExamScheduleID = exclassdto.ExamScheduleID,
                        ClassID = exclassdto.StudentClass == null || string.IsNullOrEmpty(exclassdto.StudentClass.Key) ? (int?)null : int.Parse(exclassdto.StudentClass.Key),
                        SectionID = exclassdto.Section == null || string.IsNullOrEmpty(exclassdto.Section.Key) ? (int?)null : int.Parse(exclassdto.Section.Key),
                    });
                }
            }

            dto.ExamSubjects = new List<ExamSubjectDTO>();

            foreach (var subject in this.ExamSubject.ExamSubjects)
            {
                if (subject.Subject != null)
                {
                    dto.ExamSubjects.Add(new ExamSubjectDTO()
                    {
                        ExamSubjectMapIID = subject.ExamSubjectMapIID,
                        ExamID = this.ExamIID,
                        //ExamDate = string.IsNullOrEmpty(subject.ExamDateString) ? (DateTime?)null : DateTime.ParseExact(subject.ExamDateString, dateFormat, CultureInfo.InvariantCulture),
                        SubjectID = subject.Subject == null || string.IsNullOrEmpty(subject.Subject.Key) ? (int?)null : int.Parse(subject.Subject.Key),
                        MinimumMarks = subject.MinimumMarks.HasValue ? subject.MinimumMarks : (decimal?)null,
                        MarkGradeID = subject.MarkGrade == null? (int?)null : int.Parse(subject.MarkGrade.Key),
                        MaximumMarks = subject.MaximumMarks.HasValue ? subject.MaximumMarks : (decimal?)null,
                        //StartTime = subject.StartTime != null ? subject.StartTimeString == null || subject.StartTimeString == "" ? (TimeSpan?)null : Convert.ToDateTime(subject.StartTimeString).TimeOfDay : null,
                        //EndTime = subject.EndTime != null ? subject.EndTimeString == "" ? (TimeSpan?)null : Convert.ToDateTime(subject.EndTimeString).TimeOfDay : null,
                        ConversionFactor = subject.ConversionFactor,
                });

                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ExamDTO>(jsonString);
        }
    }
}

