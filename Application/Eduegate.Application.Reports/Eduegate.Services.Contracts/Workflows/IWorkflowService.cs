using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Workflows
{
    public interface IWorkflowService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetWorkflowLogByWorflowID/{workflowID}/{headID}/{loginID}")]
        List<WorkflowLogDTO> GetWorkflowLogByWorflowID(long workflowID, long headID, long? loginID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetWorkflowLog/{headID}")]
        List<WorkflowLogDTO> GetWorkflowLog(long headID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetWorkflow/{workflowID}")]
        WorkflowDTO GetWorkflow(long workflowID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetWorkflowFeilds/{workflowTypeID}")]
        List<KeyValueDTO> GetWorkflowFeilds(int workflowTypeID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveWorkflow")]
        WorkflowDTO SaveWorkflow(WorkflowDTO dto);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate =
            "UpdateWorkflowStatus?WorkflowTransactionHeadRuleMapID={WorkflowTransactionHeadRuleMapID}&employeeID={employeeID}&statusID={statusID}&applicationID={applicationID}")]
        bool UpdateWorkflowStatus(long WorkflowTransactionHeadRuleMapID, long employeeID, int statusID, long? applicationID);
    }
}
