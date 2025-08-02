using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "SupplierBankDetails", "CRUDModel.ViewModel.BankDetails")]
    [DisplayName("BankDetails")]
    public class BankDetailViewModel: BaseMasterViewModel
    {
        public BankDetailViewModel()
        {
            BankAccounts = new List<BankAccountViewModel>();
          
        }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [DisplayName("Company Location")]
        public string CompanyLocation { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "SupplierBankAccounts", "BankAccounts")]
        [DisplayName("BankAccounts")]
        public List<BankAccountViewModel> BankAccounts { get; set; }
    }
}
