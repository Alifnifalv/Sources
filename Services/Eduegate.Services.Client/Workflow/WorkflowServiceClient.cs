using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Service.Client;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Client.Workflow
{
    public class WorkflowServiceClient : BaseClient, IWorkflowService
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string service = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.WORKFLOW_SERVICE);

        public WorkflowServiceClient(CallContext context = null, Action<string> logger = null)
           : base(context, logger)
        {
        }

        public WorkflowDTO GetWorkflow(long workflowID)
        {
            var bannerUri = string.Format("{0}/{1}/{2}", service, "GetKnowHowOptions", workflowID);
            return ServiceHelper.HttpGetRequest<WorkflowDTO>(bannerUri, _callContext, _logger);

        }

        public List<KeyValueDTO> GetWorkflowFeilds(int workflowTypeID)
        {
            var bannerUri = string.Format("{0}/{1}/{2}", service, "GetWorkflowFeilds", workflowTypeID);
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(bannerUri, _callContext, _logger);

        }

        public WorkflowDTO SaveWorkflow(WorkflowDTO dto)
        {
            var url = string.Format("{0}/{1}", service, "SaveWorkflow");
            return ServiceHelper.HttpPostGetRequest<WorkflowDTO>(url, dto, _callContext, _logger);
        }

        public List<WorkflowLogDTO> GetWorkflowLog(long headID)
        {
            var bannerUri = string.Format("{0}/{1}/{2}", service, "GetWorkflowLog", headID);
            return ServiceHelper.HttpGetRequest<List<WorkflowLogDTO>>(bannerUri, _callContext, _logger);
        }

        public WorkflowDTO UpdateWorkflowStatus(long WorkflowTransactionHeadRuleMapID, long employeeID, int statusID, long? applicationID)
        {
            var bannerUri = string.Format("{0}?WorkflowTransactionHeadRuleMapID={1}&employeeID={2}&statusID={3}&applicationID={4}"
                , service, "UpdateWorkflowStatus", WorkflowTransactionHeadRuleMapID, employeeID, statusID,applicationID);
            return ServiceHelper.HttpGetRequest<WorkflowDTO>(bannerUri, _callContext, _logger);
        }

        public List<WorkflowLogDTO> GetWorkflowLogByWorflowID(long workflowID, long headID, long? loginID)
        {
            throw new NotImplementedException();
        }
    }
}
