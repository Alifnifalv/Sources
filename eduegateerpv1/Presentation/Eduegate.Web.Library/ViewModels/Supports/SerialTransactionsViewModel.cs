using System;
using System.ComponentModel;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "Transactions", "CRUDModel.ViewModel.Transactions")]
    [DisplayName("")]
    public class SerialTransactionsViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Head ID")]
        public long HeadIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("TransactionNo")]  
        public string TransactionNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Date")]
        public Nullable<DateTime> TransactionDate { get; set; } 

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Customer")]
        public string CustomerName { get; set; }

        public long CustomerID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Supplier")]
        public string SupplierName { get; set; }

        public string InventoryTypeName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='Edit(CRUDModel.ViewModel.Transactions[$index].InventoryTypeName,CRUDModel.ViewModel.Transactions[$index].HeadIID)' ng-show='CRUDModel.ViewModel.Transactions[$index].HeadIID > 0'")]
        [DisplayName("View")] 
        public string Edit { get; set; }
         
        public long SupplierID { get; set; }

       
    }
}
