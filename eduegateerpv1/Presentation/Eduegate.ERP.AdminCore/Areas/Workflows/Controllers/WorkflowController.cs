using Eduegate.Domain;
using Eduegate.Domain.Repository;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Workflows;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.ERP.Admin.Areas.Workflows.Controllers
{
    [Area("Workflows")]
    public class WorkflowController : BaseSearchController
    {
        // GET: Workflow/Workflow
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View();
        }

        [HttpGet]
        public ActionResult GetWorkflowFeilds(int workflowType)
        {
            var vm = KeyValueViewModel.FromDTO(ClientFactory.WorkflowServiceClient(CallContext).GetWorkflowFeilds(workflowType));
            return Json(vm);
        }

        [HttpGet]
        public ActionResult GetWorkflowDetails(long headID, long? workflowID = (long?)null)
        {
            //MutualRepository mutualRepository = new MutualRepository();
            //WorkflowRepository
            //var settingValue = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_APPLICATION_WORKFLOW");
            ViewBag.WorkflowID = workflowID;
            return PartialView(headID);
        }

        [HttpGet]
        public JsonResult GetWorkflowLog(long headID)
        {
            var workflows = WorkflowLogViewModel.FromDTO(ClientFactory.WorkflowServiceClient(CallContext).GetWorkflowLog(headID));

            foreach (var workflow in workflows)
            {
                workflow.LoggedinLoginID = CallContext.LoginID;
                workflow.LoggedinEmployeeID = CallContext.EmployeeID;
            }

            return Json(workflows);
        }

        [HttpGet]
        public JsonResult GetWorkflowLogByWorflowID(long workflowID, long headID)
        {
            var workflows = WorkflowLogViewModel.FromDTO(ClientFactory.WorkflowServiceClient(CallContext).GetWorkflowLogByWorflowID(workflowID, headID,CallContext.LoginID));

            foreach (var workflow in workflows)
            {
                workflow.LoggedinLoginID = CallContext.LoginID;
                workflow.LoggedinEmployeeID = CallContext.EmployeeID;
            }

            return Json(workflows);
        }

        [HttpGet]
        public JsonResult ApproveWorkflowStatus(long workflowTransactionHeadRuleMapID, long employeeID, int statusID, long? applicationID)
        {
            var result = false;

            var resultDTO = ClientFactory.WorkflowServiceClient(CallContext).UpdateWorkflowStatus(
               workflowTransactionHeadRuleMapID, employeeID, statusID, applicationID);

            if(resultDTO != null && resultDTO.WorkflowResult)
            {
                var quotationToPOWorkFlowID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("QUOTATION_TO_PO_WORKFLOW_ID");

                //Transaction mail notification
                if (long.Parse(quotationToPOWorkFlowID) == resultDTO.WorkflowIID)
                {
                    var transDTO = new Services.Contracts.Catalog.TransactionDTO();

                    var approveTransactionStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("APPROVED_TRANSACTION_STATUS_ID");
                    var aprvTransStatusID = int.Parse(approveTransactionStatusID);

                    if (aprvTransStatusID == statusID) {
                        transDTO = new TransactionRepository().GetTransactionDTO((long)applicationID);

                        var purposeMsg = "Please confirm the receipt of this order from vendor portal.";
                        new TransactionBL(CallContext).SendTransactionMailToSupplier(transDTO, purposeMsg);
                    }
                }
            }

            result = resultDTO.WorkflowResult;

            return Json(result);
        }

    }
}