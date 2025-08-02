using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;
using System.Globalization;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Domain;
using Eduegate.Web.Library.Common;
using Eduegate.Services.Contracts.School.Students;
using Newtonsoft.Json;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Tender", "CRUDModel.ViewModel")]
    [DisplayName("Tender")]
    public class TenderMasterViewModel : BaseMasterViewModel
    {
        private static string dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

        public TenderMasterViewModel()
        {
            AuthendicationList = new List<TenderAuthenticationViewModel>() { new TenderAuthenticationViewModel() };
        }

        public long TenderIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Tender Name")]
        public string Name { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Title")]
        public string Title { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("TenderType", "Numeric", false, "")]
        [CustomDisplay("Type")]
        [LookUp("LookUps.TenderTypes")]
        public KeyValueViewModel TenderType { get; set; }
        public long? TenderTypeID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Start Date")]
        public string StartDateString { get; set; }
        public DateTime StartDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("End Date")]
        public string EndDateString { get; set; }
        public DateTime EndDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Submission Date")]
        public string SubmissionDateString { get; set; }
        public DateTime SubmissionDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Opening Date")]
        public string OpeningDateString { get; set; }
        public DateTime OpeningDate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("Is Opened")]
        public bool? IsOpened { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Number of authorities")]
        public int? NumOfAuthorities { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("TenderStatus", "Numeric", false, "")]
        [CustomDisplay("Status")]
        [LookUp("LookUps.TenderStatuses")]
        public KeyValueViewModel TenderStatus { get; set; }
        public long? TenderStatusID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Authendication List / Tender Committee")]
        public List<TenderAuthenticationViewModel> AuthendicationList { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as TenderMasterDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<TenderMasterViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<TenderMasterDTO, TenderMasterViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var tDto = dto as TenderMasterDTO;
            var vm = Mapper<TenderMasterDTO, TenderMasterViewModel>.Map(tDto);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.TenderIID = tDto.TenderIID;
            vm.Name = tDto.Name;
            vm.Description = tDto.Description;
            vm.Title = tDto.Title;
            vm.IsActive = tDto.IsActive;
            vm.IsOpened = tDto.IsOpened;

            vm.TenderType = tDto.TenderTypeID.HasValue ? new KeyValueViewModel() { Key = tDto.TenderType.Key, Value = tDto.TenderType.Value } : new KeyValueViewModel();
            vm.TenderStatus = tDto.TenderStatusID.HasValue ? new KeyValueViewModel() { Key = tDto.TenderStatus.Key, Value = tDto.TenderStatus.Value } : new KeyValueViewModel();

            vm.StartDateString = tDto.StartDate.HasValue ? tDto.StartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.EndDateString = tDto.EndDate.HasValue ? tDto.EndDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.SubmissionDateString = tDto.SubmissionDate.HasValue ? tDto.SubmissionDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.OpeningDateString = tDto.OpeningDate.HasValue ? tDto.OpeningDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.NumOfAuthorities = tDto.NumOfAuthorities;

            vm.AuthendicationList = new List<TenderAuthenticationViewModel>();

            foreach(var dat in tDto.TenderAuthenticationList)
            {
                vm.AuthendicationList.Add(new TenderAuthenticationViewModel()
                {
                    AuthenticationID = dat.AuthenticationID,
                    UserID = dat.UserID,
                    UserName = dat.UserName,
                    Password = dat.Password,
                    OldPassword = dat.OldPassword,
                    OldPasswordSalt = dat.OldPasswordSalt,
                    IsActive = dat.IsActive,
                    IsApprover = dat.IsApprover,
                    EmailID = dat.EmailID,
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<TenderMasterViewModel, TenderMasterDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<TenderMasterViewModel, TenderMasterDTO>.Map(this);

            dto.TenderIID = this.TenderIID;
            dto.Name = this.Name;
            dto.Description = this.Description;
            dto.Title = this.Title;

            dto.TenderTypeID = string.IsNullOrEmpty(this.TenderType.Key) ? (long?)null : long.Parse(this.TenderType.Key);
            dto.TenderStatusID = string.IsNullOrEmpty(this.TenderStatus.Key) ? (long?)null : long.Parse(this.TenderStatus.Key);

            dto.StartDate = string.IsNullOrEmpty(this.StartDateString) ? (DateTime?)null : DateTime.ParseExact(this.StartDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.EndDate = string.IsNullOrEmpty(this.EndDateString) ? (DateTime?)null : DateTime.ParseExact(this.EndDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.SubmissionDate = string.IsNullOrEmpty(this.SubmissionDateString) ? (DateTime?)null : DateTime.ParseExact(this.SubmissionDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.OpeningDate = string.IsNullOrEmpty(this.OpeningDateString) ? (DateTime?)null : DateTime.ParseExact(this.OpeningDateString, dateFormat, CultureInfo.InvariantCulture);

            dto.IsOpened = this.IsOpened;
            dto.IsActive = this.IsActive;
            dto.NumOfAuthorities = this.NumOfAuthorities;

            dto.TenderAuthenticationList = new List<TenderAuthenticationDTO>();

            foreach (var list in this.AuthendicationList)
            {
                if(list.UserName != null)
                {
                    dto.TenderAuthenticationList.Add(new TenderAuthenticationDTO()
                    {
                        AuthenticationID = list.AuthenticationID,
                        UserName = list.UserName,
                        UserID = list.UserID,
                        EmailID = list.EmailID,
                        Password = list.Password,
                        IsActive = list.IsActive,
                        IsApprover = list.IsApprover,
                        OldPassword = list.OldPassword,
                        OldPasswordSalt = list.OldPasswordSalt,
                    });
                }
            }

            return dto;
        }


        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<TenderMasterDTO>(jsonString);
        }

    }
}