using Eduegate.Framework;
using Eduegate.Service.Client;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Workflows;
using Eduegate.Services.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Client.Direct.Workflow
{
    public class WorkflowServiceClient : IWorkflowService
    {
        WorkflowService service = new WorkflowService();

        public WorkflowServiceClient(CallContext context = null)         
        {
        }

        public WorkflowDTO GetWorkflow(long workflowID)
        {
            return service.GetWorkflow(workflowID);
        }

        public List<KeyValueDTO> GetWorkflowFeilds(int workflowTypeID)
        {
            return service.GetWorkflowFeilds(workflowTypeID);
        }

        public List<WorkflowLogDTO> GetWorkflowLog(long headID)
        {
            return service.GetWorkflowLog(headID);
        }

        public List<WorkflowLogDTO> GetWorkflowLogByWorflowID(long workflowID, long headID, long? loginID)
        {
            return service.GetWorkflowLogByWorflowID(workflowID, headID, loginID);
        }

        public WorkflowDTO SaveWorkflow(WorkflowDTO dto)
        {
            return service.SaveWorkflow(dto);
        }

        public WorkflowDTO UpdateWorkflowStatus(long WorkflowTransactionHeadRuleMapID, long employeeID, int statusID, long? applicationID)
        {
            return service.UpdateWorkflowStatus(WorkflowTransactionHeadRuleMapID, employeeID, statusID, applicationID);
        }
    }
}
