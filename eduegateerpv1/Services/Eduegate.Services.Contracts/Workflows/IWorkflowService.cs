using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;

namespace Eduegate.Services.Contracts.Workflows
{
    public interface IWorkflowService
    {
        List<WorkflowLogDTO> GetWorkflowLogByWorflowID(long workflowID, long headID, long? loginID);

        List<WorkflowLogDTO> GetWorkflowLog(long headID);

        WorkflowDTO GetWorkflow(long workflowID);

        List<KeyValueDTO> GetWorkflowFeilds(int workflowTypeID);

        WorkflowDTO SaveWorkflow(WorkflowDTO dto);

        WorkflowDTO UpdateWorkflowStatus(long WorkflowTransactionHeadRuleMapID, long employeeID, int statusID, long? applicationID);
    }
}