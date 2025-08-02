using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Repository.Inventory
{
    public class InventoryRepository
    {
        public List<BranchInventory> GetBranchWiseInventory(long skuID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (
                    from inventory in dbContext.ProductInventories
                    join branch in dbContext.Branches on inventory.BranchID equals branch.BranchIID
                    where inventory.ProductSKUMapID == skuID
                    group new { inventory, branch } by new { inventory.BranchID, inventory.Batch, branch.BranchName} into grp
                    select new BranchInventory()
                    {
                         BranchID = grp.Key.BranchID,
                         Batch = grp.Key.Batch,
                         BranchName = grp.Key.BranchName,
                        Quantity = grp.Sum(a => a.inventory.Quantity)
                    }
                    ).AsNoTracking().ToList();
            }
        }
    }
}
