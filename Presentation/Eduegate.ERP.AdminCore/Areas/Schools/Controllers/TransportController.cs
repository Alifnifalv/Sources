using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Application.Mvc;
using Eduegate.Infrastructure.Enums;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Framework;
using Eduegate.Services.Contracts.Schedulers;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Services.Contracts.Settings;
using Eduegate.Web.Library.CRM.Leads;
using Eduegate.Web.Library.OnlineExam.OnlineExam;
using Eduegate.Web.Library.School.Academics;
using Eduegate.Web.Library.School.Exams;
using Eduegate.Web.Library.School.Fees;
using Eduegate.Web.Library.School.Students;
using Eduegate.Web.Library.School.Transports;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Notification;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Eduegate.Domain;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.Notifications;
using System.Runtime.Serialization.DataContracts;
using System.Runtime.Serialization;
using Microsoft.IdentityModel.Tokens;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Services.Contracts.Enums;
using System.IO;
using Eduegate.Domain.Repository.School;
using Hangfire;
using Eduegate.Domain.Content;
using Eduegate.Services.Contracts.HR.Payroll;

namespace Eduegate.ERP.Admin.Areas.Schools.Controllers
{
    [Area("Schools")]
    public class TransportController : BaseController
    {

        [HttpPost]
        public ActionResult UpdateScheduleLogStatus([FromBody] ScheduleLogDTO dto)
        {
            var result =  ClientFactory.SchoolServiceClient(CallContext).UpdateScheduleLogStatus(dto);
            return Json(result);
        }

    }
}