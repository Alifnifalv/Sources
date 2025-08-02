using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Repository
{
    public class MasterRepository
    {
        /// <summary>
        /// Method to get
        /// </summary>
        /// <returns></returns>
        public List<CustomerCategorization> GetCustomerCategorizations()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.CustomerCategorizations.AsNoTracking().ToList<CustomerCategorization>();
            }
        }

    }
}
