using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Models.Inventory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Repository.Inventory
{
    public class TaxRepository
    {
        public List<TaxTemplate> GetBranchWiseInventory(long documentTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (
                    from taxTemplate in dbContext.TaxTemplates
                    join doc in dbContext.DocumentTypes on taxTemplate.TaxTemplateID equals doc.TaxTemplateID
                    where doc.DocumentTypeID == documentTypeID
                    select taxTemplate
                    )
                    .AsNoTracking()
                    .ToList();
            }
        }
    }
}
