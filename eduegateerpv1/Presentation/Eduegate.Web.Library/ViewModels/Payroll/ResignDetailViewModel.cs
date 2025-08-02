using Eduegate.Framework.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "ResignDetail", "CRUDModel.ViewModel.ResignDetail")]
    [DisplayName("Employee Bank Details")]
    public class ResignDetailViewModel : BaseMasterViewModel
    {
        public ResignDetailViewModel()
        {

        }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("LastWorkingDateOfEmployee")]
        public string LastDateString { get; set; }
        public DateTime? LastDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.EmployeeLeavingType")]
        [CustomDisplay("LeavingType")]
        public string LeavingType { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("ResignationDate")]
        public string ResignationDateString { get; set; }
        public DateTime? ResignationDate { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("Grades")]
        //public string Grades { get; set; }
    }
}
