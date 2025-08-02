using Eduegate.ERP.Admin.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.ERP.Admin.Areas.Appointments.Controllers
{
    [Area("Appointments")]
    public class AppointmentController : BaseSearchController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            //var viewModel = new CRUDViewModel();
            ////viewModel.Name = "Brand";
            ////viewModel.ListActionName = "Brand";
            //viewModel.ViewModel = new AppointmentViewModel();
            ////viewModel.IID = ID;
            ////viewModel.Urls.Add(new UrlViewModel() { LookUpName = "BrandStatus", Url = "Mutual/GetLookUpData?lookType=BrandStatus" });
            ////viewModel.Urls.Add(new UrlViewModel() { LookUpName = "ImageTypes", Url = "Mutual/GetLookUpData?lookType=ImageTypes" });
            ////viewModel.Urls.Add(new UrlViewModel() { LookUpName = "BrandTags", Url = "Brand/BrandTags" });
            ////TempData["viewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel);
            ////return RedirectToAction("Create", "CRUD");
            //return Json(viewModel.ViewModel);
            return View();
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var client = ClientFactory.AppointmentServiceClient(this.CallContext);
            var result = client.GetAppointment(ID);
            return Json(AppointmentViewModel.FromDTO(result));
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }

        public JsonResult Save([FromBody] AppointmentViewModel vm)
        {
            try
            {
                var vm2 = new AppointmentViewModel();
                return Json(vm2);
            }

            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<AppointmentController>.Fatal(ex.Message.ToString(), ex);
                throw;
            }
        }
    }
}