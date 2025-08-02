using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using System;
using System.Linq;
using Eduegate.Services.Contracts.School.Transports;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Eduegate.Services.Contracts.School.Students;

namespace Eduegate.Domain.Mappers.School.Transports
{
    public class RouteShiftingMapper : DTOEntityDynamicMapper
    {
        public static RouteShiftingMapper Mapper(CallContext context)
        {
            var mapper = new RouteShiftingMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<RouteShiftingDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private RouteShiftingDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StudentRouteStopMapLogs
                    .Where(X => X.RouteID == IID && X.AcademicYearID == _context.AcademicYearID)
                    .Include(i => i.Routes1)
                    .Include(i => i.Routes11)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.RouteStopMap1).ThenInclude(i => i.Routes1)
                    .AsNoTracking()
                    .FirstOrDefault();

                var studentMapLogs = dbContext.StudentRouteStopMapLogs
                    .Where(X => X.RouteID == IID && X.AcademicYearID == _context.AcademicYearID)
                    .Include(i => i.Student)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.School)
                    .Include(i => i.Routes1)
                    .Include(i => i.Routes11)
                    .Include(i => i.RouteStopMap)
                    .Include(i => i.RouteStopMap1)
                    .Include(i => i.RouteStopMap2)
                    .OrderBy(o => o.StudentID)
                    .AsNoTracking()
                    .ToList();

                var staffMapLogs = dbContext.StaffRouteShiftMapLogs
                    .Where(X => X.ShiftFromRouteID == IID && X.AcademicYearID == _context.AcademicYearID)
                    .Include(i => i.Employee)
                    .Include(i => i.School)
                    .Include(i => i.Routes1)
                    .Include(i => i.RouteStopMap)
                    .Include(i => i.RouteStopMap1)
                    .OrderBy(o => o.StaffID)
                    .AsNoTracking()
                    .ToList();

                var routeStopMapDTO = new RouteShiftingDTO()
                {
                    RouteID = entity.RouteID,
                    Route = entity.RouteID != null ? new KeyValueDTO() { Key = entity.Routes11.RouteID.ToString(), Value = entity.Routes11.RouteCode + " - " + entity.Routes11.RouteDescription } : null,
                    RouteGroupID = entity.RouteID.HasValue ? entity.Routes11?.RouteGroupID : null,
                    ToRouteGroupID = entity.RouteStopMap1?.Routes1?.RouteGroupID,
                    AcademicYearID = entity.AcademicYearID,
                    AcademicYear = entity.AcademicYearID != null ? new KeyValueDTO() { Key = entity.AcademicYear.AcademicYearID.ToString(), Value = (string.IsNullOrEmpty(entity.AcademicYear.AcademicYearCode) ? " " : entity.AcademicYear.Description + " " + '(' + entity.AcademicYear.AcademicYearCode + ')') } : null,
                };

                #region Student Details
                foreach (var data in studentMapLogs)
                {
                    routeStopMapDTO.StudentLists.Add(new RouteShiftingStudentMapDTO()
                    {
                        //StudentRouteStopMapID = data.StudentRouteStopMapID,
                        StudentRouteStopMapLogIID = data.StudentRouteStopMapLogIID,
                        StudentID = data.StudentID,
                        Student = new KeyValueDTO() { Key = data.Student.StudentIID.ToString(), Value = (string.IsNullOrEmpty(data.Student.AdmissionNumber) ? " " : data.Student.AdmissionNumber) + '-' + data.Student.FirstName + ' ' + data.Student.MiddleName + ' ' + data.Student.LastName },
                        ClassID = data.ClassID,
                        ClassName = data.ClassID.HasValue ? data?.Class?.ClassDescription : null,
                        SectionID = data.SectionID,
                        Section = data.SectionID.HasValue ? data?.Section?.SectionName : null,
                        DateFrom = data.DateFrom,
                        DateTo = data.DateTo,
                        OldDropStop = data.OldDropStop != null ? data.OldDropStop : null,
                        OldPickUpStop = data.OldPickUpStop != null ? data.OldPickUpStop : null,
                        OldDropStopEdit = data.DropStopMapID.HasValue ? data.RouteStopMap2?.StopName : null,
                        OldPickUpStopEdit = data.PickupStopMapID.HasValue ? data.RouteStopMap1?.StopName : null,
                        PickupStopMapID = data.PickupStopMapID == null ? null : data.PickupStopMapID,
                        PickupStopMap = data.PickupStopMapID.HasValue ? data.PickupStopMapID.ToString() : null,
                        DropStopMapID = data.DropStopMapID == null ? null : data.DropStopMapID,
                        DropStopMap = data.DropStopMapID.HasValue ? data.DropStopMapID.ToString() : null,
                        CreatedBy = data.CreatedBy,
                        UpdatedBy = data.UpdatedBy,
                        CreatedDate = data.CreatedDate,
                        UpdatedDate = data.UpdatedDate,
                        //TimeStamps = data.TimeStamps == null ? null : Convert.ToBase64String(data.TimeStamps),
                    });
                }
                #endregion

                #region staff Details
                foreach (var data in staffMapLogs)
                {
                    routeStopMapDTO.StaffLists.Add(new RouteShiftingStaffMapDTO()
                    {
                        StaffRouteStopMapLogIID = data.StaffRouteStopMapLogIID,
                        //StaffRouteStopMapID = data.StaffRouteStopMapID,
                        StaffID = data.StaffID,
                        Staff = new KeyValueDTO() { Key = data.Employee.EmployeeIID.ToString(), Value = (string.IsNullOrEmpty(data.Employee.EmployeeCode) ? " " : data.Employee.EmployeeCode) + '-' + data.Employee.FirstName + ' ' + data.Employee.MiddleName + ' ' + data.Employee.LastName },
                        DateFrom = data.DateFrom,
                        DateTo = data.DateTo,
                        FromSchool = data.DropStopMapID.HasValue ? data.RouteStopMap.StopName : null,
                        ToSchool = data.PickupStopMapID.HasValue ? data.RouteStopMap1.StopName : null,
                        PickupStopMapID = data.PickupStopMapID == null ? null : data.PickupStopMapID,
                        PickupStopMap = data.PickupStopMapID.HasValue ? data.PickupStopMapID.ToString() : null,
                        DropStopMapID = data.DropStopMapID == null ? null : data.DropStopMapID,
                        DropStopMap = data.DropStopMapID.HasValue ? data.DropStopMapID.ToString() : null,
                        CreatedBy = data.CreatedBy,
                        UpdatedBy = data.UpdatedBy,
                        CreatedDate = data.CreatedDate,
                        UpdatedDate = data.UpdatedDate,
                        //TimeStamps = data.TimeStamps == null ? null : Convert.ToBase64String(data.TimeStamps),
                        OldDropStop = data.OldDropStop != null ? data.OldDropStop : null,
                        OldPickUpStop = data.OldPickUpStop != null ? data.OldPickUpStop : null,
                        OldDropStopEdit = data.DropStopMapID.HasValue ? data.RouteStopMap?.StopName : null,
                        OldPickUpStopEdit = data.PickupStopMapID.HasValue ? data.RouteStopMap1?.StopName : null,
                    });
                }
                #endregion

                return routeStopMapDTO;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as RouteShiftingDTO;

            using (var dbContext = new dbEduegateSchoolContext())
            {
                //block edit screen
                var transport_PENDING_StatusID = new Domain.Setting.SettingBL(null).GetSettingValue<long>("TRANSPORT_STATUS_PENDING_ID");

                #region Save Student Route Shifting
                if (toDto.StudentLists.Count > 0)
                {
                    foreach (var map in toDto.StudentLists)
                    {

                        if (map.StudentRouteStopMapLogIID != 0)
                        {
                            throw new Exception("Edit and save is not available for route shifting !");
                        }

                        if (map.StudentID.HasValue)
                        {
                            var studentDetails = dbContext.Students.Where(s => s.StudentIID == map.StudentID && s.AcademicYearID == toDto.AcademicYearID)
                                .AsNoTracking().FirstOrDefault();

                            if (map.DropStopMapID.HasValue && map.PickupStopMapID.HasValue)
                            {
                                map.IsOneWay = false;
                            }
                            else
                            {
                                map.IsOneWay = true;
                            }

                            if (map.PickupStopMapID != 0 || map.DropStopMapID != 0)
                            {
                                var getPickUpRouteID = dbContext.RouteStopMaps.AsNoTracking()
                                    .FirstOrDefault(r => r.RouteStopMapIID == map.PickupStopMapID);

                                var getDropRouteID = dbContext.RouteStopMaps
                                    .AsNoTracking().FirstOrDefault(r => r.RouteStopMapIID == map.DropStopMapID);

                                if (getPickUpRouteID != null && getDropRouteID != null)
                                {
                                    map.PickupRouteID = getPickUpRouteID.RouteID;
                                    map.DropStopRouteID = getDropRouteID.RouteID;
                                    map.OldDropStop = map.StudentRouteStopMapLogIID != 0 && map.OldDropStopEdit != getDropRouteID.StopName ? map.OldDropStopEdit : map.OldDropStop;
                                    map.OldPickUpStop = map.StudentRouteStopMapLogIID != 0 && map.OldPickUpStopEdit != getPickUpRouteID.StopName ? map.OldPickUpStopEdit : map.OldPickUpStop;
                                }
                                else if (getDropRouteID != null)
                                {
                                    map.DropStopRouteID = getDropRouteID.RouteID;
                                    map.OldDropStop = map.StudentRouteStopMapLogIID != 0 && map.OldDropStopEdit != getDropRouteID.StopName ? map.OldDropStopEdit : map.OldDropStop;
                                }
                                else if (getPickUpRouteID != null)
                                {
                                    map.PickupRouteID = getPickUpRouteID.RouteID;
                                    map.OldPickUpStop = map.StudentRouteStopMapLogIID != 0 && map.OldPickUpStopEdit != getPickUpRouteID.StopName ? map.OldPickUpStopEdit : map.OldPickUpStop;
                                }
                            }

                            var entity = new StudentRouteStopMap()
                            {
                                StudentID = map.StudentID,
                                AcademicYearID = toDto.AcademicYearID.HasValue ? toDto.AcademicYearID : Convert.ToInt32(_context.AcademicYearID),
                                ClassID = map.ClassID.HasValue ? map.ClassID : studentDetails?.ClassID,
                                SectionID = map.SectionID.HasValue ? map.SectionID : studentDetails?.SectionID,
                                PickupStopMapID = map.PickupStopMapID,
                                DropStopMapID = map.DropStopMapID,
                                DateFrom = map.DateFrom,
                                DateTo = map.DateTo.HasValue ? map.DateTo : null,
                                IsActive = true,
                                IsOneWay = map.IsOneWay,
                                TransportStatusID = transport_PENDING_StatusID,
                                IsRouteShifted = 1,
                                PickupRouteID = map.PickupRouteID,
                                DropStopRouteID = map.DropStopRouteID,
                                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                                CreatedBy = map.StudentRouteStopMapLogIID == 0 ? Convert.ToInt32(_context.LoginID) : toDto.CreatedBy,
                                CreatedDate = map.StudentRouteStopMapLogIID == 0 ? DateTime.Now.Date : toDto.CreatedDate,
                                UpdatedBy = map.StudentRouteStopMapLogIID != 0 ? Convert.ToInt32(_context.LoginID) : toDto.UpdatedBy,
                                UpdatedDate = map.StudentRouteStopMapLogIID != 0 ? DateTime.Now.Date : toDto.UpdatedDate,
                            };

                            dbContext.StudentRouteStopMaps.Add(entity);

                            entity.StudentRouteStopMapLogs.Add(new StudentRouteStopMapLog()
                            {
                                StudentRouteStopMapLogIID = map.StudentRouteStopMapLogIID,
                                StudentID = map.StudentID,
                                ClassID = map.ClassID.HasValue ? map.ClassID : studentDetails?.ClassID,
                                SectionID = map.SectionID.HasValue ? map.SectionID : studentDetails?.SectionID,
                                AcademicYearID = toDto.AcademicYearID.HasValue ? toDto.AcademicYearID : Convert.ToInt32(_context.AcademicYearID),
                                PickupStopMapID = map.PickupStopMapID,
                                DropStopMapID = map.DropStopMapID,
                                RouteID = toDto.RouteID,
                                DateFrom = map.DateFrom,
                                DateTo = map.DateTo.HasValue ? map.DateTo : null,
                                IsActive = true,
                                IsOneWay = map.IsOneWay,
                                TransportStatusID = transport_PENDING_StatusID,
                                IsRouteShifted = 1,
                                PickupRouteID = map.PickupRouteID,
                                DropStopRouteID = map.DropStopRouteID,
                                OldDropStop = map.OldDropStop != null ? map.OldDropStop : null,
                                OldPickUpStop = map.OldPickUpStop != null ? map.OldPickUpStop : null,
                                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                                CreatedBy = map.StudentRouteStopMapLogIID == 0 ? Convert.ToInt32(_context.LoginID) : toDto.CreatedBy,
                                CreatedDate = map.StudentRouteStopMapLogIID == 0 ? DateTime.Now.Date : toDto.CreatedDate,
                                UpdatedBy = map.StudentRouteStopMapLogIID != 0 ? Convert.ToInt32(_context.LoginID) : toDto.UpdatedBy,
                                UpdatedDate = map.StudentRouteStopMapLogIID != 0 ? DateTime.Now.Date : toDto.UpdatedDate,
                            });

                            if (map.StudentID != null && map.StudentRouteStopMapLogIID == 0)
                            {
                                using (var dbContext1 = new dbEduegateSchoolContext())
                                {
                                    var stud = dbContext1.StudentRouteStopMaps.Where(X => X.StudentID == map.StudentID && X.IsActive == true).ToList();
                                    if (stud.Count > 0 || stud != null)
                                    {
                                        foreach (var changeDat in stud)
                                        {
                                            changeDat.IsActive = false;
                                            dbContext1.Entry(changeDat).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                        }
                                        dbContext1.SaveChanges();
                                    }
                                }
                            }

                            if (map.StudentRouteStopMapLogIID != 0)
                            {
                                dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                            else
                            {
                                dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }

                            dbContext.SaveChanges();
                        }
                    }
                }
                #endregion

                #region Save Staff Route Shifting
                if (toDto.StaffLists.Count > 0)
                {
                    foreach (var staffmap in toDto.StaffLists)
                    {
                        if (staffmap.StaffRouteStopMapLogIID != 0)
                        {
                            throw new Exception("Edit and save is not available for route shifting !");
                        }

                        if (staffmap.DropStopMapID.HasValue && staffmap.PickupStopMapID.HasValue)
                        {
                            staffmap.IsOneWay = false;
                        }
                        else
                        {
                            staffmap.IsOneWay = true;
                        }

                        if (staffmap.PickupStopMapID != 0 || staffmap.DropStopMapID != 0)
                        {
                            var getPickUpRouteID = dbContext.RouteStopMaps.AsNoTracking()
                                    .FirstOrDefault(r => r.RouteStopMapIID == staffmap.PickupStopMapID);

                            var getDropRouteID = dbContext.RouteStopMaps.AsNoTracking()
                                    .FirstOrDefault(r => r.RouteStopMapIID == staffmap.PickupStopMapID);

                            if (getPickUpRouteID != null && getDropRouteID != null)
                            {
                                staffmap.PickupRouteID = getPickUpRouteID.RouteID;
                                staffmap.DropStopRouteID = getDropRouteID.RouteID;
                                staffmap.OldPickUpStop = staffmap.StaffRouteStopMapLogIID != 0 && staffmap.OldPickUpStopEdit != getPickUpRouteID.StopName ? staffmap.OldPickUpStopEdit : staffmap.OldPickUpStop;
                                staffmap.OldDropStop = staffmap.StaffRouteStopMapLogIID != 0 && staffmap.OldDropStopEdit != getDropRouteID.StopName ? staffmap.OldDropStopEdit : staffmap.OldDropStop;
                            }
                            else if (getDropRouteID != null)
                            {
                                staffmap.DropStopRouteID = getDropRouteID.RouteID;
                                staffmap.OldDropStop = staffmap.StaffRouteStopMapLogIID != 0 && staffmap.OldDropStopEdit != getDropRouteID.StopName ? staffmap.OldDropStopEdit : staffmap.OldDropStop;
                            }
                            else if (getPickUpRouteID != null)
                            {
                                staffmap.PickupRouteID = getPickUpRouteID.RouteID;
                                staffmap.OldPickUpStop = staffmap.StaffRouteStopMapLogIID != 0 && staffmap.OldPickUpStopEdit != getPickUpRouteID.StopName ? staffmap.OldPickUpStopEdit : staffmap.OldPickUpStop;
                            }
                        }

                        if (staffmap.StaffID.HasValue)
                        {
                            var staffentity = new StaffRouteStopMap()
                            {
                                //StaffRouteStopMapIID = (long)map.StaffRouteStopMapID,
                                StaffID = staffmap.StaffID,
                                AcademicYearID = toDto.AcademicYearID.HasValue ? toDto.AcademicYearID : Convert.ToInt32(_context.AcademicYearID),
                                PickupStopMapID = staffmap.PickupStopMapID,
                                DropStopMapID = staffmap.DropStopMapID,
                                DateFrom = staffmap.DateFrom,
                                DateTo = staffmap.DateTo.HasValue ? staffmap.DateTo : null,
                                IsActive = true,
                                TransportStatusID = transport_PENDING_StatusID,
                                IsRouteShifted = 1,
                                PickupRouteID = staffmap.PickupRouteID,
                                DropStopRouteID = staffmap.DropStopRouteID,
                                ShiftFromRouteID = toDto.RouteID,
                                IsOneWay = staffmap.IsOneWay,
                                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                                CreatedBy = staffmap.StaffRouteStopMapID == 0 ? Convert.ToInt32(_context.LoginID) : toDto.CreatedBy,
                                CreatedDate = staffmap.StaffRouteStopMapID == 0 ? DateTime.Now.Date : toDto.CreatedDate,
                                UpdatedBy = staffmap.StaffRouteStopMapID != 0 ? Convert.ToInt32(_context.LoginID) : toDto.UpdatedBy,
                                UpdatedDate = staffmap.StaffRouteStopMapID != 0 ? DateTime.Now.Date : toDto.UpdatedDate,
                            };

                            dbContext.StaffRouteStopMaps.Add(staffentity);

                            staffentity.StaffRouteShiftMapLogs.Add( new StaffRouteShiftMapLog()
                            {
                                StaffRouteStopMapLogIID = staffmap.StaffRouteStopMapLogIID,
                                //StaffRouteStopMapID = staffentity.StaffRouteStopMapIID != 0 ? staffentity.StaffRouteStopMapIID : staffmap.StaffRouteStopMapID,
                                StaffID = staffmap.StaffID,
                                AcademicYearID = toDto.AcademicYearID.HasValue ? toDto.AcademicYearID : Convert.ToInt32(_context.AcademicYearID),
                                PickupStopMapID = staffmap.PickupStopMapID,
                                DropStopMapID = staffmap.DropStopMapID,
                                ShiftFromRouteID = toDto.RouteID,
                                DateFrom = staffmap.DateFrom,
                                DateTo = staffmap.DateTo.HasValue ? staffmap.DateTo : null,
                                IsActive = true,
                                TransportStatusID = transport_PENDING_StatusID,
                                IsRouteShifted = 1,
                                PickupRouteID = staffmap.PickupRouteID,
                                DropStopRouteID = staffmap.DropStopRouteID,
                                OldDropStop = staffmap.OldDropStop != null ? staffmap.OldDropStop : null,
                                OldPickUpStop = staffmap.OldPickUpStop != null ? staffmap.OldPickUpStop : null,
                                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                                CreatedBy = staffmap.StaffRouteStopMapLogIID == 0 ? Convert.ToInt32(_context.LoginID) : toDto.CreatedBy,
                                CreatedDate = staffmap.StaffRouteStopMapLogIID == 0 ? DateTime.Now.Date : toDto.CreatedDate,
                                UpdatedBy = staffmap.StaffRouteStopMapLogIID != 0 ? Convert.ToInt32(_context.LoginID) : toDto.UpdatedBy,
                                UpdatedDate = staffmap.StaffRouteStopMapLogIID != 0 ? DateTime.Now.Date : toDto.UpdatedDate,
                            });

                            if (staffmap.StaffID != null && staffmap.StaffRouteStopMapLogIID == 0)
                            {
                                using (var dbContext2 = new dbEduegateSchoolContext())
                                {
                                    var staff_old = dbContext2.StaffRouteStopMaps.Where(X => X.StaffID == staffmap.StaffID && X.IsActive == true).AsNoTracking().ToList();
                                    if (staff_old != null)
                                    {
                                        foreach (var dat in staff_old)
                                        {
                                            dat.IsActive = false;
                                            dbContext2.Entry(dat).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                        }
                                        dbContext2.SaveChanges();
                                    }
                                }
                            }

                            if (staffmap.StaffRouteStopMapLogIID == 0)
                            {
                                dbContext.Entry(staffentity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(staffentity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }

                            dbContext.SaveChanges();
                        }
                    }
                }
                #endregion
                return ToDTOString(ToDTO((long)toDto.RouteID));
            }
        }

    }
}