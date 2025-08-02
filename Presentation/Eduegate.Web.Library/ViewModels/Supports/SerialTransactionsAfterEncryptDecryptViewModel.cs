using System;
using System.ComponentModel;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Values", "CRUDModel.ViewModel.EncryptDecryptKeys.Values")]
    [DisplayName("")]
    public class SerialTransactionsAfterEncryptDecryptViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Key")]
        public long Key { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Value")]  
        public string Value { get; set; }

        

       
    }
}
