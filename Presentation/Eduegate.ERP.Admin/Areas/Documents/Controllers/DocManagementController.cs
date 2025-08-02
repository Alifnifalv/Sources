using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Logger;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Documents.Controllers
{
    public class DocManagementController : BaseSearchController
    {
        // GET: Documents/DocManagement
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.DocManagement);
            return View(new SearchListViewModel
            {
                ControllerName = "Documents/" + Eduegate.Infrastructure.Enums.SearchView.DocManagement,
                ViewName = Eduegate.Infrastructure.Enums.SearchView.DocManagement,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                IsCommentLink = true,
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                InfoBar = @"<li class='status-label-mobile'>
                                        <div class='right status-label'>
                                            <div class='status-label-color'><label class='status-color-label  green'></label>Complete</div>
                                            <div class='status-label-color'><label class='status-color-label red'></label>Fail</div>
                                             <div class='status-label-color'><label class='status-color-label Yellow'></label>Others</div>
                                        </div>
                                    </li>",

                IsChild = false,
                HasChild = true,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            var view = Eduegate.Infrastructure.Enums.SearchView.DocManagement;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
            {
                view = Eduegate.Infrastructure.Enums.SearchView.DocManagement;
                runtimeFilter = runtimeFilter + " AND CompanyID = " + CallContext.CompanyID.ToString();
            }
            else
            {
                runtimeFilter = " CompanyID = " + CallContext.CompanyID.ToString();
            }

            return base.SearchData(view, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.DocManagement);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View();
        }

        [HttpGet]
        public ActionResult Upload()
        {
            return View(new UploadedFileDetailsViewModel());
        }

        [HttpPost]
        public ActionResult UploadDocument()
        {
            var files = new List<UploadedFileDetailsViewModel>();
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                //Save file content goes here
                if (file != null && file.ContentLength > 0)
                {
                    var docVM = new UploadedFileDetailsViewModel();

                    var imageExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
                    var imageFileName = Guid.NewGuid() + file.FileName;
                    var path = ConfigurationExtensions.GetAppConfigValue("DocumentsPhysicalPath");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    docVM.ActualFileName = file.FileName;
                    docVM.FileName = imageFileName;
                    docVM.FilePath = ConfigurationExtensions.GetAppConfigValue("DocumentsVirtualPath") + imageFileName;
                    files.Add(docVM);
                    file.SaveAs(Path.Combine(path, imageFileName));
                }
            }
            return Json(new { Success = true, FileInfo = files }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteUploadedDocument(string fileName)
        {
            bool isSuccessFullyDeleted = false;

            try
            {
                var userID = base.CallContext.LoginID;//Needs to fech from call context
                if (!string.IsNullOrEmpty(fileName))
                {
                    string tempFolderPath = string.Format("{0}\\{1}\\{2}", ConfigurationExtensions.GetAppConfigValue("DocumentsPhysicalPath").ToString(), userID, fileName);

                    if (System.IO.File.Exists(tempFolderPath))
                    {
                        System.IO.File.Delete(tempFolderPath);
                    }
                    isSuccessFullyDeleted = true;

                }
                else
                {
                    var file = string.Format("{0}\\{1}\\{2}", ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), userID);
                    if (System.IO.Directory.Exists(file))
                        System.IO.Directory.Delete(file, true);
                }
            }
            catch (Exception ex)
            {
                LogHelper<DocManagementController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { Success = isSuccessFullyDeleted, Message = (ex.Message), JsonRequestBehavior.AllowGet });
            }

            return Json(new { Success = isSuccessFullyDeleted });
        }

        [HttpPost]
        public JsonResult SaveUploadedFiles(List<UploadedFileDetailsViewModel> files)
        {
            var docsVM = DocumentFileViewModel.FromDTO(ClientFactory.DocumentServiceClient(CallContext)
                .SaveDocuments(UploadedFileDetailsViewModel.ToDocumentVM(files, Eduegate.Framework.Contracts.Common.Enums.EntityTypes.Others)));

            return Json(new { Success = true });
        }
    }
}