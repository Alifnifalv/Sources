using Eduegate.Services.Client.Factory;
using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Application.Mvc;
using Eduegate.Services.Contracts.Logging;

namespace Eduegate.ERP.Admin.Controllers
{
    public class ActivityController : BaseController
    {
        // GET: Activity
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetActivities()
        {
            var connectionString = Infrastructure.ConfigHelper.GetDefaultLoggerConnectionString();

            if (connectionString != null)
            {
                var alerts = ClientFactory.LoggingServicesClient(CallContext).GetActivitiesByLoginID(CallContext.LoginID.Value);
                return Json(alerts);
            }
            else
            {
                return Json(null);
            }
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            var alerts = ClientFactory.LoggingServicesClient(CallContext).GetActivity(IID);
            return View(alerts);
        }

        public IActionResult GetRunningActivities()
        {
            var activities = new List<ActivityDTO>();
            var monitor = Hangfire.JobStorage.Current.GetMonitoringApi();
            var allQueues = monitor.ProcessingJobs(0, int.MaxValue);

            foreach (var job in allQueues)
            {
                activities.Add(new ActivityDTO()
                {
                    ActionStatusName = "Pending",
                    ActivityTypeName = "Queue",
                    Description = job.Value.Job.Method.Name + "- started on "
                    + job.Value.StartedAt.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                    CreatedDate = job.Value.StartedAt
                }
                );
            }

            var succeeded = monitor.SucceededJobs(0, int.MaxValue);

            foreach (var job in succeeded)
            {
                var t = TimeSpan.FromSeconds(double.Parse(job.Value.TotalDuration.Value.ToString()));

                if (job.Value != null)
                {
                    activities.Add(new ActivityDTO()
                    {
                        ActionStatusName = "Completed",
                        ActivityTypeName = "Queue",
                        Description = job.Value?.Job?.Method?.Name + "- completed on "
                        + job.Value?.SucceededAt.Value.ToString("dd/MM/yyyy HH:mm:ss") + " - time taken : " + t.TotalMinutes + " minutes",
                        CreatedDate = job.Value?.SucceededAt
                    });
                }
            }

            return Ok(activities);
        }

        public IActionResult GetScheduledActivities()
        {
            var activities = new List<ActivityDTO>();
            var monitor = Hangfire.JobStorage.Current.GetMonitoringApi();
            var allQueues = monitor.ScheduledJobs(0, int.MaxValue);

            foreach (var job in allQueues)
            {
                activities.Add(new ActivityDTO()
                {
                    ActionStatusName = "Scheduled",
                    ActivityTypeName = "Queue",
                    Description = $"{job.Value.Job.Method.Name}- created on {job.Value.EnqueueAt.ToString("dd/MM/yyyy HH:mm:ss")}; " +
                        $"shcheduled for {job.Value.ScheduledAt.Value.ToString("dd/MM/yyyy HH:mm:ss")}",
                    CreatedDate = job.Value.EnqueueAt
                });
            }

            return Ok(activities);
        }
    }
}