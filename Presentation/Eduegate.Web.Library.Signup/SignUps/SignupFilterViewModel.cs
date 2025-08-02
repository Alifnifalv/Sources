using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.SignUp.SignUps
{
    [ContainerType(Framework.Enums.ContainerTypes.FieldSet, "SignupFilter", "CRUDModel.ViewModel.SignupFilter")]
    public class SignupFilterViewModel : BaseMasterViewModel
    {
        public SignupFilterViewModel()
        {
            SignupSlotType = new Domain.Setting.SettingBL().GetSettingValue<string>("SIGNUP_SLOT_TYPEID_SINGLE", 1);
            SlotMapStatusID = new Domain.Setting.SettingBL().GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_OPEN", 1);
            WeekDays = new List<KeyValueViewModel>();
            RecurringSlotTypeID = new Domain.Setting.SettingBL().GetSettingValue<byte>("SIGNUP_SLOT_TYPEID_RECURRING", 2);
        }

        [ControlType(Framework.Enums.ControlTypes.TimePicker, attribs: "ng-disabled='CRUDModel.ViewModel.SignupOldStatusID==CRUDModel.ViewModel.SignupPublishedStatusID'")]
        [CustomDisplay("StartTime")]
        public string StartTimeString { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TimePicker, attribs: "ng-disabled='CRUDModel.ViewModel.SignupOldStatusID==CRUDModel.ViewModel.SignupPublishedStatusID'")]
        [CustomDisplay("EndTime")]
        public string EndTimeString { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled='CRUDModel.ViewModel.SignupOldStatusID == CRUDModel.ViewModel.SignupPublishedStatusID' ng-change='ValidateDigitsOnly(CRUDModel.ViewModel.SignupFilter)'")]
        [CustomDisplay("DurationInMinutes")]
        public string Duration { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled='CRUDModel.ViewModel.SignupOldStatusID == CRUDModel.ViewModel.SignupPublishedStatusID' ng-change='ValidateDigitsOnly(CRUDModel.ViewModel.SignupFilter)'")]
        [CustomDisplay("BufferTimeInMinutes")]
        public string BufferTime { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "", attribs: "ng-disabled='CRUDModel.ViewModel.SignupOldStatusID==CRUDModel.ViewModel.SignupPublishedStatusID'")]
        [CustomDisplay("Type")]
        [LookUp("LookUps.SignupSlotTypes")]
        public string SignupSlotType { get; set; }
        public byte SignupSlotTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("WeekDays", "String", true, "", optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.SignupOldStatusID == CRUDModel.ViewModel.SignupPublishedStatusID || CRUDModel.ViewModel.SignupFilter.SignupSlotType != CRUDModel.ViewModel.SignupFilter.RecurringSlotTypeID'")]
        [CustomDisplay("WeekDays")]
        [LookUp("LookUps.WeekShortNames")]
        public List<KeyValueViewModel> WeekDays { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='ApplyButtonClick(CRUDModel.ViewModel)' ng-disabled='CRUDModel.ViewModel.SignupOldStatusID == CRUDModel.ViewModel.SignupPublishedStatusID'")]
        [CustomDisplay("Applybelow")]
        public string ApplyButton { get; set; }


        public byte? SlotMapStatusID { get; set; }
        public byte RecurringSlotTypeID { get; set; }
    }
}