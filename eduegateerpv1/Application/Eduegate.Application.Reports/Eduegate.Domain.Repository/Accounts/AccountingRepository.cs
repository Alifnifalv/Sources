using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using System.Data.Entity;
using Eduegate.Domain.Entity.Models.ValueObjects;
using System.Linq;
namespace Eduegate.Domain.Repository.Accounts
{
    public class AccountingRepository
    {
        public List<ChartRowType> GetChartRowTypes()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.ChartRowTypes.OrderBy(a=> a.Name).ToList();
            }
        }

        public List<AccountBehavoir> GetAccountBehavior()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.AccountBehavoirs.OrderBy(a=> a.Description).ToList();
            }
        }

        public Account AddOrUpdateSubAccount(Account entity, long? customerID = null, long? supplierID = null)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {                   
                    var account = dbContext.Accounts.Where(x => x.AccountID == entity.AccountID).FirstOrDefault();

                    if (account != null)
                    {
                        account.AccountName = entity.AccountName;
                        dbContext.Entry(account).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        dbContext.Accounts.Add(entity);
                        var lastAlias = dbContext.Accounts.Where(x => x.ParentAccountID == entity.ParentAccountID).Max(a => a.Alias);
                        var parentAccount = dbContext.Accounts.Where(x => x.AccountID == entity.ParentAccountID).FirstOrDefault();

                        if (string.IsNullOrEmpty(lastAlias))
                        {
                            lastAlias = parentAccount==null?"0":parentAccount.Alias;
                        }
                        if (parentAccount == null)
                        {
                            string message = "Account ID " + entity.ParentAccountID + " not found!";
                            throw new Exception(message);
                        }

                        var nextAlais = decimal.Parse(lastAlias) +  1;
                        entity.Alias = nextAlais.ToString();
                        entity.GroupID = parentAccount.GroupID;
                        entity.AccountBehavoirID = parentAccount.AccountBehavoirID;

                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                    }

                    if (customerID.HasValue)
                    {
                        var map = dbContext.CustomerAccountMaps.Where(x => x.AccountID == entity.AccountID && x.CustomerID == customerID).FirstOrDefault();

                        if (map == null)
                        {
                            entity.CustomerAccountMaps = new List<CustomerAccountMap>();
                            entity.CustomerAccountMaps.Add(new CustomerAccountMap()
                            {
                                CustomerID = customerID.Value,
                                AccountID = entity.AccountID
                            });
                        }
                    }

                    if (supplierID.HasValue)
                    {
                        var map = dbContext.SupplierAccountMaps.Where(x => x.AccountID == entity.AccountID && x.SupplierID == supplierID).FirstOrDefault();

                        if (map == null)
                        {
                            entity.SupplierAccountMaps = new List<SupplierAccountMap>();
                            entity.SupplierAccountMaps.Add(new SupplierAccountMap()
                            {
                                SupplierID = supplierID.Value,
                                AccountID = entity.AccountID
                            });
                        }
                    }

                    dbContext.SaveChanges();
                    var updatedEntity = dbContext.Accounts.Where(x => x.AccountID == entity.AccountID).FirstOrDefault();
                    return updatedEntity;
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AccountingRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }

        public Account SaveAccount(Account entity)
        {
            Account updatedEntity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.Accounts.Add(entity);

                    if (dbContext.Accounts.Any(x => x.AccountID == entity.AccountID))
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    else
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;

                    dbContext.SaveChanges();
                    updatedEntity = dbContext.Accounts
                        .Include(a=> a.Group)
                        .Include(a => a.Account1)
                        .Where(x => x.AccountID == entity.AccountID).FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AccountingRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return updatedEntity;
        }

        public Account GetAccount(long accountID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var accountEntity = dbContext.Accounts.Where(x => x.AccountID == accountID).FirstOrDefault();

                if (accountEntity != null)
                {
                    dbContext.Entry(accountEntity).Reference(a => a.Group).Load();
                    dbContext.Entry(accountEntity).Reference(a => a.Account1).Load();
                }

                return accountEntity;
            }
        }

        public List<Account> GetAccounts(string searchText)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Accounts.Where(a=> a.AccountName.Contains(searchText) 
                        || a.AccountCode.Contains(searchText) || a.Alias.Contains(searchText))
                    .Take(Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue<int>("MaxFetchCount"))
                    .OrderBy(a=> a.AccountName).ToList();
            }
        }

        public List<Account> GetAccounts()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Accounts
                    .Include(a=> a.Group).ToList();
            }
        }


        public List<Account> GetSubAccounts(long parentAccountID, string searchText = "")
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (!string.IsNullOrEmpty(searchText))
                {
                    return dbContext.Accounts
                        .Include(a => a.Group).Where(a => a.ParentAccountID == parentAccountID && (a.AccountName.Contains(searchText) || a.Alias.Contains(searchText)))
                        .OrderBy(a => a.AccountName)
                        .Take(Framework.Extensions.ConfigurationExtensions.GetAppConfigValue<int>("MaxFetchCount"))
                        .ToList();
                }
                else
                {
                    return dbContext.Accounts
                        .Include(a => a.Group).Where(a => a.ParentAccountID == parentAccountID)
                        .OrderBy(a => a.AccountName)
                        .Take(Framework.Extensions.ConfigurationExtensions.GetAppConfigValue<int>("MaxFetchCount"))
                        .ToList();
                }
            }
        }

        public List<AccountTransactionAmountDetail> GetAllAccountTransactionAmount()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Database.SqlQuery<AccountTransactionAmountDetail>("select * from [account].[AllAccountTransactions]").ToList();
            }
        }

        public List<Accounts_Chart> GetAccounts_Chart()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Database.SqlQuery<Accounts_Chart>(@"SELECT  GL, Lvl AS [Level], GroupID AS Group_ID, Parent_ID, Lvl_Sort AS Level_Sort,  PartCode, Particulars as Particulars
                FROM  account.VWS_CHART_OF_ACCOUNTS ORDER BY Lvl_Sort").ToList();
            }
        }

        public Group SaveAccountGroup(Group entity)
        {
            Group updatedEntity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.Groups.Add(entity);

                    if (dbContext.Groups.Any(x => x.GroupID == entity.GroupID))
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    else
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;

                    dbContext.SaveChanges();
                    updatedEntity = dbContext.Groups.Where(x => x.GroupID == entity.GroupID).FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AccountingRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return updatedEntity;
        }

        public Group GetAccountGroup(long accountGroupID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Groups.Where(x => x.GroupID == accountGroupID).FirstOrDefault();
            }
        }

        public List<Group> GetAccountGroups()
        {
            //using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            //{
            //    return dbContext.Groups.OrderBy(a=> a.GroupName).ToList();
            //}

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Database.SqlQuery<Group>("select * from account.VWS_AccountGroupTree where Lvl>=2 ORDER BY GROUPNAME").ToList();
            }
        }

        public ChartOfAccount GetChartOfAccount(long chartID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var chart = dbContext.ChartOfAccounts.Where(x => x.ChartOfAccountIID == chartID).FirstOrDefault();
                dbContext.Entry(chart).Collection(a => a.ChartOfAccountMaps).Load();
                return chart;
            }
        }

        public decimal GetCustomerArrears(string accountCode)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var arrears = dbContext.AccountTransactions
                    .Where(x => x.Account.AccountCode == accountCode)
                    .Sum(a => a.DebitOrCredit.HasValue ? a.DebitOrCredit.Value ? a.Amount : -1 * a.Amount : 0);
                return arrears.HasValue ? arrears.Value : 0;
            }
        }

        public ChartOfAccount SaveChartOfAccount(ChartOfAccount entity)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.ChartOfAccounts.Add(entity);

                    if (dbContext.ChartOfAccounts.Any(x => x.ChartOfAccountIID == entity.ChartOfAccountIID))
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    else
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;

                    foreach (var map in entity.ChartOfAccountMaps)
                    {
                        if (dbContext.ChartOfAccountMaps.Any(x => x.ChartOfAccountMapIID == map.ChartOfAccountMapIID))
                            dbContext.Entry(map).State = System.Data.Entity.EntityState.Modified;
                        else
                            dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                    }

                    dbContext.SaveChanges();
                    return dbContext.ChartOfAccounts.Where(x => x.ChartOfAccountIID == entity.ChartOfAccountIID).FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AccountingRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }
    }
}
