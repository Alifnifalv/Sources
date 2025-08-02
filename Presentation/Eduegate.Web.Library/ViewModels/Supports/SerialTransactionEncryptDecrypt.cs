using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "EncryptDecryptKeys", "CRUDModel.ViewModel.EncryptDecryptKeys")]
    [DisplayName("")]
    public class SerialTransactionEncryptDecrypt : BaseMasterViewModel
    {
        public SerialTransactionEncryptDecrypt()
        {
            Values = new List<SerialTransactionsAfterEncryptDecryptViewModel>();
        }
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("Keys")]
        public string Keys { get; set; }

        [ControlType(Framework.Enums.ControlTypes.YesNoCheckBox)]
        [DisplayName("Encrypt?")]
        public bool IsEncrypted { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='GetKeysEncryptDecrypt(CRUDModel.ViewModel.EncryptDecryptKeys.Keys,CRUDModel.ViewModel.EncryptDecryptKeys.IsEncrypted)'")]
        [DisplayName("Search")]
        public string Search { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid, "onecol-header-left")]
        [DisplayName("Values")]
        public List<SerialTransactionsAfterEncryptDecryptViewModel> Values { get; set; }

    }
}
