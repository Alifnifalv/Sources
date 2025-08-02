using System.Web;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Metadata;
using Eduegate.Framework.Helper;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Filter;
using Eduegate.Web.Library.ViewModels.Common;
using System.Text;
using Eduegate.Web.Library.ViewModels.Settings;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Framework;
using Eduegate.Web.Library.ViewModels.Accounts;
using Eduegate.Web.Library.ViewModels.MenuLinks;
using Eduegate.Services.Client.Factory;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using Eduegate.Services.Contracts.Enums.Common;
using Eduegate.Infrastructure.Enums;
using System.Globalization;
using Eduegate.Services.Contracts.Framework;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Application.Mvc;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.ERP.Admin.Controllers
{
    public class MutualController : BaseController
    {
        protected string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }

        // GET: Settings
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Filter(SearchView view)
        {
            var viewModel = new FilterViewModel() { View = view, ViewName = view.ToString() };
            viewModel.Columns = FilterColumnViewModel.FromDTO(ClientFactory.MetadataServiceClient(CallContext).GetFilterMetadata((Services.Contracts.Enums.SearchView)(int)view));
            viewModel.UserValues = ClientFactory.MetadataServiceClient(CallContext).GetUserFilterMetadata((Services.Contracts.Enums.SearchView)(int)view);
            return View(viewModel);
        }

        [HttpPost]
        public bool SaveFilter([FromBody] List<FilterValueViewModel> filter)
        {
            if (filter == null || filter.Count == 0) return true;
            var filterColumns = FilterColumnViewModel.FromDTO(ClientFactory.MetadataServiceClient(CallContext)
                .GetFilterMetadata((Services.Contracts.Enums.SearchView)(int)filter.First().View));

            var dto = new UserFilterValueDTO();
            dto.ColumnValues = new List<FilterColumnValueDTO>();
            dto.View = (Eduegate.Services.Contracts.Enums.SearchView)(int)filter.FirstOrDefault().View;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            //dto.LoginID = CallContext.
            foreach (var flt in filter.Where(a => a.IsDirty))
            {
                if (!string.IsNullOrEmpty(flt.FilterValue) || flt.FilterValue3 != null)
                {
                    var filterColumnInfo = filterColumns.FirstOrDefault(x => x.FilterColumnID == flt.FilterColumnID);
                    var value = flt.FilterValue3 != null && !string.IsNullOrEmpty(flt.FilterValue3.Value) ? flt.FilterValue3.Value : flt.FilterValue;
                    var value2 = flt.FilterValue2;


                    if (filterColumnInfo.ColumnType == Services.Contracts.Enums.DataTypes.Date)
                    {
                        if (!string.IsNullOrEmpty(value))
                        {
                            try
                            {
                                //conver the date into DB format
                                //value = DateTime.ParseExact(value, dateFormat, CultureInfo.InvariantCulture).ToShortDateString();

                                DateTime dateValue = DateTime.ParseExact(value, dateFormat, CultureInfo.InvariantCulture);
                                value = dateValue.ToString(dateFormat, CultureInfo.InvariantCulture);
                            }
                            catch
                            {
                                value = null;
                            }
                        }

                        if (!string.IsNullOrEmpty(value2))
                        {
                            try
                            {
                                //conver the date into DB format
                                value2 = DateTime.ParseExact(value2, dateFormat, CultureInfo.InvariantCulture).ToShortDateString();
                            }
                            catch
                            {
                                value = null;
                            }
                        }
                    }

                    //TODO need to refactor as part of the header (not in the row)
                    dto.ColumnValues.Add(new FilterColumnValueDTO()
                    {
                        FilterColumnID = flt.FilterColumnID,
                        FilterCondition = (Eduegate.Services.Contracts.Enums.Conditions)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.Conditions), flt.FilterCondition),
                        Value = value,
                        Value2 = value2
                    });
                }
            }

            var result = ClientFactory.MetadataServiceClient(CallContext).SaveUserFilterMetadata(dto);
            return true;
        }

        [HttpPost]
        public bool ResetFilter([FromBody] List<FilterValueViewModel> filter)
        {
            if (filter == null || filter.Count() == 0) return true;

            var dto = new UserFilterValueDTO();
            dto.ColumnValues = new List<FilterColumnValueDTO>();
            dto.View = (Eduegate.Services.Contracts.Enums.SearchView)(int)filter.FirstOrDefault().View;
            var result = ClientFactory.MetadataServiceClient(CallContext).SaveUserFilterMetadata(dto);
            return true;
        }

        [HttpGet]
        public JsonResult GetLookUpData(Eduegate.Services.Contracts.Enums.LookUpTypes lookType, bool defaultBlank = true, bool isBlank = false, int optionalId = 0)
        {
            if (isBlank)
            {
                return Json(new KeyValueViewModel());
            }

            var VM = KeyValueViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetLookUpData((Eduegate.Services.Contracts.Enums.LookUpTypes)((int)lookType), string.Empty, 0, 0));

            if (defaultBlank)
            {
                VM.Insert(0, new KeyValueViewModel() { Key = null, Value = string.Empty });
            }

            return Json(VM);
        }

        [HttpGet]
        public JsonResult GetDBLookUpData(string lookType, bool defaultBlank = true, bool isBlank = false)
        {
            if (isBlank)
            {
                return Json(new KeyValueViewModel());
            }

            var VM = KeyValueViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetDBLookUpData(lookType));

            if (defaultBlank)
            {
                VM.Insert(0, new KeyValueViewModel() { Key = null, Value = string.Empty });
            }

            return Json(VM);
        }

        [HttpGet]
        public JsonResult GetDynamicLookUpData(DynamicLookUpType lookType, bool defaultBlank = true, bool isBlank = false, string searchText = "", string lookupName = "")
        {
            if (isBlank)
            {
                return Json(new KeyValueViewModel());
            }

            var VM = KeyValueViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(lookType, searchText));

            if (defaultBlank)
            {
                VM.Insert(0, new KeyValueViewModel() { Key = null, Value = string.Empty });
            }

            if (string.IsNullOrEmpty(lookupName))
            {
                return Json(VM);
            }
            else
            {
                return Json(new { LookUpName = lookupName, Data = VM });
            }
        }

        [HttpGet]
        public JsonResult GetLazyDBLookUpData(string lookType, string lookupName, string searchText = "")
        {
            var VM = KeyValueViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetDBLookUpData(lookType));
            VM.Insert(0, new KeyValueViewModel() { Key = null, Value = string.Empty });

            return Json(new { LookUpName = lookupName, Data = VM });
        }

        [HttpGet]
        public JsonResult GetLazyLookUpData(Eduegate.Services.Contracts.Enums.LookUpTypes lookType, string lookupName, string searchText = "")
        {
            var VM = KeyValueViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetLookUpData((Eduegate.Services.Contracts.Enums.LookUpTypes)((int)lookType), searchText, Convert.ToInt32(new Domain.Setting.SettingBL().GetSettingValue<string>("Select2DataSize")), 0));
            VM.Insert(0, new KeyValueViewModel() { Key = null, Value = string.Empty });

            return Json(new { LookUpName = lookupName, Data = VM });

        }

        [HttpGet]
        public JsonResult GetLazyLookUpDataByReportField(string columnName, string searchText = "")
        {
            var data = new List<KeyValueViewModel>();
            var keyValueDTOs = new List<KeyValueDTO>();
            var select2Count = new Eduegate.Domain.Setting.SettingBL(CallContext).GetSettingValue<int>("Select2DataSize", 25);

            switch (columnName.ToUpper())
            {
                case "ALLACCOUNTID":
                    data = KeyValueViewModel.FromDTO(ClientFactory
                        .ReferenceDataServiceClient(CallContext)
                        .GetLookUpData(Eduegate.Services.Contracts.Enums.LookUpTypes.Account, searchText,
                        Convert.ToInt32(new Domain.Setting.SettingBL().GetSettingValue<string>("Select2DataSize")), 0));
                    break;

                case "ACCOUNTID":
                    keyValueDTOs = ClientFactory.AccountingTransactionServiceClient(CallContext).Get_AllCustomers_Accounts(searchText);

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    foreach (var dto in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel { Key = dto.Key, Value = dto.Value });
                    }
                    break;

                case "FISCALYEAR_ID":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.FiscalYear, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "COMP_ID":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.Company, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "DOCUMENT_TYPES":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.DocumentTypes, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "MAINGROUP":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.MainAccountGroup, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "SUBGROUP":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.SubAccountGroup, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "GROUP_IDS":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.AccountGroup, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "ACCOUNT_IDS":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.AccountIDs, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "COSTCENTER_IDS":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.CostCenterDetails, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "SL_ACCOUNT_IDS":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.AccountSubLedgers, searchText).ToList();

                    if (keyValueDTOs.Count > select2Count)
                    {
                        keyValueDTOs = keyValueDTOs.Take(select2Count + 1).ToList();
                    }

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "PERIOD_TYPES":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.PeriodicalTypes, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "ACADEMIC_YEAR_IDS":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.AcademicYear, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "CLASS_IDS":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.Classes, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "SECTION_IDS":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.Section, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "FEEMASTER_IDS":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.FeeMaster, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "STUDENTID":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.Student, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    if (keyValueDTOs.Count > select2Count)
                    {
                        keyValueDTOs = keyValueDTOs.Take(select2Count + 1).ToList();
                    }

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "STUDENTIID":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.Student, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    if (keyValueDTOs.Count > select2Count)
                    {
                        keyValueDTOs = keyValueDTOs.Take(select2Count + 1).ToList();
                    }

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "STUDENTIDS":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.Student, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    if (keyValueDTOs.Count > select2Count)
                    {
                        keyValueDTOs = keyValueDTOs.Take(select2Count + 1).ToList();
                    }

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "STUDENT_IDS":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.Student, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    if (keyValueDTOs.Count > select2Count)
                    {
                        keyValueDTOs = keyValueDTOs.Take(select2Count + 1).ToList();
                    }

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "ALL_STUDENTIIDS":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.FullStudents, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    if (keyValueDTOs.Count > select2Count)
                    {
                        keyValueDTOs = keyValueDTOs.Take(select2Count + 1).ToList();
                    }

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "CANDIDATE_IDS":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.Candidate, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    if (keyValueDTOs.Count > select2Count)
                    {
                        keyValueDTOs = keyValueDTOs.Take(select2Count + 1).ToList();
                    }

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "EMPLOYEEID":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.Employee, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "EMPLOYEE_IDS":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.Employee, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "SUB_TEACHER_IDS":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.SubjectTeacher, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "SUBJECTIDS":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.Subject, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "All" });

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "STUD_APPLICANTS":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.Applicant_Student, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "ALL" });

                    if (keyValueDTOs.Count > select2Count)
                    {
                        keyValueDTOs = keyValueDTOs.Take(select2Count + 1).ToList();
                    }

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "PROD_IDS":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.ProductDescription, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "ALL" });

                    if (keyValueDTOs.Count > select2Count)
                    {
                        keyValueDTOs = keyValueDTOs.Take(select2Count + 1).ToList();
                    }

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "PRODUCTIDS":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.ProductDescription, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "ALL" });

                    if (keyValueDTOs.Count > select2Count)
                    {
                        keyValueDTOs = keyValueDTOs.Take(select2Count + 1).ToList();
                    }

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "EXAM_IDS":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.Exams, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "ALL" });

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "FAMILYIDS":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.ProductFamily, searchText).ToList();

                    data.Add(new KeyValueViewModel() { Key = "0", Value = "ALL" });

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "FROMACCNUMBER":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.LibraryBooksAllAccNo, searchText).ToList();

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;

                case "TOACCNUMBER":

                    keyValueDTOs = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.LibraryBooksAllAccNo, searchText).ToList();

                    foreach (var entity in keyValueDTOs)
                    {
                        data.Add(new KeyValueViewModel() { Key = entity.Key, Value = entity.Value });
                    }
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(searchText) && data.Count > 0)
            {
                data.RemoveAt(0);
            }

            return Json(new { LookUpName = columnName, Data = data });
        }

        //TODO: Need to clear errors some items not support in core
        //[HttpPost]
        //public ActionResult UploadImages()
        //{
        //    bool isSavedSuccessfully = false;
        //    List<string> fileNames = new List<string>();
        //    var imageInfoList = new List<UploadedFileDetailsViewModel>();

        //    try
        //    {
        //        var userID = CallContext.LoginID;

        //        string tempFolderPath = string.Format("{0}{1}/{2}/{3}",
        //            new Domain.Setting.SettingBL().GetSettingValue<string>("ImagesPhysicalPath").ToString(), Request.Form["ImageType"], Constants.TEMPFOLDER, userID);

        //        foreach (var fileName in Request.Form.Files)
        //        {
        //            HttpPostedFileBase file = Request.Files[fileName];
        //            //Save file content goes here
        //            if (file != null && file.ContentLength > 0)
        //            {
        //                var imageInfo = new UploadedFileDetailsViewModel();
        //                var imageExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
        //                var imageFileName = Guid.NewGuid() + imageExtension;

        //                if (!Directory.Exists(tempFolderPath))
        //                {
        //                    Directory.CreateDirectory(tempFolderPath);
        //                }

        //                file.SaveAs(tempFolderPath + "/" + imageFileName);

        //                imageInfo.FileName = imageFileName;
        //                imageInfo.FilePath = string.Format("{0}{1}/{2}/{3}/{4}",
        //                    new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl").ToString(), Request.Form["ImageType"], Constants.TEMPFOLDER, userID, imageFileName);
        //                imageInfoList.Add(imageInfo);
        //            }
        //        }

        //        isSavedSuccessfully = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Eduegate.Logger.LogHelper<MutualController>.Fatal(ex.Message.ToString(), ex);
        //        return Json(new { Success = isSavedSuccessfully, Message = (ex.Message) });
        //    }

        //    return Json(new { Success = isSavedSuccessfully, FileInfo = imageInfoList });
        //}

        //TODO: Temporary added code to upload images
        [HttpPost]
        public async Task<IActionResult> UploadImages()
        {
            bool isSavedSuccessfully = false;
            List<string> fileNames = new List<string>();
            var imageInfoList = new List<UploadedFileDetailsViewModel>();

            try
            {
                var userID = CallContext.LoginID;
                var imagePhysicalPath = new Domain.Setting.SettingBL(CallContext).GetSettingValue<string>("ImagesPhysicalPath");
                var imageHostUrl = new Domain.Setting.SettingBL(CallContext).GetSettingValue<string>("ImageHostUrl");

                string tempFolderPath = string.Format("{0}/{1}/{2}/{3}",
                    imagePhysicalPath, Request.Form["ImageType"], Constants.TEMPFOLDER, userID);

                foreach (var formFile in Request.Form.Files)
                {
                    if (formFile.Length > 0)
                    {
                        var imageInfo = new UploadedFileDetailsViewModel();
                        var imageExtension = System.IO.Path.GetExtension(formFile.FileName).ToLower();
                        var imageFileName = Guid.NewGuid() + imageExtension;

                        if (!Directory.Exists(tempFolderPath))
                        {
                            Directory.CreateDirectory(tempFolderPath);
                        }

                        using (var inputStream = new FileStream(tempFolderPath + "/" + imageFileName, FileMode.Create))
                        {
                            // read file to stream
                            await formFile.CopyToAsync(inputStream);
                            // stream to byte array
                            byte[] array = new byte[inputStream.Length];
                            inputStream.Seek(0, SeekOrigin.Begin);
                            inputStream.Read(array, 0, array.Length);
                            // get file name
                            string fName = formFile.FileName;
                        }

                        imageInfo.FileName = imageFileName;
                        imageInfo.FilePath = string.Format("{0}/{1}/{2}/{3}/{4}",
                            imageHostUrl, Request.Form["ImageType"], Constants.TEMPFOLDER, userID, imageFileName);
                        imageInfoList.Add(imageInfo);
                    }
                }

                isSavedSuccessfully = true;
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<MutualController>.Fatal(ex.Message.ToString(), ex);
                return Ok(new { Success = isSavedSuccessfully, Message = (ex.Message) });
            }

            return Ok(new { Success = isSavedSuccessfully, FileInfo = imageInfoList });
        }

        //TODO: Need to clear errors some items not support in core
        //public ActionResult UploadDocument()
        //{
        //    var imageInfo = new List<UploadedFileDetailsViewModel>();
        //    foreach (string fileName in Request.Files)
        //    {
        //        HttpPostedFileBase file = Request.Files[fileName];
        //        //Save file content goes here
        //        if (file != null && file.ContentLength > 0)
        //        {
        //            var image = new UploadedFileDetailsViewModel();

        //            var imageExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
        //            var imageFileName = Guid.NewGuid() + file.FileName;
        //            var path = new Domain.Setting.SettingBL().GetSettingValue<string>("EmploymentRequestPhysicalPath");
        //            if (!Directory.Exists(path))
        //            {
        //                Directory.CreateDirectory(path);
        //            }
        //            image.FileName = imageFileName;
        //            image.FilePath = new Domain.Setting.SettingBL().GetSettingValue<string>("EmploymentRequestPath") + imageFileName;
        //            imageInfo.Add(image);
        //            file.SaveAs(path + "/" + imageFileName);

        //        }
        //    }
        //    return Json(new { Success = true, FileInfo = imageInfo });
        //}

        //TODO: Temporary added code to upload document
        [HttpPost]
        public async Task<IActionResult> UploadDocument()
        {
            bool isSavedSuccessfully = false;
            List<string> fileNames = new List<string>();
            var imageInfoList = new List<UploadedFileDetailsViewModel>();

            try
            {
                var userID = CallContext.LoginID;
                var imagePhysicalPath = new Domain.Setting.SettingBL(CallContext).GetSettingValue<string>("ImagesPhysicalPath");
                var imageHostUrl = new Domain.Setting.SettingBL(CallContext).GetSettingValue<string>("ImageHostUrl");

                string tempFolderPath = string.Format("{0}/{1}/{2}/{3}",
                    imagePhysicalPath, Request.Form["ImageType"], Constants.TEMPFOLDER, userID);

                foreach (var formFile in Request.Form.Files)
                {
                    if (formFile.Length > 0)
                    {
                        var imageInfo = new UploadedFileDetailsViewModel();
                        var imageExtension = System.IO.Path.GetExtension(formFile.FileName).ToLower();
                        var imageFileName = Guid.NewGuid() + imageExtension;

                        if (!Directory.Exists(tempFolderPath))
                        {
                            Directory.CreateDirectory(tempFolderPath);
                        }

                        using (var inputStream = new FileStream(tempFolderPath + "/" + imageFileName, FileMode.Create))
                        {
                            // read file to stream
                            await formFile.CopyToAsync(inputStream);
                            // stream to byte array
                            byte[] array = new byte[inputStream.Length];
                            inputStream.Seek(0, SeekOrigin.Begin);
                            inputStream.Read(array, 0, array.Length);
                            // get file name
                            string fName = formFile.FileName;
                        }

                        imageInfo.FileName = imageFileName;
                        imageInfo.FilePath = string.Format("{0}/{1}/{2}/{3}/{4}",
                            imageHostUrl, Request.Form["ImageType"], Constants.TEMPFOLDER, userID, imageFileName);
                        imageInfoList.Add(imageInfo);
                    }
                }

                isSavedSuccessfully = true;
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<MutualController>.Fatal(ex.Message.ToString(), ex);
                return Ok(new { Success = isSavedSuccessfully, Message = (ex.Message) });
            }

            return Ok(new { Success = isSavedSuccessfully, FileInfo = imageInfoList });
        }

        public async Task<IActionResult> AdvancedFilter(SearchView view)
        {
            var paramview = view;
            var viewModel = new FilterViewModel() { View = paramview, ViewName = paramview.ToString() };
            var filterClient = ClientFactory.MetadataServiceClient(CallContext);
            var metadata = filterClient.GetFilterMetadata((Eduegate.Services.Contracts.Enums.SearchView)view);
            viewModel.Columns = FilterColumnViewModel.FromDTO(metadata).OrderBy(x => x.FilterColumnID).ToList();
            viewModel.UserValues = filterClient.GetUserFilterMetadata((Services.Contracts.Enums.SearchView)(int)view);
            var gridMetadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)view);

            return View(new AdvanceFilterViewModel()
            {
                FilterViewModel = viewModel,
                SearchMetadata = new SearchListViewModel
                {
                    ViewName = paramview,
                    ViewTitle = gridMetadata.ViewName,
                    HeaderList = gridMetadata.Columns,
                    SummaryHeaderList = gridMetadata.SummaryColumns,
                    UserViews = gridMetadata.UserViews,
                    SortColumns = gridMetadata.SortColumns,
                    IsMultilineEnabled = false,
                    IsCategoryColumnEnabled = false,
                    InfoBar = string.Empty,
                    IsEditableLink = false,
                    ActualControllerName = gridMetadata.ControllerName,
                    ViewFullPath = gridMetadata.ViewFullPath,
                }
            });
        }

        [HttpPost]
        public ActionResult ShowCKEditor(CKEditiorViewModel data = null)
        {
            if (data == null)
            {
                data = new CKEditiorViewModel();
            }
            return PartialView(data);
        }

        public ActionResult CustomizeUserView(SearchView view)
        {
            return PartialView(new UserViewCustomizeViewModel() { SearchView = view });
        }

        public JsonResult AvailableViewColumns(SearchView view)
        {
            var vm = new List<KeyValueViewModel>();

            var dtos = ClientFactory.MetadataServiceClient(CallContext).AvailableViewColumns((Eduegate.Services.Contracts.Enums.SearchView)((int)view));
            foreach (var dto in dtos)
            {
                vm.Add(new KeyValueViewModel()
                {
                    Key = dto.ColumnName,
                    Value = string.IsNullOrEmpty(dto.Header) ? dto.ColumnName : dto.Header
                });
            }

            return Json(vm);
        }

        public JsonResult SelectedViewColumns(SearchView view)
        {
            var dto = ClientFactory.ReferenceDataServiceClient(CallContext).SelectedViewColumns((Services.Contracts.Enums.SearchView)(int)view);
            return Json(null);
        }

        [HttpGet]
        public JsonResult GetEntityPropertiesByType(int entityType)
        {
            try
            {
                var VM = KeyValueViewModel.FromDTO(ClientFactory.MutualServiceClient(CallContext).GetEntityPropertiesByType(entityType));
                VM.Insert(0, new KeyValueViewModel() { Key = null, Value = string.Empty });
                return Json(VM);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<MutualController>.Info("Exception : " + ex.Message.ToString());
                return Json(new { IsError = true, ErrorMessage = ex.Message.ToString() });
            }
        }

        [HttpGet]
        public JsonResult GetCountries()
        {
            try
            {
                var dtos = ClientFactory.CountryMasterServiceClient(CallContext).GetCountries();
                var VM = dtos.Select(x => new KeyValueViewModel() { Key = x.CountryID.ToString(), Value = x.CountryName }).ToList();

                VM.Insert(0, new KeyValueViewModel() { Key = null, Value = string.Empty });
                return Json(VM);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<MutualController>.Info("Exception : " + ex.Message.ToString());
                return Json(new { IsError = true, ErrorMessage = ex.Message.ToString() });
            }
        }


        public ActionResult DocumentFile()
        {
            return PartialView(new DocumentFileViewModel());
        }

        [HttpGet]
        public JsonResult GetDocumentFiles(int referenceID, Eduegate.Framework.Contracts.Common.Enums.EntityTypes entityType)
        {
            var docsVM = DocumentFileViewModel.FromDTO(ClientFactory.DocumentServiceClient(CallContext).GetDocuments(referenceID, entityType));

            //if (docsVM.First().DocumentFileIID == 0)
            //    return null;

            if (docsVM.Count > 0)
            {
                foreach (var doc in docsVM)
                {
                    doc.FilePath = string.Format(@"{0}{1}/{2}/{3}/{4}",
                           new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl").ToString(), EduegateImageTypes.Documents.ToString(), entityType.ToString(), referenceID, doc.FileName);
                }
            }



            return Json(docsVM);
        }

        public bool MoveFiles(string sourcePath, string destinationPath)
        {
            var exit = false;

            // Get files from temp if available
            if (Directory.Exists(sourcePath))
            {
                DirectoryInfo di = new DirectoryInfo(sourcePath);
                FileInfo[] files = di.GetFiles();

                if (!Directory.Exists(destinationPath))
                    Directory.CreateDirectory(destinationPath);

                foreach (var file in files)
                {
                    //Move file
                    System.IO.File.Move(file.FullName, Path.Combine(destinationPath, file.Name));
                }
                exit = true;
            }
            return exit;
        }

        [HttpGet]
        public JsonResult GetEntitlements(Eduegate.Framework.Contracts.Common.Enums.EntityTypes type, bool defaultBlank = false)
        {
            var VM = KeyValueViewModel.FromDTO(ClientFactory.MutualServiceClient(CallContext).GetEntityTypeEntitlementByEntityType(type));
            if (defaultBlank)
            {
                VM.Insert(0, new KeyValueViewModel() { Key = null, Value = string.Empty });
            }
            return Json(VM);
        }

        [HttpGet]
        public JsonResult ExecuteView(Eduegate.Services.Contracts.Enums.DynamicViews type, string IID1 = "", string IID2 = "")
        {
            var dto = ClientFactory.MutualServiceClient(CallContext).ExecuteView(type, IID1, IID2);
            return Json(ToJsonString(dto, false));
        }

        [HttpGet]
        public JsonResult ExecuteViewGrid(Eduegate.Services.Contracts.Enums.DynamicViews type, string IID1 = "", string IID2 = "")
        {
            var dto = ClientFactory.MutualServiceClient(CallContext).ExecuteView(type, IID1, IID2);
            return Json(ToJsonString(dto, true));
        }

        private string ToJsonString(Services.Contracts.Search.SearchResultDTO dto, bool isArray = true)
        {
            var jsonString = new StringBuilder();

            if (dto.Rows.Count > 0)
            {
                if (isArray)
                    jsonString.Append("[");
                int i = 0;

                foreach (var dataRow in dto.Rows)
                {
                    jsonString.Append("{");

                    for (int j = 0; j < dto.Columns.Count; j++)
                    {
                        if (j < dto.Columns.Count - 1)
                        {
                            jsonString.Append("\"" + dto.Columns[j].ColumnName.ToString()
                         + "\":"
                         + (dataRow.DataCells[j] == null ? "\"\"" :
                            dataRow.DataCells[j].GetType().Name.Equals("Boolean") ? dataRow.DataCells[j].ToString().ToLower() : "\"" +
                            HttpUtility.HtmlEncode(dataRow.DataCells[j].ToString().Replace("\t", "")) + "\"") + ",");
                        }
                        else if (j == dto.Columns.Count - 1)
                        {
                            jsonString.Append("\"" + dto.Columns[j].ColumnName.ToString()
                         + "\":"
                         + (dataRow.DataCells[j] == null ? "\"\"" :
                         dataRow.DataCells[j].GetType().Name.Equals("Boolean") ? dataRow.DataCells[j].ToString().ToLower() : "\"" +
                         HttpUtility.HtmlEncode(dataRow.DataCells[j].ToString().Replace("\t", "")) + "\""));
                        }
                    }

                    if (i == dto.Rows.Count - 1)
                    {
                        jsonString.Append("}");
                    }
                    else
                    {
                        jsonString.Append("},");
                    }

                    i++;
                }

                if (isArray)
                    jsonString.Append("]");
            }

            return jsonString.ToString();
        }

        public JsonResult GetDocumentTypesByReferenceAndBranch(Services.Contracts.Enums.DocumentReferenceTypes referenceType, long branchID = 0)
        {
            if (branchID == 0) Json(new List<KeyValueViewModel>());

            var vm = DocumentTypeViewModel.ToKeyValueVMs(ClientFactory.ReferenceDataServiceClient(CallContext).GetDocumentTypesByReferenceAndBranch(referenceType, branchID));
            vm.Insert(0, new KeyValueViewModel() { Key = null, Value = string.Empty });
            return Json(vm);
        }

        public JsonResult GetDocumentTypesByID(Services.Contracts.Enums.DocumentReferenceTypes referenceType)
        {
            var VM = new List<KeyValueViewModel>();
            var documentTypeList = ClientFactory.ReferenceDataServiceClient(CallContext).GetDocumentTypesByID(referenceType);
            VM = documentTypeList.Select(x => DocumentTypeViewModel.ToKeyValueVM(x)).ToList();
            VM.Insert(0, new KeyValueViewModel() { Key = null, Value = string.Empty });
            return Json(VM);
        }

        public JsonResult GetDocumentTypesBySystem(Services.Contracts.Enums.Systems system)
        {
            var VM = new List<KeyValueViewModel>();
            var documentTypeList = ClientFactory.ReferenceDataServiceClient(CallContext).GetDocumentTypesBySystem(system);
            VM = documentTypeList.Select(x => DocumentTypeViewModel.ToKeyValueVM(x)).ToList();
            VM.Insert(0, new KeyValueViewModel() { Key = null, Value = string.Empty });
            return Json(VM);
        }

        public JsonResult GetNextTransactionNumber(long documentTypeID)
        {
            var VM = new List<KeyValueViewModel>();
            var transactionNo = ClientFactory.MutualServiceClient(CallContext).GetNextTransactionNumber(documentTypeID);
            return Json(transactionNo);
        }

        public JsonResult GetDeliveryTypes()
        {
            var VM = new List<KeyValueViewModel>();
            var deliveryTypeList = ClientFactory.ReferenceDataServiceClient(CallContext).GetDeliveryTypeMaster();
            VM = deliveryTypeList.Select(x => DeliveryTypeViewModel.ToKeyValueViewModel(x)).ToList();
            VM.Insert(0, new KeyValueViewModel() { Key = null, Value = string.Empty });
            return Json(VM);
        }

        public JsonResult GetAssetCategories()
        {
            var VM = new List<KeyValueViewModel>();
            var assetCategoryList = ClientFactory.FixedAssetServiceClient(CallContext).GetAssetCategories();
            VM = assetCategoryList.Select(x => AssetCategoryViewModel.ToKeyValueViewModel(x)).ToList();
            VM.Insert(0, new KeyValueViewModel() { Key = null, Value = string.Empty });
            return Json(VM);
        }

        public JsonResult GetSuppliers(string searchText = null, int dataSize = 0)
        {
            var VM = new List<KeyValueViewModel>();
            var suppliersDTO = ClientFactory.SupplierServiceClient(CallContext, null).GetSuppliers(searchText, dataSize);
            //List<SupplierDTO> suppliersDTO = JsonConvert.DeserializeObject<List<SupplierDTO>>(getServiceReq);
            VM = suppliersDTO.Select(x => SupplierViewModel.ToKeyValueViewModel(x)).ToList();
            VM.Insert(0, new KeyValueViewModel() { Key = null, Value = string.Empty });
            return Json(VM);
        }

        [HttpGet]
        public string GetNextTransactionNumber(int documentTypeID)
        {
            return ClientFactory.MutualServiceClient(CallContext).GetNextTransactionNumber(documentTypeID);
        }
        [HttpGet]
        public JsonResult GetCustomerEntitlements(long customerID)
        {
            var result = ClientFactory.MutualServiceClient(CallContext).GetCustomerEntitlements(customerID);
            return Json(result);
        }

        #region Comment Methods
        public ActionResult Comment(Eduegate.Infrastructure.Enums.EntityTypes type, long referenceID)
        {
            var commentVM = new CommentViewModel() { EntityType = type, ReferenceID = referenceID };

            // Fill departments dropdown list
            var VM = KeyValueViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetLookUpData((Eduegate.Services.Contracts.Enums.LookUpTypes.Department), string.Empty, 0, 0));
            VM.Insert(0, new KeyValueViewModel() { Key = "0", Value = "All" });
            commentVM.Department = VM;
            return PartialView(commentVM);
        }
        [HttpGet]
        public JsonResult GetComments(Eduegate.Infrastructure.Enums.EntityTypes entityType, long referenceID, long departmentID)
        {
            var dto = ClientFactory.MutualServiceClient(CallContext).GetComments(entityType, referenceID, departmentID);
            var vm = new List<CommentViewModel>();

            vm.AddRange(dto.Select(c => CommentViewModel.FromDTO(c)).ToList());
            return Json(vm);
        }

        [HttpPost]
        public ActionResult AddComment(CommentViewModel comment)
        {
            var dto = ClientFactory.MutualServiceClient(CallContext).SaveComment(CommentViewModel.ToDTO(comment));
            comment = CommentViewModel.FromDTO(dto);

            return Json(comment);
        }

        #endregion

        #region Communication Methods
        [HttpGet]
        public ActionResult Communication(long? headID, long screenID, long? referenceID = (long?)null)
        {
            var comDetail = ClientFactory.MutualServiceClient(CallContext).GetMailIDDetailsFromLead(referenceID);

            ViewBag.ReferenceID = referenceID;
            return PartialView(new CommunicationViewModel()
            {
                ReferenceID = referenceID,
                FromEmail = comDetail.FromEmail,
                ToEmail = comDetail.ToEmail,
                ScreenID = screenID,
                MobileNumber = comDetail.MobileNumber,
            });
        }

        [HttpPost]
        public ActionResult SubmitCommunication(CommunicationViewModel communication)
        {
            var crudSave = ClientFactory.FrameworkServiceClient(CallContext).SaveCRUDData(new CRUDDataDTO() { ScreenID = communication.ScreenID.Value, Data = communication.AsDTOString(communication.ToDTO(CallContext)) });

            return Json(new { IsError = false, Response = crudSave });
        }

        public JsonResult GetMailIDDetailsFromLead(long? leadID)
        {
            var mails = ClientFactory.MutualServiceClient(CallContext).GetMailIDDetailsFromLead(leadID);
            return Json(mails);
        }

        public JsonResult GetEmailTemplateByID(int? templateID)
        {
            var mails = ClientFactory.MutualServiceClient(CallContext).GetEmailTemplateByID(templateID);
            return Json(mails);
        }

        #endregion

        public JsonResult AddAttachment(AttachmentViewModel attachment)
        {
            var dto = ClientFactory.MutualServiceClient(CallContext).SaveAttachment(AttachmentViewModel.ToDTO(attachment));
            attachment = AttachmentViewModel.FromDTO(dto);

            return Json(attachment);
        }

        public ActionResult Attachment(Eduegate.Framework.Contracts.Common.Enums.EntityTypes type, long referenceID)
        {
            var attachmentVM = new AttachmentViewModel() { EntityType = type, ReferenceID = referenceID };

            // Fill departments dropdown list
            var VM = KeyValueViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetLookUpData((Eduegate.Services.Contracts.Enums.LookUpTypes.Department), string.Empty, 0, 0));
            VM.Insert(0, new KeyValueViewModel() { Key = "0", Value = "All" });
            attachmentVM.Department = VM;
            return PartialView(attachmentVM);
        }

        public JsonResult GetAttachments(Eduegate.Framework.Contracts.Common.Enums.EntityTypes entityType, long referenceID, long departmentID)
        {
            var dto = ClientFactory.MutualServiceClient(CallContext).GetAttachments(entityType, referenceID, departmentID);
            var vm = new List<AttachmentViewModel>();

            vm.AddRange(dto.Select(c => AttachmentViewModel.FromDTO(c)).ToList());
            return Json(vm);
        }

        #region Save Delivery Charge

        [HttpPost]
        public JsonResult SaveDeliveryCharges(List<ProductDeliveryTypeViewModel> productDeliveryTypeMaps, long IID, bool isProduct)
        {
            bool result = false;
            List<ProductDeliveryTypeDTO> dtoList = new List<ProductDeliveryTypeDTO>();

            try
            {
                if (productDeliveryTypeMaps.IsNotNull() && productDeliveryTypeMaps.Count > 0)
                {
                    foreach (var pdtMap in productDeliveryTypeMaps)
                    {
                        if (pdtMap.IsDeliveryAvailable == true)
                            dtoList.Add(ProductDeliveryTypeViewModel.ToDTO(pdtMap));
                    }

                    result = ClientFactory.MutualServiceClient(CallContext).SaveDeliveryCharges(dtoList, IID, isProduct);
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<MutualController>.Fatal(exception.Message.ToString(), exception);
                return Json(result);
            }

            return Json(result);
        }

        #endregion

        [HttpPost]
        public JsonResult SaveCustomerDeliveryCharges(List<CustomerGroupDeliveryTypeViewModel> customergroupDeliveryTypeMaps, int customerGroupID)
        {
            bool result = false;
            List<CustomerGroupDeliveryChargeDTO> dtoList = new List<CustomerGroupDeliveryChargeDTO>();

            try
            {
                if (customergroupDeliveryTypeMaps.IsNotNull() && customergroupDeliveryTypeMaps.Count > 0)
                {
                    foreach (var cgdtMap in customergroupDeliveryTypeMaps)
                    {
                        if (cgdtMap.IsDeliveryAvailable == true)
                            dtoList.Add(CustomerGroupDeliveryTypeViewModel.ToDTO(cgdtMap));
                    }

                    result = ClientFactory.MutualServiceClient(CallContext).SaveCustomerDeliveryCharges(dtoList, customerGroupID);

                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<MutualController>.Fatal(exception.Message.ToString(), exception);
                return Json(result);
            }

            return Json(result);
        }

        [HttpPost]
        public JsonResult SaveZoneDeliveryCharges(List<ZoneDeliveryTypeViewModel> DeliveryTypeAllowedZoneMaps, short zoneID)
        {
            bool result = false;
            List<ZoneDeliveryChargeDTO> dtoList = new List<ZoneDeliveryChargeDTO>();

            try
            {
                if (DeliveryTypeAllowedZoneMaps.IsNotNull() && DeliveryTypeAllowedZoneMaps.Count > 0)
                {
                    foreach (var dtzMap in DeliveryTypeAllowedZoneMaps)
                    {
                        if (dtzMap.IsDeliveryAvailable == true)
                            dtoList.Add(ZoneDeliveryTypeViewModel.ToDTO(dtzMap));
                    }

                    result = ClientFactory.MutualServiceClient(CallContext).SaveZoneDeliveryCharges(dtoList, zoneID);

                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<MutualController>.Fatal(exception.Message.ToString(), exception);
                return Json(result);
            }

            return Json(result);
        }

        [HttpPost]
        public JsonResult SaveAreaDeliveryCharges(List<AreaDeliveryTypeViewModel> DeliveryTypeAllowedAreaMaps, int areaID)
        {
            bool result = false;
            List<AreaDeliveryChargeDTO> dtoList = new List<AreaDeliveryChargeDTO>();

            try
            {
                if (DeliveryTypeAllowedAreaMaps.IsNotNull() && DeliveryTypeAllowedAreaMaps.Count > 0)
                {
                    foreach (var dtzMap in DeliveryTypeAllowedAreaMaps)
                    {
                        if (dtzMap.IsDeliveryAvailable == true)
                            dtoList.Add(AreaDeliveryTypeViewModel.ToDTO(dtzMap));
                    }

                    result = ClientFactory.MutualServiceClient(CallContext).SaveAreaDeliveryCharges(dtoList, areaID);

                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<MutualController>.Fatal(exception.Message.ToString(), exception);
                return Json(result);
            }

            return Json(result);
        }


        [HttpGet]
        public JsonResult GetAreaByCountryID(int countryID)
        {
            var areas = new List<AreaDTO>();
            var areaVMList = new List<KeyValueViewModel>();
            var message = "";

            try
            {
                areas = ClientFactory.MutualServiceClient(this.CallContext).GetAreaByCountryID((int)countryID);

                if (areas.IsNotNull() && areas.Count > 0)
                {
                    foreach (var area in areas)
                    {
                        // creating new area vm
                        var vm = new KeyValueViewModel();
                        vm.Key = area.AreaID.ToString();
                        vm.Value = area.AreaName;

                        // Adding this to vm list
                        areaVMList.Add(vm);
                    }
                }
            }
            catch (Exception ex) { message = ex.Message.ToString(); }

            return Json(areaVMList);
        }


        [HttpGet]
        public JsonResult GetAreaByCityID(int cityID)
        {
            var areas = new List<AreaDTO>();
            var areaVMList = new List<KeyValueViewModel>();
            var message = "";

            try
            {
                areas = ClientFactory.MutualServiceClient(base.CallContext).GetAreaByCityID(cityID, 0, 0);
                if (areas.IsNotNull() && areas.Count > 0)
                {
                    foreach (var area in areas)
                    {
                        // creating new area vm
                        var vm = new KeyValueViewModel();
                        vm.Key = area.AreaID.ToString();
                        vm.Value = area.AreaName;

                        // Adding this to vm list
                        areaVMList.Add(vm);
                    }
                }
            }
            catch (Exception ex) { message = ex.Message.ToString(); }

            return Json(areaVMList);
        }
        [HttpGet]
        public JsonResult GetAccountBySupplierID(long? supplierID)
        {
            var accountData = ClientFactory.MutualServiceClient(base.CallContext).GetAccountBySupplierID(supplierID);
            return Json(accountData);
        }
        [HttpGet]
        public JsonResult GetProvisionalAccountByAdditionalExpense(int? additionalExpenseID)
        {
            var accountData = ClientFactory.MutualServiceClient(base.CallContext).GetProvisionalAccountByAdditionalExpense(additionalExpenseID);
            return Json(accountData);
        }

        [HttpGet]
        public JsonResult GetCityByCountryID(int countryID)
        {
            var cities = new List<CityDTO>();
            var cityVMList = new List<KeyValueViewModel>();
            var message = "";

            try
            {
                cities = ClientFactory.MutualServiceClient(base.CallContext).GetCityByCountryID((int)countryID);
                if (cities.IsNotNull() && cities.Count > 0)
                {
                    foreach (var city in cities)
                    {
                        // creating new area vm
                        var vm = new KeyValueViewModel();
                        vm.Key = city.CityID.ToString();
                        vm.Value = city.CityName;

                        // Adding this to vm list
                        cityVMList.Add(vm);
                    }
                }
            }
            catch (Exception ex) { message = ex.Message.ToString(); }

            return Json(cityVMList);
        }

        public JsonResult ClearCache(RepositoryTypes type)
        {
            return Json("Sucessfull cleared the cache.");
        }

        [HttpGet]
        public JsonResult GetMainDesignation(int gdesig_code)
        {
            var VMList = new List<KeyValueViewModel>();
            var mainDesignations = ClientFactory.MutualServiceClient(CallContext).GetMainDesignation(gdesig_code);

            foreach (var mainDesignation in mainDesignations)
            {
                VMList.Add(KeyValueViewModel.ToViewModel(mainDesignation));
            }

            return Json(VMList);
        }

        [HttpGet]
        public JsonResult GetHRDesignation(int mdesig_code)
        {
            var VMList = new List<KeyValueViewModel>();
            var HRDesignations = ClientFactory.MutualServiceClient(CallContext).GetHRDesignation(mdesig_code);

            foreach (var designations in HRDesignations)
            {
                VMList.Add(KeyValueViewModel.ToViewModel(designations));
            }

            return Json(VMList);
        }

        [HttpGet]
        public JsonResult GetAllowancebyPayComp(int paycomp)
        {
            var VMList = new List<KeyValueViewModel>();
            var allowances = ClientFactory.MutualServiceClient(CallContext).GetAllowancebyPayComp(paycomp);

            foreach (var allowance in allowances)
            {
                VMList.Add(KeyValueViewModel.ToViewModel(allowance));
            }

            return Json(VMList);
        }

        [HttpGet]
        public JsonResult GetLocationByDept(int deptCode)
        {
            var VMList = new List<KeyValueViewModel>();
            var allowances = ClientFactory.MutualServiceClient(CallContext).GetLocationByDept(deptCode);

            foreach (var allowance in allowances)
            {
                VMList.Add(KeyValueViewModel.ToViewModel(allowance));
            }

            return Json(VMList);
        }

        [HttpGet]
        public JsonResult GetDocType(int RecruitTypeID)
        {
            var VMList = new List<KeyValueViewModel>();

            var doctype = ClientFactory.MutualServiceClient(CallContext).GetDocType(RecruitTypeID == 2 ? "L" : "F");

            foreach (var doc in doctype)
            {
                VMList.Add(KeyValueViewModel.ToViewModel(doc));
            }

            return Json(VMList);
        }

        [HttpGet]
        public JsonResult GetNextEmpNo(int desigCode, string docType)
        {
            var docNo = ClientFactory.EmployementServiceClient(CallContext).GetNextNo(desigCode, docType);
            return Json(docNo);
        }


        [HttpGet]
        public JsonResult GetAgent()
        {
            var VMList = new List<KeyValueViewModel>();
            var allowances = ClientFactory.MutualServiceClient(CallContext).GetAgent();

            foreach (var allowance in allowances)
            {
                VMList.Add(KeyValueViewModel.ToViewModel(allowance));
            }

            return Json(VMList);
        }

        [HttpGet]
        public async Task<IActionResult> GetERPMenu(Eduegate.Services.Contracts.Enums.MenuLinkTypes menuLinkType)
        {
            var menuDto = await ClientFactory.MenuServiceClient(CallContext).GetERPMenu(menuLinkType);
            var menuTree = MenuLinkViewModel.BuildMenuLinks(menuDto);
            return Json(menuTree);
        }

        [HttpGet]
        public async Task<IActionResult> GetERPMenuFlat(Eduegate.Services.Contracts.Enums.MenuLinkTypes menuLinkType)
        {
            var menuDto = await ClientFactory.MenuServiceClient(CallContext).GetERPMenu(menuLinkType);
            //var menuTree = MenuLinkViewModel.BuildMenuLinks(menuDto);
            return Json(menuDto.OrderBy(a => a.MenuLinkIID));
        }

        [HttpGet]
        public FileContentResult GetDynamicImage(string displayText, bool noisy = true)
        {
            displayText = displayText.ToUpper();
            var spacePosition = displayText.IndexOf(' ');

            if (spacePosition == -1)
            {
                displayText = displayText.Substring(0, 2);
            }
            else
            {
                displayText = displayText.Substring(0, 1) + displayText.Substring(spacePosition + 1, 1);
            }

            var rand = new Random((int)DateTime.Now.Ticks);
            //image stream
            FileContentResult img = null;

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(70, 60))
            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.HighQuality;
                var color = Color.FromArgb(
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)));
                gfx.FillRectangle(Brushes.Blue, new Rectangle(0, 0, bmp.Width, bmp.Height));
                //add question
                gfx.DrawString(displayText, new Font("Tahoma", 30), Brushes.WhiteSmoke, 2, 3);

                //render as Png
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Png);
                img = this.File(mem.GetBuffer(), "image/png");
            }

            return File(img.FileContents, img.ContentType);
        }

        public JsonResult GetTaxTemplateItem(int taxTemplateID)
        {
            var VM = new List<KeyValueViewModel>();
            var taxItem = ClientFactory.ReferenceDataServiceClient(CallContext).GetTaxTemplateItem(taxTemplateID);
            return Json(taxItem);
        }

        public JsonResult GetScreenMetadata(Screens screen)
        {
            return Json(ClientFactory.MutualServiceClient(CallContext).GetShortCuts((long)screen));
        }

        public JsonResult GetNextSequence(string sequenceType)
        {
            return Json(ClientFactory.MutualServiceClient(CallContext).GetNextSequence(sequenceType));
        }

        public JsonResult GetAcademicYearListData()
        {
            var academicyearList = ClientFactory.MutualServiceClient(CallContext).GetAcademicYearListData();
            return Json(academicyearList);
        }

        public JsonResult GetActiveAcademicYearListData()
        {
            var academicyearList = ClientFactory.MutualServiceClient(CallContext).GetActiveAcademicYearListData();
            return Json(academicyearList);
        }

        public JsonResult GetSchoolsProfileWithAcademicYear()
        {
            var academicyearList = ClientFactory.MutualServiceClient(CallContext).GetSchoolsProfileWithAcademicYear();
            return Json(academicyearList);
        }

        [HttpGet]
        public async Task<IActionResult> ExecuteSearchView(Eduegate.Services.Contracts.Enums.SearchView view, string searchText)
        {
            var baseSearch = new BaseSearchController();
            var data = baseSearch.EduegateCustomJsonSerializer(await ClientFactory.SearchServiceClient(CallContext)
                .SearchDataPOST(new Eduegate.Services.Contracts.Search.SearchParameterDTO()
                {
                    SearchView = (Services.Contracts.Enums.SearchView)(int)view,
                    CurrentPage = 1,
                    PageSize = 50,
                    OrderBy = string.Empty,
                    RuntimeFilter = "Title like '%" + searchText + "%'"
                }));
            return Ok(data);
        }

        [HttpGet]
        public JsonResult GetDynamicLookUpDataForReport(string lookupName, string searchText = "")
        {
            var keyValueList = KeyValueViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpDataForReport(lookupName, searchText));

            return Json(keyValueList);
        }

    }
}