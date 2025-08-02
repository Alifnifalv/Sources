using Eduegate.Domain.Entity.Accounts;
using Eduegate.Domain.Entity.Budgeting;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Accounts.Accounting;
using Eduegate.Services.Contracts.Accounts.MonthlyClosing;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.SmartView;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Eduegate.Domain.Mappers.Accounts
{
    public class FinancialYearClosingMapper : DTOEntityDynamicMapper
    {
        public static FinancialYearClosingMapper Mapper(CallContext context)
        {
            var mapper = new FinancialYearClosingMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FeeGeneralMonthlyClosingDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public List<FiscalYearDTO> GetFiscalYearDetails()
        {
            using (var dbContext = new dbEduegateBudgetingContext())
            {
                var fiscalYears = new List<FiscalYearDTO>();

                var fiscalYearEntity = dbContext.FiscalYears
                    .Include(i => i.AuditTypes)
                    .OrderByDescending(i => i.FiscalYear_ID)
                    .Take(2).ToList();

                int index = 0;

                foreach (var fiscalYear in fiscalYearEntity)
                {
                    fiscalYears.Add(new FiscalYearDTO()
                    {
                        FiscalYear_ID = fiscalYear.FiscalYear_ID,
                        FiscalYear_Name = fiscalYear.FiscalYear_Name,
                        StartDate = fiscalYear.StartDate,
                        EndDate = fiscalYear.EndDate,
                        FiscalYear_Status = fiscalYear.FiscalYear_Status,
                        StatusName = fiscalYear.FiscalYear_Status == 0 ? "Completed" : "Pending",
                        AuditTypeName = fiscalYear.AuditTypes?.Description,
                        OrderNo = index++,
                    });
                }

                return fiscalYears;
            }
        }

        //public ProductHierarchy GetFinancialAuditTree(long? parentID)
        //{
        //    var productCat = new ProductHierarchy();
        //    string connectionString = Environment.GetEnvironmentVariable("dbEduegateERPContext");

        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        // Updated SQL query to filter by parentID if provided
        //        var command = new SqlCommand(@"
        //    SELECT 
        //        GroupID,
        //        GroupName, 
        //        AccountName, 
        //        Lvl_Sort
        //    FROM 
        //        [account].[VWS_CHART_OF_ACCOUNTS]
        //    WHERE 
        //        (Parent_ID = @ParentID)
        //    ORDER BY 
        //        Lvl_Sort", connection);

        //        // Adding parameter to avoid SQL injection
        //        command.Parameters.AddWithValue("@ParentID", (object)parentID ?? 0);

        //        var reader = command.ExecuteReader();

        //        // Reading SQL data and populating the product hierarchy
        //        while (reader.Read())
        //        {
        //            var groupID = reader.GetInt64(0);
        //            var groupName = reader.GetString(1); // GroupName (with level space indentation)
        //            var accountName = reader.GetString(2); // AccountName

        //            // Create the category node and add to the tree
        //            var categoryNode = new CategoryTree
        //            {
        //                CategoryID = groupID,
        //                CategoryName = groupName, // Assuming GroupName is the category name
        //                CategoryCode = accountName // Assuming AccountName is the category code
        //            };

        //            productCat.CategoryTree.Add(categoryNode);
        //        }
        //    }

        //    return productCat;
        //}

        public ProductHierarchy GetFinancialAuditTree(long? parentID, SmartViewType viewType, string searchText)
        {
            var firstTree = new List<FinancialYearClosingProcDTO>();
            var grpdTree = new ProductHierarchy();

            string connectionString = Environment.GetEnvironmentVariable("dbEduegateERPContext");

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            int compID = int.Parse(searchText);
            var fiscalYears = GetFiscalYearDetails();
            var datas = new FiscalYearDTO();
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;
            //int compID = 0;
            int fiscalYearID = 0;
            int fType = 0;
            int isShowBalance = 0;

            if (viewType == SmartViewType.CrntFinancialAudit)
            {
                datas = fiscalYears[0];

                fiscalYearID = datas.FiscalYear_ID;
                //compID = datas.Company_ID;
                startDate = datas.StartDate ?? DateTime.MinValue;
                endDate = datas.EndDate ?? DateTime.MinValue;
                fType = 0;
                isShowBalance = 1;
            }
            else if (viewType == SmartViewType.PrvFinancialAudit)
            {
                datas = fiscalYears[1];

                fiscalYearID = datas.FiscalYear_ID;
                //compID = datas.Company_ID;
                startDate = datas.StartDate ?? DateTime.MinValue;
                endDate = datas.EndDate ?? DateTime.MinValue;
                fType = 0;
                isShowBalance = 1;

            }

            using (var connection = new SqlConnection(connectionString))
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
                    using (SqlCommand sqlCommand = new SqlCommand("[account].[SPS_FINAL_STATEMENTS]", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@SDATE"].Value = startDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@EDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@EDATE"].Value = endDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@COMP_ID", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@COMP_ID"].Value = compID;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@FISCALYEAR_ID", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@FISCALYEAR_ID"].Value = fiscalYearID;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@FTYPE", SqlDbType.Int));
                        adapter.SelectCommand.Parameters["@FTYPE"].Value = fType;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@TYPE", SqlDbType.Int));
                        adapter.SelectCommand.Parameters["@TYPE"].Value = 0;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@IS_NON_CUMULATIVE", SqlDbType.Int));
                        adapter.SelectCommand.Parameters["@IS_NON_CUMULATIVE"].Value = 0;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@IS_SHOW_LEDGER", SqlDbType.Int));
                        adapter.SelectCommand.Parameters["@IS_SHOW_LEDGER"].Value = 1;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SHOW_BALANCE", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@SHOW_BALANCE"].Value = isShowBalance;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@IS_CONSOLIDATED", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@IS_CONSOLIDATED"].Value = 0;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@IS_SEP_DR_CR", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@IS_SEP_DR_CR"].Value = 0;

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
                            foreach (DataRow row in dataTable.Rows)
                            {
                                var mainGroupID = (int)row["Main_Group_ID"];
                                var mainGroupName = (string)row["Main_GroupName"];
                                var subGroupID = (int)row["Sub_Group_ID"];
                                var subGroupName = (string)row["Sub_GroupName"];
                                var groupID = (int)row["GroupID"];
                                var groupName = (string)row["GroupName"];
                                var accountID = (int)row["AccountID"];
                                var accountName = (string)row["AccountName"];
                                var amount = (decimal)row["Amount"];
                                var credit = (decimal)row["Credit"];

                                firstTree.Add(new FinancialYearClosingProcDTO()
                                {
                                    Main_Group_ID = mainGroupID,
                                    Main_GroupName = mainGroupName,
                                    Sub_Group_ID = subGroupID,
                                    Sub_GroupName = subGroupName,
                                    Amount = amount,
                                    Group_ID = groupID,
                                    GroupName = groupName,
                                    AccountID = accountID,
                                    AccountName = accountName,
                                    //Ledger = credit,
                                });
                            }
                        }

                        var grp = new List<IGrouping<int, FinancialYearClosingProcDTO>>();

                        if (parentID != null)
                        {
                            switch (parentID)
                            {
                                case var _ when firstTree.Any(a => a.Main_Group_ID == parentID):
                                    grp = firstTree
                                        .Where(a => a.Main_Group_ID == parentID)
                                        .GroupBy(c => c.Sub_Group_ID)
                                        .ToList();

                                    foreach (var row in grp)
                                    {
                                        grpdTree.CategoryTree.Add(new CategoryTree()
                                        {
                                            CategoryID = row.FirstOrDefault().Sub_Group_ID,
                                            CategoryName = row.FirstOrDefault()?.Sub_GroupName,
                                            Amount = row.Sum(s => s.Amount),
                                            Ledger = null,
                                        });
                                    }

                                    break;

                                case var _ when firstTree.Any(a => a.Sub_Group_ID == parentID):
                                    grp = firstTree
                                        .Where(a => a.Sub_Group_ID == parentID)
                                        .GroupBy(c => c.Group_ID)
                                        .ToList();

                                    foreach (var row in grp)
                                    {
                                        grpdTree.CategoryTree.Add(new CategoryTree()
                                        {
                                            CategoryID = row.FirstOrDefault().Group_ID,
                                            CategoryName = row.FirstOrDefault()?.GroupName,
                                            Amount = row.Sum(s => s.Amount),
                                            Ledger = null,
                                        });
                                    }

                                    break;

                                case var _ when firstTree.Any(a => a.Group_ID == parentID):
                                    grp = firstTree
                                        .Where(a => a.Group_ID == parentID)
                                        .GroupBy(c => c.AccountID)
                                        .ToList();

                                    foreach (var row in grp)
                                    {
                                        grpdTree.CategoryTree.Add(new CategoryTree()
                                        {
                                            CategoryID = row.FirstOrDefault().AccountID + 10000,
                                            CategoryName = row.FirstOrDefault()?.AccountName,
                                            Amount = null,
                                            Ledger = row.Sum(s => s.Amount),

                                        });
                                    }

                                    break;
                            }
                        }
                        else
                        {
                            grp = firstTree.GroupBy(c => c.Main_Group_ID).ToList();

                            foreach (var row in grp)
                            {
                                grpdTree.CategoryTree.Add(new CategoryTree()
                                {
                                    CategoryID = row.FirstOrDefault().Main_Group_ID,
                                    CategoryName = row.FirstOrDefault()?.Main_GroupName,
                                    Amount = row.Sum(s => s.Amount),
                                });
                            }
                        }

                        return grpdTree;
                    }
                }

            }

            //return productCat;
        }

        public SmartTreeViewDTO GetProductTree(SmartViewType type, long? parentID, string searchText)
        {
            var smartView = new SmartTreeViewDTO()
            {
                //Node = new TreeNodeDTO() { Caption = "Product View", ID = -1, SmartTreeNodeType = Services.Contracts.Enums.SmartTreeNodeType.Root },
                //SmartViewType = Services.Contracts.Enums.SmartViewType.Product
            };
            //FromProductEntity(new ProductDetailRepository().GetFinancialAuditTree(parentID).CategoryTree, smartView.Node);
            return smartView;
        }

        public OperationResultDTO SaveFYCEntries(int companyID, int prvFCY, int crntFCY)
        {
            var result = new OperationResultDTO();

            string connectionString = Environment.GetEnvironmentVariable("dbEduegateERPContext");

            using (var connection = new SqlConnection(connectionString))
            {
                string message = string.Empty;
                SqlConnectionStringBuilder _sBuilder = new SqlConnectionStringBuilder(Infrastructure.ConfigHelper.GetDefaultConnectionString());
                _sBuilder.ConnectTimeout = 30; // Set Timedout
                using (SqlConnection conn = new SqlConnection(_sBuilder.ConnectionString))
                {
                    try { conn.Open(); }
                    catch (Exception ex)
                    {
                        return result = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Error,
                            Message = ex.Message
                        };
                    }
                    using (SqlCommand sqlCommand = new SqlCommand("[account].[SPS_SAVE_FINANCIALYEAR_CLOSING]", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@COMPANYID", SqlDbType.Int));
                        adapter.SelectCommand.Parameters["@COMPANYID"].Value = companyID;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@PRV_FISCALYEAR_ID", SqlDbType.Int));
                        adapter.SelectCommand.Parameters["@PRV_FISCALYEAR_ID"].Value = prvFCY;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@CUR_FISCALYEAR_ID", SqlDbType.Int));
                        adapter.SelectCommand.Parameters["@CUR_FISCALYEAR_ID"].Value = crntFCY;

                        try
                        {
                            // Run the stored procedure.
                            result = new OperationResultDTO()
                            {
                                operationResult = OperationResult.Success,
                                Message = Convert.ToString(sqlCommand.ExecuteScalar() ?? "Successfully saved!")
                            };

                        }
                        catch (Exception ex)
                        {
                            result = new OperationResultDTO()
                            {
                                operationResult = OperationResult.Error,
                                Message = ex.Message
                            };
                        }
                        finally
                        {
                            conn.Close();
                        }

                    }
                }

            }

            return result;
            //return productCat;
        }


        public AuditDataDTO DownloadDatas(AuditDataDTO dto)
        {
            using (var dbContext = new dbEduegateAccountsContext())
            {
                var result = new AuditDataDTO();

                byte[] fileContent = null;

                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                var auditData = dbContext.AuditDatas.FirstOrDefault(a => a.AuditDataID == long.Parse(dto.AuditDataID));

                string connectionString = Environment.GetEnvironmentVariable("dbEduegateERPContext");

                using (var connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                    }
                    catch (Exception ex)
                    {
                       
                    }

                    using (SqlCommand sqlCommand = new SqlCommand(auditData.ProcedureName, connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.Add(new SqlParameter("@SDATE", SqlDbType.DateTime)).Value = DateTime.ParseExact(dto.StartDate, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
                        sqlCommand.Parameters.Add(new SqlParameter("@EDATE", SqlDbType.DateTime)).Value = DateTime.ParseExact(dto.EndDate, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
                        sqlCommand.Parameters.Add(new SqlParameter("@FISCALYEAR_ID", SqlDbType.Int)).Value = dto.FiscalYear_ID;
                        sqlCommand.Parameters.Add(new SqlParameter("@COMP_ID", SqlDbType.VarChar)).Value = dto.Companies;
                        sqlCommand.Parameters.Add(new SqlParameter("@FTYPE", SqlDbType.Int)).Value = auditData.FType;
                        sqlCommand.Parameters.Add(new SqlParameter("@IS_CONSOLIDATED", SqlDbType.Bit)).Value = dto.IsConsolidated;

                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);

                        if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                        {
                            DataTable dataTable = dataSet.Tables[0];

                            var fileName = auditData.Description.Replace(' ', '_') + "_" + DateTime.Now.ToString("yyyyMMdd");

                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                            using (ExcelPackage package = new ExcelPackage())
                            {
                                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("AuditData");
                                worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);

                                fileContent = package.GetAsByteArray();
                            }

                            result = new AuditDataDTO()
                            {
                                ContentBytes = fileContent,
                                FileName = fileName + ".xlsx",
                            };
                        }
                    }
                }

                return result;
            }

        }

        public FiscalYearDTO GetFiscalYearByFiscalYear(int fiscalYearID)
        {
            using (var dbContext = new dbEduegateBudgetingContext())
            {
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                var fiscalYears = new FiscalYearDTO();

                var fiscalYearEntity = dbContext.FiscalYears.Where(x => x.FiscalYear_ID == fiscalYearID).Include(a => a.AuditTypes).FirstOrDefault();

                fiscalYears = new FiscalYearDTO()
                {
                    FiscalYear_ID = fiscalYearEntity.FiscalYear_ID,
                    FiscalYear_Name = fiscalYearEntity.FiscalYear_Name,
                    StartDateString = Convert.ToDateTime(fiscalYearEntity.StartDate).ToString(dateFormat, CultureInfo.InvariantCulture),
                    EndDateString = Convert.ToDateTime(fiscalYearEntity.EndDate).ToString(dateFormat, CultureInfo.InvariantCulture),
                    FiscalYear_Status = fiscalYearEntity.FiscalYear_Status,
                    StatusName = fiscalYearEntity.FiscalYear_Status == 0 ? "Completed" : "Pending",
                    AuditTypeName = fiscalYearEntity.AuditTypes.Description,
                };

                return fiscalYears;
            }
        }

    }
}