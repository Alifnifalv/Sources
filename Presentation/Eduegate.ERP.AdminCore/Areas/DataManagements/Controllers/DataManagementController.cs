using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using static Eduegate.Domain.Mappers.DataManagement.DataManagementMapper;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Distributions;
using Hangfire;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Domain.Mappers.DataManagement;
using Eduegate.Services.Contracts.Logging;
using System.Globalization;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.ERP.Admin.Areas.DataManagements.Controllers
{
    [Area("DataManagements")]
    public class DataManagementController : BaseSearchController
    {
        private readonly IBackgroundJobClient _backgroundJobs;
        public DataManagementController(IBackgroundJobClient backgroundJobs)
        {
            _backgroundJobs = backgroundJobs;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TableInformation()
        {
            return View();
        }

        public List<TableInfo> FillTableDetails()
        {
            var vm = new Domain.DataManagement.DataManagementBL(CallContext).GetAllDataTables();

            return vm;
        }

        public ActionResult ServerInformation()
        {
            return View();
        }

        [HttpPost("ScheduleDataClearing")]
        public void ScheduleDataClearing()
        {
            _backgroundJobs.Schedule(() => DeleteAllFromServer(), TimeSpan.FromMinutes(10));
        }

        [HttpPost]
        public IActionResult DeleteAllFromServer()
        {
            try
            {
                string tempFilePath = string.Format(new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsPhysicalPath"),"Files","Temp");

                // Ensure the directory exists
                if (Directory.Exists(tempFilePath))
                {
                    // Delete all files and subdirectories
                    DirectoryInfo directory = new DirectoryInfo(tempFilePath);
                    foreach (DirectoryInfo subDirectory in directory.GetDirectories())
                    {
                        foreach (DirectoryInfo dir in subDirectory.GetDirectories())
                        {
                            foreach (FileInfo file in dir.GetFiles())
                            {
                                file.Delete();
                            }

                            dir.Delete();
                        }
                    }
                    return Ok(new { Message = "Cache cleared" });
                }
                else
                {
                    Console.WriteLine($"Directory not found: {tempFilePath}");
                    return NotFound(new { Message = "Directory not found" });
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the file deletion process
                Console.WriteLine($"An error occurred while deleting files: {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while deleting files", Error = ex.Message });
            }
        }

        public ServerInfo CheckTempDirectory()
        {
            var serverInfo = new Domain.DataManagement.DataManagementBL(CallContext).CheckTempDirectory();

            return serverInfo;

        }

        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public OperationResultDTO ArchiveTable(string tableName, DateTime Date)
        {
            var data = new Domain.DataManagement.DataManagementBL(CallContext).ArchiveTable(tableName, Date);

            return data;
        }

        public ActionResult ScheduleView()
        {
            return View();
        }

        public ActionResult BackupView()
        {
            return View();
        }

        public List<DatabaseInfo> DatabaseDetails()
        {
            var serverInfo = new Domain.DataManagement.DataManagementBL(CallContext).DatabaseDetails();

            return serverInfo;

        }

        public List<SubscriptionTypeDTO> GetScheduleTypes(long deliveryTypeId, DateTime dateTime)
        {
            return new List<SubscriptionTypeDTO>()
            {
                new SubscriptionTypeDTO()
                {
                     SubscriptionTypeID = 1,
                     SubscriptionName = "Year Wise",
                },
                new SubscriptionTypeDTO()
                {
                     SubscriptionTypeID = 2,
                     SubscriptionName = "Academic Year Wise",
                },
                new SubscriptionTypeDTO()
                {
                     SubscriptionTypeID = 3,
                     SubscriptionName = "Month Wise",
                }
            };
        }

        [HttpPost]
        public OperationResultDTO ScheduleArchive(string tableName, DateTime date, DateTime scheduleDate)
        {
            var delay = scheduleDate - DateTime.Now;

            var jobId = BackgroundJob.Schedule(
                () => new DataManagementMapper().ArchiveTable(tableName, date),
                delay);

            var result = new OperationResultDTO
            {
                operationResult = OperationResult.Success,
                Message = "Archive job scheduled successfully."
            };

            return result;
        }

        [HttpGet]
        public IActionResult GetScheduledTables()
        {
            var activities = new List<ActivityDTO>();
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat", "dd/MM/yyyy");

            var monitor = Hangfire.JobStorage.Current.GetMonitoringApi();
            var allQueues = monitor.ScheduledJobs(0, int.MaxValue);

            foreach (var job in allQueues)
            {
                activities.Add(new ActivityDTO()
                {
                    ActivityID = long.Parse(job.Key),
                    ActionStatusName = "Scheduled",
                    ActivityTypeName = "Queue",
                    Description = job.Value?.Job?.Args[0].ToString(),
                    CreatedDateString = (job.Value.ScheduledAt ?? DateTime.MinValue).ToString(dateFormat, CultureInfo.InvariantCulture),
                    EnqueueDateString = (job.Value.EnqueueAt).ToString(dateFormat, CultureInfo.InvariantCulture),
                    ActionTypeName = job.Value.InvocationData.Method,
                });
            }

            var succeeded = monitor.SucceededJobs(0, int.MaxValue);

            foreach (var job in succeeded)
            {
                var t = TimeSpan.FromSeconds(double.Parse(job.Value.TotalDuration.Value.ToString()));

                if (job.Value != null)
                {
                    activities.Add(new ActivityDTO()
                    {
                        ActivityID = long.Parse(job.Key),
                        ActionStatusName = "Completed",
                        ActivityTypeName = "Queue",
                        Description = job.Value?.Job?.Args[0].ToString(),
                        EnqueueDateString = (job.Value.SucceededAt ?? DateTime.MinValue).ToString(dateFormat, CultureInfo.InvariantCulture),
                        CreatedDateString = (job.Value.SucceededAt ?? DateTime.MinValue).ToString(dateFormat, CultureInfo.InvariantCulture),
                        ActionTypeName = job.Value.InvocationData.Method
                    });
                }
            }

            var deletedJobs = monitor.DeletedJobs(0, int.MaxValue);

            foreach (var job in deletedJobs)
            {
                if (job.Value != null)
                {
                    activities.Add(new ActivityDTO()
                    {
                        ActivityID = long.Parse(job.Key),
                        ActionStatusName = "Deleted",
                        ActivityTypeName = "Queue",
                        Description = job.Value?.Job?.Args[0].ToString(),
                        EnqueueDateString = (job.Value.DeletedAt ?? DateTime.MinValue).ToString(dateFormat, CultureInfo.InvariantCulture),
                        CreatedDateString = (job.Value.DeletedAt ?? DateTime.MinValue).ToString(dateFormat, CultureInfo.InvariantCulture),
                        ActionTypeName = job.Value.InvocationData.Method
                    });
                }
            }

            return Ok(activities);
        }

        [HttpPost]
        public OperationResultDTO DeleteSchedule(string jobID)
        {
            var monitor = Hangfire.JobStorage.Current.GetMonitoringApi();

            var jobsScheduled = monitor.ScheduledJobs(0, int.MaxValue)
            .Where(x => x.Key == jobID);

            foreach (var j in jobsScheduled)
            {
                BackgroundJob.Delete(j.Key);
            }

            var result = new OperationResultDTO
            {
                operationResult = OperationResult.Success,
                Message = "Table scheduled deleted successfully."
            };

            return result;
        }

        [HttpPost]
        public OperationResultDTO BackupDB(string databaseName, int typeID)
        {
            var data = new Domain.DataManagement.DataManagementBL(CallContext).BackupDB(databaseName, typeID);

            return data;

            //_backgroundJobs.Enqueue(() => new Domain.DataManagement.DataManagementBL(CallContext).BackupDB(databaseName, typeID));

            //return new OperationResultDTO
            //{
            //    operationResult = OperationResult.Success,
            //    Message = "A backup is being downloaded in the background. This may take a few moments. You will be notified once it's completed."
            //};

        }

        public List<KeyValueDTO> GetBackupTypes(long deliveryTypeId, DateTime dateTime)
        {
            return new List<KeyValueDTO>()
            {
                new KeyValueDTO()
                {
                     Key = "0",
                     Value = "Disk",
                },
                new KeyValueDTO()
                {
                     Key = "1",
                     Value = "URL",
                },
            };
        }

        [HttpPost]
        public OperationResultDTO ScheduleBackupDB(string databaseName, int typeID, DateTime scheduleDate)
        {
            var delay = scheduleDate - DateTime.Now;

            var jobId = BackgroundJob.Schedule(
                () => new Domain.DataManagement.DataManagementBL(CallContext).BackupDB(databaseName, typeID),
                delay);

            return new OperationResultDTO
            {
                operationResult = OperationResult.Success,
                Message = "A database backup is being scheduled. You will be notified once it's completed."
            };

        }

    }
}