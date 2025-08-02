using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "WPSGrid", "CRUDModel.ViewModel.WPSGrid")]
    [DisplayName("WPS List")]
    public class WPSGridViewModel : BaseMasterViewModel
    {
        public WPSGridViewModel()
        {

        }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Created Date")]
        public string FileCreatedDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("School")]
        public string School { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Employer EID")]
        public string EmployerEID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Payer EID")]
        public string PayerEID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Bank")]
        public string Bank { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Year")]
        public string Year { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Month")]
        public string Month { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Total Salaries")]
        public string TotalSalaries { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Total records")]
        public string TotalRecords { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "textleft", "ng-click='GenerateExcel(gridModel)'")]
        [CustomDisplay("Excel")]
        public string ExportExcel { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "textleft", "ng-click='GenerateCSV(gridModel)'")]
        [CustomDisplay("CSV")]
        public string ExportCSV { get; set; }
    }
}