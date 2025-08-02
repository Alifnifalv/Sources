using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Jobs;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.School.Fees;
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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "ApplicantInterviewEvaluation", "CRUDModel.ViewModel")]
    public class JobInterviewEvaluationViewModel : BaseMasterViewModel
    {
        public JobInterviewEvaluationViewModel()
        {
            ShortList = new List<JobInterviewEvaluationApplicantsViewModel>() { new JobInterviewEvaluationApplicantsViewModel() };
        }

        public long InterviewID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Job Title")]
        public string JobTitle { get; set; }
        public long? JobID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Job Description")]
        public string Interview { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [CustomDisplay("")]
        public string NewLine3 { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Start Date")]
        public string StartDateString { get; set; } 

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("End Date")]
        public string EndDateString { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("Remarks")]
        //public string Remarks { get; set; } 


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("")]
        public List<JobInterviewEvaluationApplicantsViewModel> ShortList { get; set; } 


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as JobInterviewDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<JobInterviewEvaluationViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<JobInterviewDTO, JobInterviewEvaluationViewModel>.CreateMap();
            var jobdto = dto as JobInterviewDTO;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var vm = Mapper<JobInterviewDTO, JobInterviewEvaluationViewModel>.Map(dto as JobInterviewDTO);

            vm.InterviewID = jobdto.InterviewID;
            vm.JobID = jobdto.JobID;
            vm.JobTitle = jobdto.JobTitle;
            vm.Interview = jobdto.Interview;
            //vm.Remarks = jobdto.Remarks;
            vm.StartDateString = jobdto.StartDate.HasValue ? jobdto.StartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.EndDateString = jobdto.EndDate.HasValue ? jobdto.EndDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;

            vm.ShortList = new List<JobInterviewEvaluationApplicantsViewModel>();

            foreach (var data in jobdto.ShortListDTO.Where(x => x.ApplicantID != 0))
            {
                vm.ShortList.Add(new JobInterviewEvaluationApplicantsViewModel()
                {
                    ApplicantID = data.ApplicantID,
                    ApplicantName = data.ApplicantName,
                    TotalRating = data.TotalRating,
                    TotalRatingGot = data.TotalRatingGot,
                    RoundMaps = data.RoundMapDTO.Select(a => new JobInterviewEvaluationApplicantRoundMapViewModel()
                    {
                        RoundID = a.RoundID,
                        Round = a.Round,
                        HeldOnDateString = a.HeldOnDate.HasValue ? a.HeldOnDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        MaximumRating = a.MaximumRating,
                        Remarks = a.Remarks,
                        Rating = a.Rating,
                    }).ToList(),
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<JobInterviewEvaluationViewModel, JobInterviewDTO>.CreateMap();
            var dto = Mapper<JobInterviewEvaluationViewModel, JobInterviewDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.InterviewID = this.InterviewID;
            dto.JobTitle = this.JobTitle;
            dto.JobID = this.JobID;
            dto.StartDate = string.IsNullOrEmpty(this.StartDateString) ? (DateTime?)null : DateTime.ParseExact(this.StartDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.EndDate = string.IsNullOrEmpty(this.EndDateString) ? (DateTime?)null : DateTime.ParseExact(this.EndDateString, dateFormat, CultureInfo.InvariantCulture);
            //dto.Remarks = this.Remarks;

            dto.ShortListDTO = new List<JobInterviewMapDTO>();
            dto.ShortListDTO = this.ShortList.Where(x => x.ApplicantID != null).Select(vmList => new JobInterviewMapDTO()
            {
                InterviewID = this.InterviewID,
                ApplicantID = vmList.ApplicantID,
                RoundMapDTO = vmList.RoundMaps.Where(ra => ra.RoundID != null).Select(r => new JobInterviewMapDTO.JobInterviewRoundMapDTO()
                {
                    RoundID = r.RoundID,
                    HeldOnDate = string.IsNullOrEmpty(r.HeldOnDateString) ? (DateTime?)null : DateTime.ParseExact(r.HeldOnDateString, dateFormat, CultureInfo.InvariantCulture),
                    Rating = r.Rating,
                    Remarks = r.Remarks,
                    MaximumRating = r.MaximumRating,
                }).ToList(),
            }).ToList();

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<JobInterviewDTO>(jsonString);
        }
    }
}
