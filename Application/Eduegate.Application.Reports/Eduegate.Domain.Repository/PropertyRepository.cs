using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        public Property GetPropertyDetail(long propertyID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Properties.Where(x => x.PropertyIID == propertyID).Single();
            }
        }
    }
}
