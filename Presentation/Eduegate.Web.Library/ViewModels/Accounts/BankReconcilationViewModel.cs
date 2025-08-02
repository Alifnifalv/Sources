using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Accounts.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    public class BankReconcilationViewModel
    {
        public BankReconcilationViewModel()
        {
            IsEdit = false;
            BankReconciliationManualEntry = new List<BankReconciliationManualEntry>() { new BankReconciliationManualEntry() };
            BankReconciliationBankTrans = new List<BankReconciliationBankTransViewModel>() { new BankReconciliationBankTransViewModel() };
            BankReconciliationLedgerTrans = new List<BankReconciliationLedgerTransViewModel>() { new BankReconciliationLedgerTransViewModel() };
            BankReconciliationEntries = new List<BankReconciliationEntriesViewModel>() { new BankReconciliationEntriesViewModel() };
            BankReconciliationMatchedEntries = new List<BankReconciliationMatchedEntriesViewModel>() { new BankReconciliationMatchedEntriesViewModel() };
            BankReconciliationUnMatchedWithLedgerEntries = new List<BankReconciliationUnMatchedWithLedgerEntriesViewModel>() { new BankReconciliationUnMatchedWithLedgerEntriesViewModel() };
            BankReconciliationUnMatchedWithBankEntries = new List<BankReconciliationUnMatchedWithBankEntriesViewModel>() { new BankReconciliationUnMatchedWithBankEntriesViewModel() };
            BankReconciliationMatchingBankTransEntries=new  List<BankReconciliationBankTransViewModel>() { new BankReconciliationBankTransViewModel() };
            BankReconciliationMatchingLedgerEntries=new List<BankReconciliationLedgerTransViewModel>() { new BankReconciliationLedgerTransViewModel() };
        }
      
        public bool IsEdit { get; set; }
        public string SelfCheckingString { get; set; }
        public decimal? BankOpeningBalance { get; set; }
        public decimal? BankClosingBalance { get; set; }
        public decimal? LedgerOpeningBalance { get; set; }
        public decimal? LedgerClosingBalance { get; set; }
        public long BankReconciliationHeadIID { get; set; }
        public string BankName { get; set; }
        public string ContentFileID { get; set; }
        public string ContentFileName { get; set; }
        public string ContentFileData { get; set; }
        public long? BankStatementID { get; set; }
        public long? BankAccountID { get; set; }        
        public DateTime? FromDate { get; set; }       
        public DateTime? ToDate { get; set; }
        public string FromDateString { get; set; }
        public string ToDateString { get; set; }     
        public short? BankReconciliationStatusID { get; set; }
        public List<BankReconciliationManualEntry> BankReconciliationManualEntry { get; set; }
        public List<BankReconciliationBankTransViewModel> BankReconciliationBankTrans { get; set; }
        public List<BankReconciliationLedgerTransViewModel> BankReconciliationLedgerTrans { get; set; }
        public List<BankReconciliationEntriesViewModel> BankReconciliationEntries { get; set; }

        public List<BankReconciliationMatchedEntriesViewModel> BankReconciliationMatchedEntries { get; set; }

        public List<BankReconciliationUnMatchedWithLedgerEntriesViewModel> BankReconciliationUnMatchedWithLedgerEntries { get; set; }

        public List<BankReconciliationUnMatchedWithBankEntriesViewModel> BankReconciliationUnMatchedWithBankEntries { get; set; }

        public List<BankStatementEntryDTO> BankStatementEntries { get; set; }

        public List<BankReconciliationBankTransViewModel> BankReconciliationMatchingBankTransEntries { get; set; }

        public List<BankReconciliationLedgerTransViewModel> BankReconciliationMatchingLedgerEntries { get; set; }
    }
}
