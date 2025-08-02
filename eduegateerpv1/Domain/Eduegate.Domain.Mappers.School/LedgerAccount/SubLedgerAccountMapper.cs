using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.LedgerAccount;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.LedgerAccount
{
    public class SubLedgerAccountMapper : DTOEntityDynamicMapper
    {
        public static SubLedgerAccountMapper Mapper(CallContext context)
        {
            var mapper = new SubLedgerAccountMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SubLedgerAccountDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private SubLedgerAccountDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Accounts_SubLedger.Where(x => x.SL_AccountID == IID)
                    .Include(x => x.Accounts_SubLedger_Relation)
                    .AsNoTracking()
                    .FirstOrDefault();

                List<KeyValueDTO> mapDto = new List<KeyValueDTO>();
                foreach (var stp in entity.Accounts_SubLedger_Relation)
                {
                    //var subledger = stp.Accounts_SubLedger != null ? stp.Accounts_SubLedger : dbContext.Accounts_SubLedger.AsNoTracking().FirstOrDefault(x => x.SL_AccountID == stp.AccountID);
                    mapDto.Add(new KeyValueDTO()
                    {
                        Key = stp.AccountID.ToString(),
                        Value = dbContext.Accounts.Where(x => x.AccountID == stp.AccountID).Select(y => y.AccountName).FirstOrDefault()
                        //Value = subledger.SL_AccountName
                    });
                }

                return new SubLedgerAccountDTO()
                {
                    SL_AccountID = entity.SL_AccountID,
                    SL_AccountCode = entity.SL_AccountCode,
                    SL_AccountName = entity.SL_AccountName,
                    SL_Alias = entity.SL_Alias,
                    IsHidden = entity.IsHidden,
                    Accounts = mapDto,
                    AllowUserDelete = entity.AllowUserDelete,
                    AllowUserEdit = entity.AllowUserEdit,
                    AllowUserRename = entity.AllowUserRename,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                };
            }
        }


        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SubLedgerAccountDTO;
            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new Accounts_SubLedger()
                {
                    SL_AccountID = toDto.SL_AccountID,
                    SL_AccountCode = toDto.SL_AccountCode,
                    SL_AccountName = toDto.SL_AccountName,
                    SL_Alias = toDto.SL_Alias,
                    IsHidden = toDto.IsHidden,
                    AllowUserDelete = toDto.AllowUserDelete,
                    AllowUserEdit = toDto.AllowUserEdit,
                    AllowUserRename = toDto.AllowUserRename,
                    CreatedBy = toDto.SL_AccountID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.SL_AccountID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.SL_AccountID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.SL_AccountID > 0 ? DateTime.Now : dto.UpdatedDate,
                };

                //delete all mapping and recreate
                var oldDatas = dbContext.Accounts_SubLedger_Relation.Where(x => x.SL_AccountID == toDto.SL_AccountID).AsNoTracking().ToList();
                if (oldDatas.Count > 0)
                {
                    dbContext.Accounts_SubLedger_Relation.RemoveRange(oldDatas);
                    dbContext.SaveChanges();
                }

                Accounts_SubLedger subjectMap = null;
                if (toDto.Accounts.Count > 0)
                {
                    foreach (KeyValueDTO keyval in toDto.Accounts)
                    {
                        entity.Accounts_SubLedger_Relation.Add(new Accounts_SubLedger_Relation()
                        {
                            AccountID = long.Parse(keyval.Key),
                            SL_AccountID = toDto.SL_AccountID,
                        });
                        //subjectMap = entity;
                    }
                }
                subjectMap = entity;

                dbContext.Accounts_SubLedger.Add(entity);
                if (entity.SL_AccountID == 0)
                {
                   
                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    foreach (var map in entity.Accounts_SubLedger_Relation)
                    {
                        if (map.SL_Rln_ID != 0)
                        {
                            dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                    }

                    
                    dbContext.Entry(entity).State = EntityState.Modified;
                }
                dbContext.SaveChanges();

                return GetEntity(subjectMap.SL_AccountID);
            }
        }

        public List<KeyValueDTO> GetSubLedgerByAccount(long accountID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var costCenters = (from sba in dbContext.Accounts_SubLedger_Relation
                                   join sb in dbContext.Accounts_SubLedger on sba.SL_AccountID equals sb.SL_AccountID
                                   where sba.AccountID == accountID
                                   select new KeyValueDTO
                                   {
                                       Key = sb.SL_AccountID.ToString(),
                                       Value = sb.SL_AccountName
                                   }).AsNoTracking().ToList();

                return costCenters;
            }
        }

    }
}