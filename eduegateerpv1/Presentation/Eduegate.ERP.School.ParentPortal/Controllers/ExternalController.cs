using Eduegate.Domain;
using Eduegate.Frameworks.Mvc.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Web.Library.School.Transports;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Payment;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Eduegate.ERP.School.ParentPortal.Controllers
{
    public class ExternalController : BaseController
    {
        public ActionResult QRCode(long iid , string qr,DateTime time,string student, string className)
        {
            var vm = new StudentPickupRequestViewModel()
            {
                StudentPickerStudentMapIID = iid,
                QRCode = Regex.Replace(qr, @"\s+", "+"),
                Student = new KeyValueViewModel() { Key = "0", Value = student },
                ClassSection = className,
                FromTimeString = time.ToString(),
            };

            return View("QRCodeViewer", vm);
        }

    }
}