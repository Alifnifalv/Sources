using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Mappers.Notification.Notifications;
using Eduegate.Domain.Mappers.Notification.Notifications.Helpers;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services.Contracts.School.Students;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Students
{
    public class StudentTransferRequestMapper : DTOEntityDynamicMapper
    {
        List<string> validationFields = new List<string>() { "StudentID" };
        public static StudentTransferRequestMapper Mapper(CallContext context)
        {
            var mapper = new StudentTransferRequestMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StudentTransferRequestDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        public StudentTransferRequestDTO GetTransferApplication(long IID)
        {
            return ToDTO(IID);
        }

        public void DeleteTransferApplication(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StudentTransferRequest.Where(a => a.StudentTransferRequestIID == IID).AsNoTracking().FirstOrDefault();
                if (entity != null)
                {
                    dbContext.StudentTransferRequest.Remove(entity);
                    dbContext.SaveChanges();
                }
            }
        }

        public List<StudentTransferRequestDTO> GetStudentTransferApplication(long studentId)
        {
            var applicationPrefix = new Domain.Setting.SettingBL(null).GetSettingValue<string>("PREFIX_TC_APPL_NO");

            List<StudentTransferRequestDTO> studentTransferDTOList = new List<StudentTransferRequestDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studentTransferDTO = dbContext.StudentTransferRequest.Where(t => t.StudentID == studentId)
                    .Include(i => i.TransferRequestStatus)
                    .OrderByDescending(o => o.StudentTransferRequestIID)
                    .AsNoTracking()
                    .ToList();

                if (studentTransferDTO != null && studentTransferDTO.Count > 0)
                {
                    studentTransferDTOList = studentTransferDTO.Select(studentTransferApplicationGroup => new StudentTransferRequestDTO()
                    {
                        StudentTransferRequestIID = studentTransferApplicationGroup.StudentTransferRequestIID,
                        StudentID = studentTransferApplicationGroup.StudentID,
                        ExpectingRelivingDate = studentTransferApplicationGroup.ExpectingRelivingDate,
                        OtherReason = studentTransferApplicationGroup.OtherReason,
                        TCAppNumber = applicationPrefix + studentTransferApplicationGroup.StudentTransferRequestIID,
                        TransferRequestStatusDescription = studentTransferApplicationGroup.TransferRequestStatus.StatusName,
                        TransferRequestStatusID = studentTransferApplicationGroup.TransferRequestStatus.TransferRequestStatusID,
                        CreatedDate = studentTransferApplicationGroup.CreatedDate,
                    }).ToList();
                }
            }

            return studentTransferDTOList;
        }

        private StudentTransferRequestDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StudentTransferRequest.Where(a => a.StudentTransferRequestIID == IID)
                    .Include(i => i.StudentTransferRequestReason)
                    .Include(i => i.TransferRequestStatus)
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        public List<StudentTransferRequestDTO> StudentDetail(long studentId)
        {
            List<StudentTransferRequestDTO> transferDTO = new List<StudentTransferRequestDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                transferDTO = dbContext.Students.Where(std => std.StudentIID == studentId)
                    .OrderByDescending(o => o.StudentIID)
                    .AsNoTracking()
                    .Select(std => new StudentTransferRequestDTO()
                    {
                        StudentID = std.StudentIID,
                    }).ToList();
            }

            return transferDTO;
        }

        public StudentTransferRequestDTO FillStudentTransferData(long StudentID)
        {
            var transfer = new StudentTransferRequest();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                transfer = dbContext.StudentTransferRequest.Where(std => std.StudentID == StudentID)
                    .Include(i => i.StudentTransferRequestReason)
                    .Include(i => i.TransferRequestStatus)
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .OrderByDescending(o => o.StudentTransferRequestIID)
                    .AsNoTracking().FirstOrDefault();
            }
            return ToDTO(transfer); 
        }

        private StudentTransferRequestDTO ToDTO(StudentTransferRequest entity)
        {
            var applicationPrefix = new Domain.Setting.SettingBL(null).GetSettingValue<string>("PREFIX_TC_APPL_NO");
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var requestDTO = new StudentTransferRequestDTO();
            if (entity != null)
            {
                requestDTO = new StudentTransferRequestDTO()
                {
                    StudentTransferRequestIID = entity.StudentTransferRequestIID,
                    StudentID = entity.StudentID,
                    StudentName = entity.Student == null ? null : entity.Student.AdmissionNumber + "-" + entity.Student.FirstName + " " + entity.Student.MiddleName + " " + entity.Student.LastName,
                    OtherReason = entity.OtherReason,
                    ExpectingRelivingDate = entity.ExpectingRelivingDate,
                    ExpectingRelivingDateString = entity.ExpectingRelivingDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture),
                    TransferRequestStatusID = entity.TransferRequestStatusID,
                    TransferRequestStatusDescription = entity.TransferRequestStatus.StatusName,
                    TransferRequestReasonID = entity.TransferRequestReasonID,
                    StudentTransferRequestReasons = entity.TransferRequestReasonID.HasValue ? entity.StudentTransferRequestReason.Reason : null,
                    IsTransferRequested = entity.IsTransferRequested,
                    Class = entity.Student.Class.ClassDescription,
                    Section = entity.Student.Section.SectionName,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    TCAppNumber = applicationPrefix + entity.StudentTransferRequestIID,
                    CreatedDateString = entity.CreatedDate.HasValue ? entity.CreatedDate.Value.ToString() : null,
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                    IsSchoolChange = entity.IsSchoolChange,
                    IsLeavingCountry = entity.IsLeavingCountry,
                    SchoolRemarks = entity.SchoolRemarks,
                    PositiveAspect = entity.PositiveAspect,
                    Concern = entity.Concern,
                    ContentFileIID = entity?.TCContentID,
                    IsChequeIssued = entity.IsChequeIssued,
                    IsTCCollected = entity.IsTCCollected,
                };
            }
            return requestDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as StudentTransferRequestDTO;
            var errorMessage = string.Empty;

            #region setting Datas
            var workFlowID = new Domain.Setting.SettingBL(null).GetSettingValue<long>("STUDENT_TRANSFER_REQUEST_WORKFLOW_ID");

            var completedStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUD_TRANSFER_STATUS_COMPLETED");
            var processingStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUD_TRANSFER_STATUS_PROCESSING");

            var transfer_FeeDueSettled_stsID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUD_TRANSFER_STATUS_FEEDUESETTELED");
            var student_TransferApplied_stsID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_TRANSFER_APPLIED_STATUSID");
            var student_FinalSettlementCompleted_stsID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_FINALSETTLEMENT_COMPLETED_STATUSID");

            var student_Transferred_stsID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_TRANSFERRED_STATUSID");
            var student_Discontinue_stsID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_DISCONTINUE_STATUSID");
            #endregion

            if ((toDto.TransferRequestStatusID == byte.Parse(completedStatusID)) && (toDto.IsTCCollected == false || toDto.IsChequeIssued == false))
            {
                throw new Exception("The status cannot be modified due to the checks in place for 'Is TC Collected' and 'Is Cheque Issued'!");
            }

            foreach (var field in validationFields)
            {
                var isValid = ValidateField(toDto, field);

                if (isValid.Key.Equals("true"))
                {
                    errorMessage = string.Concat(errorMessage, " ", isValid.Value, " ");
                }
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new Exception(errorMessage);
            }

            if (toDto.StudentTransferRequestIID != 0 && (toDto.TransferRequestStatusID == byte.Parse(completedStatusID) || toDto.TransferRequestStatusID == byte.Parse(transfer_FeeDueSettled_stsID)))
            {
                using (var dbContext1 = new dbEduegateSchoolContext())
                {
                    var transferData = dbContext1.StudentTransferRequest.Where(x => x.StudentID == toDto.StudentID).AsNoTracking().FirstOrDefault();

                    if (toDto.TransferRequestStatusID == transferData.TransferRequestStatusID)
                    {
                        throw new Exception("The request has already been approved/Completed.cannot be saved!");
                    }
                }
            }

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studentDetail = new StudentDTO();

                var stud = dbContext.Students.Where(x => x.StudentIID == toDto.StudentID)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (stud != null)
                {
                    studentDetail = new StudentDTO()
                    {
                        StudentIID = stud.StudentIID,
                        ClassID = stud.ClassID,
                        SchoolID = stud.SchoolID,
                        SectionID = stud.SectionID,
                        SectionName = stud.Section.SectionName,
                        ClassName = stud.Class.ClassDescription,
                        Status = stud.Status,
                        AcademicYearID = stud.AcademicYearID,
                    };
                }

                if (toDto.TransferRequestStatusID == byte.Parse(completedStatusID) || toDto.TransferRequestStatusID == byte.Parse(transfer_FeeDueSettled_stsID))
                {
                    #region
                    //var settingDataReturn = new Domain.Setting.SettingBL(null).GetSettingValue<string>("LIBRARY_TRANSACTION_TYP_RETURN");
                    //var returnID = int.Parse(settingDataReturn);
                    #endregion

                    var blockedTransactionTypes = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSFER_REQ_BLOCKED_LIBRARY_TRANSACTION_TYPES");
                    var blockedTransactionTypeIDs = blockedTransactionTypes.Split(',').Select(byte.Parse).ToList();

                    var libraryTransaction = dbContext.LibraryTransactions.Where(x => x.StudentID == toDto.StudentID).OrderByDescending(o => o.LibraryTransactionIID).FirstOrDefault();
                    //(from lt in dbContext.LibraryTransactions.AsEnumerable() where lt.StudentID == toDto.StudentID && lt.LibraryTransactionTypeID == 1 && lt.IsReturned == false select lt).LastOrDefault();
                    if (libraryTransaction != null && blockedTransactionTypeIDs.Contains(libraryTransaction.LibraryTransactionTypeID ?? 0))
                    {
                        throw new Exception("Issued book for this students needs to be returned in the library.So Tranfer Process cannot be completed!");
                    }
                    if ((toDto.IsTransferRequested.HasValue ? toDto.IsTransferRequested.Value : false) == true)
                    {
                        var feeDues = dbContext.StudentFeeDues.Where(x => x.StudentId == toDto.StudentID && (x.CollectionStatus == false && (x.IsCancelled != true || x.IsCancelled == null))).AsNoTracking().ToList();
                        if (studentDetail.Status != byte.Parse(student_FinalSettlementCompleted_stsID) || feeDues.Count > 0)
                        {
                            throw new Exception("Settlement Process not yet completed .So Tranfer Process cannot be completed!");
                        }
                    }
                }

                if (toDto.TransferRequestStatusID == byte.Parse(processingStatusID))
                {
                    #region
                    //var settingDataReturn = new Domain.Setting.SettingBL(null).GetSettingValue<string>("LIBRARY_TRANSACTION_TYP_RETURN");
                    //var returnID = int.Parse(settingDataReturn);
                    #endregion

                    var blockedTransactionTypes = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSFER_REQ_BLOCKED_LIBRARY_TRANSACTION_TYPES");
                    var blockedTransactionTypeIDs = blockedTransactionTypes.Split(',').Select(byte.Parse).ToList();

                    var libraryTransaction = dbContext.LibraryTransactions.Where(x => x.StudentID == toDto.StudentID).OrderByDescending(o => o.LibraryTransactionIID).FirstOrDefault();
                    //(from lt in dbContext.LibraryTransactions.AsEnumerable() where lt.StudentID == toDto.StudentID && lt.LibraryTransactionTypeID == 1 && lt.IsReturned == false select lt).LastOrDefault();
                    if (libraryTransaction != null && blockedTransactionTypeIDs.Contains(libraryTransaction.LibraryTransactionTypeID ?? 0))
                    {
                        throw new Exception("Issued book for this students needs to be returned in the library.So Tranfer Process cannot be completed!");
                    }

                    try
                    {
                        if (toDto.IsTransferRequested == true)
                        {
                            var tcRequest = dbContext.StudentTransferRequest
                                .Where(x => x.StudentID == toDto.StudentID && x.StudentTransferRequestIID == toDto.StudentTransferRequestIID)
                                .AsNoTracking().FirstOrDefault();
                            //var isMailSent = ;
                            if (tcRequest.IsMailSent == false || tcRequest.IsMailSent == null)
                            {
                                //var message = TCFeeDueGeneration(toDto);
                                bool? result = TransferFeeDueGeneration(toDto);

                                if(result == true)
                                {
                                    var mailSent = TCMailSend(toDto);
                                    if (mailSent == true)
                                    {
                                        toDto.IsMailSent = true;
                                    }
                                }
                            }
                        }
                        //Update student status to Transfer Applied when Transfer status changes to Processing
                        var entityStudent = dbContext.Students
                            .Where(x => x.StudentIID == toDto.StudentID && x.IsActive == true)
                            .FirstOrDefault();

                        entityStudent.Status = byte.Parse(student_TransferApplied_stsID);

                        dbContext.Entry(entityStudent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        dbContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }

                var entity = new StudentTransferRequest()
                {
                    StudentTransferRequestIID = toDto.StudentTransferRequestIID,
                    StudentID = toDto.StudentID == null ? null : toDto.StudentID,
                    OtherReason = toDto.OtherReason == null ? null : toDto.OtherReason,
                    ExpectingRelivingDate = toDto.ExpectingRelivingDate,
                    IsTransferRequested = toDto.IsTransferRequested,
                    TransferRequestStatusID = toDto.TransferRequestStatusID.HasValue ? toDto.TransferRequestStatusID : toDto.StudentTransferRequestIID == 0 ? 1 : toDto.TransferRequestStatusID,
                    TransferRequestReasonID = toDto.TransferRequestReasonID,
                    SchoolID = _context.SchoolID != null ? (byte)_context.SchoolID : studentDetail.SchoolID,
                    AcademicYearID = toDto.AcademicYearID != null ? toDto.AcademicYearID : studentDetail.AcademicYearID,
                    CreatedBy = toDto.StudentTransferRequestIID == 0 ? (int)_context.LoginID : toDto.CreatedBy,
                    UpdatedBy = toDto.StudentTransferRequestIID > 0 ? (int)_context.LoginID : toDto.UpdatedBy,
                    CreatedDate = toDto.StudentTransferRequestIID == 0 ? DateTime.Now : toDto.CreatedDate,
                    UpdatedDate = toDto.StudentTransferRequestIID > 0 ? DateTime.Now : toDto.UpdatedDate,
                    //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                    IsSchoolChange = toDto.IsSchoolChange,
                    IsLeavingCountry = toDto.IsLeavingCountry,
                    SchoolRemarks = toDto.SchoolRemarks,
                    PositiveAspect = toDto.PositiveAspect,
                    Concern = toDto.Concern,
                    IsMailSent = toDto.IsMailSent,
                    IsChequeIssued = toDto.IsChequeIssued,
                    IsTCCollected = toDto.IsTCCollected,
                };

                using (var dbContext1 = new dbEduegateSchoolContext())
                {
                    if (entity.StudentTransferRequestIID == 0)
                    {
                        var maxGroupID = dbContext1.StudentTransferRequest.Max(a => (long?)a.StudentTransferRequestIID);
                        entity.StudentTransferRequestIID = maxGroupID == null ? 1 : long.Parse(maxGroupID.ToString()) + 1;
                        dbContext1.StudentTransferRequest.Add(entity);
                        dbContext1.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                        Workflows.Helpers.WorkflowGeneratorHelper.GenerateWorkflows(workFlowID, entity.StudentTransferRequestIID);
                    }
                    else
                    {
                        dbContext1.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }

                    dbContext1.SaveChanges();
                }

                if (toDto.TransferRequestStatusID == byte.Parse(completedStatusID) && (toDto.IsTCCollected == true || toDto.IsChequeIssued == true))
                {
                    var entityStudent = dbContext.Students.Where(x => x.StudentIID == toDto.StudentID).AsNoTracking().FirstOrDefault();
                    entityStudent.Status = ((toDto.IsTransferRequested.Value == true) ? byte.Parse(student_Transferred_stsID) : byte.Parse(student_Discontinue_stsID));
                    entityStudent.IsActive = false;
                    entityStudent.InactiveDate = DateTime.Now;
                    dbContext.Students.Add(entityStudent);
                    dbContext.Entry(entityStudent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    #region Update IsActive = False in Student's Transport 
                    var entityTransport = dbContext.StudentRouteStopMaps.Where(x => x.StudentID == toDto.StudentID && x.IsActive == true).AsNoTracking().FirstOrDefault();
                    if (entityTransport != null)
                    {
                        entityTransport.IsActive = false;
                        dbContext.StudentRouteStopMaps.Add(entityTransport);
                        dbContext.Entry(entityTransport).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    #endregion

                    dbContext.SaveChanges();
                }
                
                SaveAlertsNotification(entity);

                return ToDTOString(ToDTO(entity.StudentTransferRequestIID));
            }
        }

        public override KeyValueDTO ValidateField(BaseMasterDTO dto, string field)
        {
            var toDto = dto as StudentTransferRequestDTO;
            var valueDTO = new KeyValueDTO();

            switch (field)
            {
                case "StudentID":
                    if (!string.IsNullOrEmpty(toDto.StudentID.ToString()))
                    {
                        var hasDuplicated = IsStudentDuplicated(toDto.StudentID.ToString(), toDto.StudentTransferRequestIID);
                        if (hasDuplicated)
                        {
                            valueDTO.Key = "true";
                            valueDTO.Value = "Student  already exists, Please try with different Student.";
                        }
                        else
                        {
                            valueDTO.Key = "false";
                        }
                    }
                    else
                    {
                        valueDTO.Key = "false";
                    }
                    break;
            }
            return valueDTO;
        }

        public bool IsStudentDuplicated(string StudentID, long StudentTransferRequestIID)
        {
            // Can apply if the previous one is rejected,ie Considering reject case.
            var student_Transfer_stasusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSPORT_TRANSFER_REJECT_STATUS_ID");

            using (var db = new dbEduegateSchoolContext())
            {
                List<StudentTransferRequest> stud;

                if (StudentTransferRequestIID == 0)
                {
                    stud = db.StudentTransferRequest.Where(x => x.StudentID.ToString() == StudentID && x.TransferRequestStatusID != byte.Parse(student_Transfer_stasusID)).AsNoTracking().ToList();
                }
                else
                {
                    stud = db.StudentTransferRequest.Where(x => x.StudentTransferRequestIID != StudentTransferRequestIID && x.StudentID.ToString() == StudentID && x.TransferRequestStatusID != byte.Parse(student_Transfer_stasusID)).AsNoTracking().ToList();
                }

                if (stud.Count() >= 1)
                {
                    return true;
                }

                return false;
            }
        }

        public void SaveAlertsNotification(StudentTransferRequest entity)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dto = new NotificationAlertsDTO();
                var title = "TC Applied From Student : ";

                var studentDetail = dbContext.Students.Where(x => x.StudentIID == entity.StudentID)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Parent)
                    .AsNoTracking()
                    .FirstOrDefault();

                #region getting data from setting table

                var alertStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ALERTSTATUS_ID");
                var alertType = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ALERTTYPE_ID");
                var superAdminID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("SUPER_ADMIN_LOGIN_ID");
                var transferScreenID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_TC_REQUEST_SCREENID");
                var admissionCordDesigID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("EMPLOYEE_ADMISSION_CORDINATOR_DESIG_ID");
                var cordinatorsDesigID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("IT_CORDINATORS_DESIG_ID");
                #endregion

                var designationID = int.Parse(cordinatorsDesigID);
                var admndesignationID = int.Parse(admissionCordDesigID);
                var screenID = int.Parse(transferScreenID);

                var CordinatorsLoginIDs = dbContext.Employees
                    .Where(y => y.DesignationID == designationID && y.IsActive == true || y.DesignationID == admndesignationID && y.IsActive == true)
                    .AsNoTracking().ToList();

                var message = studentDetail.Class.ClassDescription + ' ' + studentDetail.Section.SectionName + " - " + studentDetail.AdmissionNumber + " - " + studentDetail.FirstName + " " + studentDetail.MiddleName + " " + studentDetail.LastName;

                var settings = NotificationSetting.GetEmployeeAppSettings();

                #region notification entry for IT and Admission cordinators 
                foreach (var em in CordinatorsLoginIDs)
                {
                    var notificationForCordinators = new NotificationAlert()
                    {
                        AlertStatusID = int.Parse(alertStatus),
                        AlertTypeID = int.Parse(alertType),
                        FromLoginID = entity.CreatedBy,
                        ToLoginID = em != null ? em.LoginID : null,
                        Message = title + " " + message,
                        ReferenceID = entity.StudentTransferRequestIID,
                        NotificationDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        CreatedBy = entity.CreatedBy,
                        IsITCordinator = true,
                        ReferenceScreenID = screenID,
                    };

                    dbContext.Entry(notificationForCordinators).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    PushNotificationMapper.SendPushNotification((long)em.LoginID, message, title, settings);
                }
                #endregion

                #region notification entry for Library in Charger if Issued Library exists
                var settingDataReturn = new Domain.Setting.SettingBL(null).GetSettingValue<string>("LIBRARY_TRANSACTION_TYP_RETURN");
                var libraryInChargerID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("LIBRARY_INCHRAGER_CLAMSETID");
                var processingStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUD_TRANSFER_STATUS_PROCESSING");

                var returnID = int.Parse(settingDataReturn);

                var libraryTransaction = dbContext.LibraryTransactions.Where(x => x.StudentID == entity.StudentID).OrderByDescending(o => o.LibraryTransactionIID).FirstOrDefault();

                if ((libraryTransaction != null && libraryTransaction.LibraryTransactionTypeID != returnID) && entity.TransferRequestStatusID == byte.Parse(processingStatusID))
                {
                    using (var dbContext1 = new dbEduegateERPContext())
                    {
                        var libraryMessage = studentDetail.AdmissionNumber + " - " + studentDetail.FirstName + " " + studentDetail.MiddleName + " " + studentDetail.LastName + " has been applied for TC. Please ensure the book has been returned";
                        var libraryInChargerIDs = dbContext1.ClaimSetLoginMaps.Where(a => a.ClaimSetID == long.Parse(libraryInChargerID) && a.Login.Employees.Any(b => b.BranchID == studentDetail.SchoolID))
                            .Include(a => a.Login).ThenInclude(a => a.Employees)
                            .AsNoTracking().ToList();
                        foreach (var em in libraryInChargerIDs)
                        {
                            var notificationForCordinators = new NotificationAlert()
                            {
                                AlertStatusID = int.Parse(alertStatus),
                                AlertTypeID = int.Parse(alertType),
                                FromLoginID = entity.CreatedBy,
                                ToLoginID = em != null ? em.LoginID : null,
                                Message = libraryMessage,
                                ReferenceID = entity.StudentTransferRequestIID,
                                NotificationDate = DateTime.Now,
                                CreatedDate = DateTime.Now,
                                CreatedBy = entity.CreatedBy,
                                ReferenceScreenID = screenID,
                            };

                            dbContext.Entry(notificationForCordinators).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            PushNotificationMapper.SendPushNotification((long)em.LoginID, message, title, settings);
                        }
                    }
                }
                #endregion

                var settingData = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUD_TRANSFER_STATUS_COMPLETED");
                var approvedStatusID = byte.Parse(settingData);

                if (entity.TransferRequestStatusID == approvedStatusID)
                {
                    var messageForParent = studentDetail.AdmissionNumber + " - " + studentDetail.FirstName + " " + studentDetail.MiddleName + " " + studentDetail.LastName + " 's TC Application : TC_" + entity.StudentTransferRequestIID + " is approved";
                    var notificationForParent = new NotificationAlert()
                    {
                        AlertStatusID = int.Parse(alertStatus),
                        AlertTypeID = int.Parse(alertType),
                        FromLoginID = entity.CreatedBy,
                        ToLoginID = studentDetail != null ? studentDetail.Parent.LoginID : null,
                        Message = messageForParent,
                        ReferenceID = entity.StudentTransferRequestIID,
                        NotificationDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        CreatedBy = entity.CreatedBy,
                        IsITCordinator = true,
                        ReferenceScreenID = screenID,
                    };

                    dbContext.Entry(notificationForParent).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                dbContext.SaveChanges();

            }
        }

        public string TCFeeDueGeneration(StudentTransferRequestDTO dto)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studDetails = dbContext.Students.Where(a => a.StudentIID == dto.StudentID).AsNoTracking().FirstOrDefault();

                var academicyearid = studDetails.AcademicYearID;
                var classID = studDetails.ClassID;
                var sectionID = studDetails.SectionID;

                var tcFeeMasterID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSFER_FEE_ID");

                string message = string.Empty;
                string _sFeePeriodIds = null;

                using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetSchoolConnectionString()))
                {
                    conn.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("[schools].[SPS_FEE_DUE_MERGE]", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.Add(new SqlParameter("@ACADEMICYEARID", SqlDbType.Int));
                        sqlCommand.Parameters["@ACADEMICYEARID"].Value = academicyearid;

                        sqlCommand.Parameters.Add(new SqlParameter("@SCHOOLID", SqlDbType.Int));
                        sqlCommand.Parameters["@SCHOOLID"].Value = _context.SchoolID ?? 0;

                        sqlCommand.Parameters.Add(new SqlParameter("@COMPANYID", SqlDbType.Int));
                        sqlCommand.Parameters["@COMPANYID"].Value = _context.CompanyID;

                        sqlCommand.Parameters.Add(new SqlParameter("@INVOICEDATE", SqlDbType.DateTime));
                        sqlCommand.Parameters["@INVOICEDATE"].Value = DateTime.Now;

                        sqlCommand.Parameters.Add(new SqlParameter("@DUEDATE", SqlDbType.DateTime));
                        sqlCommand.Parameters["@DUEDATE"].Value = DateTime.Now;

                        sqlCommand.Parameters.Add(new SqlParameter("@ACCOUNTDATE", SqlDbType.DateTime));
                        sqlCommand.Parameters["@ACCOUNTDATE"].Value = DateTime.Now;

                        sqlCommand.Parameters.Add(new SqlParameter("@CLASSIDs", SqlDbType.VarChar));
                        sqlCommand.Parameters["@CLASSIDs"].Value = string.Empty;

                        sqlCommand.Parameters.Add(new SqlParameter("@STUDENTIDs", SqlDbType.VarChar));
                        sqlCommand.Parameters["@STUDENTIDs"].Value = dto.StudentID;

                        sqlCommand.Parameters.Add(new SqlParameter("@SECTIONIDs", SqlDbType.VarChar));
                        sqlCommand.Parameters["@SECTIONIDs"].Value = string.Empty;

                        sqlCommand.Parameters.Add(new SqlParameter("@LOGINID", SqlDbType.Int));
                        sqlCommand.Parameters["@LOGINID"].Value = _context.LoginID;

                        sqlCommand.Parameters.Add(new SqlParameter("@FEEPERIODIDs", SqlDbType.VarChar));
                        sqlCommand.Parameters["@FEEPERIODIDs"].Value = _sFeePeriodIds;

                        sqlCommand.Parameters.Add(new SqlParameter("@FEEMASTERIDs", SqlDbType.VarChar));
                        sqlCommand.Parameters["@FEEMASTERIDs"].Value = tcFeeMasterID;

                        sqlCommand.Parameters.Add(new SqlParameter("@FINEMASTERIDs", SqlDbType.VarChar));
                        sqlCommand.Parameters["@FINEMASTERIDs"].Value = string.Empty;

                        sqlCommand.Parameters.Add(new SqlParameter("@PARENTIDs", SqlDbType.VarChar));
                        sqlCommand.Parameters["@PARENTIDs"].Value = string.Empty;

                        sqlCommand.Parameters.Add(new SqlParameter("@FEEDUEIDs", SqlDbType.VarChar));
                        sqlCommand.Parameters["@FEEDUEIDs"].Value = string.Empty;

                        sqlCommand.Parameters.Add(new SqlParameter("@FEEDUETYPEMAPIDs", SqlDbType.VarChar));
                        sqlCommand.Parameters["@FEEDUETYPEMAPIDs"].Value = string.Empty;

                        sqlCommand.Parameters.Add(new SqlParameter("@AMOUNT", SqlDbType.Decimal));
                        sqlCommand.Parameters["@AMOUNT"].Value = 0;

                        sqlCommand.Parameters.Add(new SqlParameter("@REMARKS", SqlDbType.VarChar));
                        sqlCommand.Parameters["@REMARKS"].Value = string.Empty;

                        try
                        {
                            // Run the stored procedure.
                            Convert.ToString(sqlCommand.ExecuteScalar() ?? string.Empty);
                            message = null;
                        }
                        catch (Exception ex)
                        {
                            // throw new Exception("Something Wrong! Please check after sometime");
                            message = "0#Error on Saving";
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                }

                return message;
            }

        }

        public bool TCMailSend(StudentTransferRequestDTO toDto)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var emaildata = new EmailNotificationDTO();

                var studentDetails = dbContext.Students.Where(s => s.StudentIID == toDto.StudentID)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.School)
                    .Include(i => i.Parent)
                    .AsNoTracking()
                    .FirstOrDefault();

                var toEmailID = studentDetails.Parent?.GaurdianEmail;

                if (!string.IsNullOrEmpty(toEmailID))
                {
                    string emailDetails = "";
                    string emailSub = "";

                    var emailTC = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TC_MAIL_CONTENT_ID");
                    var emailTCID = int.Parse(emailTC);

                    var TCEmailBody = dbContext.EmailTemplates2.Where(x => x.EmailTemplateID == emailTCID).AsNoTracking().FirstOrDefault();

                    emailDetails = TCEmailBody?.EmailTemplate;
                    //emailDetails = "TC Mail from school";

                    var admissionNo = studentDetails.AdmissionNumber;
                    var studentName = studentDetails.FirstName + " " + studentDetails.MiddleName + " " + studentDetails.LastName;
                    var className = studentDetails.Class?.ClassDescription + ' ' + studentDetails.Section?.SectionName;

                    emailDetails = emailDetails.Replace("{admnNo}", admissionNo);
                    emailDetails = emailDetails.Replace("{studentName}", studentName);
                    emailDetails = emailDetails.Replace("{class}", className);

                    emailSub = "Automatic reply: Student TC Mail";

                    var schoolShortName = studentDetails?.School?.SchoolShortName?.ToLower();

                    if (string.IsNullOrEmpty(schoolShortName))
                    {
                        if (studentDetails.SchoolID.HasValue)
                        {
                            var data = new Eduegate.Domain.Setting.SettingBL(_context).GetSchoolDetailByID(studentDetails.SchoolID.Value);

                            schoolShortName = data?.SchoolShortName?.ToLower();
                        }
                    }

                    var mailParameters = new Dictionary<string, string>()
                    {
                        { "SCHOOL_SHORT_NAME", schoolShortName},
                    };

                    try
                    {
                        string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(toEmailID, emailDetails);

                        var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

                        string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

                        if (emailDetails != "")
                        {
                            if (hostDet.ToLower() == "live")
                            {
                                new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(toEmailID, emailSub, mailMessage, Eduegate.Services.Contracts.Enums.EmailTypes.TCProcess, mailParameters);
                            }
                            else
                            {
                                new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSub, mailMessage, Eduegate.Services.Contracts.Enums.EmailTypes.TCProcess, mailParameters);
                            }
                        }

                    }
                    catch { }
                }

                //var tcRequest = dbContext.StudentTransferRequest.FirstOrDefault(x => x.StudentID == toDto.StudentID && x.StudentTransferRequestIID == toDto.StudentTransferRequestIID);

                //tcRequest.IsMailSent = true;

                //dbContext.Entry(tcRequest).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }

            return true;
            //tcRequest.IsMailSent = true;
        }

        public string UpdateTCStatus(long? studentTransferRequestID, long TCContentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                try
                {
                    var approvalStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_TRANSFER_REQUEST_PENDING_FOR_APPROVAL_ID");

                    var entityStudent = dbContext.StudentTransferRequest.Where(a => a.StudentTransferRequestIID == studentTransferRequestID).AsNoTracking().FirstOrDefault();

                    entityStudent.TransferRequestStatusID = byte.Parse(approvalStatus);
                    entityStudent.TCContentID = TCContentID.ToString();

                    dbContext.Entry(entityStudent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();

                    return "Updated successfully";
                }
                catch (Exception ex)
                {
                    _ = ex.Message;

                    return null;
                }
            }
        }

        public string UpdateTCStatusToComplete(long? studentTransferRequestID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                try
                {
                    //student transfer status changes to Completed/Approved
                    var approvalStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_TRANSFER_REQUEST_APPROVAL_ID");

                    var entityStudent = dbContext.StudentTransferRequest.Where(a => a.StudentTransferRequestIID == studentTransferRequestID).AsNoTracking().FirstOrDefault();

                    entityStudent.TransferRequestStatusID = byte.Parse(approvalStatus);

                    dbContext.Entry(entityStudent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();

                    return "Approved and Updloaded Successfully";
                }
                catch (Exception ex)
                {
                    _ = ex.Message;

                    return null;
                }
            }
        }

        public bool? TransferFeeDueGeneration(StudentTransferRequestDTO dto)
        {
            var result = false;
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var tcFeeMasterID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSFER_FEE_ID");

                var studDetails = dbContext.Students.Where(a => a.StudentIID == dto.StudentID).AsNoTracking().FirstOrDefault();

                int feeMasterID = int.Parse(tcFeeMasterID);

                // Check for duplicates in the StudentFeeDues  
                bool hasDuplicate = dbContext.StudentFeeDues
                    .Include(x => x.FeeDueFeeTypeMaps)
                    .Any(x => x.StudentId == dto.StudentID && x.AcadamicYearID == studDetails.AcademicYearID &&
                               x.FeeDueFeeTypeMaps.Any(y => y.FeeMasterID == feeMasterID));

                if (hasDuplicate)
                {
                    result = false;
                }
                else
                {
                    var feeDues = dbContext.StudentFeeDues.OrderByDescending(x => x.StudentFeeDueIID).FirstOrDefault();

                    var feeMaster = dbContext.FeeMasters.FirstOrDefault(x => x.FeeMasterID == feeMasterID);

                    string currentInvoiceNumber = feeDues.InvoiceNo;
                    string prefix = "INV-";
                    string newInvoiceNumber = null;

                    // Extract the numeric part of the invoice number  
                    string numericPart = currentInvoiceNumber.Substring(prefix.Length);
                    if (int.TryParse(numericPart, out int numericValue))
                    {
                        // Increment the numeric value  
                        numericValue++;

                        // Format the new invoice number (pad with leading zeros if necessary)  
                        newInvoiceNumber = $"{prefix}{numericValue:D7}"; // D7 formats the number to 7 digits  
                    }

                    if (newInvoiceNumber != null)
                    {
                        var entity = new StudentFeeDue()
                        {
                            StudentId = dto.StudentID,
                            ClassId = studDetails?.ClassID,
                            SectionID = studDetails?.SectionID,
                            AcadamicYearID = studDetails?.AcademicYearID,
                            CreatedDate = DateTime.Now,
                            CreatedBy = (int?)_context.LoginID,
                            InvoiceDate = DateTime.Now,
                            DueDate = DateTime.Now,
                            InvoiceNo = newInvoiceNumber,
                            CollectionStatus = false,
                            IsAccountPost = false,
                            SchoolID = studDetails?.SchoolID,
                        };

                        if (feeMaster != null)
                        {
                            entity.FeeDueFeeTypeMaps.Add(new FeeDueFeeTypeMap()
                            {
                                StudentFeeDueID = entity.StudentFeeDueIID,
                                Amount = feeMaster.Amount,
                                CreatedDate = DateTime.Now,
                                CreatedBy = (int?)_context.LoginID,
                                Status = false,
                                FeeMasterID = int.Parse(tcFeeMasterID),
                            });
                        }

                        dbContext.StudentFeeDues.Add(entity);
                        dbContext.SaveChanges();
                    }

                    result = true;
                }

            }

            return result;
        }

    }
}