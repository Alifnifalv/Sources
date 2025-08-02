using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Jobs;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.School.Transports;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace Eduegate.Web.Library.HR.Career
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "InterviewManagement", "CRUDModel.ViewModel")]
    public class JobInterviewManagementViewModel : BaseMasterViewModel
    {
        public JobInterviewManagementViewModel()
        {
            ShortList = new List<JobInterviewShortListViewModel>() { new JobInterviewShortListViewModel() };
            Interviewer = new KeyValueViewModel();
            InterviewRounds = new List<KeyValueViewModel>();
            IsInterviewManagement = true;
        }

        public bool IsInterviewManagement { get; set; }
        public long InterviewID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [CustomDisplay("Job applications for")]
        [DataPicker("CareerListing")]
        public string JobTitle { get; set; }
        public long? JobID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [CustomDisplay("")]
        public string NewLine3 { get; set; }
        
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Interviewer")]
        [Select2("Interviewer", "String", false, "")]
        [LookUp("LookUps.ActiveEmployees")]
        public KeyValueViewModel Interviewer { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Rounds", "Numeric", true, "")]
        [LookUp("LookUps.InterviewRounds")]
        [CustomDisplay("Rounds")]
        public List<KeyValueViewModel> InterviewRounds { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Duration (in minutes)")]
        public int? Duration { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [CustomDisplay("")]
        public string NewLine4 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Start Date")]
        public string StartDateString { get; set; } 

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("End Date")]
        public string EndDateString { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; } 
        
        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='CreateMeetingLink($event,CRUDModel.ViewModel)'")]
        [CustomDisplay("Generate meeting link")]
        public string GenerateLinkBtn { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [CustomDisplay("")]
        public string NewLine5 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.RichTextEditor, "w-100")]
        [CustomDisplay("Link")]
        public string MeetingLink { get; set; } 


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("")]
        public List<JobInterviewShortListViewModel> ShortList { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as JobInterviewDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<JobInterviewManagementViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<JobInterviewDTO, JobInterviewManagementViewModel>.CreateMap();
            var jobdto = dto as JobInterviewDTO;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var vm = Mapper<JobInterviewDTO, JobInterviewManagementViewModel>.Map(dto as JobInterviewDTO);

            vm.InterviewID = jobdto.InterviewID;
            vm.JobID = jobdto.JobID;
            vm.JobTitle = jobdto.JobTitle;
            vm.Duration = jobdto.Duration;
            vm.Remarks = jobdto.Remarks;
            vm.MeetingLink = jobdto.MeetingLink;
            vm.Interviewer = jobdto.InterviewerID.HasValue ? new KeyValueViewModel() { Key = jobdto.Interviewer.Key, Value = jobdto.Interviewer.Value } : new KeyValueViewModel();
            vm.StartDateString = jobdto.StartDate.HasValue ? jobdto.StartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.EndDateString = jobdto.EndDate.HasValue ? jobdto.EndDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;

            vm.ShortList = new List<JobInterviewShortListViewModel>();

            foreach (var data in jobdto.ShortListDTO.Where(x => x.ApplicantID != 0))
            {
                vm.ShortList.Add(new JobInterviewShortListViewModel()
                {
                    ApplicantID = data.ApplicantID,
                    ApplicantName = data.ApplicantName,
                    Education = data.Education,
                    StartTimeString = data.StartTime.HasValue ? DateTime.Today.Add(data.StartTime.Value).ToString("hh:mm tt") : null,
                    EndTimeString = data.EndTime.HasValue ? DateTime.Today.Add(data.EndTime.Value).ToString("hh:mm tt") : null,
                });
            }

            vm.InterviewRounds = jobdto.InterviewRounds.Select(x => new KeyValueViewModel()
            {
                Key = x.Key.ToString(),
                Value = x.Value.ToString(),

            }).ToList();

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<JobInterviewManagementViewModel, JobInterviewDTO>.CreateMap();
            var dto = Mapper<JobInterviewManagementViewModel, JobInterviewDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.InterviewID = this.InterviewID;
            dto.JobTitle = this.JobTitle;
            dto.JobID = this.JobID;
            dto.InterviewerID = string.IsNullOrEmpty(this.Interviewer.Key) ? (int?)null : int.Parse(this.Interviewer.Key);
            dto.StartDate = string.IsNullOrEmpty(this.StartDateString) ? (DateTime?)null : DateTime.ParseExact(this.StartDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.EndDate = string.IsNullOrEmpty(this.EndDateString) ? (DateTime?)null : DateTime.ParseExact(this.EndDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.MeetingLink = this.MeetingLink;
            dto.Duration = this.Duration;
            dto.Remarks = this.Remarks;
            dto.Interviewer = this.Interviewer != null ?  new KeyValueDTO(){ Key = this.Interviewer.Key.ToString(), Value = this.Interviewer.Value.ToString() } : new KeyValueDTO();

            dto.ShortListDTO = new List<JobInterviewMapDTO>();
            dto.ShortListDTO = this.ShortList.Select(vmList => new JobInterviewMapDTO()
            {
                InterviewID = this.InterviewID,
                StartTime = this == null || vmList.StartTimeString == null || vmList.StartTimeString == "" ? (TimeSpan?)null : DateTime.Parse(vmList.StartTimeString).TimeOfDay,
                EndTime = this == null || vmList.EndTimeString == null || vmList.EndTimeString == "" ? (TimeSpan?)null : DateTime.Parse(vmList.EndTimeString).TimeOfDay,
                ApplicantID = vmList.ApplicantID,
                Remarks = vmList.Remarks,
                ApplicantMailID = vmList.ApplicantMailID,
                ApplicantName = vmList.ApplicantName,

            }).Where(x => x.ApplicantID != null).ToList();

            dto.InterviewRounds = this.InterviewRounds.Select(y => new KeyValueDTO()
            {
                Key = y.Key.ToString(),
                Value = y.Value.ToString(),

            }).ToList();

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<JobInterviewDTO>(jsonString);
        }


        public static JobInterviewManagementViewModel FromDTOtoVM(List<JobApplicationDTO> dto)
        {
            var vm = new JobInterviewManagementViewModel();
            vm.ShortList = new List<JobInterviewShortListViewModel>();

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            if (dto.Count > 0)
            {
                vm.JobTitle = dto.FirstOrDefault().JobTitle;
                vm.JobID = (long)dto.FirstOrDefault().JobID;

                if (dto != null)
                {
                    foreach (var detailDat in dto)
                    {
                        vm.ShortList.Add(new JobInterviewShortListViewModel()
                        {
                            ApplicantID=detailDat.ApplicantID,
                            JobID = detailDat.JobID,
                            ApplicantName = detailDat.ApplicantName,
                            Education = detailDat.Education,
                            TotalYearOfExperience = detailDat.TotalYearOfExperience,
                            ApplicantMailID = detailDat.ApplicantMailID,
                        });
                    }
                }
            }
            return vm;
        }
    }
}
