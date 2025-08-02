using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;
using System;
using System.Globalization;
using Eduegate.Framework.Extensions;


namespace Eduegate.Web.Library.School.Mutual
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "SchoolBankDetails", "CRUDModel.ViewModel.SchoolBankDetails")]
    [DisplayName("Bank Details")]
    public class SchoolBankDetailsViewModel : BaseMasterViewModel
    {
        public SchoolBankDetailsViewModel() {

            PayerGrid = new List<SchoolBankPayerGridViewModel>() { new SchoolBankPayerGridViewModel() };
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        [CustomDisplay("Employer QID")]
        public string EmployerEID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        [CustomDisplay("Payer EID")]
        public string PayerEID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        [CustomDisplay("Payer QID")]
        public string PayerQID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Payer Bank Details")]
        public List<SchoolBankPayerGridViewModel> PayerGrid { get; set; }
    }
}