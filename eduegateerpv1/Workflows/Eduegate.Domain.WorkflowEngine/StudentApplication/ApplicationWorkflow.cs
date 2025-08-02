using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Models.Workflows;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Domain.WorkflowEngine.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.WorkflowEngine.StudentApplication
{
    public class ApplicationWorkflow : IWorkflowProcessor, IWorflowManager
    {
        public bool GenerateWorkFlow(long workflowID, long referenceID)
        {
            using (var dbContext = new dbWorkflowERPContext())
            {
                var workflowMaps = new List<WorkflowLogMap>();

                //if the workflow exists
                var workflowLogData = dbContext.WorkflowLogMaps.Where(x => x.WorkflowID.Value == workflowID && x.ReferenceID.Value == referenceID)
                    .ToList();

                if (workflowLogData == null || workflowLogData.Count() == 0)
                {
                    var workflow = dbContext.Workflows.Where(x => x.WorkflowIID == workflowID)
                        .Include(i => i.WorkflowRules).ThenInclude(i => i.WorkflowRuleConditions)
                        .FirstOrDefault();

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
                            var workflowStatuses = dbContext.WorkflowStatuses.Where(x => x.WorkflowTypeID == workflow.WorkflowTypeID).FirstOrDefault();

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
                                condition.WorkflowTransactionRuleApproverMaps = new List<Entity.Worflow.WorkFlows.WorkflowTransactionRuleApproverMap>();
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

                        foreach(var logMap in workflowMaps)
                        {
                            dbContext.Entry(logMap).State = EntityState.Added;
                            dbContext.SaveChanges();
                        }
                        
                        return true;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
        }

        public bool ProcessSteps()
        {
            throw new NotImplementedException();
        }

        public bool UpdateWorkFlow()
        {
            throw new NotImplementedException();
        }
    }
}
