using Eduegate.Domain.Entity.Models.Workflows;
using Eduegate.Frameworks.Enums.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Helpers
{
    public class WorkflowHelper
    {
        public static WorkflowTransactionHeadMap GetActiveWorkflow(List<WorkflowTransactionHeadMap> workflowMaps)
        {
            WorkflowTransactionHeadMap workflowmap = null;

            foreach (var map in workflowMaps)
            {
                if(map.WorkflowStatusID != (byte)WorkflowStatuses.Approved)
                {
                    workflowmap = map;
                    break;
                }
            }

            return workflowmap;
        }

        public static void UpdateWorkflow(List<WorkflowTransactionHeadMap> workflowMaps)
        {
            foreach (var map in workflowMaps)
            {

            }
        }
    }
}
