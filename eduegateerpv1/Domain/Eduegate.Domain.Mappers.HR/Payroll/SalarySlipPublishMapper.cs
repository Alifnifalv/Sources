using Eduegate.Domain.Entity.HR;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Payroll;
using System.Linq;
using Newtonsoft.Json;
using Eduegate.Framework;
using System;
using System.Collections.Generic;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Framework.Contracts.Common.Enums;
using System.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class SalarySlipPublishMapper : DTOEntityDynamicMapper
    {
        CallContext _callContext;
        public static SalarySlipPublishMapper Mapper(CallContext context)
        {
            var mapper = new SalarySlipPublishMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SalarySlipDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public List<SalarySlipEmployeeDTO> GetSalarySlipEmployeeData(int departmentID, int month, int year, long employeeID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

                var employeeSalarySlipList = new List<SalarySlipEmployeeDTO>();

                var salaryslipsDtos = new List<SalarySlipDTO>();

                employeeID = employeeID >= 0 ? employeeID : 0;

                string query = "SELECT * FROM payroll.SalarySlipReviewView WHERE MONTH(SlipDate) = " + month + " AND YEAR(SlipDate) = " + year;

                if (departmentID != 0)
                {
                    query += " AND DepartmentID = " + departmentID;
                }
                if (employeeID != 0)
                {
                    query += " AND employeeID = " + employeeID;
                }

                using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetSchoolConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

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
                            try
                            {
                                foreach (DataRow data in dataTable.Rows)
                                {
                                    if (dataTable.Columns.Contains("SalarySlipIID") && data["SalarySlipIID"] != null && !string.IsNullOrEmpty(data["SalarySlipIID"].ToString()))
                                    {
                                        salaryslipsDtos.Add(new SalarySlipDTO()
                                        {
                                            SalarySlipIID = (long)data["SalarySlipIID"],
                                            SlipDate = dataTable.Columns.Contains("SlipDate") && data["SlipDate"] != null && !string.IsNullOrEmpty(data["SlipDate"].ToString()) ? (DateTime?)data["SlipDate"] : null,
                                            EmployeeID = dataTable.Columns.Contains("EmployeeID") && data["EmployeeID"] != null && !string.IsNullOrEmpty(data["EmployeeID"].ToString()) ? (long)data["EmployeeID"] : (long?)null,
                                            EmployeeCode = dataTable.Columns.Contains("EmployeeCode") && data["EmployeeCode"] != null && !string.IsNullOrEmpty(data["EmployeeCode"].ToString()) ? data["EmployeeCode"].ToString() : null,
                                            EmployeeName = dataTable.Columns.Contains("EmployeeName") && data["EmployeeName"] != null && !string.IsNullOrEmpty(data["EmployeeName"].ToString()) ? data["EmployeeName"].ToString() : null,
                                            EmployeeWorkEmail = dataTable.Columns.Contains("EmployeeWorkEmail") && data["EmployeeWorkEmail"] != null && !string.IsNullOrEmpty(data["EmployeeWorkEmail"].ToString()) ? data["EmployeeWorkEmail"].ToString() : null,
                                            WorkingDays = dataTable.Columns.Contains("WorkingDays") && data["WorkingDays"] != null && !string.IsNullOrEmpty(data["WorkingDays"].ToString()) ? (decimal)data["WorkingDays"] : 0,
                                            LOPDays = dataTable.Columns.Contains("LOPDays") && data["LOPDays"] != null && !string.IsNullOrEmpty(data["LOPDays"].ToString()) ? (decimal)data["LOPDays"] : 0,
                                            NormalHours = dataTable.Columns.Contains("NormalHours") && data["NormalHours"] != null && !string.IsNullOrEmpty(data["NormalHours"].ToString()) ? (decimal)data["NormalHours"] : 0,
                                            OTHours = dataTable.Columns.Contains("OTHours") && data["OTHours"] != null && !string.IsNullOrEmpty(data["OTHours"].ToString()) ? (decimal)data["OTHours"] : 0,
                                            SalarySlipStatusID = dataTable.Columns.Contains("SalarySlipStatusID") && data["SalarySlipStatusID"] != null && !string.IsNullOrEmpty(data["SalarySlipStatusID"].ToString()) ? (byte?)data["SalarySlipStatusID"] : (byte?)null,
                                            SalarySlipStatusName = dataTable.Columns.Contains("SalarySlipStatus") && data["SalarySlipStatus"] != null && !string.IsNullOrEmpty(data["SalarySlipStatus"].ToString()) ? data["SalarySlipStatus"].ToString() : null,
                                            IsVerified = dataTable.Columns.Contains("IsVerified") && data["IsVerified"] != null && !string.IsNullOrEmpty(data["IsVerified"].ToString()) ? (bool)data["IsVerified"] : (bool?)null,
                                            ReportContentID = dataTable.Columns.Contains("ReportContentID") && data["ReportContentID"] != null && !string.IsNullOrEmpty(data["ReportContentID"].ToString()) ? (long?)data["ReportContentID"] : null,
                                            EarningAmount = dataTable.Columns.Contains("Earnings") && data["Earnings"] != null && !string.IsNullOrEmpty(data["Earnings"].ToString()) ? (decimal)data["Earnings"] : 0,
                                            DeductingAmount = dataTable.Columns.Contains("Deductions") && data["Deductions"] != null && !string.IsNullOrEmpty(data["Deductions"].ToString()) ? (decimal)data["Deductions"] : 0,
                                            Amount = dataTable.Columns.Contains("Amount") && data["Amount"] != null && !string.IsNullOrEmpty(data["Amount"].ToString()) ? (decimal)data["Amount"] : 0,
                                            BranchID = dataTable.Columns.Contains("BranchID") && data["BranchID"] != null && !string.IsNullOrEmpty(data["BranchID"].ToString()) ? (long)data["BranchID"] : (long?)null,
                                        });
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                var message = ex.Message;
                            }
                        }
                    }
                    conn.Close();
                }

                foreach (var salaryslip in salaryslipsDtos)
                {
                    employeeSalarySlipList.Add(new SalarySlipEmployeeDTO
                    {
                        SalarySlipIID = salaryslip.SalarySlipIID,
                        SlipDate = salaryslip.SlipDate.HasValue ? salaryslip.SlipDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        EmployeeID = salaryslip.EmployeeID,
                        EmployeeName = salaryslip.EmployeeCode + " - " + salaryslip.EmployeeName,
                        EmailAddress = salaryslip.EmployeeWorkEmail,
                        ReportContentID = salaryslip.ReportContentID,
                        WorkingDays = salaryslip.WorkingDays,
                        NormalHours = salaryslip.NormalHours,
                        OTHours = salaryslip.OTHours,
                        LOPDays = salaryslip.LOPDays,
                        SlipPublishStatusID = salaryslip.SalarySlipStatusID,
                        SlipPublishStatusName = salaryslip.SalarySlipStatusName,
                        IsVerified = salaryslip.IsVerified == null ? false : salaryslip.IsVerified,
                        IsSelected = false,
                        EarningAmount = salaryslip.EarningAmount,
                        DeductingAmount = salaryslip.DeductingAmount,
                        AmountToPay = salaryslip.Amount,
                        BranchID = salaryslip.BranchID,
                    });
                }

                return employeeSalarySlipList;
            }
        }

        public OperationResultDTO UpdateSalarySlipIsVerifiedData(long salarySlipID, bool isVerifiedStatus)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var message = new OperationResultDTO();
                try
                {
                    var slip = dbContext.SalarySlips.AsNoTracking().FirstOrDefault(x => x.SalarySlipIID == salarySlipID);

                    var salarySlips = slip != null ? dbContext.SalarySlips
                        .Where(y => y.EmployeeID == slip.EmployeeID && y.SlipDate == slip.SlipDate).AsNoTracking().ToList() : null;

                    if (salarySlips != null)
                    {
                        foreach (var slipData in salarySlips)
                        {
                            slipData.IsVerified = isVerifiedStatus;
                            slipData.UpdatedBy = Convert.ToInt32(_context.LoginID);
                            slipData.UpdatedDate = DateTime.Now;

                            dbContext.Entry(slipData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                            dbContext.SaveChanges();
                        }
                    }
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = "Verification status updated!"
                    };
                }
                catch (Exception ex)
                {
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = ex.Message
                    };
                }
                return message;
            }
        }

        public OperationResultDTO PublishSalarySlips(List<SalarySlipDTO> salarySlipDTOList)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var message = new OperationResultDTO();

                try
                {
                    foreach (var slipDTO in salarySlipDTOList)
                    {
                        UpdateSlipData(slipDTO);
                    }
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = "Slips published!"
                    };
                }
                catch (Exception ex)
                {
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = ex.Message
                    };
                }

                return message;
            }
        }

        public OperationResultDTO UpdateSlipData(SalarySlipDTO salarySlipDTO)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var settingBL = new Domain.Setting.SettingBL(_callContext);
                var message = new OperationResultDTO();
                var salarySlipApproved = settingBL.GetSettingValue<int>("SALARYSLIPAPPROVED_ID");
                var loanComponentID = settingBL.GetSettingValue<int>("LOAN_COMPONENT");
                var advanceComponentID = settingBL.GetSettingValue<int>("ADVANCE_COMPONENT");
                var approvedoanStatus = settingBL.GetSettingValue<int>("LOAN_STATUS_APPROVE");
                var scheduleInstallmentstatus = settingBL.GetSettingValue<byte>("LOAN_SCHEDULE_INSTALLMENT_STATUS");
                var paidInstallmentstatus = settingBL.GetSettingValue<byte>("LOAN_PAID_INSTALLMENT_STATUS");
                var completdLoanstatus = settingBL.GetSettingValue<byte>("COMPLETED_LOAN_STATUS");
                try
                {
                    var salarySlips = dbContext.SalarySlips
                        .Where(y => y.EmployeeID == salarySlipDTO.EmployeeID && y.SlipDate == salarySlipDTO.SlipDate).AsNoTracking().ToList();

                    if (salarySlips != null)
                    {
                        foreach (var slipData in salarySlips)
                        {
                            slipData.SalarySlipStatusID = salarySlipDTO.SalarySlipStatusID;
                            slipData.UpdatedBy = Convert.ToInt32(_context.LoginID);
                            slipData.UpdatedDate = DateTime.Now;

                            dbContext.Entry(slipData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                            dbContext.SaveChanges();

                            if (salarySlipDTO.SalarySlipStatusID == salarySlipApproved && (slipData.SalaryComponentID == loanComponentID || slipData.SalaryComponentID == advanceComponentID))
                            {
                                #region Loan

                                var loanData = (from lh in dbContext.LoanHeads
                                                where lh.EmployeeID == slipData.EmployeeID
                                                                && lh.LoanStatusID == approvedoanStatus
                                                                && lh.LoanDetails.Select(x => x.LoanEntryStatusID).Contains(scheduleInstallmentstatus)
                                                select lh)
                                                .Include(i => i.LoanDetails)
                                                .AsNoTracking().ToList();


                                var listHeadIds = loanData.Select(x => x.LoanHeadIID).Distinct().ToList();
                                var loanDetail = (from eld in dbContext.LoanDetails
                                                  where listHeadIds.Contains(eld.LoanHeadID.Value)
                                                  && (eld.InstallmentDate.Value.Year == slipData.SlipDate.Value.Year
                                                  && eld.InstallmentDate.Value.Month == slipData.SlipDate.Value.Month)
                                                  && eld.LoanEntryStatusID == scheduleInstallmentstatus
                                                  select eld).AsNoTracking().ToList();

                                var listDetailIds = loanDetail.Select(x => x.LoanDetailID).Distinct().ToList();
                                if (listDetailIds.Any())
                                {
                                    listDetailIds
                                        .All(w =>
                                        {
                                            var dRepLoanDetail = dbContext.LoanDetails.Where(x => x.LoanDetailID == w).AsNoTracking().FirstOrDefault();
                                            if (dRepLoanDetail != null)
                                            {
                                                dRepLoanDetail.PaidAmount = dRepLoanDetail.InstallmentAmount;
                                                dRepLoanDetail.InstallmentReceivedDate = System.DateTime.Now;
                                                dRepLoanDetail.IsPaid = true;
                                                dRepLoanDetail.LoanEntryStatusID = paidInstallmentstatus;
                                                dbContext.Entry(dRepLoanDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                                dbContext.SaveChanges();
                                            }
                                            return true;
                                        });
                                }

                                if (listHeadIds.Any())
                                {
                                    listHeadIds
                                        .All(w =>
                                        {
                                            var dRepLoanHeads = dbContext.LoanHeads.Where(x => x.LoanHeadIID == w).Include(x => x.LoanDetails).AsNoTracking().FirstOrDefault();
                                            if (dRepLoanHeads != null)
                                            {
                                                if (dRepLoanHeads.LoanDetails.Where(i => i.LoanHeadID == w && i.LoanEntryStatusID != paidInstallmentstatus).Count() == 0)
                                                {
                                                    dRepLoanHeads.LoanStatusID = completdLoanstatus;
                                                }
                                                dRepLoanHeads.LastInstallmentAmount = dRepLoanHeads.InstallmentAmount;
                                                dRepLoanHeads.LastInstallmentDate = System.DateTime.Now;
                                                dbContext.Entry(dRepLoanHeads).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                                dbContext.SaveChanges();
                                            }
                                            return true;
                                        });
                                }
                                #endregion
                            }

                        }
                    }
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = "Slip updated!"
                    };
                }
                catch (Exception ex)
                {
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = ex.Message
                    };
                }

                return message;
            }
        }

    }
}