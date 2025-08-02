using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Employee;
using Eduegate.Services.Contracts.School.Transports;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace Eduegate.Domain.Mappers.School.Transports
{
    public class AssignVehicleMapper : DTOEntityDynamicMapper
    {
        public static AssignVehicleMapper Mapper(CallContext context)
        {
            var mapper = new AssignVehicleMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AssignVehicleDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private AssignVehicleDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.AssignVehicleMaps.Where(x => x.AssignVehicleMapIID == IID)
                    .Include(i => i.Employee)
                    .Include(i => i.Vehicle)
                    .Include(i => i.Routes1)
                    .Include(i => i.AssignVehicleAttendantMaps).ThenInclude(i => i.Employee)
                    .AsNoTracking()
                    .FirstOrDefault();

                List<KeyValueDTO> mapDto = new List<KeyValueDTO>();

                foreach (var stp in entity.AssignVehicleAttendantMaps)
                {
                    mapDto.Add(new KeyValueDTO()
                    {
                        Key = stp.EmployeeID.ToString(),
                        Value = stp.Employee != null ? stp.Employee.EmployeeCode + " - " + stp.Employee.FirstName + " " + stp.Employee.MiddleName + " " + stp.Employee.LastName : GetEmployeeNameByID(stp.EmployeeID)
                    });
                }

                return new AssignVehicleDTO()
                {
                    AssignVehicleMapIID = entity.AssignVehicleMapIID,
                    DateFrom = entity.DateFrom,
                    DateTo = entity.DateTo,
                    EmployeeID = entity.EmployeeID,
                    DriverName = entity.Employee?.EmployeeCode + " - " + entity.Employee?.FirstName + " " + entity.Employee?.MiddleName + " " + entity.Employee?.LastName,
                    VehicleID = entity.VehicleID,
                    Vehicle = entity.VehicleID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.VehicleID.ToString(),
                        Value = entity.Vehicle?.VehicleRegistrationNumber
                    } : new KeyValueDTO(),
                    RouteID = entity.RouteID,
                    Routes = entity.RouteID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.RouteID?.ToString(),
                        Value = entity.Routes1?.RouteCode + " - " + entity.Routes1?.RouteDescription
                    } : new KeyValueDTO(),
                    RouteGroupID = entity.RouteID.HasValue ? entity.Routes1?.RouteGroupID : null,
                    Notes = entity.Notes,
                    IsActive = entity.IsActive,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    Attendanter = mapDto,
                    CreatedBy = entity.AssignVehicleMapIID == 0 ? (int)_context.LoginID : entity.CreatedBy,
                    UpdatedBy = entity.AssignVehicleMapIID > 0 ? (int)_context.LoginID : entity.UpdatedBy,
                    CreatedDate = entity.AssignVehicleMapIID == 0 ? DateTime.Now : entity.CreatedDate,
                    UpdatedDate = entity.AssignVehicleMapIID > 0 ? DateTime.Now : entity.UpdatedDate,
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AssignVehicleDTO;

            // Date Different Check
            if (toDto.DateFrom >= toDto.DateTo)
            {
                throw new Exception("Select Date Properlly!!");
            }

            if (toDto.EmployeeID == null || toDto.EmployeeID == 0)
            {
                throw new Exception("Please Select Employee");
            }

            //if (toDto.Attendanter.Count == 0)
            //{
            //    throw new Exception("Please Select Attendanter");
            //}

            if (toDto.RouteID == null || toDto.RouteID == 0)
            {
                throw new Exception("Please Select Route");
            }

            if (toDto.VehicleID == null || toDto.VehicleID == 0)
            {
                throw new Exception("Please Select Vehicle");
            }

            //convert the dto to entity and pass to the repository.
            var entity = new AssignVehicleMap()
            {
                AssignVehicleMapIID = toDto.AssignVehicleMapIID,
                DateFrom = toDto.DateFrom,
                DateTo = toDto.DateTo,
                EmployeeID = toDto.EmployeeID,
                VehicleID = toDto.VehicleID,
                RouteID = toDto.RouteID,
                Notes = toDto.Notes,
                IsActive = toDto.IsActive,
                SchoolID = toDto.SchoolID.HasValue ? toDto.SchoolID : (byte)_context.SchoolID,
                AcademicYearID = GetAcademicYearIDFromRouteGroup(toDto.RouteGroupID),
                CreatedBy = toDto.AssignVehicleMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.AssignVehicleMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.AssignVehicleMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.AssignVehicleMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (toDto.Attendanter.Count > 0)
                {
                    foreach (var keyval in toDto.Attendanter)
                    {
                        entity.AssignVehicleAttendantMaps.Add(new AssignVehicleAttendantMap()
                        {
                            EmployeeID = long.Parse(keyval.Key),
                            AssignVehicleMapID = entity.AssignVehicleMapIID,
                            CreatedBy = entity.CreatedBy,
                            CreatedDate = entity.CreatedDate,
                            UpdatedBy = entity.UpdatedBy,
                            UpdatedDate = entity.UpdatedDate
                        });
                    }
                }

                if (entity.AssignVehicleMapIID == 0)
                {
                    //Inactive previous mapping data
                    var existingMaps = dbContext.AssignVehicleMaps.Where(x => x.RouteID == entity.RouteID && x.IsActive == true).AsNoTracking().ToList();

                    if (existingMaps.Count > 0)
                    {
                        foreach (var map in existingMaps)
                        {
                            map.IsActive = false;
                            map.DateTo = DateTime.Now;
                            dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }

                    foreach (var attendantMap in entity.AssignVehicleAttendantMaps)
                    {
                        dbContext.Entry(attendantMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    //delete map data and re-entry
                    var oldMapDatas = dbContext.AssignVehicleAttendantMaps.Where(x => x.AssignVehicleMapID == toDto.AssignVehicleMapIID).AsNoTracking().ToList();
                    if (oldMapDatas.Count() > 0)
                    {
                        dbContext.AssignVehicleAttendantMaps.RemoveRange(oldMapDatas);
                    }

                    foreach (var attendantMap in entity.AssignVehicleAttendantMaps)
                    {
                        dbContext.Entry(attendantMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.AssignVehicleMapIID));
        }

        public string GetEmployeeNameByID(long? employeeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Employees.Where(x => x.EmployeeIID == employeeID)
                    .AsNoTracking()
                    .FirstOrDefault();

                var employeeName = entity != null ? entity.EmployeeCode + " - " + entity.FirstName + " " + entity.MiddleName + " " + entity.LastName : null;

                return employeeName;
            }
        }

        public int? GetAcademicYearIDFromRouteGroup(int? routeGroupID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.RouteGroups.Where(x => x.RouteGroupID == routeGroupID).AsNoTracking().FirstOrDefault();

                return entity.AcademicYearID;
            }
        }

        public string DuplicateCheck(AssignVehicleDTO vDTO)
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                //For Check Diver Duplicate
                var mapDetail = dbContext.AssignVehicleMaps.Where(X => X.EmployeeID == vDTO.EmployeeID && X.IsActive == true)
                    .AsNoTracking().FirstOrDefault();

                if (mapDetail != null)
                {
                    if (vDTO.DateFrom <= mapDetail.DateTo)
                    {
                        throw new Exception("Driver already allocated in another vehicle");
                    }
                }
            }

            return null;
        }

        public List<VehicleDTO> GetVehicleDetailsByEmployeeLoginID(long loginID)
        {
            var dtos = new List<VehicleDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                int? currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);

                var employeeDet = dbContext.Employees.Where(e => e.LoginID == loginID && e.IsActive == true).AsNoTracking().FirstOrDefault();

                if (employeeDet != null)
                {
                    var assignVehicleDetails = dbContext.AssignVehicleMaps.Where(a => a.EmployeeID == employeeDet.EmployeeIID && a.IsActive == true &&
                    a.Routes1.IsActive == true && a.Routes1.RouteGroup.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                        .AsNoTracking().ToList();

                    if (assignVehicleDetails.Count > 0)
                    {
                        foreach (var assign in assignVehicleDetails)
                        {
                            var vehicleMapDetails = dbContext.RouteVehicleMaps.Where(v => v.RouteID == assign.RouteID && v.IsActive == true &&
                            v.Routes1.IsActive == true && v.Routes1.RouteGroup.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                                .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                                .AsNoTracking()
                                .ToList();

                            if (vehicleMapDetails != null && vehicleMapDetails.Count > 0)
                            {
                                foreach (var vehicleMap in vehicleMapDetails)
                                {
                                    var vehicleDet = vehicleMap.Vehicle.IsActive == true ? vehicleMap.Vehicle : null;

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
                                            RouteID = vehicleMap.RouteID,
                                            EmployeeID = employeeDet.EmployeeIID,
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return dtos;
        }

        public List<AssignVehicleDTO> GetVehicleAssignDetailsByEmployeeIDandRouteID(long employeeID, long routeID)
        {
            var dtos = new List<AssignVehicleDTO>();
            var attendDto = new List<VehicleAttendantDTO>();

            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                int? currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);

                var assignVehicleDetails = dbContext.AssignVehicleMaps
                    .Include(i => i.Employee)
                    .Include(i => i.Routes1).ThenInclude(i => i.RouteGroup).ThenInclude(i => i.AcademicYear)
                    .Where(a => a.EmployeeID == employeeID && a.RouteID == routeID && a.IsActive == true && a.Routes1.IsActive == true &&
                    a.Routes1.RouteGroup.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                    .AsNoTracking()
                    .ToList();

                if (assignVehicleDetails.Count > 0)
                {
                    foreach (var assign in assignVehicleDetails)
                    {
                        attendDto = new List<VehicleAttendantDTO>();

                        var attendMapDet = dbContext.AssignVehicleAttendantMaps
                            .Include(i => i.Employee)
                            .Include(i => i.Employee).ThenInclude(i => i.Gender)
                            .Include(i => i.Employee).ThenInclude(i => i.Departments1)
                            .Include(i => i.Employee).ThenInclude(i => i.Designation)
                            .Where(a => a.AssignVehicleMapID == assign.AssignVehicleMapIID)
                            .AsNoTracking().ToList();

                        if (attendMapDet.Count > 0)
                        {
                            foreach (var attend in attendMapDet)
                            {
                                if (attend.EmployeeID.HasValue)
                                {
                                    if (attend.Employee != null)
                                    {
                                        attendDto.Add(new VehicleAttendantDTO()
                                        {
                                            EmployeeIID = attend.Employee.EmployeeIID,
                                            EmployeeCode = attend.Employee.EmployeeCode,
                                            EmployeeAlias = attend.Employee.EmployeeAlias,
                                            EmployeeFullName = attend.Employee.FirstName + " " + attend.Employee.MiddleName + " " + attend.Employee.LastName,
                                            EmployeeFullNamewithCode = attend.Employee.EmployeeCode + " - " + attend.Employee.FirstName + "" + attend.Employee.MiddleName + "" + attend.Employee.LastName,
                                            FirstName = attend.Employee.FirstName,
                                            MiddleName = attend.Employee.MiddleName,
                                            LastName = attend.Employee.LastName,
                                            DateOfJoining = attend.Employee.DateOfJoining.HasValue ? attend.Employee.DateOfJoining : null,
                                            JoiningDateString = attend.Employee.DateOfJoining.HasValue ? attend.Employee.DateOfJoining.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : "NA",
                                            DateOfBirth = attend.Employee.DateOfBirth.HasValue ? attend.Employee.DateOfBirth : null,
                                            DateOfBirthString = attend.Employee.DateOfBirth.HasValue ? attend.Employee.DateOfBirth.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : "NA",
                                            WorkMobileNo = attend.Employee.WorkMobileNo != null ? attend.Employee.WorkMobileNo : "NA",
                                            WorkEmail = attend.Employee.WorkEmail != null ? attend.Employee.WorkEmail : "NA",
                                            WorkPhone = attend.Employee.WorkPhone != null ? attend.Employee.WorkPhone : "NA",
                                            Age = attend.Employee.Age,
                                            AdhaarCardNo = attend.Employee.AdhaarCardNo != null ? attend.Employee.AdhaarCardNo : "NA",
                                            GenderID = attend.Employee.GenderID,
                                            GenderName = attend.Employee.GenderID.HasValue ? attend.Employee.Gender.Description : "NA",
                                            DepartmentID = attend.Employee.DepartmentID,
                                            DepartmentName = attend.Employee.DepartmentID.HasValue ? attend.Employee.Departments1.DepartmentName : "NA",
                                            DesignationID = attend.Employee.DesignationID,
                                            DesignationName = attend.Employee.DesignationID.HasValue ? attend.Employee.Designation.DesignationName : "NA",
                                        });
                                    }
                                }
                            }
                        }

                        dtos.Add(new AssignVehicleDTO()
                        {
                            AssignVehicleMapIID = assign.AssignVehicleMapIID,
                            DateFrom = assign.DateFrom,
                            DateTo = assign.DateTo,
                            EmployeeID = assign.EmployeeID,
                            Employee = assign.EmployeeID.HasValue ? new KeyValueDTO() { Key = assign.EmployeeID.ToString(), Value = assign.Employee.FirstName + " " + assign.Employee.MiddleName + " " + assign.Employee.LastName } : new KeyValueDTO(),
                            IsActive = assign.IsActive,
                            DriverName = assign.EmployeeID.HasValue ? assign.Employee.FirstName + " " + assign.Employee.MiddleName + " " + assign.Employee.LastName : null,
                            VehicleAttendantDetails = attendDto,
                        });
                    }
                }
            }

            return dtos;
        }

        //GetDriver Schedule datas by route,vehicle & selected date
        public List<DriverScheduleLogDTO> GetDriverScheduleListByParameters(int? routeID, int? vehicleID, DateTime scheduledDate)
        {
            List<DriverScheduleLogDTO> driverScheduleList = new List<DriverScheduleLogDTO>();
            string connectionString = Infrastructure.ConfigHelper.GetSchoolConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("[schools].[GET_DRIVER_SCHEDULE_DATAS]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@RouteID", SqlDbType.Int).Value = routeID;
                    cmd.Parameters.Add("@VehicleID", SqlDbType.Int).Value = vehicleID;
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = scheduledDate;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DriverScheduleLogDTO driverSchedule = new DriverScheduleLogDTO
                            {
                                PassengerTypeID = reader["PassengerTypeID"] as long?,
                                code = reader["code"] as string,
                                SheduleDate = reader["SheduleDate"] as DateTime?,
                                VehicleID = reader["VehicleID"] as long?,
                                VehicleRegistrationNumber = reader["VehicleRegistrationNumber"] as string,
                                RouteCode = reader["RouteCode"] as string,
                                EmployeeID = reader["EmployeeID"] as long?,
                                RouteID = reader["RouteID"] as int?,
                                DriverScheduleLogIID = (long)reader["DriverScheduleLogIID"],
                                StudentID = reader["StudentID"] as long?,
                                ClassSection = reader["ClassSection"] as string,
                                SectionName = reader["SectionName"] as string,
                                PassengerCode = reader["PassengerCode"] as string,
                                PassengerName = reader["PassengerName"] as string,
                                StatusMark = reader["StatusMark"] as string
                            };
                            driverScheduleList.Add(driverSchedule);
                        }
                    }
                }
            }

            return driverScheduleList;
        }

        public EmployeesDTO GetDriverDetailsByStudent(long studentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                int? currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);
                var studentDet = dbContext.Students.Where(y => y.StudentIID == studentID && y.IsActive == true && y.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID).AsNoTracking().FirstOrDefault();

                var driverDetails = new EmployeesDTO();
                var currentDate = DateTime.Now;
                var loginID = _context.LoginID;

                if (studentDet != null)
                {
                    var transportDet = dbContext.StudentRouteStopMaps.Where(a => a.StudentID == studentDet.StudentIID && a.IsActive == true && a.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                        .Include(i => i.Routes1)
                        .Include(i => i.Routes11)
                        .AsNoTracking()
                        .OrderByDescending(x => x.StudentRouteStopMapIID)
                        .FirstOrDefault();

                    if (transportDet != null)
                    {
                        var route = transportDet.Routes11 != null ? dbContext.Routes1.AsNoTracking().FirstOrDefault(b => b.RouteID == transportDet.Routes11.RouteID && b.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID) : transportDet.Routes1 != null ? dbContext.Routes1.FirstOrDefault(b => b.RouteID == transportDet.Routes1.RouteID) : null;

                        var assign = route != null ? dbContext.AssignVehicleMaps
                                        .Where(d => d.RouteID == route.RouteID && d.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                                        .Include(v => v.Vehicle) // Include the Vehicle information
                                        .AsNoTracking()
                                        .FirstOrDefault() : null;

                        var employeeDet = assign != null ? dbContext.Employees.Where(e => e.EmployeeIID == assign.EmployeeID)
                            .Include(g => g.Gender)
                            .AsNoTracking().FirstOrDefault() : null;


                        if (employeeDet != null)
                        {
                            driverDetails.EmployeeName = employeeDet.EmployeeName;
                            driverDetails.EmployeeCode = employeeDet.EmployeeCode;
                            driverDetails.Age= employeeDet.Age;
                            driverDetails.GenderName = employeeDet.Gender.Description;
                            driverDetails.WorkMobileNo = route.ContactNumber;
                            //driverDetails.WorkMobileNo = employeeDet.EmergencyContactNo;
                            driverDetails.DateOfJoining = employeeDet.DateOfJoining;
                            //driverDetails.Vehicle = assign.Vehicle.VehicleRegistrationNumber.ToString();
                            driverDetails.Vehicle = assign?.Vehicle?.VehicleRegistrationNumber;
                            driverDetails.RouteCode = route.RouteCode;



                        }

                    }
                }

                return driverDetails;
            }
        }



        public List<RouteStopFeeDTO> GetStopsPositionsByRoute(long studentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                int? currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);
                var studentDet = dbContext.Students.Where(y => y.StudentIID == studentID && y.IsActive == true && y.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID).AsNoTracking().FirstOrDefault();

                var driverDetails = new RouteStopFeeDTO();
                var routeDtos = new List<RoutesDTO>();
                var stopDtos = new List<RouteStopFeeDTO>();

                if (studentDet != null)
                {
                    var transportDet = dbContext.StudentRouteStopMaps.Where(a => a.StudentID == studentDet.StudentIID && a.IsActive == true && a.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                        .Include(i => i.Routes1).ThenInclude(i => i.RouteStopMaps)
                        .Include(i => i.Routes11).ThenInclude(i => i.RouteStopMaps)
                        .Include(i => i.RouteStopMap)
                        .AsNoTracking()
                        .OrderByDescending(x => x.StudentRouteStopMapIID)
                        .FirstOrDefault();

                    if (transportDet != null)
                    {
                        var route = transportDet.Routes11 != null ? dbContext.Routes1.AsNoTracking().FirstOrDefault(b => b.RouteID == transportDet.Routes11.RouteID && b.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID) : transportDet.Routes1 != null ? dbContext.Routes1.FirstOrDefault(b => b.RouteID == transportDet.Routes1.RouteID) : null;


                        var assign = route != null ? dbContext.AssignVehicleMaps
                            .Where(d => d.RouteID == route.RouteID && d.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                            .Include(v => v.Routes1).AsNoTracking().FirstOrDefault() : null;


                        stopDtos = new List<RouteStopFeeDTO>();

                        var stopDetails = dbContext.RouteStopMaps
                            .Where(s => s.RouteID == route.RouteID && s.IsActive == true)
                            .OrderBy(x => x.SequenceNo)
                            .ToList();

                        if (stopDetails.Count > 0)
                        {
                            foreach (var stop in stopDetails)
                            {
                                stopDtos.Add(new RouteStopFeeDTO()
                                {
                                    RouteStopMapIID = stop.RouteStopMapIID,
                                    RouteID = stop.RouteID,
                                    StopName = stop.StopName,
                                    StopCode = stop.StopCode,
                                    Latitude = stop.Latitude,
                                    Longitude = stop.Longitude,
                                    SequenceNo = stop.SequenceNo,

                                    RouteFareOneWay = stop.OneWayFee,
                                    RouteFareTwoWay = stop.TwoWayFee,
                                    IsActive = stop.IsActive,
                                    AcademicYearID = stop.AcademicYearID
                                });

                            }
                        }

                    }
                }

                return stopDtos;
            }
        }


        public bool GetStudentInOutVehicleStatus(long studentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var todayDate = DateTime.Now.Date;

                // Define the desired order for ScheduleLogType
                var logTypeOrder = new Dictionary<string, int>
        {
            { "PICK-IN", 1 },
            { "PICK-OUT", 2 },
            { "DROP-IN", 3 },
            { "DROP-OUT", 4 }
        };

                // Fetch and sort the logs based on SheduleDate and then by ScheduleLogType
                var scheduleFilterDet = dbContext.DriverScheduleLogs
                    .Where(s => s.StudentID == studentID &&
                                s.SheduleDate.HasValue &&
                                s.SheduleDate.Value.Date == todayDate)
                    .AsNoTracking()
                    .ToList()
                    .OrderBy(s => s.SheduleDate)
                    .ThenBy(s => logTypeOrder.ContainsKey(s.ScheduleLogType) ? logTypeOrder[s.ScheduleLogType] : int.MaxValue);

                var isStudentIn = false;

                // Determine the student's in/out status based on the sorted logs
                foreach (var scheduleFilter in scheduleFilterDet)
                {
                    if ((scheduleFilter.ScheduleLogType == "PICK-IN" || scheduleFilter.ScheduleLogType == "DROP-IN") && scheduleFilter.Status == "I")
                    {
                        isStudentIn = true;
                    }
                    else if ((scheduleFilter.ScheduleLogType == "PICK-OUT" || scheduleFilter.ScheduleLogType == "DROP-OUT") && scheduleFilter.Status == "O")
                    {
                        isStudentIn = false;
                    }
                }

                return isStudentIn;
            }
        }



        public List<RouteStopFeeDTO> GetStopsPositionsByRouteStaff(long staffId)
        {
            using (var schoolContext = new dbEduegateSchoolContext())
            {
                try
                {
                    // Fetch the current academic year status
                    var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                    int currentAcademicYearStatusId = int.Parse(currentAcademicYearStatus);

                    // Retrieve the staff details and ensure the staff is active
                    var staff = schoolContext.Employees
                        .AsNoTracking()
                        .FirstOrDefault(e => e.EmployeeIID == staffId && e.IsActive == true);

                    var stopDtos = new List<RouteStopFeeDTO>();

                    if (staff != null)
                    {
                        // Fetch the staff route stop map details and related routes
                        var staffRouteStopMap = schoolContext.StaffRouteStopMaps
                            .Include(s => s.Routes1).ThenInclude(r => r.RouteStopMaps)
                            .Include(s => s.Routes11).ThenInclude(r => r.RouteStopMaps)
                            .Where(s => s.StaffID == staff.EmployeeIID && s.IsActive == true && s.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusId)
                            .AsNoTracking()
                            .OrderByDescending(s => s.StaffRouteStopMapIID)
                            .FirstOrDefault();

                        if (staffRouteStopMap != null)
                        {
                            // Determine the route
                            var route = staffRouteStopMap.Routes11 ?? staffRouteStopMap.Routes1;
                            if (route != null)
                            {
                                // Fetch the vehicle assignment details
                                var assignedVehicleMap = schoolContext.AssignVehicleMaps
                                    .Where(avm => avm.RouteID == route.RouteID && avm.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusId)
                                    .Include(v => v.Routes1)
                                    .AsNoTracking()
                                    .FirstOrDefault();

                                // Fetch the stop details for the route directly from the database
                                var stopDetails = schoolContext.RouteStopMaps
                                    .Where(s => s.RouteID == route.RouteID && s.IsActive == true)
                                    .OrderBy(s => s.SequenceNo)
                                    .Select(stop => new RouteStopFeeDTO
                                    {
                                        RouteStopMapIID = stop.RouteStopMapIID,
                                        RouteID = stop.RouteID,
                                        StopName = stop.StopName,
                                        StopCode = stop.StopCode,
                                        Latitude = stop.Latitude,
                                        Longitude = stop.Longitude,
                                        SequenceNo = stop.SequenceNo,
                                        RouteFareOneWay = stop.OneWayFee,
                                        RouteFareTwoWay = stop.TwoWayFee,
                                        IsActive = stop.IsActive,
                                        AcademicYearID = stop.AcademicYearID
                                    })
                                    .ToList();

                                // Add the retrieved stop details to stopDtos
                                stopDtos.AddRange(stopDetails);
                            }
                        }
                    }

                    return stopDtos;
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return new List<RouteStopFeeDTO>(); // Return an empty list or handle the exception as appropriate
                }
            }
        }



        public bool GetStaffInOutVehicleStatus(long staffID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var todayDate = DateTime.Now.Date;

                // Fetch the schedule logs for the staff for the current date
                var scheduleLogs = dbContext.DriverScheduleLogs
                    .Where(s => s.EmployeeID == staffID &&
                                s.SheduleDate.HasValue &&
                                s.SheduleDate.Value.Year == todayDate.Year &&
                                s.SheduleDate.Value.Month == todayDate.Month &&
                                s.SheduleDate.Value.Day == todayDate.Day)
                    .OrderBy(s => s.SheduleDate)
                    .AsNoTracking()
                    .ToList();

                var isStaffIn = false;

                // Determine the in-out status based on the logs
                foreach (var log in scheduleLogs)
                {
                    if ((log.ScheduleLogType == "PICK-IN" || log.ScheduleLogType == "DROP-IN") && log.Status == "I")
                    {
                        isStaffIn = true;
                    }
                    else if ((log.ScheduleLogType == "PICK-OUT" || log.ScheduleLogType == "DROP-OUT") && log.Status == "O")
                    {
                        isStaffIn = false;
                    }

                    // Return early if the last log type is processed
                    if (log.ScheduleLogType == "DROP-OUT")
                    {
                        break;
                    }
                }

                return isStaffIn;
            }
        }

        public EmployeesDTO GetDriverDetailsByStaff(long employeeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                // Fetch the current academic year status
                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                int? currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);

                // Fetch the staff details
                var staffDet = dbContext.Employees
                    .Where(e => e.EmployeeIID == employeeID && e.IsActive == true)
                    .Include(e => e.Gender)
                    .AsNoTracking()
                    .FirstOrDefault();

                var driverDetails = new EmployeesDTO();

                if (staffDet != null)
                {
                    // Fetch the staff's route and vehicle assignment details
                    var staffRouteMap = dbContext.StaffRouteStopMaps
                        .Where(s => s.StaffID == staffDet.EmployeeIID && s.IsActive == true && s.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                        .Include(s => s.Routes1)
                        .Include(s => s.Routes11)
                        .AsNoTracking()
                        .OrderByDescending(s => s.StaffRouteStopMapIID)
                        .FirstOrDefault();

                    if (staffRouteMap != null)
                    {
                        // Determine the route
                        var route = staffRouteMap.Routes11 ?? staffRouteMap.Routes1;

                        if (route != null)
                        {
                            // Fetch the assigned vehicle details
                            var assignedVehicleMap = dbContext.AssignVehicleMaps
                                .Where(avm => avm.RouteID == route.RouteID && avm.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                                .Include(avm => avm.Vehicle)
                                .AsNoTracking()
                                .FirstOrDefault();

                            if (assignedVehicleMap != null)
                            {
                                // Fetch the driver details
                                var driverDet = dbContext.Employees
                                    .Where(e => e.EmployeeIID == assignedVehicleMap.EmployeeID)
                                    .Include(e => e.Gender)
                                    .AsNoTracking()
                                    .FirstOrDefault();

                                if (driverDet != null)
                                {
                                    driverDetails.EmployeeName = driverDet.EmployeeName;
                                    driverDetails.EmployeeCode = driverDet.EmployeeCode;
                                    driverDetails.Age = driverDet.Age;
                                    driverDetails.GenderName = driverDet.Gender.Description;
                                    driverDetails.WorkMobileNo = driverDet.EmergencyContactNo; // Assuming this is the correct field for mobile number
                                    driverDetails.DateOfJoining = driverDet.DateOfJoining;
                                    driverDetails.Vehicle = assignedVehicleMap.Vehicle.VehicleRegistrationNumber;
                                }
                            }
                        }
                    }
                }

                return driverDetails;
            }
        }



    }
}