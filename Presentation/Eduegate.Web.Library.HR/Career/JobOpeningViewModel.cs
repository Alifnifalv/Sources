using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Jobs;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.School.Transports;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace Eduegate.Web.Library.HR.Career
{
    //Career Listing screen
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "JobManagement", "CRUDModel.ViewModel")]
    public class JobOpeningViewModel : BaseMasterViewModel
    {
        public JobOpeningViewModel()
        {
            Skills = new List<KeyValueViewModel>();
            JobCriteria = new List<JobOpeningCriteriaListViewModel>() { new JobOpeningCriteriaListViewModel() };
            Country = new KeyValueViewModel();
            JDReference = new KeyValueViewModel();
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.TypeOfJob")]
        [DisplayName("Type Of Job")]
        public string TypeOfJob { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [Required]
        [DisplayName("Job Title")]
        public string JobTitle { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("JobDescription", "Numeric", false, "JDTitleChanges($event, $element, CRUDModel.ViewModel)")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=JobDescription", "LookUps.JobDescription")]
        [CustomDisplay("Fill Job Description by JD title")]
        public KeyValueViewModel JDReference { get; set; }
        public long? JDReferenceID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Designation")]
        [CustomDisplay("Designation")]
        public string Designation { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Department")]
        [DisplayName("Department")]
        public string Department { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine8 { get; set; } 


        [ControlType(Framework.Enums.ControlTypes.RichTextEditor, "w-100")]
        [Required]
        [DisplayName("Job Description")]
        public string JobDescription { get; set; }

        [ControlType(Framework.Enums.ControlTypes.RichTextEditor, "w-100")]
        [Required]
        [DisplayName("Job Detail")]
        public string JobDetail { get; set; }

        private DateTime? createdDate;
        public long JobIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Monthly Salary (from - to)")]
        public string MonthlySalary { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(2, ErrorMessage = "Maximum Length should be within 2!")]
        [CustomDisplay("Total year of experience")]
        public int? TotalYearOfExperience { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Country", "Numeric", false, "")]
        [LookUp("LookUps.Country")]
        [CustomDisplay("Preferred Country of Applicant")]
        public KeyValueViewModel Country { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Skills", "Numeric", true, isFreeText: true)]
        [LookUp("LookUps.JobSkills")]
        [CustomDisplay("Skills")]
        public List<KeyValueViewModel> Skills { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Publish Date")]
        public string PublishDateString { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Closing Date")]
        public string ClosingDateString { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(2, ErrorMessage = "Maximum Length should be within 2!")]
        [CustomDisplay("No of Vacancies")]
        public int? NoOfVacancies { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Location")]
        public string Location { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.JobStatus")]
        [DisplayName("Status")]
        public string JobStatus { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("")]
        public List<JobOpeningCriteriaListViewModel> JobCriteria { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as JobsDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<JobOpeningViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<JobsDTO, JobOpeningViewModel>.CreateMap();
            var jobdto = dto as JobsDTO;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var vm = Mapper<JobsDTO, JobOpeningViewModel>.Map(dto as JobsDTO);
            vm.JobIID = jobdto.JobIID;
            vm.JobTitle = jobdto.JobTitle;
            vm.JobDescription = jobdto.JobDescription;
            vm.JobDetail = jobdto.JobDetails;
            vm.Country = jobdto.CountryID.HasValue ? new KeyValueViewModel() { Key = jobdto.Country.Key, Value = jobdto.Country.Value } : new KeyValueViewModel();
            vm.JDReference = jobdto.JDReferenceID.HasValue ? new KeyValueViewModel() { Key = jobdto.JDReference.Key.ToString(), Value = jobdto.JDReference.Value.ToString() } : new KeyValueViewModel();
            vm.TotalYearOfExperience = jobdto.TotalYearOfExperience;
            vm.NoOfVacancies = jobdto.NoOfVacancies;
            vm.MonthlySalary = jobdto.MonthlySalary;
            vm.PublishDateString = jobdto.PublishDate.HasValue ? jobdto.PublishDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.ClosingDateString = jobdto.ClosingDate.HasValue ? jobdto.ClosingDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.JobStatus = jobdto.JobStatusID.ToString();
            vm.Department = jobdto.DepartmentID.ToString();
            vm.Designation = jobdto.DesignationID.ToString();
            vm.TypeOfJob = jobdto.JobTypeID.ToString();
            vm.Location = jobdto.Location;
            vm.CreatedDate = jobdto.CreatedDate;
            vm.CreatedBy = jobdto.CreatedBy;
            vm.UpdatedBy = jobdto.UpdatedBy;
            vm.UpdatedDate = jobdto.UpdatedDate;

            vm.Skills = new List<KeyValueViewModel>();

            if(jobdto.SkillList.Count > 0)
            {
                vm.Skills = jobdto.SkillList.Select(x => new KeyValueViewModel()
                {
                    Key = x.Key.ToString(),
                    Value = x.Value.ToString(),
                }).ToList();
            }

            vm.JobCriteria = new List<JobOpeningCriteriaListViewModel>();

            if(jobdto.AvailableJobCriteriaMapDTO.Count > 0)
            {
                vm.JobCriteria = jobdto.AvailableJobCriteriaMapDTO.Select(jc => new JobOpeningCriteriaListViewModel()
                {
                    CriteriaID = jc.CriteriaID,
                    JobID = jc.JobID,
                    JobCriteria = jc.TypeID.ToString(),
                    EducationQualification = jc.QualificationID.ToString(),
                    FieldOfStudy = jc.FieldOfStudy,
                }).ToList();
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<JobOpeningViewModel, JobsDTO>.CreateMap();
            var dto = Mapper<JobOpeningViewModel, JobsDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            dto.JobIID = this.JobIID;
            dto.JobStatusID = string.IsNullOrEmpty(this.JobStatus) ? (byte?)null : byte.Parse(this.JobStatus);
            dto.DepartmentID = string.IsNullOrEmpty(this.Department) ? (long?)null : long.Parse(this.Department);
            dto.DesignationID = string.IsNullOrEmpty(this.Designation) ? (int?)null : int.Parse(this.Designation);
            dto.JobTypeID = string.IsNullOrEmpty(this.TypeOfJob) ? (int?)null : int.Parse(this.TypeOfJob);
            dto.PublishDate = string.IsNullOrEmpty(this.PublishDateString) ? (DateTime?)null : DateTime.ParseExact(this.PublishDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.ClosingDate = string.IsNullOrEmpty(this.ClosingDateString) ? (DateTime?)null : DateTime.ParseExact(this.ClosingDateString, dateFormat, CultureInfo.InvariantCulture);

            dto.JobTitle = this.JobTitle;
            dto.JobDescription = this.JobDescription;
            dto.JobDetails = this.JobDetail;
            dto.MonthlySalary = this.MonthlySalary;
            dto.TotalYearOfExperience = this.TotalYearOfExperience;
            dto.NoOfVacancies = this.NoOfVacancies;
            dto.CountryID = string.IsNullOrEmpty(this.Country.Key) ? (int?)null : int.Parse(this.Country.Key);
            dto.JDReferenceID = string.IsNullOrEmpty(this.JDReference.Key) ? (long?)null : long.Parse(this.JDReference.Key);
            dto.Location = this.Location;

            dto.CreatedDate = this.CreatedDate;
            dto.CreatedBy = this.CreatedBy;
            dto.UpdatedBy = this.UpdatedBy;
            dto.UpdatedDate = this.UpdatedDate;

            dto.SkillList = new List<KeyValueDTO>();

            if(this.Skills.Count > 0)
            {
                dto.SkillList = this.Skills.Select(skill => new KeyValueDTO()
                {
                    Key = skill.Key.ToString(),
                    Value = skill.Value.ToString()
                }).ToList();
            }
                        
            dto.AvailableJobCriteriaMapDTO = new List<AvailableJobCriteriaMapDTO>();
            if(this.JobCriteria.Count > 0)
            {
                dto.AvailableJobCriteriaMapDTO = this.JobCriteria.Select(cr => new AvailableJobCriteriaMapDTO()
                {
                    CriteriaID = cr.CriteriaID,
                    JobID = cr.JobID,
                    TypeID = string.IsNullOrEmpty(cr.JobCriteria) ? (int?)null : int.Parse(cr.JobCriteria),
                    QualificationID = string.IsNullOrEmpty(cr.EducationQualification) ? (byte?)null : byte.Parse(cr.EducationQualification),
                    FieldOfStudy = cr.FieldOfStudy,
                }).ToList();
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<JobsDTO>(jsonString);
        }
    }
}
