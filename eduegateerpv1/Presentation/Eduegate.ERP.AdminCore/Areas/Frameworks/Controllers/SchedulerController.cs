using Eduegate.ERP.Admin.Controllers;
using Eduegate.Application.Mvc;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.ERP.Admin.Areas.Frameworks.Controllers
{
    [Area("Frameworks")]
    public class SchedulerController : BaseController
    {
        // GET: Scheduler
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEntityValues(Eduegate.Framework.Enums.EntityTypes? type = null)
        {
            try
            {
                var VM = new List<KeyValueViewModel>();

                switch (type)
                {
                    case Eduegate.Framework.Enums.EntityTypes.Job:
                        {
                            var documentTypeList = ClientFactory.ReferenceDataServiceClient(CallContext).GetDocumentTypesByID(Services.Contracts.Enums.DocumentReferenceTypes.WarehouseOperations);
                            VM = documentTypeList.Select(x => DocumentTypeViewModel.ToKeyValueVM(x)).ToList();
                        }
                        break;
                    case Eduegate.Framework.Enums.EntityTypes.DocumentType:
                        {
                            var documentTypeList = ClientFactory.ReferenceDataServiceClient(CallContext).GetDocumentTypesByID(Services.Contracts.Enums.DocumentReferenceTypes.All);
                            VM = documentTypeList.Select(x => DocumentTypeViewModel.ToKeyValueVM(x)).ToList();
                        }
                        break;
                    case Eduegate.Framework.Enums.EntityTypes.PurchaseOrder:
                        {
                            var documentTypeList = ClientFactory.ReferenceDataServiceClient(CallContext).GetDocumentTypesByID(Services.Contracts.Enums.DocumentReferenceTypes.PurchaseOrder);
                            VM = documentTypeList.Select(x => DocumentTypeViewModel.ToKeyValueVM(x)).ToList();
                        }
                        break;
                    case Eduegate.Framework.Enums.EntityTypes.SalesOrder:
                        {
                            var documentTypeList = ClientFactory.ReferenceDataServiceClient(CallContext).GetDocumentTypesByID(Services.Contracts.Enums.DocumentReferenceTypes.SalesOrder);
                            VM = documentTypeList.Select(x => DocumentTypeViewModel.ToKeyValueVM(x)).ToList();
                        }
                        break;
                    case Eduegate.Framework.Enums.EntityTypes.PurchaseInvoice:
                        {
                            var documentTypeList = ClientFactory.ReferenceDataServiceClient(CallContext).GetDocumentTypesByID(Services.Contracts.Enums.DocumentReferenceTypes.PurchaseInvoice);
                            VM = documentTypeList.Select(x => DocumentTypeViewModel.ToKeyValueVM(x)).ToList();
                        }
                        break;
                    case Eduegate.Framework.Enums.EntityTypes.SalesInvoice:
                        {
                            var documentTypeList = ClientFactory.ReferenceDataServiceClient(CallContext).GetDocumentTypesByID(Services.Contracts.Enums.DocumentReferenceTypes.SalesInvoice);
                            VM = documentTypeList.Select(x => DocumentTypeViewModel.ToKeyValueVM(x)).ToList();
                        }
                        break;
                }

                return Json(VM);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<MutualController>.Info("Exception : " + ex.Message.ToString());
                return Json(new { IsError = true, ErrorMessage = ex.Message.ToString() });
            }
        }

        [HttpGet]
        public JsonResult GetSchedulerEntities()
        {
            var VM = KeyValueViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetLookUpData((Eduegate.Services.Contracts.Enums.LookUpTypes.SchedulerType), string.Empty, 0, 0));
            VM.Insert(0, new KeyValueViewModel() { Key = null, Value = string.Empty });
            return Json(VM);
        }
    }
}