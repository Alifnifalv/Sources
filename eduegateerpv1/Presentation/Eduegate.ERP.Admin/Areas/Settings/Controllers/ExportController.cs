using System;
using System.IO;
using System.Web.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.Mvc.Controllers;
using Eduegate.Services.Client.Factory;

namespace Eduegate.ERP.Admin.Areas.Settings.Controllers
{
    public class ExportController : BaseController
    {
        private static string rootURL { get { return ConfigurationExtensions.GetAppConfigValue("RootUrl"); } }
        private static string documentPhysicalPath { get { return ConfigurationExtensions.GetAppConfigValue("DocumentsPhysicalPath"); } }
        private static string documentVirtualPath { get { return ConfigurationExtensions.GetAppConfigValue("DocumentsVirtualPath"); } }
        private static string ExportFolerName = "Export";
        private static string FileExtension = ".xlsx";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ExportData(string viewName)
        {
            bool isSuccessful = false;
            try
            {
                Services.Contracts.Enums.SearchView searchView = (Services.Contracts.Enums.SearchView)Enum.Parse(typeof(Services.Contracts.Enums.SearchView), viewName);
                var metadata = ClientFactory.SearchServiceClient(CallContext).SearchData(searchView, 1, int.MaxValue, null, string.Empty,'E');
                string exportedFile = Eduegate.Utilities.DataExportImport.Export.ToExcel(metadata, Path.Combine(documentPhysicalPath, ExportFolerName, CallContext.LoginID.ToString()), string.Concat(searchView.ToString(), FileExtension));
                string fileServerPath = string.Concat(rootURL, documentVirtualPath, ExportFolerName, "/", CallContext.LoginID.ToString(), "/", exportedFile);
                isSuccessful = true;
                return Json(new { Success = isSuccessful, FilePath = fileServerPath, JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                string message = "File download was not successful!";
                Eduegate.Logger.LogHelper<ExportController>.Fatal(message, ex);
                return Json(new { Success = isSuccessful, Message = message, JsonRequestBehavior.AllowGet });
            }

        }
    }
}