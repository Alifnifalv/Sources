using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Workflows;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Workflows;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Eduegate.Domain.Workflows
{
    public class WorkflowBL
    {
        private Eduegate.Framework.CallContext _callContext { get; set; }

        public WorkflowBL(Eduegate.Framework.CallContext context)
        {
            _callContext = context;
        }

        public List<WorkflowLogDTO> GetWorkflowLog(long headID)
        {
            List<WorkflowLogDTO> dtos = new List<WorkflowLogDTO>();
            var logs = new WorkflowRepository().GetWorkflowLog(headID);

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var workflowApprovedStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("WORKFLOW_APPROVED_STATUS_ID");
                var approvedStatusID = byte.Parse(workflowApprovedStatusID);

                var workflowRejectedStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("WORKFLOW_REJECTED_STATUS_ID");
                var rejectStatusID = byte.Parse(workflowRejectedStatusID);
                foreach (var log in logs)
                {
                    foreach (var rule in log.WorkflowTransactionHeadRuleMaps)
                    {
                        var workflowLog = new WorkflowLogDTO();

                        if (rule.WorkflowStatusID == approvedStatusID)
                        {
                            workflowLog.Description = workflowLog.StatusDescription + " - Approved by ";
                        }
                        else
                        {
                            workflowLog.Description = workflowLog.StatusDescription + " - Waiting for ";
                        }


                        workflowLog.Approvers = new List<KeyValueDTO>();
                        workflowLog.WorkflowTransactionHeadRuleMapID = rule.WorkflowTransactionHeadRuleMapIID;
                        workflowLog.WorkflowRuleID = rule.WorkflowTransactionHeadRuleMapIID;
                        workflowLog.HeadID = log.TransactionHeadID;
                        workflowLog.WorkflowID = log.WorkflowID;
                        workflowLog.ConditionID = rule.WorkflowConditionID;
                        workflowLog.ConditionName = rule.WorkflowCondition.ConditionName;
                        string additionalDescription = string.Empty;

                        foreach (var approver in rule.WorkflowTransactionRuleApproverMaps)
                        {
                            workflowLog.StatusID = approver.WorkflowStatusID.HasValue ? approver.WorkflowStatusID.Value : 1;
                            workflowLog.StatusDescription = approver.WorkflowStatusID.HasValue ? approver.WorkflowStatus.StatusName : string.Empty;

                            workflowLog.Approvers.Add(
                                       new KeyValueDTO()
                                       {
                                           Key = approver.EmployeeID.ToString(),
                                           Value = approver.Employee.FirstName + " " + approver.Employee.MiddleName + " " + approver.Employee.LastName
                                       });

                            if (rule.WorkflowStatusID != approvedStatusID)
                            {
                                workflowLog.Description = workflowLog.Description + approver.Employee.FirstName + " " + approver.Employee.MiddleName + " " + approver.Employee.LastName + ",";
                            }
                            else
                            {
                                if (approver.WorkflowStatusID == approvedStatusID)
                                {
                                    workflowLog.Description = workflowLog.Description + approver.Employee.FirstName + " " + approver.Employee.MiddleName + " " + approver.Employee.LastName + ",";
                                }

                                additionalDescription = additionalDescription + approver.Employee.FirstName + " " + approver.Employee.MiddleName + " " + approver.Employee.LastName + ",";
                            }
                        }

                        if (!string.IsNullOrEmpty(additionalDescription))
                        {
                            workflowLog.Description = workflowLog.Description + "</br>Assigned to (" + additionalDescription + ")";
                        }

                        dtos.Add(workflowLog);
                    }
                }
            }

            return dtos;
        }

        public List<WorkflowLogDTO> GetWorkflowLogByWorflowID(long workflowID, long headID, long? loginID)
        {
            List<WorkflowLogDTO> dtos = new List<WorkflowLogDTO>();
            var logs = new WorkflowRepository().GetWorkflowLogByWorkflowIDAndReferenceID(workflowID, headID);
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var workflowApprovedStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("WORKFLOW_APPROVED_STATUS_ID");
                var approvedStatusID = byte.Parse(workflowApprovedStatusID);

                var workflowRejectedStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("WORKFLOW_REJECTED_STATUS_ID");
                var rejectStatusID = byte.Parse(workflowRejectedStatusID);

                if (logs != null)
                {
                    foreach (var log in logs)
                    {
                        var ruleList = log.WorkflowLogMapRuleMaps?.ToList();

                        for (int rindex = 0; rindex < ruleList.Count; rindex++)
                        {
                            var rule = ruleList[rindex];
                            var workflowLog = new WorkflowLogDTO();

                            workflowLog.IsFlowCompleted = rule.IsFlowCompleted;
                            workflowLog.Approvers = new List<KeyValueDTO>();
                            workflowLog.WorkflowTransactionHeadRuleMapID = rule.WorkflowLogMapRuleMapIID;
                            workflowLog.WorkflowRuleID = rule.WorkflowLogMapRuleMapIID;
                            workflowLog.HeadID = log.ReferenceID;
                            workflowLog.WorkflowID = log.WorkflowID;
                            workflowLog.ConditionID = rule.WorkflowConditionID;
                            workflowLog.ConditionName = rule.WorkflowConditionID.ToString();
                            workflowLog.IsFlowCompleted = rule.IsFlowCompleted == null ? false : true;
                            workflowLog.Description = rule.Description;
                            string additionalDescription = string.Empty;
                            workflowLog.Buttons = new List<KeyValueDTO>();

                            var approverList = rule.WorkflowLogMapRuleApproverMaps?.ToList();
                            for (int index = 0; index < approverList.Count; index++)
                            {
                                var approver = approverList[index];
                                workflowLog.StatusID = approver.WorkflowStatusID.HasValue ? approver.WorkflowStatusID.Value : 1;

                                workflowLog.Approvers.Add(new KeyValueDTO()
                                {
                                    Key = approver.EmployeeID.ToString(),
                                    Value = approver.Employee.EmployeeCode + " - " + approver.Employee.FirstName + " " + approver.Employee.MiddleName + " " + approver.Employee.LastName
                                });

                                if (rule.Description != null)
                                {
                                    workflowLog.Description = rule.Description;
                                }
                                else
                                {
                                    //workflowLog.Description = @" - <b>Waiting for </b>" + approver.Employee.EmployeeCode + " - " + approver.Employee.FirstName + " " + approver.Employee.MiddleName + " " + approver.Employee.LastName;
                                    string approverNames = string.Join(", </br>", approverList.Select(a => a.Employee.EmployeeCode + " - " + a.Employee.FirstName + " " + a.Employee.MiddleName + " " + a.Employee.LastName));
                                    workflowLog.Description = $" - <b>Waiting for </b>{approverNames}";
                                }

                                //Button assign 
                                if (loginID == approver.Employee.LoginID && !workflowLog.Buttons.Any(b => b.Key == rule.Value2.ToString()))
                                {
                                    workflowLog.Buttons.Add(
                                        new KeyValueDTO()
                                        {
                                            Key = rule.Value2.ToString(),
                                            Value = rule.Value1
                                        });
                                }
                            }

                            //Hide all buttons in case some one already rejected that workflow
                            if (ruleList.Count > 0)
                            {
                                workflowLog.HideButtons = false;
                                var needHide = ruleList.Any(x => x.WorkflowStatusID == rejectStatusID);

                                if (needHide == true)
                                {
                                    workflowLog.HideButtons = true;
                                }
                            }

                            dtos.Add(workflowLog);
                        }
                    }
                }
            }

            return dtos;
        }

        public WorkflowDTO GetWorkflow(long workflowID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetWorkflowFeilds(int workflowTypeID)
        {
            var keyValues = new List<KeyValueDTO>();
            foreach (var field in new WorkflowRepository().GetWorkflowFeilds(workflowTypeID))
            {
                keyValues.Add(new KeyValueDTO()
                {
                    Key = field.WorkflowFieldID.ToString(),
                    Value = field.ColumnName
                });
            }

            return keyValues;
        }

        public WorkflowDTO SaveWorkflow(WorkflowDTO dto)
        {
            throw new NotImplementedException();
        }

        public WorkflowDTO UpdateWorkflowStatus(long WorkflowTransactionHeadRuleMapID, long employeeID, int statusID, long? referenceID)
        {
            var workflowResult = false;

            var workflowDTO = new WorkflowDTO();

            var workflow = new WorkflowRepository().GetWorkFlowRules(WorkflowTransactionHeadRuleMapID, employeeID);

            if (workflow == null || workflow.WorkflowID == null || string.IsNullOrEmpty(workflow.Value1))
                return null;

            var workflowApprovedStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("WORKFLOW_APPROVED_STATUS_ID");
            var approveStatusID = byte.Parse(workflowApprovedStatusID);

            var workflowrejectedStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("WORKFLOW_REJECTED_STATUS_ID");
            var rejectStatusID = byte.Parse(workflowrejectedStatusID);

            var applicationWorkFlowID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_APPLICATION_WORKFLOW_ID");
            var markEntryWorkFlowID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("MARK_ENTRY_WORKFLOW_ID");
            var assignmentWorkFlowID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ASSIGNMENT_WORKFLOW_ID");
            var lessonPlanWorkFlowID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("LESSON_PLAN_WORKFLOW_ID");
            var leaveApprovalWorkFlowID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_LEAVE_APPROVAL_WORKFLOW_ID");
            var transferApprovalWorkFlowID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_TRANSFER_REQUEST_WORKFLOW_ID");
            var applicationFormWorkFlowID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("APPLICATION_FORM_APPROVAL_WORKFLOW_ID");
            var updateStockworkFlowID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STOCK_UPDATION_WORKFLOW_ID");
            var quotationToPOWorkFlowID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("QUOTATION_TO_PO_WORKFLOW_ID"); 
            var libraryBookReturnWorkFlowID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("LIBRARY_BOOK_RETURN_WORKFLOW_ID");  

            var status = false;

            //temporary methode need to bring common 
            if (statusID == 0)
            {
                switch (workflow.WorkflowID)
                {
                    case var value when value == long.Parse(lessonPlanWorkFlowID):
                        {
                            status = new WorkflowRepository().UpdateLessonPlanStatus(workflow.WorkflowRuleIID, workflow.Value1, statusID, referenceID);
                        }
                        break;                   
                    case var value when value == long.Parse(quotationToPOWorkFlowID):
                        {
                            statusID = (int)Eduegate.Services.Contracts.Enums.TransactionStatus.Cancelled;
                            status = new WorkflowRepository().UpdateTransactionDocumentStatusID((long?)workflow.WorkflowID, workflow.WorkflowRuleIID, workflow.Value1, statusID, referenceID, employeeID);
                        }
                        break;
                }

                new WorkflowRepository().RejectCurrentWorkFlow(WorkflowTransactionHeadRuleMapID, employeeID, rejectStatusID);
            }
            else
            {
                switch (workflow.WorkflowID)
                {
                    case var value when value == long.Parse(applicationWorkFlowID):
                        {
                            status = new WorkflowRepository().UpdateApplicationStatus(workflow.WorkflowRuleIID, workflow.Value1,statusID, referenceID,employeeID);
                        }
                        break;
                    case var value when value == long.Parse(markEntryWorkFlowID):
                        {
                            status = new WorkflowRepository().UpdateMarkRegisterStatus(workflow.WorkflowRuleIID, workflow.Value1, referenceID);
                        }
                        break;
                    case var value when value == long.Parse(assignmentWorkFlowID):
                        {
                            status = new WorkflowRepository().UpdateAssignmentStatus(workflow.WorkflowRuleIID, workflow.Value1, referenceID);
                        }
                        break;
                    case var value when value == long.Parse(lessonPlanWorkFlowID):
                        {
                            status = new WorkflowRepository().UpdateLessonPlanStatus(workflow.WorkflowRuleIID, workflow.Value1, statusID, referenceID);
                        }
                        break;
                    case var value when value == long.Parse(leaveApprovalWorkFlowID):
                        {
                            status = new WorkflowRepository().UpdateLeaveStatus(workflow.WorkflowRuleIID, workflow.Value1, statusID, referenceID, employeeID);
                        }
                        break;
                    case var value when value == long.Parse(transferApprovalWorkFlowID):
                        {
                            status = new WorkflowRepository().UpdateTransferRequestStatus((long)workflow.WorkflowID,workflow.WorkflowRuleIID, workflow.Value1, statusID, referenceID, employeeID,WorkflowTransactionHeadRuleMapID);
                        }
                        break;
                    case var value when value == long.Parse(applicationFormWorkFlowID):
                        {
                            status = new WorkflowRepository().UpdateApplicationFormStatus(workflow.WorkflowRuleIID, workflow.Value1, statusID, referenceID, employeeID);
                        }
                        break;
                    case var value when value == long.Parse(updateStockworkFlowID):
                        {
                            status = new WorkflowRepository().UpdateStockQuantityByWorkflow((long?)workflow.WorkflowID,workflow.WorkflowRuleIID, workflow.Value1, statusID, referenceID, employeeID);
                        }
                        break;                    
                    case var value when value == long.Parse(quotationToPOWorkFlowID):
                        {
                            status = new WorkflowRepository().UpdateTransactionDocumentStatusID((long?)workflow.WorkflowID,workflow.WorkflowRuleIID, workflow.Value1, statusID, referenceID, employeeID);
                        }
                        break;
                    case var value when value == long.Parse(libraryBookReturnWorkFlowID):
                        {
                            status = new WorkflowRepository().UpdateLibraryBookTransactionStatusID((long?)workflow.WorkflowID, workflow.WorkflowRuleIID, workflow.Value1, statusID, referenceID, _callContext.LoginID);
                        }
                        break;
                }
                if (status == true)
                {
                    new WorkflowRepository().UpdateWorkflowStatus(WorkflowTransactionHeadRuleMapID, employeeID, approveStatusID, referenceID);
                }
            }

            workflowResult = status;

            workflowDTO = new WorkflowDTO()
            {
                WorkflowIID = (long)workflow.WorkflowID,
                WorkflowStatusID = statusID,
                EmployeeID = employeeID,
                WorkflowReferenceID = referenceID,
                WorkflowResult = workflowResult,
            };

            return workflowDTO;
        }

    }
}