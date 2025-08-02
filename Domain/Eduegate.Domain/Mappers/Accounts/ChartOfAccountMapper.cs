using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.Domain.Mappers.Accounts
{
    public class ChartOfAccountMapper : IDTOEntityMapper<ChartOfAccountDTO, ChartOfAccount>
    {
        private CallContext _context;

        public static ChartOfAccountMapper Mapper(CallContext context)
        {
            var mapper = new ChartOfAccountMapper();
            mapper._context = context;
            return mapper;
        }

        public ChartOfAccountDTO ToDTO(ChartOfAccount entity)
        {
            var chart = new ChartOfAccountDTO()
            {
                ChartOfAccountIID = entity.ChartOfAccountIID,
                ChartName = entity.ChartName,
                Details = new List<ChartOfAccountDetailDTO>(),
                UpdatedBy = entity.UpdatedBy,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
            };

            foreach (var row in entity.ChartOfAccountMaps)
            {
                chart.Details.Add(new ChartOfAccountDetailDTO()
                {
                    ChartOfAccountMapIID = row.ChartOfAccountMapIID,
                    AccountID = row.AccountID,
                    AccountCode = row.AccountCode,
                    Name = row.Name,
                    ChartRowTypeID = row.ChartRowTypeID,
                    IncomeOrBalanceID = row.IncomeOrBalance,
                    IsNewPage = row.IsNewPage,
                    NoOfBlankLines = row.NoOfBlankLines,
                    UpdatedBy = row.UpdatedBy,
                    CreatedBy = row.CreatedBy,
                    CreatedDate = row.CreatedDate,
                    UpdatedDate = row.UpdatedDate,
                    //TimeStamps = row.TimeStamps == null ? null : Convert.ToBase64String(row.TimeStamps),
                });
            }

            return chart;
        }

        public ChartOfAccount ToEntity(ChartOfAccountDTO dto)
        {
            var chart = new ChartOfAccount()
            {
                ChartOfAccountIID = dto.ChartOfAccountIID,
                ChartName = dto.ChartName,
                ChartOfAccountMaps = new List<ChartOfAccountMap>(),
                UpdatedBy = int.Parse(_context.LoginID.ToString()),
                CreatedBy = dto.ChartOfAccountIID == 0 ? int.Parse(_context.LoginID.ToString()) : dto.CreatedBy,
                //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                CreatedDate = dto.ChartOfAccountIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = DateTime.Now,
            };

            foreach (var row in dto.Details)
            {
                chart.ChartOfAccountMaps.Add(new ChartOfAccountMap()
                {
                    ChartOfAccountMapIID = row.ChartOfAccountMapIID,
                    AccountID = row.AccountID,
                    AccountCode = row.AccountCode,
                    Name = row.Name,
                    ChartRowTypeID = row.ChartRowTypeID,
                    IncomeOrBalance = row.IncomeOrBalanceID,
                    IsNewPage = row.IsNewPage,
                    NoOfBlankLines = row.NoOfBlankLines,
                    UpdatedBy = int.Parse(_context.LoginID.ToString()),
                    CreatedDate = dto.ChartOfAccountIID == 0 ? DateTime.Now : dto.CreatedDate,
                    CreatedBy = dto.ChartOfAccountIID == 0 ? int.Parse(_context.LoginID.ToString()) : dto.CreatedBy,
                    //TimeStamps = row.TimeStamps == null ? null : Convert.FromBase64String(row.TimeStamps),
                    UpdatedDate = DateTime.Now,
                });
            }

            return chart;
        }
    }
}
