using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Workflows
{
    public class WorkflowAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Workflows";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Workflow_default",
                "Workflows/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}