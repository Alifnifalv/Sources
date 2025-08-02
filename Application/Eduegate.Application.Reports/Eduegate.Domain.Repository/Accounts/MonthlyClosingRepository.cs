using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using System.Data.Entity;
using Eduegate.Domain.Entity.Models.ValueObjects;
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
