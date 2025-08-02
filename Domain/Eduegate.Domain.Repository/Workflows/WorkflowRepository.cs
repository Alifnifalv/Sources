using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Models.Workflows;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Logs;
using Eduegate.Services.Contracts.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Eduegate.Domain.Repository.Workflows
{
    public class WorkflowRepository
    {
        public Workflow GetWorkflowByDocumentType(long documentTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var workflow = dbContext.DocumentTypes
                    .Include(a => a.Workflow).ThenInclude(a => a.WorkflowRules).ThenInclude(a => a.Condition)

                    .Include(a => a.Workflow).ThenInclude(a => a.WorkflowRules).ThenInclude(a => a.WorkflowRuleConditions).ThenInclude(a => a.Condition)

                    .Include(a => a.Workflow).ThenInclude(a => a.WorkflowRules).ThenInclude(a => a.WorkflowRuleConditions).ThenInclude(a => a.WorkflowRuleApprovers)

                    //.Include(a => a.Workflow).ThenInclude(a => a.WorkflowRules).ThenInclude(a => a.WorkflowRuleConditions).ThenInclude(a => a.WorkflowRule)
                    .Include(a => a.Workflow).ThenInclude(a => a.WorkflowFiled)
                    .Where(a => a.DocumentTypeID == documentTypeID)
                    .AsNoTracking()
                    .FirstOrDefault();

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
                    entity = dbContext.WorkflowTypes
                        .AsNoTracking()
                        .ToList();
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
                    entity = dbContext.WorkflowFileds.Where(a => a.WorkflowTypeID == workflowType)
                        .AsNoTracking()
                        .ToList();
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

                        .Include(a => a.WorkflowTransactionHeadRuleMaps)
                        .ThenInclude(ab => ab.WorkflowTransactionRuleApproverMaps)
                        .ThenInclude(ab => ab.Employee)
                        .ThenInclude(ab => ab.Login)

                        .Include(a => a.WorkflowTransactionHeadRuleMaps)
                        .ThenInclude(ab => ab.WorkflowStatus)

                        .Include(a => a.WorkflowTransactionHeadRuleMaps)
                        .ThenInclude(ab => ab.WorkflowCondition)

                        .Where(a => a.TransactionHeadID == headID)
                        .AsNoTracking()
                        .ToList();
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
                            .ThenInclude(i => i.WorkflowRuleConditions)
                                .ThenInclude(i => i.Condition)

                                .Include(j => j.WorkflowRules)
                                .ThenInclude(k => k.WorkflowRuleConditions)
                                .ThenInclude(k => k.WorkflowRuleApprovers)
                                .ThenInclude(k => k.Employee)

                        .Include(a => a.WorkflowFiled)
                        .Include(a => a.WorkflowType)
                        .Where(a => a.WorkflowIID == workflowID)
                        .AsNoTracking()
                        .FirstOrDefault();

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

                        .Include(a => a.WorkflowLogMapRuleMaps)
                        .ThenInclude(a => a.WorkflowLogMapRuleApproverMaps)
                        .ThenInclude(a => a.Employee)
                        .ThenInclude(a => a.Login)
                        .Where(a => (workflowID == 0 || a.WorkflowID.Value == workflowID) && a.ReferenceID == referenceID)
                        .AsNoTracking()
                        .ToList();
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
                    entity = dbContext.Workflows.Where(a => a.WorkflowIID == workflowID).AsNoTracking().FirstOrDefault();
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
                    entity = dbContext.Workflows
                        .AsNoTracking()
                        .ToList();
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
                    entity = dbContext.Workflows.Where(a => a.WorkflowTypeID == workflowTypeID)
                        .AsNoTracking()
                        .ToList();
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
                    entity = dbContext.WorkflowConditions.Where(a => a.ConditionType == conditionType)
                        .AsNoTracking()
                        .ToList();
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
                        .Where(a => a.WorkflowTransactionHeadRuleMapIID == roleMapID)
                        .AsNoTracking()
                        .FirstOrDefault();

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

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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


                        var studentAssignment = dbContext.Assignment.Where(X => X.AssignmentIID == referenceID)
                                             .AsNoTracking()
                                             .FirstOrDefault();

                        var applicationStatuses = dbContext.AssignmentStatus.Where(X => X.AssignmentStatusID != 0)
                                             .AsNoTracking()
                                             .ToList();


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

                            dbContext.Entry(studentAssignment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

                        var application = dbContext.MarkRegister
                                 .AsNoTracking()
                                 .FirstOrDefault(a => a.MarkRegisterIID == referenceID);

                        var applicationStatuses = dbContext.MarkEntryStatus.Where(a => a.MarkEntryStatusID != 0)
                                .AsNoTracking()
                                .ToList();

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

                            dbContext.Entry(application).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
        public bool UpdateLessonPlanStatus(long workflowRuleID, string rule, int statusID, long? referenceID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {

                    //For Change Update Lesson Plan Status Based on Work Flow

                    if (rule != null)
                    {
                        var application = dbContext.LessonPlan.Where(l => l.LessonPlanIID == referenceID)
                            .AsNoTracking()
                            .FirstOrDefault();

                        if (statusID == 0)
                        {
                            var settingData = new Domain.Setting.SettingBL(null).GetSettingValue<string>("Lesson_Rejected_StatusID");
                            var rejectedStsID = byte.Parse(settingData);
                            statusID = rejectedStsID;
                        }

                        application.LessonPlanStatusID = (byte?)statusID;
                        application.UpdatedDate = DateTime.Now;

                        dbContext.Entry(application).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

        public bool UpdateApplicationStatus(long workflowRuleID, string rule,int statusID, long? referenceID,long employeeID)
        {
            try
            {
                using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
                {
                    //For Change Student Application Status Based on Work Flow
                    if (rule != null)
                    {
                        var application = dbContext.StudentApplications.FirstOrDefault(a => a.ApplicationIID == referenceID);

                        if (application != null)
                        {
                            var empLoginID = dbContext.Employees.AsNoTracking().FirstOrDefault(x => x.EmployeeIID == employeeID).LoginID;

                            application.ApplicationStatusID = (byte?)statusID;
                            application.UpdatedDate = DateTime.Now;
                            application.UpdatedBy = (int?)empLoginID;

                            dbContext.Entry(application).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            dbContext.SaveChanges();

                            string Message = @"<p style='font-size:0.9em;' font-family:'Times New Roman;' align='left'>Congratulations!! <br /> Your Candidate Application taken into Acceptance.we will contact you soon...<br /><br />Regards,<br />Pearl School</p><br/><br/><p style='font-size:0.7rem;'<b>Please Note: </b>do not reply to this email as it is a computer generated email</p>";

                            if (Message != "")
                            {
                                string emailBody = @"
                                       
                                        <style='font-size:0.9rem;'>" + Message + @"</style><br />";

                                string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(null).PopulateBody(" ", emailBody);

                                var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

                                string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");
                                var mailParameters = new Dictionary<string, string>();

                                if (hostDet.ToLower() == "live")
                                {
                                    if (!string.IsNullOrEmpty(application.EmailID))
                                    {
                                        new Eduegate.Domain.Notification.EmailNotificationBL(null).SendMail(application.EmailID, "Admission Status", mailMessage, EmailTypes.WorkFlowCreation, mailParameters);

                                    }
                                }
                                else
                                {
                                    new Eduegate.Domain.Notification.EmailNotificationBL(null).SendMail(defaultMail, "Admission Status", mailMessage, EmailTypes.WorkFlowCreation, mailParameters);
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
            try
            {
                using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
                {
                    var approvedStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("LEAVE_APPROVED_STATUS_ID");
                    var rejectedStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("LEAVE_REJECT_STATUS_ID");

                    var leaveApprovedStatusID = byte.Parse(approvedStatusID);
                    var leaveRejectedStatusID = byte.Parse(rejectedStatusID);

                    var workflowApprovedStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("WORKFLOW_APPROVED_STATUS_ID");
                    var approveStatusID = byte.Parse(workflowApprovedStatusID);

                    var workflowRejectedStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("WORKFLOW_REJECTED_STATUS_ID");
                    var rejectStatusID = byte.Parse(workflowRejectedStatusID);

                    var studLeaveList = dbContext.StudentLeaveApplications.AsNoTracking().FirstOrDefault(x => x.StudentLeaveApplicationIID == referenceID);
                    var employee = dbContext.Employees.AsNoTracking().FirstOrDefault(x => x.EmployeeIID == employeeID);

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
                    dbContext.Entry(studLeaveList).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();

                    if (statusID == approveStatusID || statusID == rejectStatusID)//approve or reject case
                    {
                        LeaveAlertsNotificationSendtoParent(referenceID, studLeaveList.UpdatedBy);
                    }
                }

                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var workflowApprovedStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("WORKFLOW_APPROVED_STATUS_ID");
                    var approveStatusID = byte.Parse(workflowApprovedStatusID);

                    var workflowRejectedStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("WORKFLOW_REJECTED_STATUS_ID");
                    var rejectStatusID = byte.Parse(workflowRejectedStatusID);

                    var wrkFlowLog = dbContext.WorkflowLogMaps.Where(s => s.ReferenceID == referenceID).AsNoTracking().FirstOrDefault();
                    var wrkflowlogRuleMap = wrkFlowLog != null ? dbContext.WorkflowLogMapRuleMaps.AsNoTracking().FirstOrDefault(t => t.WorkflowLogMapID == wrkFlowLog.WorkflowLogMapIID) : null;

                    if (rule != null && statusID == rejectStatusID)//rejected
                    {
                        wrkflowlogRuleMap.WorkflowStatusID = rejectStatusID;
                    }

                    else
                    {
                        wrkflowlogRuleMap.WorkflowStatusID = approveStatusID;
                    }
                    dbContext.Entry(wrkflowlogRuleMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

        public bool UpdateTransferRequestStatus(long workflowID,long workflowRuleID, string rule, int statusID, long? referenceID, long employeeID,long logMapRuleMapID)
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var studTransferData = dbContext.StudentTransferRequest.FirstOrDefault(x => x.StudentTransferRequestIID == referenceID);
                var empLoginID = dbContext.Employees.AsNoTracking().FirstOrDefault(x => x.EmployeeIID == employeeID)?.LoginID;

                studTransferData.TransferRequestStatusID = (byte?)statusID;
                studTransferData.UpdatedBy = (int?)empLoginID;
                studTransferData.UpdatedDate = DateTime.Now;

                dbContext.Entry(studTransferData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();

                SaveTCworkflowAlertsNotification(workflowID, (long)referenceID,employeeID,statusID, logMapRuleMapID);

                var processingStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUD_TRANSFER_STATUS_PROCESSING");
                if(int.Parse(processingStatusID) == statusID)
                {
                    TransferFeeDueGeneration(referenceID, empLoginID);
                }
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
                        .Where(a => a.WorkflowLogMapRuleMapIID == roleMapID)
                        .FirstOrDefault();

                    dbContext.WorkflowLogMapRuleMaps.Add(entity);

                    foreach (var approver in entity.WorkflowLogMapRuleApproverMaps)
                    {
                        if (approver.EmployeeID == employeeID)
                        {
                            var emp = dbContext.Employees.Where(x => x.EmployeeIID == employeeID).AsNoTracking().FirstOrDefault();
                            entity.Description = @" - <b>" + entity.Value1 + "</b>" + " by " + emp.EmployeeCode + " - " + emp.FirstName + " " + emp.MiddleName + " " + emp.LastName;
                            entity.WorkflowStatusID = (byte?)statusID;
                            entity.IsFlowCompleted = true;
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

                    //var workFlowRuleApproverRepository = new EntityRepository<WorkflowRuleApprover, dbEduegateERPContext>(dbContext);
                    //var ruleApprover = workFlowRuleApproverRepository.Get(a => a.EmployeeID == employeeID && a.WorkflowRuleCondition.WorkflowRule.WorkflowID == workflowID).FirstOrDefault();

                    //if (ruleApprover != null)
                    //{
                    //    var workFlowRuleConditionRepository = new EntityRepository<WorkflowRuleCondition, dbEduegateERPContext>(dbContext);

                    //    var ruleCondition = workFlowRuleConditionRepository.Get(a => a.WorkflowRuleConditionIID == ruleApprover.WorkflowRuleConditionID).FirstOrDefault();

                    //    if (ruleCondition != null)
                    //    {
                    //        var workFlowRuleRepository = new EntityRepository<WorkflowRule, dbEduegateERPContext>(dbContext);
                    //        var rule = workFlowRuleRepository.Get(a => a.WorkflowRuleIID == ruleCondition.WorkflowRuleID).FirstOrDefault();

                    //        if (rule != null)
                    //        {
                    //            return rule;
                    //        }

                    //    }
                    //}
                    #endregion old code end

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                         .Include(a => a.WorkflowLogMap)
                         .Where(a => a.WorkflowLogMapRuleMapIID == roleMapID)
                         .FirstOrDefault();

                    dbContext.WorkflowLogMapRuleMaps.Add(entity);

                    var emp = dbContext.Employees.Where(x => x.EmployeeIID == employeeID).AsNoTracking().FirstOrDefault();
                    entity.Description = @" - <b>Rejected by </b>" + emp.EmployeeCode + " - " + emp.FirstName + " " + emp.MiddleName + " " + emp.LastName;
                    entity.WorkflowStatusID = (byte?)statusID;
                    entity.IsFlowCompleted = true;
                    entity.WorkflowLogMap.WorkflowStatusID = (byte?)statusID;

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var roleMap = dbContext.WorkflowLogMapRuleMaps.Where(r => r.WorkflowLogMapRuleMapIID == roleMapID).AsNoTracking().FirstOrDefault();
                var getWorkFlowRule = dbContext.WorkflowRules.Where(x => x.WorkflowRuleIID == roleMap.WorkflowRuleID).AsNoTracking().FirstOrDefault();

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
                        .Where(a => a.WorkflowIID == workflowID)
                        .AsNoTracking()
                        .FirstOrDefault();
                    return dbContext.WorkflowStatuses.Where(a => a.WorkflowTypeID == workflow.WorkflowTypeID)
                        .AsNoTracking()
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
                return null;
            }
        }

        public Entity.Models.Setting GetSettingData(string sequenceTypes)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var querySettings = (from st in db.Settings where st.CompanyID == 1 select st);
                var queryTransferProcess = querySettings.Where(x => x.SettingCode == sequenceTypes)
                    .AsNoTracking()
                    .FirstOrDefault();
                return queryTransferProcess;
            }
        }

        public void UpdateStudentAttendanceStatus(long? referenceID, int? fromLoginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studLeaveApplication = dbContext.StudentLeaveApplications.Where(x => x.StudentLeaveApplicationIID == referenceID).AsNoTracking().FirstOrDefault();

                var absentExcusedID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ATTENDANCE_STATUS_ID_ABSENT_EXCUSED");

                var absentExcusedStatusID = byte.Parse(absentExcusedID);

                for (DateTime aDate = studLeaveApplication.FromDate.Value.Date; aDate.Date <= studLeaveApplication.ToDate.Value.Date; aDate = aDate.AddDays(1))
                {
                    var studAttendance = dbContext.StudentAttendences.FirstOrDefault(a => a.StudentID == studLeaveApplication.StudentID && a.AttendenceDate == aDate);
                    if (studAttendance != null)
                    {
                        studAttendance.UpdatedBy = fromLoginID;
                        studAttendance.UpdatedDate = DateTime.Now;
                        studAttendance.PresentStatusID = absentExcusedStatusID;
                        dbContext.Entry(studAttendance).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        dbContext.SaveChanges();
                    }
                }
            }
        }

        //Send notification to next approvers and parent
        public void SaveTCworkflowAlertsNotification(long workflowID, long referenceID, long employeeID, int statusID, long logMapRuleMapID)
        {
            var logs = GetWorkflowLogByWorkflowIDAndReferenceID(workflowID, referenceID);

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var processingStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSFER_REQUEST_STATUS_ID_PROCESSING");
                var feeDueSettledStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSFER_REQUEST_STATUS_ID_FEEDUESETTLED");
                var approveStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSFER_REQUEST_STATUS_ID_APPROVED");
                bool IsITcordinator = true;
                var toLoginList = new List<long?>();

                var studentDetail = dbContext.StudentTransferRequest.Where(x => x.StudentTransferRequestIID == referenceID)
                                    .Include(i => i.Student)
                                    .AsNoTracking().FirstOrDefault();

                var title = "TC Request TC_" + studentDetail.StudentTransferRequestIID + " : ";
                var message = studentDetail.Student.AdmissionNumber + " - " + studentDetail.Student.FirstName + " " + studentDetail.Student.MiddleName + " " + studentDetail.Student.LastName;

                //notification send to approvers and parents

                //status change from requested to processing
                if (statusID == int.Parse(processingStatusID)) //Send to feedueSetteled approvers
                {
                    foreach (var log in logs)
                    {
                        var ruleList = log.WorkflowLogMapRuleMaps.Where(x => x.Value2 == feeDueSettledStatusID).ToList();
                        for (int rindex = 0; rindex < ruleList.Count; rindex++)
                        {
                            var rule = ruleList[rindex];
                            toLoginList = rule.WorkflowLogMapRuleApproverMaps?.Select(s => s.Employee?.LoginID).ToList();
                        }
                    }
                }
                //status change from processing to feedueSetteled
                else if (statusID == int.Parse(feeDueSettledStatusID)) //Send to approved/completed approvers
                {
                    foreach (var log in logs)
                    {
                        var ruleList = log.WorkflowLogMapRuleMaps.Where(x => x.Value2 == approveStatusID).ToList();
                        for (int rindex = 0; rindex < ruleList.Count; rindex++)
                        {
                            var rule = ruleList[rindex];
                            toLoginList = rule.WorkflowLogMapRuleApproverMaps?.Select(s => s.Employee?.LoginID).ToList();
                        }
                    }
                }
                //status change from feedueSetteled to approved/completed
                else if (statusID == int.Parse(approveStatusID)) //Send to parent
                {
                    toLoginList = dbContext.Students.Where(s => s.StudentIID == studentDetail.StudentID).Include(s => s.Parent)
                                  .AsNoTracking().Select(p => p.Parent.LoginID).ToList();
                    IsITcordinator = false;
                }

                #region getting data from setting table
                var alertStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ALERTSTATUS_ID");
                var alertType = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ALERTTYPE_ID");
                var superAdminID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("SUPER_ADMIN_LOGIN_ID");
                var transferScreenID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_TC_REQUEST_SCREENID");
                #endregion

                var employee = dbContext.Employees.AsNoTracking().FirstOrDefault(x => x.EmployeeIID == employeeID);
                var screenID = int.Parse(transferScreenID);

                var toEntityList = new List<Eduegate.Domain.Entity.School.Models.NotificationAlert>();

                //Send Notifications 
                foreach (var toLogin in toLoginList)
                {
                    var newNotification = new Eduegate.Domain.Entity.School.Models.NotificationAlert()
                    {
                        AlertStatusID = int.Parse(alertStatus),
                        AlertTypeID = int.Parse(alertType),
                        FromLoginID = employee.LoginID,
                        ToLoginID = toLogin,
                        Message = title + " " + message,
                        ReferenceID = referenceID,
                        NotificationDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        CreatedBy = employeeID,
                        IsITCordinator = IsITcordinator,
                        ReferenceScreenID = screenID
                    };
                    toEntityList.Add(newNotification);
                    dbContext.NotificationAlerts.Add(newNotification);
                }
                dbContext.SaveChanges();
            }
        }

        public void LeaveAlertsNotificationSendtoParent(long? referenceID, int? fromLoginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                #region getting data from setting table
                var alertStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ALERTSTATUS_ID");
                var alertType = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ALERTTYPE_ID");

                var viewscreenID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_LEAVEAPPLICATION_SCREENID");
                var screenID = int.Parse(viewscreenID);
                #endregion
                //Send Parent portal notification alert when student leave application is Approved/rejected
                var studentDetail = dbContext.StudentLeaveApplications.Where(x => x.StudentLeaveApplicationIID == referenceID)
                    .Include(i => i.Student).ThenInclude(i => i.Parent)
                    .Include(i => i.LeaveStatus)
                    .AsNoTracking().FirstOrDefault();

                var message = studentDetail.Student.AdmissionNumber + " - " + studentDetail.Student.FirstName + " " + studentDetail.Student.MiddleName + " " + studentDetail.Student.LastName + "'s leave Application : LV_" + studentDetail.StudentLeaveApplicationIID + " is " + studentDetail.LeaveStatus.StatusName;
                var parent = dbContext.Parents.AsNoTracking().FirstOrDefault(p => p.ParentIID == studentDetail.Student.ParentID);

                var entity = dbContext.NotificationAlerts.AsNoTracking().FirstOrDefault();
                entity.AlertStatusID = int.Parse(alertStatus);
                entity.AlertTypeID = int.Parse(alertType);
                entity.FromLoginID = fromLoginID != null ? fromLoginID : 2;
                entity.ToLoginID = parent != null ? parent.LoginID : null;
                entity.Message = message;
                entity.ReferenceID = referenceID;
                entity.NotificationDate = DateTime.Now;
                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = null;
                entity.IsITCordinator = false;
                entity.ReferenceScreenID = screenID;

                dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
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
                        var employee = dbContext.Employees.AsNoTracking().FirstOrDefault(e => e.EmployeeIID == employeeID);

                        var applicationStatuses = dbContext.ApplicationStatuses.Where(a => a.ApplicationStatusID != 0).AsNoTracking().ToList();

                        var formFields = dbContext.FormFields.Where(f => f.FieldName.ToLower().Contains("status") || f.FieldName.ToLower().Contains("update")).AsNoTracking().ToList();

                        foreach (var field in formFields)
                        {
                            var formValue = dbContext.FormValues.AsNoTracking().FirstOrDefault(v => v.ReferenceID == referenceID && v.FormFieldID == field.FormFieldID);

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

                                dbContext.Entry(formValue).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

        public bool UpdateStockQuantityByWorkflow(long? workflowID, long workflowRuleID, string rule, int statusID, long? referenceID, long employeeID)
        {
            try
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    //get productSKUIDs from submitted TransHead STCKADJ table by headID
                    var invTransactions = dbContext.InvetoryTransactions.Where(h => h.HeadID == referenceID).AsNoTracking().ToList();

                    foreach (var inv in invTransactions)
                    {
                        if (inv.Quantity != null)
                        {
                            var productInv = dbContext.ProductInventories.Where(p => p.ProductSKUMapID == inv.ProductSKUMapID && p.BranchID == inv.BranchID).OrderBy(a => a.Batch).ToList();

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

                                    dbContext.Entry(dat).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    dbContext.SaveChanges();
                                }
                            }
                            else
                            {
                                using (var dbContext1 = new dbEduegateERPContext())
                                {
                                    var toQuantity = inv.Quantity - productInv.Sum(x => x.Quantity);

                                    var lastBatch = productInv.OrderByDescending(x => x.Batch).FirstOrDefault();

                                    var addEntity = new ProductInventory()
                                    {
                                        ProductSKUMapID = (long)inv.ProductSKUMapID,
                                        Batch = (lastBatch != null ? lastBatch.Batch : 0) + 1,
                                        CompanyID = 1,
                                        BranchID = inv.BranchID,
                                        Quantity = toQuantity,
                                        CreatedDate = DateTime.Now,
                                        CreatedBy = (int?)employeeID,
                                        HeadID = inv.HeadID,
                                    };

                                    dbContext1.Entry(addEntity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                    dbContext1.SaveChanges();
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

            //update [workflow].[WorkflowLogMaps] table workflowStatusID as Approved
            using (var dbContext2 = new dbEduegateERPContext())
            {
                var workflowLog = dbContext2.WorkflowLogMaps.FirstOrDefault(w => w.WorkflowID == workflowID && w.ReferenceID == referenceID);
                if (workflowLog != null)
                {
                    workflowLog.WorkflowStatusID = (byte?)statusID;
                    dbContext2.Entry(workflowLog).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext2.SaveChanges();
                }
            }

            return true;
        }

        public bool UpdateTransactionDocumentStatusID(long? workflowID, long workflowRuleID, string rule, int statusID, long? referenceID, long employeeID)
        {
            var resultVal = false;

            try
            {
                var approveTransactionStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("APPROVED_TRANSACTION_STATUS_ID");
                var aprvTransStatusID = byte.Parse(approveTransactionStatusID);

                using (var dbContext = new dbEduegateERPContext())
                {
                    var transHead = dbContext.TransactionHeads.Where(x => x.HeadIID == referenceID)
                        .Include(x => x.Branch)
                        .Include(x => x.Supplier)
                        .AsNoTracking().FirstOrDefault();

                    if (transHead != null)
                    {
                        var approveDocStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSACTION_DOC_STS_ID_APPROVED");
                        var approvedStsID = byte.Parse(approveDocStatusID);   
                    

                        if(statusID == aprvTransStatusID)
                        {
                            transHead.DocumentStatusID = (long?)approvedStsID;
                        }

                        transHead.TransactionStatusID = (byte?)statusID;
                        transHead.UpdatedBy = (int?)employeeID;
                        transHead.UpdatedDate = DateTime.Now;
                    }
                    dbContext.Entry(transHead).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();

                    var quotationToPOWorkFlowID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("QUOTATION_TO_PO_WORKFLOW_ID");

                    if(workflowID == long.Parse(quotationToPOWorkFlowID) && statusID == aprvTransStatusID)
                    {
                        var alertstatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULTALERTSTATUSID");

                        var notification = new List<Eduegate.Domain.Entity.Models.NotificationAlert>();

                        notification.Add(new Eduegate.Domain.Entity.Models.NotificationAlert()
                        {
                            NotificationAlertIID = 0,
                            Message = "You have a Purchase Order : " + transHead.TransactionNo + " from " + transHead.Branch.BranchName,
                            FromLoginID = transHead.UpdatedBy.HasValue ? transHead.UpdatedBy : transHead.CreatedBy,
                            ToLoginID = transHead.Supplier.LoginID,
                            NotificationDate = DateTime.Now,
                            CreatedDate = DateTime.Now,
                            AlertStatusID = int.Parse(alertstatus),
                        });

                        new NotificationRepository().SaveAlerts(notification);
                    }

                    resultVal = true;
                }
            }
            catch (Exception ex)
            {
                resultVal = false;
            }

            return resultVal;

        }

        public bool UpdateLibraryBookTransactionStatusID(long? workflowID, long workflowRuleID, string rule, int statusID, long? referenceID, long? employeeID)
        {
            var resultVal = false;

            try
            {
                using (var dbContext = new dbEduegateSchoolContext())
                {
                    var libraryTrans = dbContext.LibraryTransactions.Where(x => x.LibraryTransactionIID == referenceID).AsNoTracking().FirstOrDefault();

                    if (libraryTrans != null)
                    {
                        libraryTrans.LibraryTransactionTypeID = (byte?)statusID;
                        libraryTrans.UpdatedBy = (int?)employeeID;
                        libraryTrans.UpdatedDate = DateTime.Now;
                    }
                    dbContext.Entry(libraryTrans).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();

                    resultVal = true;
                }
            }
            catch (Exception ex)
            {
                resultVal = false;
            }

            return resultVal;

        }

        public bool? TransferFeeDueGeneration(long? referenceID,long? loginID)
        {
            var result = false;
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var tcFeeMasterID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSFER_FEE_ID");

                var transReq = dbContext.StudentTransferRequest.FirstOrDefault(a => a.StudentTransferRequestIID == referenceID);

                var studDetails = dbContext.Students.Where(a => a.StudentIID == transReq.StudentID).AsNoTracking().FirstOrDefault();

                int feeMasterID = int.Parse(tcFeeMasterID);

                // Check for duplicates in the StudentFeeDues  
                bool hasDuplicate = dbContext.StudentFeeDues
                    .Include(x => x.FeeDueFeeTypeMaps)
                    .Any(x => x.StudentId == studDetails.StudentIID && x.AcadamicYearID == studDetails.AcademicYearID &&
                               x.FeeDueFeeTypeMaps.Any(y => y.FeeMasterID == feeMasterID));

                if (hasDuplicate)
                {
                    result = false;
                }
                else
                {
                    var feeDues = dbContext.StudentFeeDues.OrderByDescending(x => x.StudentFeeDueIID).FirstOrDefault();

                    var feeMaster = dbContext.FeeMasters.FirstOrDefault(x => x.FeeMasterID == feeMasterID);

                    string currentInvoiceNumber = feeDues.InvoiceNo;
                    string prefix = "INV-";
                    string newInvoiceNumber = null;

                    // Extract the numeric part of the invoice number  
                    string numericPart = currentInvoiceNumber.Substring(prefix.Length);
                    if (int.TryParse(numericPart, out int numericValue))
                    {
                        // Increment the numeric value  
                        numericValue++;

                        // Format the new invoice number (pad with leading zeros if necessary)  
                        newInvoiceNumber = $"{prefix}{numericValue:D7}"; // D7 formats the number to 7 digits  
                    }

                    if (newInvoiceNumber != null)
                    {
                        var entity = new Eduegate.Domain.Entity.School.Models.StudentFeeDue()
                        {
                            StudentId = studDetails.StudentIID,
                            ClassId = studDetails?.ClassID,
                            SectionID = studDetails?.SectionID,
                            AcadamicYearID = studDetails?.AcademicYearID,
                            CreatedDate = DateTime.Now,
                            CreatedBy = (int?)loginID,
                            InvoiceDate = DateTime.Now,
                            DueDate = DateTime.Now,
                            InvoiceNo = newInvoiceNumber,
                            CollectionStatus = false,
                            IsAccountPost = false,
                            SchoolID = studDetails?.SchoolID,
                        };

                        if (feeMaster != null)
                        {
                            entity.FeeDueFeeTypeMaps.Add(new Eduegate.Domain.Entity.School.Models.FeeDueFeeTypeMap()
                            {
                                StudentFeeDueID = entity.StudentFeeDueIID,
                                Amount = feeMaster.Amount,
                                CreatedDate = DateTime.Now,
                                CreatedBy = (int?)loginID,
                                Status = false,
                                FeeMasterID = int.Parse(tcFeeMasterID),
                            });
                        }

                        dbContext.StudentFeeDues.Add(entity);
                        dbContext.SaveChanges();
                    }

                    result = true;
                }

            }

            return result;
        }

    }
}