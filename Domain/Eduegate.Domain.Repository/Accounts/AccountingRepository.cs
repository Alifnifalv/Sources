using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Eduegate.Domain.Entity.Accounts;
using System.Data.SqlClient;

namespace Eduegate.Domain.Repository.Accounts
{
    public class AccountingRepository
    {
        public List<ChartRowType> GetChartRowTypes()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.ChartRowTypes.OrderBy(a=> a.Name).AsNoTracking().ToList();
            }
        }

        public List<AccountBehavoir> GetAccountBehavior()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.AccountBehavoirs.OrderBy(a=> a.Description).AsNoTracking().ToList();
            }
        }

        public Account AddOrUpdateSubAccount(Account entity, long? customerID = null, long? supplierID = null)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {                   
                    var account = dbContext.Accounts
                        .AsNoTracking()
                        .FirstOrDefault(x => x.AccountID == entity.AccountID);

                    if (account != null)
                    {
                        account.AccountName = entity.AccountName;
                        dbContext.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    else
                    {
                        dbContext.Accounts.Add(entity);
                        var lastAlias = dbContext.Accounts.Where(x => x.ParentAccountID == entity.ParentAccountID).AsNoTracking().Max(a => a.Alias);
                        var parentAccount = dbContext.Accounts
                            .AsNoTracking()
                            .FirstOrDefault(x => x.AccountID == entity.ParentAccountID);

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

                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }

                    if (customerID.HasValue)
                    {
                        var map = dbContext.CustomerAccountMaps
                            .AsNoTracking()
                            .FirstOrDefault(x => x.AccountID == entity.AccountID && x.CustomerID == customerID);

                        if (map == null)
                        {
                            entity.CustomerAccountMaps = new List<CustomerAccountMap>
                            {
                                new CustomerAccountMap()
                                {
                                    CustomerID = customerID.Value,
                                    AccountID = entity.AccountID
                                }
                            };
                        }
                    }

                    dbContext.SaveChanges();

                    if (supplierID.HasValue)
                    {
                        var map = dbContext.SupplierAccountMaps
                            .AsNoTracking()
                            .FirstOrDefault(x => x.AccountID == entity.AccountID && x.SupplierID == supplierID);

                        if (map == null)
                        {
                            entity.SupplierAccountMaps = new List<SupplierAccountMap>
                            {
                                new SupplierAccountMap()
                                {
                                    SupplierID = supplierID.Value,
                                    AccountID = entity.AccountID
                                }
                            };

                            foreach (var accountMap in entity.SupplierAccountMaps)
                            {
                                dbContext.Entry(accountMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
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
                throw;
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
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    else
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                    dbContext.SaveChanges();
                    updatedEntity = dbContext.Accounts
                        .Include(a=> a.Group)
                        .Include(a => a.ParentAccount)
                        .Where(x => x.AccountID == entity.AccountID).AsNoTracking().FirstOrDefault();
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
                var accountEntity = dbContext.Accounts.Where(x => x.AccountID == accountID)
                    .Include(i => i.Group)
                    //.Include(i => i.Accounts1)
                    .AsNoTracking()
                    .FirstOrDefault();

                //if (accountEntity != null)
                //{
                //    dbContext.Entry(accountEntity).Reference(a => a.Group).Load();
                //    dbContext.Entry(accountEntity).Reference(a => a.Account1).Load();
                //}

                return accountEntity;
            }
        }
       
        public DataTable GetLedgerTransactions(int groupID)
        {
            try
            {
                using (var dbContext = new dbEduegateAccountsContext())
                {
                    string message = string.Empty;
                    SqlConnectionStringBuilder _sBuilder = new SqlConnectionStringBuilder(Infrastructure.ConfigHelper.GetDefaultConnectionString());
                    _sBuilder.ConnectTimeout = 30; // Set Timedout
                    using (SqlConnection conn = new SqlConnection(_sBuilder.ConnectionString))
                    {
                        try { conn.Open(); }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        using (SqlCommand sqlCommand = new SqlCommand("account.SPS_GROUP_SUMMARY_CURRENT", conn))
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                            adapter.SelectCommand.Parameters.Add(new SqlParameter("@GROUP_IDs", SqlDbType.BigInt));
                            adapter.SelectCommand.Parameters["@GROUP_IDs"].Value = groupID;

                            DataSet dt = new DataSet();
                            adapter.Fill(dt);
                            DataTable dataTable = null;

                            if (dt.Tables.Count > 0)
                            {
                                if (dt.Tables[0].Rows.Count > 0)
                                {
                                    dataTable = dt.Tables[0];
                                }
                            }


                            return dataTable;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                var exceptionMessage = ex.Message;
                var fields = "GROUP_IDs" + groupID;
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                errorMessage = errorMessage + fields;
                Eduegate.Logger.LogHelper<string>.Fatal($"Exception in GetLedgerTransactions(). Error message: {errorMessage}", ex);
                throw ex;
            }
        }
        public List<Account> GetAccounts(string searchText)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Accounts.Where(a=> a.AccountName.Contains(searchText) 
                        || a.AccountCode.Contains(searchText) || a.Alias.Contains(searchText))
                    .Take(new Domain.Setting.SettingBL(null).GetSettingValue<int>("MaxFetchCount"))
                    .OrderBy(a=> a.AccountName).AsNoTracking().ToList();
            }
        }

        public List<Account> GetAccounts()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Accounts
                    .Include(a=> a.Group)
                    .AsNoTracking()
                    .ToList();
            }
        }


        public List<Account> GetSubAccounts(long parentAccountID, string searchText = "")
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (!string.IsNullOrEmpty(searchText))
                {
                    return dbContext.Accounts
                        .Include(a => a.Group)
                        .Where(a => a.ParentAccountID == parentAccountID && (a.AccountName.Contains(searchText) || a.Alias.Contains(searchText)))
                        .OrderBy(a => a.AccountName)
                        .Take(new Domain.Setting.SettingBL(null).GetSettingValue<int>("MaxFetchCount"))
                        .AsNoTracking()
                        .ToList();
                }
                else
                {
                    return dbContext.Accounts
                        .Include(a => a.Group).Where(a => a.ParentAccountID == parentAccountID)
                        .OrderBy(a => a.AccountName)
                        .Take(new Domain.Setting.SettingBL(null).GetSettingValue<int>("MaxFetchCount"))
                        .ToList();
                }
            }
        }

        public List<AccountTransactionAmountDetail> GetAllAccountTransactionAmount()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.AccountTransactionAmountDetails.FromSqlRaw($@"select * from [account].[AllAccountTransactions]")
                    .AsNoTracking().ToList();
            }
        }

        public List<Accounts_Chart> GetAccounts_Chart()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.AccountCharts.FromSqlRaw($@"SELECT  GL, Lvl AS [Level], GroupID AS Group_ID, Parent_ID, Lvl_Sort AS Level_Sort,  PartCode, Particulars as Particulars
                FROM  account.VWS_CHART_OF_ACCOUNTS  where GL='G' ORDER BY Lvl_Sort")
                    .AsNoTracking().ToList();
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
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    else
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                    dbContext.SaveChanges();
                    updatedEntity = dbContext.Groups.Where(x => x.GroupID == entity.GroupID).AsNoTracking().FirstOrDefault();
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
                return dbContext.Groups.Where(x => x.GroupID == accountGroupID).AsNoTracking().FirstOrDefault();
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
                return dbContext.Groups.FromSqlRaw($@"select * from account.VWS_AccountGroupTree where Lvl>=2 ORDER BY GROUPNAME").AsNoTracking().ToList();
            }
        }

        public ChartOfAccount GetChartOfAccount(long chartID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var chart = dbContext.ChartOfAccounts.Where(x => x.ChartOfAccountIID == chartID)
                    .Include(i => i.ChartOfAccountMaps)
                    .AsNoTracking()
                    .FirstOrDefault();
                //dbContext.Entry(chart).Collection(a => a.ChartOfAccountMaps).Load();
                return chart;
            }
        }

        public decimal GetCustomerArrears(string accountCode)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var arrears = dbContext.AccountTransactions
                    .Where(x => x.Account.AccountCode == accountCode)
                    .AsNoTracking()
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
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    else
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                    foreach (var map in entity.ChartOfAccountMaps)
                    {
                        if (dbContext.ChartOfAccountMaps.Any(x => x.ChartOfAccountMapIID == map.ChartOfAccountMapIID))
                            dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        else
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }

                    dbContext.SaveChanges();
                    return dbContext.ChartOfAccounts.Where(x => x.ChartOfAccountIID == entity.ChartOfAccountIID).AsNoTracking().FirstOrDefault();
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
