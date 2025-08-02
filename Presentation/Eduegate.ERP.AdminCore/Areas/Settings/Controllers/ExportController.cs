using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Client.Factory;
using Eduegate.Application.Mvc;
using Eduegate.Domain;

namespace Eduegate.ERP.Admin.Areas.Settings.Controllers
{
    [Area("Settings")]
    public class ExportController : BaseController
    {
        private static string rootURL { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("RootUrl"); } }
        private static string documentPhysicalPath { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsPhysicalPath"); } }
        private static string documentVirtualPath { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsVirtualPath"); } }
        private static string ExportFolerName = "Export";
        private static string FileExtension = ".xlsx";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ExportData(string viewName)
        {
            bool isSuccessful = false;
            try
            {
                Services.Contracts.Enums.SearchView searchView = (Services.Contracts.Enums.SearchView)Enum.Parse(typeof(Services.Contracts.Enums.SearchView), viewName);
                var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchData(searchView, 1, int.MaxValue, null, string.Empty,'E');
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
                string exportedFile = Eduegate.Utilities.DataExportImport.Export.ToExcel(metadata, Path.Combine(documentPhysicalPath, ExportFolerName, CallContext.LoginID.ToString()), string.Concat(searchView.ToString(), FileExtension), dateFormat);
                string fileServerPath = string.Concat(rootURL, documentVirtualPath, ExportFolerName, "/", CallContext.LoginID.ToString(), "/", exportedFile);
                isSuccessful = true;
                return Json(new { Success = isSuccessful, FilePath = fileServerPath });
            }
            catch (Exception ex)
            {
                string message = "File download was not successful!";
                Eduegate.Logger.LogHelper<ExportController>.Fatal(message, ex);
                return Json(new { Success = isSuccessful, Message = message });
            }

        }
    }
}