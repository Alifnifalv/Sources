using Eduegate.Domain.Entity.Budgeting;
using Eduegate.Domain.Entity.Budgeting.Models;
using Eduegate.Domain.Entity.Models.Inventory;
using Eduegate.Domain.Mappers.Budgeting;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Budgeting;
using Eduegate.Services.Contracts.School.Fees;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;


namespace Eduegate.Domain.Mappers.Accounts
{
    public class BudgetingMapper : DTOEntityDynamicMapper
    {
        public static BudgetingMapper Mapper(CallContext context)
        {
            var mapper = new BudgetingMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<BudgetEntryDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private BudgetEntryDTO ToDTO(long IID)
        {
            using (dbEduegateBudgetingContext dbContext = new dbEduegateBudgetingContext())
            {
                var entity = dbContext.BudgetEntries
                    .Include(i => i.BudgetEntryAccountMaps).ThenInclude(x => x.Account)
                    .Include(i => i.BudgetEntryAccountMaps).ThenInclude(x => x.Group)
                    .Include(i => i.BudgetEntryAllocations)
                    .Include(i => i.BudgetSuggestion)
                    .AsNoTracking()
                    .FirstOrDefault(a => a.BudgetID == IID);

                return ToDTO(entity);
            }
        }

        private BudgetEntryDTO ToDTO(BudgetEntry entity)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var dto = new BudgetEntryDTO()
            {
                BudgetEntryIID = entity.BudgetEntryIID,
                BudgetID = entity.BudgetID,
                Budget = entity.BudgetID.HasValue ? new KeyValueDTO()
                {
                    Key = entity.Budget.ToString(),
                    Value = entity.Budget.BudgetName
                } : new KeyValueDTO(),
                BudgetSuggestion = entity.BudgetSuggestionID.HasValue ? new KeyValueDTO()
                {
                    Key = entity.BudgetSuggestionID.ToString(),
                    Value = entity.BudgetSuggestion.Description
                } : new KeyValueDTO(),
                BudgetSuggestionID = entity.BudgetSuggestionID,
                SuggestedValue = entity.SuggestedValue,
                Percentage = entity.Percentage,
                EstimateValue = entity.EstimateValue,
                Amount = entity.Amount,
                SuggestedStartDate = entity.SuggestedStartDate,
                SuggestedEndDate = entity.SuggestedEndDate,
                BudgetEntryAccountMapDtos = GetBudgetEntryAccountMapDTO(entity.BudgetEntryAccountMaps.FirstOrDefault()),
                BudgetEntryAllocationDtos = GetBudgetEntryAllocationDTO(entity.BudgetEntryAllocations.ToList()),
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
            };

            return dto;
        }

        public List<BudgetEntryDTO> FillBudgetEntriesByID(int budgetID)
        {
            var entryList = new List<BudgetEntryDTO>();
            var budgetEntryDTO = new BudgetEntryDTO();
            using (var dbContext = new dbEduegateBudgetingContext())
            {
                var dfndbudget = dbContext.BudgetEntries
                    .Where(a => a.BudgetID == budgetID)
                    .Include(i => i.BudgetEntryAccountMaps).ThenInclude(x => x.Account)
                    .Include(i => i.BudgetEntryAccountMaps).ThenInclude(x => x.Group)
                    .Include(i => i.BudgetEntryAllocations)
                    .Include(i => i.BudgetSuggestion)
                    .Include(i => i.Budget)
                    .AsNoTracking()
                    .ToList();

                if (dfndbudget != null)
                {
                    foreach (var budget in dfndbudget)
                    {
                        entryList.Add(ToDTO(budget));
                    }
                }

                return entryList;
            }
        }

        private List<BudgetEntryAccountMapDTO> GetBudgetEntryAccountMapDTO(BudgetEntryAccountMap budgetEntryAccountMaps)
        {
            var lstBudgetEntryAccountMap = new List<BudgetEntryAccountMapDTO>();

            lstBudgetEntryAccountMap.Add(new BudgetEntryAccountMapDTO()
            {
                BudgetEntryAccountMapIID = budgetEntryAccountMaps.BudgetEntryAccountMapIID,
                BudgetEntryID = budgetEntryAccountMaps.BudgetEntryID,
                GroupID = budgetEntryAccountMaps.GroupID,
                AccountID = budgetEntryAccountMaps.AccountID,
                AccountName = budgetEntryAccountMaps.Account.AccountName,
                Group = budgetEntryAccountMaps.GroupID.HasValue ? new KeyValueDTO()
                {
                    Key = budgetEntryAccountMaps.GroupID.ToString(),
                    Value = budgetEntryAccountMaps.Group.GroupName
                } : new KeyValueDTO(),
                Account = budgetEntryAccountMaps.AccountID.HasValue ? new KeyValueDTO()
                {
                    Key = budgetEntryAccountMaps.AccountID.ToString(),
                    Value = budgetEntryAccountMaps.Account.AccountName
                } : new KeyValueDTO(),
                GroupDefaultSide = budgetEntryAccountMaps.Group.Default_Side,
                Remarks = budgetEntryAccountMaps.Remarks
            }); 

            return lstBudgetEntryAccountMap;
        }

        private List<BudgetEntryAllocationDTO> GetBudgetEntryAllocationDTO(List<BudgetEntryAllocation> budgetEntryAllocation)
        {
            var lstBudgetEntryAllocationMap = new List<BudgetEntryAllocationDTO>();

            lstBudgetEntryAllocationMap = (from budgetEntry in budgetEntryAllocation
                                           select new BudgetEntryAllocationDTO
                                           {
                                               BudgetEntryAllocationIID = budgetEntry.BudgetEntryAllocationIID,
                                               BudgetEntryID = budgetEntry.BudgetEntryID,
                                               SuggestedStartDate = budgetEntry.SuggestedStartDate,
                                               SuggestedEndDate = budgetEntry.SuggestedEndDate,
                                               StartDate = budgetEntry.StartDate,
                                               EndDate = budgetEntry.EndDate,
                                               SuggestedValue = budgetEntry.SuggestedValue,
                                               Percentage = budgetEntry.Percentage,
                                               EstimateValue = budgetEntry.EstimateValue,
                                               Amount = budgetEntry.Amount,
                                               Remarks = budgetEntry.Remarks,
                                           }).ToList();

            return lstBudgetEntryAllocationMap;
        }

        public string SaveBudgetEntry(List<BudgetEntryDTO> budgetEntryListDTO)
        {
            using (var dbContext = new dbEduegateBudgetingContext())
            {
                //Map DTO to Entities
                #region Map Entities
                var entities = new List<BudgetEntry>();
                var _sLstBudgetEntryIDs = new List<long>();
                var _sLstBudgetEntryAccountMapIDs = new List<long>();

                entities = (from s in budgetEntryListDTO
                            select new BudgetEntry()
                            {
                                //BudgetEntryIID = s.BudgetEntryIID,
                                BudgetID = s.BudgetID,
                                BudgetSuggestionID = s.BudgetSuggestionID,
                                BudgetEntryAccountMaps = GetBudgetEntryAccountMaps(s.BudgetEntryAccountMapDtos),
                                BudgetEntryAllocations = GetBudgetEntryAllocation(s.BudgetEntryAllocationDtos),
                                SuggestedStartDate = s.SuggestedStartDate,
                                SuggestedEndDate = s.SuggestedEndDate,
                                SuggestedValue = s.SuggestedValue,
                                Percentage = s.Percentage,
                                Amount = s.Amount,
                                EstimateValue = s.EstimateValue,
                                CreatedBy = s.BudgetEntryIID == 0 ? (int)_context.LoginID : s.CreatedBy,
                                UpdatedBy = s.BudgetEntryIID > 0 ? (int)_context.LoginID : s.UpdatedBy,
                                CreatedDate = s.BudgetEntryIID == 0 ? DateTime.Now : s.CreatedDate,
                                UpdatedDate = s.BudgetEntryIID > 0 ? DateTime.Now : s.UpdatedDate,

                            }).ToList();

                #endregion Map Entities

                #region Delete Entities
                List<int?> budgetIDs = entities.Select(x => x.BudgetID).ToList();
                List<int?> groupIDS = entities.SelectMany(x => (x.BudgetEntryAccountMaps.Select(y => y.GroupID))).ToList();
                List<long?> accountIDS = entities.SelectMany(x => (x.BudgetEntryAccountMaps.Select(y => y.AccountID))).ToList();
                var existBudgeting = (from budgetAccMaps in dbContext.BudgetEntryAccountMaps
                                      join budgetEntry in dbContext.BudgetEntries on budgetAccMaps.BudgetEntryID equals budgetEntry.BudgetEntryIID
                                      where budgetIDs.Contains(budgetEntry.BudgetID)
                                            && groupIDS.Contains(budgetAccMaps.GroupID)
                                            && accountIDS.Contains(budgetAccMaps.AccountID)
                                      select new BudgetEntryAccountMapDTO()
                                      {
                                          BudgetEntryID = budgetEntry.BudgetEntryIID,
                                      });

                _sLstBudgetEntryIDs = existBudgeting.Select(x => x.BudgetEntryID.Value).ToList();

                //Delete:- 
                if (_sLstBudgetEntryIDs.Any())
                {

                    var removableBudgetEntryAllocaEntities = dbContext.BudgetEntryAllocations
                                         .Where(x => _sLstBudgetEntryIDs.Contains(x.BudgetEntryID.Value)).AsNoTracking();

                    if (removableBudgetEntryAllocaEntities.Any())
                        dbContext.BudgetEntryAllocations.RemoveRange(removableBudgetEntryAllocaEntities);

                    var removableBudgetEntryAcconuntEntities = dbContext.BudgetEntryAccountMaps
                                       .Where(x => _sLstBudgetEntryIDs.Contains(x.BudgetEntryID.Value)).AsNoTracking();

                    if (removableBudgetEntryAcconuntEntities.Any())
                        dbContext.BudgetEntryAccountMaps.RemoveRange(removableBudgetEntryAcconuntEntities);

                    var removableBudgetEntryEntities = dbContext.BudgetEntries
                                     .Where(x => _sLstBudgetEntryIDs.Contains(x.BudgetEntryIID)).AsNoTracking();

                    if (removableBudgetEntryEntities.Any())
                        dbContext.BudgetEntries.RemoveRange(removableBudgetEntryEntities);

                    dbContext.SaveChanges();

                }

                #endregion Delete Entities

                //Add or Modify entities
                #region Save Entities

                dbContext.BudgetEntries.AddRange(entities);
                dbContext.SaveChanges();


                #endregion Save Entities
                //dbContext.SaveChanges();
            }

            //#endregion DB Updates

            return "Saved successfully";
        }

        private List<BudgetEntryAccountMap> GetBudgetEntryAccountMaps(List<BudgetEntryAccountMapDTO> budgetEntryAccountMaps)
        {
            var lstBudgetEntryAccountMap = new List<BudgetEntryAccountMap>();
            var accountData = budgetEntryAccountMaps.FirstOrDefault();

            lstBudgetEntryAccountMap.Add(new BudgetEntryAccountMap()
            {
                // BudgetEntryAccountMapIID = accountData.BudgetEntryAccountMapIID,
                BudgetEntryID = accountData.BudgetEntryID,
                GroupID = accountData.GroupID,
                AccountID = accountData.AccountID,
                Remarks = accountData.Remarks,
            });

            return lstBudgetEntryAccountMap;
        }

        private List<BudgetEntryAllocation> GetBudgetEntryAllocation(List<BudgetEntryAllocationDTO> budgetEntryAllocationDTO)
        {
            var lstBudgetEntryAllocationMap = new List<BudgetEntryAllocation>();

            lstBudgetEntryAllocationMap = (from budgetEntry in budgetEntryAllocationDTO
                                           select new BudgetEntryAllocation
                                           {
                                               // BudgetEntryAllocationIID = budgetEntry.BudgetEntryAllocationIID,
                                               BudgetEntryID = budgetEntry.BudgetEntryID,
                                               SuggestedStartDate = budgetEntry.SuggestedStartDate,
                                               SuggestedEndDate = budgetEntry.SuggestedEndDate,
                                               StartDate = budgetEntry.StartDate,
                                               EndDate = budgetEntry.EndDate,
                                               SuggestedValue = budgetEntry.SuggestedValue,
                                               Percentage = budgetEntry.Percentage,
                                               EstimateValue = budgetEntry.EstimateValue,
                                               Amount = budgetEntry.Amount,
                                               Remarks = budgetEntry.Remarks,
                                           }
                                         ).ToList();

            return lstBudgetEntryAllocationMap;
        }

        public List<AccountSuggesionMapDTO> GetBudgetSuggestionValue(int? budgetID, byte? suggestionType, List<long?> accountGroupIDs, DateTime? fromDate, DateTime? toDate, List<long?> accountIDs = null)
        {
            var budgetMasterDTO = new BudgetMasterDTO();
            var AccountGrpinfos = new List<Entity.Budgeting.Models.Group>();
            using (dbEduegateBudgetingContext dbContext = new dbEduegateBudgetingContext())
            {
                var entity = dbContext.Budgets1
                    .Where(a => a.BudgetID == budgetID)
                    .Include(i => i.BudgetType)
                    .Include(i => i.BudgetGroup)
                    .AsNoTracking()
                    .FirstOrDefault();

                budgetMasterDTO = new BudgetMasterDTO()
                {
                    BudgetTypeID = entity.BudgetTypeID,
                    CompanyID = entity.CompanyID,
                    //FinancialYearID = entity.FinancialYearID,
                };
                 AccountGrpinfos = dbContext.Groups
                    .Where(a => accountGroupIDs.Contains(a.GroupID))                 
                    .AsNoTracking()
                    .ToList();

            }

            var lstAccountSuggesionMapDTO = new List<AccountSuggesionMapDTO>();

            #region Filteration

            string lstAccountGroupIDs = string.Empty;
            if (accountGroupIDs != null && accountGroupIDs.Any())
                lstAccountGroupIDs = string.Join(",", accountGroupIDs);

            string lstAccountIDs = string.Empty;
            if (accountIDs != null && accountIDs.Any())
                lstAccountIDs = string.Join(",", accountIDs);

            #endregion

            //string dateFromString = "20230101";
            //DateTime dtfrom;

            //if (DateTime.TryParseExact(dateFromString, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtfrom))
            //{
            //    // Parsing successful, dt now holds the parsed DateTime value
            //}
            //else
            //{
            //    // Parsing failed, handle the error accordingly
            //}
            //string dateToString = "20231231";
            //DateTime dtTo;

            //if (DateTime.TryParseExact(dateToString, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtTo))
            //{
            //    // Parsing successful, dt now holds the parsed DateTime value
            //}
            //else
            //{
            //    // Parsing failed, handle the error accordingly
            //}

            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetSchoolConnectionString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("account.SPS_LEDGER_SUMMARY ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@SDATE", SqlDbType.DateTime));
                    adapter.SelectCommand.Parameters["@SDATE"].Value = fromDate;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@EDATE", SqlDbType.DateTime));
                    adapter.SelectCommand.Parameters["@EDATE"].Value = toDate;
                    
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@COMP_ID", SqlDbType.NVarChar));
                    adapter.SelectCommand.Parameters["@COMP_ID"].Value = budgetMasterDTO.CompanyID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@FISCALYEAR_ID", SqlDbType.NVarChar));
                    adapter.SelectCommand.Parameters["@FISCALYEAR_ID"].Value = "";

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@GROUP_IDs", SqlDbType.NVarChar));
                    adapter.SelectCommand.Parameters["@GROUP_IDs"].Value = lstAccountGroupIDs;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ACCOUNT_IDs", SqlDbType.NVarChar));
                    adapter.SelectCommand.Parameters["@ACCOUNT_IDs"].Value = lstAccountIDs;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@TYPE", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@TYPE"].Value = budgetMasterDTO.BudgetTypeID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@DOCUMENT_TYPES", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@DOCUMENT_TYPES"].Value = null;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@IS_CONSOLIDATED", SqlDbType.Bit));
                    adapter.SelectCommand.Parameters["@IS_CONSOLIDATED"].Value = 0;

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

                    if (dataTable != null)
                    {
                        string expression = "Period_ID <>  0 and Period_ID <>  13";// to skip opening balance & closing balance
                        string sortOrder = "Period_ID ASC";
                        DataRow[] foundRows;
                        foundRows = dataTable.Select(expression, sortOrder);

                        foreach (DataRow row in foundRows)
                        {
                            if ((int?)(row["Period_ID"]) != 0 && (int?)(row["Period_ID"]) != 13)
                            {
                                lstAccountSuggesionMapDTO.Add(new AccountSuggesionMapDTO()
                                {
                                    AccountID = (long?)(row["AccountID"]),
                                    GroupID = (int?)(row["GroupID"]),
                                    Group = row["GroupID"] != null ? new KeyValueDTO()
                                    {
                                        Key = row["GroupID"].ToString(),
                                        Value = row["GroupName"].ToString(),
                                    } : new KeyValueDTO(),
                                    Account = row["AccountID"] != null ? new KeyValueDTO()
                                    {
                                        Key = row["AccountID"].ToString(),
                                        Value = row["AccountName"].ToString(),
                                    } : new KeyValueDTO(),
                                    GroupDefaultSide= AccountGrpinfos.FirstOrDefault(x=>x.GroupID== (int?)(row["GroupID"])).Default_Side,
                                    MonthID = (int?)(row["Period_ID"]),                                    
                                    Year = (row["Period_Caption"] != null) ?TryParseYear(row["Period_Caption"].ToString(), fromDate.Value.Year) : fromDate.Value.Year,                                   
                                    Amount = (decimal?)(row["Amount"]),
                                });
                            }
                        }
                    }

                    return lstAccountSuggesionMapDTO;
                }
            }
        }


        private int TryParseYear(string periodCaption, int fromYear)
        {
            if (string.IsNullOrWhiteSpace(periodCaption))
            {
                return fromYear; 
            }

            int year;
            if (periodCaption.Contains(','))
            {
                string[] parts = periodCaption.Split(',');
                if (parts.Length > 1 && int.TryParse(parts[1].Trim(), out year))
                {
                    return year;
                }
            }

            return DateTime.Now.Year;
        }
    }
}
