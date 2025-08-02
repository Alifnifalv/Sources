using Eduegate.ERP.Admin.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Workflows;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Workflows.Controllers
{
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
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetWorkflowDetails(long headID, long? workflowID = (long?)null)
        {
            //MutualRepository mutualRepository = new MutualRepository();
            //WorkflowRepository
            //var settingValue = mutualRepository.GetSettingData("STUDENT_APPLICATION_WORKFLOW");
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

            return Json(workflows, JsonRequestBehavior.AllowGet);
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

            return Json(workflows, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ApproveWorkflowStatus(long workflowTransactionHeadRuleMapID, long employeeID, int statusID, long? applicationID)
        {
            var result = ClientFactory.WorkflowServiceClient(CallContext).UpdateWorkflowStatus(
               workflowTransactionHeadRuleMapID, employeeID, statusID, applicationID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}