using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "WPS", "CRUDModel.ViewModel")]
    [DisplayName("WPS")]
    public class WPSViewModel : BaseMasterViewModel
    {
        public WPSViewModel()
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            WPSGrid = new List<WPSGridViewModel>() { new WPSGridViewModel() };
            FileCreationDateString = DateTime.Now.ToString(dateFormat);
        }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("School", "Numeric", false, "SchoolChanges($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.School")]
        [CustomDisplay("School")]
        public KeyValueViewModel School { get; set; }
        public int? SchoolID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("BankName", "Numeric", false, "BankChanges($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.BankName")]
        [CustomDisplay("Payer Bank")]
        public KeyValueViewModel Bank { get; set; }
        public int? BankID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DateTimePicker,"disabled")]
        [CustomDisplay("File Creation Date")]
        public string FileCreationDateString { get; set; }
        public System.DateTime? FileCreationDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Employer EID")]
        public string EmployerEID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Payer EID")]
        public string PayerEID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Payer QID")]
        public string PayerQID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Payer Bank Short Name")]
        public string PayerBankShortName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Payer IBAN")]
        public string PayerIBAN { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Years", "Numeric", false, "")]
        [LookUp("LookUps.Years")]
        [CustomDisplay("Year")]
        public KeyValueViewModel Year { get; set; }
        public int? YearID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Months", "Numeric", false, "")]
        [LookUp("LookUps.Months")]
        [CustomDisplay("Month")]
        public KeyValueViewModel Month { get; set; }
        public int? MonthID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "textleft", "ng-click='GenerateWPS($event, $element, CRUDModel.ViewModel)'")]
        [CustomDisplay("Generate")]
        public string GenerateWPS { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("WPS Grid")]
        public List<WPSGridViewModel> WPSGrid { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SalarySlipDTO);
        }

            
        public override BaseMasterDTO ToDTO()
        {
            Mapper<WPSViewModel, SalarySlipDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<WPSViewModel, SalarySlipDTO>.Map(this);

            return dto;
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<WPSViewModel>(jsonString);
        }

    }
}
