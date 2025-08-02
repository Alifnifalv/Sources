using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Accounts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Eduegate.Domain.Mappers.School.Accounts
{
    public class AccountEntryMapper : DTOEntityDynamicMapper
    {
        public static AccountEntryMapper Mapper(CallContext context)
        {
            var mapper = new AccountEntryMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AccountsDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private AccountsDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Accounts.Where(X => X.AccountID == IID)
                    .Include(x => x.Group)
                    .Include(x => x.AccountBehavoir)
                    .Include(x => x.Account1)
                    .AsNoTracking()
                    .FirstOrDefault();

                var accountDTO = new AccountsDTO()
                {
                    AccountID = entity.AccountID,
                    AccountName = entity.AccountName,
                    Alias = entity.Alias,
                    AccountCode = entity.AccountCode,
                    AccountBehavoirID = entity.AccountBehavoirID,
                    GroupID = entity.GroupID.HasValue ? entity.GroupID : null,
                    //ParentAccount = entity.ParentAccountID.HasValue ? new AccountDTO() { AccountID = entity.ParentAccountID.Value, AccountName = entity.Account1.AccountName } : null,
                    AccountGroup = entity.GroupID.HasValue ? new KeyValueDTO() { Key = entity.Group.GroupID.ToString(), Value = entity.Group.GroupName } : null,
                    //AccountBehavior = entity.AccountBehavoirID.HasValue ? (Eduegate.Services.Contracts.Enums.Accounting.AccountBehavior)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.Accounting.AccountBehavior), entity.AccountBehavoirID.ToString()) : Services.Contracts.Enums.Accounting.AccountBehavior.Both,
                    AccountBehavior = entity.AccountBehavoirID.HasValue ? entity.AccountBehavoir.Description : null,
                    IsEnableSubLedger = entity.IsEnableSubLedger,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                };

                accountDTO.SubGroup = GetParentGroupIDByGroupID(entity.GroupID);
                accountDTO.MainGroup = string.IsNullOrEmpty(accountDTO.SubGroup.Key) ? new KeyValueDTO() : GetParentGroupIDByGroupID(int.Parse(accountDTO.SubGroup.Key));

                return accountDTO;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AccountsDTO;

            if (toDto.GroupID == 0 || toDto.GroupID == null)
            {
                throw new Exception("Select any account group");
            }

            if (toDto.AccountBehavoirID == 0 || toDto.AccountBehavoirID == null)
            {
                throw new Exception("Select any behaviour");
            }

            //convert the dto to entity and pass to the repository.
            var entity = new Account()
            {
                AccountID = toDto.AccountID,
                AccountName = toDto.AccountName,
                Alias = toDto.Alias,
                AccountCode = toDto.AccountCode,
                GroupID = toDto.GroupID,
                //ParentAccountID = dto.ParentAccount == null ? null : (long?)dto.ParentAccount.AccountID,
                AccountBehavoirID = toDto.AccountBehavoirID,
                IsEnableSubLedger = toDto.IsEnableSubLedger,
                CreatedBy = toDto.AccountID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.AccountID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.AccountID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.AccountID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {

                dbContext.Accounts.Add(entity);
                if (entity.AccountID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.AccountID));
        }

        public void AccountTransMergewithMultipleIDs(string referenceIDs, DateTime currentDate, int loginId, int type)
        {
            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetSchoolConnectionString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[account].[SPS_ACCOUNT_TRANS_MERGE]", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(new SqlParameter("@REFERENCE_IDs", SqlDbType.NVarChar));
                    sqlCommand.Parameters["@REFERENCE_IDs"].Value = referenceIDs;

                    sqlCommand.Parameters.Add(new SqlParameter("@TRANDATE", SqlDbType.DateTime));
                    sqlCommand.Parameters["@TRANDATE"].Value = currentDate;

                    sqlCommand.Parameters.Add(new SqlParameter("@LOGINID", SqlDbType.Int));
                    sqlCommand.Parameters["@LOGINID"].Value = loginId;

                    sqlCommand.Parameters.Add(new SqlParameter("@TYPE", SqlDbType.Int));
                    sqlCommand.Parameters["@TYPE"].Value = type;

                    try
                    {
                        conn.Open();

                        // Run the stored procedure.
                        sqlCommand.ExecuteNonQuery();


                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Something Wrong! Please check after sometime");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

        }
        public void AccountTransMerge(long referenceID,DateTime currentDate, int loginId, int type)
        {


            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetSchoolConnectionString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[account].[SPS_ACCOUNT_TRANS_MERGE]", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(new SqlParameter("@REFERENCE_IDs", SqlDbType.BigInt));
                    sqlCommand.Parameters["@REFERENCE_IDs"].Value = referenceID;

                    sqlCommand.Parameters.Add(new SqlParameter("@TRANDATE", SqlDbType.DateTime));
                    sqlCommand.Parameters["@TRANDATE"].Value = currentDate;

                    sqlCommand.Parameters.Add(new SqlParameter("@LOGINID", SqlDbType.Int));
                    sqlCommand.Parameters["@LOGINID"].Value = loginId;

                    sqlCommand.Parameters.Add(new SqlParameter("@TYPE", SqlDbType.Int));
                    sqlCommand.Parameters["@TYPE"].Value = type;

                    try
                    {
                        conn.Open();

                        // Run the stored procedure.
                        sqlCommand.ExecuteNonQuery();


                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Something Wrong! Please check after sometime");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

        }

        public void AccountTransactionSync(long accountTransactionHeadIID,long referenceID,int loginId,int type)
        {
           

            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetSchoolConnectionString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[account].[SPS_ACCOUNT_TRANSACTION_SYNC]", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(new SqlParameter("@ACCOUNTTRANHEAD_ID", SqlDbType.BigInt));
                    sqlCommand.Parameters["@ACCOUNTTRANHEAD_ID"].Value = accountTransactionHeadIID;

                    sqlCommand.Parameters.Add(new SqlParameter("@REFERENCE_ID", SqlDbType.BigInt));
                    sqlCommand.Parameters["@REFERENCE_ID"].Value = referenceID;

                    sqlCommand.Parameters.Add(new SqlParameter("@CURRENT_USER_ID", SqlDbType.Int));
                    sqlCommand.Parameters["@CURRENT_USER_ID"].Value =loginId  ;

                    sqlCommand.Parameters.Add(new SqlParameter("@TYPE", SqlDbType.Int));
                    sqlCommand.Parameters["@TYPE"].Value = type;

                    try
                    {
                        conn.Open();

                        // Run the stored procedure.
                        sqlCommand.ExecuteNonQuery();


                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Something Wrong! Please check after sometime");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

        }

        public string GetNextTransactionNumber(int documentTypeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var documentType = dbContext.DocumentTypes.Where(x => x.DocumentTypeID == documentTypeID).AsNoTracking().FirstOrDefault();
                if (documentType != null)
                {
                    documentType.LastTransactionNo = documentType.LastTransactionNo.HasValue ? documentType.LastTransactionNo + 1 : 1;
                    dbContext.SaveChanges();
                    return documentType.TransactionNoPrefix + documentType.LastTransactionNo.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        public AccountsDTO GetAccountCodeByGroup(long groupID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dtos = new AccountsDTO();

                //Get Application details
                var code = dbContext.Accounts.FromSqlRaw($@"SELECT TOP 1 AccountID, Alias, AccountName, ParentAccountID, GroupID, AccountBehavoirID, CreatedBy, UpdatedBy, CreatedDate, UpdatedDate, TimeStamps, ChildAliasPrefix, ChildLastID, ExternalReferenceID, 
                    account.FNS_ACCOUNT_CODE({groupID}) AS AccountCode, AccountAddress, TaxRegistrationNum, IsEnableSubLedger FROM account.Accounts")
                    .AsNoTracking().FirstOrDefault();

                if (code != null)
                {
                    dtos = new AccountsDTO()
                    {
                        AccountID = 0,
                        AccountCode = code.AccountCode,
                        Alias = String.Empty,
                    };
                }

                return dtos;
            }
        }

        public List<KeyValueDTO> GetSubGroup(int mainGroupID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var subGroupList = new List<KeyValueDTO>();

                var entities = dbContext.Groups.FromSqlRaw($@"SELECT * FROM account.Groups WHERE GroupID IN(SELECT DISTINCT Sub_Group_ID FROM account.VWS_AccountGroupTree WHERE Main_Group_ID={mainGroupID} AND Sub_Group_ID!={ mainGroupID})")
                    .AsNoTracking().ToList();

                foreach (var subGroup in entities)
                {
                    subGroupList.Add(new KeyValueDTO
                    {
                        Key = subGroup.GroupID.ToString(),
                        Value = subGroup.GroupName
                    });
                }

                return subGroupList;
            }
        }

        public List<KeyValueDTO> GetAccountGroup(int subGroupID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var accountGroupList = new List<KeyValueDTO>();

                var entities = dbContext.Groups.FromSqlRaw($@"SELECT * FROM account.Groups WHERE GroupID IN(SELECT DISTINCT GroupID FROM account.VWS_AccountGroupTree WHERE Sub_Group_ID={subGroupID} AND GroupID!={subGroupID})")
                    .AsNoTracking().ToList();

                foreach (var accountGroup in entities)
                {
                    accountGroupList.Add(new KeyValueDTO
                    {
                        Key = accountGroup.GroupID.ToString(),
                        Value = accountGroup.GroupName
                    });
                }

                return accountGroupList;
            }
        }

        public List<KeyValueDTO> GetAccountByGroupID(int groupID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var accountList = new List<KeyValueDTO>();

                var entities = dbContext.Accounts.Where(X => X.GroupID == groupID)
                    .Include(x => x.Group)
                    .Include(x => x.AccountBehavoir)
                    .AsNoTracking()
                    .ToList();

                foreach (var account in entities)
                {
                    accountList.Add(new KeyValueDTO
                    {
                        Key = account.AccountID.ToString(),
                        Value = account.AccountName
                    });
                }

                return accountList;
            }
        }

        public List<KeyValueDTO> GetAccountCodeByLedger(int ledgerGroupID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var accountCodeList = new List<KeyValueDTO>();
                var entities = dbContext.Accounts.Where(X => X.GroupID == ledgerGroupID).AsNoTracking().ToList();

                foreach (var account in entities)
                {
                    accountCodeList.Add(new KeyValueDTO
                    {
                        Key = account.AccountID.ToString(),
                        Value = account.AccountCode
                    });
                }

                return accountCodeList;
            }
        }

        public AccountsGroupDTO GetAccountGroupDataByID(int groupID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var groupData = new AccountsGroupDTO();

                var groupEntity = dbContext.Groups.Where(X => X.GroupID == groupID).AsNoTracking().FirstOrDefault();

                if (groupEntity != null)
                {
                    groupData = new AccountsGroupDTO()
                    {
                        GroupID = groupEntity.GroupID,
                        GroupCode = groupEntity.GroupCode,
                        GroupName = groupEntity.GroupName,
                        Default_Side = groupEntity.Default_Side,
                    };
                }

                return groupData;
            }
        }

        public KeyValueDTO GetParentGroupIDByGroupID(int? groupID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var parentGroupData = new KeyValueDTO();

                var entity = dbContext.Groups.Where(X => X.GroupID == groupID).AsNoTracking().FirstOrDefault();
                var parentGroup = entity != null ? dbContext.Groups.Where(X => X.GroupID == entity.Parent_ID).AsNoTracking().FirstOrDefault() : null;

                if (parentGroup != null)
                {
                    parentGroupData = new KeyValueDTO
                    {
                        Key = parentGroup.GroupID.ToString(),
                        Value = parentGroup.GroupName
                    };
                };

                return parentGroupData;
            }
        }

    }
}
