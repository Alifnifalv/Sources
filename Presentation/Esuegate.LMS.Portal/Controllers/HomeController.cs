using Microsoft.AspNetCore.Mvc;
using Eduegate.Application.Mvc;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Client.Factory;
using Eduegate.Framework.Enums;
using Eduegate.Domain;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts.Settings;
using Eduegate.Web.Library.Lms.Common;
using System.Threading.Tasks;
using System;
using Eduegate.Framework;
using Eduegate.Web.Library.School.Students;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.School.Academics;
using System.Linq;

namespace Eduegate.Signup.Portal.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            var userDTO = new UserDTO();
            var CompanyID = "1";

            if (CallContext.LoginID == null)
            {
                return Redirect("~/Account/Login");
            }
            else
            {
                if (CallContext.UserRole.IsNullOrEmpty())
                {
                    userDTO = ClientFactory.AccountServiceClient(CallContext).GetUserDetails(CallContext.EmailID);

                    if (userDTO.IsNotNull())
                        ResetCookies(userDTO, int.Parse(CompanyID));
                }

                return View();
            }            
        }

        public JsonResult GetUserDetails()
        {
            var userDetail = ClientFactory.AccountServiceClient(CallContext).GetUserDetails(CallContext.EmailID);

            if (userDetail == null)
            {
                return Json(new { IsError = true, Response = "There are some issue with you login credential, please check with the administrator." });
            }
            else
            {
                if (!string.IsNullOrEmpty(userDetail.ProfileFile))
                {
                    userDetail.ProfileFile = string.Format("{0}/{1}/{2}", new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl"), EduegateImageTypes.UserProfile, userDetail.ProfileFile);
                }

                return Json(new { IsError = false, Response = userDetail });
            }
        }

        public ActionResult Conference()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetParentStudents()
        {
            var studentsDetail = ClientFactory.SchoolServiceClient(CallContext).GetStudentsSiblings(CallContext.LoginID.HasValue ? CallContext.LoginID.Value : 0);

            if (studentsDetail == null)
            {
                return Json(new { IsError = true, Response = "There are some issue !" });
            }
            else
            {
                return Json(new { IsError = false, Response = studentsDetail });
            }
        }

        #region Portal redirect from parent portal
        public ActionResult Meeting(long? loginID)
        {
            var userDTO = new UserDTO();

            loginID = loginID.HasValue ? loginID.Value : 0;

            var loginData = new AccountBL(null).GetLoginDetailByLoginID(loginID.Value);

            if (loginData != null)
            {
                userDTO = ClientFactory.AccountServiceClient(CallContext).GetUserDetails(loginData.LoginEmailID);
                userDTO.UserSettings = new AccountBL(CallContext).GetUserSettings(userDTO.UserID);
                userDTO.CompanyID = 1;

                if (userDTO.IsNotNull())
                    ResetCookies(userDTO, userDTO.CompanyID.Value);
                else
                    return Json(new { IsSuccess = false });

                var result = FillContextAndClaim(loginData);

                return Redirect("Index");
            }
            else
            {
                return Redirect("~/Account/Login");
            }
        }

        public async Task<IActionResult> FillContextAndClaim(Eduegate.Domain.Entity.Models.Login login)
        {
            var claims = new List<Claim>
                            {
                                new System.Security.Claims.Claim(ClaimTypes.Name, login.LoginEmailID),
                                new System.Security.Claims.Claim("FullName", login.LoginEmailID),
                                new System.Security.Claims.Claim(ClaimTypes.UserData,
                                JsonConvert.SerializeObject(new CallContext() {
                                    EmailID = login.LoginEmailID,
                                    LoginID = login.LoginIID,
                                    UserId  = login.LoginUserID,
                                })),
                            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return null;
        }
        #endregion Portal redirect

        public ActionResult UserProfile()
        {
            return View();
        }

        public ActionResult MyLessonPlans()
        {
            return View();
        }

        public ActionResult MyAssignments()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetStudentDetails(long studentId)
        {
            List<StudentDTO> data = ClientFactory.SchoolServiceClient(CallContext).GetStudentDetails(studentId);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat", 0, "dd/MM/yyyy");
            var StudentDetail = new List<StudentProfileViewModel>();
            foreach (var stud in data)
            {
                var std = new StudentProfileViewModel()
                {
                    StudentIID = stud.StudentIID,
                    AdmissionNumber = stud.AdmissionNumber,
                    FirstName = stud.FirstName,
                    MiddleName = stud.MiddleName,
                    LastName = stud.LastName,
                    StudentClass = stud.ClassID.HasValue ? new KeyValueViewModel() { Key = stud.ClassID.ToString(), Value = stud.ClassName } : new KeyValueViewModel(),
                    Section = stud.SectionID.HasValue ? new KeyValueViewModel() { Key = stud.SectionID.ToString(), Value = stud.SectionName } : new KeyValueViewModel(),
                    Gender = stud.GenderID.HasValue ? new KeyValueViewModel() { Key = stud.GenderID.Value.ToString(), Value = stud.GenderName } : new KeyValueViewModel(),
                    Category = stud.CategoryID.HasValue ? new KeyValueViewModel() { Key = stud.CategoryID.ToString(), Value = stud.CategoryName } : new KeyValueViewModel(),
                    Cast = stud.CastID.HasValue ? new KeyValueViewModel() { Key = stud.CastID.Value.ToString(), Value = stud.CastName } : new KeyValueViewModel(),
                    Relegion = stud.RelegionID.HasValue ? new KeyValueViewModel() { Key = stud.RelegionID.Value.ToString(), Value = stud.RelegionName } : new KeyValueViewModel(),
                    Community = stud.CommunityID.HasValue ? new KeyValueViewModel() { Key = stud.CommunityID.Value.ToString(), Value = stud.Community } : new KeyValueViewModel(),
                    BloodGroup = stud.BloodGroupID.HasValue ? new KeyValueViewModel() { Key = stud.BloodGroupID.Value.ToString(), Value = stud.BloodGroupName } : new KeyValueViewModel(),
                    StudentHouse = stud.StudentHouseID.HasValue ? new KeyValueViewModel() { Key = stud.StudentHouseID.Value.ToString(), Value = stud.StudentHouse } : new KeyValueViewModel(),
                    DateOfBirthString = stud.DateOfBirth.HasValue ? stud.DateOfBirth.Value.ToString(dateFormat) : null,
                    AsOnDateString = stud.AsOnDate.HasValue ? stud.AsOnDate.Value.ToString(dateFormat) : null,
                    AdmissionDateString = stud.AdmissionDate.HasValue ? stud.AdmissionDate.Value.ToString(dateFormat) : null,
                    School = stud.SchoolID.HasValue ? new KeyValueViewModel() { Key = stud.SchoolID.ToString(), Value = stud.SchoolName } : new KeyValueViewModel(),
                    LoginID = stud.LoginID.HasValue ? stud.LoginID : (long?)null,
                    EmailID = stud.EmailID,
                    AsOnDate = stud.AsOnDate,
                    DateOfBirth = stud.DateOfBirth,
                    MobileNumber = stud.MobileNumber,
                    StudentHouseID = stud.StudentHouseID,
                    Height = stud.Height,
                    Weight = stud.Weight,
                    PassportNo = stud.StudentPassportDetails?.PassportNo,
                    PassportNoExpiry = stud.StudentPassportDetails?.PassportNoExpiry,
                    AdhaarCardNo = stud.StudentPassportDetails?.AdhaarCardNo,
                    VisaNo = stud.StudentPassportDetails?.VisaNo,
                    VisaExpiry = stud.StudentPassportDetails?.VisaExpiry,
                    NationalIDNo = stud.StudentPassportDetails?.NationalIDNo,
                    NationalIDNoExpiry = stud.StudentPassportDetails?.NationalIDNoExpiry,
                };

                StudentDetail.Add(std);
            }
            if (StudentDetail == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = StudentDetail });
            }

        }

        [HttpGet]
        public JsonResult GetAssignmentStudentwise(long studentId, int? SubjectID, int? page, int? pageSize)
        {
            List<AssignmentDTO> data = ClientFactory.SchoolServiceClient(CallContext).GetAssignmentStudentwise(studentId, SubjectID);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            // Pagination logic
            int currentPage = page ?? 1; // Default to page 1 if not provided
            int defaultPageSize = pageSize ?? 10; // Default page size
            int totalItems = data.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / defaultPageSize);

            var pagedData = data
                .Skip((currentPage - 1) * defaultPageSize)
                .Take(defaultPageSize)
                .ToList();

            var AssignmentList = new List<AssignmentViewViewModel>();
            foreach (var assignment in pagedData) // Iterate over the paged data
            {
                // ... (Your existing mapping logic remains the same)
                var ass = new AssignmentViewViewModel()
                {
                    AssignmentIID = Convert.ToInt32(assignment.AssignmentIID),
                    IsExpand = false,
                    Title = assignment.Title != null ? assignment.Title : null,
                    IsActive = assignment.IsActive != null ? assignment.IsActive : null,
                    Description = assignment.Description != null ? assignment.Description : null,
                    StartDateString = assignment.StartDate?.ToString(dateFormat), // Null-conditional operator
                    FreezeDateString = assignment.FreezeDate?.ToString(dateFormat), // Null-conditional operator
                    SubmissionDateString = assignment.DateOfSubmission?.ToString(dateFormat), // Null-conditional operator
                    ClassID = string.IsNullOrEmpty(assignment.Class?.Key) ? (int?)null : int.Parse(assignment.Class.Key), // Null-conditional operator
                    SubjectID = string.IsNullOrEmpty(assignment.Subject?.Key) ? (int?)null : int.Parse(assignment.Subject.Key), // Null-conditional operator
                    SectionID = string.IsNullOrEmpty(assignment.Section?.Key) ? (int?)null : int.Parse(assignment.Section.Key), // Null-conditional operator
                    AcademicYearID = string.IsNullOrEmpty(assignment.AcademicYear?.Key) ? (int?)null : int.Parse(assignment.AcademicYear.Key), // Null-conditional operator
                    AssignmentTypeId = string.IsNullOrEmpty(assignment.AssignmentType?.Key) ? (byte?)null : byte.Parse(assignment.AssignmentType.Key), // Null-conditional operator
                    AssignmentStatusId = string.IsNullOrEmpty(assignment.AssignmentStatus?.Key) ? (byte?)null : byte.Parse(assignment.AssignmentStatus.Key), // Null-conditional operator
                    StudentClass = assignment.ClassID.HasValue ? new KeyValueViewModel() { Key = assignment.ClassID.ToString(), Value = assignment.Class?.Value } : new KeyValueViewModel(), // Null-conditional operator
                    Subject = assignment.SubjectID.HasValue ? new KeyValueViewModel() { Key = assignment.Subject?.Key?.ToString(), Value = assignment.Subject?.Value } : new KeyValueViewModel(), // Null-conditional operator
                    Section = assignment.SectionID.HasValue ? new KeyValueViewModel() { Key = assignment.Section?.Key?.ToString(), Value = assignment.Section?.Value } : new KeyValueViewModel(), // Null-conditional operator
                    Academic = assignment.AcademicYearID.HasValue ? new KeyValueViewModel() { Key = assignment.AcademicYear?.Key?.ToString(), Value = assignment.AcademicYear?.Value } : new KeyValueViewModel(), // Null-conditional operator
                    AssignmentType = assignment.AssignmentTypeID.HasValue ? new KeyValueViewModel() { Key = assignment.AssignmentType?.Key?.ToString(), Value = assignment.AssignmentType?.Value } : new KeyValueViewModel(), // Null-conditional operator
                    CreatedDateString = assignment.CreatedDate?.ToString(dateFormat), // Null-conditional operator
                    AssignmentStatus = assignment.AssignmentStatusID.HasValue ? new KeyValueViewModel() { Key = assignment.AssignmentStatus?.Key?.ToString(), Value = assignment.AssignmentStatus?.Value } : new KeyValueViewModel(), // Null-conditional operator
                    Attachments = (from attachment in assignment.AssignmentAttachmentMaps
                                   select new AssignmentAttachmentViewModel()
                                   {
                                       AssignmentAttachmentIID = attachment.AssignmentAttachmentMapIID,
                                       ContentFileIID = attachment.AttachmentReferenceID, // No need for HasValue check here if it's nullable
                                       AssignmentID = attachment.AssignmentID, // No need for HasValue check here if it's nullable
                                       ContentFileName = attachment.AttachmentName,
                                       Description = attachment.AttachmentDescription,
                                       Notes = attachment.Notes,
                                   }).ToList(),
                };

                AssignmentList.Add(ass);
            }

            return Json(new
            {
                IsError = false,
                Response = AssignmentList,
                TotalPages = totalPages,
                CurrentPage = currentPage,
                PageSize = defaultPageSize
            });
        }
        
        public JsonResult Getstudentsubjectlist(long studentId)
        {
            List<KeyValueDTO> data = ClientFactory.SchoolServiceClient(CallContext).Getstudentsubjectlist(studentId);


            if (data == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = data });
            }

        }


        [HttpGet]
        public JsonResult GetGuardianDetails(long studentId)
        {
            GuardianDTO data = ClientFactory.SchoolServiceClient(CallContext).GetGuardianDetails(studentId);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat", 0, "dd/MM/yyyy");

            var guardianDetails = new GaurdianViewModel()
            {
                ParentIID = data.ParentIID,
                ParentCode = data.ParentCode,
                FatherFirstName = data.FatherFirstName,
                FatherMiddleName = data.FatherMiddleName,
                FatherLastName = data.FatherLastName,
                MotherFirstName = data.MotherFirstName,
                MotherMiddleName = data.MotherMiddleName,
                MotherLastName = data.MotherLastName,
                GuardianFirstName = data.GuardianFirstName,
                GuardianMiddleName = data.GuardianMiddleName,
                GuardianLastName = data.GuardianLastName,
                FatherEmailID = data.FatherEmailID ?? "NA",
                GaurdianEmail = data.GaurdianEmail ?? "NA",
                MotherEmailID = data.MotherEmailID ?? "NA",
                MotherPhone = data.MotherPhone ?? "NA",
                GuardianPhone = data.GuardianPhone ?? "NA",
                FatherPhoneNumber = data.PhoneNumber ?? "NA",
                FatherMobileNumberTwo = data.FatherMobileNumberTwo ?? "NA",
                FatherPassportNumber = data.FatherPassportNumber ?? "NA",
                MotherPassportNumber = data.MotherPassportNumber ?? "NA",
                MotherNationalID = data.MotherNationalID,
                FatherNationalID = data.FatherNationalID,
                FatherCompanyName = data.FatherCompanyName ?? "NA",
                MotherCompanyName = data.MotherCompanyName ?? "NA",
                MotherCountry = data.MotherCountry ?? "NA",
                FatherCountry = data.FatherCountry ?? "NA",
                FatherOccupation = data.FatherOccupation ?? "NA",
                MotherOccupation = data.MotherOccupation ?? "NA",
                FatherPassportNoExpiryString = data.FatherPassportNoExpiryDate.HasValue ? data.FatherPassportNoExpiryDate.Value.ToString(dateFormat) : "NA",
                FatherPassportNoIssueString = data.FatherPassportNoIssueDate.HasValue ? data.FatherPassportNoIssueDate.Value.ToString(dateFormat) : "NA",
                MotherPassportNoExpiryString = data.MotherPassportNoExpiryDate.HasValue ? data.MotherPassportNoExpiryDate.Value.ToString(dateFormat) : "NA",
                MotherPassportNoIssueString = data.MotherPassportNoIssueDate.HasValue ? data.MotherPassportNoIssueDate.Value.ToString(dateFormat) : "NA",
                MotherNationalDNoIssueDateString = data.MotherNationalDNoIssueDate.HasValue ? data.MotherNationalDNoIssueDate.Value.ToString(dateFormat) : "NA",
                MotherNationaIDNoExpiryDateString = data.MotherNationalDNoExpiryDate.HasValue ? data.MotherNationalDNoExpiryDate.Value.ToString(dateFormat) : "NA",
                FatherNationalDNoIssueDateString = data.FatherNationalDNoIssueDate.HasValue ? data.FatherNationalDNoIssueDate.Value.ToString(dateFormat) : "NA",
                FatherNationalDNoExpiryDateString = data.FatherNationalDNoExpiryDate.HasValue ? data.FatherNationalDNoExpiryDate.Value.ToString(dateFormat) : "NA",
                LocationNo = data.LocationNo ?? "NA",
                BuildingNo = data.BuildingNo ?? "NA",
                FlatNo = data.FlatNo ?? "NA",
                StreetName = data.StreetName ?? "NA",
                StreetNo = data.StreetNo ?? "NA",
                ZipNo = data.ZipNo ?? "NA",
                LocationName = data.LocationName ?? "NA",
                PostBoxNo = data.PostBoxNo ?? "NA",
                City = data.City ?? "NA",
                CountryID = data.CountryID,
                FatherCountryID = data.FatherCountryID,
                MotherCountryID = data.MotherCountryID,
                GuardianTypeID = data.GuardianTypeID,
                Country = data.Country ?? "NA",
            };

            if (guardianDetails == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = guardianDetails });
            }

        }

        public JsonResult UserApplications()
        {
            var userDetail = ClientFactory.SchoolServiceClient(CallContext).GetStudentApplication(CallContext.LoginID.Value);

            if (userDetail == null)
            {
                return Json(new { IsError = true, Response = "There are some issue with you login credential, please check with the administrator." });
            }
            else
            {
                return Json(new { IsError = false, Response = userDetail });
            }
        }
        [HttpGet]
        public JsonResult GetLessonPlanList(long studentID)
        {
            var lessonPlanDetail = ClientFactory.SchoolServiceClient(CallContext).GetLessonPlanList(studentID);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat", 0, "dd/MM/yyyy");

            if (lessonPlanDetail == null)
            {
                return Json(new { IsError = true, Response = "There are some issue !" });
            }
            else
            {
                return Json(new { IsError = false, Response = lessonPlanDetail });
            }
        }

        [HttpGet]
        public JsonResult GetStudentsSiblings(long parentId)
        {
            var siblingsDetail = ClientFactory.SchoolServiceClient(CallContext).GetStudentsSiblings(CallContext.LoginID.HasValue ? CallContext.LoginID.Value : 0);

            if (siblingsDetail == null)
            {
                return Json(new { IsError = true, Response = "There are some issue !" });
            }
            else
            {
                return Json(new { IsError = false, Response = siblingsDetail });
            }
        }

    }
}