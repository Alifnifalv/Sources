
using System;
using System.Linq;
using System.Web.Mvc;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Common;
using Eduegate.Frameworks.Mvc.Controllers;
using Newtonsoft.Json.Linq;
using Eduegate.Services.Contracts.Framework;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.Interface;
using Eduegate.Infrastructure.Enums;

namespace Eduegate.ERP.Admin.Areas.Frameworks.Controllers
{
    public class CRUDController : BaseController
    {
        // GET: CRUD
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Get(Screens screen = Screens.None, long ID = 0)
        {
            var data = ClientFactory.FrameworkServiceClient(CallContext).GetScreenData((long)screen, ID);
            var viewModel = CRUDViewModel.FromDTO(ClientFactory.FrameworkServiceClient(CallContext).GetScreenMetadata((long)screen));
            var cultures = ClientFactory.ReferenceDataServiceClient(CallContext).GetCultureList();
            viewModel.ViewModel.Cultures = cultures;
            viewModel.ViewModel = viewModel.ViewModel.ToVM(viewModel.ViewModel.ToDTO(data));
            return Json(viewModel.ViewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(Screens screen = Screens.None, long ID = 0, string parameters = "")
        {
            ScreenMetadataDTO metaData;

            metaData = screen != Screens.None ? ClientFactory.FrameworkServiceClient(CallContext).GetScreenMetadata((long)screen) : null;
            var cultures = CultureDataInfoViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetCultureList());

            var viewModel =
                screen == Screens.None ? TempData["viewModel"] as CRUDViewModel :
                CRUDViewModel.FromDTO(metaData, cultures);

            if (viewModel.UserRoles == null)
            {
                viewModel.UserRoles = Eduegate.Web.Library.ViewModels.Security.UserRoleViewModel.ToVM(ClientFactory.SecurityServiceClient(CallContext).GetUserRoles(CallContext.LoginID.Value));
            }

            viewModel.IID = ID;
            viewModel.Screen = screen;
            TempData["screenData"] = metaData;

            if (viewModel.ScreenTypeID == 1 || viewModel.ScreenTypeID == 0)
            {
                return RedirectToAction("CreateMaster", new { screen = screen, ID = ID, parameters = parameters });
            }
            else
            {
                return RedirectToAction("CreateMasterDetail", new { screen = screen, ID = ID, parameters = parameters });
            }
        }

        public ActionResult CreateMaster(Screens screen = Screens.None, long ID = 0, string parameters = "")
        {
            var cultures = CultureDataInfoViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetCultureList());

            var viewModel =
                screen == Screens.None || TempData["viewModel"] != null ? TempData["viewModel"] as CRUDViewModel :
                CRUDViewModel.FromDTO(TempData["screenData"] != null ? TempData["screenData"] as ScreenMetadataDTO : ClientFactory.FrameworkServiceClient(CallContext).GetScreenMetadata((long)screen), cultures);

            if (viewModel == null || viewModel.Screen != screen)
            {
                viewModel = CRUDViewModel.FromDTO(ClientFactory.FrameworkServiceClient(CallContext).GetScreenMetadata((long)screen), cultures);
            }

            if (viewModel.UserRoles == null)
            {
                viewModel.UserRoles = Eduegate.Web.Library.ViewModels.Security.UserRoleViewModel.ToVM(ClientFactory.SecurityServiceClient(CallContext).GetUserRoles(CallContext.LoginID.Value));
            }

            ExtractRuntimeParameters(viewModel, parameters);
            Helpers.PortalWebHelper.GetDefaultSettingsToViewBag(CallContext, ViewBag);
            viewModel.IID = ID;
            viewModel.Screen = screen;
            //extract meta data
            return View(viewModel);
        }

        public ActionResult CreateMasterDetail(Screens screen, long ID = 0, string parameters = "")
        {
            var cultures = CultureDataInfoViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetCultureList());

            var viewModel = screen == Screens.None || TempData["viewModel"] != null ? TempData["viewModel"] as CRUDMasterDetailViewModel :
                    CRUDMasterDetailViewModel.FromDTO(TempData["screenData"] != null ? TempData["screenData"] as ScreenMetadataDTO : ClientFactory.FrameworkServiceClient(CallContext).GetScreenMetadata((long)screen), cultures);

            if (viewModel == null)
            {
                viewModel = CRUDMasterDetailViewModel.FromDTO(ClientFactory.FrameworkServiceClient(CallContext).GetScreenMetadata((long)screen), cultures);
            }

            if (viewModel.UserRoles == null)
            {
                viewModel.UserRoles = Eduegate.Web.Library.ViewModels.Security.UserRoleViewModel.ToVM(ClientFactory.SecurityServiceClient(CallContext).GetUserRoles(CallContext.LoginID.Value));
            }

            ExtractRuntimeParameters(viewModel, parameters);
            Helpers.PortalWebHelper.GetDefaultSettingsToViewBag(CallContext, ViewBag);
            viewModel.IID = ID;
            viewModel.Screen = screen;
            return View(viewModel);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SaveMaster(string model)
        {
            var jsonData = JObject.Parse(model);
            var screen = (int)(Screens)Enum.Parse(typeof(Screens), jsonData.SelectToken("screen").ToString());
            var viewModel = CRUDViewModel.FromDTO(ClientFactory.FrameworkServiceClient(CallContext).GetScreenMetadata(screen));
            var cultures = ClientFactory.ReferenceDataServiceClient(CallContext).GetCultureList();

            viewModel.ViewModel = viewModel.ViewModel.ToVM(jsonData.SelectToken("data").ToString());
            viewModel.ViewModel.Cultures = cultures;
            //viewModel.ViewModel = viewModel.ViewModel.ToVM(viewModel.ViewModel.ToDTO());
            var crudSave = ClientFactory.FrameworkServiceClient(CallContext).SaveCRUDData(new CRUDDataDTO() { ScreenID = screen, Data = viewModel.ViewModel.AsDTOString(viewModel.ViewModel.ToDTO(CallContext)) });

            if (crudSave.IsError)
            {
                return Json(new { IsError = crudSave.IsError, UserMessage = crudSave.ErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            viewModel.ViewModel.Cultures = cultures;
            return Json(viewModel.ViewModel.ToVM(viewModel.ViewModel.ToDTO(crudSave.Data)), JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SaveMasterDetail(string model)
        {
            var jsonData = JObject.Parse(model);
            var screen = (int)(Screens)Enum.Parse(typeof(Screens), jsonData.SelectToken("screen").ToString());
            var viewModel = CRUDMasterDetailViewModel.FromDTO(ClientFactory.FrameworkServiceClient(CallContext).GetScreenMetadata(screen));
            viewModel.Model = viewModel.Model.ToVM(jsonData.SelectToken("data").ToString());
            var crudSave = ClientFactory.FrameworkServiceClient(CallContext).SaveCRUDData(new CRUDDataDTO() { ScreenID = screen, Data = viewModel.Model.AsDTOString(viewModel.Model.ToDTO()) });

            if (crudSave.IsError)
            {
                return Json(new { IsError = crudSave.IsError, UserMessage = crudSave.ErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            return Json(viewModel.Model.ToVM(crudSave.Data), JsonRequestBehavior.AllowGet);
        }

        private void ExtractRuntimeParameters(ICRUD viewModel, string parameters)
        {
            if (!string.IsNullOrEmpty(parameters))
            {
                foreach (var param in parameters.Split(';'))
                {
                    var paramDetail = param.Split('=');
                    var parse = paramDetail[0];

                    if (parse.IndexOf('.') > 0)
                    {
                        parse = parse.Substring(0, parse.IndexOf('.'));
                    }

                    var binding = paramDetail.Length > 0 ? paramDetail[0] : string.Empty;
                    var value = paramDetail.Length > 1 ? paramDetail[1] : string.Empty;

                    viewModel.Parameters.Add(new KeyValueViewModel()
                    {
                        Key = binding,
                        Value = value
                    });

                    viewModel.ClientParameters.Add(new ClientParameter()
                    {
                        ParameterBindingName = binding,
                        Value = value,
                        ParameterName = parse
                    });

                    //also set the value into the default values
                    var parameter = viewModel.FieldSettings.FirstOrDefault(a => a.ModelName.Trim().Equals(binding.Trim()) || a.FieldName.Trim().Equals(binding.Trim()));

                    if (parameter != null)
                    {
                        parameter.DefaultValue = value;
                    }
                }

                var clientRole = viewModel.Parameters.Where(a => a.Key.Contains("Roles")).FirstOrDefault();

                if (clientRole != null)
                {
                    foreach (var role in clientRole.Value.Split('|'))
                    {
                        viewModel.Claims.Add(new Web.Library.ViewModels.Security.ClaimViewModel()
                        {
                            ResourceName = role,
                            ClaimName = role,
                        });
                    }
                }
            }
        }

        [HttpPost]
        public JsonResult ValidateField(string model)
        {
            var jsonData = JObject.Parse(model);
            var fieldName = jsonData.SelectToken("fieldName").ToString();
            var screen = (int)(Screens)Enum.Parse(typeof(Screens), jsonData.SelectToken("Screen").ToString());
            var viewModel = CRUDMasterDetailViewModel.FromDTO(ClientFactory.FrameworkServiceClient(CallContext).GetScreenMetadata(screen));
            viewModel.Model = viewModel.Model.ToVM(jsonData.SelectToken("data").ToString());
            var keyvalueDTO = ClientFactory.FrameworkServiceClient(CallContext).ValidateField(new CRUDDataDTO() { ScreenID = screen, Data = viewModel.Model.AsDTOString(viewModel.Model.ToDTO()) }, fieldName);
            return Json(new { IsError = keyvalueDTO.Key.ToLower() == "false" ? false : true, UserMessage = keyvalueDTO.Value }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Duplicate(Screens screen = Screens.None, long ID = 0, string parameters = "")
        {
            try
            {
                var newIID = ClientFactory.FrameworkServiceClient(CallContext)
                    .CloneCRUDData((long)screen, ID);

                if (newIID == 0)
                {
                    return Json(new { IsError = true, UserMessage = "Clone failed." }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { IsError = false, UserMessage = "Duplicate created successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                var data = ClientFactory.FrameworkServiceClient(CallContext).GetScreenData((long)screen, ID);
                var cultures = ClientFactory.ReferenceDataServiceClient(CallContext).GetCultureList();
                var viewModel = CRUDViewModel.FromDTO(ClientFactory.FrameworkServiceClient(CallContext).GetScreenMetadata((long)screen),
                    CultureDataInfoViewModel.FromDTO(cultures));
                viewModel.ViewModel.Cultures = cultures;
                viewModel.ViewModel.Context = CallContext;
                viewModel.ViewModel.InitializeVM(CultureDataInfoViewModel.FromDTO(cultures));
                viewModel.ViewModel = viewModel.ViewModel.ToVM(viewModel.ViewModel.ToDTO(data));
                viewModel.ViewModel.Cultures = cultures;
                viewModel.ViewModel.SetIdentityColumnsZero(viewModel.ViewModel);
                var crudSave = ClientFactory.FrameworkServiceClient(CallContext)
                  .SaveCRUDData(new CRUDDataDTO()
                  {
                      ScreenID = (long)screen,
                      Data = viewModel.ViewModel.AsDTOString(viewModel.ViewModel.CloneVM().ToDTO(CallContext))
                  });

                if (crudSave.IsError)
                {
                    return Json(new { IsError = crudSave.IsError, UserMessage = crudSave.ErrorMessage }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { IsError = false, UserMessage = "Duplicate created successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(Screens screen = Screens.None, long ID = 0, string parameters = "")
        {
            var crudSave = ClientFactory.FrameworkServiceClient(CallContext)
              .DeleteCRUDData((long)screen, ID);

            if (!crudSave)
            {
                return Json(new { IsError = true, UserMessage = "Error occured while deleting." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { IsError = false, UserMessage = "Deleted successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}