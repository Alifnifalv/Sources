using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Repository.Accounts;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Accounts.Accounting;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.Domain.Accounts
{
    public class AccountingBL
    {
        private CallContext _callContext;

        public AccountingBL(CallContext context)
        {
            _callContext = context;
        }

        public AccountDTO SaveAccount(AccountDTO dto)
        {
            var mapper = Mappers.Accounts.AccountMapper.Mapper(_callContext);
            return mapper.ToDTO(new AccountingRepository().SaveAccount(mapper.ToEntity(dto)));
        }

        public AccountDTO GetAccount(long accountID)
        {
            return Mappers.Accounts.AccountMapper.Mapper(_callContext).ToDTO(new AccountingRepository().GetAccount(accountID));
        }

        public AccountGroupDTO SaveAccountGroup(AccountGroupDTO dto)
        {
            var mapper = Mappers.Accounts.GroupMapper.Mapper(_callContext);
            return mapper.ToDTO(new AccountingRepository().SaveAccountGroup(mapper.ToEntity(dto)));
        }

        public AccountGroupDTO GetAccountGroup(long accountGroupID)
        {
            return Mappers.Accounts.GroupMapper.Mapper(_callContext).ToDTO(new AccountingRepository().GetAccountGroup(accountGroupID));
        }

        public ChartOfAccountDTO SaveChartOfAccount(ChartOfAccountDTO dto)
        {
            var mapper = Mappers.Accounts.ChartOfAccountMapper.Mapper(_callContext);
            return mapper.ToDTO(new AccountingRepository().SaveChartOfAccount(mapper.ToEntity(dto)));
        }

        public ChartOfAccountDTO GetChartOfAccount(long chartID)
        {
            return Mappers.Accounts.ChartOfAccountMapper.Mapper(_callContext).ToDTO(new AccountingRepository().GetChartOfAccount(chartID));
        }

        

        public List<ChartOfAccountLedgerDetailsDTO> GetLedgerTransactions(int groupID)
        {
            var repository = new AccountingRepository();
            var dtos = new List<ChartOfAccountLedgerDetailsDTO>();
            var data = repository.GetLedgerTransactions(groupID);
            if (data!=null && data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    dtos.Add(new ChartOfAccountLedgerDetailsDTO()
                    {
                        LedgerCode = Convert.ToString(row["AccountCode"]),
                        LedgerName = Convert.ToString(row["AccountName"]),
                        LedgerGroup= Convert.ToString(row["GroupName"]),
                        Company =Convert.ToString(row["CompanyName"]),
                        ClosingBalance = (decimal?)row["ClBalance"],
                        OpeningBalance = (decimal?)row["OpBalance"],
                        Credit = (decimal?)row["Credit"],
                        Debit = (decimal?)row["Debit"],
                        AccountID=Convert.ToInt64(row["AccountID"]),
                        GroupID = Convert.ToInt32(row["GroupID"]),

                    });
                }
            }
            return dtos;
        }

        public List<ChartOfAccountDetailDTO> GetAllChartOfAccount()
        {
            var repository = new AccountingRepository();
            var dtos = new List<ChartOfAccountDetailDTO>();
            var totalAmount = repository.GetAllAccountTransactionAmount();

            //foreach (var account in repository.GetAccounts())
            //{
            //    var accountAmount = totalAmount.Where(a => a.AccountID == account.AccountID).FirstOrDefault();

            //    dtos.Add(new ChartOfAccountDetailDTO()
            //    {
            //        Name = account.AccountName,
            //        AccountCode = account.Alias,
            //        AccountID = account.AccountID,
            //        AccountGroupID = account.Group != null ? account.Group.GroupID : 0,
            //        AccountGroupName = account.Group != null ? account.Group.GroupName : string.Empty,
            //        Balance = accountAmount == null ? 0 : accountAmount.TotalAmount
            //    });
            //}

            foreach (var account in repository.GetAccounts_Chart())
            {
                dtos.Add(new ()
                {
                    Name = account.Group_ID+"-"+ account.Particulars,//.PadLeft((account.Level *5) + account.Particulars.Length  , ' '),
                    AccountCode = account.PartCode,
                    AccountID = account.Group_ID,
                    AccountGroupID = account.Group_ID,
                    AccountGroupName = account.Particulars.PadLeft(account.Level, '-'),
                    Balance = 0,
                    Level=account.Level,
                    ParentID=account.Parent_ID,
                    LevelSort=account.Level_Sort,
                }) ;
            }

            return dtos;
        }
      //  GetAllChartOfAccount(groupID)

        public decimal GetCustomerArrears(string accountCode)
        {
            return new AccountingRepository().GetCustomerArrears(accountCode);
        }
    }
}
