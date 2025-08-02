using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Mappers.Notification.Notifications;
using Eduegate.Domain.Mappers.Notification.Notifications.Helpers;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Security;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Services.Contracts.School.Transports;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Eduegate.Domain.Mappers.School.Transports
{
    public class StudentPickupRequestMapper : DTOEntityDynamicMapper
    {
        public static StudentPickupRequestMapper Mapper(CallContext context)
        {
            var mapper = new StudentPickupRequestMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StudentPickupRequestDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private StudentPickupRequestDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StudentPickupRequests.Where(X => X.StudentPickupRequestIID == IID)
                    .Include(x => x.Student)
                    .Include(x => x.StudentPickupRequestStatus)
                    .Include(x => x.StudentPickedBy)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private StudentPickupRequestDTO ToDTO(StudentPickupRequest entity)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var timeFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TimeFormatWithoutSecond");

            var routeStopMapDTO = new StudentPickupRequestDTO()
            {
                StudentPickupRequestIID = entity.StudentPickupRequestIID,
                StudentID = entity.StudentID,
                Student = new KeyValueDTO() { Key = entity.StudentID.ToString(), Value = entity.Student.AdmissionNumber + " - " + entity.Student.FirstName + " " + (entity.Student.MiddleName != null ? entity.Student.MiddleName + " " : " ") + entity.Student.LastName },
                RequestDate = entity.RequestDate,
                PickedDate = entity.PickedDate,
                RequestStatusID = entity.RequestStatusID,
                PickedByID = entity.PickedByID,
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                LastName = entity.LastName,
                AdditionalInfo = entity.AdditionalInfo,
                RequestCode = entity.RequestCode,
                RequestCodeImage = entity.RequestCodeImage,
                PhotoContentID = entity.PhotoContentID,
                RequestStatus = entity.RequestStatusID.HasValue ? entity.StudentPickupRequestStatus.StudentPickupRequestStatusName : "NA",
                PickedByDescription = entity.PickedByID.HasValue ? entity.StudentPickedBy.Name : "NA",
                RequestStringDate = entity.RequestDate.HasValue ? entity.RequestDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : "NA",
                PickStringDate = entity.PickedDate.HasValue ? entity.PickedDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : "NA",
                FromTime = entity.FromTime,
                ToTime = entity.ToTime,
                FromStringTime = entity.FromTime.HasValue ? DateTime.Today.Add(entity.FromTime.Value).ToString(timeFormat) : "NA",
                ToStringTime = entity.ToTime.HasValue ? DateTime.Today.Add(entity.ToTime.Value).ToString(timeFormat) : "NA",
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
            };

            return routeStopMapDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as StudentPickupRequestDTO;

            if (toDto.FromTime >= toDto.ToTime)
            {
                throw new Exception("Select Time Properlly!!");
            }

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new StudentPickupRequest()
                {
                    StudentPickupRequestIID = toDto.StudentPickupRequestIID,
                    StudentID = toDto.StudentID,
                    RequestDate = toDto.RequestDate.HasValue ? toDto.RequestDate : DateTime.Now.Date,
                    PickedDate = toDto.PickedDate,
                    RequestStatusID = toDto.RequestStatusID,
                    PickedByID = toDto.PickedByID,
                    FirstName = toDto.FirstName,
                    MiddleName = toDto.MiddleName,
                    LastName = toDto.LastName,
                    AdditionalInfo = toDto.AdditionalInfo,
                    RequestCode = toDto.RequestCode,
                    RequestCodeImage = toDto.RequestCodeImage,
                    PhotoContentID = toDto.PhotoContentID,
                    FromTime = toDto.FromTime,
                    ToTime = toDto.ToTime,
                    CreatedBy = toDto.StudentPickupRequestIID == 0 ? (int)_context.LoginID : toDto.CreatedBy,
                    UpdatedBy = toDto.StudentPickupRequestIID > 0 ? (int)_context.LoginID : toDto.UpdatedBy,
                    CreatedDate = toDto.StudentPickupRequestIID == 0 ? DateTime.Now : toDto.CreatedDate,
                    UpdatedDate = toDto.StudentPickupRequestIID > 0 ? DateTime.Now : toDto.UpdatedDate,
                };

                dbContext.StudentPickupRequests.Add(entity);

                if (entity.StudentPickupRequestIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.StudentPickupRequestIID));
            }
        }

        public List<StudentPickupRequestDTO> GetStudentPickupRequestsByLoginID(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var requestListDTOs = new List<StudentPickupRequestDTO>();

                var students = dbContext.Students.Where(s => s.IsActive == true && s.Parent.LoginID == loginID).AsNoTracking().ToList();

                foreach (var student in students)
                {
                    var pickupRequests = dbContext.StudentPickupRequests.Where(p => p.StudentID == student.StudentIID)
                        .Include(x => x.Student)
                        .Include(x => x.StudentPickupRequestStatus)
                        .Include(x => x.StudentPickedBy)
                    .AsNoTracking().ToList();

                    foreach (var request in pickupRequests)
                    {
                        requestListDTOs.Add(ToDTO(request));
                    }
                }

                var pickupRequestDTOs = new List<StudentPickupRequestDTO>();

                if (requestListDTOs.Count > 0)
                {
                    var sortRequestDTO = requestListDTOs.OrderByDescending(x => x.StudentPickupRequestIID);

                    foreach (var request in sortRequestDTO)
                    {
                        pickupRequestDTOs.Add(request);
                    }
                }

                return pickupRequestDTOs;
            }
        }

        public string CancelStudentPickupRequestByID(long pickupRequestID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var cancelledStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_PICKUP_REQUEST_STATUS_CANCELLED");

                var cancelledStatusID = cancelledStatus != null ? byte.Parse(cancelledStatus) : (byte?)null;

                var pickupRequest = dbContext.StudentPickupRequests.Where(p => p.StudentPickupRequestIID == pickupRequestID).AsNoTracking().FirstOrDefault();

                if (pickupRequest != null && cancelledStatusID != null)
                {
                    pickupRequest.RequestStatusID = cancelledStatusID;
                    pickupRequest.AdditionalInfo = "Cancelled by parent.";
                    pickupRequest.UpdatedBy = (int)_context.LoginID;
                    pickupRequest.UpdatedDate = DateTime.Now;

                    dbContext.Entry(pickupRequest).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    dbContext.SaveChanges();

                    return "Application Cancelled Successfully!";
                }
                else
                {
                    return null;
                }
            }
        }

        public decimal GetPickupRequestsCount(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                decimal pickupRequestCount = 0;

                var students = dbContext.Students.Where(s => s.Parent.LoginID == loginID && s.IsActive == true).AsNoTracking().ToList();

                foreach (var std in students)
                {
                    var pickupRequests = dbContext.StudentPickupRequests.Where(p => p.StudentID == std.StudentIID).AsNoTracking().ToList();

                    if (pickupRequests != null)
                    {
                        pickupRequestCount += pickupRequests.Count;
                    }
                }

                return pickupRequestCount;
            }
        }

        public OperationResultDTO SubmitStudentPickupRequest(StudentPickupRequestDTO studentPickupRequest)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var message = new OperationResultDTO();
                try
                {
                    var requestStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_PICKUP_STATUS_NEW");

                    var requestStatusID = requestStatus != null ? byte.Parse(requestStatus) : (byte?)null;

                    var entity = new StudentPickupRequest()
                    {
                        StudentPickupRequestIID = studentPickupRequest.StudentPickupRequestIID,
                        StudentID = studentPickupRequest.StudentID,
                        RequestDate = studentPickupRequest.RequestDate.HasValue ? studentPickupRequest.RequestDate : DateTime.Now.Date,
                        PickedDate = studentPickupRequest.PickedDate,
                        RequestStatusID = studentPickupRequest.RequestStatusID.HasValue ? studentPickupRequest.RequestStatusID : requestStatusID,
                        PickedByID = studentPickupRequest.PickedByID,
                        FirstName = studentPickupRequest.FirstName,
                        MiddleName = studentPickupRequest.MiddleName,
                        LastName = studentPickupRequest.LastName,
                        AdditionalInfo = studentPickupRequest.AdditionalInfo,
                        FromTime = studentPickupRequest.FromTime,
                        ToTime = studentPickupRequest.ToTime,
                        CreatedBy = studentPickupRequest.StudentPickupRequestIID == 0 ? (int)_context.LoginID : studentPickupRequest.CreatedBy,
                        UpdatedBy = studentPickupRequest.StudentPickupRequestIID > 0 ? (int)_context.LoginID : studentPickupRequest.UpdatedBy,
                        CreatedDate = studentPickupRequest.StudentPickupRequestIID == 0 ? DateTime.Now : studentPickupRequest.CreatedDate,
                        UpdatedDate = studentPickupRequest.StudentPickupRequestIID > 0 ? DateTime.Now : studentPickupRequest.UpdatedDate,
                    };

                    dbContext.StudentPickupRequests.Add(entity);

                    if (entity.StudentPickupRequestIID == 0)
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }

                    dbContext.SaveChanges();

                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = "Saved successfully!"
                    };
                }
                catch (Exception ex)
                {
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = ex.Message
                    };
                }

                return message;
            }
        }



        #region Parent Mobile APP StudentDailyPickup Request Register

        #region Save -- Submit
        public OperationResultDTO SubmitStudentDailyPickupRequest(StudentPickupRequestDTO register)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var message = new OperationResultDTO();

                var newQR = StringCipher.Encrypt(register.QRCODE, register.QRCODE);

                var checQRExist = dbContext.StudentPickerStudentMaps.Where(x => x.QRCODE == newQR && x.IsActive == true).AsNoTracking().ToList();
                if (checQRExist.Count > 0)
                {
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = "This person is already registered, please check !"
                    };

                    return message;
                }

                try
                {
                    var entity = new StudentPicker()
                    {
                        StudentPickerIID = register.StudentPickerIID,
                        ParentID = register.ParentID,
                        PickedByID = register.PickedByID,
                        FirstName = register.FirstName,
                        MiddleName = register.MiddleName,
                        LastName = register.LastName,
                        AdditionalInfo = register.AdditionalInfo,
                        PhotoContentID = register.PhotoContentID,
                        CreatedBy = register.StudentPickerIID == 0 ? (int)_context.LoginID : register.CreatedBy,
                        UpdatedBy = register.StudentPickerIID > 0 ? (int)_context.LoginID : register.UpdatedBy,
                        CreatedDate = register.StudentPickerIID == 0 ? DateTime.Now : register.CreatedDate,
                        UpdatedDate = register.StudentPickerIID > 0 ? DateTime.Now : register.UpdatedDate,
                        VisitorCode = register.VisitorCode?.ToUpper(),
                    };

                    dbContext.Add(entity);

                    entity.StudentPickerStudentMaps.Add(new StudentPickerStudentMap()
                    {
                        StudentPickerStudentMapIID = register.StudentPickerStudentMapIID,
                        StudentPickerID = entity.StudentPickerIID,
                        StudentID = register.StudentID == 0 ? null : register.StudentID,
                        CreatedBy = register.StudentPickerIID == 0 ? (int)_context.LoginID : register.CreatedBy,
                        UpdatedBy = register.StudentPickerIID > 0 ? (int)_context.LoginID : register.UpdatedBy,
                        CreatedDate = register.StudentPickerIID == 0 ? DateTime.Now : register.CreatedDate,
                        UpdatedDate = register.StudentPickerIID > 0 ? DateTime.Now : register.UpdatedDate,
                        QRCODE = StringCipher.Encrypt(register.QRCODE, register.QRCODE),
                        IsActive = true,
                        PickUpLoginID = register.PickUpLoginID,
                    });

                    if (entity.StudentPickerIID == 0)
                    {
                        dbContext.Entry(entity).State = EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(entity).State = EntityState.Modified;
                    }
                    dbContext.SaveChanges();

                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = "Saved successfully!"
                    };
                }
                catch (Exception ex)
                {
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = ex.Message
                    };
                }

                return message;
            }
        }
        #endregion

        #region Get RegisteredLits by ParentLoginID
        public List<StudentPickupRequestDTO> GetRegisteredPickupRequestsByLoginID(long loginID, string barCodeValue)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var registeredListDTOs = new List<StudentPickupRequestDTO>();

                var pickupRequests = new List<StudentPickerStudentMap>();
                var studentDetails = new Student();

                if (barCodeValue != "null")
                {
                    pickupRequests = dbContext.StudentPickerStudentMaps
                                            .AsNoTracking()
                                            .Where(x => x.PickUpLoginID == loginID && x.IsActive == true && x.Student.AdmissionNumber == barCodeValue)
                                            .Include(x => x.Student)
                                            .ThenInclude(x => x.Class)
                                            .Include(x => x.Student)
                                            .ThenInclude(x => x.Section)
                                            .Include(x => x.StudentPicker)
                                            .ThenInclude(x => x.StudentPickedBy)
                                            .OrderByDescending(x => x.StudentPickerStudentMapIID)
                                            .ThenByDescending(y => y.IsActive == true)
                                            .ToList();

                    studentDetails = dbContext.Students
                        .AsNoTracking()
                        .FirstOrDefault(s => s.AdmissionNumber == barCodeValue);
                }

                else
                {
                    pickupRequests = dbContext.StudentPickerStudentMaps
                        .AsNoTracking()
                        .Where(x => x.StudentPicker.Parent.LoginID == loginID)
                        .Include(x => x.Student)
                        .ThenInclude(x => x.Class)
                        .Include(x => x.Student)
                        .ThenInclude(x => x.Section)
                        .Include(x => x.StudentPicker)
                        .ThenInclude(x => x.StudentPickedBy)
                        .ToList()
                        .OrderByDescending(x => x.StudentPickerStudentMapIID).OrderByDescending(y => y.IsActive == true)
                        .ToList();
                }

                foreach (var request in pickupRequests)
                {
                    registeredListDTOs.Add(new StudentPickupRequestDTO()
                    {
                        StudentPickerStudentMapIID = request.StudentPickerStudentMapIID,
                        StudentPickerIID = (long)request.StudentPickerID,
                        CreatedDateString = request.CreatedDate.HasValue ? request.CreatedDate.Value.ToString(dateFormat) : "NA",
                        Student = new KeyValueDTO() { Key = request.Student?.StudentIID.ToString(), Value = request.Student?.AdmissionNumber + " - " + request.Student?.FirstName + " " + request.Student?.MiddleName + " " + request.Student?.LastName },
                        ClassSection = request.Student?.Class?.ClassDescription + " " + request.Student?.Section?.SectionName,
                        PickByFullName = request.StudentPicker?.FirstName + " " + request.StudentPicker?.MiddleName + " " + request.StudentPicker?.LastName,
                        IsActive = request.IsActive,
                        QRCODE = request.QRCODE,
                        PickerProfile = request.StudentPicker?.PhotoContentID.ToString(),
                        VisitorCode = request.StudentPicker?.VisitorCode,
                        AdmissionNumber = request.Student?.AdmissionNumber,
                        StudentID = studentDetails?.StudentIID,
                        StudentProfile = studentDetails?.StudentProfile,
                        LogStatus = null,
                        PickUpBy = request.StudentPicker?.StudentPickedBy?.Name,
                    });
                }

                return registeredListDTOs;
            }
        }
        #endregion

        #region cancel or Active Registration
        public string CancelorActiveStudentPickupRegistration(long studentPickerStudentMapIID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var regData = dbContext.StudentPickerStudentMaps.Where(p => p.StudentPickerStudentMapIID == studentPickerStudentMapIID).AsNoTracking().FirstOrDefault();

                var returnMsg = "";

                if (regData != null)
                {
                    if (regData.IsActive)
                    {
                        regData.IsActive = false;
                        returnMsg = "Registration Cancelled Successfully!";
                    }
                    else
                    {
                        regData.IsActive = true;
                        returnMsg = "Registration Activated Successfully!";
                    }
                    regData.UpdatedBy = (int)_context.LoginID;
                    regData.UpdatedDate = DateTime.Now;

                    dbContext.Entry(regData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    dbContext.SaveChanges();

                    return returnMsg;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region Count for MainMenu
        public decimal GetPickupRegisterCount(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var pickupRequests = dbContext.StudentPickerStudentMaps.Where(s => s.CreatedBy == loginID && s.IsActive == true).AsNoTracking().ToList();

                return pickupRequests != null ? pickupRequests.Count : 0;
            }
        }
        #endregion

        #endregion

        #region Staff App  

        #region Get RegisteredLits by QRCODE
        public StudentPickupRequestDTO GePickupRegisteredDetailsByQR(string qrCode)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var registeredDTOs = new StudentPickupRequestDTO();

                var mapList = dbContext.StudentPickerStudentMaps
                                .Where(x => x.IsActive == true &&  ((x.QRCODE == qrCode) || (x.Student.AdmissionNumber == qrCode)))
                    .Include(i => i.StudentPicker)
                    .ThenInclude(i => i.StudentPickedBy)

                    .Include(i => i.StudentPicker)
                    .ThenInclude(i => i.Parent)
                    .ThenInclude(i => i.Students)

                    .Include(i => i.Student)
                    .ThenInclude(i => i.Parent)

                    .Include(i => i.Student)
                    .ThenInclude(i => i.Class)

                    .Include(i => i.Student)
                    .ThenInclude(i => i.Section)
                    .AsNoTracking().ToList();

                var parentID = mapList.FirstOrDefault().StudentPicker?.ParentID;

                var getParentDetail = mapList.FirstOrDefault().StudentID != null ? mapList.FirstOrDefault().Student?.Parent : dbContext.Parents.Where(p => p.ParentIID == parentID).AsNoTracking().FirstOrDefault();

                registeredDTOs = new StudentPickupRequestDTO()
                {
                    //ParentDetails
                    GuardianName = getParentDetail?.GuardianFirstName + " " + getParentDetail?.GuardianMiddleName + " " + getParentDetail?.GuardianLastName,
                    GuardianContact = getParentDetail?.GuardianPhone != null ? getParentDetail?.GuardianPhone : getParentDetail?.PhoneNumber,
                    GuardianEmailID = getParentDetail?.GaurdianEmail != null ? getParentDetail?.GaurdianEmail : getParentDetail?.FatherEmailID,
                };

                foreach (var list in mapList)
                {
                    if (list.StudentID == null)
                    {
                        var studList = new List<Student>();

                        var checkQR = dbContext.Students.Where(sd => sd.AdmissionNumber == qrCode)
                            .Include(i => i.Parent)
                            .Include(i => i.Class)
                            .Include(i => i.Section)
                            .AsNoTracking().ToList();

                        if (checkQR.Count == 1)
                        {
                            studList = checkQR;
                        }
                        else
                        {
                            studList = dbContext.Students.Where(s => s.IsActive == true && s.ParentID == list.StudentPicker.ParentID)
                                .Include(i => i.Parent)
                                .Include(i => i.Class)
                                .Include(i => i.Section)
                                .AsNoTracking().ToList();
                        }

                        foreach (var stud in studList)
                        {
                            if (!registeredDTOs.StudentsList.Any(d => d.Student.Key == stud.StudentIID.ToString()))
                            {
                                registeredDTOs.StudentsList.Add(new StudentPickupRequestDTO()
                                {
                                    StudentPickerStudentMapIID = list.StudentPickerStudentMapIID,
                                    CreatedDateString = list.CreatedDate.HasValue ? list.CreatedDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : "NA",
                                    AdmissionNumber = stud.AdmissionNumber,
                                    Student = new KeyValueDTO()
                                    {
                                        Key = stud.StudentIID.ToString(),
                                        Value = stud.FirstName + " " + stud.MiddleName + " " + stud.LastName
                                    },
                                    StudentProfile = stud.StudentProfile,
                                    ClassSection = stud.Class?.ClassDescription + " " + stud.Section?.SectionName,
                                });
                            }
                        }
                    }
                    else if (!registeredDTOs.StudentsList.Any(d => d.Student.Key == list.StudentID.ToString()))
                    {
                        registeredDTOs.StudentsList.Add(new StudentPickupRequestDTO()
                        {
                            StudentPickerStudentMapIID = list.StudentPickerStudentMapIID,
                            CreatedDateString = list.CreatedDate.HasValue ? list.CreatedDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : "NA",
                            AdmissionNumber = list?.Student?.AdmissionNumber,
                            Student = new KeyValueDTO()
                            {
                                Key = list?.Student?.StudentIID.ToString(),
                                Value = list?.Student?.FirstName + " " + list?.Student?.MiddleName + " " + list?.Student?.LastName
                            },
                            StudentProfile = list?.Student?.StudentProfile,
                            ClassSection = list.Student?.Class?.ClassDescription + " " + list.Student?.Section?.SectionName,
                        });
                    }

                    if (!registeredDTOs.StudentPickersList.Any(d => d.StudentPickerIID == list.StudentPickerID))
                    {
                        registeredDTOs.StudentPickersList.Add(new StudentPickupRequestDTO()
                        {
                            PickByFullName = list?.StudentPicker?.FirstName + " " + list?.StudentPicker?.MiddleName + " " + list?.StudentPicker?.LastName,
                            IsActive = list.IsActive,
                            QRCODE = list?.QRCODE,
                            PickerProfile = list?.StudentPicker?.PhotoContentID.ToString(),
                            PickUpBy = list?.StudentPicker?.StudentPickedBy?.Name,
                            AdditionalInfo = list?.StudentPicker?.AdditionalInfo,
                            StudentPickerIID = (long)list?.StudentPickerID,
                        });
                    }
                }

                return registeredDTOs;
            }
        }
        #endregion

        #region Submit Log after scan QRCode (Proceed/Cancel)
        public OperationResultDTO SubmitStudentPickLogs(StudentPickupRequestDTO submitLog)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var message = new OperationResultDTO();
                var entity = new List<StudentPickLog>();

                try
                {
                    foreach (var listDat in submitLog.StudentsList)
                    {
                        var pickerDat = submitLog.StudentPickersList.FirstOrDefault();

                        entity.Add(new StudentPickLog()
                        {
                            StudentPickLogIID = listDat.StudentPickLogIID,
                            PickDate = submitLog.PickDate==null?DateTime.Now:submitLog.PickDate,
                            StudentPickerID = (long)(listDat.StudentPickerIID != 0 ? listDat?.StudentPickerIID : pickerDat?.StudentPickerIID),

                            StudentID = listDat.StudentID != null ? listDat.StudentID : long.Parse(listDat.Student.Key),
                            Remarks = submitLog.Remarks,
                            LogStatus = submitLog.LogStatus,
                            PhotoContentID = submitLog.StudentPickerContentID,

                            CreatedBy = listDat.StudentPickLogIID == 0 ? (int)_context?.LoginID : listDat.CreatedBy,
                            UpdatedBy = listDat.StudentPickLogIID > 0 ? (int)_context?.LoginID : listDat.UpdatedBy,
                            CreatedDate = listDat.StudentPickLogIID == 0 ? DateTime.Now : listDat.CreatedDate,
                            UpdatedDate = listDat.StudentPickLogIID > 0 ? DateTime.Now : listDat.UpdatedDate,
                        });
                    }

                    dbContext.StudentPickLogs.AddRange(entity);
                    dbContext.SaveChanges();

                    GetPickLogAndSendNotification(entity);

                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = "Saved successfully!"
                    };
                }
                catch (Exception ex)
                {
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = ex.Message
                    };
                }

                return message;
            }
        }

        public bool GetPickLogAndSendNotification(List<StudentPickLog> entity)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                string designationIdsString = new Domain.Setting.SettingBL(null).GetSettingValue<string>("Student_Late_Notifications_To_Designations");
                MutualRepository mutualRepository = new MutualRepository();

                foreach (var dat in entity)
                {
                    var driverSheduleDat = dbContext.DriverScheduleLogs.Where(d => d.ScheduleLogType == "DROP-IN" &&
                    d.SheduleDate.Value.Day == dat.PickDate.Value.Day && d.SheduleDate.Value.Month == dat.PickDate.Value.Month && d.SheduleDate.Value.Year == dat.PickDate.Value.Year && d.StudentID == dat.StudentID)
                        .Include(i => i.Routes1)
                        .AsNoTracking().FirstOrDefault();

                    if (driverSheduleDat != null)
                    {
                        var studData = dbContext.StudentPickLogs.Where(x => x.StudentPickLogIID == dat.StudentPickLogIID)
                            .Include(i => i.StudentPicker).ThenInclude(i => i.StudentPickedBy)
                            .Include(i => i.Student).ThenInclude(i => i.Parent)
                            .Include(i => i.Student).ThenInclude(i => i.Class)
                            .Include(i => i.Student).ThenInclude(i => i.Section)
                            .AsNoTracking().FirstOrDefault();

                        if (studData.LogStatus == true)
                        {
                            var studFullName = studData.Student?.AdmissionNumber + " - " + studData.Student?.FirstName + " " + studData.Student?.MiddleName + " " + studData.Student?.LastName;
                            var ClassSection = studData.Student?.Class?.ClassDescription + " " + studData.Student?.Section?.SectionName;
                            var PickByFullName = studData.StudentPicker?.FirstName + " " + studData.StudentPicker?.MiddleName + " " + studData.StudentPicker?.LastName;
                            var LogStatus = studData.LogStatus;

                            List<int?> designationIds = designationIdsString
                                .Split(',')
                                .Select(x => int.TryParse(x, out int result) ? (int?)result : null)
                                .ToList();

                            var sendToEmp = dbContext.Employees
                                .Where(e => (e.BranchID == studData.Student.SchoolID && designationIds.Contains(e.DesignationID)) || (e.AssignVehicleMaps.Any(v => v.IsActive == true && v.RouteID == driverSheduleDat.RouteID)))
                                .AsNoTracking().ToList();

                            var settings = new Dictionary<string, string>();

                            settings = NotificationSetting.GetEmployeeAppSettings();
                            var title = "Student PICKEDUP from school";
                            var p_message = studFullName
                                + " using Bus no. : " + driverSheduleDat?.Routes1?.RouteCode
                                + ", is PICKEDUP by " + PickByFullName + " today ( " + dat.PickDate.Value.ToString("dd/MM/yyyy") + " )";

                            foreach (var sendTo in sendToEmp)
                            {
                                if (sendTo.LoginID.HasValue)
                                {
                                    PushNotificationMapper.SendAndSavePushNotification((long)sendTo.LoginID, (long)dat.CreatedBy, p_message, title, settings);
                                }
                            }
                        }
                    }
                }

            }
            return true;
        }

        #endregion

        #region Get todays Student pick logs
        public List<StudentPickupRequestDTO> GetTodayStudentPickLogs()
        {
            var dateTimeFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("12HrDateTimeFormat");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var pickLogs = dbContext.StudentPickLogs
                  .Where(x => x.CreatedBy == _context.LoginID &&
                      x.CreatedDate.Value.Date == DateTime.Now.Date &&
                      x.Student.IsActive == true)
                  .Include(i => i.Student).ThenInclude(i => i.Class)
                  .Include(i => i.Student).ThenInclude(i => i.Section)
                  .Include(i => i.Student).Include(i => i.StudentPicker)
                  .OrderByDescending(x => x.StudentPickLogIID)
                  .AsNoTracking()
                  .ToList();


                var StudentPickLogs = new List<StudentPickupRequestDTO>();

                foreach (var list in pickLogs)
                {
                    if (!StudentPickLogs.Any(d => d.Student.Key == list.StudentID.ToString()))
                    {
                        StudentPickLogs.Add(new StudentPickupRequestDTO()
                        {
                            StudentPickLogIID = list.StudentPickLogIID,
                            Student = new KeyValueDTO()
                            {
                                Key = list.Student?.StudentIID.ToString(),
                                Value = list.Student?.AdmissionNumber + " - " + list.Student?.FirstName + " " + list.Student?.MiddleName + " " + list.Student?.LastName
                            },
                            ClassSection = list.Student?.Class?.ClassDescription + " " + list.Student?.Section?.SectionName,
                            PickByFullName = list.StudentPicker?.FirstName + " " + list.StudentPicker?.MiddleName + " " + list.StudentPicker?.LastName,
                            LogStatus = list.LogStatus,
                            StudentProfile = list.Student?.StudentProfile,
                            PickerProfile = list.PhotoContentID != null ? list.PhotoContentID.ToString() : list.StudentPicker?.PhotoContentID.ToString(),
                            PickDate = list.PickDate,
                            PickDateEndTime = list.PickDate.Value.AddMinutes(120),
                            PickStringDate = list.PickDate.HasValue ? list.PickDate.Value.ToString(dateTimeFormat) : null,
                            PickUpStringDate = list.PickDate.Value,
                            CreatedBy = list.CreatedBy,
                            CreatedDate = list.CreatedDate,
                            UpdatedBy = list.UpdatedBy,
                            UpdatedDate = list.UpdatedDate,
                        });
                    }
                }

                return StudentPickLogs;
            }
        }
        #endregion Todays student pick logs END

        #endregion Staff app END

        public string UpdateStudentPickLogStatus(long studentPicklogID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var pickLogDetails = dbContext.StudentPickLogs.Where(x => x.StudentPickLogIID == studentPicklogID).AsNoTracking().FirstOrDefault();

                if (pickLogDetails != null)
                {
                    pickLogDetails.UpdatedBy = _context != null && _context.LoginID.HasValue ? Convert.ToInt32(_context.LoginID) : (int?)null;
                    pickLogDetails.UpdatedDate = DateTime.Now;
                    pickLogDetails.LogStatus = false;

                    dbContext.Entry(pickLogDetails).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    dbContext.SaveChanges();

                    return "Successfully updated!";
                }
                else
                {
                    return null;
                }
            }
        }

        #region Get Visitor Details by vistor code
        public VisitorDTO GetVisitorDetailsByVisitorCode(string visitorCode)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var vsCode = visitorCode.ToUpper();

                // Attempt to retrieve the visitor details, could be null
                var vistorDetails = dbContext.Visitors
                                             .Include(x => x.VisitorAttachmentMaps)
                                             .AsNoTracking()
                                             .FirstOrDefault(x => x.VisitorNumber == vsCode);

                // Check if vistorDetails is null
                if (vistorDetails == null)
                {
                    return null; // Return null if no matching visitor found
                }

                // Safely access VisitorAttachmentMaps and get the VisitorProfileID, if available
                var visitorData = new VisitorDTO()
                {
                    VisitorIID = vistorDetails.VisitorIID,
                    VisitorNumber = vistorDetails.VisitorNumber,
                    FirstName = vistorDetails.FirstName,
                    MiddleName = vistorDetails.MiddleName,
                    LastName = vistorDetails.LastName,
                    LoginID = vistorDetails.LoginID,
                    VisitorProfileID = vistorDetails.VisitorAttachmentMaps?.FirstOrDefault()?.VisitorProfileID
                };

                return visitorData;
            }
        }

        #endregion

        #region Get/Check InspectionColour from setting Table
        public string GetTodayInspectionColour()
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var bgColor = "";
                bool isSettingDataUpdate = false;

                var settingTable = dbContext.Settings.FirstOrDefault(s => s.SettingCode == "Color_palette_for_Inspection");
                var valueString = settingTable.SettingValue;

                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

                //string format = "dd/MM/yy";

                if (valueString == null)
                {
                    valueString = DateTime.Now.ToString();
                }

                var split = valueString.Split('_').ToList();
                
                var dateString = split[0];

                var settingDate = new DateTime();

                DateTime dateTime;

                if (DateTime.TryParseExact(dateString, "dd/M/yy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                {
                    var stringDate = dateTime.ToString(dateFormat, CultureInfo.InvariantCulture);
                    settingDate = DateTime.ParseExact(stringDate, dateFormat, CultureInfo.InvariantCulture);
                }

                DateTime currentDate = DateTime.Now;
                var currentDateString = $"{currentDate.Day}/{currentDate.Month}/{currentDate.Year % 100}";

                if (settingDate.Date == currentDate.Date && settingDate.Month == currentDate.Month && settingDate.Year == currentDate.Year)
                {
                    isSettingDataUpdate = false;
                }
                else
                {
                    isSettingDataUpdate = true;
                }

                if (isSettingDataUpdate)
                {
                    var color = GenerateColor();
                    settingTable.SettingValue = currentDateString + "_" + color;
                    dbContext.Entry(settingTable).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();

                    bgColor = color;
                }
                else
                {
                    string pattern = @"#([A-Fa-f0-9]{6})"; // matches hex color codes

                    Match match = Regex.Match(valueString, pattern);
                    if (match.Success)
                    {
                        string color = match.Groups[1].Value;
                        bgColor = "#" + color;
                    }
                }

                return bgColor;
            }
        }

        #endregion

        #region
        //Inspection buttons calls
        public List<StudentPickupRequestDTO> GetAndUpdateActivePickLogsForInspection()
        {
            //Steps :
            //        1.Update log with setting_validity time
            //        2.Get current picklogs with loginID

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dateTimeFormat = dbContext.Settings.FirstOrDefault(s => s.SettingCode == "12HrDateTimeFormat").SettingValue;

                var settingDat = dbContext.Settings.FirstOrDefault(s => s.SettingCode == "Student_Pickup_Inspection_Expire").SettingValue;

                var toMinute = double.Parse(settingDat);

                var pickLogs = dbContext.StudentPickLogs.Where(x => x.CreatedBy == _context.LoginID &&
                x.CreatedDate.Value.Day == DateTime.Now.Day && x.CreatedDate.Value.Month == DateTime.Now.Month && x.CreatedDate.Value.Year == DateTime.Now.Year && x.LogStatus == true)
                    .Include(x => x.Student)
                    .Include(x => x.StudentPicker)
                    .Include(x => x.Student.Class)
                    .Include(x => x.Student.Section)
                    .ToList()
                    .OrderByDescending(x => x.StudentPickLogIID);

                var StudentPickLogs = new List<StudentPickupRequestDTO>();

                if (pickLogs != null)
                {
                    foreach (var list in pickLogs)
                    {
                        var currentTime = DateTime.Now;
                        var endTime = list.PickDate.Value.AddMinutes(toMinute);

                        if (currentTime > endTime)
                        {
                            UpdateStudentPickLogStatus(list.StudentPickLogIID);
                        }
                        else
                        {
                            if (!StudentPickLogs.Any(d => d.Student.Key == list.StudentID.ToString()))
                            {
                                StudentPickLogs.Add(new StudentPickupRequestDTO()
                                {
                                    StudentPickLogIID = list.StudentPickLogIID,
                                    Student = new KeyValueDTO() { Key = list.Student?.StudentIID.ToString(), Value = list.Student?.AdmissionNumber + " - " + list.Student?.FirstName + " " + list.Student?.MiddleName + " " + list.Student?.LastName },
                                    ClassSection = list.Student?.Class?.ClassDescription + " " + list.Student?.Section?.SectionName,
                                    PickByFullName = list.StudentPicker?.FirstName + " " + list.StudentPicker?.MiddleName + " " + list.StudentPicker?.LastName,
                                    LogStatus = list.LogStatus,
                                    StudentProfile = list.Student?.StudentProfile,
                                    PickerProfile = list.PhotoContentID != null ? list.PhotoContentID.ToString() : list.StudentPicker?.PhotoContentID.ToString(),
                                    PickDate = list.PickDate,
                                    PickDateEndTime = list.PickDate.Value.AddMinutes(120),
                                    PickStringDate = list.PickDate.HasValue ? list.PickDate.Value.ToString(dateTimeFormat) : null,
                                    PickUpStringDate = list.PickDate,
                                    CreatedBy = list.CreatedBy,
                                    CreatedDate = list.CreatedDate,
                                    UpdatedBy = list.UpdatedBy,
                                    UpdatedDate = list.UpdatedDate,
                                });
                            }
                        }
                    }
                }

                return StudentPickLogs;
            }
        }
        #endregion
        #region Color Generate
        public string GenerateColor()
        {
            // Generate and print random hex color
            string randomHexColor = GenerateRandomHexColor();
            return randomHexColor;
        }

        public static string GenerateRandomHexColor()
        {
            byte[] bytes = new byte[3];
            Random random = new Random();

            // Generate a random color until it is not black, red, or white
            string hexColor;
            do
            {
                random.NextBytes(bytes);
                hexColor = "#" + BitConverter.ToString(bytes).Replace("-", "");
            } while (hexColor == "#000000" || hexColor == "#FF0000" || hexColor == "#FFFFFF");

            return hexColor;
        }
        #endregion
        #region Get Parent Details By code
        public GuardianDTO GetParentDetailsByParentCode(string parentCode)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var prntCode = parentCode.ToUpper();
                var data = dbContext.Parents.FirstOrDefault(x => x.ParentCode == prntCode);

                var parentData = new GuardianDTO()
                {
                    LoginID = data.LoginID,
                    ParentCode = data.ParentCode,
                    FatherFirstName = data.FatherFirstName,
                    FatherMiddleName = data.FatherMiddleName,
                    FatherLastName = data.FatherLastName,
                    MotherFirstName = data.MotherFirstName,
                    MotherMiddleName = data.MotherMiddleName,
                    MotherLastName = data.MotherLastName,
                    GuardianFirstName = data.GuardianFirstName,
                    GuardianMiddleName = data.GuardianMiddleName,
                    GuardianLastName = data.GuardianLastName
                };

                return parentData;
            }
        }
        #endregion
        public List<StudentPickupRequestDTO> GetUnverifiedStudentsAssignedToVisitor(string visitorCode)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                // Step 1: Retrieve all Visitors (StudentPickers) based on the provided VisitorCode
                var visitors = dbContext.StudentPickers
                    .Where(sp => sp.VisitorCode == visitorCode)
                    .AsNoTracking()
                    .ToList();

                if (!visitors.Any())
                {
                    // Return an empty list if no visitors are found
                    return new List<StudentPickupRequestDTO>();
                }

                // Step 2: Retrieve students assigned to each visitor using StudentPickerStudentMaps
                var unverifiedStudents = dbContext.StudentPickerStudentMaps
                    .Where(spm => visitors.Select(v => (long?)v.StudentPickerIID).Contains(spm.StudentPickerID) && spm.IsActive == true)
                    .Include(spm => spm.Student)  // Ensure Student is a navigation property
                    .Include(spm => spm.Student.Class)  // Ensure Class is included
                    .Include(spm => spm.Student.Section)  // Ensure Section is included
                    .AsNoTracking()
                    .ToList();

                var studentPickupRequests = new List<StudentPickupRequestDTO>();

                // Loop through each student mapping in unverifiedStudents
                foreach (var studentMap in unverifiedStudents)
                {
                    var student = studentMap.Student;

                    studentPickupRequests.Add(new StudentPickupRequestDTO()
                    {
                        StudentID = student.StudentIID,
                        Student = new KeyValueDTO()
                        {
                            Key = student.StudentIID.ToString(),
                            Value = student.AdmissionNumber + " - " + student.FirstName + " " + student.MiddleName + " " + student.LastName
                        },
                        ClassSection = student.Class?.ClassDescription + " " + student.Section?.SectionName,
                        StudentProfile = student.StudentProfile, // Assuming this field exists
                                                                 // Add other necessary fields if needed
                    });
                }

                return studentPickupRequests;
            }
        }

        public List<StudentPickupRequestDTO> GetTodayStudentPickLogsByvisitorLoginID(string visitorCode)
        {
            var dateTimeFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("12HrDateTimeFormat");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                // Step 1: Retrieve all visitors with the given visitorCode
                var visitors = dbContext.StudentPickers
                    .Where(v => v.VisitorCode == visitorCode)
                    .ToList();

                // Step 2: Extract the StudentPickerID values from the visitor records
                var visitorIds = visitors.Select(v => v.StudentPickerIID).ToList();

                // Step 3: Retrieve today's StudentPickLogs based on StudentPickerID and CreatedDate
                var pickLogs = dbContext.StudentPickLogs
                    .Where(log => visitorIds.Contains(log.StudentPickerID) &&
                                  log.CreatedDate.Value.Date == DateTime.Now.Date)
                    .Include(log => log.Student).ThenInclude(s => s.Class)
                    .Include(log => log.Student).ThenInclude(s => s.Section)
                    .Include(log => log.StudentPicker)
                    .OrderByDescending(log => log.StudentPickLogIID)
                    .AsNoTracking()
                    .ToList();

                // Step 4: Populate the DTO list with log details, only including entries where CreatedBy is an employee
                var studentPickLogs = pickLogs.Select(log =>
                {
                    // Check if CreatedBy is an employee and fetch employee details
                    var employee = dbContext.Employees
                        .Where(e => e.LoginID == log.CreatedBy)
                        .Select(e => new
                        {
                            e.EmployeePhoto,
                            e.EmployeeName,
                            // Add any other employee details you need
                        })
                        .FirstOrDefault();

                    // If CreatedBy is not an employee, skip this log entry
                    if (employee == null) return null;

                    // Build the DTO with employee details included
                    return new StudentPickupRequestDTO
                    {
                        StudentPickLogIID = log.StudentPickLogIID,
                        Student = new KeyValueDTO
                        {
                            Key = log.Student?.StudentIID.ToString(),
                            Value = $"{log.Student?.AdmissionNumber} - {log.Student?.FirstName} {log.Student?.MiddleName} {log.Student?.LastName}"
                        },
                        ClassSection = $"{log.Student?.Class?.ClassDescription} {log.Student?.Section?.SectionName}",
                        PickByFullName = $"{log.StudentPicker?.FirstName} {log.StudentPicker?.MiddleName} {log.StudentPicker?.LastName}",
                        LogStatus = log.LogStatus,
                        StudentProfile = log.Student?.StudentProfile,
                        PickerProfile = log.PhotoContentID?.ToString() ?? log.StudentPicker?.PhotoContentID.ToString(),
                        PickDate = log.PickDate,
                        PickDateEndTime = log.PickDate?.AddMinutes(120),
                        PickStringDate = log.PickDate?.ToString(dateTimeFormat),
                        PickUpStringDate = log.PickDate,
                        CreatedBy = log.CreatedBy,
                        CreatedDate = log.CreatedDate,
                        UpdatedBy = log.UpdatedBy,
                        UpdatedDate = log.UpdatedDate,
                        // Include employee details
                        EmployeeDetails = new KeyValueDTO
                        {
                            Key = employee.EmployeePhoto,
                            Value = employee.EmployeeName
                            // Include other relevant fields from employee
                        }
                    };
                })
                // Filter out nulls (logs with non-employee creators)
                .Where(dto => dto != null)
                .ToList();

                return studentPickLogs;
            }
        }

        public List<StudentPickupRequestDTO> GetPickupRequestsByClassSectionDate(long classID, long sectionID, DateTime date)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var resultList = new List<StudentPickupRequestDTO>();

                //byte?[] statusIDs = { 1, 2 };

                var pickRequests = dbContext.StudentPickupRequests
                    .Where(a => a.Student.ClassID == classID
                             && a.Student.SectionID == sectionID
                             && a.PickedDate == date)
                    .Include(i => i.Student)
                    .Include(i => i.StudentPickupRequestStatus)
                    .Include(i => i.StudentPickedBy)
                    .Distinct()
                    .AsNoTracking()
                    .ToList();

                foreach (var request in pickRequests)
                {
                    var dto = new StudentPickupRequestDTO
                    {
                        StudentID = request.StudentID,
                        AdmissionNumber = request.Student?.AdmissionNumber,
                        Student = new KeyValueDTO()
                        {
                            Key = request.StudentID.ToString(),
                            Value = request.Student?.FirstName + " " + (request.Student?.MiddleName ?? "") + (request.Student?.LastName ?? "")
                        },
                        RequestStatus = request.StudentPickupRequestStatus?.StudentPickupRequestStatusName,
                        StudentProfile = request.Student?.StudentProfile,
                        PickedByDescription = request.StudentPickedBy?.Name,
                        PickByFullName = request.FirstName + " " + (request.MiddleName ?? "") + (request.LastName ?? "")
                    };

                    resultList.Add(dto);
                }

                return resultList;
            }
        }

    }
}