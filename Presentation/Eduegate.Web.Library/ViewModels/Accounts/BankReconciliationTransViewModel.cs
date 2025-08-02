using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    public class BankReconciliationEntriesViewModel
    {
        public long? AccountID { get; set; }
        public string Particulars { get; set; }
        public string BankDescription { get; set; }
        public string Narration { get; set; }
        public string ChequeNo { get; set; }
        public decimal? LedgerDebitAmount { get; set; }
        public decimal? LedgerCreditAmount { get; set; }
        public decimal? BankCreditAmount { get; set; }
        public decimal? BankDebitAmount { get; set; }
        public decimal? ReconciliationCreditAmount { get; set; }
        public decimal? ReconciliationDebitAmount { get; set; }
        public string PostDate { get; set; }
        public string TransDate { get; set; }
        public long? TranHeadID { get; set; }
        public long? TranTailID { get; set; }
    }

    public class BankReconciliationMatchedEntriesViewModel
    {
        public long? AccountID { get; set; }
        public string Particulars { get; set; }
        public string BankDescription { get; set; }
        public string ChequeNo { get; set; }
        public decimal? LedgerDebitAmount { get; set; }
        public decimal? LedgerCreditAmount { get; set; }
        public decimal? BankCreditAmount { get; set; }
        public decimal? BankDebitAmount { get; set; }
        public decimal? ReconciliationCreditAmount { get; set; }
        public decimal? ReconciliationDebitAmount { get; set; }
        public string PostDate { get; set; }
        public string TransDate { get; set; }
        public string ChequeDate { get; set; }
        public long? TranHeadID { get; set; }
        public long? TranTailID { get; set; }
        public string PartyName { get; set; }
        public string Reference { get; set; }
        public long? BankStatementEntryID { get; set; }
        public string ReferenceGroupNo { get; set; }
        public string ReferenceGroupName { get; set; }
        public List<MatchedBankEntries> MatchedBankEntries { get; set; }
        public List<MatchedLedgerEntries> MatchedLedgerEntries { get; set; }
    }

    public class MatchedBankEntries
    {       
        public string BankDescription { get; set; }
        public string ChequeNo { get; set; }
        public string ChequeDate { get; set; }
        public string PostDate { get; set; }
        public string PartyName { get; set; }
        public string Reference { get; set; }
        public decimal? BankCreditAmount { get; set; }
        public decimal? BankDebitAmount { get; set; }
        public long? BankStatementEntryID { get; set; }
        public string ReferenceGroupNo { get; set; }
        public string ReferenceGroupName { get; set; }
    }    
    public class MatchedLedgerEntries
    {
        public long? AccountID { get; set; }
        public string Particulars { get; set; }
        public string ChequeNo { get; set; }
        public decimal? LedgerDebitAmount { get; set; }
        public decimal? LedgerCreditAmount { get; set; }
        public string TransDate { get; set; }
        public string ChequeDate { get; set; }
        public long? TranHeadID { get; set; }
        public long? TranTailID { get; set; }
        public string PartyName { get; set; }
        public string Reference { get; set; }
        public string ReferenceGroupNo { get; set; }
        public string ReferenceGroupName { get; set; }

    }
    
public class BankReconciliationUnMatchedWithBankEntriesViewModel
    {
        public long? BankStatementEntryID { get; set; }
        public long? AccountID { get; set; }
        public string Particulars { get; set; }
        public string BankDescription { get; set; }
        public string ChequeNo { get; set; }
        public decimal? LedgerDebitAmount { get; set; }
        public decimal? LedgerCreditAmount { get; set; }
        public decimal? BankCreditAmount { get; set; }
        public decimal? BankDebitAmount { get; set; }
        public decimal? ReconciliationCreditAmount { get; set; }
        public decimal? ReconciliationDebitAmount { get; set; }
        public string PostDate { get; set; }
        public string TransDate { get; set; }

        public string PartyName { get; set; }

        public string Reference { get; set; }

        public string ChequeDate { get; set; }
        public bool IsMoved { get; set; } = false;
        public bool IsMatching { get; set; } = false;
        public bool IsSelected { get; set; } = false;
        public long? TranHeadID { get; set; }
        public long? TranTailID { get; set; }
        public string ReferenceGroupNo { get; set; }
        public string ReferenceGroupName { get; set; }
    }

    public class BankReconciliationUnMatchedWithLedgerEntriesViewModel
    {
        public long? BankStatementEntryID { get; set; }
        public long? AccountID { get; set; }
        public string Particulars { get; set; }
        public string BankDescription { get; set; }
        public string ChequeNo { get; set; }
        public decimal? LedgerDebitAmount { get; set; }
        public decimal? LedgerCreditAmount { get; set; }
        public decimal? BankCreditAmount { get; set; }
        public decimal? BankDebitAmount { get; set; }
        public decimal? ReconciliationCreditAmount { get; set; }
        public decimal? ReconciliationDebitAmount { get; set; }
        public string PostDate { get; set; }
        public string TransDate { get; set; }

        public string PartyName { get; set; }

        public string Reference { get; set; }

        public string ChequeDate { get; set; }
        public bool IsMoved { get; set; } = false;
        public bool IsMatching { get; set; } = false;       
        public bool IsSelected { get; set; } = false;
        public long? TranHeadID { get; set; }
        public long? TranTailID { get; set; }
        public string ReferenceGroupNo { get; set; }
        public string ReferenceGroupName { get; set; }


    }

    public class BankReconciliationManualEntry
    {
        public long? BankStatementEntryID { get; set; }
        public long? AccountID { get; set; }
        public string Particulars { get; set; }
        public string BankDescription { get; set; }
        public string ChequeNo { get; set; }
        public string PartyName { get; set; }
        public decimal? LedgerDebitAmount { get; set; }
        public decimal? LedgerCreditAmount { get; set; }
        public decimal? BankCreditAmount { get; set; }
        public decimal? BankDebitAmount { get; set; }
        public decimal? ReconciliationCreditAmount { get; set; }
        public decimal? ReconciliationDebitAmount { get; set; }
        public string PostDate { get; set; }
        public string TransDate { get; set; }
        public string ChequeDate { get; set; }
        public long? TranHeadID { get; set; }
        public long? TranTailID { get; set; }
        public string Reference { get; set; }
        public string ReferenceGroupNo { get; set; }
        public string ReferenceGroupName { get; set; }
    }

    public class BankReconciliationLedgerTransViewModel
    {
        public long? AccountID { get; set; }
        public string AccountName { get; set; }
        public decimal? LedgerDebitAmount { get; set; }
        public string Narration { get; set; }
        public decimal? LedgerCreditAmount { get; set; }
        public string PostDate { get; set; }

        public string ChequeNo { get; set; }

        public string PartyName { get; set; }

        public string Reference { get; set; }

        public string ChequeDate { get; set; }
        public bool IsMoved { get; set; } = false;
        public bool IsMatching { get; set; } = false;
        public bool IsSelected { get; set; } = false;
        public long? TranHeadID { get; set; }
        public long? TranTailID { get; set; }

        public decimal? LedgerOpeningBalance { get; set; }
        public decimal? LedgerClosingBalance { get; set; }

        public string ReferenceGroupNo { get; set; }
        public string ReferenceGroupName { get; set; }

    }
    public class BankReconciliationMatchingLedgerTransViewModel
    {
        public long? AccountID { get; set; }
        public string AccountName { get; set; }
        public decimal? LedgerDebitAmount { get; set; }
        public string Narration { get; set; }
        public decimal? LedgerCreditAmount { get; set; }
        public string PostDate { get; set; }

        public string ChequeNo { get; set; }

        public string PartyName { get; set; }

        public string Reference { get; set; }

        public string ChequeDate { get; set; }
        public bool IsMoved { get; set; } = false;
        public bool IsMatching { get; set; } = false;
        public bool IsSelected { get; set; } = false;
        public long? TranHeadID { get; set; }
        public long? TranTailID { get; set; }

        public decimal? LedgerOpeningBalance { get; set; }
        public decimal? LedgerClosingBalance { get; set; }

        public string ReferenceGroupNo { get; set; }
        public string ReferenceGroupName { get; set; }

    }
    public class BankReconciliationBankTransViewModel
    {
        public long? BankStatementEntryID { get; set; }
        public long? BankStatementID { get; set; }
        public long? AccountID { get; set; }
        public string AccountName { get; set; }
        public string PostDate { get; set; }
        public decimal? BankCreditAmount { get; set; }
        public string Narration { get; set; }
        public decimal? BankDebitAmount { get; set; }
        public decimal? Balance { get; set; }
        public bool IsMoved { get; set; } = false;
        public bool IsMatching { get; set; } = false;
        public bool IsSelected { get; set; } = false;
        public long? TranHeadID { get; set; }
        public long? TranTailID { get; set; }
        public string ChequeNo { get; set; }

        public long? SlNO { get; set; }
        public string PartyName { get; set; }
        public string Reference { get; set; }
        public string ChequeDate { get; set; }
        public string ReferenceGroupNo { get; set; }
        public string ReferenceGroupName { get; set; }

    }
    public class BankReconciliationMatchingBankTransViewModel
    {
        public long? BankStatementEntryID { get; set; }
        public long? BankStatementID { get; set; }
        public long? AccountID { get; set; }
        public string AccountName { get; set; }
        public string PostDate { get; set; }
        public decimal? BankCreditAmount { get; set; }
        public string Narration { get; set; }
        public decimal? BankDebitAmount { get; set; }
        public decimal? Balance { get; set; }
        public bool IsMoved { get; set; } = false;
        public bool IsMatching { get; set; } = false;
        public bool IsSelected { get; set; } = false;
        public long? TranHeadID { get; set; }
        public long? TranTailID { get; set; }
        public string ChequeNo { get; set; }

        public long? SlNO { get; set; }
        public string PartyName { get; set; }
        public string Reference { get; set; }
        public string ChequeDate { get; set; }
        public string ReferenceGroupNo { get; set; }
        public string ReferenceGroupName { get; set; }

    }
}
