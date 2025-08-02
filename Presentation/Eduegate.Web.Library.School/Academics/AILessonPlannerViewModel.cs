using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Accounts.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Academics
{
    public class AILessonPlannerViewModel
    {
        public AILessonPlannerViewModel()
        {
            IsEdit = false;

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
      
    }
}
