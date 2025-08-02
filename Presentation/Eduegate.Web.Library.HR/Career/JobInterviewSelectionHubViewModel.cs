using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Jobs;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.School.Fees;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace Eduegate.Web.Library.HR.Career
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "ApplicantInterviewSelectionHub", "CRUDModel.ViewModel")]
    public class JobInterviewSelectionHubViewModel : BaseMasterViewModel
    {
        public JobInterviewSelectionHubViewModel()
        {
            FilterBy = new FilterListByViewModel();
            SelectionList = new List<JobInterviewSelectionListViewModel>() { new JobInterviewSelectionListViewModel() };
        }

        public long InterviewID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Job Title")]
        public string JobTitle { get; set; }
        public long? JobID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Job Description")]
        public string Interview { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Interviewer")]
        public string InterviewerName { get; set; } 

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [CustomDisplay("")]
        public string NewLine3 { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Start Date")]
        public string StartDateString { get; set; } 

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("End Date")]
        public string EndDateString { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "htmllabelinfo")]
        [CustomDisplay("Rounds")]
        public string Rounds { get; set; }

        [ControlType(Framework.Enums.ControlTypes.FieldSet, "fullwidth", isFullColumn: false)]
        [CustomDisplay("Filter by")]
        public FilterListByViewModel FilterBy { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.FieldSet, "FilterBy", "CRUDModel.ViewModel.FilterBy")]
        public class FilterListByViewModel : BaseMasterViewModel
        {
            [ControlType(Framework.Enums.ControlTypes.TextBox)]
            [RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
            [MaxLength(3, ErrorMessage = "Maximum Length should be within 3!")]
            [CustomDisplay("View Toppers by Count")]
            public int? FilterByCount { get; set; }      
            
            [ControlType(Framework.Enums.ControlTypes.TextBox)]
            [RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
            [MaxLength(3, ErrorMessage = "Maximum Length should be within 3!")]
            [CustomDisplay("Rounds Completed")]
            public int? FilterByRounds { get; set; } 
            
            [ControlType(Framework.Enums.ControlTypes.TextBox)]
            [RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
            [MaxLength(3, ErrorMessage = "Maximum Length should be within 3!")]
            [CustomDisplay("Show Ratings From")]
            public int? FilterByRatings { get; set; }

            [ControlType(Framework.Enums.ControlTypes.Button, "medium-col-width", "ng-click='FilterSelectionList($event,CRUDModel.ViewModel)'")]
            [CustomDisplay("Filter")]
            public string FilterList { get; set; }
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("")]
        public List<JobInterviewSelectionListViewModel> SelectionList { get; set; } 


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as JobInterviewDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<JobInterviewSelectionHubViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<JobInterviewDTO, JobInterviewSelectionHubViewModel>.CreateMap();
            var jobdto = dto as JobInterviewDTO;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var vm = Mapper<JobInterviewDTO, JobInterviewSelectionHubViewModel>.Map(dto as JobInterviewDTO);

            vm.InterviewID = jobdto.InterviewID;
            vm.JobID = jobdto.JobID;
            vm.JobTitle = jobdto.JobTitle;
            vm.Interview = jobdto.Interview;
            vm.Rounds = jobdto.Rounds;
            vm.StartDateString = jobdto.StartDate.HasValue ? jobdto.StartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.EndDateString = jobdto.EndDate.HasValue ? jobdto.EndDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.InterviewerName = jobdto.Interviewer != null ? jobdto.Interviewer.Value.ToString() : null;

            vm.SelectionList = new List<JobInterviewSelectionListViewModel>();

            foreach (var data in jobdto.ShortListDTO.Where(x => x.ApplicantID != 0))
            {
                vm.SelectionList.Add(new JobInterviewSelectionListViewModel()
                {
                    ApplicantID = data.ApplicantID,
                    ApplicantName = data.ApplicantName,
                    TotalRating = data.TotalRating,
                    TotalRatingGot = data.TotalRatingGot,
                    TotalRounds = data.TotalRounds,
                    RoundsCompleted = data.RoundsCompleted,
                    TotalRatingEarned = data.TotalRatingEarned,
                    IsSelected = data.IsSelected,
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<JobInterviewSelectionHubViewModel, JobInterviewDTO>.CreateMap();
            var dto = Mapper<JobInterviewSelectionHubViewModel, JobInterviewDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.InterviewID = this.InterviewID;
            dto.JobTitle = this.JobTitle;
            dto.JobID = this.JobID;
            dto.StartDate = string.IsNullOrEmpty(this.StartDateString) ? (DateTime?)null : DateTime.ParseExact(this.StartDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.EndDate = string.IsNullOrEmpty(this.EndDateString) ? (DateTime?)null : DateTime.ParseExact(this.EndDateString, dateFormat, CultureInfo.InvariantCulture);
            //dto.Remarks = this.Remarks;

            dto.ShortListDTO = new List<JobInterviewMapDTO>();
            dto.ShortListDTO = this.SelectionList.Where(x => x.ApplicantID != null).Select(vmList => new JobInterviewMapDTO()
            {
                InterviewID = this.InterviewID,
                ApplicantID = vmList.ApplicantID,
                IsSelected = vmList.IsSelected,
            }).ToList();

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<JobInterviewDTO>(jsonString);
        }
    }
}
