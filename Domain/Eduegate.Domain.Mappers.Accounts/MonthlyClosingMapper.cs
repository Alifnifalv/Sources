using Eduegate.Domain.Entity;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Accounts.MonthlyClosing;
using Eduegate.Services.Contracts.School.Academics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Eduegate.Domain.Mappers.Accounts
{
    public class MonthlyClosingMapper : DTOEntityDynamicMapper
    {
        public static MonthlyClosingMapper Mapper(CallContext context)
        {
            var mapper = new MonthlyClosingMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FeeGeneralMonthlyClosingDTO>(entity);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.PeriodClosingTranHeads.Where(X => X.PeriodClosingTranHeadIID == IID)                    
                    .FirstOrDefault();

                var entitydto = new FeeGeneralMonthlyClosingDTO()
                {
                   CompanyID=entity.CompanyID,                    
                   StartDate =entity.FromDate, 
                   EndDate=entity.ToDate,
                };
                return ToDTOString(entitydto);
            }
        }


        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public List<FeeGeneralMonthlyClosingDTO> GetFeeGeneralMonthlyClosing(DateTime? startDate, DateTime? endDate, int? companyID)
        {
            var listData = new List<FeeGeneralMonthlyClosingDTO>();
            using (var dbContext = new dbEduegateERPContext())
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
                    using (SqlCommand sqlCommand = new SqlCommand("[account].[SPS_PC_FEE_GENERAL]", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@SDATE"].Value = startDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@EDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@EDATE"].Value = endDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SCHOOLS_IDs", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@SCHOOLS_IDs"].Value = _context.SchoolID.ToString();

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@COMPANY_IDs", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@COMPANY_IDs"].Value =companyID.HasValue?companyID.ToString(): "1";

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

                                listData.Add(new FeeGeneralMonthlyClosingDTO()
                                {
                                    FeeTypeID = (int)row["FeeTypeID"],
                                    FeeCycleID = (int)row["FeeCycleID"],
                                    FeeMasterID = (int)row["FeeMasterID"],
                                    FeeTypeName = (string)row["FeeTypeName"],
                                    FeeCycleName = (string)row["FeeCycleName"],
                                    FeeMasterName = (string)row["FeeMasterName"],
                                    ClosingDebit = (decimal?)row["Cls_Debit"],
                                    ClosingCredit = (decimal?)row["Cls_Credit"],
                                    ClosingAmount = (decimal?)row["Cls_Amount"],
                                    OpeningDebit = (decimal?)row["Opn_Debit"],
                                    OpeningCredit = (decimal?)row["Opn_Credit"],
                                    OpeningAmount = (decimal?)row["Opn_Amount"],
                                    TransactionDebit = (decimal?)row["Trn_Debit"],
                                    TransactionCredit = (decimal?)row["Trn_Credit"],
                                    TransactionAmount = (decimal?)row["Trn_Amount"]
                                });
                            }
                        }
                        return listData;
                    }
                }

            }

        }
        public List<FeeGeneralMonthlyClosingDTO> GetFeeAccountCompareMonthlyClosing(DateTime? startDate, DateTime? endDate, int? companyID)
        {
            var listData = new List<FeeGeneralMonthlyClosingDTO>();
            using (var dbContext = new dbEduegateERPContext())
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
                    using (SqlCommand sqlCommand = new SqlCommand("account.SPS_PC_FEE_ACC_COMPARE", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@SDATE"].Value = startDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@EDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@EDATE"].Value = endDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SCHOOLS_IDs", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@SCHOOLS_IDs"].Value = _context.SchoolID.ToString();

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@COMPANY_IDs", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@COMPANY_IDs"].Value = companyID.HasValue?companyID.ToString(): "1";

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
                                listData.Add(new FeeGeneralMonthlyClosingDTO()
                                {
                                    FeeTypeID = (int)row["FeeTypeID"],
                                    FeeCycleID = (int)row["FeeCycleID"],
                                    FeeMasterID = (int)row["FeeMasterID"],
                                    FeeTypeName = (string)row["FeeTypeName"],
                                    FeeCycleName = (string)row["FeeCycleName"],
                                    FeeMasterName = (string)row["FeeMasterName"],
                                    FeeAmount = (decimal?)row["Fee_Amount"],
                                    AccountAmount = (decimal?)row["Acc_Amount"]
                                });
                            }
                        }
                        return listData;
                    }
                }

            }
            return listData;
        }

        public List<InvoiceGeneralMonthlyClosingDTO> GetInventoryGeneralMonthlyClosing(DateTime? startDate, DateTime? endDate, int? companyID)
        {
            var listData = new List<InvoiceGeneralMonthlyClosingDTO>();
            using (var dbContext = new dbEduegateERPContext())
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
                    using (SqlCommand sqlCommand = new SqlCommand("account.SPS_PC_INVENTORY_GENERAL", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@SDATE"].Value = startDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@EDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@EDATE"].Value = endDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SCHOOLS_IDs", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@SCHOOLS_IDs"].Value = _context.SchoolID.ToString();

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@COMPANY_IDs", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@COMPANY_IDs"].Value = companyID.HasValue?companyID.ToString(): "1";

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
                                listData.Add(new InvoiceGeneralMonthlyClosingDTO()
                                {
                                    TransactionTypeID = (int)row["TypeID"],
                                    DocumentTypeID = (int)row["DocumentTypeID"],

                                    TransactionTypeName = (string)row["TypeName"],
                                    DocumentTypeName = (string)row["DocumentTypeName"],

                                    ClosingDebit = (decimal?)row["Cls_Debit"],
                                    ClosingCredit = (decimal?)row["Cls_Credit"],
                                    ClosingAmount = (decimal?)row["Cls_Amount"],
                                    OpeningDebit = (decimal?)row["Opn_Debit"],
                                    OpeningCredit = (decimal?)row["Opn_Credit"],
                                    OpeningAmount = (decimal?)row["Opn_Amount"],
                                    TransactionDebit = (decimal?)row["Trn_Debit"],
                                    TransactionCredit = (decimal?)row["Trn_Credit"],
                                    TransactionAmount = (decimal?)row["Trn_Amount"]
                                });
                            }
                        }
                        return listData;
                    }
                }

            }
            return listData;
        }

        public List<FeeGeneralMonthlyClosingDTO> GetFeeCancelMonthlyClosing(DateTime? startDate, DateTime? endDate, int? companyID)
        {
            var listData = new List<FeeGeneralMonthlyClosingDTO>();
            using (var dbContext = new dbEduegateERPContext())
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
                    using (SqlCommand sqlCommand = new SqlCommand("account.SPS_PC_FEE_CANCELLED_GENERAL", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@SDATE"].Value = startDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@EDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@EDATE"].Value = endDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SCHOOLS_IDs", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@SCHOOLS_IDs"].Value = _context.SchoolID.ToString();

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@COMPANY_IDs", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@COMPANY_IDs"].Value = companyID.HasValue?companyID.ToString(): "1";

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
                                listData.Add(new FeeGeneralMonthlyClosingDTO()
                                {
                                    FeeTypeID = (int)row["FeeTypeID"],
                                    FeeCycleID = (int)row["FeeCycleID"],
                                    FeeMasterID = (int)row["FeeMasterID"],
                                    FeeTypeName = (string)row["FeeTypeName"],
                                    FeeCycleName = (string)row["FeeCycleName"],
                                    FeeMasterName = (string)row["FeeMasterName"],
                                    ClosingDebit = (decimal?)row["Cls_Debit"],
                                    ClosingCredit = (decimal?)row["Cls_Credit"],
                                    ClosingAmount = (decimal?)row["Cls_Amount"],
                                    OpeningDebit = (decimal?)row["Opn_Debit"],
                                    OpeningCredit = (decimal?)row["Opn_Credit"],
                                    OpeningAmount = (decimal?)row["Opn_Amount"],
                                    TransactionDebit = (decimal?)row["Trn_Debit"],
                                    TransactionCredit = (decimal?)row["Trn_Credit"],
                                    TransactionAmount = (decimal?)row["Trn_Amount"]
                                });
                            }
                        }
                        return listData;
                    }
                }

            }
            return listData;
        }

        public List<InvoiceGeneralMonthlyClosingDTO> GetInventoryCancelMonthlyClosing(DateTime? startDate, DateTime? endDate, int? companyID)
        {
            var listData = new List<InvoiceGeneralMonthlyClosingDTO>();
            using (var dbContext = new dbEduegateERPContext())
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
                    using (SqlCommand sqlCommand = new SqlCommand("account.SPS_PC_INVENTORY_CANCELLED_GENERAL", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@SDATE"].Value = startDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@EDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@EDATE"].Value = endDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SCHOOLS_IDs", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@SCHOOLS_IDs"].Value = _context.SchoolID.ToString();

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@COMPANY_IDs", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@COMPANY_IDs"].Value = companyID.HasValue?companyID.ToString(): "1";

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
                                listData.Add(new InvoiceGeneralMonthlyClosingDTO()
                                {
                                    TransactionTypeID = (int)row["TranTypeID"],
                                    DocumentTypeID = (int)row["DocumentTypeID"],

                                    TransactionTypeName = (string)row["TranTypeName"],
                                    DocumentTypeName = (string)row["DocumentTypeName"],

                                    ClosingDebit = (decimal?)row["Cls_Debit"],
                                    ClosingCredit = (decimal?)row["Cls_Credit"],
                                    ClosingAmount = (decimal?)row["Cls_Amount"],
                                    OpeningDebit = (decimal?)row["Opn_Debit"],
                                    OpeningCredit = (decimal?)row["Opn_Credit"],
                                    OpeningAmount = (decimal?)row["Opn_Amount"],
                                    TransactionDebit = (decimal?)row["Trn_Debit"],
                                    TransactionCredit = (decimal?)row["Trn_Credit"],
                                    TransactionAmount = (decimal?)row["Trn_Amount"]
                                });
                            }
                        }
                        return listData;
                    }
                }

            }
            return listData;
        }

        public List<AccountsMonthlyClosingDTO> GetAccountCancelMonthlyClosing(DateTime? startDate, DateTime? endDate, int? companyID)
        {
            var listData = new List<AccountsMonthlyClosingDTO>();
            using (var dbContext = new dbEduegateERPContext())
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
                    using (SqlCommand sqlCommand = new SqlCommand("account.SPS_PC_ACCOUNT_CANCELLED_GENERAL", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@SDATE"].Value = startDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@EDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@EDATE"].Value = endDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SCHOOLS_IDs", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@SCHOOLS_IDs"].Value = _context.SchoolID.ToString();

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@COMPANY_IDs", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@COMPANY_IDs"].Value = companyID.HasValue?companyID.ToString(): "1";

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
                                listData.Add(new AccountsMonthlyClosingDTO()
                                {
                                    TransactionTypeID = (int)row["TranTypeID"],
                                    DocumentTypeID = (int)row["DocumentTypeID"],

                                    TransactionTypeName = (string)row["TranTypeName"],
                                    DocumentTypeName = (string)row["DocumentTypeName"],

                                    ClosingDebit = (decimal?)row["Cls_Debit"],
                                    ClosingCredit = (decimal?)row["Cls_Credit"],
                                    ClosingAmount = (decimal?)row["Cls_Amount"],
                                    OpeningDebit = (decimal?)row["Opn_Debit"],
                                    OpeningCredit = (decimal?)row["Opn_Credit"],
                                    OpeningAmount = (decimal?)row["Opn_Amount"],
                                    TransactionDebit = (decimal?)row["Trn_Debit"],
                                    TransactionCredit = (decimal?)row["Trn_Credit"],
                                    TransactionAmount = (decimal?)row["Trn_Amount"]
                                });
                            }
                        }
                        return listData;
                    }
                }
            }
            return listData;
        }

        public List<StockMonthlyClosingDTO> GetStockMonthlyClosing(DateTime? startDate, DateTime? endDate, int? companyID)
        {
            var listData = new List<StockMonthlyClosingDTO>();
            using (var dbContext = new dbEduegateERPContext())
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
                    using (SqlCommand sqlCommand = new SqlCommand("account.SPS_PC_STOCK_GENERAL", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@SDATE"].Value = startDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@EDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@EDATE"].Value = endDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SCHOOLS_IDs", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@SCHOOLS_IDs"].Value = _context.SchoolID.ToString();

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@COMPANY_IDs", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@COMPANY_IDs"].Value = companyID.HasValue?companyID.ToString(): "1";

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
                                listData.Add(new StockMonthlyClosingDTO()
                                {
                                    TypeID = (int?)row["TypeID"],
                                    // AccountID = (long?)row["AccountID"],

                                    TypeName = (string)row["TypeName"],
                                    AccountName = (string)row["AccountName"],

                                    ClosingDebit = (decimal?)row["Cls_Debit"],
                                    ClosingCredit = (decimal?)row["Cls_Credit"],
                                    ClosingAmount = (decimal?)row["Cls_Amount"],
                                    OpeningDebit = (decimal?)row["Opn_Debit"],
                                    OpeningCredit = (decimal?)row["Opn_Credit"],
                                    OpeningAmount = (decimal?)row["Opn_Amount"],
                                    TransactionDebit = (decimal?)row["Trn_Debit"],
                                    TransactionCredit = (decimal?)row["Trn_Credit"],
                                    TransactionAmount = (decimal?)row["Trn_Amount"],

                                });
                            }
                        }
                        return listData;
                    }
                }
            }
            return listData;
        }
        public List<FeeMismatchedMonthlyClosingDTO> GetFeeMismatchedMonthlyClosing(DateTime? startDate, DateTime? endDate, int? companyID)
        {
            var listData = new List<FeeMismatchedMonthlyClosingDTO>();
            using (var dbContext = new dbEduegateERPContext())
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
                    using (SqlCommand sqlCommand = new SqlCommand("account.SPS_PC_FEE_MISMATCHED", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@SDATE"].Value = startDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@EDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@EDATE"].Value = endDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SCHOOLS_IDs", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@SCHOOLS_IDs"].Value = _context.SchoolID.ToString();

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@COMPANY_IDs", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@COMPANY_IDs"].Value = companyID.HasValue?companyID.ToString(): "1";

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
                                listData.Add(new FeeMismatchedMonthlyClosingDTO()
                                {
                                    FeeTypeID = (int)row["FeeTypeID"],
                                    FeeTypeName = (string)row["FeeTypeName"],
                                    SchoolName = (string)row["SchoolName"],
                                    StudentName = (string)row["StudentName"],
                                    InvoiceNo = (string)row["InvoiceNo"],
                                    InvoiceDate = (DateTime?)row["InvoiceDate"],
                                    Amount = (decimal?)row["Amount"],
                                    FeeName = (string)row["FeeNames"],
                                    Remarks = (string)row["Remarks"],
                                    AdmissionNumber = (string)row["AdmissionNumber"],
                                });
                            }
                        }
                        return listData;
                    }
                }
            }
            return listData;
        }

        public List<AccountMismatchedMonthlyClosingDTO> GetAccountsMismatchedMonthlyClosing(DateTime? startDate, DateTime? endDate, int? companyID)
        {
            var listData = new List<AccountMismatchedMonthlyClosingDTO>();
            using (var dbContext = new dbEduegateERPContext())
            {

                string message = string.Empty;
                SqlConnectionStringBuilder _sBuilder = new SqlConnectionStringBuilder(Infrastructure.ConfigHelper.GetDefaultConnectionString());
                _sBuilder.ConnectTimeout = 30; // Set Timedout
                using (SqlConnection conn = new SqlConnection(_sBuilder.ConnectionString))
                {
                    try
                    {
                        conn.Open();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    using (SqlCommand sqlCommand = new SqlCommand("account.SPS_PC_ACCOUNT_MISMATCHED", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@SDATE"].Value = startDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@EDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@EDATE"].Value = endDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SCHOOLS_IDs", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@SCHOOLS_IDs"].Value = _context.SchoolID.ToString();

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@COMPANY_IDs", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@COMPANY_IDs"].Value = companyID.HasValue?companyID.ToString(): "1";

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
                                listData.Add(new AccountMismatchedMonthlyClosingDTO()
                                {
                                    TranTypeName = (string)row["TranTypeName"],
                                    DocumentTypeName = (string)row["DocumentTypeName"],
                                    //TranDate = row["TranDate"] == null ? (DateTime?)null : (DateTime?)row["TranDate"],
                                    // Narration = (string)row["Narration"],
                                    //VoucherNo = (string)row["VoucherNo"],
                                    Amount = (decimal?)row["Amount"],
                                    Remarks = (string)row["Remarks"],
                                    //Branch = (string)row["Branch"],
                                    TranNo = (string)row["TranNo"]
                                });
                            }
                        }
                        return listData;
                    }
                }
            }
            return listData;
        }
        public List<AccountsGeneralMonthlyClosingDTO> GetAccountsGeneralMonthlyClosing(DateTime? startDate, DateTime? endDate, int? companyID)
        {
            var listData = new List<AccountsGeneralMonthlyClosingDTO>();
            using (var dbContext = new dbEduegateERPContext())
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
                    using (SqlCommand sqlCommand = new SqlCommand("account.SPS_PC_ACCOUNTS_GENERAL", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@SDATE"].Value = startDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@EDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@EDATE"].Value = endDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SCHOOLS_IDs", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@SCHOOLS_IDs"].Value = _context.SchoolID.ToString();

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@COMPANY_IDs", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@COMPANY_IDs"].Value = companyID.HasValue?companyID.ToString(): "1";

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
                                listData.Add(new AccountsGeneralMonthlyClosingDTO()
                                {
                                    //MainGroupID = (long?)row["MainGroupID"],
                                    MainGroupName = (string)row["MainGroupName"],
                                    //SubGroupID = (long?)row["SubGroupID"],
                                    SubGroupName = (string)row["SubGroupName"],
                                    //GroupID  = (long?)row["GroupID"], 
                                    GroupName = (string)row["GroupName"],
                                    ClosingDebit = (decimal?)row["Cls_Debit"],
                                    ClosingCredit = (decimal?)row["Cls_Credit"],
                                    ClosingAmount = (decimal?)row["Cls_Amount"],
                                    OpeningDebit = (decimal?)row["Opn_Debit"],
                                    OpeningCredit = (decimal?)row["Opn_Credit"],
                                    OpeningAmount = (decimal?)row["Opn_Amount"],
                                    TransactionDebit = (decimal?)row["Trn_Debit"],
                                    TransactionCredit = (decimal?)row["Trn_Credit"],
                                    TransactionAmount = (decimal?)row["Trn_Amount"]
                                });
                            }
                        }
                        return listData;
                    }
                }
            }
            return listData;
        }

        public string SubmitMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {
            var returnValue = false;
            var listData = new List<FeeMismatchedMonthlyClosingDTO>();
            using (var dbContext = new dbEduegateERPContext())
            {
                SqlConnectionStringBuilder _sBuilder = new SqlConnectionStringBuilder(Infrastructure.ConfigHelper.GetDefaultConnectionString());
                _sBuilder.ConnectTimeout = 30; // Set Timedout
                using (SqlConnection conn = new SqlConnection(_sBuilder.ConnectionString))
                {
                    try { conn.Open(); }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    using (SqlCommand sqlCommand = new SqlCommand("account.SPS_PC_SAVE_TRANSACTIONS", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@SDATE"].Value = startDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@EDATE", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@EDATE"].Value = toDate;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@FISCALYEAR_ID", SqlDbType.Int));
                        adapter.SelectCommand.Parameters["@FISCALYEAR_ID"].Value = 0;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@SCHOOLS_IDs", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@SCHOOLS_IDs"].Value = _context.SchoolID.ToString();

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@COMPANY_IDs", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@COMPANY_IDs"].Value = companyID.HasValue?companyID.ToString(): "1";

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@LOGINID", SqlDbType.Int));
                        adapter.SelectCommand.Parameters["@LOGINID"].Value = _context.LoginID;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@TRANHEADID", SqlDbType.Int));
                        adapter.SelectCommand.Parameters["@TRANHEADID"].Value = 0;


                        try
                        {
                            // Run the stored procedure.
                            returnValue = Convert.ToBoolean(sqlCommand.ExecuteScalar() ?? false);
                        }
                        catch (Exception ex)
                        {

                            returnValue = false;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
            return returnValue == true ? "1" : "0";
        }
        public DateTime? GetMonthlyClosingDate(long? branchID)
        {
            var branchIDs = branchID.HasValue && branchID != 0 ? branchID : _context.SchoolID.HasValue? (long?)_context.SchoolID:0;
            if (branchIDs == 0)
                return null;
            var companyID = branchIDs == 30 ? 2 : 1;
            using (var _sContext = new dbEduegateERPContext())
            {
                return _sContext.PeriodClosingTranHeads.Where(x => x.CompanyID == companyID).Max(x => x.ToDate);
            }
        }

    }
}