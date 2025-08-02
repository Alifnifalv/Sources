using Eduegate.ERP.Admin.Controllers;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Supports;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.ERP.Admin.Areas.Supports.Controllers
{
    [Area("Supports")]
    public class SerialKeyTrackingController : BaseSearchController
    {
        // GET: Supports/SerialKey
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "SerialKeyTracking";
            viewModel.DisplayName = "Tracking serial keys";
            var serialVM = new SerialKeyViewModel();
            serialVM.Transactions = new List<SerialTransactionsViewModel>();
            serialVM.EncryptDecryptKeys = new SerialTransactionEncryptDecrypt();
            serialVM.Transactions.Add(new SerialTransactionsViewModel());
            //serialVM.EncryptDecryptKeys.Add(new SerialTransactionEncryptDecrypt());
            serialVM.EncryptDecryptKeys.Values = new List<SerialTransactionsAfterEncryptDecryptViewModel>();
            serialVM.EncryptDecryptKeys.Values.Add(new SerialTransactionsAfterEncryptDecryptViewModel());
            viewModel.ViewModel = serialVM;
            viewModel.DetailViewModel = new SerialTransactionsViewModel();
            viewModel.IID = ID;
            viewModel.IsSavePanelRequired = false;
            viewModel.JsControllerName = "SerialKeyTrackingController";

            TempData["viewModel"] = viewModel;
            return RedirectToAction("Create", "CRUD", new { area = "Frameworks", ID = ID });
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

    }
}