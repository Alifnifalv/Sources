using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.School.Transports;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Eduegate.Domain.Entity.Contents;
using Eduegate.Domain.Mappers.Notification.Notifications;
using Eduegate.Domain.Mappers.Notification.Notifications.Helpers;

namespace Eduegate.Domain.Mappers.School.Transports
{
    public class DriverScheduleLogMapper : DTOEntityDynamicMapper
    {
        public static DriverScheduleLogMapper Mapper(CallContext context)
        {
            var mapper = new DriverScheduleLogMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<DriverScheduleLogDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private DriverScheduleLogDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.DriverScheduleLogs.Where(x => x.DriverScheduleLogIID == IID)
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.Employee)
                    .Include(i => i.ScheduleLogStatus)
                    .Include(i => i.StopEntryStatus)
                    .Include(i => i.Routes1)
                    .Include(i => i.RouteStopMap)
                    .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                    .AsNoTracking()
                    .FirstOrDefault();

                var scheduleDatas = dbContext.DriverScheduleLogs.Where(s => s.StopEntryStatusID == entity.StopEntryStatusID &&
                (s.SheduleDate.Value.Day == entity.SheduleDate.Value.Day && s.SheduleDate.Value.Month == entity.SheduleDate.Value.Month &&
                s.SheduleDate.Value.Year == entity.SheduleDate.Value.Year) && s.RouteID == entity.RouteID && s.VehicleID == entity.VehicleID
                && s.RouteStopMapID == entity.RouteStopMapID &&
                (entity.StudentID.HasValue ? s.StudentID == entity.StudentID : s.EmployeeID == entity.EmployeeID))
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.Employee)
                    .Include(i => i.ScheduleLogStatus)
                    .Include(i => i.StopEntryStatus)
                    .Include(i => i.Routes1)
                    .Include(i => i.RouteStopMap)
                    .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                    .AsNoTracking().ToList();

                scheduleDatas = scheduleDatas.OrderBy(o => o.DriverScheduleLogIID).ToList();

                var inLog = scheduleDatas.FirstOrDefault(x => x.ScheduleLogType.ToLower().Contains("in"));

                var outLog = scheduleDatas.FirstOrDefault(x => x.ScheduleLogType.ToLower().Contains("out"));

                var routeStopMapDTO = new DriverScheduleLogDTO()
                {
                    DriverScheduleLogIID = entity.DriverScheduleLogIID,
                    StudentID = entity.StudentID,
                    StudentName = entity.StudentID.HasValue ? entity.Student != null ? entity.Student.AdmissionNumber + " - " + entity.Student.FirstName + " " + entity.Student.MiddleName + " " + entity.Student.LastName : null : null,
                    ClassName = entity.StudentID.HasValue ? entity.Student?.Class?.ClassDescription : null,
                    SectionName = entity.StudentID.HasValue ? entity.Student?.Section?.SectionName : null,
                    EmployeeID = entity.EmployeeID,
                    StaffName = entity.EmployeeID.HasValue ? entity.Employee != null ? entity.Employee.EmployeeCode + " - " + entity.Employee.FirstName.Trim() + " " + (string.IsNullOrEmpty(entity.Employee.MiddleName) ? "" : entity.Employee.MiddleName.Trim() + " ") + entity.Employee.LastName.Trim() : null : null,
                    SheduleDate = entity.SheduleDate,
                    RouteID = entity.RouteID,
                    RouteCode = entity.RouteID.HasValue ? entity.Routes1?.RouteCode : null,
                    RouteGroupID = entity.RouteID.HasValue ? entity.Routes1?.RouteGroupID : null,
                    RouteStopMapID = entity.RouteStopMapID,
                    StopName = entity.RouteStopMapID.HasValue ? entity.RouteStopMap?.StopName : null,
                    VehicleID = entity.VehicleID,
                    VehicleRegistrationNumber = entity.VehicleID.HasValue ? entity.Vehicle.VehicleRegistrationNumber : null,
                    SheduleLogStatusID = entity.SheduleLogStatusID,
                    StopEntryStatusID = entity.StopEntryStatusID,
                    Status = entity.Status,
                    ScheduleLogType = entity.ScheduleLogType,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                };

                routeStopMapDTO.InLog = new ScheduleLogInfoDTO()
                {
                    IID = inLog != null ? inLog.DriverScheduleLogIID : 0,
                    StatusID = inLog != null && inLog.SheduleLogStatusID.HasValue ? inLog.SheduleLogStatusID : 0,
                    Status = inLog?.Status,
                    ScheduleLogType = inLog?.ScheduleLogType
                };

                routeStopMapDTO.OutLog = new ScheduleLogInfoDTO()
                {
                    IID = outLog != null ? outLog.DriverScheduleLogIID : 0,
                    StatusID = outLog != null && outLog.SheduleLogStatusID.HasValue ? outLog.SheduleLogStatusID : 0,
                    Status = outLog?.Status,
                    ScheduleLogType = outLog?.ScheduleLogType
                };

                return routeStopMapDTO;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as DriverScheduleLogDTO;

            if (!toDto.EmployeeID.HasValue && !toDto.StudentID.HasValue)
            {
                throw new Exception("Employee or student is required!");
            }
            if (!toDto.RouteID.HasValue)
            {
                throw new Exception("Route is required!");
            }
            if (!toDto.RouteStopMapID.HasValue)
            {
                throw new Exception("Stop is required!");
            }
            if (!toDto.VehicleID.HasValue)
            {
                throw new Exception("Vehicle is required!");
            }

            long getLogIID = 0;

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var scheduleInData = dbContext.DriverScheduleLogs
                    .Where(s => s.DriverScheduleLogIID == toDto.InLog.IID)
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.Employee)
                    .Include(i => i.ScheduleLogStatus)
                    .Include(i => i.StopEntryStatus)
                    .Include(i => i.Routes1)
                    .Include(i => i.RouteStopMap)
                    .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                    .AsNoTracking().FirstOrDefault();

                if (scheduleInData != null)
                {
                    getLogIID = toDto.InLog.IID;
                    scheduleInData.SheduleLogStatusID = toDto.InLog.StatusID;
                    scheduleInData.Status = toDto.InLog.Status;
                    scheduleInData.ScheduleLogType = toDto.InLog.ScheduleLogType;
                    scheduleInData.UpdatedBy = (int)_context.LoginID;
                    scheduleInData.UpdatedDate = DateTime.Now;

                    dbContext.Entry(scheduleInData).State = EntityState.Modified;
                }
                else
                {
                    getLogIID = AddMissingLogs(toDto, toDto.InLog);
                }

                var scheduleOutData = dbContext.DriverScheduleLogs
                    .Where(s => s.DriverScheduleLogIID == toDto.OutLog.IID)
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.Employee)
                    .Include(i => i.ScheduleLogStatus)
                    .Include(i => i.StopEntryStatus)
                    .Include(i => i.Routes1)
                    .Include(i => i.RouteStopMap)
                    .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                    .AsNoTracking().FirstOrDefault();

                if (scheduleOutData != null)
                {
                    getLogIID = toDto.OutLog.IID;
                    scheduleOutData.SheduleLogStatusID = toDto.OutLog.StatusID;
                    scheduleOutData.Status = toDto.OutLog.Status;
                    scheduleOutData.ScheduleLogType = toDto.OutLog.ScheduleLogType;
                    scheduleOutData.UpdatedBy = (int)_context.LoginID;
                    scheduleOutData.UpdatedDate = DateTime.Now;

                    dbContext.Entry(scheduleOutData).State = EntityState.Modified;
                }
                else
                {
                    getLogIID = AddMissingLogs(toDto, toDto.OutLog);
                }

                dbContext.SaveChanges();
            }


            return ToDTOString(ToDTO(getLogIID));
        }

        private long AddMissingLogs(DriverScheduleLogDTO toDto, ScheduleLogInfoDTO logInfo)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new DriverScheduleLog()
                {
                    DriverScheduleLogIID = toDto.DriverScheduleLogIID,
                    StudentID = toDto.StudentID,
                    EmployeeID = toDto.EmployeeID,
                    SheduleDate = toDto.SheduleDate.HasValue ? toDto.SheduleDate : DateTime.Now.Date,
                    RouteID = toDto.RouteID,
                    RouteStopMapID = toDto.RouteStopMapID,
                    VehicleID = toDto.VehicleID,
                    SheduleLogStatusID = logInfo.StatusID,
                    StopEntryStatusID = toDto.StopEntryStatusID,
                    Status = logInfo.Status,
                    ScheduleLogType = logInfo.ScheduleLogType,
                    CreatedBy = toDto.DriverScheduleLogIID == 0 ? (int)_context.LoginID : toDto.CreatedBy,
                    CreatedDate = toDto.DriverScheduleLogIID == 0 ? DateTime.Now : toDto.CreatedDate,
                    UpdatedBy = toDto.DriverScheduleLogIID > 0 ? (int)_context.LoginID : toDto.UpdatedBy,
                    UpdatedDate = toDto.DriverScheduleLogIID > 0 ? DateTime.Now : toDto.UpdatedDate,
                };

                dbContext.DriverScheduleLogs.Add(entity);

                dbContext.SaveChanges();

                return entity.DriverScheduleLogIID;
            }
        }

        public List<VehicleDTO> GetAllRouteStudentsAndStaffDetails()
        {
            var dtos = new List<VehicleDTO>();
            var routeDtos = new List<RoutesDTO>();
            var stopDtos = new List<RouteStopFeeDTO>();
            var studentStopDtos = new List<StudentRouteStopMapDTO>();
            var staffStopDtos = new List<StaffRouteStopMapDTO>();

            DateTime date = DateTime.Now;

            var currentDate = DateTime.Now;

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                int? currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);

                var todayDate = DateTime.Now.Date;

                var pickupStopEntry = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STOP_ENTRY_STATUS_ID_PICKUP");
                var dropStopEntry = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STOP_ENTRY_STATUS_ID_DROP");
                var rejectedTransportStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSPORT_STATUS_REJECT");

                int? stopEntryPickupID = int.Parse(pickupStopEntry);
                int? stopEntryDropID = int.Parse(dropStopEntry);
                long? rejectedTransportStatusID = int.Parse(rejectedTransportStatus);
                long? loginID = 0;

                var assignVehicleDetails = dbContext.AssignVehicleMaps.Where(a => a.IsActive == true &&
                a.Routes1.RouteGroup.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID &&
                a.Routes1.IsActive == true && a.Routes1.RouteGroup.AcademicYear.IsActive == true)
                    .Include(i => i.Employee)
                    .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                    .AsNoTracking().ToList();

                if (assignVehicleDetails.Count > 0)
                {
                    List<long?> employeeIDs = assignVehicleDetails.Select(x => x.EmployeeID).ToList();
                    var employeeDet = dbContext.Employees.Where(e => employeeIDs.Contains(e.EmployeeIID) && e.IsActive == true).AsNoTracking().ToList();

                    foreach (var assign in assignVehicleDetails)
                    {
                        loginID = employeeDet.Where(x => x.EmployeeIID == assign.EmployeeID).Select(x => x.LoginID.Value).FirstOrDefault();

                        var vehicleMapDetails = dbContext.RouteVehicleMaps.Where(v => v.RouteID == assign.RouteID &&
                        v.Routes1.RouteGroup.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID &&
                        v.IsActive == true && v.Routes1.IsActive == true && v.Routes1.RouteGroup.AcademicYear.IsActive == true)
                            .Include(i => i.Routes1)
                            .Include(i => i.Vehicle)
                            .Include(i => i.AcademicYear)
                            .Include(i => i.School)
                            .AsNoTracking().ToList();

                        if (vehicleMapDetails != null && vehicleMapDetails.Count > 0)
                        {
                            foreach (var vehicleMap in vehicleMapDetails)
                            {
                                routeDtos = new List<RoutesDTO>();

                                var vehicleDet = dbContext.Vehicles.Where(v => v.VehicleIID == vehicleMap.VehicleID && v.IsActive == true)
                                    .Include(i => i.VehicleType)
                                    .AsNoTracking().FirstOrDefault();

                                var routeDetails = dbContext.Routes1.Where(b => b.RouteID == assign.RouteID && b.IsActive == true &&
                                b.RouteGroup.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID &&
                                b.RouteGroup.IsActive == true)
                                    .AsNoTracking().ToList();

                                if (routeDetails.Count > 0)
                                {
                                    foreach (var route in routeDetails)
                                    {
                                        stopDtos = new List<RouteStopFeeDTO>();

                                        var stopDetails = dbContext.RouteStopMaps
                                            .Where(s => s.RouteID == route.RouteID && s.IsActive == true)
                                            .OrderBy(x => x.SequenceNo).AsNoTracking().ToList();

                                        if (stopDetails.Count > 0)
                                        {
                                            foreach (var stop in stopDetails)
                                            {
                                                //Get Student Route Stop Details Start
                                                studentStopDtos = new List<StudentRouteStopMapDTO>();

                                                bool isStudentSynced = false;

                                                var studentPickupStopDetails = dbContext.StudentRouteStopMaps
                                                    .Where(p => p.PickupStopMapID == stop.RouteStopMapIID && p.IsActive == true && p.TransportStatusID != rejectedTransportStatusID &&
                                                    p.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID &&
                                                    ((p.DateFrom >= currentDate.Date && p.DateTo <= currentDate.Date) ||
                                                    (p.DateFrom >= currentDate.Date && p.DateTo >= currentDate.Date) ||
                                                    (p.DateFrom <= currentDate.Date && p.DateTo >= currentDate.Date)))
                                                    .Include(i => i.Student).ThenInclude(i => i.Class)
                                                    .Include(i => i.Student).ThenInclude(i => i.Section)
                                                    .AsNoTracking().ToList();

                                                if (studentPickupStopDetails.Count > 0)
                                                {
                                                    isStudentSynced = SyncStudentPickupScheduleLogDatas(studentPickupStopDetails, route, stop, vehicleDet);
                                                }

                                                var studentDropStopDetails = dbContext.StudentRouteStopMaps
                                                    .Where(d => d.DropStopMapID == stop.RouteStopMapIID && d.IsActive == true && d.TransportStatusID != rejectedTransportStatusID &&
                                                    d.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID &&
                                                    ((d.DateFrom >= currentDate.Date && d.DateTo <= currentDate.Date) ||
                                                    (d.DateFrom >= currentDate.Date && d.DateTo >= currentDate.Date) ||
                                                    (d.DateFrom <= currentDate.Date && d.DateTo >= currentDate.Date)))
                                                    .Include(i => i.Student).ThenInclude(i => i.Class)
                                                    .Include(i => i.Student).ThenInclude(i => i.Section)
                                                    .AsNoTracking().ToList();

                                                if (studentDropStopDetails.Count > 0)
                                                {
                                                    isStudentSynced = SyncStudentDropScheduleLogDatas(studentDropStopDetails, route, stop, vehicleDet);
                                                }

                                                if (isStudentSynced == true)
                                                {
                                                    var driverStudentScheduleDetails = dbContext.DriverScheduleLogs.Where(x => x.StudentID.HasValue && x.Status != "O")
                                                        .Include(i => i.Student).ThenInclude(i => i.Class)
                                                        .Include(i => i.Student).ThenInclude(i => i.Section)
                                                        .Include(i => i.Employee)
                                                        .Include(i => i.ScheduleLogStatus)
                                                        .Include(i => i.StopEntryStatus)
                                                        .Include(i => i.Routes1)
                                                        .Include(i => i.RouteStopMap)
                                                        .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                                                        .AsNoTracking().ToList();

                                                    if (studentPickupStopDetails.Count > 0)
                                                    {
                                                        foreach (var pickupStop in studentPickupStopDetails)
                                                        {
                                                            var isStudentIn = false;

                                                            var scheduleFilterDet = driverStudentScheduleDetails
                                                                .Where(s => s.StudentID == pickupStop.StudentID &&
                                                                (s.SheduleDate.Value.Day == todayDate.Day && s.SheduleDate.Value.Month == todayDate.Month &&
                                                                s.SheduleDate.Value.Year == todayDate.Year) && s.StopEntryStatusID == stopEntryPickupID)
                                                                .OrderByDescending(o => o.DriverScheduleLogIID).FirstOrDefault();

                                                            if (scheduleFilterDet != null)
                                                            {
                                                                if (scheduleFilterDet.SheduleLogStatusID == 1)
                                                                {
                                                                    isStudentIn = true;
                                                                }
                                                                else
                                                                {
                                                                    isStudentIn = false;
                                                                }
                                                            }

                                                            studentStopDtos.Add(new StudentRouteStopMapDTO()
                                                            {
                                                                StudentRouteStopMapIID = pickupStop.StudentRouteStopMapIID,
                                                                StudentID = pickupStop.StudentID,
                                                                AdmissionNumber = pickupStop.StudentID.HasValue ? pickupStop.Student != null ? pickupStop.Student.AdmissionNumber : null : null,
                                                                StudentName = pickupStop.StudentID.HasValue ? pickupStop.Student != null ? pickupStop.Student.FirstName + " " + pickupStop.Student.MiddleName + " " + pickupStop.Student.LastName : null : null,
                                                                Student = pickupStop.StudentID.HasValue ? pickupStop.Student != null ? new KeyValueDTO() { Key = pickupStop.Student.StudentIID.ToString(), Value = pickupStop.Student.AdmissionNumber + " - " + pickupStop.Student.FirstName + " " + pickupStop.Student.MiddleName + " " + pickupStop.Student.LastName } : new KeyValueDTO() : new KeyValueDTO(),
                                                                StudentProfile = pickupStop.StudentID.HasValue ? pickupStop.Student?.StudentProfile : null,
                                                                IsOneWay = pickupStop.IsOneWay,
                                                                IsActive = pickupStop.IsActive,
                                                                SchoolID = pickupStop.SchoolID,
                                                                ClassID = pickupStop.Student != null ? pickupStop.Student.ClassID : null,
                                                                ClassName = pickupStop.Student != null ? pickupStop.Student.ClassID.HasValue && pickupStop.Student.Class != null ? pickupStop.Student.Class.ClassDescription : "NA" : "NA",
                                                                SectionID = pickupStop.Student != null ? pickupStop.Student.SectionID : null,
                                                                SectionName = pickupStop.Student != null ? pickupStop.Student.SectionID.HasValue && pickupStop.Student.Section != null ? pickupStop.Student.Section.SectionName : "NA" : "NA",
                                                                AcademicYearID = pickupStop.AcademicYearID,
                                                                Termsandco = pickupStop.Termsandco,
                                                                DateFrom = pickupStop.DateFrom,
                                                                DateTo = pickupStop.DateTo,
                                                                IsPickupStop = true,
                                                                IsDropStop = false,
                                                                IsStudentIn = isStudentIn,
                                                                DriverScheduleLogIID = scheduleFilterDet != null ? scheduleFilterDet.DriverScheduleLogIID : 0,
                                                            });
                                                        }
                                                    }

                                                    if (studentDropStopDetails.Count > 0)
                                                    {
                                                        foreach (var dropStop in studentDropStopDetails)
                                                        {
                                                            var isStudentIn = false;

                                                            var scheduleFilterDet = driverStudentScheduleDetails
                                                                .Where(s => s.StudentID == dropStop.StudentID &&
                                                                (s.SheduleDate.Value.Day == todayDate.Day && s.SheduleDate.Value.Month == todayDate.Month &&
                                                                s.SheduleDate.Value.Year == todayDate.Year) && s.StopEntryStatusID == stopEntryDropID)
                                                                .OrderByDescending(o => o.DriverScheduleLogIID).FirstOrDefault();

                                                            if (scheduleFilterDet != null)
                                                            {
                                                                if (scheduleFilterDet.SheduleLogStatusID == 1)
                                                                {
                                                                    isStudentIn = true;
                                                                }
                                                                else
                                                                {
                                                                    isStudentIn = false;
                                                                }
                                                            }

                                                            studentStopDtos.Add(new StudentRouteStopMapDTO()
                                                            {
                                                                StudentRouteStopMapIID = dropStop.StudentRouteStopMapIID,
                                                                StudentID = dropStop.StudentID,
                                                                AdmissionNumber = dropStop.StudentID.HasValue ? dropStop.Student != null ? dropStop.Student.AdmissionNumber : null : null,
                                                                StudentName = dropStop.StudentID.HasValue ? dropStop.Student != null ? dropStop.Student.FirstName + " " + dropStop.Student.MiddleName + " " + dropStop.Student.LastName : null : null,
                                                                Student = dropStop.StudentID.HasValue ? dropStop.Student != null ? new KeyValueDTO() { Key = dropStop.Student.StudentIID.ToString(), Value = dropStop.Student.AdmissionNumber + " - " + dropStop.Student.FirstName + " " + dropStop.Student.MiddleName + " " + dropStop.Student.LastName } : new KeyValueDTO() : null,
                                                                StudentProfile = dropStop.StudentID.HasValue ? dropStop.Student?.StudentProfile : null,
                                                                IsOneWay = dropStop.IsOneWay,
                                                                IsActive = dropStop.IsActive,
                                                                SchoolID = dropStop.SchoolID,
                                                                ClassID = dropStop.Student != null ? dropStop.Student.ClassID : null,
                                                                ClassName = dropStop.Student != null ? dropStop.Student.ClassID.HasValue && dropStop.Student.Class != null ? dropStop.Student.Class.ClassDescription : "NA" : "NA",
                                                                SectionID = dropStop.Student != null ? dropStop.Student.SectionID : null,
                                                                SectionName = dropStop.Student != null ? dropStop.Student.SectionID.HasValue && dropStop.Student.Section != null ? dropStop.Student.Section.SectionName : "NA" : "NA",
                                                                AcademicYearID = dropStop.AcademicYearID,
                                                                Termsandco = dropStop.Termsandco,
                                                                DateFrom = dropStop.DateFrom,
                                                                DateTo = dropStop.DateTo,
                                                                IsPickupStop = false,
                                                                IsDropStop = true,
                                                                IsStudentIn = isStudentIn,
                                                                DriverScheduleLogIID = scheduleFilterDet != null ? scheduleFilterDet.DriverScheduleLogIID : 0,
                                                            });
                                                        }
                                                    }
                                                }
                                                //Get Student Route Stop Details End

                                                //Get Staff Route Stop Details Start
                                                staffStopDtos = new List<StaffRouteStopMapDTO>();

                                                bool isStaffSynced = false;

                                                var staffPickupStopDetails = dbContext.StaffRouteStopMaps
                                                    .Where(p => p.PickupStopMapID == stop.RouteStopMapIID && p.IsActive == true && p.TransportStatusID != rejectedTransportStatusID &&
                                                    p.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID &&
                                                    ((p.DateFrom >= currentDate.Date && p.DateTo <= currentDate.Date) ||
                                                    (p.DateFrom >= currentDate.Date && p.DateTo >= currentDate.Date) ||
                                                    (p.DateFrom <= currentDate.Date && p.DateTo >= currentDate.Date)))
                                                    .Include(i => i.Employee)
                                                    .AsNoTracking().ToList();

                                                if (staffPickupStopDetails.Count > 0)
                                                {
                                                    isStaffSynced = SyncStaffPickupScheduleLogDatas(staffPickupStopDetails, route, stop, vehicleDet);
                                                }

                                                var staffDropStopDetails = dbContext.StaffRouteStopMaps
                                                    .Where(d => d.DropStopMapID == stop.RouteStopMapIID && d.IsActive == true && d.TransportStatusID != rejectedTransportStatusID &&
                                                    d.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID &&
                                                    ((d.DateFrom >= currentDate.Date && d.DateTo <= currentDate.Date) ||
                                                    (d.DateFrom >= currentDate.Date && d.DateTo >= currentDate.Date) ||
                                                    (d.DateFrom <= currentDate.Date && d.DateTo >= currentDate.Date))).ToList();

                                                if (staffDropStopDetails.Count > 0)
                                                {
                                                    isStaffSynced = SyncStaffDropScheduleLogDatas(staffDropStopDetails, route, stop, vehicleDet);
                                                }

                                                if (isStaffSynced == true)
                                                {
                                                    var driverStaffScheduleDetails = dbContext.DriverScheduleLogs
                                                        .Where(x => x.EmployeeID.HasValue && x.Status != "O")
                                                        .Include(i => i.Student).ThenInclude(i => i.Class)
                                                        .Include(i => i.Student).ThenInclude(i => i.Section)
                                                        .Include(i => i.Employee)
                                                        .Include(i => i.ScheduleLogStatus)
                                                        .Include(i => i.StopEntryStatus)
                                                        .Include(i => i.Routes1)
                                                        .Include(i => i.RouteStopMap)
                                                        .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                                                        .AsNoTracking().ToList();

                                                    if (staffPickupStopDetails.Count > 0)
                                                    {
                                                        foreach (var pickupStop in staffPickupStopDetails)
                                                        {
                                                            var isStaffIn = false;

                                                            var scheduleFilterDet = driverStaffScheduleDetails.Where(s => s.EmployeeID == pickupStop.StaffID && (s.SheduleDate.Value.Day == todayDate.Day && s.SheduleDate.Value.Month == todayDate.Month && s.SheduleDate.Value.Year == todayDate.Year) && s.StopEntryStatusID == stopEntryPickupID)
                                                                .OrderByDescending(o => o.DriverScheduleLogIID).FirstOrDefault();
                                                            if (scheduleFilterDet != null)
                                                            {
                                                                if (scheduleFilterDet.SheduleLogStatusID == 1)
                                                                {
                                                                    isStaffIn = true;
                                                                }
                                                                else
                                                                {
                                                                    isStaffIn = false;
                                                                }
                                                            }

                                                            staffStopDtos.Add(new StaffRouteStopMapDTO()
                                                            {
                                                                StaffRouteStopMapIID = pickupStop.StaffRouteStopMapIID,
                                                                StaffID = pickupStop.StaffID,
                                                                EmployeeCode = pickupStop.StaffID.HasValue ? pickupStop.Employee != null ? pickupStop.Employee.EmployeeCode : null : null,
                                                                StaffName = pickupStop.StaffID.HasValue ? pickupStop.Employee != null ? pickupStop.Employee.FirstName + " " + pickupStop.Employee.MiddleName + " " + pickupStop.Employee.LastName : null : null,
                                                                Staff = pickupStop.StaffID.HasValue ? pickupStop.Employee != null ? new KeyValueDTO()
                                                                {
                                                                    Key = pickupStop.Employee.EmployeeIID.ToString(),
                                                                    Value = pickupStop.Employee.EmployeeCode + " - " + pickupStop.Employee.FirstName + " " + pickupStop.Employee.MiddleName + " " + pickupStop.Employee.LastName
                                                                } : new KeyValueDTO() : new KeyValueDTO(),
                                                                StaffProfile = pickupStop.StaffID.HasValue ? pickupStop.Employee?.EmployeePhoto : null,
                                                                IsOneWay = pickupStop.IsOneWay,
                                                                IsActive = pickupStop.IsActive,
                                                                SchoolID = pickupStop.SchoolID,
                                                                AcademicYearID = pickupStop.AcademicYearID,
                                                                //Termsandco = pickupStop.Termsandco,
                                                                DateFrom = pickupStop.DateFrom,
                                                                DateTo = pickupStop.DateTo,
                                                                IsPickupStop = true,
                                                                IsDropStop = false,
                                                                IsStaffIn = isStaffIn,
                                                                DriverScheduleLogIID = scheduleFilterDet != null ? scheduleFilterDet.DriverScheduleLogIID : 0,
                                                            });
                                                        }
                                                    }

                                                    if (staffDropStopDetails.Count > 0)
                                                    {
                                                        foreach (var dropStop in staffDropStopDetails)
                                                        {
                                                            var isStaffIn = false;

                                                            var scheduleFilterDet = driverStaffScheduleDetails.Where(s => s.EmployeeID == dropStop.StaffID && (s.SheduleDate.Value.Day == todayDate.Day && s.SheduleDate.Value.Month == todayDate.Month && s.SheduleDate.Value.Year == todayDate.Year) && s.StopEntryStatusID == stopEntryDropID)
                                                                .OrderByDescending(o => o.DriverScheduleLogIID).FirstOrDefault();
                                                            if (scheduleFilterDet != null)
                                                            {
                                                                if (scheduleFilterDet.SheduleLogStatusID == 1)
                                                                {
                                                                    isStaffIn = true;
                                                                }
                                                                else
                                                                {
                                                                    isStaffIn = false;
                                                                }
                                                            }

                                                            staffStopDtos.Add(new StaffRouteStopMapDTO()
                                                            {
                                                                StaffRouteStopMapIID = dropStop.StaffRouteStopMapIID,
                                                                StaffID = dropStop.StaffID,
                                                                EmployeeCode = dropStop.StaffID.HasValue ? dropStop.Employee != null ? dropStop.Employee.EmployeeCode : null : null,
                                                                StaffName = dropStop.StaffID.HasValue ? dropStop.Employee != null ? dropStop.Employee.FirstName + " " + dropStop.Employee.MiddleName + " " + dropStop.Employee.LastName : null : null,
                                                                Staff = dropStop.StaffID.HasValue ? dropStop.Employee != null ? new KeyValueDTO() { Key = dropStop.Employee.EmployeeIID.ToString(), Value = dropStop.Employee.EmployeeCode + " - " + dropStop.Employee.FirstName + " " + dropStop.Employee.MiddleName + " " + dropStop.Employee.LastName } : new KeyValueDTO() : null,
                                                                StaffProfile = dropStop.StaffID.HasValue ? dropStop.Employee?.EmployeePhoto : null,
                                                                IsOneWay = dropStop.IsOneWay,
                                                                IsActive = dropStop.IsActive,
                                                                SchoolID = dropStop.SchoolID,
                                                                AcademicYearID = dropStop.AcademicYearID,
                                                                //Termsandco = dropStop.Termsandco,
                                                                DateFrom = dropStop.DateFrom,
                                                                DateTo = dropStop.DateTo,
                                                                IsPickupStop = false,
                                                                IsDropStop = true,
                                                                IsStaffIn = isStaffIn,
                                                                DriverScheduleLogIID = scheduleFilterDet != null ? scheduleFilterDet.DriverScheduleLogIID : 0,
                                                            });
                                                        }
                                                    }
                                                }
                                                //Get Staff Route Stop Details End

                                                stopDtos.Add(new RouteStopFeeDTO()
                                                {
                                                    RouteStopMapIID = stop.RouteStopMapIID,
                                                    RouteID = stop.RouteID,
                                                    StopName = stop.StopName,
                                                    StopCode = stop.StopCode,
                                                    RouteFareOneWay = stop.OneWayFee,
                                                    RouteFareTwoWay = stop.TwoWayFee,
                                                    IsActive = stop.IsActive,
                                                    AcademicYearID = stop.AcademicYearID,
                                                    StopsStudentDetails = studentStopDtos,
                                                    StopsStaffDetails = staffStopDtos,
                                                });
                                            }
                                        }

                                        routeDtos.Add(new RoutesDTO()
                                        {
                                            RouteID = route.RouteID,
                                            RouteCode = route.RouteCode,
                                            RouteDescription = route.RouteDescription,
                                            IsActive = route.IsActive,
                                            Stops = stopDtos,
                                        });
                                    }
                                }
                                if (vehicleDet != null)
                                {
                                    dtos.Add(new VehicleDTO()
                                    {
                                        VehicleIID = vehicleDet.VehicleIID,
                                        VehicleCode = vehicleDet.VehicleCode,
                                        Color = vehicleDet.Color,
                                        VehicleTypeID = vehicleDet.VehicleTypeID,
                                        VehicleType = vehicleDet.VehicleTypeID.HasValue ? vehicleDet.VehicleType?.VehicleTypeName : "NA",
                                        IsActive = vehicleDet.IsActive,
                                        VehicleRegistrationNumber = vehicleDet.VehicleRegistrationNumber,
                                        RoutesDetails = routeDtos,
                                        LoginID = loginID
                                    });
                                }
                            }
                        }
                    }
                }

            }

            return dtos;
        }

        public long SyncDriverScheduleLogs(DriverScheduleLogDTO scheduleLogData)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new DriverScheduleLog()
                {
                    DriverScheduleLogIID = scheduleLogData.DriverScheduleLogIID,
                    StudentID = scheduleLogData.StudentID,
                    EmployeeID = scheduleLogData.EmployeeID,
                    SheduleDate = scheduleLogData.SheduleDate.HasValue ? scheduleLogData.SheduleDate : DateTime.Now.Date,
                    RouteID = scheduleLogData.RouteID,
                    RouteStopMapID = scheduleLogData.RouteStopMapID,
                    VehicleID = scheduleLogData.VehicleID,
                    SheduleLogStatusID = scheduleLogData.SheduleLogStatusID,
                    StopEntryStatusID = scheduleLogData.StopEntryStatusID,
                    Status = scheduleLogData.Status,
                    ScheduleLogType = scheduleLogData.ScheduleLogType,
                    CreatedBy = scheduleLogData.DriverScheduleLogIID == 0 ? (int)_context.LoginID : scheduleLogData.CreatedBy,
                    UpdatedBy = scheduleLogData.DriverScheduleLogIID > 0 ? (int)_context.LoginID : scheduleLogData.UpdatedBy,
                    CreatedDate = scheduleLogData.DriverScheduleLogIID == 0 ? DateTime.Now : scheduleLogData.CreatedDate,
                    UpdatedDate = scheduleLogData.DriverScheduleLogIID > 0 ? DateTime.Now : scheduleLogData.UpdatedDate,
                };


                try
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    dbContext.SaveChanges();

                    return entity.DriverScheduleLogIID;
                }
                catch (Exception ex)
                {
                    var exceptionMessage = ex.Message;

                    return 0;
                }
            }
        }

        public OperationResultDTO SyncStudentPickupScheduleDatas(long vehicleID, long routeStopMapID, long routeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                OperationResultDTO message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Success,
                    Message = null
                };

                var currentDate = DateTime.Now;

                var studentSheduleDataList = dbContext.DriverScheduleLogs
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.Employee)
                    .Include(i => i.ScheduleLogStatus)
                    .Include(i => i.StopEntryStatus)
                    .Include(i => i.Routes1)
                    .Include(i => i.RouteStopMap)
                    .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                    .Where(x => x.ScheduleLogStatus.StatusNameEn == "IN" && x.StopEntryStatus.StatusTitleEn == "P" &&
                    x.RouteID == routeID && x.RouteStopMapID == routeStopMapID && x.VehicleID == vehicleID &&
                    x.StudentID.HasValue && !x.EmployeeID.HasValue &&
                    x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                    .AsNoTracking().ToList();

                foreach (var scheduleLogData in studentSheduleDataList)
                {
                    try
                    {
                        var entity = new DriverScheduleLog()
                        {
                            DriverScheduleLogIID = 0,
                            StudentID = scheduleLogData.StudentID,
                            EmployeeID = null,
                            SheduleDate = DateTime.Now.Date,
                            RouteID = scheduleLogData.RouteID,
                            RouteStopMapID = scheduleLogData.RouteStopMapID,
                            VehicleID = scheduleLogData.VehicleID,
                            SheduleLogStatusID = null,
                            StopEntryStatusID = scheduleLogData.StopEntryStatusID,
                            Status = "O",
                            CreatedBy = (int)_context.LoginID,
                            CreatedDate = DateTime.Now,
                        };

                        var sheduleData = dbContext.DriverScheduleLogs
                            .Include(i => i.Student).ThenInclude(i => i.Class)
                            .Include(i => i.Student).ThenInclude(i => i.Section)
                            .Include(i => i.Employee)
                            .Include(i => i.ScheduleLogStatus)
                            .Include(i => i.StopEntryStatus)
                            .Include(i => i.Routes1)
                            .Include(i => i.RouteStopMap)
                            .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                            .Where(x => x.SheduleLogStatusID == null && x.StopEntryStatus.StatusTitleEn == "P" &&
                            x.RouteID == routeID && x.RouteStopMapID == routeStopMapID && x.VehicleID == vehicleID &&
                            x.StudentID == scheduleLogData.StudentID &&
                            x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                            .OrderByDescending(y => y.DriverScheduleLogIID)
                            .AsNoTracking().FirstOrDefault();

                        if (sheduleData != null)
                        {
                            entity.DriverScheduleLogIID = sheduleData.DriverScheduleLogIID;
                        }
                        else
                        {
                            var notINSheduleData = dbContext.DriverScheduleLogs
                                .Include(i => i.Student).ThenInclude(i => i.Class)
                                .Include(i => i.Student).ThenInclude(i => i.Section)
                                .Include(i => i.Employee)
                                .Include(i => i.ScheduleLogStatus)
                                .Include(i => i.StopEntryStatus)
                                .Include(i => i.Routes1)
                                .Include(i => i.RouteStopMap)
                                .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                                .Where(x => x.ScheduleLogStatus.StatusNameEn != "IN" && x.StopEntryStatus.StatusTitleEn == "P" &&
                                x.RouteID == routeID && x.RouteStopMapID == routeStopMapID && x.VehicleID == vehicleID &&
                                x.StudentID == scheduleLogData.StudentID &&
                                x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                                .OrderByDescending(y => y.DriverScheduleLogIID)
                                .AsNoTracking().FirstOrDefault();

                            if (notINSheduleData != null)
                            {
                                entity.DriverScheduleLogIID = notINSheduleData.DriverScheduleLogIID;
                            }
                        }

                        if (entity.DriverScheduleLogIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        dbContext.SaveChanges();

                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Success,
                            Message = "Successfully saved"
                        };
                    }
                    catch (Exception ex)
                    {
                        var exceptionMessage = ex.Message;

                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Error,
                            Message = "Failed to save!"
                        };
                    }
                }

                return message;
            }
        }

        public OperationResultDTO SyncStaffPickupScheduleDatas(long vehicleID, long routeStopMapID, long routeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                OperationResultDTO message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Success,
                    Message = null
                };

                var currentDate = DateTime.Now;

                var staffSheduleDataList = dbContext.DriverScheduleLogs
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.Employee)
                    .Include(i => i.ScheduleLogStatus)
                    .Include(i => i.StopEntryStatus)
                    .Include(i => i.Routes1)
                    .Include(i => i.RouteStopMap)
                    .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                    .Where(x => x.ScheduleLogStatus.StatusNameEn == "IN" && x.StopEntryStatus.StatusTitleEn == "P" &&
                    x.RouteID == routeID && x.RouteStopMapID == routeStopMapID && x.VehicleID == vehicleID &&
                    x.EmployeeID.HasValue && !x.StudentID.HasValue &&
                    x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                    .AsNoTracking().ToList();

                foreach (var scheduleLogData in staffSheduleDataList)
                {
                    try
                    {
                        var entity = new DriverScheduleLog()
                        {
                            DriverScheduleLogIID = 0,
                            StudentID = null,
                            EmployeeID = scheduleLogData.EmployeeID,
                            SheduleDate = DateTime.Now.Date,
                            RouteID = scheduleLogData.RouteID,
                            RouteStopMapID = scheduleLogData.RouteStopMapID,
                            VehicleID = scheduleLogData.VehicleID,
                            SheduleLogStatusID = null,
                            StopEntryStatusID = scheduleLogData.StopEntryStatusID,
                            Status = "O",
                            CreatedBy = (int)_context.LoginID,
                            CreatedDate = DateTime.Now,
                        };

                        var sheduleData = dbContext.DriverScheduleLogs
                            .Include(i => i.Student).ThenInclude(i => i.Class)
                            .Include(i => i.Student).ThenInclude(i => i.Section)
                            .Include(i => i.Employee)
                            .Include(i => i.ScheduleLogStatus)
                            .Include(i => i.StopEntryStatus)
                            .Include(i => i.Routes1)
                            .Include(i => i.RouteStopMap)
                            .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                            .Where(x => x.SheduleLogStatusID == null && x.StopEntryStatus.StatusTitleEn == "P" &&
                            x.RouteID == routeID && x.RouteStopMapID == routeStopMapID && x.VehicleID == vehicleID &&
                            x.EmployeeID == scheduleLogData.EmployeeID &&
                            x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                            .OrderByDescending(y => y.DriverScheduleLogIID)
                            .AsNoTracking().FirstOrDefault();

                        if (sheduleData != null)
                        {
                            entity.DriverScheduleLogIID = sheduleData.DriverScheduleLogIID;
                        }
                        else
                        {
                            var notINSheduleData = dbContext.DriverScheduleLogs
                                .Include(i => i.Student).ThenInclude(i => i.Class)
                                .Include(i => i.Student).ThenInclude(i => i.Section)
                                .Include(i => i.Employee)
                                .Include(i => i.ScheduleLogStatus)
                                .Include(i => i.StopEntryStatus)
                                .Include(i => i.Routes1)
                                .Include(i => i.RouteStopMap)
                                .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                                .Where(x => x.ScheduleLogStatus.StatusNameEn != "IN" && x.StopEntryStatus.StatusTitleEn == "P" &&
                                x.RouteID == routeID && x.RouteStopMapID == routeStopMapID && x.VehicleID == vehicleID &&
                                x.EmployeeID == scheduleLogData.EmployeeID &&
                                x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                                .OrderByDescending(y => y.DriverScheduleLogIID)
                                .AsNoTracking().FirstOrDefault();

                            if (notINSheduleData != null)
                            {
                                entity.DriverScheduleLogIID = notINSheduleData.DriverScheduleLogIID;
                            }
                        }

                        if (entity.DriverScheduleLogIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        dbContext.SaveChanges();

                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Success,
                            Message = "Successfully saved"
                        };
                    }
                    catch (Exception ex)
                    {
                        var exceptionMessage = ex.Message;

                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Error,
                            Message = "Failed to save!"
                        };
                    }
                }

                return message;
            }
        }

        public List<DriverScheduleLogDTO> GetStudentPickupScheduleDatas(long vehicleID, long routeStopMapID, long routeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var sheduleLogDTOList = new List<DriverScheduleLogDTO>();
                var currentDate = DateTime.Now;

                var studentStopDtos = new StudentRouteStopMapDTO();

                var studentSheduleDataList = dbContext.DriverScheduleLogs
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.Employee)
                    .Include(i => i.ScheduleLogStatus)
                    .Include(i => i.StopEntryStatus)
                    .Include(i => i.Routes1)
                    .Include(i => i.RouteStopMap)
                    .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                    .Where(x => x.SheduleLogStatusID == null && x.Status == "O" && x.StopEntryStatus.StatusTitleEn == "P" &&
                    x.RouteID == routeID && x.RouteStopMapID == routeStopMapID && x.VehicleID == vehicleID &&
                    x.StudentID.HasValue && !x.EmployeeID.HasValue &&
                    x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                    .AsNoTracking().ToList();

                var studentSheduleOutDataList = dbContext.DriverScheduleLogs
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.Employee)
                    .Include(i => i.ScheduleLogStatus)
                    .Include(i => i.StopEntryStatus)
                    .Include(i => i.Routes1)
                    .Include(i => i.RouteStopMap)
                    .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                    .Where(x => x.SheduleLogStatusID.HasValue && x.Status == "O" && x.StopEntryStatus.StatusTitleEn == "P" &&
                    x.RouteID == routeID && x.RouteStopMapID == routeStopMapID && x.VehicleID == vehicleID &&
                    x.StudentID.HasValue && !x.EmployeeID.HasValue && x.ScheduleLogStatus.StatusNameEn != "IN" &&
                    x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                    .AsNoTracking().ToList();

                foreach (var sheduleData in studentSheduleDataList)
                {
                    studentStopDtos = new StudentRouteStopMapDTO();

                    studentStopDtos = new StudentRouteStopMapDTO()
                    {
                        StudentID = sheduleData.StudentID,
                        AdmissionNumber = sheduleData.Student?.AdmissionNumber,
                        StudentName = sheduleData.Student != null ? sheduleData.Student.FirstName + " " + sheduleData.Student.MiddleName + " " + sheduleData.Student.LastName : null,
                        Student = sheduleData.Student != null ? new KeyValueDTO() { Key = sheduleData.Student.StudentIID.ToString(), Value = sheduleData.Student.AdmissionNumber + " - " + sheduleData.Student.FirstName + " " + sheduleData.Student.MiddleName + " " + sheduleData.Student.LastName } : new KeyValueDTO(),
                        StudentProfile = sheduleData.Student?.StudentProfile,
                        IsActive = sheduleData.Student?.IsActive,
                        SchoolID = sheduleData.Student?.SchoolID,
                        ClassID = sheduleData.Student?.ClassID,
                        ClassName = sheduleData.Student != null ? sheduleData.Student.ClassID.HasValue && sheduleData.Student.Class != null ? sheduleData.Student.Class.ClassDescription : "NA" : "NA",
                        SectionID = sheduleData.Student?.SectionID,
                        SectionName = sheduleData.Student != null ? sheduleData.Student.SectionID.HasValue && sheduleData.Student.Section != null ? sheduleData.Student.Section.SectionName : "NA" : "NA",
                        AcademicYearID = sheduleData.Student?.AcademicYearID,
                        IsPickupStop = true,
                        IsDropStop = false,
                        IsStudentIn = true,
                    };

                    sheduleLogDTOList.Add(new DriverScheduleLogDTO()
                    {
                        DriverScheduleLogIID = sheduleData.DriverScheduleLogIID,
                        StudentID = sheduleData.StudentID,
                        EmployeeID = sheduleData.EmployeeID,
                        SheduleDate = sheduleData.SheduleDate,
                        RouteID = sheduleData.RouteID,
                        RouteStopMapID = sheduleData.RouteStopMapID,
                        VehicleID = sheduleData.VehicleID,
                        SheduleLogStatusID = sheduleData.SheduleLogStatusID,
                        StopEntryStatusID = sheduleData.StopEntryStatusID,
                        StudentRouteStopMap = studentStopDtos,
                        CreatedBy = sheduleData.DriverScheduleLogIID == 0 ? (int)_context.LoginID : sheduleData.CreatedBy,
                        UpdatedBy = sheduleData.DriverScheduleLogIID > 0 ? (int)_context.LoginID : sheduleData.UpdatedBy,
                        CreatedDate = sheduleData.DriverScheduleLogIID == 0 ? DateTime.Now : sheduleData.CreatedDate,
                        UpdatedDate = sheduleData.DriverScheduleLogIID > 0 ? DateTime.Now : sheduleData.UpdatedDate,
                    });
                }

                foreach (var outsheduleData in studentSheduleOutDataList)
                {
                    studentStopDtos = new StudentRouteStopMapDTO();

                    studentStopDtos = new StudentRouteStopMapDTO()
                    {
                        StudentID = outsheduleData.StudentID,
                        AdmissionNumber = outsheduleData.Student?.AdmissionNumber,
                        StudentName = outsheduleData.Student != null ? outsheduleData.Student.FirstName + " " + outsheduleData.Student.MiddleName + " " + outsheduleData.Student.LastName : null,
                        Student = outsheduleData.Student != null ? new KeyValueDTO() { Key = outsheduleData.Student.StudentIID.ToString(), Value = outsheduleData.Student.AdmissionNumber + " - " + outsheduleData.Student.FirstName + " " + outsheduleData.Student.MiddleName + " " + outsheduleData.Student.LastName } : new KeyValueDTO(),
                        StudentProfile = outsheduleData.Student?.StudentProfile,
                        IsActive = outsheduleData.Student?.IsActive,
                        SchoolID = outsheduleData.Student?.SchoolID,
                        ClassID = outsheduleData.Student?.ClassID,
                        ClassName = outsheduleData.Student != null ? outsheduleData.Student.ClassID.HasValue && outsheduleData.Student.Class != null ? outsheduleData.Student.Class.ClassDescription : "NA" : "NA",
                        SectionID = outsheduleData.Student?.SectionID,
                        SectionName = outsheduleData.Student != null ? outsheduleData.Student.SectionID.HasValue && outsheduleData.Student.Section != null ? outsheduleData.Student.Section.SectionName : "NA" : "NA",
                        AcademicYearID = outsheduleData.Student?.AcademicYearID,
                        IsPickupStop = true,
                        IsDropStop = false,
                        IsStudentIn = false,
                    };

                    sheduleLogDTOList.Add(new DriverScheduleLogDTO()
                    {
                        DriverScheduleLogIID = outsheduleData.DriverScheduleLogIID,
                        StudentID = outsheduleData.StudentID,
                        EmployeeID = outsheduleData.EmployeeID,
                        SheduleDate = outsheduleData.SheduleDate,
                        RouteID = outsheduleData.RouteID,
                        RouteStopMapID = outsheduleData.RouteStopMapID,
                        VehicleID = outsheduleData.VehicleID,
                        SheduleLogStatusID = outsheduleData.SheduleLogStatusID,
                        StopEntryStatusID = outsheduleData.StopEntryStatusID,
                        StudentRouteStopMap = studentStopDtos,
                        CreatedBy = outsheduleData.DriverScheduleLogIID == 0 ? (int)_context.LoginID : outsheduleData.CreatedBy,
                        UpdatedBy = outsheduleData.DriverScheduleLogIID > 0 ? (int)_context.LoginID : outsheduleData.UpdatedBy,
                        CreatedDate = outsheduleData.DriverScheduleLogIID == 0 ? DateTime.Now : outsheduleData.CreatedDate,
                        UpdatedDate = outsheduleData.DriverScheduleLogIID > 0 ? DateTime.Now : outsheduleData.UpdatedDate,
                    });
                }

                return sheduleLogDTOList;
            }
        }

        public List<DriverScheduleLogDTO> GetStaffPickupScheduleDatas(long vehicleID, long routeStopMapID, long routeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var sheduleLogDTOList = new List<DriverScheduleLogDTO>();
                var currentDate = DateTime.Now;

                var staffStopDtos = new StaffRouteStopMapDTO();

                var studentSheduleDataList = dbContext.DriverScheduleLogs
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.Employee)
                    .Include(i => i.ScheduleLogStatus)
                    .Include(i => i.StopEntryStatus)
                    .Include(i => i.Routes1)
                    .Include(i => i.RouteStopMap)
                    .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                    .Where(x => x.SheduleLogStatusID == null && x.Status == "O" && x.StopEntryStatus.StatusTitleEn == "P" &&
                    x.RouteID == routeID && x.RouteStopMapID == routeStopMapID && x.VehicleID == vehicleID &&
                    x.EmployeeID.HasValue && !x.StudentID.HasValue &&
                    x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                    .AsNoTracking().ToList();

                foreach (var sheduleData in studentSheduleDataList)
                {
                    staffStopDtos = new StaffRouteStopMapDTO()
                    {
                        StaffID = sheduleData.EmployeeID,
                        EmployeeCode = sheduleData.Employee?.EmployeeCode,
                        StaffName = sheduleData.Employee != null ? sheduleData.Employee.FirstName + " " + sheduleData.Employee.MiddleName + " " + sheduleData.Employee.LastName : null,
                        Staff = sheduleData.Employee != null ? new KeyValueDTO() { Key = sheduleData.Employee.EmployeeIID.ToString(), Value = sheduleData.Employee.EmployeeCode + " - " + sheduleData.Employee.FirstName + " " + sheduleData.Employee.MiddleName + " " + sheduleData.Employee.LastName } : new KeyValueDTO(),
                        StaffProfile = sheduleData.Employee?.EmployeePhoto,
                        IsActive = sheduleData.Employee?.IsActive,
                        SchoolID = sheduleData.Employee != null ? Convert.ToByte(sheduleData.Employee.BranchID) : (byte?)null,
                        IsPickupStop = true,
                        IsDropStop = false,
                        IsStaffIn = true,
                    };

                    sheduleLogDTOList.Add(new DriverScheduleLogDTO()
                    {
                        DriverScheduleLogIID = sheduleData.DriverScheduleLogIID,
                        StudentID = sheduleData.StudentID,
                        EmployeeID = sheduleData.EmployeeID,
                        SheduleDate = sheduleData.SheduleDate,
                        RouteID = sheduleData.RouteID,
                        RouteStopMapID = sheduleData.RouteStopMapID,
                        VehicleID = sheduleData.VehicleID,
                        SheduleLogStatusID = sheduleData.SheduleLogStatusID,
                        StopEntryStatusID = sheduleData.StopEntryStatusID,
                        StaffRouteStopMap = staffStopDtos,
                        CreatedBy = sheduleData.DriverScheduleLogIID == 0 ? (int)_context.LoginID : sheduleData.CreatedBy,
                        UpdatedBy = sheduleData.DriverScheduleLogIID > 0 ? (int)_context.LoginID : sheduleData.UpdatedBy,
                        CreatedDate = sheduleData.DriverScheduleLogIID == 0 ? DateTime.Now : sheduleData.CreatedDate,
                        UpdatedDate = sheduleData.DriverScheduleLogIID > 0 ? DateTime.Now : sheduleData.UpdatedDate,
                    });
                }

                return sheduleLogDTOList;
            }
        }

        public OperationResultDTO SyncStudentDropScheduleDatas(long vehicleID, long routeStopMapID, long routeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                OperationResultDTO message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Success,
                    Message = null
                };

                var currentDate = DateTime.Now;

                var studentSheduleDataList = dbContext.DriverScheduleLogs
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.Employee)
                    .Include(i => i.ScheduleLogStatus)
                    .Include(i => i.StopEntryStatus)
                    .Include(i => i.Routes1)
                    .Include(i => i.RouteStopMap)
                    .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                    .Where(x => x.ScheduleLogStatus.StatusNameEn == "IN" && x.StopEntryStatus.StatusTitleEn == "D" &&
                    x.RouteID == routeID && x.RouteStopMapID == routeStopMapID && x.VehicleID == vehicleID &&
                    x.StudentID.HasValue && !x.EmployeeID.HasValue &&
                    x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                    .AsNoTracking().ToList();

                foreach (var scheduleLogData in studentSheduleDataList)
                {
                    try
                    {
                        var entity = new DriverScheduleLog()
                        {
                            DriverScheduleLogIID = 0,
                            StudentID = scheduleLogData.StudentID,
                            EmployeeID = null,
                            SheduleDate = DateTime.Now.Date,
                            RouteID = scheduleLogData.RouteID,
                            RouteStopMapID = scheduleLogData.RouteStopMapID,
                            VehicleID = scheduleLogData.VehicleID,
                            SheduleLogStatusID = null,
                            StopEntryStatusID = scheduleLogData.StopEntryStatusID,
                            Status = "O",
                            CreatedBy = (int)_context.LoginID,
                            CreatedDate = DateTime.Now,
                        };

                        var sheduleData = dbContext.DriverScheduleLogs
                            .Include(i => i.Student).ThenInclude(i => i.Class)
                            .Include(i => i.Student).ThenInclude(i => i.Section)
                            .Include(i => i.Employee)
                            .Include(i => i.ScheduleLogStatus)
                            .Include(i => i.StopEntryStatus)
                            .Include(i => i.Routes1)
                            .Include(i => i.RouteStopMap)
                            .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                            .Where(x => x.SheduleLogStatusID == null && x.StopEntryStatus.StatusTitleEn == "D" &&
                            x.RouteID == routeID && x.RouteStopMapID == routeStopMapID && x.VehicleID == vehicleID &&
                            x.StudentID == scheduleLogData.StudentID &&
                            x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                            .OrderByDescending(y => y.DriverScheduleLogIID)
                            .AsNoTracking().FirstOrDefault();

                        if (sheduleData != null)
                        {
                            entity.DriverScheduleLogIID = sheduleData.DriverScheduleLogIID;
                        }
                        else
                        {
                            var notINSheduleData = dbContext.DriverScheduleLogs
                                .Include(i => i.Student).ThenInclude(i => i.Class)
                                .Include(i => i.Student).ThenInclude(i => i.Section)
                                .Include(i => i.Employee)
                                .Include(i => i.ScheduleLogStatus)
                                .Include(i => i.StopEntryStatus)
                                .Include(i => i.Routes1)
                                .Include(i => i.RouteStopMap)
                                .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                                .Where(x => x.ScheduleLogStatus.StatusNameEn != "IN" && x.StopEntryStatus.StatusTitleEn == "D" &&
                                x.RouteID == routeID && x.RouteStopMapID == routeStopMapID && x.VehicleID == vehicleID &&
                                x.StudentID == scheduleLogData.StudentID &&
                                x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                                .OrderByDescending(y => y.DriverScheduleLogIID)
                                .AsNoTracking().FirstOrDefault();

                            if (notINSheduleData != null)
                            {
                                entity.DriverScheduleLogIID = notINSheduleData.DriverScheduleLogIID;
                            }
                        }

                        if (entity.DriverScheduleLogIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        dbContext.SaveChanges();

                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Success,
                            Message = "Successfully saved"
                        };
                    }
                    catch (Exception ex)
                    {
                        var exceptionMessage = ex.Message;

                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Error,
                            Message = "Failed to save!"
                        };
                    }
                }

                return message;
            }
        }

        public OperationResultDTO SyncStaffDropScheduleDatas(long vehicleID, long routeStopMapID, long routeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                OperationResultDTO message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Success,
                    Message = null
                };

                var currentDate = DateTime.Now;

                var staffSheduleDataList = dbContext.DriverScheduleLogs
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.Employee)
                    .Include(i => i.ScheduleLogStatus)
                    .Include(i => i.StopEntryStatus)
                    .Include(i => i.Routes1)
                    .Include(i => i.RouteStopMap)
                    .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                    .Where(x => x.ScheduleLogStatus.StatusNameEn == "IN" && x.StopEntryStatus.StatusTitleEn == "D" &&
                    x.RouteID == routeID && x.RouteStopMapID == routeStopMapID && x.VehicleID == vehicleID &&
                    x.EmployeeID.HasValue && !x.StudentID.HasValue &&
                    x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                    .AsNoTracking().ToList();

                foreach (var scheduleLogData in staffSheduleDataList)
                {
                    try
                    {
                        var entity = new DriverScheduleLog()
                        {
                            DriverScheduleLogIID = 0,
                            StudentID = null,
                            EmployeeID = scheduleLogData.EmployeeID,
                            SheduleDate = DateTime.Now.Date,
                            RouteID = scheduleLogData.RouteID,
                            RouteStopMapID = scheduleLogData.RouteStopMapID,
                            VehicleID = scheduleLogData.VehicleID,
                            SheduleLogStatusID = null,
                            StopEntryStatusID = scheduleLogData.StopEntryStatusID,
                            Status = "O",
                            CreatedBy = (int)_context.LoginID,
                            CreatedDate = DateTime.Now,
                        };

                        var sheduleData = dbContext.DriverScheduleLogs
                            .Include(i => i.Student).ThenInclude(i => i.Class)
                            .Include(i => i.Student).ThenInclude(i => i.Section)
                            .Include(i => i.Employee)
                            .Include(i => i.ScheduleLogStatus)
                            .Include(i => i.StopEntryStatus)
                            .Include(i => i.Routes1)
                            .Include(i => i.RouteStopMap)
                            .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                            .Where(x => x.SheduleLogStatusID == null && x.StopEntryStatus.StatusTitleEn == "D" &&
                            x.RouteID == routeID && x.RouteStopMapID == routeStopMapID && x.VehicleID == vehicleID &&
                            x.EmployeeID == scheduleLogData.EmployeeID &&
                            x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                            .OrderByDescending(y => y.DriverScheduleLogIID)
                            .AsNoTracking().FirstOrDefault();

                        if (sheduleData != null)
                        {
                            entity.DriverScheduleLogIID = sheduleData.DriverScheduleLogIID;
                        }
                        else
                        {
                            var notINSheduleData = dbContext.DriverScheduleLogs
                                .Include(i => i.Student).ThenInclude(i => i.Class)
                                .Include(i => i.Student).ThenInclude(i => i.Section)
                                .Include(i => i.Employee)
                                .Include(i => i.ScheduleLogStatus)
                                .Include(i => i.StopEntryStatus)
                                .Include(i => i.Routes1)
                                .Include(i => i.RouteStopMap)
                                .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                                .Where(x => x.ScheduleLogStatus.StatusNameEn != "IN" && x.StopEntryStatus.StatusTitleEn == "D" &&
                                x.RouteID == routeID && x.RouteStopMapID == routeStopMapID && x.VehicleID == vehicleID &&
                                x.EmployeeID == scheduleLogData.EmployeeID &&
                                x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                                .OrderByDescending(y => y.DriverScheduleLogIID)
                                .AsNoTracking().FirstOrDefault();

                            if (notINSheduleData != null)
                            {
                                entity.DriverScheduleLogIID = notINSheduleData.DriverScheduleLogIID;
                            }
                        }

                        if (entity.DriverScheduleLogIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        dbContext.SaveChanges();

                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Success,
                            Message = "Successfully saved"
                        };
                    }
                    catch (Exception ex)
                    {
                        var exceptionMessage = ex.Message;

                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Error,
                            Message = "Failed to save!"
                        };
                    }
                }

                return message;
            }
        }

        public OperationResultDTO SyncStudentPickupScheduleDatasByRoute(long vehicleID, long routeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                OperationResultDTO message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Success,
                    Message = null
                };

                var currentDate = DateTime.Now;

                var studentSheduleDataList = dbContext.DriverScheduleLogs
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.Employee)
                    .Include(i => i.ScheduleLogStatus)
                    .Include(i => i.StopEntryStatus)
                    .Include(i => i.Routes1)
                    .Include(i => i.RouteStopMap)
                    .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                    .Where(x => x.ScheduleLogStatus.StatusNameEn == "IN" && x.StopEntryStatus.StatusTitleEn == "P" &&
                    x.RouteID == routeID && x.VehicleID == vehicleID &&
                    x.StudentID.HasValue && !x.EmployeeID.HasValue &&
                    x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                    .AsNoTracking().ToList();

                foreach (var scheduleLogData in studentSheduleDataList)
                {
                    try
                    {
                        var entity = new DriverScheduleLog()
                        {
                            DriverScheduleLogIID = 0,
                            StudentID = scheduleLogData.StudentID,
                            EmployeeID = null,
                            SheduleDate = DateTime.Now.Date,
                            RouteID = scheduleLogData.RouteID,
                            RouteStopMapID = scheduleLogData.RouteStopMapID,
                            VehicleID = scheduleLogData.VehicleID,
                            SheduleLogStatusID = null,
                            StopEntryStatusID = scheduleLogData.StopEntryStatusID,
                            Status = "O",
                            CreatedBy = (int)_context.LoginID,
                            CreatedDate = DateTime.Now,
                        };

                        var sheduleData = dbContext.DriverScheduleLogs
                            .Include(i => i.Student).ThenInclude(i => i.Class)
                            .Include(i => i.Student).ThenInclude(i => i.Section)
                            .Include(i => i.Employee)
                            .Include(i => i.ScheduleLogStatus)
                            .Include(i => i.StopEntryStatus)
                            .Include(i => i.Routes1)
                            .Include(i => i.RouteStopMap)
                            .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                            .Where(x => x.SheduleLogStatusID == null && x.StopEntryStatus.StatusTitleEn == "P" &&
                            x.RouteID == routeID && x.VehicleID == vehicleID &&
                            x.StudentID == scheduleLogData.StudentID &&
                            x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                            .OrderByDescending(y => y.DriverScheduleLogIID)
                            .AsNoTracking().FirstOrDefault();

                        if (sheduleData != null)
                        {
                            entity.DriverScheduleLogIID = sheduleData.DriverScheduleLogIID;
                        }
                        else
                        {
                            var notINSheduleData = dbContext.DriverScheduleLogs
                                .Include(i => i.Student).ThenInclude(i => i.Class)
                                .Include(i => i.Student).ThenInclude(i => i.Section)
                                .Include(i => i.Employee)
                                .Include(i => i.ScheduleLogStatus)
                                .Include(i => i.StopEntryStatus)
                                .Include(i => i.Routes1)
                                .Include(i => i.RouteStopMap)
                                .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                                .Where(x => x.ScheduleLogStatus.StatusNameEn != "IN" && x.StopEntryStatus.StatusTitleEn == "P" &&
                                x.RouteID == routeID && x.VehicleID == vehicleID &&
                                x.StudentID == scheduleLogData.StudentID &&
                                x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                                .OrderByDescending(y => y.DriverScheduleLogIID)
                                .AsNoTracking().FirstOrDefault();

                            if (notINSheduleData != null)
                            {
                                entity.DriverScheduleLogIID = notINSheduleData.DriverScheduleLogIID;
                            }
                        }

                        if (entity.DriverScheduleLogIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        dbContext.SaveChanges();

                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Success,
                            Message = "Successfully saved"
                        };
                    }
                    catch (Exception ex)
                    {
                        var exceptionMessage = ex.Message;

                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Error,
                            Message = "Failed to save!"
                        };
                    }
                }

                return message;
            }
        }

        public OperationResultDTO SyncStaffPickupScheduleDatasByRoute(long vehicleID, long routeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                OperationResultDTO message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Success,
                    Message = null
                };

                var currentDate = DateTime.Now;

                var staffSheduleDataList = dbContext.DriverScheduleLogs
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.Employee)
                    .Include(i => i.ScheduleLogStatus)
                    .Include(i => i.StopEntryStatus)
                    .Include(i => i.Routes1)
                    .Include(i => i.RouteStopMap)
                    .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                    .Where(x => x.ScheduleLogStatus.StatusNameEn == "IN" && x.StopEntryStatus.StatusTitleEn == "P" &&
                    x.RouteID == routeID && x.VehicleID == vehicleID &&
                    x.EmployeeID.HasValue && !x.StudentID.HasValue &&
                    x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                    .AsNoTracking().ToList();

                foreach (var scheduleLogData in staffSheduleDataList)
                {
                    try
                    {
                        var entity = new DriverScheduleLog()
                        {
                            DriverScheduleLogIID = 0,
                            StudentID = null,
                            EmployeeID = scheduleLogData.EmployeeID,
                            SheduleDate = DateTime.Now.Date,
                            RouteID = scheduleLogData.RouteID,
                            RouteStopMapID = scheduleLogData.RouteStopMapID,
                            VehicleID = scheduleLogData.VehicleID,
                            SheduleLogStatusID = null,
                            StopEntryStatusID = scheduleLogData.StopEntryStatusID,
                            Status = "O",
                            CreatedBy = (int)_context.LoginID,
                            CreatedDate = DateTime.Now,
                        };

                        var sheduleData = dbContext.DriverScheduleLogs
                            .Include(i => i.Student).ThenInclude(i => i.Class)
                            .Include(i => i.Student).ThenInclude(i => i.Section)
                            .Include(i => i.Employee)
                            .Include(i => i.ScheduleLogStatus)
                            .Include(i => i.StopEntryStatus)
                            .Include(i => i.Routes1)
                            .Include(i => i.RouteStopMap)
                            .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                            .Where(x => x.SheduleLogStatusID == null && x.StopEntryStatus.StatusTitleEn == "P" &&
                            x.RouteID == routeID && x.VehicleID == vehicleID &&
                            x.EmployeeID == scheduleLogData.EmployeeID &&
                            x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                            .OrderByDescending(y => y.DriverScheduleLogIID)
                            .AsNoTracking().FirstOrDefault();

                        if (sheduleData != null)
                        {
                            entity.DriverScheduleLogIID = sheduleData.DriverScheduleLogIID;
                        }
                        else
                        {
                            var notINSheduleData = dbContext.DriverScheduleLogs
                                .Include(i => i.Student).ThenInclude(i => i.Class)
                                .Include(i => i.Student).ThenInclude(i => i.Section)
                                .Include(i => i.Employee)
                                .Include(i => i.ScheduleLogStatus)
                                .Include(i => i.StopEntryStatus)
                                .Include(i => i.Routes1)
                                .Include(i => i.RouteStopMap)
                                .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                                .Where(x => x.ScheduleLogStatus.StatusNameEn != "IN" && x.StopEntryStatus.StatusTitleEn == "P" &&
                                x.RouteID == routeID && x.VehicleID == vehicleID &&
                                x.EmployeeID == scheduleLogData.EmployeeID &&
                                x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                                .OrderByDescending(y => y.DriverScheduleLogIID)
                                .AsNoTracking().FirstOrDefault();

                            if (notINSheduleData != null)
                            {
                                entity.DriverScheduleLogIID = notINSheduleData.DriverScheduleLogIID;
                            }
                        }

                        if (entity.DriverScheduleLogIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        dbContext.SaveChanges();

                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Success,
                            Message = "Successfully saved"
                        };
                    }
                    catch (Exception ex)
                    {
                        var exceptionMessage = ex.Message;

                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Error,
                            Message = "Failed to save!"
                        };
                    }
                }

                return message;
            }
        }

        public OperationResultDTO SyncStudentDropScheduleDatasByRoute(long vehicleID, long routeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                OperationResultDTO message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Success,
                    Message = null
                };

                var currentDate = DateTime.Now;

                var studentSheduleDataList = dbContext.DriverScheduleLogs
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.Employee)
                    .Include(i => i.ScheduleLogStatus)
                    .Include(i => i.StopEntryStatus)
                    .Include(i => i.Routes1)
                    .Include(i => i.RouteStopMap)
                    .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                    .Where(x => x.ScheduleLogStatus.StatusNameEn == "IN" && x.StopEntryStatus.StatusTitleEn == "D" &&
                    x.RouteID == routeID && x.VehicleID == vehicleID &&
                    x.StudentID.HasValue && !x.EmployeeID.HasValue &&
                    x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                    .AsNoTracking().ToList();

                foreach (var scheduleLogData in studentSheduleDataList)
                {
                    try
                    {
                        var entity = new DriverScheduleLog()
                        {
                            DriverScheduleLogIID = 0,
                            StudentID = scheduleLogData.StudentID,
                            EmployeeID = null,
                            SheduleDate = DateTime.Now.Date,
                            RouteID = scheduleLogData.RouteID,
                            RouteStopMapID = scheduleLogData.RouteStopMapID,
                            VehicleID = scheduleLogData.VehicleID,
                            SheduleLogStatusID = null,
                            StopEntryStatusID = scheduleLogData.StopEntryStatusID,
                            Status = "O",
                            CreatedBy = (int)_context.LoginID,
                            CreatedDate = DateTime.Now,
                        };

                        var sheduleData = dbContext.DriverScheduleLogs
                            .Include(i => i.Student).ThenInclude(i => i.Class)
                            .Include(i => i.Student).ThenInclude(i => i.Section)
                            .Include(i => i.Employee)
                            .Include(i => i.ScheduleLogStatus)
                            .Include(i => i.StopEntryStatus)
                            .Include(i => i.Routes1)
                            .Include(i => i.RouteStopMap)
                            .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                            .Where(x => x.SheduleLogStatusID == null && x.StopEntryStatus.StatusTitleEn == "D" &&
                            x.RouteID == routeID && x.VehicleID == vehicleID &&
                            x.StudentID == scheduleLogData.StudentID &&
                            x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                            .OrderByDescending(y => y.DriverScheduleLogIID)
                            .AsNoTracking().FirstOrDefault();

                        if (sheduleData != null)
                        {
                            entity.DriverScheduleLogIID = sheduleData.DriverScheduleLogIID;
                        }
                        else
                        {
                            var notINSheduleData = dbContext.DriverScheduleLogs
                                .Include(i => i.Student).ThenInclude(i => i.Class)
                                .Include(i => i.Student).ThenInclude(i => i.Section)
                                .Include(i => i.Employee)
                                .Include(i => i.ScheduleLogStatus)
                                .Include(i => i.StopEntryStatus)
                                .Include(i => i.Routes1)
                                .Include(i => i.RouteStopMap)
                                .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                                .Where(x => x.ScheduleLogStatus.StatusNameEn != "IN" && x.StopEntryStatus.StatusTitleEn == "D" &&
                                x.RouteID == routeID && x.VehicleID == vehicleID &&
                                x.StudentID == scheduleLogData.StudentID &&
                                x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                                .OrderByDescending(y => y.DriverScheduleLogIID)
                                .AsNoTracking().FirstOrDefault();

                            if (notINSheduleData != null)
                            {
                                entity.DriverScheduleLogIID = notINSheduleData.DriverScheduleLogIID;
                            }
                        }

                        if (entity.DriverScheduleLogIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        dbContext.SaveChanges();

                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Success,
                            Message = "Successfully saved"
                        };
                    }
                    catch (Exception ex)
                    {
                        var exceptionMessage = ex.Message;

                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Error,
                            Message = "Failed to save!"
                        };
                    }
                }

                return message;
            }
        }

        public OperationResultDTO SyncStaffDropScheduleDatasByRoute(long vehicleID, long routeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                OperationResultDTO message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Success,
                    Message = null
                };

                var currentDate = DateTime.Now;

                var staffSheduleDataList = dbContext.DriverScheduleLogs
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.Employee)
                    .Include(i => i.ScheduleLogStatus)
                    .Include(i => i.StopEntryStatus)
                    .Include(i => i.Routes1)
                    .Include(i => i.RouteStopMap)
                    .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                    .Where(x => x.ScheduleLogStatus.StatusNameEn == "IN" && x.StopEntryStatus.StatusTitleEn == "D" &&
                    x.RouteID == routeID && x.VehicleID == vehicleID &&
                    x.EmployeeID.HasValue && !x.StudentID.HasValue &&
                    x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                    .AsNoTracking().ToList();

                foreach (var scheduleLogData in staffSheduleDataList)
                {
                    try
                    {
                        var entity = new DriverScheduleLog()
                        {
                            DriverScheduleLogIID = 0,
                            StudentID = null,
                            EmployeeID = scheduleLogData.EmployeeID,
                            SheduleDate = DateTime.Now.Date,
                            RouteID = scheduleLogData.RouteID,
                            RouteStopMapID = scheduleLogData.RouteStopMapID,
                            VehicleID = scheduleLogData.VehicleID,
                            SheduleLogStatusID = null,
                            StopEntryStatusID = scheduleLogData.StopEntryStatusID,
                            Status = "O",
                            CreatedBy = (int)_context.LoginID,
                            CreatedDate = DateTime.Now,
                        };

                        var sheduleData = dbContext.DriverScheduleLogs
                            .Include(i => i.Student).ThenInclude(i => i.Class)
                            .Include(i => i.Student).ThenInclude(i => i.Section)
                            .Include(i => i.Employee)
                            .Include(i => i.ScheduleLogStatus)
                            .Include(i => i.StopEntryStatus)
                            .Include(i => i.Routes1)
                            .Include(i => i.RouteStopMap)
                            .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                            .Where(x => x.SheduleLogStatusID == null && x.StopEntryStatus.StatusTitleEn == "D" &&
                            x.RouteID == routeID && x.VehicleID == vehicleID &&
                            x.EmployeeID == scheduleLogData.EmployeeID &&
                            x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                            .OrderByDescending(y => y.DriverScheduleLogIID)
                            .AsNoTracking().FirstOrDefault();

                        if (sheduleData != null)
                        {
                            entity.DriverScheduleLogIID = sheduleData.DriverScheduleLogIID;
                        }
                        else
                        {
                            var notINSheduleData = dbContext.DriverScheduleLogs
                                .Include(i => i.Student).ThenInclude(i => i.Class)
                                .Include(i => i.Student).ThenInclude(i => i.Section)
                                .Include(i => i.Employee)
                                .Include(i => i.ScheduleLogStatus)
                                .Include(i => i.StopEntryStatus)
                                .Include(i => i.Routes1)
                                .Include(i => i.RouteStopMap)
                                .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                                .Where(x => x.ScheduleLogStatus.StatusNameEn != "IN" && x.StopEntryStatus.StatusTitleEn == "D" &&
                                x.RouteID == routeID && x.VehicleID == vehicleID &&
                                x.EmployeeID == scheduleLogData.EmployeeID &&
                                x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                                .OrderByDescending(y => y.DriverScheduleLogIID)
                                .AsNoTracking().FirstOrDefault();

                            if (notINSheduleData != null)
                            {
                                entity.DriverScheduleLogIID = notINSheduleData.DriverScheduleLogIID;
                            }
                        }

                        if (entity.DriverScheduleLogIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        dbContext.SaveChanges();

                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Success,
                            Message = "Successfully saved"
                        };
                    }
                    catch (Exception ex)
                    {
                        var exceptionMessage = ex.Message;

                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Error,
                            Message = "Failed to save!"
                        };
                    }
                }

                return message;
            }
        }


        #region Offile Code Sync 

        public List<DriverScheduleLogDTO> GetScheduleLogsByDateForOfflineDB(long? loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var currentDate = DateTime.Now;

                var pickupStopEntry = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STOP_ENTRY_STATUS_ID_PICKUP");
                var dropStopEntry = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STOP_ENTRY_STATUS_ID_DROP");
                var rejectedTransportStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSPORT_STATUS_REJECT");
                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");

                int currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);
                int stopEntryPickupID = int.Parse(pickupStopEntry);
                int stopEntryDropID = int.Parse(dropStopEntry);
                long rejectedTransportStatusID = int.Parse(rejectedTransportStatus);

                var assignEmployeesData = GetAssignEmployeesDataAsync(dbContext, loginID, currentAcademicYearStatusID);

                var vehicleIDs = assignEmployeesData.Select(x => x.VehicleID);
                var routeIds = assignEmployeesData.Select(x => x.RouteID);
                var scheduleDatas = GetDriverScheduleLogsAsync(dbContext, currentDate, vehicleIDs, routeIds);

                var scheduleLogDTOList = MapToDTO(scheduleDatas, loginID);

                return scheduleLogDTOList;
            }
        }

        private List<AssignVehicleDTO> GetAssignEmployeesDataAsync(dbEduegateSchoolContext dbContext, long? loginID, int currentAcademicYearStatusID)
        {
            var assignEmployeesData = (from AV in dbContext.AssignVehicleMaps
                                       join E in dbContext.Employees on AV.EmployeeID equals E.EmployeeIID
                                       join R in dbContext.RouteVehicleMaps on AV.RouteID equals R.RouteID
                                       join DS in dbContext.DriverScheduleLogs on AV.RouteID equals DS.RouteID
                                       where R.VehicleID == DS.VehicleID && E.LoginID == loginID
                                       && AV.IsActive == true && AV.Routes1.IsActive == true &&
                                       AV.Routes1.RouteGroup.IsActive == true &&
                                       AV.Routes1.RouteGroup.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID
                                       select new AssignVehicleDTO()
                                       {
                                           VehicleID = R.VehicleID,
                                           RouteID = AV.RouteID,
                                           LoginID = E.LoginID
                                       }).AsNoTracking().ToList();

            return assignEmployeesData;
        }

        private List<DriverScheduleLog> GetDriverScheduleLogsAsync(dbEduegateSchoolContext dbContext, DateTime currentDate, IEnumerable<long?> vehicleIDs, IEnumerable<int?> routeIds)
        {
            var scheduleDatas = dbContext.DriverScheduleLogs
                .Where(x => x.SheduleDate.Value.Date == currentDate.Date && vehicleIDs.Contains(x.VehicleID) && routeIds.Contains(x.RouteID))
                .Include(i => i.Student).ThenInclude(i => i.Class)
                .Include(i => i.Student).ThenInclude(i => i.Section)
                .Include(i => i.Employee)
                .Include(i => i.ScheduleLogStatus)
                .Include(i => i.StopEntryStatus)
                .Include(i => i.Routes1)
                .Include(i => i.RouteStopMap)
                .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                .AsNoTracking().ToList();

            return scheduleDatas;
        }

        private List<DriverScheduleLogDTO> MapToDTO(List<DriverScheduleLog> scheduleDatas, long? loginID)
        {
            var sheduleLogDTOList = new List<DriverScheduleLogDTO>();

            var det = scheduleDatas;

            foreach (var data in scheduleDatas)
            {
                var IsStaffPickIn = data.EmployeeID.HasValue ? det.Where(x => x.EmployeeID == data.EmployeeID && x.ScheduleLogType == "PICK-IN"
                                            && x.Status == "I").Count() > 0 ? true : false : false;
                var IsStaffDropIn = data.EmployeeID.HasValue ? det.Where(x => x.EmployeeID == data.EmployeeID && x.ScheduleLogType == "DROP-IN"
                                    && x.Status == "I"
                                    ).Count() > 0 ? true : false : false;
                var IsStudentIn = data.StudentID.HasValue ? det.Where(x => (x.StudentID == data.StudentID && x.ScheduleLogType == "PICK-IN"
                                   && x.Status == "I")).Count() > 0 ? true : false : false;
                var IsStudentDropIn = data.StudentID.HasValue ? det.Where(x => (x.StudentID == data.StudentID && x.ScheduleLogType == "DROP-IN"
                                   && x.Status == "I")).Count() > 0 ? true : false : false;

                sheduleLogDTOList.Add(new DriverScheduleLogDTO
                {
                    DriverScheduleLogIID = data.DriverScheduleLogIID,
                    StudentID = data.StudentID,
                    EmployeeID = data.EmployeeID,
                    SheduleDate = data.SheduleDate,
                    RouteID = data.RouteID,
                    RouteStopMapID = data.RouteStopMapID,
                    VehicleID = data.VehicleID,
                    SheduleLogStatusID = data.SheduleLogStatusID,
                    StopEntryStatusID = data.StopEntryStatusID,
                    Status = data.Status,
                    VehicleRegistrationNumber = data.VehicleID.HasValue ? data.Vehicle?.VehicleRegistrationNumber : null,
                    VehicleType = data.VehicleID.HasValue ? data.Vehicle != null ? data.Vehicle.VehicleTypeID.HasValue ? data.Vehicle?.VehicleType?.VehicleTypeName : null : null : null,
                    RouteCode = data.RouteID.HasValue ? data.Routes1?.RouteCode : null,
                    StopName = data.RouteStopMapID.HasValue ? data.RouteStopMap?.StopName : null,
                    AdmissionNumber = data.StudentID.HasValue ? data.Student?.AdmissionNumber : null,
                    StudentName = data.StudentID.HasValue ? data.Student != null ? data.Student.FirstName + " " + data.Student.MiddleName + " " + data.Student.LastName : null : null,
                    ClassName = data.StudentID.HasValue ? data.Student?.Class?.ClassDescription : null,
                    SectionName = data.StudentID.HasValue ? data.Student?.Section?.SectionName : null,
                    IsStudentIn = data.StudentID.HasValue ? data.ScheduleLogType == "PICK-IN" || data.ScheduleLogType == "PICK-OUT" ? IsStudentIn : (data.ScheduleLogType == "DROP-IN" || data.ScheduleLogType == "DROP-OUT") ? IsStudentDropIn : false : false,
                    EmployeeCode = data.EmployeeID.HasValue ? data.Employee?.EmployeeCode : null,
                    StaffName = data.EmployeeID.HasValue ? data.Employee != null ? data.Employee.FirstName + " " + data.Employee.MiddleName + " " + data.Employee.LastName : null : null,
                    IsStaffIn = data.EmployeeID.HasValue ? ((data.ScheduleLogType == "PICK-IN" || data.ScheduleLogType == "PICK-OUT") ? IsStaffPickIn : (data.ScheduleLogType == "DROP-IN" || data.ScheduleLogType == "DROP-OUT" ? IsStaffDropIn : false)) : false,
                    IsPickupStop = data.ScheduleLogType == "PICK-IN" || data.ScheduleLogType == "PICK-OUT" ? true : false,
                    IsDropStop = data.ScheduleLogType == "DROP-IN" || data.ScheduleLogType == "DROP-OUT" ? true : false,
                    LoginID = loginID,
                    ScheduleLogType = data.ScheduleLogType,
                    CreatedBy = data.DriverScheduleLogIID == 0 ? (int)_context.LoginID : data.CreatedBy,
                    UpdatedBy = data.DriverScheduleLogIID > 0 ? (int)_context.LoginID : data.UpdatedBy,
                    CreatedDate = data.DriverScheduleLogIID == 0 ? DateTime.Now : data.CreatedDate,
                    UpdatedDate = data.DriverScheduleLogIID > 0 ? DateTime.Now : data.UpdatedDate,
                });
            }

            return sheduleLogDTOList;
        }

        #endregion



        ///Driver Schedule Mobile APP using codes ....
        #region START -- DRIVER SCHEDULE CODES -- Online

        #region Main function calling first time when click driver schedular button
        public List<VehicleDTO> GetRouteStudentsAndStaffDetailsByEmployeeLoginID(long loginID)
        {
            var dtos = new List<VehicleDTO>();
            var routeDtos = new List<RoutesDTO>();
            var stopDtos = new List<RouteStopFeeDTO>();
            var studentStopDtos = new List<StudentRouteStopMapDTO>();
            var staffStopDtos = new List<StaffRouteStopMapDTO>();
            DateTime date = DateTime.Now;
            var contentList = new List<Eduegate.Domain.Entity.Contents.ContentFile>();
            var currentDate = DateTime.Now;
            try
            {
                using (var dbContext = new dbEduegateSchoolContext())
                {
                    var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                    int? currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);

                    var todayDate = DateTime.Now.Date;

                    var rejectedTransportStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSPORT_STATUS_REJECT");
                    long? rejectedTransportStatusID = int.Parse(rejectedTransportStatus);

                    var assignVehicleDetails = dbContext.AssignVehicleMaps
                        .Where(a => a.Employee.LoginID == loginID && a.IsActive == true &&
                        a.Routes1.RouteGroup.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID &&
                        a.Routes1.IsActive == true && a.Routes1.RouteGroup.IsActive == true)
                        .Include(i => i.Employee).AsNoTracking().ToList();

                    if (assignVehicleDetails.Count > 0)
                    {
                        foreach (var assign in assignVehicleDetails)
                        {
                            var vehicleMapDetails = dbContext.RouteVehicleMaps
                                .Include(i => i.Routes1)
                                .Include(i => i.Vehicle)
                                .Include(i => i.School)
                                .Include(i => i.Vehicle).ThenInclude(v => v.VehicleType)
                                .Where(v => v.RouteID == assign.RouteID && v.IsActive == true &&
                                v.Routes1.RouteGroup.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID &&
                                v.Routes1.IsActive == true && v.Routes1.RouteGroup.IsActive == true).ToList();

                            var driverScheduleLogs = dbContext.DriverScheduleLogs
                                                        .Where(x => x.SheduleDate == currentDate.Date
                                                            && x.RouteID == assign.RouteID
                                                            && (assign.VehicleID == null || x.VehicleID == assign.VehicleID))
                                                        .AsNoTracking()
                                                        .ToList();

                            if (vehicleMapDetails != null && vehicleMapDetails.Count > 0)
                            {
                                foreach (var vehicleMap in vehicleMapDetails)
                                {
                                    routeDtos = new List<RoutesDTO>();

                                    var vehicleDet = vehicleMap.Vehicle.IsActive.Value ? vehicleMap.Vehicle : null;

                                    var routeDetails = dbContext.Routes1
                                        .Where(b => b.RouteID == assign.RouteID && b.IsActive == true &&
                                        b.RouteGroup.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID &&
                                        b.RouteGroup.IsActive == true).ToList();

                                    if (routeDetails.Count > 0)
                                    {
                                        foreach (var route in routeDetails)
                                        {
                                            stopDtos = new List<RouteStopFeeDTO>();

                                            var stopDetails = dbContext.RouteStopMaps
                                                .Where(s => s.RouteID == route.RouteID && s.IsActive == true)
                                                .OrderBy(x => x.SequenceNo)
                                                .ToList();

                                            if (stopDetails.Count > 0)
                                            {
                                                foreach (var stop in stopDetails)
                                                {

                                                    #region Sync StudentData
                                                    studentStopDtos = new List<StudentRouteStopMapDTO>();

                                                    bool isStudentSynced = driverScheduleLogs.Where(s => s.StudentID.HasValue).Count() > 0 ? true : false;

                                                    var studentTransport = dbContext.StudentRouteStopMaps
                                                        .Where(p => p.IsActive == true
                                                        && p.TransportStatusID != rejectedTransportStatusID
                                                        && p.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID &&
                                                        (currentDate.Date >= p.DateFrom || currentDate.Date <= p.DateTo)
                                                        && (stop.RouteStopMapIID == p.PickupStopMapID || stop.RouteStopMapIID == p.DropStopMapID))
                                                        .Include(i => i.Student).ThenInclude(i => i.Class)
                                                        .Include(i => i.Student).ThenInclude(i => i.Section)
                                                        .AsNoTracking().ToList();

                                                    var toSyncPickData = studentTransport
                                                                                    .Where(s => !driverScheduleLogs.Any(d => d.StudentID == s.StudentID)
                                                                                    && s.PickupStopMapID == stop.RouteStopMapIID)
                                                                                    .ToList();

                                                    var toSyncDropData = studentTransport
                                                                                    .Where(s => !driverScheduleLogs.Any(d => d.StudentID == s.StudentID)
                                                                                    && s.DropStopMapID == stop.RouteStopMapIID)
                                                                                    .ToList();
                                                    //Sync/Insert StudentPickDatas
                                                    if (toSyncPickData.Count > 0)
                                                    {
                                                        isStudentSynced = SyncStudentPickupScheduleLogDatas(toSyncPickData, route, stop, vehicleDet);
                                                    }

                                                    //Sync/Insert StudentDropDatas
                                                    if (toSyncDropData.Count > 0)
                                                    {
                                                        isStudentSynced = SyncStudentDropScheduleLogDatas(toSyncDropData, route, stop, vehicleDet);
                                                    }
                                                    #endregion

                                                    #region Syn StaffData
                                                    staffStopDtos = new List<StaffRouteStopMapDTO>();

                                                    bool isStaffSynced = driverScheduleLogs.Where(e => e.EmployeeID.HasValue).Count() > 0 ? true : false;

                                                    var staffTransport = dbContext.StaffRouteStopMaps
                                                        .Where(p => p.IsActive == true && p.TransportStatusID != rejectedTransportStatusID &&
                                                        p.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID && (currentDate.Date >= p.DateFrom || currentDate.Date <= p.DateTo)
                                                        && (stop.RouteStopMapIID == p.PickupStopMapID || stop.RouteStopMapIID == p.DropStopMapID))
                                                        .Include(i => i.Employee)
                                                        .ToList();

                                                    var toSyncStaffPickData = staffTransport
                                                                                    .Where(s => !driverScheduleLogs.Any(d => d.EmployeeID == s.StaffID)
                                                                                    && s.PickupStopMapID == stop.RouteStopMapIID)
                                                                                    .ToList();

                                                    var toSyncStaffDropData = staffTransport
                                                                                    .Where(s => !driverScheduleLogs.Any(d => d.EmployeeID == s.StaffID)
                                                                                    && s.DropStopMapID == stop.RouteStopMapIID)
                                                                                    .ToList();

                                                    if (toSyncStaffPickData.Count > 0)
                                                    {
                                                        isStaffSynced = SyncStaffPickupScheduleLogDatas(toSyncStaffPickData, route, stop, vehicleDet);
                                                    }


                                                    if (toSyncStaffDropData.Count > 0)
                                                    {
                                                        isStaffSynced = SyncStaffDropScheduleLogDatas(toSyncStaffDropData, route, stop, vehicleDet);
                                                    }
                                                    #endregion

                                                    var todayPickINScheduleData = dbContext.DriverScheduleLogs.Where(s => s.ScheduleLogType == "PICK-IN" &&
                                                    (s.SheduleDate.Value.Day == todayDate.Day && s.SheduleDate.Value.Month == todayDate.Month && s.SheduleDate.Value.Year == todayDate.Year)).ToList();

                                                    var todayDropINScheduleData = dbContext.DriverScheduleLogs.Where(s => s.ScheduleLogType == "DROP-IN" &&
                                                    (s.SheduleDate.Value.Day == todayDate.Day && s.SheduleDate.Value.Month == todayDate.Month && s.SheduleDate.Value.Year == todayDate.Year)).ToList();

                                                    //Get Student Route Stop Details Start

                                                    var studentPickupStopDetails = studentTransport
                                                        .Where(p => p.PickupStopMapID == stop.RouteStopMapIID).ToList();

                                                    var studentDropStopDetails = studentTransport
                                                        .Where(d => d.DropStopMapID == stop.RouteStopMapIID).ToList();

                                                    if (isStudentSynced == true)
                                                    {
                                                        if (studentPickupStopDetails.Count > 0)
                                                        {
                                                            foreach (var pickupStop in studentPickupStopDetails)
                                                            {
                                                                var isStudentIn = false;

                                                                var scheduleFilterDet = todayPickINScheduleData.Where(s => s.StudentID == pickupStop.StudentID).FirstOrDefault();

                                                                if (scheduleFilterDet != null)
                                                                {
                                                                    if (scheduleFilterDet.Status == "I")
                                                                    {
                                                                        isStudentIn = true;
                                                                    }
                                                                    else
                                                                    {
                                                                        isStudentIn = false;
                                                                    }
                                                                }
                                                                studentStopDtos.Add(new StudentRouteStopMapDTO()
                                                                {
                                                                    StudentRouteStopMapIID = pickupStop.StudentRouteStopMapIID,
                                                                    StudentID = pickupStop.StudentID,
                                                                    AdmissionNumber = pickupStop.StudentID.HasValue ? pickupStop.Student != null ? pickupStop.Student.AdmissionNumber : null : null,
                                                                    StudentName = pickupStop.StudentID.HasValue ? pickupStop.Student != null ? pickupStop.Student.FirstName + " " + pickupStop.Student.MiddleName + " " + pickupStop.Student.LastName : null : null,
                                                                    Student = pickupStop.StudentID.HasValue ? pickupStop.Student != null ? new KeyValueDTO() { Key = pickupStop.Student.StudentIID.ToString(), Value = pickupStop.Student.AdmissionNumber + " - " + pickupStop.Student.FirstName + " " + pickupStop.Student.MiddleName + " " + pickupStop.Student.LastName } : new KeyValueDTO() : null,
                                                                    StudentProfile = pickupStop.Student?.StudentProfile != null ? GetStudentProfileNameByID(long.Parse(pickupStop.Student?.StudentProfile), pickupStop.StudentID) : null,
                                                                    IsOneWay = pickupStop.IsOneWay,
                                                                    IsActive = pickupStop.IsActive,
                                                                    SchoolID = pickupStop.SchoolID,
                                                                    ClassID = pickupStop.Student != null ? pickupStop.Student.ClassID : null,
                                                                    ClassName = pickupStop.Student != null ? pickupStop.Student.ClassID.HasValue && pickupStop.Student.Class != null ? pickupStop.Student.Class.ClassDescription : "NA" : "NA",
                                                                    SectionID = pickupStop.Student != null ? pickupStop.Student.SectionID : null,
                                                                    SectionName = pickupStop.Student != null ? pickupStop.Student.SectionID.HasValue && pickupStop.Student.Section != null ? pickupStop.Student.Section.SectionName : "NA" : "NA",
                                                                    AcademicYearID = pickupStop.AcademicYearID,
                                                                    Termsandco = pickupStop.Termsandco,
                                                                    DateFrom = pickupStop.DateFrom,
                                                                    DateTo = pickupStop.DateTo,
                                                                    IsPickupStop = true,
                                                                    IsDropStop = false,
                                                                    IsStudentIn = isStudentIn,
                                                                    DriverScheduleLogIID = scheduleFilterDet != null ? scheduleFilterDet.DriverScheduleLogIID : 0,
                                                                    ScheduleLogType = scheduleFilterDet != null ? scheduleFilterDet.ScheduleLogType : null,
                                                                    Status = scheduleFilterDet != null ? scheduleFilterDet.Status : null,
                                                                });
                                                            }
                                                        }

                                                        if (studentDropStopDetails.Count > 0)
                                                        {
                                                            foreach (var dropStop in studentDropStopDetails)
                                                            {
                                                                var isStudentIn = false;

                                                                var scheduleFilterDet = todayDropINScheduleData.Where(s => s.StudentID == dropStop.StudentID).FirstOrDefault();

                                                                if (scheduleFilterDet != null)
                                                                {
                                                                    if (scheduleFilterDet.Status == "I")
                                                                    {
                                                                        isStudentIn = true;
                                                                    }
                                                                    else
                                                                    {
                                                                        isStudentIn = false;
                                                                    }
                                                                }

                                                                studentStopDtos.Add(new StudentRouteStopMapDTO()
                                                                {
                                                                    StudentRouteStopMapIID = dropStop.StudentRouteStopMapIID,
                                                                    StudentID = dropStop.StudentID,
                                                                    AdmissionNumber = dropStop.StudentID.HasValue ? dropStop.Student != null ? dropStop.Student.AdmissionNumber : null : null,
                                                                    StudentName = dropStop.StudentID.HasValue ? dropStop.Student != null ? dropStop.Student.FirstName + " " + dropStop.Student.MiddleName + " " + dropStop.Student.LastName : null : null,
                                                                    Student = dropStop.StudentID.HasValue ? dropStop.Student != null ? new KeyValueDTO() { Key = dropStop.Student.StudentIID.ToString(), Value = dropStop.Student.AdmissionNumber + " - " + dropStop.Student.FirstName + " " + dropStop.Student.MiddleName + " " + dropStop.Student.LastName } : new KeyValueDTO() : null,
                                                                    StudentProfile = dropStop.Student?.StudentProfile != null ? GetStudentProfileNameByID(long.Parse(dropStop.Student?.StudentProfile), dropStop.StudentID) : null,
                                                                    IsOneWay = dropStop.IsOneWay,
                                                                    IsActive = dropStop.IsActive,
                                                                    SchoolID = dropStop.SchoolID,
                                                                    ClassID = dropStop.Student != null ? dropStop.Student.ClassID : null,
                                                                    ClassName = dropStop.Student != null ? dropStop.Student.ClassID.HasValue && dropStop.Student.Class != null ? dropStop.Student.Class.ClassDescription : "NA" : "NA",
                                                                    SectionID = dropStop.Student != null ? dropStop.Student.SectionID : null,
                                                                    SectionName = dropStop.Student != null ? dropStop.Student.SectionID.HasValue && dropStop.Student.Section != null ? dropStop.Student.Section.SectionName : "NA" : "NA",
                                                                    AcademicYearID = dropStop.AcademicYearID,
                                                                    Termsandco = dropStop.Termsandco,
                                                                    DateFrom = dropStop.DateFrom,
                                                                    DateTo = dropStop.DateTo,
                                                                    IsPickupStop = false,
                                                                    IsDropStop = true,
                                                                    IsStudentIn = isStudentIn,
                                                                    DriverScheduleLogIID = scheduleFilterDet != null ? scheduleFilterDet.DriverScheduleLogIID : 0,
                                                                    ScheduleLogType = scheduleFilterDet != null ? scheduleFilterDet.ScheduleLogType : null,
                                                                    Status = scheduleFilterDet != null ? scheduleFilterDet.Status : null,
                                                                });
                                                            }
                                                        }
                                                    }
                                                    //Get Student Route Stop Details End

                                                    //Get Staff Route Stop Details Start
                                                    

                                                    var staffPickupStopDetails = staffTransport
                                                        .Where(p => p.PickupStopMapID == stop.RouteStopMapIID).ToList();

                                                    var staffDropStopDetails = staffTransport
                                                        .Where(d => d.DropStopMapID == stop.RouteStopMapIID).ToList();

                                                    if (isStaffSynced == true)
                                                    {
                                                        // get STAFF only PICK-IN datas for tab == pickup > [ pick-in ]
                                                        var driverStaffScheduleDetails = todayPickINScheduleData.Where(x => x.EmployeeID != null).ToList();

                                                        if (staffPickupStopDetails.Count() > 0)
                                                        {
                                                            foreach (var pickupStop in staffPickupStopDetails)
                                                            {
                                                                var isStaffIn = false;

                                                                var scheduleFilterDet = todayPickINScheduleData.Where(s => s.EmployeeID == pickupStop.StaffID).FirstOrDefault();

                                                                if (scheduleFilterDet != null)
                                                                {
                                                                    if (scheduleFilterDet.Status == "I")
                                                                    {
                                                                        isStaffIn = true;
                                                                    }
                                                                    else
                                                                    {
                                                                        isStaffIn = false;
                                                                    }
                                                                }

                                                                staffStopDtos.Add(new StaffRouteStopMapDTO()
                                                                {
                                                                    StaffRouteStopMapIID = pickupStop.StaffRouteStopMapIID,
                                                                    StaffID = pickupStop.StaffID,
                                                                    EmployeeCode = pickupStop.StaffID.HasValue ? pickupStop.Employee != null ? pickupStop.Employee.EmployeeCode : null : null,
                                                                    StaffName = pickupStop.StaffID.HasValue ? pickupStop.Employee != null ? pickupStop.Employee.FirstName + " " + pickupStop.Employee.MiddleName + " " + pickupStop.Employee.LastName : null : null,
                                                                    Staff = pickupStop.StaffID.HasValue ? pickupStop.Employee != null ? new KeyValueDTO() { Key = pickupStop.Employee.EmployeeIID.ToString(), Value = pickupStop.Employee.EmployeeCode + " - " + pickupStop.Employee.FirstName + " " + pickupStop.Employee.MiddleName + " " + pickupStop.Employee.LastName } : new KeyValueDTO() : null,
                                                                    StaffProfile = pickupStop.StaffID.HasValue ? pickupStop.Employee?.EmployeePhoto : null,
                                                                    IsOneWay = pickupStop.IsOneWay,
                                                                    IsActive = pickupStop.IsActive,
                                                                    SchoolID = pickupStop.SchoolID,
                                                                    AcademicYearID = pickupStop.AcademicYearID,
                                                                    //Termsandco = pickupStop.Termsandco,
                                                                    DateFrom = pickupStop.DateFrom,
                                                                    DateTo = pickupStop.DateTo,
                                                                    IsPickupStop = true,
                                                                    IsDropStop = false,
                                                                    IsStaffIn = isStaffIn,
                                                                    DriverScheduleLogIID = scheduleFilterDet != null ? scheduleFilterDet.DriverScheduleLogIID : 0,
                                                                    ScheduleLogType = scheduleFilterDet != null ? scheduleFilterDet.ScheduleLogType : null,
                                                                    Status = scheduleFilterDet != null ? scheduleFilterDet.Status : null,
                                                                });
                                                            }
                                                        }

                                                        if (staffDropStopDetails.Count > 0)
                                                        {
                                                            foreach (var dropStop in staffDropStopDetails)
                                                            {
                                                                var scheduleFilterDet = todayDropINScheduleData.Where(s => s.EmployeeID == dropStop.StaffID).FirstOrDefault();

                                                                staffStopDtos.Add(new StaffRouteStopMapDTO()
                                                                {
                                                                    StaffRouteStopMapIID = dropStop.StaffRouteStopMapIID,
                                                                    StaffID = dropStop.StaffID,
                                                                    EmployeeCode = dropStop.StaffID.HasValue ? dropStop.Employee != null ? dropStop.Employee.EmployeeCode : null : null,
                                                                    StaffName = dropStop.StaffID.HasValue ? dropStop.Employee != null ? dropStop.Employee.FirstName + " " + dropStop.Employee.MiddleName + " " + dropStop.Employee.LastName : null : null,
                                                                    Staff = dropStop.StaffID.HasValue ? dropStop.Employee != null ? new KeyValueDTO() { Key = dropStop.Employee.EmployeeIID.ToString(), Value = dropStop.Employee.EmployeeCode + " - " + dropStop.Employee.FirstName + " " + dropStop.Employee.MiddleName + " " + dropStop.Employee.LastName } : new KeyValueDTO() : null,
                                                                    StaffProfile = dropStop.StaffID.HasValue ? dropStop.Employee?.EmployeePhoto : null,
                                                                    IsOneWay = dropStop.IsOneWay,
                                                                    IsActive = dropStop.IsActive,
                                                                    SchoolID = dropStop.SchoolID,
                                                                    AcademicYearID = dropStop.AcademicYearID,
                                                                    //Termsandco = dropStop.Termsandco,
                                                                    DateFrom = dropStop.DateFrom,
                                                                    DateTo = dropStop.DateTo,
                                                                    IsPickupStop = false,
                                                                    IsDropStop = true,
                                                                    IsStaffIn = scheduleFilterDet != null && scheduleFilterDet.Status == "I" ? true : false,
                                                                    DriverScheduleLogIID = scheduleFilterDet != null ? scheduleFilterDet.DriverScheduleLogIID : 0,
                                                                    ScheduleLogType = scheduleFilterDet != null ? scheduleFilterDet.ScheduleLogType : null,
                                                                    Status = scheduleFilterDet != null ? scheduleFilterDet.Status : null,
                                                                });
                                                            }
                                                        }
                                                    }
                                                    //Get Staff Route Stop Details End

                                                    stopDtos.Add(new RouteStopFeeDTO()
                                                    {
                                                        RouteStopMapIID = stop.RouteStopMapIID,
                                                        RouteID = stop.RouteID,
                                                        StopName = stop.StopName,
                                                        StopCode = stop.StopCode,
                                                        RouteFareOneWay = stop.OneWayFee,
                                                        RouteFareTwoWay = stop.TwoWayFee,
                                                        IsActive = stop.IsActive,
                                                        AcademicYearID = stop.AcademicYearID,
                                                        StopsStudentDetails = studentStopDtos,
                                                        StopsStaffDetails = staffStopDtos,
                                                    });
                                                }
                                            }

                                            routeDtos.Add(new RoutesDTO()
                                            {
                                                RouteID = route.RouteID,
                                                RouteCode = route.RouteCode,
                                                RouteDescription = route.RouteDescription,
                                                IsActive = route.IsActive,
                                                Stops = stopDtos,
                                            });
                                        }
                                    }

                                    if (vehicleDet != null)
                                    {
                                        dtos.Add(new VehicleDTO()
                                        {
                                            VehicleIID = vehicleDet.VehicleIID,
                                            VehicleCode = vehicleDet.VehicleCode,
                                            Color = vehicleDet.Color,
                                            VehicleTypeID = vehicleDet.VehicleTypeID,
                                            VehicleType = vehicleDet.VehicleTypeID.HasValue ? vehicleDet.VehicleType?.VehicleTypeName : "NA",
                                            IsActive = vehicleDet.IsActive,
                                            VehicleRegistrationNumber = vehicleDet.VehicleRegistrationNumber,
                                            RoutesDetails = routeDtos,
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
            }

            return dtos;
        }

        #endregion


        #region First time student PICK-IN , PICK-OUT Insert function
        public bool SyncStudentPickupScheduleLogDatas(List<StudentRouteStopMap> studentRouteStopMapListData, Routes1 route, RouteStopMap routeStopMap, Vehicle vehicleDet)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                bool syncCompleted = false;

                var currentDate = DateTime.Now;

                foreach (var map in studentRouteStopMapListData)
                {
                    var entity = new DriverScheduleLog()
                    {
                        DriverScheduleLogIID = 0,
                        StudentID = map.StudentID,
                        EmployeeID = null,
                        SheduleDate = DateTime.Now.Date,
                        RouteID = route.RouteID,
                        RouteStopMapID = routeStopMap.RouteStopMapIID,
                        VehicleID = vehicleDet.VehicleIID,
                        SheduleLogStatusID = null,
                        StopEntryStatusID = 1,
                        Status = "A",
                        ScheduleLogType = "PICK-IN",
                        CreatedBy = (int)_context.LoginID,
                        CreatedDate = DateTime.Now,
                    };

                    var entity1 = new DriverScheduleLog()
                    {
                        DriverScheduleLogIID = 0,
                        StudentID = map.StudentID,
                        EmployeeID = null,
                        SheduleDate = DateTime.Now.Date,
                        RouteID = route.RouteID,
                        RouteStopMapID = routeStopMap.RouteStopMapIID,
                        VehicleID = vehicleDet.VehicleIID,
                        SheduleLogStatusID = null,
                        StopEntryStatusID = 1,
                        Status = "A",
                        ScheduleLogType = "PICK-OUT",
                        CreatedBy = (int)_context.LoginID,
                        CreatedDate = DateTime.Now,
                    };

                    var sheduleData = dbContext.DriverScheduleLogs
                        .Where(x => x.StopEntryStatusID == entity.StopEntryStatusID &&
                        x.RouteID == entity.RouteID && x.RouteStopMapID == entity.RouteStopMapID && x.VehicleID == entity.VehicleID &&
                        x.StudentID == entity.StudentID &&
                        x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                        .OrderByDescending(y => y.DriverScheduleLogIID)
                        .AsNoTracking().FirstOrDefault();

                    if (sheduleData != null)
                    {
                        entity.DriverScheduleLogIID = sheduleData.DriverScheduleLogIID;
                    }

                    try
                    {
                        if (entity.DriverScheduleLogIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            dbContext.Entry(entity1).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                            dbContext.SaveChanges();
                            syncCompleted = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        var exceptionMessage = ex.Message;

                        syncCompleted = false;
                    }
                }
                return syncCompleted;
            }
        }
        #endregion

        #region First time Student DROP-IN, DROP-OUT insert function
        public bool SyncStudentDropScheduleLogDatas(List<StudentRouteStopMap> studentRouteStopMapListData, Routes1 route, RouteStopMap routeStopMap, Vehicle vehicleDet)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                bool syncCompleted = false;

                var currentDate = DateTime.Now;

                foreach (var map in studentRouteStopMapListData)
                {
                    var entity = new DriverScheduleLog()
                    {
                        DriverScheduleLogIID = 0,
                        StudentID = map.StudentID,
                        EmployeeID = null,
                        SheduleDate = DateTime.Now.Date,
                        RouteID = route.RouteID,
                        RouteStopMapID = routeStopMap.RouteStopMapIID,
                        VehicleID = vehicleDet.VehicleIID,
                        SheduleLogStatusID = null,
                        StopEntryStatusID = 2,
                        Status = "A",
                        ScheduleLogType = "DROP-IN",
                        CreatedBy = (int)_context.LoginID,
                        CreatedDate = DateTime.Now,
                    };

                    var entity2 = new DriverScheduleLog()
                    {
                        DriverScheduleLogIID = 0,
                        StudentID = map.StudentID,
                        EmployeeID = null,
                        SheduleDate = DateTime.Now.Date,
                        RouteID = route.RouteID,
                        RouteStopMapID = routeStopMap.RouteStopMapIID,
                        VehicleID = vehicleDet.VehicleIID,
                        SheduleLogStatusID = null,
                        StopEntryStatusID = 2,
                        Status = "A",
                        ScheduleLogType = "DROP-OUT",
                        CreatedBy = (int)_context.LoginID,
                        CreatedDate = DateTime.Now,
                    };

                    var sheduleData = dbContext.DriverScheduleLogs
                        .Where(x => x.StopEntryStatusID == entity.StopEntryStatusID &&
                        x.RouteID == entity.RouteID && x.RouteStopMapID == entity.RouteStopMapID && x.VehicleID == entity.VehicleID &&
                        x.StudentID == entity.StudentID &&
                        x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                        .OrderByDescending(y => y.DriverScheduleLogIID)
                        .AsNoTracking().FirstOrDefault();

                    if (sheduleData != null)
                    {
                        entity.DriverScheduleLogIID = sheduleData.DriverScheduleLogIID;
                    }

                    try
                    {
                        if (entity.DriverScheduleLogIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            dbContext.Entry(entity2).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                            dbContext.SaveChanges();
                            syncCompleted = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        var exceptionMessage = ex.Message;

                        syncCompleted = false;
                    }
                }
                return syncCompleted;
            }
        }
        #endregion


        #region First time staff PICK-IN, PICK-OUT insert function
        public bool SyncStaffPickupScheduleLogDatas(List<StaffRouteStopMap> studentRouteStopMapListData, Routes1 route, RouteStopMap routeStopMap, Vehicle vehicleDet)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                bool syncCompleted = false;

                var currentDate = DateTime.Now;

                foreach (var map in studentRouteStopMapListData)
                {
                    var entity = new DriverScheduleLog()
                    {
                        DriverScheduleLogIID = 0,
                        StudentID = null,
                        EmployeeID = map.StaffID,
                        SheduleDate = DateTime.Now.Date,
                        RouteID = route.RouteID,
                        RouteStopMapID = routeStopMap.RouteStopMapIID,
                        VehicleID = vehicleDet.VehicleIID,
                        SheduleLogStatusID = null,
                        StopEntryStatusID = 1,
                        Status = "A",
                        ScheduleLogType = "PICK-IN",
                        CreatedBy = (int)_context.LoginID,
                        CreatedDate = DateTime.Now,
                    };

                    var entity1 = new DriverScheduleLog()
                    {
                        DriverScheduleLogIID = 0,
                        StudentID = null,
                        EmployeeID = map.StaffID,
                        SheduleDate = DateTime.Now.Date,
                        RouteID = route.RouteID,
                        RouteStopMapID = routeStopMap.RouteStopMapIID,
                        VehicleID = vehicleDet.VehicleIID,
                        SheduleLogStatusID = null,
                        StopEntryStatusID = 1,
                        Status = "A",
                        ScheduleLogType = "PICK-OUT",
                        CreatedBy = (int)_context.LoginID,
                        CreatedDate = DateTime.Now,
                    };

                    var sheduleData = dbContext.DriverScheduleLogs
                        .Where(x => (x.SheduleLogStatusID == null || x.SheduleLogStatusID.HasValue) && x.StopEntryStatusID == entity.StopEntryStatusID &&
                        x.RouteID == entity.RouteID && x.RouteStopMapID == entity.RouteStopMapID && x.VehicleID == entity.VehicleID &&
                        x.EmployeeID == entity.EmployeeID &&
                        x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                        .OrderByDescending(y => y.DriverScheduleLogIID)
                        .AsNoTracking().FirstOrDefault();

                    if (sheduleData != null)
                    {
                        entity.DriverScheduleLogIID = sheduleData.DriverScheduleLogIID;
                    }

                    try
                    {
                        if (entity.DriverScheduleLogIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            dbContext.Entry(entity1).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }

                        dbContext.SaveChanges();

                        syncCompleted = true;
                    }
                    catch (Exception ex)
                    {
                        var exceptionMessage = ex.Message;

                        syncCompleted = false;
                    }
                }
                return syncCompleted;
            }
        }
        #endregion

        #region First time staff DROP-IN, DROP-OUT insert function
        public bool SyncStaffDropScheduleLogDatas(List<StaffRouteStopMap> studentRouteStopMapListData, Routes1 route, RouteStopMap routeStopMap, Vehicle vehicleDet)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                bool syncCompleted = false;

                var currentDate = DateTime.Now;

                foreach (var map in studentRouteStopMapListData)
                {
                    var entity = new DriverScheduleLog()
                    {
                        DriverScheduleLogIID = 0,
                        StudentID = null,
                        EmployeeID = map.StaffID,
                        SheduleDate = DateTime.Now.Date,
                        RouteID = route.RouteID,
                        RouteStopMapID = routeStopMap.RouteStopMapIID,
                        VehicleID = vehicleDet.VehicleIID,
                        SheduleLogStatusID = null,
                        StopEntryStatusID = 2,
                        Status = "A",
                        ScheduleLogType = "DROP-IN",
                        CreatedBy = (int)_context.LoginID,
                        CreatedDate = DateTime.Now,
                    };
                    var entity1 = new DriverScheduleLog()
                    {
                        DriverScheduleLogIID = 0,
                        StudentID = null,
                        EmployeeID = map.StaffID,
                        SheduleDate = DateTime.Now.Date,
                        RouteID = route.RouteID,
                        RouteStopMapID = routeStopMap.RouteStopMapIID,
                        VehicleID = vehicleDet.VehicleIID,
                        SheduleLogStatusID = null,
                        StopEntryStatusID = 2,
                        Status = "A",
                        ScheduleLogType = "DROP-OUT",
                        CreatedBy = (int)_context.LoginID,
                        CreatedDate = DateTime.Now,
                    };

                    var sheduleData = dbContext.DriverScheduleLogs
                        .Where(x => (x.SheduleLogStatusID == null || x.SheduleLogStatusID.HasValue) && x.StopEntryStatusID == entity.StopEntryStatusID &&
                        x.RouteID == entity.RouteID && x.RouteStopMapID == entity.RouteStopMapID && x.VehicleID == entity.VehicleID &&
                        x.EmployeeID == entity.EmployeeID &&
                        x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                        .OrderByDescending(y => y.DriverScheduleLogIID)
                        .AsNoTracking().FirstOrDefault();

                    if (sheduleData != null)
                    {
                        entity.DriverScheduleLogIID = sheduleData.DriverScheduleLogIID;
                    }

                    try
                    {
                        if (entity.DriverScheduleLogIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            dbContext.Entry(entity1).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }

                        dbContext.SaveChanges();

                        syncCompleted = true;
                    }
                    catch (Exception ex)
                    {
                        var exceptionMessage = ex.Message;

                        syncCompleted = false;
                    }
                }
                return syncCompleted;
            }
        }
        #endregion


        #region IN/OUT Status Saving function
        public string SubmitScheduleLogs(DriverScheduleLogDTO scheduleLogData)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new DriverScheduleLog()
                {
                    DriverScheduleLogIID = scheduleLogData.DriverScheduleLogIID,
                    StudentID = scheduleLogData.StudentID,
                    EmployeeID = scheduleLogData.EmployeeID,
                    SheduleDate = scheduleLogData.SheduleDate.HasValue ? scheduleLogData.SheduleDate : DateTime.Now.Date,
                    RouteID = scheduleLogData.RouteID,
                    RouteStopMapID = scheduleLogData.RouteStopMapID,
                    VehicleID = scheduleLogData.VehicleID,
                    SheduleLogStatusID = scheduleLogData.SheduleLogStatusID,
                    StopEntryStatusID = scheduleLogData.StopEntryStatusID,
                    Status = scheduleLogData.Status,
                    ScheduleLogType = scheduleLogData.ScheduleLogType,
                    CreatedBy = scheduleLogData.DriverScheduleLogIID == 0 ? (int)_context.LoginID : scheduleLogData.CreatedBy,
                    UpdatedBy = scheduleLogData.DriverScheduleLogIID > 0 ? (int)_context.LoginID : scheduleLogData.UpdatedBy,
                    CreatedDate = scheduleLogData.DriverScheduleLogIID == 0 ? DateTime.Now : scheduleLogData.CreatedDate,
                    UpdatedDate = DateTime.Now,
                };

                try
                {
                    if (entity.DriverScheduleLogIID > 0)
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    else
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }

                    dbContext.SaveChanges();

                    if (entity.StudentID.HasValue)
                    {
                        SendPushNotification(entity);
                    }

                    return entity.Status;
                }
                catch (Exception ex)
                {
                    var exceptionMessage = ex.Message;

                    return exceptionMessage;
                }
            }
        }

        public bool SendPushNotification(DriverScheduleLog entity)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var getSchedule = dbContext.DriverScheduleLogs
                    .Where(x => x.DriverScheduleLogIID == entity.DriverScheduleLogIID)
                    .Include(i => i.Student).ThenInclude(i => i.Parent)
                    .Include(i => i.Routes1)
                    .AsNoTracking().FirstOrDefault();

                if (getSchedule.ScheduleLogType == "PICK-IN" && getSchedule.Status == "I" || getSchedule.ScheduleLogType == "DROP-OUT" && getSchedule.Status == "O")
                {
                    string status = null;
                    if (getSchedule.Status == "I")
                    {
                        status = " PICKEDUP in";
                    }
                    else
                    {
                        status = " DROP OFF from";
                    }

                    var studFullName = getSchedule.Student?.AdmissionNumber + " - " + getSchedule.Student?.FirstName + " " + getSchedule.Student?.MiddleName + " " + getSchedule.Student?.LastName;
                    var settings = NotificationSetting.GetParentAppSettings();
                    var title = "Bus Attendance " + getSchedule.SheduleDate.Value.ToString("dd/MM/yyyy");
                    var message = "Your ward " + studFullName + " has been " + status + " the bus (" + getSchedule.Routes1?.RouteCode + ")";
                    long toLoginID = (long)getSchedule.Student?.Parent?.LoginID;
                    long fromLoginID = _context?.LoginID != null ? (long)_context.LoginID : 2;

                    if (toLoginID != 0)
                    {
                        PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings);
                    }
                }
            }

            return true;
        }

        #endregion


        #region Get --PICK-OUT-- and --DROP-OUT-- data Main Function 
        public List<DriverScheduleLogDTO> GetScheduleLogsByRoute(string scheduleType, string passengerType, long vehicleID, long routeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var sheduleLogDTOList = new List<DriverScheduleLogDTO>();

                //to get student + staff pickup out data 
                if (scheduleType == "IsPickup")
                {
                    sheduleLogDTOList.AddRange(GetStudentStaffPickupOutScheduleDatas(passengerType, vehicleID, routeID));
                }

                //to get student + staff drop out data
                if (scheduleType == "IsDrop")
                {
                    GetStudentStaffDropScheduleDatasforDropOut(passengerType, vehicleID, routeID);
                }

                return sheduleLogDTOList;
            }
        }



        #endregion

        #region Get Student PICK-OUT and Staff PICK-OUT datas 
        public List<DriverScheduleLogDTO> GetStudentStaffPickupOutScheduleDatas(string passengerType, long vehicleID, long routeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var sheduleLogDTOList = new List<DriverScheduleLogDTO>();
                var currentDate = DateTime.Now;

                var scheduleDatas = dbContext.DriverScheduleLogs
                                    .Include(i => i.Student).ThenInclude(i => i.Class)
                                    .Include(i => i.Student).ThenInclude(i => i.Section)
                                    .Include(i => i.Employee)
                                    .Include(i => i.ScheduleLogStatus)
                                    .Include(i => i.StopEntryStatus)
                                    .Include(i => i.Routes1)
                                    .Include(i => i.RouteStopMap)
                                    .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                                    .Where(x => x.RouteID == routeID && x.VehicleID == vehicleID &&
                                    x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                                    .AsNoTracking().ToList();

                #region Get PICK-OUT student Datas
                if (passengerType.Contains("Student"))
                {
                    var studentStopDtos = new StudentRouteStopMapDTO();

                    var studentSheduleDataListForOut = scheduleDatas
                        .Where(x => x.ScheduleLogType == "PICK-OUT" &&
                                    x.StudentID.HasValue && !x.EmployeeID.HasValue).ToList();

                    var studentSheduleAlreadIn = scheduleDatas
                        .Where(x => x.ScheduleLogType == "PICK-IN" && x.Status == "I"
                        && x.StudentID.HasValue && !x.EmployeeID.HasValue).Select(x => x.StudentID).ToList();

                    //get the student logs only in (Absent logs must exclude)
                    var studentPickedIn = studentSheduleDataListForOut.Where(x => studentSheduleAlreadIn.Contains(x.StudentID));

                    foreach (var sheduleData in studentPickedIn)
                    {
                        studentStopDtos = new StudentRouteStopMapDTO();

                        studentStopDtos = new StudentRouteStopMapDTO()
                        {
                            StudentID = sheduleData.StudentID,
                            AdmissionNumber = sheduleData.Student?.AdmissionNumber,
                            StudentName = sheduleData.Student != null ? sheduleData.Student.FirstName + " " + sheduleData.Student.MiddleName + " " + sheduleData.Student.LastName : null,
                            Student = sheduleData.Student != null ? new KeyValueDTO() { Key = sheduleData.Student.StudentIID.ToString(), Value = sheduleData.Student.AdmissionNumber + " - " + sheduleData.Student.FirstName + " " + sheduleData.Student.MiddleName + " " + sheduleData.Student.LastName } : new KeyValueDTO(),
                            StudentProfile = sheduleData.Student?.StudentProfile != null ? GetStudentProfileNameByID(long.Parse(sheduleData.Student?.StudentProfile), sheduleData.StudentID) : null,
                            IsActive = sheduleData.Student?.IsActive,
                            SchoolID = sheduleData.Student?.SchoolID,
                            ClassID = sheduleData.Student?.ClassID,
                            ClassName = sheduleData.Student != null ? sheduleData.Student.ClassID.HasValue && sheduleData.Student.Class != null ? sheduleData.Student.Class.ClassDescription : "NA" : "NA",
                            SectionID = sheduleData.Student?.SectionID,
                            SectionName = sheduleData.Student != null ? sheduleData.Student.SectionID.HasValue && sheduleData.Student.Section != null ? sheduleData.Student.Section.SectionName : "NA" : "NA",
                            AcademicYearID = sheduleData.Student?.AcademicYearID,
                            StopName = sheduleData.RouteStopMap?.StopName,
                            IsPickupStop = true,
                            IsDropStop = false,
                            IsStudentIn = sheduleData.Status == "O" ? false : true,
                            Status = sheduleData.Status,
                        };
                        sheduleLogDTOList.Add(new DriverScheduleLogDTO()
                        {
                            DriverScheduleLogIID = sheduleData.DriverScheduleLogIID,
                            StudentID = sheduleData.StudentID,
                            EmployeeID = sheduleData.EmployeeID,
                            SheduleDate = sheduleData.SheduleDate,
                            RouteID = sheduleData.RouteID,
                            RouteStopMapID = sheduleData.RouteStopMapID,
                            VehicleID = sheduleData.VehicleID,
                            SheduleLogStatusID = sheduleData.SheduleLogStatusID,
                            StopEntryStatusID = sheduleData.StopEntryStatusID,
                            StudentRouteStopMap = studentStopDtos,
                            ScheduleLogType = sheduleData.ScheduleLogType,
                            Status = sheduleData.Status,
                            CreatedBy = sheduleData.DriverScheduleLogIID == 0 ? (int)_context.LoginID : sheduleData.CreatedBy,
                            UpdatedBy = sheduleData.DriverScheduleLogIID > 0 ? (int)_context.LoginID : sheduleData.UpdatedBy,
                            CreatedDate = sheduleData.DriverScheduleLogIID == 0 ? DateTime.Now : sheduleData.CreatedDate,
                            UpdatedDate = sheduleData.DriverScheduleLogIID > 0 ? DateTime.Now : sheduleData.UpdatedDate,
                        });
                    }
                }
                #endregion

                #region Get PICK-OUT staff Datas
                if (passengerType.Contains("Staff"))
                {
                    var staffStopDtos = new StaffRouteStopMapDTO();

                    var staffShedulePickOutLogs = scheduleDatas.Where(x => x.ScheduleLogType == "PICK-OUT"
                                                    && x.EmployeeID.HasValue && !x.StudentID.HasValue).ToList();

                    var staffShedulePickInEmployees = scheduleDatas.Where(x => x.ScheduleLogType == "PICK-IN"
                                                    && x.Status == "I" && x.EmployeeID.HasValue && !x.StudentID.HasValue)
                                                        .Select(x => x.EmployeeID).ToList();

                    //get the employee logs only in (Absent logs must exclude)
                    var employeePickedIn = staffShedulePickOutLogs.Where(x => staffShedulePickInEmployees.Contains(x.EmployeeID));

                    foreach (var sheduleData in employeePickedIn)
                    {
                        staffStopDtos = new StaffRouteStopMapDTO()
                        {
                            StaffID = sheduleData.EmployeeID,
                            EmployeeCode = sheduleData.Employee?.EmployeeCode,
                            StaffName = sheduleData.Employee != null ? sheduleData.Employee.FirstName + " " + sheduleData.Employee.MiddleName + " " + sheduleData.Employee.LastName : null,
                            Staff = sheduleData.Employee != null ? new KeyValueDTO() { Key = sheduleData.Employee.EmployeeIID.ToString(), Value = sheduleData.Employee.EmployeeCode + " - " + sheduleData.Employee.FirstName + " " + sheduleData.Employee.MiddleName + " " + sheduleData.Employee.LastName } : new KeyValueDTO(),
                            StaffProfile = sheduleData.Employee?.EmployeePhoto,
                            IsActive = sheduleData.Employee?.IsActive,
                            SchoolID = sheduleData.Employee != null ? Convert.ToByte(sheduleData.Employee.BranchID) : (byte?)null,
                            StopName = sheduleData.RouteStopMap?.StopName,
                            IsPickupStop = true,
                            IsDropStop = false,
                            IsStaffIn = sheduleData.Status == "O" ? false : true,
                            Status = sheduleData.Status,
                        };

                        sheduleLogDTOList.Add(new DriverScheduleLogDTO()
                        {
                            DriverScheduleLogIID = sheduleData.DriverScheduleLogIID,
                            StudentID = sheduleData.StudentID,
                            EmployeeID = sheduleData.EmployeeID,
                            SheduleDate = sheduleData.SheduleDate,
                            RouteID = sheduleData.RouteID,
                            RouteStopMapID = sheduleData.RouteStopMapID,
                            VehicleID = sheduleData.VehicleID,
                            SheduleLogStatusID = sheduleData.SheduleLogStatusID,
                            StopEntryStatusID = sheduleData.StopEntryStatusID,
                            StaffRouteStopMap = staffStopDtos,
                            ScheduleLogType = sheduleData.ScheduleLogType,
                            Status = sheduleData.Status,
                            CreatedBy = sheduleData.DriverScheduleLogIID == 0 ? (int)_context.LoginID : sheduleData.CreatedBy,
                            UpdatedBy = sheduleData.DriverScheduleLogIID > 0 ? (int)_context.LoginID : sheduleData.UpdatedBy,
                            CreatedDate = sheduleData.DriverScheduleLogIID == 0 ? DateTime.Now : sheduleData.CreatedDate,
                            UpdatedDate = sheduleData.DriverScheduleLogIID > 0 ? DateTime.Now : sheduleData.UpdatedDate,
                        });
                    }
                }
                #endregion

                return sheduleLogDTOList;

            }

        }

        #endregion

        #region Get student DROP-OUT and staff DROP-OUT data
        //DROP-OUT DATA LISTING IN drop tab> drop-out   ----- student && staff
        public DriverScheduleLogDTO GetStudentStaffDropScheduleDatasforDropOut(string passengerType, long vehicleID, long routeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var sheduleLogDTO = new DriverScheduleLogDTO();
                var currentDate = DateTime.Now;

                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                int? currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);

                var stopDtos = new List<RouteStopFeeDTO>();

                var stopDetails = dbContext.RouteStopMaps.Where(s => s.RouteID == routeID && s.IsActive == true).OrderByDescending(x => x.SequenceNo).ToList();

                foreach (var stop in stopDetails)
                {
                    var studentStopDtos = new List<StudentRouteStopMapDTO>();
                    var staffStopDtos = new List<StaffRouteStopMapDTO>();

                    var sheduleDataLog = dbContext.DriverScheduleLogs
                                        .Include(i => i.Student).ThenInclude(i => i.Class)
                                        .Include(i => i.Student).ThenInclude(i => i.Section)
                                        .Include(i => i.Employee)
                                        .Include(i => i.ScheduleLogStatus)
                                        .Include(i => i.StopEntryStatus)
                                        .Include(i => i.Routes1)
                                        .Include(i => i.RouteStopMap)
                                        .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                                        .Where(x => x.RouteID == routeID && x.VehicleID == vehicleID && x.RouteStopMapID == stop.RouteStopMapIID &&
                                        x.SheduleDate.Value.Day == currentDate.Day && x.SheduleDate.Value.Month == currentDate.Month && x.SheduleDate.Value.Year == currentDate.Year)
                                        .AsNoTracking().ToList();

                    #region Get Staff DROP-OUT data
                    if (passengerType.Contains("Staff"))
                    {
                        var staffSheduleDataListForOut = sheduleDataLog.Where(x => x.ScheduleLogType == "DROP-OUT" && x.EmployeeID.HasValue && !x.StudentID.HasValue).ToList();

                        var staffSheduleAlreadIn = sheduleDataLog.Where(x => x.ScheduleLogType == "DROP-IN" && x.Status == "I" && x.EmployeeID.HasValue && !x.StudentID.HasValue)
                            .Select(x => x.EmployeeID).ToList();

                        //get the staff logs only in (Absent logs must exclude)
                        var staffDropOutList = staffSheduleDataListForOut.Where(x => staffSheduleAlreadIn.Contains(x.EmployeeID));

                        foreach (var sheduleData in staffDropOutList)
                        {
                            staffStopDtos.Add(new StaffRouteStopMapDTO()
                            {
                                StaffID = sheduleData.EmployeeID,
                                DriverScheduleLogIID = sheduleData.DriverScheduleLogIID,
                                EmployeeCode = sheduleData.Employee?.EmployeeCode,
                                StaffName = sheduleData.Employee != null ? sheduleData.Employee.FirstName + " " + sheduleData.Employee.MiddleName + " " + sheduleData.Employee.LastName : null,
                                Staff = sheduleData.Employee != null ? new KeyValueDTO() { Key = sheduleData.Employee.EmployeeIID.ToString(), Value = sheduleData.Employee.EmployeeCode + " - " + sheduleData.Employee.FirstName + " " + sheduleData.Employee.MiddleName + " " + sheduleData.Employee.LastName } : new KeyValueDTO(),
                                StaffProfile = sheduleData.Employee?.EmployeePhoto,
                                IsActive = sheduleData.Employee?.IsActive,
                                SchoolID = sheduleData.Employee != null ? Convert.ToByte(sheduleData.Employee.BranchID) : (byte?)null,
                                IsPickupStop = false,
                                IsDropStop = true,
                                IsStaffIn = sheduleData.Status == "O" ? false : true,
                                ScheduleLogType = sheduleData.ScheduleLogType,
                                Status = sheduleData.Status,
                            });
                        }
                    }
                    #endregion

                    #region Get student DROP-OUT data
                    if (passengerType.Contains("Student"))
                    {
                        var studentSheduleDataListForOut = sheduleDataLog.Where(x => x.ScheduleLogType == "DROP-OUT"
                        && x.StudentID.HasValue && !x.EmployeeID.HasValue).ToList();

                        var studentSheduleAlreadIn = sheduleDataLog.Where(x => x.ScheduleLogType == "DROP-IN" && x.Status == "I" &&
                            x.StudentID.HasValue && !x.EmployeeID.HasValue).Select(x => x.StudentID)
                            .ToList();

                        //get the student logs only in (Absent logs must exclude)
                        var studentDropOutList = studentSheduleDataListForOut.Where(x => studentSheduleAlreadIn.Contains(x.StudentID));

                        foreach (var sheduleData in studentDropOutList)
                        {
                            studentStopDtos.Add(new StudentRouteStopMapDTO()
                            {
                                StudentID = sheduleData.StudentID,
                                DriverScheduleLogIID = sheduleData.DriverScheduleLogIID,
                                AdmissionNumber = sheduleData.Student?.AdmissionNumber,
                                StudentName = sheduleData.Student != null ? sheduleData.Student.FirstName + " " + sheduleData.Student.MiddleName + " " + sheduleData.Student.LastName : null,
                                Student = sheduleData.Student != null ? new KeyValueDTO() { Key = sheduleData.Student.StudentIID.ToString(), Value = sheduleData.Student.AdmissionNumber + " - " + sheduleData.Student.FirstName + " " + sheduleData.Student.MiddleName + " " + sheduleData.Student.LastName } : new KeyValueDTO(),
                                StudentProfile = sheduleData.Student?.StudentProfile != null ? GetStudentProfileNameByID(long.Parse(sheduleData.Student?.StudentProfile), sheduleData.StudentID) : null,
                                IsActive = sheduleData.Student?.IsActive,
                                SchoolID = sheduleData.Student?.SchoolID,
                                ClassID = sheduleData.Student?.ClassID,
                                ClassName = sheduleData.Student != null ? sheduleData.Student.ClassID.HasValue && sheduleData.Student.Class != null ? sheduleData.Student.Class.ClassDescription : "NA" : "NA",
                                SectionID = sheduleData.Student?.SectionID,
                                SectionName = sheduleData.Student != null ? sheduleData.Student.SectionID.HasValue && sheduleData.Student.Section != null ? sheduleData.Student.Section.SectionName : "NA" : "NA",
                                AcademicYearID = sheduleData.Student?.AcademicYearID,
                                IsPickupStop = false,
                                IsDropStop = true,
                                IsStudentIn = sheduleData.Status == "O" ? false : true,
                                ScheduleLogType = sheduleData.ScheduleLogType,
                                Status = sheduleData.Status,
                            });
                        }
                    }
                    #endregion

                    stopDtos.Add(new RouteStopFeeDTO()
                    {
                        RouteStopMapIID = stop.RouteStopMapIID,
                        RouteID = stop.RouteID,
                        StopName = stop.StopName,
                        StopCode = stop.StopCode,
                        RouteFareOneWay = stop.OneWayFee,
                        RouteFareTwoWay = stop.TwoWayFee,
                        IsActive = stop.IsActive,
                        AcademicYearID = stop.AcademicYearID,
                        StopsStudentDetails = studentStopDtos,
                        StopsStaffDetails = staffStopDtos,
                    });

                    sheduleLogDTO = new DriverScheduleLogDTO()
                    {
                        Stops = stopDtos,
                    };
                }

                return sheduleLogDTO;
            }
        }
        #endregion


        public string GetStudentProfileNameByID(long? studentProfileID, long? studentID)
        {
            using (dbContentContext dbContext = new dbContentContext()) { 

                var studentProfileUrl = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_PROFILE_URL");

                var contentFileName = dbContext.ContentFiles
                    .AsNoTracking()
                    .Where(x => x.ContentFileIID == studentProfileID)
                    .Select(x => x.ContentFileName)
                    .FirstOrDefault();

                var studentProfileName = studentProfileUrl + studentID + "/Thumbnail/" + contentFileName;

                return studentProfileName;
            }
        }


        #endregion END

       

        #region ERP-DriverShedule
        public string UpdateScheduleLogStatus(ScheduleLogDTO dto)  
        {
            var returnMsg = "";

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if(dto.IIDs.Count > 0)
                {
                    try
                    {

                        // Parse keys outside of the LINQ query
                        var parsedKeys = dto.IIDs.Select(k => long.Parse(k.Key)).ToList();

                        // Filter DriverScheduleLogs based on DriverScheduleLogIID in the IIDs list
                        var existingLogs = dbContext.DriverScheduleLogs
                            .AsNoTracking()
                            .Where(x => parsedKeys.Contains(x.DriverScheduleLogIID))
                            .ToList();                    

                        if (existingLogs.Count > 0)
                        {
                            existingLogs.ForEach(x =>
                            {
                                if (dto.IsRowUpdation)
                                {
                                    if( x.Status == "A" && (x.ScheduleLogType == "PICK-IN" || x.ScheduleLogType == "DROP-IN"))
                                    {
                                        x.Status = "I";
                                    }
                                    else if(x.Status == "A" && (x.ScheduleLogType == "PICK-OUT" || x.ScheduleLogType == "DROP-OUT"))
                                    {
                                        x.Status = "O";
                                    }
                                    else
                                    {
                                        x.Status = "A";
                                    }
                                }
                                else
                                {
                                    x.Status = dto.Status;
                                }
                                x.UpdatedBy = (int?)_context.LoginID;
                                x.UpdatedDate = DateTime.Now;
                            });

                            dbContext.DriverScheduleLogs.UpdateRange(existingLogs);
                            // Save changes to the database
                            dbContext.SaveChanges();
                        }

                        returnMsg = "Sucessfully Updated";
                    }
                    catch (Exception ex)
                    {
                        var exceptionMessage = ex.Message;

                        var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;

                        Eduegate.Logger.LogHelper<string>.Fatal($"Transport Module Discrepancy Tracker Status change failed. Error message: {errorMessage}", ex);

                        returnMsg = exceptionMessage;

                    }
                }

                return returnMsg;
            }
        }

        #endregion
    }
}