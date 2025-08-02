using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Repository.Inventory
{
    public class ShareHolderRepository
    {
        public ShareHolder GetShareHolder(string shareHolderNumber)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var shareHolder = dbContext.ShareHolders.Where(a => a.NationalID == shareHolderNumber).FirstOrDefault();
                var customer = dbContext.Customers            
                   .Where(x => x.Telephone == shareHolder.MobileNumber).FirstOrDefault();

                if (customer == null)
                {
                    dbContext.Customers.Add(new Customer()
                    {
                        FirstName = shareHolder.Name,
                        LastName = shareHolder.FamilyName,
                        Telephone = shareHolder.MobileNumber,
                        CustomerEmail = shareHolder.Email,
                        ShareHolderID = shareHolder.ShareHolderID,
                        CustomerAddress = shareHolder.Address,
                        DefaultBranchID = 2,
                        Login = new Login()
                        {
                            LoginEmailID = shareHolder.Email,
                            UserName = shareHolder.Name,
                        },
                        Contacts = new List<Contact>()
                        {
                            new Contact()
                            {
                                 FirstName = shareHolder.Name,
                                 LastName = shareHolder.FamilyName,
                                 TelephoneCode = shareHolder.MobileNumber,
                                 AddressLine1 = shareHolder.Address,
                                 MobileNo1 = shareHolder.MobileNumber,
                            }
                        }
                    });

                    dbContext.SaveChanges();
                }

                return shareHolder;
            }
        }
    }
}
