using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using System;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Mappers.Notification.Notifications;
using Eduegate.Domain.Mappers.Notification.Notifications.Helpers;
using Eduegate.Services.Contracts.Enums;
using System.Net.Mail;
using Microsoft.VisualBasic;
using Eduegate.Services.Contracts.SignUp.Meeting;
using Eduegate.Domain.Entity.Lms.Models;
using Eduegate.Services.Contracts.Lms;
using Eduegate.Services.Contracts.Lms.Lms;

namespace Eduegate.Domain.Mappers.Lms.Lms
{
    public class LmsMapper : DTOEntityDynamicMapper
    {
        public static LmsMapper Mapper(CallContext context)
        {
            var mapper = new LmsMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LmsDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private LmsDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateLmsContext())
            {
                var entity = dbContext.Signups.Where(x => x.SignupIID == IID)
                    .Include(i => i.SignupGroup)
                    .Include(i => i.School)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.OrganizerEmployee)
                    .Include(i => i.SignupCategory)
                    .Include(i => i.SignupType)
                    .Include(i => i.SignupStatus)
                    .Include(i => i.Student)
                    .Include(i => i.SignupAudienceMaps).ThenInclude(i => i.Student)
                    .Include(i => i.SignupAudienceMaps).ThenInclude(i => i.Parent)
                    .Include(i => i.SignupAudienceMaps).ThenInclude(i => i.Employee)
                    .Include(i => i.SignupSlotMaps).ThenInclude(i => i.SlotMapStatus)
                    .Include(i => i.SignupSlotMaps).ThenInclude(i => i.SignupSlotAllocationMaps).ThenInclude(i => i.Student)
                    .Include(i => i.SignupSlotMaps).ThenInclude(i => i.SignupSlotAllocationMaps).ThenInclude(i => i.Parent)
                    .Include(i => i.SignupSlotMaps).ThenInclude(i => i.SignupSlotAllocationMaps).ThenInclude(i => i.Employee)
                    .Include(i => i.SignupSlotMaps).ThenInclude(i => i.SignupSlotAllocationMaps).ThenInclude(i => i.SlotMapStatus)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private LmsDTO ToDTO(Signup entity)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var LmsDTO = new LmsDTO()
            {
                SignupIID = entity.SignupIID,
                SignupName = entity.SignupName,
                SchoolID = entity.SchoolID,
                ClassID = entity.ClassID,
                ClassName = entity.Class?.ClassDescription,
                SectionID = entity.SectionID,
                AcademicYearID = entity.AcademicYearID,
                OrganizerEmployeeID = entity.OrganizerEmployeeID,
                LocationInfo = entity.LocationInfo,
                Message = entity.Message,
                Remarks = entity.Remarks,
                StudentID = entity.StudentID,
                IsActive = entity.IsActive,
                SignupGroupID = entity.SignupGroupID,
                SignupCategoryID = entity.SignupCategoryID,
                SignupCategoryName = entity.SignupCategoryID.HasValue ? entity.SignupCategory?.SignupCategoryName : null,
                SignupTypeID = entity.SignupTypeID,
                SignupTypeName = entity.SignupTypeID.HasValue ? entity.SignupType?.SignupTypeName : null,
                SignupStatusID = entity.SignupStatusID,
                SignupOldStatusID = entity.SignupStatusID,
                SignupStatusName = entity.SignupStatusID.HasValue ? entity.SignupStatus?.SignupStatusName : null,
                DateFrom = entity.DateFrom,
                FromDateString = entity.DateFrom.HasValue ? entity.DateFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                DateTo = entity.DateTo,
                ToDateString = entity.DateTo.HasValue ? entity.DateTo.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                GroupDateFrom = entity.SignupGroup?.FromDate,
                GroupDateTo = entity.SignupGroup?.ToDate,
                IsSlotShowToUser = entity.IsSlotShowToUser,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
            };
            
            FillStudent(entity, LmsDTO);
            FillOrganizerEmployee(entity, LmsDTO);
            FillSignUpAudienceMaps(entity, LmsDTO);
            FillSignUpSlotMaps(entity, LmsDTO);

            return LmsDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LmsDTO;

            var signupPublishedStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_STATUSID_PUBLISH", 3);

            var signupCancelledStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_STATUSID_CANCEL", 2);

            var signupClosedStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_STATUSID_CLOSED", 4);

            if (!toDto.OrganizerEmployeeID.HasValue)
            {
                throw new Exception("Select any organizer employee");
            }

            if (toDto.DateFrom > toDto.DateTo)
            {
                throw new Exception("The to date must be greater than or equal to the from date.");
            }

            if (toDto.SignupOldStatusID == signupPublishedStatusID && toDto.SignupStatusID != signupPublishedStatusID)
            {
                if (toDto.SignupStatusID != signupCancelledStatusID && toDto.SignupStatusID != signupClosedStatusID)
                {
                    throw new Exception("After they are published, only status changes to 'cancelled' or 'closed' are allowed.");
                }
            }

            using (var dbContext = new dbEduegateLmsContext())
            {
                var entity = new Signup()
                {
                    SignupIID = toDto.SignupIID,
                    SignupName = toDto.SignupName,
                    SchoolID = toDto.SchoolID.HasValue ? toDto.SchoolID : Convert.ToByte(_context.SchoolID),
                    ClassID = toDto.ClassID,
                    SectionID = toDto.SectionID,
                    AcademicYearID = toDto.AcademicYearID.HasValue ? toDto.AcademicYearID : _context.AcademicYearID,
                    OrganizerEmployeeID = toDto.OrganizerEmployeeID,
                    LocationInfo = toDto.LocationInfo,
                    Message = toDto.Message,
                    Remarks = toDto.Remarks,
                    StudentID = toDto.StudentID,
                    IsActive = toDto.IsActive,
                    DateFrom = toDto.DateFrom,
                    DateTo = toDto.DateTo,
                    SignupCategoryID = toDto.SignupCategoryID,
                    SignupTypeID = toDto.SignupTypeID,
                    SignupGroupID = toDto.SignupGroupID,
                    SignupStatusID = toDto.SignupStatusID,
                    IsSlotShowToUser = toDto.IsSlotShowToUser,
                    CreatedBy = toDto.SignupIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.SignupIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.SignupIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.SignupIID > 0 ? DateTime.Now : dto.UpdatedDate,
                };

                var IIDs = toDto.SignupSlotMaps
                    .Select(a => a.SignupSlotMapIID).ToList();

                //delete maps
                var entities = dbContext.SignupSlotMaps.Where(x =>
                    x.SignupID == entity.SignupIID &&
                    !IIDs.Contains(x.SignupSlotMapIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                {
                    if (entities.Count > 0)
                    {
                        if (toDto.SignupOldStatusID != signupPublishedStatusID)
                        {
                            dbContext.SignupSlotMaps.RemoveRange(entities);
                        }
                        else
                        {
                            throw new Exception("It is not possible to delete or add entries after they are published. Only status changes are allowed.");
                        }
                    }
                }

                entity.SignupSlotMaps = new List<SignupSlotMap>();

                if (toDto.SignupSlotMaps.Count > 0)
                {
                    foreach (var map in toDto.SignupSlotMaps)
                    {
                        if (map.SlotDate.HasValue)
                        {
                            if (!map.StartTime.HasValue)
                            {
                                throw new Exception("The start time is mandatory for saving the slot.");
                            }
                            else if (!map.EndTime.HasValue)
                            {
                                throw new Exception("The end time is mandatory for saving the slot.");
                            }

                            SlotAvailabilityCheckForEmployee(entity, toDto, map);

                            entity.SignupSlotMaps.Add(new SignupSlotMap()
                            {
                                SignupSlotMapIID = map.SignupSlotMapIID,
                                SignupID = map.SignupID,
                                SlotDate = map.SlotDate,
                                StartTime = map.StartTime,
                                EndTime = map.EndTime,
                                Duration = map.Duration,
                                SchoolID = map.SchoolID,
                                AcademicYearID = map.AcademicYearID,
                                SignupSlotTypeID = map.SignupSlotTypeID,
                                SlotMapStatusID = map.SlotMapStatusID,
                                CreatedBy = map.SignupSlotMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                                UpdatedBy = map.SignupSlotMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                                CreatedDate = map.SignupSlotMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                                UpdatedDate = map.SignupSlotMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                            });
                        }
                    }
                }

                dbContext.Signups.Add(entity);
                if (entity.SignupIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    if (entity.SignupSlotMaps.Count > 0)
                    {
                        foreach (var map in entity.SignupSlotMaps)
                        {
                            if (map.SignupSlotMapIID == 0)
                            {
                                dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                if (entity.SignupIID > 0)
                {
                    if (toDto.IsSendNotification == true)
                    {
                        SendPushNotification(toDto);
                        SendEmailNotification(toDto);
                    }
                }

                return ToDTOString(ToDTO(entity.SignupIID));
            }
        }

        private void SlotAvailabilityCheckForEmployee(Signup entity, LmsDTO LmsDTO, LmsSlotMapDTO slotMapDTO)
        {
            using (var dbContext = new dbEduegateLmsContext())
            {
                var signupPublishedStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_STATUSID_PUBLISH", 3);

                var slotOpenStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_OPEN", 1);
                var slotAssignedStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_ASSIGNED", 2);

                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
                var timeFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TimeFormatWithoutSecond");

                var slotMapData = dbContext.SignupSlotMaps
                    .Where(m => m.SlotDate == slotMapDTO.SlotDate && m.StartTime == slotMapDTO.StartTime && m.EndTime == slotMapDTO.EndTime
                    && (m.SlotMapStatusID == slotOpenStatusID || m.SlotMapStatusID == slotAssignedStatusID)
                    && m.Signup.OrganizerEmployeeID == entity.OrganizerEmployeeID 
                    && m.Signup.SignupIID != entity.SignupIID && m.Signup.IsActive == true && m.Signup.SignupGroup.IsActive == true
                    && m.Signup.SignupStatusID == signupPublishedStatusID)
                    .Include(i => i.Signup).ThenInclude(i => i.SignupGroup)
                    .AsNoTracking().FirstOrDefault();

                if (slotMapData != null)
                {
                    var errorMessage = "The date: " + slotMapDTO.SlotDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture)
                        + ", start time: " + DateTime.Parse(slotMapDTO.StartTime.Value.ToString()).ToString(timeFormat)
                        + ", or end time: " + DateTime.Parse(slotMapDTO.EndTime.Value.ToString()).ToString(timeFormat)
                        + " is used in another meeting. Please select another time for the slot!";

                    throw new Exception(errorMessage);
                }

            }
        }

        private void FillOrganizerEmployee(Signup entity, LmsDTO LmsDTO)
        {
            if (entity.OrganizerEmployeeID.HasValue)
            {
                var organizerEmployee = string.Empty;

                if (entity.OrganizerEmployee != null)
                {
                    organizerEmployee = entity.OrganizerEmployee.EmployeeCode + " - " + entity.OrganizerEmployee.FirstName + " " +
                        (!string.IsNullOrEmpty(entity.OrganizerEmployee.MiddleName) ? entity.OrganizerEmployee.MiddleName + " " : "") +
                        entity.OrganizerEmployee.LastName;
                }
                else
                {
                    using (var dbContext = new dbEduegateLmsContext())
                    {
                        var employeeDet = dbContext.Employees.Where(e => e.EmployeeIID == entity.OrganizerEmployeeID).AsNoTracking().FirstOrDefault();

                        organizerEmployee = employeeDet != null ? employeeDet.EmployeeCode + " - " + employeeDet.FirstName + " " +
                        (!string.IsNullOrEmpty(employeeDet.MiddleName) ? employeeDet.MiddleName + " " : "") + employeeDet.LastName : null;
                    }
                }

                LmsDTO.OrganizerEmployeeName = organizerEmployee;
            }
        }

        private void FillStudent(Signup entity, LmsDTO LmsDTO)
        {
            if (entity.StudentID.HasValue)
            {
                var studentName = string.Empty;

                if (entity.Student != null)
                {
                    studentName = entity.Student.AdmissionNumber + " - " + entity.Student.FirstName +
                        (!string.IsNullOrEmpty(entity.Student.MiddleName) ? entity.Student.MiddleName + " " : " ") +
                        entity.Student.LastName;
                }
                else
                {
                    using (var dbContext = new dbEduegateLmsContext())
                    {
                        var studentDet = dbContext.Students.Where(e => e.StudentIID == entity.StudentID).AsNoTracking().FirstOrDefault();

                        studentName = studentDet != null ? studentDet.AdmissionNumber + " - " + studentDet.FirstName +
                        (!string.IsNullOrEmpty(studentDet.MiddleName) ? studentDet.MiddleName + " " : " ") + studentDet.LastName : null;
                    }
                }

                LmsDTO.StudentName = studentName;
            }
        }

        private void FillSignUpAudienceMaps(Signup entity, LmsDTO LmsDTO)
        {
            LmsDTO.SignupAudienceMaps = new List<LmsAudienceMapDTO>();
            foreach (var audMap in entity.SignupAudienceMaps)
            {
                LmsDTO.SignupAudienceMaps.Add(new LmsAudienceMapDTO()
                {
                    SignupAudienceMapIID = audMap.SignupAudienceMapIID,
                    SignupID = audMap.SignupID,
                    StudentID = audMap.StudentID,
                    ParentID = audMap.ParentID,
                    EmployeeID = audMap.EmployeeID,
                    SchoolID = audMap.SchoolID,
                    AcademicYearID = audMap.AcademicYearID,
                    CreatedBy = audMap.CreatedBy,
                    CreatedDate = audMap.CreatedDate,
                    UpdatedBy = audMap.UpdatedBy,
                    UpdatedDate = audMap.UpdatedDate,
                });
            }
        }

        private void FillSignUpSlotMaps(Signup entity, LmsDTO LmsDTO)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var timeFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TimeFormatWithoutSecond");

            var assignedSlotMapStatsID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_ASSIGNED", 2);

            LmsDTO.SignupSlotMaps = new List<LmsSlotMapDTO>();
            foreach (var slotMap in entity.SignupSlotMaps)
            {
                var slotAllocMapDTOs = new List<LmsSlotAllocationMapDTO>();

                var isSlotAllocated = false;

                foreach (var slotAlloc in slotMap.SignupSlotAllocationMaps)
                {
                    if (slotAlloc.SlotMapStatusID == assignedSlotMapStatsID)
                    {
                        var slotAllocDTO = FillLmsSlotAllocationMapDTO(slotAlloc);
                        if (slotAllocDTO != null)
                        {
                            slotAllocMapDTOs.Add(slotAllocDTO);
                        }
                    }
                }

                if (slotAllocMapDTOs.Any(x => x.SlotMapStatusID == assignedSlotMapStatsID))
                {
                    isSlotAllocated = true;
                }

                var lastSlotAllocMapDTO = slotAllocMapDTOs?.OrderByDescending(o => o.SignupSlotAllocationMapIID)?.FirstOrDefault();

                LmsDTO.SignupSlotMaps.Add(new LmsSlotMapDTO()
                {
                    SignupSlotMapIID = slotMap.SignupSlotMapIID,
                    SignupID = slotMap.SignupID,
                    SignupSlotTypeID = slotMap.SignupSlotTypeID,
                    SignupSlotType = slotMap.SignupSlotType?.SignupSlotTypeName,
                    SlotDate = slotMap.SlotDate,
                    SlotDateString = slotMap.SlotDate.HasValue ? slotMap.SlotDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                    StartTime = slotMap.StartTime,
                    StartTimeString = slotMap.StartTime.HasValue ? DateTime.Today.Add(slotMap.StartTime.Value).ToString(timeFormat, CultureInfo.InvariantCulture) : null,
                    EndTime = slotMap.EndTime,
                    EndTimeString = slotMap.EndTime.HasValue ? DateTime.Today.Add(slotMap.EndTime.Value).ToString(timeFormat, CultureInfo.InvariantCulture) : null,
                    Duration = slotMap.Duration,
                    SchoolID = slotMap.SchoolID,
                    School = slotMap.SchoolID.HasValue ? slotMap.School?.SchoolName : null,
                    AcademicYearID = slotMap.AcademicYearID,
                    AcademicYear = slotMap.AcademicYearID.HasValue ? slotMap.AcademicYear != null ? slotMap.AcademicYear?.Description + " (" + slotMap.AcademicYear?.AcademicYearCode + ")" : null : null,
                    SlotMapStatusID = slotMap.SlotMapStatusID,
                    SlotMapStatusName = slotMap.SlotMapStatusID.HasValue ? slotMap.SlotMapStatus?.SlotMapStatusName : null,
                    OldSlotMapStatusID = slotMap.SlotMapStatusID,
                    IsSlotAllocated = isSlotAllocated,
                    EmployeeID = lastSlotAllocMapDTO?.EmployeeID,
                    Employee = lastSlotAllocMapDTO?.Employee,
                    StudentID = lastSlotAllocMapDTO?.StudentID,
                    Student = lastSlotAllocMapDTO?.Student,
                    ParentID = lastSlotAllocMapDTO?.ParentID,
                    Parent = lastSlotAllocMapDTO?.Parent,
                    CreatedBy = slotMap.CreatedBy,
                    CreatedDate = slotMap.CreatedDate,
                    UpdatedBy = slotMap.UpdatedBy,
                    UpdatedDate = slotMap.UpdatedDate,
                });
            }
        }

        private LmsSlotAllocationMapDTO FillLmsSlotAllocationMapDTO(SignupSlotAllocationMap slotAlloc)
        {
            var slotAllocMapDTO = new LmsSlotAllocationMapDTO();

            if (slotAlloc != null)
            {
                slotAllocMapDTO = new LmsSlotAllocationMapDTO()
                {
                    SignupSlotAllocationMapIID = slotAlloc.SignupSlotAllocationMapIID,
                    SignupSlotMapID = slotAlloc.SignupSlotMapID,
                    StudentID = slotAlloc.StudentID,
                    Student = slotAlloc.StudentID.HasValue && slotAlloc.Student != null ? slotAlloc.Student.AdmissionNumber + " - " + slotAlloc.Student.FirstName + " " + (string.IsNullOrEmpty(slotAlloc.Student.MiddleName) ? "" : slotAlloc.Student.MiddleName + " ") + slotAlloc.Student.LastName : null,
                    ParentID = slotAlloc.ParentID,
                    Parent = slotAlloc.ParentID.HasValue && slotAlloc.Parent != null ? slotAlloc.Parent.ParentCode + " - " + slotAlloc.Parent.FatherFirstName + " " + (string.IsNullOrEmpty(slotAlloc.Parent.FatherMiddleName) ? "" : slotAlloc.Parent.FatherMiddleName + " ") + slotAlloc.Parent.FatherLastName : null,
                    EmployeeID = slotAlloc.EmployeeID,
                    Employee = slotAlloc.EmployeeID.HasValue && slotAlloc.Employee != null ? slotAlloc.Employee.EmployeeCode + " - " + slotAlloc.Employee.FirstName + " " + (string.IsNullOrEmpty(slotAlloc.Employee.MiddleName) ? "" : slotAlloc.Employee.MiddleName + " ") + slotAlloc.Employee.LastName : null,
                    SchoolID = slotAlloc.SchoolID,
                    School = slotAlloc.SchoolID.HasValue ? slotAlloc.School?.SchoolName : null,
                    AcademicYearID = slotAlloc.AcademicYearID,
                    AcademicYear = slotAlloc.AcademicYearID.HasValue ? slotAlloc.AcademicYear?.Description + " (" + slotAlloc.AcademicYear?.AcademicYearCode + ")" : null,
                    SlotMapStatusID = slotAlloc.SlotMapStatusID,
                    SlotMapStatus = slotAlloc.SlotMapStatusID.HasValue ? slotAlloc.SlotMapStatus?.SlotMapStatusName : null,
                    OldSlotMapStatusID = slotAlloc.SlotMapStatusID,
                    CreatedBy = slotAlloc.CreatedBy,
                    CreatedDate = slotAlloc.CreatedDate,
                    UpdatedBy = slotAlloc.UpdatedBy,
                    UpdatedDate = slotAlloc.UpdatedDate,
                };
            }

            return slotAllocMapDTO;
        }

        public List<LmsDTO> FillSignUpDetailsByGroupID(int groupID)
        {
            var dtos = new List<LmsDTO>();

            var signupPublishedStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_STATUSID_PUBLISH", 3);
            var meetingTypeID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_TYPEID_MEETING", 1);

            using (var dbContext = new dbEduegateLmsContext())
            {
                var entities = dbContext.Signups
                    .Where(x => x.SignupGroupID == groupID && x.IsActive == true && x.SignupStatusID == signupPublishedStatusID && x.SignupTypeID == meetingTypeID)
                    .Include(x => x.SignupGroup)
                    .Include(i => i.School)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.OrganizerEmployee)
                    .Include(i => i.SignupCategory)
                    .Include(i => i.SignupType)
                    .Include(i => i.SignupStatus)
                    .Include(i => i.SignupAudienceMaps).ThenInclude(i => i.Student)
                    .Include(i => i.SignupAudienceMaps).ThenInclude(i => i.Parent)
                    .Include(i => i.SignupAudienceMaps).ThenInclude(i => i.Employee)
                    .Include(i => i.SignupSlotMaps).ThenInclude(i => i.SlotMapStatus)
                    .Include(i => i.SignupSlotMaps).ThenInclude(i => i.SignupSlotAllocationMaps).ThenInclude(i => i.Student)
                    .Include(i => i.SignupSlotMaps).ThenInclude(i => i.SignupSlotAllocationMaps).ThenInclude(i => i.Parent)
                    .Include(i => i.SignupSlotMaps).ThenInclude(i => i.SignupSlotAllocationMaps).ThenInclude(i => i.Employee)
                    .AsNoTracking()
                    .ToList();

                foreach (var entity in entities)
                {
                    dtos.Add(ToDTO(entity));
                }
            }

            return dtos;
        }

        public OperationResultDTO SaveSelectedSignUpSlot(LmsSlotAllocationMapDTO allocDTO)
        {
            using (var dbContext = new dbEduegateLmsContext())
            {
                var returnResult = new OperationResultDTO();

                var entity = new SignupSlotAllocationMap()
                {
                    SignupSlotAllocationMapIID = allocDTO.SignupSlotAllocationMapIID,
                    SignupSlotMapID = allocDTO.SignupSlotMapID,
                    StudentID = allocDTO.StudentID,
                    EmployeeID = allocDTO.EmployeeID,
                    ParentID = allocDTO.ParentID,
                    SchoolID = allocDTO.SchoolID,
                    AcademicYearID = allocDTO.AcademicYearID,
                    SlotMapStatusID = allocDTO.SlotMapStatusID,
                    CreatedBy = allocDTO.SignupSlotAllocationMapIID == 0 ? (int)_context.LoginID : allocDTO.CreatedBy,
                    UpdatedBy = allocDTO.SignupSlotAllocationMapIID > 0 ? (int)_context.LoginID : allocDTO.UpdatedBy,
                    CreatedDate = allocDTO.SignupSlotAllocationMapIID == 0 ? DateTime.Now : allocDTO.CreatedDate,
                    UpdatedDate = allocDTO.SignupSlotAllocationMapIID > 0 ? DateTime.Now : allocDTO.UpdatedDate,
                };

                var isTimeSlotBooked = CheckSlotBookingStatus(allocDTO.SignupSlotMapID.Value);

                if (isTimeSlotBooked == true)
                {
                    returnResult = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = "Apologies, the slot is already reserved for another user. Please select a different slot.",
                    };
                }
                else
                {
                    dbContext.SignupSlotAllocationMaps.Add(entity);

                    if (entity.SignupSlotAllocationMapIID == 0)
                    {
                        dbContext.Entry(entity).State = EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(entity).State = EntityState.Modified;
                    }
                    dbContext.SaveChanges();

                    SendSlotBookingAndCancelPushNotification(allocDTO);

                    returnResult = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = "Saved successfully!",
                    };
                }

                return returnResult;
            }
        }

        public bool CheckSlotBookingStatus(long signupSlotMapID)
        {
            using (var dbContext = new dbEduegateLmsContext())
            {
                var isError = false;

                var assignedSlotMapStatsID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_ASSIGNED", 2);

                var slotMap = dbContext.SignupSlotMaps
                    .Where(x => x.SignupSlotMapIID == signupSlotMapID)
                    .Include(i => i.SignupSlotAllocationMaps)
                    .AsNoTracking().FirstOrDefault();

                if (slotMap != null)
                {
                    if (slotMap.SignupSlotAllocationMaps.Count > 0)
                    {
                        if (slotMap.SignupSlotAllocationMaps.Any(x => x.SlotMapStatusID == assignedSlotMapStatsID))
                        {
                            isError = true;
                        }
                    }
                    else
                    {
                        isError = false;
                    }
                }

                return isError;
            }
        }

        public void UpdateSignupSlotMapStatus(long signupSlotMapID, byte? statusID)
        {
            using (var dbContext = new dbEduegateLmsContext())
            {
                var entity = dbContext.SignupSlotMaps.Where(x => x.SignupSlotMapIID == signupSlotMapID).AsNoTracking().FirstOrDefault();
                if (entity != null)
                {
                    entity.SlotMapStatusID = statusID;
                    entity.UpdatedBy = _context.LoginID.HasValue ? Convert.ToInt32(_context.LoginID) : null;
                    entity.UpdatedDate = DateTime.Now;

                    dbContext.Entry(entity).State = EntityState.Modified;

                    dbContext.SaveChanges();
                }
            }
        }

        public OperationResultDTO CancelSelectedSignUpSlot(LmsSlotMapDTO signUpSlotMap)
        {
            using (var dbContext = new dbEduegateLmsContext())
            {
                var returnResult = new OperationResultDTO();

                var assignedSlotMapStatsID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_ASSIGNED", 2);
                var cancelledSlotMapStatsID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_CANCEL", 3);

                var entity = dbContext.SignupSlotAllocationMaps
                    .Where(x => x.SignupSlotMapID == signUpSlotMap.SignupSlotMapIID && x.ParentID == signUpSlotMap.ParentID && x.SlotMapStatusID == assignedSlotMapStatsID)
                    .AsNoTracking().FirstOrDefault();

                if (entity != null)
                {
                    entity.SlotMapStatusID = cancelledSlotMapStatsID;
                    entity.UpdatedBy = _context.LoginID.HasValue ? Convert.ToInt32(_context.LoginID) : null;
                    entity.UpdatedDate = DateTime.Now;

                    dbContext.Entry(entity).State = EntityState.Modified;
                    dbContext.SaveChanges();

                    returnResult = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = "Saved successfully!",
                    };
                }

                return returnResult;
            }
        }

        public List<LmsDTO> GetActiveSignUpDetailsByEmployeeID(long employeeID)
        {
            var dtos = new List<LmsDTO>();

            var signupPublishedStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_STATUSID_PUBLISH", 3);

            using (var dbContext = new dbEduegateLmsContext())
            {
                var entities = dbContext.Signups
                    .Where(x => x.IsActive == true && x.SignupStatusID == signupPublishedStatusID && (employeeID != 0 ? x.OrganizerEmployeeID == employeeID : x.OrganizerEmployeeID != 0))
                    .Include(x => x.SignupGroup)
                    .Include(i => i.School)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.OrganizerEmployee)
                    .Include(i => i.SignupCategory)
                    .Include(i => i.SignupType)
                    .Include(i => i.SignupStatus)
                    .Include(i => i.SignupAudienceMaps).ThenInclude(i => i.Student)
                    .Include(i => i.SignupAudienceMaps).ThenInclude(i => i.Parent)
                    .Include(i => i.SignupAudienceMaps).ThenInclude(i => i.Employee)
                    .Include(i => i.SignupSlotMaps).ThenInclude(i => i.SlotMapStatus)
                    .Include(i => i.SignupSlotMaps).ThenInclude(i => i.SignupSlotAllocationMaps).ThenInclude(i => i.Student)
                    .Include(i => i.SignupSlotMaps).ThenInclude(i => i.SignupSlotAllocationMaps).ThenInclude(i => i.Parent)
                    .Include(i => i.SignupSlotMaps).ThenInclude(i => i.SignupSlotAllocationMaps).ThenInclude(i => i.Employee)
                    .AsNoTracking()
                    .ToList();

                foreach (var entity in entities)
                {
                    dtos.Add(ToDTO(entity));
                }
            }

            return dtos;
        }

        private void SendPushNotification(LmsDTO signDTO)
        {
            var loginIDs = new List<long?>();

            var message = "Meeting Status changed.";
            var title = "Meeting Changes";
            var settings = NotificationSetting.GetParentAppSettings();

            FillDetailsForNotification(signDTO, message, loginIDs);

            foreach (var login in loginIDs)
            {
                long toLoginID = Convert.ToInt32(login);
                long fromLoginID = _context?.LoginID != null ? (long)_context.LoginID : 2;

                PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings);
            }
        }

        private void FillDetailsForNotification(LmsDTO signDTO, string message, List<long?> loginIDs)
        {
            var signupPublishedStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_STATUSID_PUBLISH", 3);

            var signupCancelledStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_STATUSID_CANCEL", 2);

            var signupClosedStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_STATUSID_CLOSED", 4);

            if (signDTO.SignupOldStatusID.HasValue)
            {
                if (signDTO.SignupOldStatusID == signupPublishedStatusID && signDTO.SignupStatusID != signupPublishedStatusID)
                {
                    if (signDTO.SignupStatusID == signupCancelledStatusID)
                    {
                        message = "Sorry, the scheduled meeting has been cancelled.";
                    }

                    if (signDTO.SignupSlotMaps.Count > 0)
                    {
                        foreach (var slotMap in signDTO.SignupSlotMaps)
                        {
                            var slotMapAlloc = GetAssignedSlotAllocationDetailBySlotMapID(slotMap.SignupSlotMapIID);
                            if (slotMapAlloc != null)
                            {
                                if (slotMapAlloc.StudentID.HasValue)
                                {
                                    var loginID = new Eduegate.Domain.School.ParentBL(_context).GetParentLoginIDByStudentID(slotMapAlloc.StudentID);
                                    loginIDs.Add(loginID);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (signDTO.SignupStatusID == signupPublishedStatusID)
                {
                    message = "A new meeting is scheduled. Please check and select your slot for the meeting.";

                    if (signDTO.ClassID.HasValue)
                    {
                        if (signDTO.SectionID.HasValue)
                        {
                            loginIDs = new Eduegate.Domain.School.ParentBL(_context).GetParentsLoginIDByClassSection(signDTO.ClassID, signDTO.SectionID);
                        }
                        else
                        {
                            loginIDs = new Eduegate.Domain.School.ParentBL(_context).GetParentsLoginIDByClassSection(signDTO.ClassID, null);
                        }
                    }
                    if (signDTO.StudentID.HasValue)
                    {
                        var loginID = new Eduegate.Domain.School.ParentBL(_context).GetParentLoginIDByStudentID(signDTO.StudentID);
                        loginIDs.Add(loginID);
                    }
                }
            }
        }

        public LmsSlotAllocationMapDTO GetAssignedSlotAllocationDetailBySlotMapID(long signupSlotMapID)
        {
            var assignedSlotMapStatsID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_ASSIGNED", 2);

            var LmsSlotAllocationMapDTO = new LmsSlotAllocationMapDTO();

            using (var dbContext = new dbEduegateLmsContext())
            {
                var entity = dbContext.SignupSlotAllocationMaps
                    .Where(s => s.SignupSlotMapID == signupSlotMapID && s.SlotMapStatusID == assignedSlotMapStatsID)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (entity != null)
                {
                    LmsSlotAllocationMapDTO = FillLmsSlotAllocationMapDTO(entity);
                }
            }

            return LmsSlotAllocationMapDTO;
        }

        private void SendEmailNotification(LmsDTO signDTO)
        {
            var signupPublishedStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_STATUSID_PUBLISH", 3);

            var signupCancelledStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_STATUSID_CANCEL", 2);

            var signupClosedStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_STATUSID_CLOSED", 4);

            var parentEmails = new List<string>();
            if (signDTO.ClassID.HasValue)
            {
                if (signDTO.SectionID.HasValue)
                {
                    parentEmails = new Eduegate.Domain.School.ParentBL(_context).GetParentsEmailIDByClassSection(signDTO.ClassID, signDTO.SectionID);
                }
                else
                {
                    parentEmails = new Eduegate.Domain.School.ParentBL(_context).GetParentsEmailIDByClassSection(signDTO.ClassID, null);
                }
            }
            if (signDTO.StudentID.HasValue)
            {
                var emailID = new Eduegate.Domain.School.ParentBL(_context).GetParentEmailIDByStudentID(signDTO.StudentID);
                parentEmails.Add(emailID);
            }

            var settings = NotificationSetting.GetParentAppSettings();
            string schoolShortName = null;
            foreach (var emailID in parentEmails)
            {
                EmailProcess(emailID, schoolShortName);
            }
        }

        public void EmailProcess(string toEmail, string schoolShortName)
        {
            var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

            string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

            string emailBody = @"<br />
                        <p align='left'>
                        Dear Parent/Guardian,<br /></p>
                        A Meeting related changes happened.<br />
                        To view the changes,  please login to your parent portal and check<br /><br />
                        <br/><br/><p style='font-size:0.7rem;'<b>Please Note: </b>do not reply to this email as it is a computer generated email</p>";

            string emailSubject = "Meeting";

            try
            {
                schoolShortName = schoolShortName?.ToLower();
                string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(toEmail, emailBody);

                var mailParameters = new Dictionary<string, string>()
                {
                    { "SCHOOL_SHORT_NAME", schoolShortName},
                };

                if (emailBody != "")
                {
                    if (hostDet.ToLower() == "live")
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(toEmail, emailSubject, mailMessage, EmailTypes.SignupMeeting, mailParameters);
                    }
                    else
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSubject, mailMessage, EmailTypes.SignupMeeting, mailParameters);
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Circular Mailing failed. Error message: {errorMessage}", ex);
            }
        }

        private void SendSlotBookingAndCancelPushNotification(LmsSlotAllocationMapDTO allocDTO)
        {
            var slotAssignedStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_ASSIGNED", 2);

            var slotCancelledStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_CANCEL", 3);

            var slotClosedStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_CLOSED", 4);

            var message = "Meeting Status changed.";
            var title = "Meeting Appointment - PTA meeting";
            var settings = NotificationSetting.GetParentAppSettings();

            if (allocDTO.SlotMapStatusID == slotAssignedStatusID)
            {
                message = $"Your appointment for the Parent-Teacher Meeting with {allocDTO.SignupOrganizerEmployeeName} on {allocDTO.SlotDateString} at {allocDTO.SlotTimeString} has been confirmed.  Thank you!";
            }

            else if (allocDTO.SlotMapStatusID == slotCancelledStatusID)
            {
                message = "Successfully cancelled the booked slot.";
            }
            else if (allocDTO.SlotMapStatusID == slotClosedStatusID)
            {
                message = "Your booked slot has been closed.";
            }

            long toLoginID = _context != null && _context.LoginID.HasValue ? Convert.ToInt64(_context.LoginID) : 0;

            long fromLoginID = _context?.LoginID != null ? (long)_context.LoginID : 2;

            PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings);
        }

        public List<LmsDTO> GetActiveMeetingRequestDetailsByEmployeeID(long employeeID)
        {
            var dtos = new List<LmsDTO>();

            var signupPublishedStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_STATUSID_PUBLISH", 3);
            var meetingRequestTypeID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_TYPEID_MEETING_REQUEST", 2);

            using (var dbContext = new dbEduegateLmsContext())
            {
                var entities = dbContext.Signups
                    .Where(x => x.IsActive == true && x.SignupStatusID == signupPublishedStatusID && x.OrganizerEmployeeID == employeeID && x.SignupTypeID == meetingRequestTypeID)
                    .Include(x => x.SignupGroup)
                    .Include(i => i.School)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.OrganizerEmployee)
                    .Include(i => i.SignupCategory)
                    .Include(i => i.SignupType)
                    .Include(i => i.SignupStatus)
                    .Include(i => i.SignupAudienceMaps).ThenInclude(i => i.Student)
                    .Include(i => i.SignupAudienceMaps).ThenInclude(i => i.Parent)
                    .Include(i => i.SignupAudienceMaps).ThenInclude(i => i.Employee)
                    .Include(i => i.SignupSlotMaps).ThenInclude(i => i.SlotMapStatus)
                    .Include(i => i.SignupSlotMaps).ThenInclude(i => i.SignupSlotAllocationMaps).ThenInclude(i => i.Student)
                    .Include(i => i.SignupSlotMaps).ThenInclude(i => i.SignupSlotAllocationMaps).ThenInclude(i => i.Parent)
                    .Include(i => i.SignupSlotMaps).ThenInclude(i => i.SignupSlotAllocationMaps).ThenInclude(i => i.Employee)
                    .AsNoTracking()
                    .ToList();

                foreach (var entity in entities)
                {
                    dtos.Add(ToDTO(entity));
                }
            }

            return dtos;
        }

        public List<KeyValueDTO> GetAvailableSlotsByDate(string stringDate)
        {
            var keyValueDTOs = new List<KeyValueDTO>();

            var signupPublishedStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_STATUSID_PUBLISH", 3);
            var meetingRequestTypeID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_TYPEID_MEETING_REQUEST", 2);

            var slotOpenedStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_OPEN", 1);

            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var timeFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TimeFormatWithoutSecond");

            var slotDate = DateTime.ParseExact(stringDate, dateFormat, CultureInfo.InvariantCulture);

            using (var dbContext = new dbEduegateLmsContext())
            {
                var slotEntities = dbContext.SignupSlotMaps.Where(x => x.SlotDate == slotDate && x.SlotMapStatusID == slotOpenedStatusID
                //&& (x.Signup.SignupGroup != null && x.Signup.SignupGroup.IsActive == true)
                && x.Signup.IsActive == true && x.Signup.SignupStatusID == signupPublishedStatusID && x.Signup.SignupTypeID == meetingRequestTypeID)
                    .Include(i => i.Signup).ThenInclude(i => i.SignupGroup)
                    .AsNoTracking()
                    .ToList();

                foreach (var entity in slotEntities)
                {
                    keyValueDTOs.Add(new KeyValueDTO()
                    {
                        Key = entity.SignupSlotMapIID.ToString(),
                        Value = DateTime.Today.Add(entity.StartTime.Value).ToString(timeFormat, CultureInfo.InvariantCulture) + " - " + DateTime.Today.Add(entity.EndTime.Value).ToString(timeFormat, CultureInfo.InvariantCulture)
                    });
                }
            }

            return keyValueDTOs;
        }

        public string SaveSignupSlotAllocation(LmsSlotAllocationMapDTO slotAllocMapDTO)
        {
            using (var dbContext = new dbEduegateLmsContext())
            {
                var slotAssignedStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_ASSIGNED", 2);

                var entity = new SignupSlotAllocationMap()
                {
                    SignupSlotAllocationMapIID = slotAllocMapDTO.SignupSlotAllocationMapIID,
                    SignupSlotMapID = slotAllocMapDTO.SignupSlotMapID,
                    StudentID = slotAllocMapDTO.StudentID,
                    EmployeeID = slotAllocMapDTO.EmployeeID,
                    ParentID = slotAllocMapDTO.ParentID,
                    SchoolID = slotAllocMapDTO.SchoolID,
                    AcademicYearID = slotAllocMapDTO.AcademicYearID,
                    SlotMapStatusID = slotAllocMapDTO.SlotMapStatusID,
                    CreatedBy = slotAllocMapDTO.SignupSlotAllocationMapIID == 0 ? (int)_context.LoginID : slotAllocMapDTO.CreatedBy,
                    UpdatedBy = slotAllocMapDTO.SignupSlotAllocationMapIID > 0 ? (int)_context.LoginID : slotAllocMapDTO.UpdatedBy,
                    CreatedDate = slotAllocMapDTO.SignupSlotAllocationMapIID == 0 ? DateTime.Now : slotAllocMapDTO.CreatedDate,
                    UpdatedDate = slotAllocMapDTO.SignupSlotAllocationMapIID > 0 ? DateTime.Now : slotAllocMapDTO.UpdatedDate,
                };

                try
                {
                    dbContext.SignupSlotAllocationMaps.Add(entity);

                    if (entity.SignupSlotAllocationMapIID == 0)
                    {
                        dbContext.Entry(entity).State = EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(entity).State = EntityState.Modified;
                    }
                    dbContext.SaveChanges();

                    return "Saved successfully!";
                }
                catch (Exception ex)
                {
                    var errorMessage = ex.Message;
                    return null;
                }
            }
        }

    }
}