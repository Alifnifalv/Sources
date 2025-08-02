using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Models.Mutual;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Schedulers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.Mutual
{
    public class GeoLocationLogMapper : DTOEntityDynamicMapper
    {
        public static GeoLocationLogMapper Mapper(CallContext context)
        {
            var mapper = new GeoLocationLogMapper();
            mapper._context = context;
            return mapper;
        }

        public List<GeoLocationLogDTO> GetGeoLocationBySchoolID(long schoolID)
        {
            var dtos = new List<GeoLocationLogDTO>();

            using (var dbContext = new dbEduegateERPContext())
            {
                var logs = dbContext.GeoLocationLogs.Where(x => x.GeoLocationLogIID == schoolID)
                    .AsNoTracking()
                    .ToList();

                foreach (var log in logs)
                {
                    dtos.Add(ToDTO(log));
                }
            }

            return dtos;
        }

        public List<GeoLocationLogDTO> GetGeoLocationByParentLoginID(long loginID)
        {
            var dtos = new List<GeoLocationLogDTO>();

            using (var dbContext = new dbEduegateERPContext())
            {
                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                int? currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);

                var parentDet = dbContext.Parents.AsNoTracking().FirstOrDefault(x => x.LoginID == loginID);

                //var studentDet = parentDet != null ? dbContext.Students.Where(y => y.ParentID == parentDet.ParentIID) : null;

                //var studentList = parentDet != null ? dbContext.Students.Where(y => y.ParentID == parentDet.ParentIID && y.IsActive == true && y.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID).AsNoTracking().ToList() : null;

                var studentDet = parentDet != null ? dbContext.Students.AsNoTracking().FirstOrDefault(y => y.ParentID == parentDet.ParentIID && y.IsActive == true && y.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID) : null;

                if (studentDet != null)
                {
                    //var transportDet = dbContext.StudentRouteStopMaps.FirstOrDefault(a => studentList.Any(b => b.StudentIID == a.StudentID && a.IsActive == true) && a.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID);
                    var transportDet = dbContext.StudentRouteStopMaps.Where(a => a.StudentID == studentDet.StudentIID && a.IsActive == true && a.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                        .Include(i => i.Routes1)
                        .Include(i => i.Routes11)
                        .AsNoTracking()
                        .OrderByDescending(x => x.StudentRouteStopMapIID)
                        .FirstOrDefault();

                    if (transportDet != null)
                    {
                        var route = transportDet.Routes11 != null ? dbContext.Routes1.AsNoTracking().FirstOrDefault(b => b.RouteID == transportDet.Routes11.RouteID && b.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID) : transportDet.Routes1 != null ? dbContext.Routes1.FirstOrDefault(b => b.RouteID == transportDet.Routes1.RouteID) : null;

                        //var vehicleMap = route != null ? dbContext.RouteVehicleMaps.FirstOrDefault(c => c.RouteID == route.RouteID) : null;

                        //var assign = vehicleMap != null ? dbContext.AssignVehicleMaps.FirstOrDefault(d => d.VehicleID == vehicleMap.VehicleID) : null;
                        var assign = route != null ? dbContext.AssignVehicleMaps.AsNoTracking().FirstOrDefault(d => d.RouteID == route.RouteID && d.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID) : null;

                        var employeeDet = assign != null ? dbContext.Employees.AsNoTracking().FirstOrDefault(e => e.EmployeeIID == assign.EmployeeID) : null;

                        var logs = employeeDet != null ? dbContext.GeoLocationLogs.Where(x => x.ReferenceID2 == employeeDet.EmployeeIID.ToString())
                            .OrderByDescending(x => x.GeoLocationLogIID)
                            .AsNoTracking()
                            .FirstOrDefault() : null;

                        if (logs != null)
                        {
                            //foreach (var log in logs)
                            //{
                            //    dtos.Add(ToDTO(log));
                            //}
                            dtos.Add(ToDTO(logs));
                        }
                    }
                }
            }

            return dtos;
        }
       
        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var entitydto = dbContext.GeoLocationLogs.Where(x => x.GeoLocationLogIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTOString(ToDTO(entitydto));
            }
        }

        public GeoLocationLogDTO ToDTO(GeoLocationLog entity)
        {
            return new GeoLocationLogDTO()
            {
                GeoLocationLogIID = entity.GeoLocationLogIID,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                ReferenceID1 = entity.ReferenceID1,
                ReferenceID2 = entity.ReferenceID2,
                ReferenceID3 = entity.ReferenceID3,
                Type = entity.Type,
                CreatedBy = entity.GeoLocationLogIID == 0 ? (int)_context.LoginID : entity.CreatedBy,
                UpdatedBy = entity.GeoLocationLogIID > 0 ? (int)_context.LoginID : entity.UpdatedBy,
                CreatedDate = entity.GeoLocationLogIID == 0 ? DateTime.Now : entity.CreatedDate,
                UpdatedDate = entity.GeoLocationLogIID > 0 ? DateTime.Now : entity.UpdatedDate,
            };
        }

        public GeoLocationLog ToEntity(GeoLocationLogDTO dto)
        {
            var log = new GeoLocationLog()
            {
                GeoLocationLogIID = dto.GeoLocationLogIID,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                ReferenceID1 = dto.ReferenceID1,
                ReferenceID2 = dto.ReferenceID2,
                ReferenceID3 = dto.ReferenceID3,
                Type = dto.Type,
                CreatedBy = dto.GeoLocationLogIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = dto.GeoLocationLogIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = dto.GeoLocationLogIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = dto.GeoLocationLogIID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            if (!dto.CreatedBy.HasValue)
            {
                log.CreatedBy = _context.LoginID != null ? int.Parse(_context.LoginID.ToString()) : 0;
                log.CreatedDate = DateTime.Now;
            }

            return log;
        }

        public void UpdateUserGeoLocation(string geoLocation, bool? isDriver)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                if (string.IsNullOrEmpty(geoLocation)) return;

                if (_context == null || !_context.LoginID.HasValue) return;

                var location = geoLocation.Split(',');

                if (location.Length == 0) return;

                var mutualRepository = new MutualRepository();

                GeoLocationLog geoLocationDeails = new GeoLocationLog();

                if (isDriver == true)
                {
                    geoLocationDeails.Type = "driverlocation";
                }
                else
                {
                    geoLocationDeails.Type = "stafflocation";
                }

                geoLocationDeails.ReferenceID1 = _context.LoginID.Value.ToString();
                geoLocationDeails.ReferenceID2 = _context.EmployeeID.HasValue ? _context.EmployeeID.Value.ToString() : null;
                geoLocationDeails.ReferenceID3 = _context.CustomerID.HasValue ? _context.CustomerID.Value.ToString() : null;
                geoLocationDeails.Longitude = location.Length > 0 ? location[1] : null;
                geoLocationDeails.Latitude = location.Length > 1 ? location[0] : null;
                geoLocationDeails.CreatedDate = DateTime.Now;

                var geoLocationLastData = dbContext.GeoLocationLogs.Where(x => x.ReferenceID1 == geoLocationDeails.ReferenceID1)
                    .OrderByDescending(x => x.GeoLocationLogIID)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.ReferenceID1 == geoLocationDeails.ReferenceID1);

                if(geoLocationLastData != null)
                {
                    if (geoLocationLastData.Latitude != geoLocationDeails.Latitude && geoLocationLastData.Longitude != geoLocationDeails.Longitude)
                    {
                        mutualRepository.SaveGeoLocation(geoLocationDeails);
                    }
                }
                else
                {
                    mutualRepository.SaveGeoLocation(geoLocationDeails);
                }
                
            }
        }

        public GeoLocationLog GetLastGeoLocationByLoginID(long loginID)
        {
            var geoLocationDeails = new GeoLocationLog();
            string referenceID1 = loginID.ToString();

            using (var dbContext = new dbEduegateERPContext())
            {
                var geoLocationLastData = dbContext.GeoLocationLogs.Where(x => x.ReferenceID1 == referenceID1)
                    .OrderByDescending(x => x.GeoLocationLogIID)
                    .AsNoTracking()
                    .FirstOrDefault();

                if(geoLocationLastData != null)
                {
                    geoLocationDeails.ReferenceID1 = geoLocationLastData.ReferenceID1;
                    geoLocationDeails.ReferenceID2 = geoLocationLastData.ReferenceID2;
                    geoLocationDeails.ReferenceID3 = geoLocationLastData.ReferenceID3;
                    geoLocationDeails.Longitude = geoLocationLastData.Longitude;
                    geoLocationDeails.Latitude = geoLocationLastData.Latitude;
                }
            }
            return geoLocationDeails;
        }

        public List<GeoLocationLogDTO> GetDriverGeoLocationByStudent(long studentID)
        {
            var dtos = new List<GeoLocationLogDTO>();

            using (var dbContext = new dbEduegateERPContext())
            {
                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                int? currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);

                //var parentDet = dbContext.Parents.AsNoTracking().FirstOrDefault(x => x.LoginID == loginID);

                //var studentDet = parentDet != null ? dbContext.Students.Where(y => y.ParentID == parentDet.ParentIID) : null;

                //var studentList = parentDet != null ? dbContext.Students.Where(y => y.ParentID == parentDet.ParentIID && y.IsActive == true && y.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID).AsNoTracking().ToList() : null;

                var studentDet = dbContext.Students.AsNoTracking().FirstOrDefault(y => y.StudentIID == studentID && y.IsActive == true && y.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID);

                if (studentDet != null)
                {
                    //var transportDet = dbContext.StudentRouteStopMaps.FirstOrDefault(a => studentList.Any(b => b.StudentIID == a.StudentID && a.IsActive == true) && a.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID);
                    var transportDet = dbContext.StudentRouteStopMaps.Where(a => a.StudentID == studentDet.StudentIID && a.IsActive == true && a.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                        .Include(i => i.Routes1)
                        .Include(i => i.Routes11)
                        .AsNoTracking()
                        .OrderByDescending(x => x.StudentRouteStopMapIID)
                        .FirstOrDefault();

                    if (transportDet != null)
                    {
                        var route = transportDet.Routes11 != null ? dbContext.Routes1.AsNoTracking().FirstOrDefault(b => b.RouteID == transportDet.Routes11.RouteID && b.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID) : transportDet.Routes1 != null ? dbContext.Routes1.FirstOrDefault(b => b.RouteID == transportDet.Routes1.RouteID) : null;

                        //var vehicleMap = route != null ? dbContext.RouteVehicleMaps.FirstOrDefault(c => c.RouteID == route.RouteID) : null;

                        //var assign = vehicleMap != null ? dbContext.AssignVehicleMaps.FirstOrDefault(d => d.VehicleID == vehicleMap.VehicleID) : null;
                        var assign = route != null ? dbContext.AssignVehicleMaps.AsNoTracking().FirstOrDefault(d => d.RouteID == route.RouteID && d.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID) : null;

                        var employeeDet = assign != null ? dbContext.Employees.AsNoTracking().FirstOrDefault(e => e.EmployeeIID == assign.EmployeeID) : null;

                        var logs = employeeDet != null ? dbContext.GeoLocationLogs.Where(x => x.ReferenceID2 == employeeDet.EmployeeIID.ToString())
                            .OrderByDescending(x => x.GeoLocationLogIID)
                            .AsNoTracking()
                            .FirstOrDefault() : null;

                        if (logs != null)
                        {
                            //foreach (var log in logs)
                            //{
                            //    dtos.Add(ToDTO(log));
                            //}
                            dtos.Add(ToDTO(logs));
                        }
                    }
                }
            }

            return dtos;
        }


        public List<GeoLocationLogDTO> GetDriverGeoLocationLogByStudent(long studentID)
        {
            var dtos = new List<GeoLocationLogDTO>();

            using (var dbContext = new dbEduegateERPContext())
            {
                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                int? currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);

                //var parentDet = dbContext.Parents.AsNoTracking().FirstOrDefault(x => x.LoginID == loginID);

                //var studentDet = parentDet != null ? dbContext.Students.Where(y => y.ParentID == parentDet.ParentIID) : null;

                //var studentList = parentDet != null ? dbContext.Students.Where(y => y.ParentID == parentDet.ParentIID && y.IsActive == true && y.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID).AsNoTracking().ToList() : null;

                var studentDet = dbContext.Students.AsNoTracking().FirstOrDefault(y => y.StudentIID == studentID && y.IsActive == true && y.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID);

                if (studentDet != null)
                {
                    //var transportDet = dbContext.StudentRouteStopMaps.FirstOrDefault(a => studentList.Any(b => b.StudentIID == a.StudentID && a.IsActive == true) && a.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID);
                    var transportDet = dbContext.StudentRouteStopMaps.Where(a => a.StudentID == studentDet.StudentIID && a.IsActive == true && a.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                        .Include(i => i.Routes1)
                        .Include(i => i.Routes11)
                        .AsNoTracking()
                        .OrderByDescending(x => x.StudentRouteStopMapIID)
                        .FirstOrDefault();

                    if (transportDet != null)
                    {
                        var route = transportDet.Routes11 != null ? dbContext.Routes1.AsNoTracking().FirstOrDefault(b => b.RouteID == transportDet.Routes11.RouteID && b.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID) : transportDet.Routes1 != null ? dbContext.Routes1.FirstOrDefault(b => b.RouteID == transportDet.Routes1.RouteID) : null;

                        //var vehicleMap = route != null ? dbContext.RouteVehicleMaps.FirstOrDefault(c => c.RouteID == route.RouteID) : null;

                        //var assign = vehicleMap != null ? dbContext.AssignVehicleMaps.FirstOrDefault(d => d.VehicleID == vehicleMap.VehicleID) : null;
                        var assign = route != null ? dbContext.AssignVehicleMaps.AsNoTracking().FirstOrDefault(d => d.RouteID == route.RouteID && d.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID) : null;

                        var employeeDet = assign != null ? dbContext.Employees.AsNoTracking().FirstOrDefault(e => e.EmployeeIID == assign.EmployeeID) : null;

                        var logs = employeeDet != null ? dbContext.GeoLocationLogs.Where(x => x.ReferenceID2 == employeeDet.EmployeeIID.ToString())
                            .OrderByDescending(x => x.GeoLocationLogIID)
                            .AsNoTracking()
                            .ToList() : null;

                        if (logs != null)
                        {
                            foreach (var log in logs)
                            {
                                dtos.Add(ToDTO(log));
                            }
                        }
                    }
                }
            }

            return dtos;
        }
        public List<GeoLocationLogDTO> GetDriverGeoLocationByStaff(long employeeId)
        {
            var geoLocationLogDtos = new List<GeoLocationLogDTO>();

            using (var dbContext = new dbEduegateERPContext())
            using (var schoolContext = new dbEduegateSchoolContext())
            {
                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                int? currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);

                // Retrieve the employee details and ensure the employee is active
                var employee = dbContext.Employees.AsNoTracking()
                    .FirstOrDefault(e => e.EmployeeIID == employeeId && e.IsActive == true);

                if (employee != null)
                {
                    // Fetch the staff route stop map details and related routes
                    var staffRouteStopMap = schoolContext.StaffRouteStopMaps.Where(a => a.StaffID == employee.EmployeeIID && a.IsActive == true && a.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                        .Include(i => i.Routes1)
                        .Include(i => i.Routes11)
                        .AsNoTracking()
                        .OrderByDescending(x => x.StaffRouteStopMapIID)
                        .FirstOrDefault();

                    if (staffRouteStopMap != null)
                    {
                        // Determine the route based on which one is not null
                        Eduegate.Domain.Entity.Models.Routes1 route1 = null;
                        Eduegate.Domain.Entity.School.Models.Routes1 route11 = null;

                        if (staffRouteStopMap.Routes11 != null)
                        {
                            route11 = staffRouteStopMap.Routes11;
                            route1 = dbContext.Routes1.AsNoTracking()
                                .FirstOrDefault(b => b.RouteID == route11.RouteID && b.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID);
                        }
                        else
                        {
                            //route1 = staffRouteStopMap;
                        }

                        // Continue if we have a valid route
                        if (route1 != null)
                        {
                            var assign = dbContext.AssignVehicleMaps.AsNoTracking()
                                .FirstOrDefault(d => d.RouteID == route1.RouteID && d.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID);

                            var assignedEmployee = assign != null ? dbContext.Employees.AsNoTracking()
                                .FirstOrDefault(e => e.EmployeeIID == assign.EmployeeID) : null;

                            var logs = assignedEmployee != null ? dbContext.GeoLocationLogs.Where(x => x.ReferenceID2 == assignedEmployee.EmployeeIID.ToString())
                                .OrderByDescending(x => x.GeoLocationLogIID)
                                .AsNoTracking()
                                .FirstOrDefault() : null;

                            if (logs != null)
                            {
                                geoLocationLogDtos.Add(ToDTO(logs));
                            }
                        }
                    }
                }
            }

            return geoLocationLogDtos;
        }



    }
}