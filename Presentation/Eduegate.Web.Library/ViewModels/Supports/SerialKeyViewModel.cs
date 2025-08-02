using System;
using System.Collections.Generic;
using System.ComponentModel;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts.Supports;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Web.Library.ViewModels.Supports
{
    [ContainerType(Framework.Enums.ContainerTypes.Default, "SerialKeys", "CRUDModel.ViewModel", "class='alignleft two-column-header'")]
    [DisplayName("SerialKeys Tracking")]
    public class SerialKeyViewModel : BaseMasterViewModel
    {
        public SerialKeyViewModel() 
        {
            Transactions = new List<SerialTransactionsViewModel>();
            EncryptDecryptKeys = new SerialTransactionEncryptDecrypt();
        }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("Serial No")]
        public string SerialNo { get; set; }
         
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Is Digital")]
        public bool IsDigital { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='SearchTransactions(CRUDModel.ViewModel.SerialNo,CRUDModel.ViewModel.IsDigital)'")]
        [DisplayName("Search")]
        public string Search { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='ResetSerialNo(CRUDModel.ViewModel.SerialNo)'")]
        [DisplayName("Reset")]
        public string Reset { get; set; } 


        [ControlType(Framework.Enums.ControlTypes.Grid, "onecol-header-left")]
        [DisplayName("Transactions")] 
        public List<SerialTransactionsViewModel> Transactions { get; set; }


        [ContainerType(Framework.Enums.ContainerTypes.Tab, "EncryptDecryptKeys", "EncryptDecryptKeys")]
        [DisplayName("EncryptDecryptKeys")]
        public SerialTransactionEncryptDecrypt EncryptDecryptKeys { get; set; }

        public static List<SerialTransactionsViewModel> FromDTO(List<TransactionHeadDTO> dto)
        { 
            SerialKeyViewModel serialViewModel = new SerialKeyViewModel();

            if (dto.IsNotNull() && dto.Count > 0)
            {
                SerialTransactionsViewModel tVM = null;

                foreach (TransactionHeadDTO tDTO in dto)
                {
                    tVM = new SerialTransactionsViewModel();

                    tVM.HeadIID = tDTO.HeadIID;
                    tVM.TransactionNo = tDTO.TransactionNo;
                    tVM.TransactionDate = tDTO.TransactionDate;
                    tVM.CustomerName = tDTO.CustomerName;
                    tVM.SupplierName = tDTO.SupplierName;
                    tVM.InventoryTypeName = tDTO.DocumentReferenceType;
                    

                    serialViewModel.Transactions.Add(tVM);

                }
            }
            return serialViewModel.Transactions;
        }

    }
}

