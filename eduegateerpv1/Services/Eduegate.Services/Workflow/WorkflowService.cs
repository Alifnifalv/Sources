using Eduegate.Domain.Workflows;
using Eduegate.Framework.Services;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Workflows;
using System.ServiceModel;

namespace Eduegate.Services.Workflow
{
    public class WorkflowService : BaseService, IWorkflowService
    {        
        public WorkflowDTO GetWorkflow(long workflowID)
        {
            try
            {
                return new WorkflowBL(base.CallContext).GetWorkflow(workflowID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<BannerService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<KeyValueDTO> GetWorkflowFeilds(int workflowTypeID)
        {
            try
            {
                return new WorkflowBL(base.CallContext).GetWorkflowFeilds(workflowTypeID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<BannerService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<WorkflowLogDTO> GetWorkflowLog(long headID)
        {
            try
            {
                return new WorkflowBL(base.CallContext).GetWorkflowLog(headID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WorkflowService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<WorkflowLogDTO> GetWorkflowLogByWorflowID(long workflowID, long headID, long? loginID)
        {
            try
            {
                return new WorkflowBL(base.CallContext).GetWorkflowLogByWorflowID(workflowID, headID, loginID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WorkflowService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public WorkflowDTO SaveWorkflow(WorkflowDTO dto)
        {
            try
            {
                return new WorkflowBL(base.CallContext).SaveWorkflow(dto);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<BannerService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public WorkflowDTO UpdateWorkflowStatus(long WorkflowTransactionHeadRuleMapID, long employeeID, int statusID, long? applicationID)
        {
            try
            {
                return new WorkflowBL(base.CallContext).UpdateWorkflowStatus(WorkflowTransactionHeadRuleMapID, employeeID, statusID, applicationID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<BannerService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
    }
}
