using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Eduegate.Framework;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.HR;
using Eduegate.Web.Library.ViewModels.Common;

namespace Eduegate.Web.Library.ViewModels.HR
{
    public class JobOpeningViewModel : BaseMasterViewModel
    {
        public JobOpeningViewModel()
        {
            JobTitle = new MultiLanguageText();
            JobDescription = new MultiLanguageText();
            JobDetail = new MultiLanguageText();
            Tags = new List<KeyValueViewModel>();
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("ID")]
        public string Id { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.TypeOfJob")]
        [DisplayName("TypeOfJob")]
        public string TypeOfJob { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBoxWithMultiLanguage)]
        [Required]
        [DisplayName("Job Title")]
        public MultiLanguageText JobTitle { get; set; }

        [ControlType(Framework.Enums.ControlTypes.RichTextEditorMultiLanguage)]
        [Required]
        [DisplayName("Job Short Description")]
        public MultiLanguageText JobDescription { get; set; }

        [ControlType(Framework.Enums.ControlTypes.RichTextEditorMultiLanguage)]
        [Required]
        [DisplayName("Job Full Description")]
        public MultiLanguageText JobDetail { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Department")]
        [DisplayName("Department")]
        public string Department { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Tags")]
        [Select2("Tags", "String", true, null, true, isTagUpper:false)]
        [LookUp("LookUps.Tags")]
        public List<KeyValueViewModel> Tags { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.JobStatus")]
        [DisplayName("Status")]
        public string JobStatus { get; set; }

        private DateTime? createdDate;
        public long JobIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Created on")]
        public string CreatedDate
        {
            get
            {
                return createdDate.HasValue ? createdDate.Value.ToString("dd MMM, yyyy") : string.Empty;
            }
            set {
                if (string.IsNullOrEmpty(value))
                    createdDate = (DateTime?)null;
                else
                    createdDate = Convert.ToDateTime(value);
            }
        }

        //public override JobOpeningViewModel InitializeVM(List<CultureDataInfoViewModel> datas)
        //{
        //    this.JobDescription = new MultiLanguageText(datas); //dto.JobDescription,
        //    this.JobDetail = new MultiLanguageText(datas); //dto.JobDetail,
        //    this.JobTitle = new MultiLanguageText(datas); //dto.JobTitle,
        //    return this;
        //} 

        public override void InitializeVM(List<CultureDataInfoViewModel> datas)
        {
            this.JobDescription = new MultiLanguageText(datas); //dto.JobDescription,
            this.JobDetail = new MultiLanguageText(datas); //dto.JobDetail,
            this.JobTitle = new MultiLanguageText(datas); //dto.JobTitle,
        }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<JobOpeningViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dtoBase, List<CultureDataInfoDTO> cultures)
        {
            var dto = dtoBase as JobOpeningDTO;
            var cultureVm = CultureDataInfoViewModel.FromDTO(cultures);

            var vm = new JobOpeningViewModel()
            {
                Id = dto.Id,
                CreatedDate = dto.CreatedDate.ToString(),
                JobDescription = new MultiLanguageText() { Text = dto.JobDescription, CultureDatas = CultureDataInfoViewModel.FromDTO(cultures)}, //dto.JobDescription,
                JobDetail = new MultiLanguageText() { Text = dto.JobDetail, CultureDatas = CultureDataInfoViewModel.FromDTO(cultures) }, //dto.JobDetail,
                JobTitle = new MultiLanguageText() { Text = dto.JobTitle, CultureDatas = CultureDataInfoViewModel.FromDTO(cultures) }, //dto.JobTitle,
                Department = dto.DepartmentId.ToString(),
                TypeOfJob = dto.TypeOfJob,
                JobStatus = dto.JobStatus,
                JobIID = dto.JobIID,
            };

            if (cultures != null)
            {
                bool isFirst = true;

                foreach (var culture in cultureVm)
                {
                    var cultureDTO = dto.CultureDatas.FirstOrDefault(a => a.CulturID == culture.CultureID);

                    if (isFirst && cultureDTO == null)
                    {
                        cultureDTO = new JobOpeningCultureDataDTO()
                        {
                            CulturID = culture.CultureID,
                            JobIID = dto.JobIID,
                            JobDescription = dto.JobDescription,
                            JobDetail = dto.JobDetail,
                            JobTitle = dto.JobTitle,
                        };

                        isFirst = false;
                        //continue;
                    }
                    vm.JobDescription.SetValueByCultureID(culture, cultureDTO == null ? string.Empty : cultureDTO.JobDescription, cultureDTO == null ? null : cultureDTO.TimeStamps);
                    vm.JobTitle.SetValueByCultureID(culture, cultureDTO == null ? string.Empty : cultureDTO.JobTitle, cultureDTO == null ? null : cultureDTO.TimeStamps);
                    vm.JobDetail.SetValueByCultureID(culture, cultureDTO == null ? string.Empty : cultureDTO.JobDetail, cultureDTO == null ? null : cultureDTO.TimeStamps);
                }
            }

            if (dto.Tags != null)
            {
                vm.Tags = KeyValueViewModel.FromDTO(dto.Tags);
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO(CallContext context)
        {
            return ToDTO(this, null);
        }

        public static JobOpeningDTO ToDTO(JobOpeningViewModel vm, List<CultureDataInfoViewModel> cultures)
        {
            var dto = new JobOpeningDTO()
            {
                Id = vm.Id,
                JobTitle = vm.JobTitle.Text,
                JobDescription = vm.JobDescription.Text,
                JobDetail = vm.JobDetail.Text,
                DepartmentId = int.Parse(vm.Department),
                CreatedDate = string.IsNullOrWhiteSpace(vm.CreatedDate) ? DateTime.Now : DateTime.Parse(vm.CreatedDate),
                TypeOfJob = vm.TypeOfJob,
                JobStatus= vm.JobStatus,
                JobIID = vm.JobIID,
            };

            dto.CultureDatas = ToCultureDTO(vm, cultures);
            dto.Tags = KeyValueViewModel.ToDTO(vm.Tags);
            return dto;
        }

        public static List<JobOpeningCultureDataDTO> ToCultureDTO(JobOpeningViewModel vm, List<CultureDataInfoViewModel> cultures)
        {
            var dtos = new List<JobOpeningCultureDataDTO>();
            bool isFrist = true;

            if(cultures == null)
            {
                foreach (var culture in vm.JobDescription.CultureDatas)
                {
                    dtos.Add(new JobOpeningCultureDataDTO()
                    {
                        CulturID = culture.CultureID,
                        JobIID = vm.JobIID,
                        JobDescription = vm.JobDescription.GetValueByCultureID(culture.CultureID),
                        JobDetail = vm.JobDetail.GetValueByCultureID(culture.CultureID),
                        JobTitle = vm.JobTitle.GetValueByCultureID(culture.CultureID),
                    });
                }                   
            }
            else 
            foreach (var culture in cultures)
            {
                //Assume that first one is the default culture which will be there by default.
                if (isFrist)
                {
                    isFrist = false;
                    continue;
                }

                dtos.Add(new JobOpeningCultureDataDTO()
                {
                    CulturID = culture.CultureID,
                    JobIID = vm.JobIID,
                    JobDescription = vm.JobDescription.GetValueByCultureID(culture.CultureID),
                    JobDetail = vm.JobDetail.GetValueByCultureID(culture.CultureID),
                    JobTitle = vm.JobTitle.GetValueByCultureID(culture.CultureID),
                });
            }

            return dtos;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<JobOpeningDTO>(jsonString);
        }
        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            return ToVM(dto as JobOpeningDTO, base.Cultures);
        }
    }
}
