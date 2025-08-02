using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Application.Mvc;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.School.Forms;
using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.ERP.School.ParentPortal.Controllers
{
    public class FormBuilderController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CoachingFormHistory()
        {
            return View();
        }

        public ActionResult CoachingForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CoachingFormSubmit()
        {
            try
            {
                var formDatas = this.Request.Form;

                var formID = int.Parse(formDatas["FormID"].ToString());
                var formValues = new List<FormValueDTO>();

                foreach (var form in formDatas)
                {
                    formValues.Add(new FormValueDTO()
                    {
                        FormID = formID,
                        FormFieldName = form.Key.ToString(),
                        FormFieldValue = form.Value.ToString(),
                        ReferenceID = string.IsNullOrEmpty(formDatas["StudentID"]) ? (long?)null : long.Parse(formDatas["StudentID"].ToString()),
                    });
                }

                var data = ClientFactory.FormBuilderServiceClient(CallContext).SaveFormValues(formID, formValues);

                if (data.operationResult == OperationResult.Error)
                {
                    ViewBag.IsError = true;
                    ViewBag.ErrorMessage = data.Message;

                    return View("CoachingForm", "_Layout");
                }
                else
                {
                    return View("CoachingFormHistory", "_Layout");
                }
            }
            catch (Exception ex)
            {
                ViewBag.IsError = true;
                ViewBag.ErrorMessage = ex.Message;

                return View("CoachingForm", "_Layout");
            }
        }

        [HttpGet]
        public JsonResult GetCoachingFormList()
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat", 0 , "dd/MM/yyyy");
            var coachingFormDTOs = new List<CoachingFormDTO>();

            var studentDatas = ClientFactory.SchoolServiceClient(CallContext).GetStudentsSiblings(CallContext.LoginID.HasValue ? CallContext.LoginID.Value : 0);

            foreach (var studDet in studentDatas)
            {
                var data = ClientFactory.FormBuilderServiceClient(CallContext).GetFormValuesByFormAndReferenceID(studDet.StudentIID, 1);

                var coachingForm = new CoachingFormDTO();

                foreach (var value in data.FormValues)
                {
                    switch (value.FormFieldName)
                    {
                        case "StudentID":
                            {
                                coachingForm.StudentID = long.Parse(value.FormFieldValue);
                            }
                            break;
                        case "StudentName":
                            {
                                coachingForm.StudentName = value.FormFieldValue;
                            }
                            break;
                        case "AdmissionNumber":
                            {
                                coachingForm.AdmissionNumber = value.FormFieldValue;
                            }
                            break;
                        case "SchoolID":
                            {
                                coachingForm.SchoolID = byte.Parse(value.FormFieldValue);
                            }
                            break;
                        case "SchoolName":
                            {
                                coachingForm.SchoolName = value.FormFieldValue;
                            }
                            break;
                        case "AcademicYearID":
                            {
                                coachingForm.AcademicYearID = int.Parse(value.FormFieldValue);
                            }
                            break;
                        case "AcademicYearName":
                            {
                                coachingForm.AcademicYearName = value.FormFieldValue;
                            }
                            break;
                        case "ClassID":
                            {
                                coachingForm.ClassID = int.Parse(value.FormFieldValue);
                            }
                            break;
                        case "SectionID":
                            {
                                coachingForm.SectionID = int.Parse(value.FormFieldValue);
                            }
                            break;
                        case "Grade":
                            {
                                coachingForm.Grade = value.FormFieldValue;
                            }
                            break;
                        case "ApplicationStatusID":
                            {
                                coachingForm.ApplicationStatusID = value.FormFieldValue;
                            }
                            break;
                        case "ApplicationStatus":
                            {
                                coachingForm.ApplicationStatus = value.FormFieldValue;
                            }
                            break;
                        case "CreatedBy":
                            {
                                coachingForm.CreatedBy = int.Parse(value.FormFieldValue);
                            }
                            break;
                        case "CreatedDate":
                            {
                                coachingForm.AppliedDateString = DateTimeOffset.Parse(value.FormFieldValue).UtcDateTime.ToString(dateFormat);
                            }
                            break;
                        //case "UpdatedBy":
                        //    {
                        //        coachingForm.UpdatedBy = int.Parse(value.FormFieldValue);
                        //    }
                        //    break;
                        //case "UpdatedDate":
                        //    {
                        //        coachingForm.UpdatedDate = DateTime.ParseExact(value.FormFieldValue, dateFormat, CultureInfo.InvariantCulture);
                        //    }
                        //    break;
                    }
                    
                }

                if (!string.IsNullOrEmpty(coachingForm.StudentName))
                {
                    coachingFormDTOs.Add(coachingForm);
                }
            }

            if (coachingFormDTOs == null)
            {
                return Json(new { IsError = true, Response = "There are some issue !" });
            }
            else
            {
                return Json(new { IsError = false, Response = coachingFormDTOs });
            }
        }

    }
}