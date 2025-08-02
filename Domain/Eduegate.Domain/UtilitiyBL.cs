using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.DataAccess.Interfaces;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain
{
    public class UtilitiyBL
    {
        private static IUtilityManagementDA utilityManagement = new UtilityRepository();

        public static string GetCurrencyConfiguration(long ipAddress)
        {
            return utilityManagement.GetCurrencyConfiguration(ipAddress);
        }
    }
}
