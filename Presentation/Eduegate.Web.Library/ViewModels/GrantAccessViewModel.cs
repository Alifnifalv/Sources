using System.Collections.Generic;
using System.ComponentModel;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Web.Library.Common;
using Eduegate.Services.Contracts.Security;
using Newtonsoft.Json;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Granr=tAccess", "CRUDModel.ViewModel")]
    [DisplayName("Grant Acess")]
    public class GrantAccessViewModel : BaseMasterViewModel
    {
        public GrantAccessViewModel()
        {
            AssociateTeacher = new KeyValueViewModel();
            SecuritySettings = new List<GrandAccessSettingsViewModel>() { new GrandAccessSettingsViewModel() };
        }

        public long GrantAccessIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AssociateTeacher", "Numeric", false, "PaymodeChanges($event, $index, CRUDModel.ViewModel)", false, "ng-click=getClaimsets(CRUDModel.ViewModel)")]
        [LookUp("LookUps.AssociateTeacher")]
        [CustomDisplay("AssociateTeacher")]
        public KeyValueViewModel AssociateTeacher { get; set; }

        //[Required]
        //[EmailAddress]
        //[ControlType(Framework.Enums.ControlTypes.TextBox,"", attribs: "ng-crud-unique controllercall=" + "'Login/CheckCustomerEmailIDAvailability?loginID={{CRUDModel.ViewModel.UserID}}&loginEmailID={{CRUDModel.ViewModel.LoginEmailID}}'" + " message=' already exist.'")]
        //[CustomDisplay("LoginEmailID")]
        //[MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        //public string LoginEmailID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("UserName")]
        //[MaxLength(200, ErrorMessage = "Maximum Length should be within 200!")]
        //public string UserName { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Grid, "SecuritySettings", "SecuritySettings")]
        //[CustomDisplay("Accesses")]
        //public Security.SecuritySettingsViewModel SecuritySettings { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Accesses")]
        public List<GrandAccessSettingsViewModel> SecuritySettings { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Grid)]
        //[DisplayName("Settings")]
        //public List<Common.GridSettingsViewModel> Settings { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ClaimLoginMainMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<GrantAccessViewModel>(jsonString);
        }

        public static GrantAccessViewModel FromDTO(ClaimLoginMapDTO dto)
        {
            Mapper<ClaimLoginMapDTO, GrantAccessViewModel>.CreateMap();
            var vm = Mapper<ClaimLoginMapDTO, GrantAccessViewModel>.Map(dto);


            //if (dto.ClaimSets != null)
            //{
            //    vm.SecuritySettings.ClaimSets = KeyValueViewModel.FromDTO(dto.ClaimSets);
            //}

            //if (dto.UserSettings != null)
            //{
            //    vm.Settings = GridSettingsViewModel.FromDTO(dto.UserSettings, string.IsNullOrEmpty(dto.LoginID) ? (long?)null : long.Parse(dto.LoginID));
            //}
            //else
            //{
            //    vm.Settings.Add(new GridSettingsViewModel());
            //}

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            var dto = new ClaimLoginMainMapDTO()
            {
                AssociateTeacherID = this == null || this.AssociateTeacher == null || this.AssociateTeacher.Key == null ? 0 : long.Parse(this.AssociateTeacher.Key),
            };

            dto.LoginMaps = new List<ClaimLoginMapDTO>();

            foreach (var logins in this.SecuritySettings)
            {
                if (logins.IsRowSelected == true)
                {
                    dto.LoginMaps.Add(new ClaimLoginMapDTO()
                    {
                        ClaimID = logins.ClaimIID,

                    });
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClaimLoginMainMapDTO>(jsonString);
        }

    }
}