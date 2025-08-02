using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Jobs;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.School.Fees;
using Eduegate.Web.Library.School.Transports;
using Eduegate.Web.Library.ViewModels;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
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
    public class CareerLoginViewModel : BaseMasterViewModel
    {
        public CareerLoginViewModel()
        {
            PasswordUpdate = false;
        }

        public long? LoginID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("User ID")]
        public string UserID { get; set; }     
        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Email ID")]
        public string EmailID { get; set; } 
        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Username")]
        public string UserName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine6 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("Is update password")]
        public bool? PasswordUpdate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Password,attribs:"ng-disabled=!CRUDModel.ViewModel.PasswordUpdate")]
        [CustomDisplay("Password")]
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Last OTP")]
        public string OTP { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("Is Active")]
        public bool? IsActive { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as RegisterUserDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<CareerLoginViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<RegisterUserDTO, CareerLoginViewModel>.CreateMap();
            var loginDto = dto as RegisterUserDTO;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var vm = Mapper<RegisterUserDTO, CareerLoginViewModel>.Map(dto as RegisterUserDTO);

            vm.LoginID = loginDto.LoginID;
            vm.UserID = loginDto.UserID;

            vm.Password = loginDto.Password;
            vm.PasswordSalt = loginDto.PasswordSalt;

            vm.EmailID = loginDto.EmailID;
            vm.UserName = loginDto.UserName;
            vm.IsActive = loginDto.IsActive;
            vm.OTP = loginDto.OTP;

            vm.CreatedBy = loginDto.CreatedBy;
            vm.CreatedDate = loginDto.CreatedDate;
            vm.UpdatedBy = loginDto.UpdatedBy;
            vm.UpdatedDate = loginDto.UpdatedDate;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<CareerLoginViewModel, RegisterUserDTO>.CreateMap();
            var dto = Mapper<CareerLoginViewModel, RegisterUserDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.LoginID = this.LoginID;
            dto.UserID = this.UserID;

            dto.Password = this.Password;
            dto.PasswordSalt = this.PasswordSalt;

            dto.EmailID = this.EmailID;
            dto.UserName = this.UserName;
            dto.IsActive = this.IsActive;
            dto.OTP = this.OTP;

            dto.CreatedBy = this.CreatedBy;
            dto.CreatedDate = this.CreatedDate;
            dto.UpdatedBy = this.UpdatedBy;
            dto.UpdatedDate = this.UpdatedDate;

            dto.PasswordUpdate = this.PasswordUpdate == true ? true : false;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<RegisterUserDTO>(jsonString);
        }
    }
}
