using System;
using System.Linq;
using Eduegate.Domain.Entity;

namespace Eduegate.Domain.Repository.Accounts
{
    public class MonthlyClosingRepository
    {
        public DateTime? GetMonthlyClosingDate()
        {

            using (var _sContext = new dbEduegateERPContext())
            {
                return _sContext.PeriodClosingTranHeads.Max(x => x.ToDate);
            }

        }
    }
}
