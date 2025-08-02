using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Transports;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers.School.Transports
{
    public class EventTransportAllocationMapper : DTOEntityDynamicMapper
    {
        public static EventTransportAllocationMapper Mapper(CallContext context)
        {
            var mapper = new EventTransportAllocationMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<EventTransportAllocationDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }
        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private EventTransportAllocationDTO ToDTO(long IID)
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.EventTransportAllocations.Where(x => x.EventTransportAllocationIID == IID)
                    .Include(x => x.SchoolEvent)
                    .Include(x => x.Vehicle)
                    .Include(x => x.Employee1)
                    .Include(x => x.Employee)

                    .Include(x => x.EventTransportAllocationMaps)
                    .ThenInclude(i => i.StudentRouteStopMap).ThenInclude(i => i.Student).ThenInclude(i => i.Class)

                    .Include(x => x.EventTransportAllocationMaps)
                    .ThenInclude(i => i.StudentRouteStopMap).ThenInclude(i => i.Student).ThenInclude(i => i.Section)

                    .Include(x => x.EventTransportAllocationMaps)
                    .ThenInclude(i => i.StaffRouteStopMap).ThenInclude(i => i.Employee).ThenInclude(i => i.Designation)

                     .Include(x => x.EventTransportAllocationMaps)
                    .ThenInclude(i => i.ToRoute)

                    .AsNoTracking()
                    .FirstOrDefault();

                var getToRoute = dbContext.Routes1
                    .AsNoTracking().FirstOrDefault(r => r.RouteID == entity.RouteID);

                var dto = new EventTransportAllocationDTO()
                {
                    EventTransportAllocationIID = entity.EventTransportAllocationIID,
                    EventID = entity.EventID.HasValue ? entity.EventID : null,
                    DriverID = entity.DriverID.HasValue ? entity.DriverID : null,
                    AttendarID = entity.AttendarID.HasValue ? entity.AttendarID : null,
                    VehicleID = entity.VehicleID.HasValue ? entity.VehicleID : null,
                    RouteID = entity.RouteID.HasValue ? entity.RouteID : null,
                    Event = entity.EventID.HasValue ? new KeyValueDTO() { Key = entity.EventID.ToString(), Value = entity.SchoolEvent != null ? entity.SchoolEvent.EventName : null } : new KeyValueDTO(),
                    ToRoute = entity.RouteID.HasValue ? new KeyValueDTO() { Key = entity.RouteID.ToString(), Value = getToRoute != null ? getToRoute.RouteCode : null } : new KeyValueDTO(),
                    Description = entity.Description,
                    Vehicle = entity.VehicleID.HasValue ? new KeyValueDTO() { Key = entity.VehicleID.ToString(), Value = entity.Vehicle?.VehicleRegistrationNumber } : new KeyValueDTO(),
                    Driver = entity.DriverID.HasValue ? new KeyValueDTO() { Key = entity.DriverID.ToString(), Value = entity.Employee1 != null ? entity.Employee1.EmployeeCode+" - "+ entity.Employee1.FirstName+" "+ entity.Employee1.MiddleName+" "+ entity.Employee1.LastName : null } : new KeyValueDTO(),
                    Attendar = entity.AttendarID.HasValue ? new KeyValueDTO() { Key = entity.AttendarID.ToString(), Value = entity.Employee != null ? entity.Employee.EmployeeCode + " - " + entity.Employee.FirstName + " " + entity.Employee.MiddleName + " " + entity.Employee.LastName : null } : new KeyValueDTO(),
                    SchoolID = entity.SchoolID,
                    AcademicYearID = entity.AcademicYearID,
                    EventDate = entity.EventDate,
                    IsPickUp = entity.IsPickUp,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                };

                foreach (var listDat in entity.EventTransportAllocationMaps)
                {
                    if (listDat.StudentID != null)
                    {
                        dto.StudentList.Add(new EventTransportAllocationMapDTO()
                        {
                            EventTransportAllocationMapIID = listDat.EventTransportAllocationMapIID,
                            EventTransportAllocationID = listDat.EventTransportAllocationID,
                            StudentRouteStopMapID = listDat.StudentRouteStopMapID,
                            StudentID = listDat.StudentID,
                            ClassSection = listDat.StudentRouteStopMap.Student.Class.ClassDescription +" - "+ listDat.StudentRouteStopMap.Student.Section.SectionName,
                            PickUpRoute = listDat?.PickUpRoute,
                            PickupStop = listDat?.PickUpStop,
                            DropRoute = listDat?.DropRoute,
                            DropStop = listDat?.DropStop,
                            ToRouteID = listDat?.ToRouteID,
                            Student = listDat.StudentID.HasValue ? new KeyValueDTO() { Key = listDat.StudentID.ToString(), Value = listDat.StudentRouteStopMap.Student != null ? 
                            listDat.StudentRouteStopMap.Student.AdmissionNumber+" - "+ listDat.StudentRouteStopMap.Student.FirstName+" "+ listDat.StudentRouteStopMap.Student.MiddleName+" "+listDat.StudentRouteStopMap.Student.LastName : null } : new KeyValueDTO(),
                        });
                    }

                    if (listDat.EmployeeID != null)
                    {
                        dto.StaffList.Add(new EventTransportAllocationMapDTO()
                        {
                            EventTransportAllocationMapIID = listDat.EventTransportAllocationMapIID,
                            EventTransportAllocationID = listDat.EventTransportAllocationID,
                            StaffRouteStopMapID = listDat.StaffRouteStopMapID,
                            StaffID = listDat.EmployeeID,
                            Designation = listDat.StaffRouteStopMap.Employee.Designation?.DesignationName,
                            PickUpRoute = listDat?.PickUpRoute,
                            PickupStop = listDat?.PickUpStop,
                            DropRoute = listDat?.DropRoute,
                            DropStop = listDat?.DropStop,
                            ToRouteID = listDat?.ToRouteID,
                            Staff = listDat.EmployeeID.HasValue ? new KeyValueDTO()
                            {
                                Key = listDat.EmployeeID.ToString(),
                                Value = listDat.StaffRouteStopMap.Employee != null ?
                            listDat.StaffRouteStopMap.Employee.EmployeeCode + " - " + listDat.StaffRouteStopMap.Employee.FirstName + " " + listDat.StaffRouteStopMap.Employee.MiddleName + " " + listDat.StaffRouteStopMap.Employee.LastName : null
                            } : new KeyValueDTO(),

                        });
                    }
                }


                return dto;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as EventTransportAllocationDTO;

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (toDto.EventID == null)
                {
                    throw new Exception("Please select event !");
                }

                if (toDto.DriverID == null)
                {
                    throw new Exception("Please select driver !");
                }

                var entity = new EventTransportAllocation()
                {
                    EventTransportAllocationIID = toDto.EventTransportAllocationIID,
                    EventID = toDto.EventID,
                    EventDate = toDto.EventDate,
                    Description = toDto.Description,
                    IsPickUp = toDto.IsPickUp,
                    VehicleID = toDto.VehicleID,
                    DriverID = toDto.DriverID,
                    AttendarID = toDto.AttendarID,
                    RouteID = toDto.RouteID,
                    SchoolID = toDto.EventTransportAllocationIID == 0 ? Convert.ToByte(_context.SchoolID) : toDto.SchoolID,
                    AcademicYearID = toDto.EventTransportAllocationIID == 0 ? Convert.ToInt32(_context.AcademicYearID) : toDto.AcademicYearID,
                    CreatedBy = toDto.EventTransportAllocationIID == 0 ? Convert.ToInt32(_context.LoginID) : toDto.CreatedBy,
                    CreatedDate = toDto.EventTransportAllocationIID == 0 ? DateTime.Now.Date : toDto.CreatedDate,
                    UpdatedBy = toDto.EventTransportAllocationIID == 0 ? toDto.UpdatedBy : Convert.ToInt32(_context.LoginID),
                    UpdatedDate = toDto.EventTransportAllocationIID == 0 ? toDto.UpdatedDate : DateTime.Now.Date, 
                };

                #region remove map data
                var studIIDs = toDto.StudentList
                    .Select(a => a.EventTransportAllocationMapIID)
                    .ToList();

                var staffIIDs = toDto.StaffList
                    .Select(a => a.EventTransportAllocationMapIID)
                    .ToList();

                var allIIDs = studIIDs.Concat(staffIIDs).ToList();

                var entityIID = entity.EventTransportAllocationIID;

                var entitiesToRemove = dbContext.EventTransportAllocationMaps
                    .Where(x => x.EventTransportAllocationID == entityIID && !allIIDs.Contains(x.EventTransportAllocationMapIID))
                    .AsNoTracking()
                    .ToList();

                if (entitiesToRemove.Any())
                {
                    dbContext.EventTransportAllocationMaps.RemoveRange(entitiesToRemove);
                }

                #endregion

                entity.EventTransportAllocationMaps = new List<EventTransportAllocationMap>();
                //from student List Dto to entity
                foreach (var dtoStudList in toDto.StudentList)
                {
                    entity.EventTransportAllocationMaps.Add(new EventTransportAllocationMap()
                    {
                        EventTransportAllocationMapIID = dtoStudList.EventTransportAllocationMapIID,
                        EventTransportAllocationID = toDto.EventTransportAllocationIID,
                        StudentRouteStopMapID = dtoStudList.StudentRouteStopMapID,
                        StudentID = dtoStudList.StudentID,
                        PickUpRoute = dtoStudList.PickUpRoute,
                        DropRoute = dtoStudList.DropRoute,
                        PickUpStop = dtoStudList.PickupStop,
                        DropStop = dtoStudList.DropStop,
                        ToRouteID = toDto.RouteID.HasValue ? toDto.RouteID : dtoStudList.ToRouteID.HasValue ? dtoStudList.ToRouteID : toDto.IsPickUp == true ? dtoStudList.PickupRouteID : dtoStudList.DropRouteID,
                    });
                }

                //from staff List Dto to entity
                foreach (var dtoStaffList in toDto.StaffList)
                {
                    entity.EventTransportAllocationMaps.Add(new EventTransportAllocationMap()
                    {
                        EventTransportAllocationMapIID = dtoStaffList.EventTransportAllocationMapIID,
                        EventTransportAllocationID = toDto.EventTransportAllocationIID,
                        StaffRouteStopMapID = dtoStaffList.StaffRouteStopMapID,
                        EmployeeID = dtoStaffList.StaffID,
                        PickUpRoute = dtoStaffList.PickUpRoute,
                        DropRoute = dtoStaffList.DropRoute,
                        PickUpStop = dtoStaffList.PickupStop,
                        DropStop = dtoStaffList.DropStop,
                        ToRouteID = toDto.RouteID.HasValue ? toDto.RouteID : dtoStaffList.ToRouteID.HasValue ? dtoStaffList.ToRouteID : toDto.IsPickUp == true ? dtoStaffList.PickupRouteID : dtoStaffList.DropRouteID,
                    });
                }

                dbContext.EventTransportAllocations.Add(entity);
                if (entity.EventTransportAllocationIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    if (entity.EventTransportAllocationMaps.Count > 0)
                    {
                        foreach (var mapData in entity.EventTransportAllocationMaps)
                        {
                            if (mapData.EventTransportAllocationMapIID == 0)
                            {
                                dbContext.Entry(mapData).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(mapData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.EventTransportAllocationIID));
            }
        }

        #region --- Get Data

        public List<EventTransportAllocationMapDTO> GetStudentandStaffsByRouteIDforEvent(EventTransportAllocationDTO eventDto)
        {
            var listDataDTO = new List<EventTransportAllocationMapDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var routeIds = eventDto.Route?.Select(r => int.Parse(r.Key) as int?).ToList();
                var classIds = eventDto.Class?.Select(r => int.Parse(r.Key) as int?).ToList();

                var studentTransportMaps = dbContext.StudentRouteStopMaps
                .Include(i => i.Student.Class)
                .Include(i => i.Student.Section)
                .Include(i => i.RouteStopMap1)
                .Include(i => i.RouteStopMap2)
                .Include(i => i.Routes11)
                .Include(i => i.Routes1)
                .Where(x => x.SchoolID == _context.SchoolID && x.IsActive == true 
                 && eventDto.ListStudents == true && (classIds == null || classIds.Count == 0 || classIds.Contains(x.Student.ClassID)) &&
                 (eventDto.IsRouteType == "Pick" && (routeIds == null || routeIds.Count == 0 || routeIds.Contains(x.PickupRouteID))) ||
                 (eventDto.IsRouteType == "Drop" && (routeIds == null || routeIds.Count == 0 || routeIds.Contains(x.DropStopRouteID))))
                .AsNoTracking().OrderBy(o => o.Student.AdmissionNumber).ToList();

                var staffTransportMaps = dbContext.StaffRouteStopMaps
                    .Include(i => i.Employee.Departments1)
                    .Include(i => i.Employee.Designation)
                    .Include(i => i.RouteStopMap1)
                    .Include(i => i.RouteStopMap2)
                    .Include(i => i.Routes11)
                    .Include(i => i.Routes1)
                    .Where(x => x.SchoolID == _context.SchoolID && x.IsActive == true && eventDto.ListStaffs == true
                    && (eventDto.IsRouteType == "Pick" && (routeIds == null || routeIds.Count == 0 || routeIds.Contains(x.PickupRouteID))) ||
                    (eventDto.IsRouteType == "Drop" && (routeIds == null || routeIds.Count == 0 || routeIds.Contains(x.DropStopRouteID))))
                    .AsNoTracking().OrderBy(o => o.Employee.EmployeeCode).ToList();

                listDataDTO.AddRange(studentTransportMaps.Select(CreateStudentDTO));
                listDataDTO.AddRange(staffTransportMaps.Select(CreateStaffDTO));
            }
            return listDataDTO;
        }

        private EventTransportAllocationMapDTO CreateStudentDTO(StudentRouteStopMap student)
        {
            return new EventTransportAllocationMapDTO
            {
                StudentID = student.StudentID,
                StudentRouteStopMapID = student.StudentRouteStopMapIID,
                Student = new KeyValueDTO { Key = student.StudentID.ToString(), Value = $"{student.Student.AdmissionNumber} - {student.Student.FirstName} {student.Student.MiddleName} {student.Student.LastName}" },
                ClassName = student.Student.Class.ClassDescription,
                Section = student.Student.Section.SectionName,
                PickupStop = student.RouteStopMap1?.StopName,
                DropStop = student.RouteStopMap2?.StopName,
                PickUpRoute = student.Routes11?.RouteCode,
                DropRoute = student.Routes1?.RouteCode,
                PickupRouteID = student.PickupRouteID,
                DropRouteID = student.DropStopRouteID,
                ClassSection = $"{student.Student.Class.ClassDescription} - {student.Student.Section.SectionName}",
            };
        }

        private EventTransportAllocationMapDTO CreateStaffDTO(StaffRouteStopMap staff)
        {
            return new EventTransportAllocationMapDTO
            {
                StaffID = staff.StaffID,
                StaffRouteStopMapID = staff.StaffRouteStopMapIID,
                Staff = new KeyValueDTO { Key = staff.StaffID.ToString(), Value = $"{staff.Employee.EmployeeCode} - {staff.Employee.FirstName} {staff.Employee.MiddleName} {staff.Employee.LastName}" },
                Department = staff.Employee.Departments1?.DepartmentName,
                Designation = staff.Employee.Designation?.DesignationName,
                PickupStop = staff.RouteStopMap1?.StopName,
                DropStop = staff.RouteStopMap2?.StopName,
                PickUpRoute = staff.Routes11?.RouteCode,
                DropRoute = staff.Routes1?.RouteCode,
                PickupRouteID = staff.PickupRouteID,
                DropRouteID= staff.DropStopRouteID,
            };
        }
        #endregion

    }
}