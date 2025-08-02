using Eduegate.Application.Mvc;
using Eduegate.Domain;
using Eduegate.Infrastructure.Enums;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Jobs;
using Eduegate.Web.Library;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Filter;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Eduegate.Recruitment.Portal.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            //return View();
            return RedirectToAction("Login","Account");
        }

        public IActionResult Home()
        {
            return View();
        } 
        
        public IActionResult JobList()
        {
            return View();
        } 
       
        public IActionResult JobDetails(long jobIID) {
            if (jobIID != 0)
            {
                var jobData = new RecruitmentBL(CallContext).GetAvailableJobList().FirstOrDefault(x => x.JobIID == jobIID);
                return View(jobData);
            }
            else
            {
                return RedirectToAction("JobList");
            }
        } 
        
        public IActionResult MyProfile()
        {
            return View();
        } 
        
        public IActionResult InterviewList()
        {
            return View();
        }       
        
        public IActionResult AppliedJobs()
        {
            return View();
        }  
        
        public IActionResult JobDescription()
        {
            return View();
        }

        public JsonResult GetUserDetails()
        {
            var userDetail = new RecruitmentBL(CallContext).GetUserDetails(CallContext.LoginID);

            return Json(new { IsError = false, Response = userDetail });
        }

        public IActionResult JobApply(long jobIID) 
        {
            if (jobIID != 0)
            {
                var jobData = new RecruitmentBL(CallContext).GetAvailableJobList().FirstOrDefault(x => x.JobIID == jobIID);
                return View(jobData);
            }
            else
            {
                return RedirectToAction("JobList");
            }
        }


        public IActionResult _ToastMessage() 
        {
            return View();
        }


        [HttpGet]
        public JsonResult GetAvailableJobList()
        {
            var result = new RecruitmentBL(CallContext).GetAvailableJobList();

            return Json(new { IsError = false, Response = result });
        }  
        
        [HttpGet]
        public JsonResult GetProfileDetails()
        {
            var result = new RecruitmentBL(CallContext).GetProfileDetails(CallContext.LoginID);

            return Json(new { IsError = false, Response = result });
        }


        public async Task<IActionResult> DataListView(SearchView listName)
        {
            var view = listName;
            var paramview = view;
            var viewModel = new FilterViewModel() { View = paramview, ViewName = paramview.ToString() };
            var filterClient = ClientFactory.MetadataServiceClient(CallContext);
            var metadata = filterClient.GetFilterMetadata((Eduegate.Services.Contracts.Enums.SearchView)view);
            viewModel.Columns = FilterColumnViewModel.FromDTO(metadata).OrderBy(x => x.FilterColumnID).ToList();
            viewModel.UserValues = filterClient.GetUserFilterMetadata((Services.Contracts.Enums.SearchView)(int)view);
            var gridMetadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)view);

            return View(new AdvanceFilterViewModel()
            {
                FilterViewModel = viewModel,
                SearchMetadata = new SearchListViewModel
                {
                    ViewName = paramview,
                    ViewTitle = gridMetadata.ViewName,
                    HeaderList = gridMetadata.Columns,
                    SummaryHeaderList = gridMetadata.SummaryColumns,
                    UserViews = gridMetadata.UserViews,
                    SortColumns = gridMetadata.SortColumns,
                    IsMultilineEnabled = false,
                    IsCategoryColumnEnabled = false,
                    InfoBar = string.Empty,
                    IsEditableLink = gridMetadata.IsEditable,
                    IsGenericCRUDSave = gridMetadata.IsGenericCRUDSave,
                    ActualControllerName = gridMetadata.ControllerName,
                    ViewFullPath = gridMetadata.ViewFullPath,
                    RuntimeFilter = "",
                }
            });
        }


        [HttpPost]
        public IActionResult ApplyForJob([FromBody] JobApplicationDTO applicationDTO)
        {
            var result = new RecruitmentBL(CallContext).ApplyForJob(applicationDTO);
            return Ok(applicationDTO);
        }

        [HttpGet]
        public JsonResult GetAppliedJobList()
        {
            var result = new RecruitmentBL(CallContext).GetAppliedJobList();

            return Json(new { IsError = false, Response = result });
        }
        
        [HttpGet]
        public JsonResult GetMyInterviewList()
        {
            var result = new RecruitmentBL(CallContext).GetMyInterviewList();

            return Json(new { IsError = false, Response = result });
        } 
        
        [HttpGet]
        public JsonResult GetJDListByLoginID()
        {
            var result = new RecruitmentBL(CallContext).GetJDListByLoginID();

            return Json(new { IsError = false, Response = result });
        }

        [HttpPost]
        public IActionResult UpdateUserProfile([FromBody] RegisterUserDTO registrationDTO)
        {
            var result = new RecruitmentBL(CallContext).UpdateUserProfile(registrationDTO);
            return Ok(registrationDTO);
        }

        [HttpGet]
        public JsonResult GetNotifications()
        {
            var result = new BoilerPlateBL(CallContext).GetCareerPortalNotificationsForDashBoard();

            return Json(new { IsError = false, Response = result });
        }

        [HttpPost]
        public IActionResult MarkAllasReadNotifications()
        {
            var result = new BoilerPlateBL(CallContext).MarkAllasReadNotifications();

            return Json(new { IsError = result.operationResult == Framework.Contracts.Common.Enums.OperationResult.Success ? false : true, UserMessage = result.Message });

        }
        

        private readonly string _dashBoardBannerImgFolder= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/DashBoardBannerImgFolder");

        public IActionResult DashBoardBannerImages()
        {
            var imageFiles = Directory.GetFiles(_dashBoardBannerImgFolder, "*.png") // Or "*.jpg", etc.  
                                      .Select(Path.GetFileName)
                                      .ToList();

            return Json(new { IsError = false, Response = imageFiles });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult MarkJDasAgreed(long iid)
        {
            var result = new RecruitmentBL(CallContext).MarkJDasAgreed(iid); 

            return Json(new { IsError = result.operationResult == Framework.Contracts.Common.Enums.OperationResult.Success ? false : true, UserMessage = result.Message });

        }

    }
}
