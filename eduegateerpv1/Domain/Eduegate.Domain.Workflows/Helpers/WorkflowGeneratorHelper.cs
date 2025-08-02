using Eduegate.Domain.Entity.Models.Workflows;
using Eduegate.Domain.Repository.Workflows;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Workflows.Helpers
{
    public class WorkflowGeneratorHelper
    {
        public static List<WorkflowLogMap> GenerateWorkflows(long workflowID, long referenceID)
        {
            var workflowMaps = new List<WorkflowLogMap>();
            var repository = new WorkflowRepository();
            //if the workflow exists
            var workflowLogData = repository.GetWorkflowLogByWorkflowIDAndReferenceID(workflowID, referenceID);

            if (workflowLogData == null || workflowLogData.Count == 0)
            {
                var workflow = repository.GetWorkflowByWorkflowID(workflowID);

                if (workflow != null)
                {
                    var workflowLog = new WorkflowLogMap()
                    {
                        WorkflowStatusID = 1,
                        ReferenceID = referenceID,
                        WorkflowID = workflowID,
                        WorkflowLogMapRuleMaps = new List<WorkflowLogMapRuleMap>()
                    };

                    var rules = new List<WorkflowLogMapRuleMap>();
                    
                    foreach (var ruleMap in workflow.WorkflowRules)
                    {
                        var workflowStatuses = repository.GetWorkflowStatusesByWorkflowID(workflowID).FirstOrDefault();

                        var rule = new WorkflowLogMapRuleMap()
                        {
                            WorkflowStatusID = workflowStatuses == null ? (byte?)null : workflowStatuses.WorkflowStatusID,
                            WorkflowRuleID = ruleMap.WorkflowRuleIID,
                            WorkflowConditionID = ruleMap.ConditionID,
                            Remarks = "Workflow assigned by the system.",
                            Value1 = ruleMap.Value1,
                            Value2 = ruleMap.Value2,
                            Value3 = ruleMap.Value3,
                        };

                        foreach (var condition in ruleMap.WorkflowRuleConditions)
                        {
                            condition.WorkflowTransactionRuleApproverMaps = new List<WorkflowTransactionRuleApproverMap>();
                            rule.WorkflowConditionID = condition.ConditionID;

                            foreach (var approver in condition.WorkflowRuleApprovers)
                            {
                                rule.WorkflowLogMapRuleApproverMaps.Add(new WorkflowLogMapRuleApproverMap()
                                {
                                    EmployeeID = approver.EmployeeID,
                                    WorkflowConditionID = condition.ConditionID,
                                });
                            }
                        }

                        workflowLog.WorkflowLogMapRuleMaps.Add(rule);
                    }

                    workflowMaps.Add(workflowLog);
                    repository.UpdateWorkflowLogs(workflowMaps);
                    return workflowMaps;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }            
        }
    }
}
