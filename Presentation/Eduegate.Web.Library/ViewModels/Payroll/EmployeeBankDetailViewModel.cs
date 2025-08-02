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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "BankDetail", "CRUDModel.ViewModel.BankDetail")]
    [DisplayName("Employee Bank Details")]
    public class EmployeeBankDetailViewModel : BaseMasterViewModel
    {
        public EmployeeBankDetailViewModel()
        {

        }

        public long EmployeeBankIID { get; set; }
        public long? EmployeeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.BankName")]
        [CustomDisplay("BankName")]
        public string EmpBankName { get; set; }
        public int? BankID { get; set; }

        //[Required]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("BankAccountNumber")]
        public string AccountNo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("IBANNumber")]
        public string IBAN { get; set; }

        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("BankShortName")]
        public string SwiftCode { get; set; }
    }
}
