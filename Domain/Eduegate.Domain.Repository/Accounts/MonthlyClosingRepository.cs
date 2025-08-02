using System;
using System.Linq;
using Eduegate.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Repository.Accounts
{
    public class MonthlyClosingRepository
    {
        public DateTime? GetMonthlyClosingDate(long? branchID)
        {
            var companyID=branchID.HasValue&& branchID!=0? branchID==30?2:1 : 0;            
            if (companyID == 0)
                return null;
            using (var _sContext = new dbEduegateERPContext())
            {
                return _sContext.PeriodClosingTranHeads.Where(x=>x.CompanyID==companyID).Max(x => x.ToDate);
            }

        }
    }
}
