using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Models.Workflows;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Logs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Data.Entity;
using Eduegate.Domain.Repository.Frameworks;
using System.Net.Mail;
using Eduegate.Framework.Extensions;
using System.Net;
using Eduegate.Domain.Entity.School.Models.School;

namespace Eduegate.Domain.Repository.Workflows
{
    public class WorkflowRepository
    {
        public Workflow GetWorkflowByDocumentType(long documentTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var workflow = dbContext.DocumentTypes
                    .Include(a => a.Workflow)
                    .Include(a => a.Workflow.WorkflowRules)
                    .Include(a => a.Workflow.WorkflowFiled)
                    .Where(a => a.DocumentTypeID == documentTypeID)
                    .FirstOrDefault();

                if (workflow != null && workflow.Workflow != null)
                {
                    foreach (var rule in workflow.Workflow.WorkflowRules)
                    {
                        dbContext.Entry(rule).Collection(a => a.WorkflowRuleConditions).Load();
                        dbContext.Entry(rule).Reference(a => a.WorkflowCondition).Load();

                        foreach (var condition in rule.WorkflowRuleConditions)
                        {
                            dbContext.Entry(condition).Collection(a => a.WorkflowRuleApprovers).Load();
                            dbContext.Entry(condition).Reference(a => a.WorkflowRule).Load();
                            dbContext.Entry(condition).Reference(a => a.WorkflowCondition).Load();
                        }
                    }
                }

                return workflow == null ? null : workflow.Workflow;
            }
        }


        public List<WorkflowType> GetWorkflowTypes()
        {
            List<WorkflowType> entity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    entity = dbContext.WorkflowTypes.ToList();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
            }

            return entity;
        }

        public List<WorkflowFiled> GetWorkflowFeilds(int workflowType)
        {
            List<WorkflowFiled> entity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    entity = dbContext.WorkflowFileds.Where(a => a.WorkflowTypeID == workflowType).ToList();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
            }

            return entity;
        }

        public List<WorkflowTransactionHeadMap> GetWorkflowLog(long headID)
        {
            List<WorkflowTransactionHeadMap> entity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    entity = dbContext.WorkflowTransactionHeadMaps
                        .Include(a => a.Workflow)
                        .Include(a => a.WorkflowStatus)
                        .Include(a => a.WorkflowTransactionHeadRuleMaps.Select(ab => ab.WorkflowStatus))
                        .Include(a => a.WorkflowTransactionHeadRuleMaps.Select(ab => ab.WorkflowCondition))
                        .Where(a => a.TransactionHeadID == headID).ToList();

                    foreach (var log in entity)
                    {
                        foreach (var rule in log.WorkflowTransactionHeadRuleMaps)
                        {
                            foreach (var approver in rule.WorkflowTransactionRuleApproverMaps)
                            {
                                dbContext.Entry(approver).Reference(a => a.Employee).Load();
                                dbContext.Entry(approver.Employee).Reference(a => a.Login).Load();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
            }

            return entity;
        }

        public Workflow GetWorkflowByWorkflowID(long workflowID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var entity = dbContext.Workflows
                        .Include(a => a.WorkflowRules)
                        .Include(a => a.WorkflowFiled)
                        .Include(a => a.WorkflowType)
                        .Include("WorkflowRules.WorkflowRuleConditions")
                        .Include("WorkflowRules.WorkflowRuleConditions.WorkflowCondition.WorkflowRuleConditions")
                        .Include("WorkflowRules.WorkflowRuleConditions.WorkflowCondition.WorkflowRuleConditions.WorkflowRuleApprovers")
                        .Include("WorkflowRules.WorkflowRuleConditions.WorkflowCondition.WorkflowRuleConditions.WorkflowRuleApprovers.Employee")
                        .FirstOrDefault(a => a.WorkflowIID == workflowID);

                    return entity;
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
                return null;
            }

        }

        public List<WorkflowLogMap> GetWorkflowLogByWorkflowIDAndReferenceID(long workflowID, long referenceID)
        {
            List<WorkflowLogMap> entity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    entity = dbContext.WorkflowLogMaps
                        .Include(a => a.Workflow)
                        .Include(a => a.WorkflowStatus)
                        .Where(a => (workflowID == 0 || a.WorkflowID.Value == workflowID) && a.ReferenceID == referenceID).ToList();

                    foreach (var log in entity)
                    {
                        foreach (var rule in log.WorkflowLogMapRuleMaps)
                        {
                            foreach (var approver in rule.WorkflowLogMapRuleApproverMaps)
                            {
                                dbContext.Entry(approver).Reference(a => a.Employee).Load();
                                dbContext.Entry(approver.Employee).Reference(a => a.Login).Load();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
            }

            return entity;
        }

        public Workflow GetWorkflow(long workflowID)
        {
            Workflow entity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    entity = dbContext.Workflows.Where(a => a.WorkflowIID == workflowID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
            }

            return entity;
        }

        public List<Workflow> GetWorkflow()
        {
            List<Workflow> entity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    entity = dbContext.Workflows.ToList();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
            }

            return entity;
        }

        public List<Workflow> GetWorkflowByType(long workflowTypeID)
        {
            List<Workflow> entity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    entity = dbContext.Workflows.Where(a => a.WorkflowTypeID == workflowTypeID).ToList();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
            }

            return entity;
        }

        public List<WorkflowCondition> GetWorkflowConditionByType(string conditionType)
        {
            List<WorkflowCondition> entity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    entity = dbContext.WorkflowConditions.Where(a => a.ConditionType == conditionType).ToList();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
            }

            return entity;
        }

        public bool UpdateTransactionWorkflowStatus(long roleMapID, long employeeID, int statusID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var entity = dbContext.WorkflowTransactionHeadRuleMaps
                        .Include(a => a.WorkflowTransactionRuleApproverMaps)
                        .Include(a => a.WorkflowTransactionHeadMap)
                        .Where(a => a.WorkflowTransactionHeadRuleMapIID == roleMapID).FirstOrDefault();

                    var allApproved = true;
                    var anyApproved = false;

                    foreach (var approver in entity.WorkflowTransactionRuleApproverMaps)
                    {
                        if (approver.EmployeeID == employeeID)
                        {
                            approver.WorkflowStatusID = byte.Parse(statusID.ToString());
                        }

                        if (approver.WorkflowConditionID == 4 && allApproved && approver.WorkflowStatusID == 1)
                        {
                            allApproved = false;
                        }

                        if (approver.WorkflowConditionID == 3 && !anyApproved && approver.WorkflowStatusID == 2)
                        {
                            anyApproved = true;
                        }
                    }


                    if (allApproved || anyApproved)
                    {
                        entity.WorkflowStatusID = 2;
                        bool allRulesApproved = true;
                        //check all status approved
                        foreach (var rules in entity.WorkflowTransactionHeadMap.WorkflowTransactionHeadRuleMaps)
                        {
                            if (rules.WorkflowStatusID == 1)
                            {
                                allRulesApproved = false;
                            }
                        }

                        if (allRulesApproved)
                        {
                            entity.WorkflowTransactionHeadMap.WorkflowStatusID = 2;
                        }
                    }

                    dbContext.Entry(entity).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
                return false;
            }

            return true;
        }

        public bool UpdateAssignmentStatus(long workflowRuleID, string rule, long? referenceID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    //For Change Assignment Status Based on Work Flow

                    if (rule != null)
                    {
                        var StudentAssignmentRepository = new EntiyRepository<Assignment, dbEduegateERPContext>(dbContext);
                        var studentAssignment = StudentAssignmentRepository.Get(a => a.AssignmentIID == referenceID).FirstOrDefault();

                        var applicationStatusRepository = new EntiyRepository<AssignmentStatus, dbEduegateERPContext>(dbContext);
                        var applicationStatuses = applicationStatusRepository.Get(a => a.AssignmentStatusID != 0).ToList();

                        if (studentAssignment != null)
                        {

                            foreach (var status in applicationStatuses)
                            {
                                if (rule.Trim().ToUpper() == status.StatusName.Trim().ToUpper())
                                {
                                    studentAssignment.AssignmentStatusID = status.AssignmentStatusID;

                                    break;
                                }
                            }

                            dbContext.Entry(studentAssignment).State = EntityState.Modified;
                            dbContext.SaveChanges();


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Assignment", EventLogEntryType.Error, ex, "Not able to Update to Assignment Status", TrackingCode.ERP);
                return false;
            }

            return true;
        }

        public bool UpdateMarkRegisterStatus(long workflowRuleID, string rule, long? referenceID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {

                    //For Change Mark Register Status Based on Work Flow

                    if (rule != null)
                    {
                        var applicationRepository = new EntiyRepository<MarkRegister, dbEduegateERPContext>(dbContext);
                        var application = applicationRepository.Get(a => a.MarkRegisterIID == referenceID).FirstOrDefault();

                        var applicationStatusRepository = new EntiyRepository<MarkEntryStatus, dbEduegateERPContext>(dbContext);
                        var applicationStatuses = applicationStatusRepository.Get(a => a.MarkEntryStatusID != 0).ToList();


                        if (application != null)
                        {

                            foreach (var status in applicationStatuses)
                            {
                                if (rule.Trim().ToUpper() == status.MarkEntryStatusName.Trim().ToUpper())
                                {
                                    application.MarkEntryStatusID = status.MarkEntryStatusID;

                                    break;
                                }
                            }

                            dbContext.Entry(application).State = EntityState.Modified;
                            dbContext.SaveChanges();


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Update MarkRegister Status", EventLogEntryType.Error, ex, "Not able to Update Mark Register Status", TrackingCode.ERP);
                return false;
            }

            return true;
        }
        public bool UpdateLessonPlanStatus(long workflowRuleID, string rule,int statusID, long? referenceID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {

                    //For Change Update Lesson Plan Status Based on Work Flow

                    if (rule != null)
                    {
                        var application = dbContext.LessonPlan.FirstOrDefault(l => l.LessonPlanIID == referenceID);

                        if (statusID == 0)
                        {
                            var settingData = dbContext.Settings.FirstOrDefault(s => s.SettingCode == "Lesson_Rejected_StatusID");
                            var rejectedStsID = byte.Parse(settingData?.SettingValue);
                            statusID = rejectedStsID;
                        }

                        application.LessonPlanStatusID = (byte?)statusID;
                        application.UpdatedDate = DateTime.Now;

                        dbContext.Entry(application).State = EntityState.Modified;
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Update Lesson Plan Status", EventLogEntryType.Error, ex, "Not able to Update Lesson Plan Status", TrackingCode.ERP);
                return false;
            }

            return true;
        }

        public bool UpdateApplicationStatus(long workflowRuleID, string rule, long? referenceID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {

                    //For Change Student Application Status Based on Work Flow


                    if (rule != null)
                    {
                        var applicationRepository = new EntiyRepository<StudentApplication, dbEduegateERPContext>(dbContext);
                        var application = applicationRepository.Get(a => a.ApplicationIID == referenceID).FirstOrDefault();

                        var applicationStatusRepository = new EntiyRepository<ApplicationStatus, dbEduegateERPContext>(dbContext);
                        var applicationStatuses = applicationStatusRepository.Get(a => a.ApplicationStatusID != 0).ToList();

                        MutualRepository mutualRepository = new MutualRepository();
                        var settingValue = mutualRepository.GetSettingData("STUDENT_APPLICATION_WORKFLOW_ID");

                        if (application != null)
                        {
                            String Status = "";
                            foreach (var status in applicationStatuses)
                            {
                                if (rule.Trim().ToUpper() == status.Description.Trim().ToUpper())
                                {
                                    application.ApplicationStatusID = status.ApplicationStatusID;

                                    if (settingValue.SettingValue != null && long.Parse(settingValue.SettingValue) == workflowRuleID)
                                    {
                                        Status = @"
                                                <p style='font-size:0.9em;' font-family:'Times New Roman;' align='left'>Congratulations!! <br /> Your Candidate Application taken into Acceptance.we will contact you soon...<br /><br />Regards,<br />Pearl School</p><br/><br/><p style='font-size:0.7rem;'<b>Please Note: </b>do not reply to this email as it is a computer generated email</p>";
                                    }
                                    break;
                                }
                            }

                            dbContext.Entry(application).State = EntityState.Modified;
                            dbContext.SaveChanges();
                            if (Status != "")
                            {
                                var hostUser = ConfigurationExtensions.GetAppConfigValue("EmailUser").ToString();
                                String emailBody = @"
                                       
                                        <style='font-size:0.9rem;'>" + Status + @"</style><br />";
                                String replaymessage = PopulateBody(" ", emailBody);

                                var hostDet = mutualRepository.GetSettingData("HOST_NAME").SettingValue;

                                string defaultMail = mutualRepository.GetSettingData("DEFAULT_MAIL_ID").SettingValue;

                                if (hostDet == "Live")
                                {

                                }
                                else
                                {
                                    SendMail(defaultMail, "Admission Status", replaymessage, hostUser);
                                }
                            }

                        }
                    }


                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to update the status", TrackingCode.ERP);
                return false;
            }

            return true;
        }

        public bool UpdateLeaveStatus(long workflowRuleID, string rule, int statusID, long? referenceID, long employeeID)
        {
            MutualRepository mutualRepository = new MutualRepository();
            try
            {
                using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
                {
                    var approvedStatusID = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "LEAVE_APPROVED_STATUS_ID");
                    var rejectedStatusID = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "LEAVE_REJECT_STATUS_ID");

                    var leaveApprovedStatusID = byte.Parse(approvedStatusID.SettingValue);
                    var leaveRejectedStatusID = byte.Parse(rejectedStatusID.SettingValue);

                    var workflowApprovedStatusID = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "WORKFLOW_APPROVED_STATUS_ID");
                    var approveStatusID = byte.Parse(workflowApprovedStatusID.SettingValue);

                    var workflowRejectedStatusID = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "WORKFLOW_REJECTED_STATUS_ID");
                    var rejectStatusID = byte.Parse(workflowRejectedStatusID.SettingValue);

                    var studLeaveList = dbContext.StudentLeaveApplications.FirstOrDefault(x => x.StudentLeaveApplicationIID == referenceID);
                    var employee = dbContext.Employees.FirstOrDefault(x => x.EmployeeIID == employeeID);

                    if (rule != null && statusID == approveStatusID && studLeaveList != null)//approve
                    {
                        studLeaveList.UpdatedBy = (int?)employee.LoginID;
                        studLeaveList.UpdatedDate = DateTime.Now;
                        studLeaveList.LeaveStatusID = leaveApprovedStatusID;
                        UpdateStudentAttendanceStatus(referenceID, studLeaveList.UpdatedBy);
                    }
                    else if (statusID == rejectStatusID && studLeaveList != null) //reject
                    {
                        studLeaveList.UpdatedBy = (int?)employee.LoginID;
                        studLeaveList.UpdatedDate = DateTime.Now;
                        studLeaveList.LeaveStatusID = leaveRejectedStatusID;
                    }
                    dbContext.Entry(studLeaveList).State = EntityState.Modified;
                    dbContext.SaveChanges();

                    if (statusID == approveStatusID || statusID == rejectStatusID)//approve or reject case
                    {
                        LeaveAlertsNotificationSendtoParent(referenceID, studLeaveList.UpdatedBy);
                    }
                }

                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var workflowApprovedStatusID = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "WORKFLOW_APPROVED_STATUS_ID");
                    var approveStatusID = byte.Parse(workflowApprovedStatusID.SettingValue);

                    var workflowRejectedStatusID = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "WORKFLOW_REJECTED_STATUS_ID");
                    var rejectStatusID = byte.Parse(workflowRejectedStatusID.SettingValue);

                    var wrkFlowLog = dbContext.WorkflowLogMaps.FirstOrDefault(s => s.ReferenceID == referenceID);
                    var wrkflowlogRuleMap = wrkFlowLog != null ? dbContext.WorkflowLogMapRuleMaps.FirstOrDefault(t => t.WorkflowLogMapID == wrkFlowLog.WorkflowLogMapIID) : null;

                    if (rule != null && statusID == rejectStatusID)//rejected
                    {
                        wrkflowlogRuleMap.WorkflowStatusID = rejectStatusID;
                    }

                    else
                    {
                        wrkflowlogRuleMap.WorkflowStatusID = approveStatusID;
                    }
                    dbContext.Entry(wrkflowlogRuleMap).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to update the status", TrackingCode.ERP);
                return false;
            }

            return true;
        }

        public bool UpdateTransferRequestStatus(long workflowRuleID, string rule, int statusID, long? referenceID, long employeeID)
        {

            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var processingStatusID = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "TRANSFER_REQUEST_STATUS_ID_PROCESSING");
                var feeDueSettledStatusID = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "TRANSFER_REQUEST_STATUS_ID_FEEDUESETTLED");
                var approveStatusID = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "TRANSFER_REQUEST_STATUS_ID_APPROVED");
                var transferApprovalWorkFlowID = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "STUDENT_TRANSFER_REQUEST_WORKFLOW_ID");

                var stage1 = byte.Parse(processingStatusID.SettingValue);
                var stage2 = byte.Parse(feeDueSettledStatusID.SettingValue);
                var stage3 = byte.Parse(approveStatusID.SettingValue);
                var workflowID = long.Parse(transferApprovalWorkFlowID.SettingValue);

                var workflowApprovedStatusID = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "WORKFLOW_APPROVED_STATUS_ID");
                var workFlowApprovedID = byte.Parse(workflowApprovedStatusID.SettingValue);

                var workflowRejectedStatusID = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "WORKFLOW_REJECTED_STATUS_ID");
                var rejectStatusID = byte.Parse(workflowRejectedStatusID.SettingValue);

                var studTransferData = dbContext.StudentTransferRequest.FirstOrDefault(x => x.StudentTransferRequestIID == referenceID);

                var stage0 = studTransferData.TransferRequestStatusID == stage1 || studTransferData.TransferRequestStatusID == stage2 || studTransferData.TransferRequestStatusID == stage3;

                if (rule != null && statusID == workFlowApprovedID && stage0 == false)//update transfer statuses => processing to FeeDueSettled
                {
                    studTransferData.UpdatedBy = (int?)employeeID;
                    studTransferData.UpdatedDate = DateTime.Now;
                    studTransferData.TransferRequestStatusID = stage1;
                }
                else if (rule != null && statusID == workFlowApprovedID && studTransferData.TransferRequestStatusID == stage1)
                {
                    studTransferData.UpdatedBy = (int?)employeeID;
                    studTransferData.UpdatedDate = DateTime.Now;
                    studTransferData.TransferRequestStatusID = stage2;
                }
                else if (rule != null && statusID == workFlowApprovedID && studTransferData.TransferRequestStatusID == stage2)//update transfer statuses => FeeDueSettled to Completed/Approved
                {
                    studTransferData.UpdatedBy = (int?)employeeID;
                    studTransferData.UpdatedDate = DateTime.Now;
                    studTransferData.TransferRequestStatusID = stage3;
                }
                dbContext.Entry(studTransferData).State = EntityState.Modified;
                dbContext.SaveChanges();
                SaveTCworkflowAlertsNotification(workflowID, (long)referenceID, studTransferData.TransferRequestStatusID, employeeID);
            }

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var workflowApprovedStatusID = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "WORKFLOW_APPROVED_STATUS_ID");
                var workFlowApprovedID = byte.Parse(workflowApprovedStatusID.SettingValue);

                var workflowRejectedStatusID = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "WORKFLOW_REJECTED_STATUS_ID");
                var rejectStatusID = byte.Parse(workflowRejectedStatusID.SettingValue);

                var wrkFlowLog = dbContext.WorkflowLogMaps.FirstOrDefault(s => s.ReferenceID == referenceID);
                var wrkflowlogRuleMap = wrkFlowLog != null ? dbContext.WorkflowLogMapRuleMaps.FirstOrDefault(t => t.WorkflowLogMapID == wrkFlowLog.WorkflowLogMapIID) : null;

                if (rule != null && statusID == rejectStatusID && wrkflowlogRuleMap != null)//reject
                {
                    wrkflowlogRuleMap.WorkflowStatusID = rejectStatusID;
                }

                else if (rule != null && statusID == workFlowApprovedID && wrkflowlogRuleMap != null)//approve
                {
                    wrkflowlogRuleMap.WorkflowStatusID = workFlowApprovedID;
                }

                dbContext.Entry(wrkflowlogRuleMap).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
            return true;
        }

        public WorkflowRule UpdateWorkflowStatus(long roleMapID, long employeeID, int statusID, long? referenceID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {

                    var entity = dbContext.WorkflowLogMapRuleMaps
                        .Include(a => a.WorkflowLogMapRuleApproverMaps)
                        .FirstOrDefault(a => a.WorkflowLogMapRuleMapIID == roleMapID);


                    foreach (var approver in entity.WorkflowLogMapRuleApproverMaps)
                    {
                        if (approver.EmployeeID == employeeID)
                        {
                            var emp = dbContext.Employees.FirstOrDefault(x => x.EmployeeIID == employeeID);
                            entity.Description = @" - <b>"+ entity.Value1 + "</b>"  + " by " + emp.EmployeeCode + " - " + emp.FirstName + " " + emp.MiddleName + " " + emp.LastName;
                            entity.WorkflowStatusID = (byte?)statusID;
                            entity.IsFlowCompleted = true;
                        }
                        else
                        {
                            return null;
                        }
                    }

                    #region old code commented // for future reference

                    //var allApproved = true;
                    //var anyApproved = false;
                    //var workflowID = (long?)null;

                    //foreach (var approver in entity.WorkflowLogMapRuleApproverMaps)
                    //{
                    //    if (approver.EmployeeID == employeeID)
                    //    {
                    //        approver.WorkflowStatusID = byte.Parse(statusID.ToString());
                    //    }

                    //    if (approver.WorkflowConditionID == 4 && allApproved && approver.WorkflowStatusID == 1)
                    //    {
                    //        allApproved = false;
                    //    }

                    //    if (approver.WorkflowConditionID == 3 && !anyApproved && approver.WorkflowStatusID == approveStatusID)
                    //    {
                    //        anyApproved = true;
                    //    }
                    //    workflowID = approver.WorkflowLogMapRuleMap.WorkflowRule.WorkflowID;
                    //}

                    //if (allApproved || anyApproved)
                    //{
                    //    entity.WorkflowStatusID = approveStatusID;
                    //    bool allRulesApproved = true;
                    //    //check all status approved
                    //    foreach (var rules in entity.WorkflowLogMapRuleApproverMaps)
                    //    {
                    //        if (rules.WorkflowStatusID == 1)
                    //        {
                    //            allRulesApproved = false;
                    //        }
                    //    }

                    //    if (allRulesApproved)
                    //    {
                    //        entity.WorkflowLogMap.WorkflowStatusID = approveStatusID;
                    //    }
                    //}

                    //var workFlowRuleApproverRepository = new EntiyRepository<WorkflowRuleApprover, dbEduegateERPContext>(dbContext);
                    //var ruleApprover = workFlowRuleApproverRepository.Get(a => a.EmployeeID == employeeID && a.WorkflowRuleCondition.WorkflowRule.WorkflowID == workflowID).FirstOrDefault();

                    //if (ruleApprover != null)
                    //{
                    //    var workFlowRuleConditionRepository = new EntiyRepository<WorkflowRuleCondition, dbEduegateERPContext>(dbContext);

                    //    var ruleCondition = workFlowRuleConditionRepository.Get(a => a.WorkflowRuleConditionIID == ruleApprover.WorkflowRuleConditionID).FirstOrDefault();

                    //    if (ruleCondition != null)
                    //    {
                    //        var workFlowRuleRepository = new EntiyRepository<WorkflowRule, dbEduegateERPContext>(dbContext);
                    //        var rule = workFlowRuleRepository.Get(a => a.WorkflowRuleIID == ruleCondition.WorkflowRuleID).FirstOrDefault();

                    //        if (rule != null)
                    //        {
                    //            return rule;
                    //        }

                    //    }
                    //}
                    #endregion old code end


                    dbContext.Entry(entity).State = EntityState.Modified;
                    dbContext.SaveChanges();                  
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("UpdateWorkflowStatus", EventLogEntryType.Error, ex, "Not able to update the status", TrackingCode.ERP);
                return null;
            }

            return null;
        }

        public WorkflowRule RejectCurrentWorkFlow(long roleMapID, long employeeID, int statusID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {

                   var entity = dbContext.WorkflowLogMapRuleMaps
                        .Include(a => a.WorkflowLogMapRuleApproverMaps)
                        .FirstOrDefault(a => a.WorkflowLogMapRuleMapIID == roleMapID);

                    var emp = dbContext.Employees.FirstOrDefault(x => x.EmployeeIID == employeeID);
                    entity.Description = @" - <b>Rejected by </b>" + emp.EmployeeCode + " - " + emp.FirstName + " " + emp.MiddleName + " " + emp.LastName;
                    entity.WorkflowStatusID = (byte?)statusID;
                    entity.IsFlowCompleted = true;

                    dbContext.Entry(entity).State = EntityState.Modified;
                    dbContext.SaveChanges();

                    GetWorkFlowRules(roleMapID, employeeID);
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("UpdateWorkflowStatus", EventLogEntryType.Error, ex, "Not able to update the status", TrackingCode.ERP);
                return null;
            }

            return null;
        }



        public WorkflowRule GetWorkFlowRules(long roleMapID, long employeeID)
        {

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var roleMap = dbContext.WorkflowLogMapRuleMaps.FirstOrDefault(r => r.WorkflowLogMapRuleMapIID == roleMapID);
                var getWorkFlowRule = dbContext.WorkflowRules.FirstOrDefault(x => x.WorkflowRuleIID == roleMap.WorkflowRuleID);

                if (getWorkFlowRule != null)
                {
                    return getWorkFlowRule;

                }
                else
                {
                    return null;
                }
            }
        }

        public void SendMail(string email, string subject, string msg, string mailname)
        {
            string email_id = email;
            string mail_body = msg;
            try
            {
                var hostEmail = ConfigurationExtensions.GetAppConfigValue("SMTPUserName").ToString();
                var hostPassword = ConfigurationExtensions.GetAppConfigValue("SMTPPassword").ToString();
                var fromEmail = ConfigurationExtensions.GetAppConfigValue("EmailFrom").ToString();

                SmtpClient ss = new SmtpClient();
                ss.Host = ConfigurationExtensions.GetAppConfigValue("EmailHost").ToString();//"smtpout.secureserver.net";// "smtp.gmail.com";//"smtp.zoho.com";//
                ss.Port = ConfigurationExtensions.GetAppConfigValue<int>("smtpPort");// 587;//465;//;
                ss.Timeout = 20000;
                ss.DeliveryMethod = SmtpDeliveryMethod.Network;
                ss.UseDefaultCredentials = true;
                ss.EnableSsl = true;
                ss.Credentials = new NetworkCredential(hostEmail, hostPassword);//elcguide@gmail.com elcguide!@#$

                MailMessage mailMsg = new MailMessage(hostEmail, email, subject, msg);
                mailMsg.From = new MailAddress(fromEmail, mailname);
                mailMsg.IsBodyHtml = true;
                mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                ss.Send(mailMsg);

            }
            catch (Exception ex)
            {

                //lb_error.Visible = true;
                // return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }


            //return Json("ok", JsonRequestBehavior.AllowGet);
        }
        private String PopulateBody(String Name, String htmlMessage)
        {
            string body = string.Empty;
            //using (StreamReader reader = new StreamReader("http://erp.eduegate.com/emailtemplate.html"))
            //{
            //    body = reader.ReadToEnd();
            //}
            body = "<!DOCTYPE html> <html> <head> <title></title> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <meta name='viewport' content='width=device-width, initial-scale=1'> <meta http-equiv='X-UA-Compatible' content='IE=edge' /> <style type='text/css'> </style> </head> <body style='background-color: #f4f4f4; margin: 0 !important; padding: 0 !important;'> <!-- HIDDEN PREHEADER TEXT --> <table border='0' cellpadding='0' cellspacing='0' width='100%'> <!-- LOGO --> <tr> <td bgcolor='#bd051c' align='center'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'> <tr> <td align='center' valign='top' style='padding: 40px 10px 40px 10px;'> </td> </tr> </table> </td> </tr> <tr> <td bgcolor='#bd051c' align='center' style='padding: 0px 10px 0px 10px;'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'> <tr> <td bgcolor='#ffffff' align='center' valign='top' style='padding: 40px 20px 20px 20px; border-radius: 4px 4px 0px 0px; color: #111111; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 48px; font-weight: 400; letter-spacing: 4px; line-height: 48px;'> <div align='center' style='width:100%;display:inline-block;text-align:center;'><img src='https://parent.pearlschool.org/img/podarlogo_mails.png' style='height:70px;margin:1rem;' /></div> </td> </tr> </table> </td> </tr> <tr> <td bgcolor='#f4f4f4' align='center' style='padding: 0px 10px 0px 10px;'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'> <tr> <td bgcolor='#ffffff' align='left' style='padding: 1rem; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;'>Hi <b>'{CUSTOMERNAME}'</b><br />{HTMLMESSAGE}</td > </tr > </table > </td > </tr > <tr > <td bgcolor='#f4f4f4' align='center' style='padding: 30px 10px 0px 10px;' > <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;' > <tr > <td bgcolor='black' align='center' style='padding: 30px 30px 30px 30px; border-radius: 4px 4px 4px 4px; color: #fffefe; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;'> <div class='copyrightdiv' style='color: white;padding: 30px 30px 30px 30px;' >Copyright &copy; {YEAR}<a style='text-decoration: none' target='_blank' href='http://pearlschool.org/' > <span style='color: white; font-weight: bold;' >&nbsp;&nbsp; PEARL SCHOOL</span > </a > </div > </td > </tr > </table > </td > </tr > <tr > <td bgcolor='#f4f4f4' align='center' style='padding: 0px 10px 0px 10px;' > <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;' > <tr > <td bgcolor='#f4f4f4' align='left' style='padding: 0px 30px 30px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 400; line-height: 18px;' > <br > <div class='PoweredBy' style='text-align:center;' > <div style='padding-bottom:1rem;' > Powered By: <a style='text-decoration: none; color: #0c7aec;' id='eduegate' href='https://softopsolutionpvtltd.com/' target='_blank' > SOFTOP SOLUTIONS PVT LTD</a > </div > <a href='https://www.facebook.com/pearladmin1/' > <img src='https://parent.pearlschool.org/Images/logo/fb-logo.png' alt='facebook' width='30' height='25' border='0' style='margin-right:.5rem;' / > </a > <a href='https://www.linkedin.com/company/pearl-school-qatar/?viewAsMember=true' > <img src='https://parent.pearlschool.org/Images/logo/linkedin-logo.png' alt='twitter' width='30' height='25' border='0' style='margin-right:.5rem;' / > </a > <a href='https://www.instagram.com/pearlschool_qatar/' > <img src='https://parent.pearlschool.org/Images/logo/insta-logo.png' alt='instagram' width='30' height='25' border='0' style='margin-right:.5rem;' / > </a > <a href='https://www.youtube.com/channel/UCFQKYMivtaUgeSifVmg79aQ' > <img src='https://parent.pearlschool.org/Images/logo/youtube-logo.png' alt='twitter' width='30' height='25' border='0' style='margin-right:.5rem;' / > </a > </div > </td > </tr > </table > </td > </tr > </table > </body > </html >";
            body = body.Replace("{CUSTOMERNAME}", Name);
            body = body.Replace("{HTMLMESSAGE}", htmlMessage);
            body = body.Replace("{YEAR}", DateTime.Now.Year.ToString());
            return body;
        }
        public bool UpdateWorkflowLogs(List<WorkflowLogMap> logs)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                dbContext.WorkflowLogMaps.AddRange(logs);
                dbContext.SaveChanges();
            }

            return true;
        }

        public List<WorkflowStatus> GetWorkflowStatusesByWorkflowID(long workflowID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var workflow = dbContext.Workflows
                        .Where(a => a.WorkflowIID == workflowID).FirstOrDefault();
                    return dbContext.WorkflowStatuses.Where(a => a.WorkflowTypeID == workflow.WorkflowTypeID).ToList();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
                return null;
            }
        }

        public Setting GetSettingData(string sequenceTypes)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var querySettings = (from st in db.Settings where st.CompanyID == 1 select st);
                var queryTransferProcess = querySettings.Where(x => x.SettingCode == sequenceTypes).FirstOrDefault();
                return queryTransferProcess;
            }
        }

        public void UpdateStudentAttendanceStatus(long? referenceID, int? fromLoginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studLeaveApplication = dbContext.StudentLeaveApplications.FirstOrDefault(x => x.StudentLeaveApplicationIID == referenceID);

                var absentExcusedID = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "ATTENDANCE_STATUS_ID_ABSENT_EXCUSED");

                var absentExcusedStatusID = byte.Parse(absentExcusedID.SettingValue);

                for (DateTime aDate = studLeaveApplication.FromDate.Value.Date; aDate.Date <= studLeaveApplication.ToDate.Value.Date; aDate = aDate.AddDays(1))
                {
                    var studAttendance = dbContext.StudentAttendences.FirstOrDefault(a => a.StudentID == studLeaveApplication.StudentID && a.AttendenceDate == aDate);
                    if (studAttendance != null)
                    {
                        studAttendance.UpdatedBy = fromLoginID;
                        studAttendance.UpdatedDate = DateTime.Now;
                        studAttendance.PresentStatusID = absentExcusedStatusID;
                        dbContext.Entry(studAttendance).State = System.Data.Entity.EntityState.Modified;
                        dbContext.SaveChanges();
                    }
                }
            }
        }

        public void SaveTCworkflowAlertsNotification(long workflowID, long referenceID, byte? transferRequestStatusID, long employeeID)
        {
            var logs = GetWorkflowLogByWorkflowIDAndReferenceID(workflowID, referenceID);

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var processingStatusID = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "TRANSFER_REQUEST_STATUS_ID_PROCESSING");
                var feeDueSettledStatusID = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "TRANSFER_REQUEST_STATUS_ID_FEEDUESETTLED");
                var approveStatusID = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "TRANSFER_REQUEST_STATUS_ID_APPROVED");

                var stage1 = byte.Parse(processingStatusID.SettingValue);
                var stage2 = byte.Parse(feeDueSettledStatusID.SettingValue);
                var stage3 = byte.Parse(approveStatusID.SettingValue);

                //case 1 : statusID=2 processing then send notification alerts to feeduesettlement team
                //case 2 ; statusID=4 feeduesettled then send notification alerts to complete/approve team

                #region getting data from setting table
                var alertStatus = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "ALERTSTATUS_ID");
                var alertType = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "ALERTTYPE_ID");
                var superAdminID = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "SUPER_ADMIN_LOGIN_ID");
                var transferScreenID = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "STUDENT_TC_REQUEST_SCREENID");
                #endregion
                var screenID = int.Parse(transferScreenID.SettingValue);
                var studentDetail = dbContext.StudentTransferRequest.FirstOrDefault(x => x.StudentTransferRequestIID == referenceID);

                var title = "TC Request TC_" + studentDetail.StudentTransferRequestIID + " : ";
                var message = studentDetail.Student.AdmissionNumber + " - " + studentDetail.Student.FirstName + " " + studentDetail.Student.MiddleName + " " + studentDetail.Student.LastName;
                var employee = dbContext.Employees.FirstOrDefault(x => x.EmployeeIID == employeeID);

                if (logs != null && transferRequestStatusID == stage1)
                {
                    foreach (var log in logs)
                    {
                        var ruleList = log.WorkflowLogMapRuleMaps.Where(x => x.Value2 == stage2.ToString()).ToList();
                        for (int rindex = 0; rindex < ruleList.Count; rindex++)
                        {
                            var rule = ruleList[rindex];
                            var approverList = rule.WorkflowLogMapRuleApproverMaps?.ToList();
                            for (int index = 0; index < approverList.Count; index++)
                            {
                                var approver = approverList[index];
                                var empLoginID = approver.Employee.LoginID;

                                var entityTable = dbContext.NotificationAlerts.FirstOrDefault();

                                entityTable.AlertStatusID = int.Parse(alertStatus.SettingValue);
                                entityTable.AlertTypeID = int.Parse(alertType.SettingValue);
                                entityTable.FromLoginID = employee.LoginID;
                                entityTable.ToLoginID = empLoginID != null ? empLoginID : null;
                                entityTable.Message = title + " " + message;
                                entityTable.ReferenceID = referenceID;
                                entityTable.NotificationDate = DateTime.Now;
                                entityTable.CreatedDate = DateTime.Now;
                                entityTable.CreatedBy = employeeID;
                                entityTable.IsITCordinator = true;
                                entityTable.ReferenceScreenID = screenID;

                                dbContext.Entry(entityTable).State = System.Data.Entity.EntityState.Added;
                            }
                            dbContext.SaveChanges();
                        }
                    }
                }
                else if (logs != null && transferRequestStatusID == stage2)
                {
                    foreach (var log in logs)
                    {
                        var ruleList = log.WorkflowLogMapRuleMaps.Where(x => x.Value2 == stage3.ToString()).ToList();
                        for (int rindex = 0; rindex < ruleList.Count; rindex++)
                        {
                            var rule = ruleList[rindex];
                            var approverList = rule.WorkflowLogMapRuleApproverMaps?.ToList();
                            for (int index = 0; index < approverList.Count; index++)
                            {
                                var approver = approverList[index];
                                var empLoginID = approver.Employee.LoginID;

                                var entityTable = dbContext.NotificationAlerts.FirstOrDefault();

                                entityTable.AlertStatusID = int.Parse(alertStatus.SettingValue);
                                entityTable.AlertTypeID = int.Parse(alertType.SettingValue);
                                entityTable.FromLoginID = employee.LoginID;
                                entityTable.ToLoginID = empLoginID != null ? empLoginID : null;
                                entityTable.Message = title + " " + message;
                                entityTable.ReferenceID = referenceID;
                                entityTable.NotificationDate = DateTime.Now;
                                entityTable.CreatedDate = DateTime.Now;
                                entityTable.CreatedBy = employeeID;
                                entityTable.IsITCordinator = true;
                                entityTable.ReferenceScreenID = screenID;

                                dbContext.Entry(entityTable).State = System.Data.Entity.EntityState.Added;
                            }
                            dbContext.SaveChanges();
                        }
                    }
                }
                //notification send to parent when application approved
                if (studentDetail.TransferRequestStatusID == stage3)
                {
                    var entityforParent = dbContext.NotificationAlerts.FirstOrDefault();
                    var stud = dbContext.Students.FirstOrDefault(s => s.StudentIID == studentDetail.StudentID);

                    entityforParent.AlertStatusID = int.Parse(alertStatus.SettingValue);
                    entityforParent.AlertTypeID = int.Parse(alertType.SettingValue);
                    entityforParent.FromLoginID = employee.LoginID;
                    entityforParent.ToLoginID = stud != null ? stud.Parent.LoginID : null;
                    entityforParent.Message = title + " " + message;
                    entityforParent.ReferenceID = referenceID;
                    entityforParent.NotificationDate = DateTime.Now;
                    entityforParent.CreatedDate = DateTime.Now;
                    entityforParent.CreatedBy = employeeID;
                    entityforParent.IsITCordinator = true;
                    entityforParent.ReferenceScreenID = screenID;

                    dbContext.Entry(entityforParent).State = System.Data.Entity.EntityState.Added;
                    dbContext.SaveChanges();
                }
            }
        }

        public void LeaveAlertsNotificationSendtoParent(long? referenceID, int? fromLoginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                #region getting data from setting table
                var alertStatus = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "ALERTSTATUS_ID");
                var alertType = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "ALERTTYPE_ID");

                var viewscreenID = dbContext.Settings.FirstOrDefault(x => x.SettingCode == "STUDENT_LEAVEAPPLICATION_SCREENID");
                var screenID = int.Parse(viewscreenID.SettingValue);
                #endregion
                //Send Parent portal notification alert when student leave application is Approved/rejected
                var studentDetail = dbContext.StudentLeaveApplications.FirstOrDefault(x => x.StudentLeaveApplicationIID == referenceID);
                var message = studentDetail.Student.AdmissionNumber + " - " + studentDetail.Student.FirstName + " " + studentDetail.Student.MiddleName + " " + studentDetail.Student.LastName + "'s leave Application : LV_" + studentDetail.StudentLeaveApplicationIID + " is " + studentDetail.LeaveStatus.StatusName;
                var parent = dbContext.Parents.FirstOrDefault(p => p.ParentIID == studentDetail.Student.ParentID);

                var entity = dbContext.NotificationAlerts.FirstOrDefault();
                entity.AlertStatusID = int.Parse(alertStatus.SettingValue);
                entity.AlertTypeID = int.Parse(alertType.SettingValue);
                entity.FromLoginID = fromLoginID != null ? fromLoginID : 2;
                entity.ToLoginID = parent != null ? parent.LoginID : null;
                entity.Message = message;
                entity.ReferenceID = referenceID;
                entity.NotificationDate = DateTime.Now;
                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = null;
                entity.IsITCordinator = false;
                entity.ReferenceScreenID = screenID;

                dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                dbContext.SaveChanges();
            }
        }

        public bool UpdateApplicationFormStatus(long workflowRuleID, string rule, int statusID, long? referenceID, long employeeID)
        {
            try
            {
                using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
                {
                    //For change Application form status based on work flow
                    if (rule != null)
                    {
                        var employee = dbContext.Employees.FirstOrDefault(e => e.EmployeeIID == employeeID);

                        var applicationStatuses = dbContext.ApplicationStatuses.Where(a => a.ApplicationStatusID != 0).ToList();

                        var formFields = dbContext.FormFields.Where(f => f.FieldName.ToLower().Contains("status") || f.FieldName.ToLower().Contains("update")).ToList();

                        foreach (var field in formFields)
                        {
                            var formValue = dbContext.FormValues.FirstOrDefault(v => v.ReferenceID == referenceID && v.FormFieldID == field.FormFieldID);

                            if (formValue != null)
                            {
                                var status = applicationStatuses.Find(s => s.Description.Trim().ToUpper() == rule.Trim().ToUpper());

                                if (status != null)
                                {
                                    if (field.FieldName.ToLower().Contains("statusid"))
                                    {
                                        formValue.FormFieldValue = status.ApplicationStatusID.ToString();
                                    }
                                    if (field.FieldName.ToLower().Contains("status") && !field.FieldName.ToLower().Contains("id"))
                                    {
                                        formValue.FormFieldValue = status.Description;
                                    }
                                }
                                if (field.FieldName.ToLower().Contains("update") && field.FieldName.ToLower().Contains("date"))
                                {
                                    formValue.FormFieldValue = DateTime.Now.ToString("o");
                                }
                                if (field.FieldName.ToLower().Contains("update") && !field.FieldName.ToLower().Contains("date"))
                                {
                                    formValue.FormFieldValue = employee?.LoginID?.ToString();
                                }

                                dbContext.Entry(formValue).State = EntityState.Modified;
                                dbContext.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to update the status", TrackingCode.ERP);
                return false;
            }

            return true;
        }


        public bool UpdateStockQuantityByWorkflow(long workflowRuleID, string rule, int statusID, long? referenceID, long employeeID)
        {
            try
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    //get productSKUIDs from submitted TransHead STCKADJ table by headID
                    var invTransactions = dbContext.InvetoryTransactions.Where(h => h.HeadID == referenceID).ToList();

                    foreach (var inv in invTransactions)
                    {
                        if (inv.Quantity != null)
                        {
                            var productInv = dbContext.ProductInventories.Where(p => p.ProductSKUMapID == inv.ProductSKUMapID && p.BranchID == inv.BranchID).ToList().OrderBy(a => a.Batch);

                            //when the quantity is less than the current table quantity
                            if (inv.Quantity < productInv.Sum(x => x.Quantity))
                            {
                                var diffQty = productInv.Sum(x => x.Quantity) - inv.Quantity;

                                foreach (var dat in productInv)
                                {
                                    var qtyToDecrse = dat.Quantity < diffQty ? dat.Quantity : diffQty;

                                    dat.Quantity = dat.Quantity - qtyToDecrse;

                                    dat.UpdatedDate = DateTime.Now;
                                    dat.UpdatedBy = (int?)employeeID;
                                    dat.HeadID = referenceID;

                                    diffQty = diffQty - qtyToDecrse;

                                    dbContext.Entry(dat).State = EntityState.Modified;
                                    dbContext.SaveChanges();
                                }
                            }
                            else
                            {
                                var toQuantity = inv.Quantity - productInv.Sum(x => x.Quantity);

                                var addEntity = new ProductInventory()
                                {
                                    ProductSKUMapID = (long)inv.ProductSKUMapID,
                                    Batch = productInv.ToList().LastOrDefault() != null ? productInv.ToList().LastOrDefault().Batch + 1 : 1,
                                    CompanyID = 1,
                                    BranchID = inv.BranchID,
                                    Quantity = toQuantity,
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = (int?)employeeID,
                                    HeadID = inv.HeadID,
                                };

                                dbContext.Entry(addEntity).State = EntityState.Added;
                                dbContext.SaveChanges();
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to update the status", TrackingCode.ERP);
                return false;
            }

            return true;
        }

    }
}