using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using System;
using System.Linq;
using Eduegate.Services.Contracts.School.Transports;
using System.Collections.Generic;
using System.Data.SqlClient;
using Eduegate.Framework.Extensions;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Repository;
using System.Net.Mail;
using System.Net;
using Eduegate.Services.Contracts.Notifications;
using System.Globalization;
using Eduegate.Services.Contracts.Enums;
using System.Text.RegularExpressions;
using Eduegate.Services.Contracts.Enums.Notifications;
using Eduegate.Domain.Mappers.Notification;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Domain.Mappers.Notification.Notifications;

namespace Eduegate.Domain.Mappers.School.Transports
{
    public class StudentRouteStopMapMapper : DTOEntityDynamicMapper
    {
        List<string> validationFields = new List<string>() { "StudentID" };
        public static StudentRouteStopMapMapper Mapper(CallContext context)
        {
            var mapper = new StudentRouteStopMapMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StudentRouteStopMapDTO>(entity);
        }


        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private StudentRouteStopMapDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StudentRouteStopMaps.Where(x => x.StudentRouteStopMapIID == IID)
                    .Include(x => x.Student)
                    .Include(x => x.AcademicYear)
                    .Include(x => x.TransportStatus)
                    .Include(x => x.Routes1)
                    .Include(x => x.Routes11)
                    .Include(x => x.RouteStopMap1).ThenInclude(x => x.Routes1)
                    .Include(x => x.RouteStopMap2).ThenInclude(x => x.Routes1)
                    .Include(x => x.StudentRouteMonthlySplits)
                    .Include(x => x.StudentRoutePeriodMaps).ThenInclude(x => x.FeePeriod)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (entity.IsNull())
                {
                    return GetStudentTransportApplication(IID);
                }
                var routeStopMapDTO = new StudentRouteStopMapDTO()
                {
                    StudentRouteStopMapIID = entity.StudentRouteStopMapIID,
                    DateFrom = entity.DateFrom,
                    DateTo = entity.DateTo,
                    CancelDate = entity.CancelDate,
                    StudentID = entity.StudentID,
                    Student = new KeyValueDTO()
                    {
                        Key = entity.Student.StudentIID.ToString(),
                        Value = (string.IsNullOrEmpty(entity.Student.AdmissionNumber) ? " " : entity.Student.AdmissionNumber) + '-' + entity.Student.FirstName + ' ' + entity.Student.MiddleName + ' ' + entity.Student.LastName
                    },
                    AcademicYearID = entity.AcademicYearID,
                    AcademicYear = entity.AcademicYearID != null ? new KeyValueDTO()
                    {
                        Key = entity.AcademicYear.AcademicYearID.ToString(),
                        Value = string.IsNullOrEmpty(entity.AcademicYear.AcademicYearCode) ? " " : entity.AcademicYear.Description + " " + '(' + entity.AcademicYear.AcademicYearCode + ')'
                    } : new KeyValueDTO(),

                    PickupStopMapID = entity.PickupStopMapID == null ? null : entity.PickupStopMapID,
                    PickupStopMap = entity.PickupStopMapID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.PickupStopMapID.ToString(),
                        Value = entity.RouteStopMap1.Routes1.RouteCode + " - " + entity.RouteStopMap1.StopName
                    } : null,
                    PickUpRouteCode = entity.RouteStopMap1?.Routes1?.RouteCode,
                    DropStopMapID = entity.DropStopMapID == null ? null : entity.DropStopMapID,
                    DropStopMap = entity.DropStopMapID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.DropStopMapID.ToString(),
                        Value = entity.RouteStopMap2.Routes1.RouteCode + " - " + entity.RouteStopMap2.StopName
                    } : null,
                    DropRouteCode = entity.RouteStopMap2?.Routes1?.RouteCode,
                    PickupSeatMap = entity.PickupStopMapID.HasValue ? GetPickUpBusSeatAvailabiltyforEdit(entity.PickupStopMapID.Value, entity.AcademicYearID.Value) : null,
                    DropSeatMap = entity.DropStopMapID.HasValue ? GetDropBusSeatAvailabiltyforEdit(entity.DropStopMapID.Value, entity.AcademicYearID.Value) : null,
                    IsOneWay = entity.IsOneWay,
                    IsActive = entity.IsActive,
                    Termsandco = entity.IsActive == false ? null : entity.Termsandco,
                    CreatedBy = entity.StudentRouteStopMapIID == 0 ? (int)_context.LoginID : entity.CreatedBy,
                    UpdatedBy = entity.StudentRouteStopMapIID > 0 ? (int)_context.LoginID : entity.UpdatedBy,
                    CreatedDate = entity.StudentRouteStopMapIID == 0 ? DateTime.Now : entity.CreatedDate,
                    UpdatedDate = entity.StudentRouteStopMapIID > 0 ? DateTime.Now : entity.UpdatedDate,
                    ////TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                    //AcademicYearID = AcademicYearID,
                    SchoolID = entity.SchoolID,
                    TransportApplctnStudentMapID = entity.TransportApplctnStudentMapID,
                    Remarks = entity.Remarks,
                    TransporStatusID = entity.Termsandco == false ? null : entity.TransportStatusID.HasValue ? entity.TransportStatusID : null,
                    TransporStatus = entity.TransportStatusID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.TransportStatusID.ToString(),
                        Value = entity.TransportStatus.StatusName
                    } : null,
                    PickupRouteID = entity.PickupRouteID.HasValue ? entity.PickupRouteID : null,
                    PickUpRoute = entity.PickupRouteID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.PickupRouteID.ToString(),
                        Value = entity.Routes11.RouteCode + " - " + entity.Routes11.RouteDescription
                    } : null,
                    DropStopRouteID = entity.DropStopRouteID.HasValue ? entity.DropStopRouteID : null,
                    DropRoute = entity.DropStopRouteID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.DropStopRouteID.ToString(),
                        Value = entity.Routes1.RouteCode + " - " + entity.Routes1.RouteDescription
                    } : null,
                    RouteTypeID = entity.IsOneWay == true ? RouteTypeDetail(entity).RouteTypeID : null,
                    RouteTypeName = entity.IsOneWay == true ? RouteTypeDetail(entity).RouteTypeName : null,

                    RouteGroupID = entity.PickupRouteID.HasValue ? entity.Routes11?.RouteGroupID : entity.DropStopRouteID.HasValue ? entity.Routes1?.RouteGroupID : null,

                    //data bind for old data reference -- start
                    OldPickupStopMapID = entity.PickupStopMapID == null ? null : entity.PickupStopMapID,
                    OldPickupStopMap = entity.PickupStopMapID.HasValue ? entity.RouteStopMap1.Routes1.RouteCode + " - " + entity.RouteStopMap1.StopName : null,

                    OldDropStopMapID = entity.DropStopMapID == null ? null : entity.DropStopMapID,
                    OldDropStopMap = entity.DropStopMapID.HasValue ? entity.RouteStopMap2.Routes1.RouteCode + " - " + entity.RouteStopMap2.StopName : null,

                    OldPickupRouteID = entity.PickupRouteID.HasValue ? entity.PickupRouteID : null,
                    OldPickUpRoute = entity.PickupRouteID.HasValue ? entity.Routes11.RouteCode + " - " + entity.Routes11.RouteDescription : null,

                    OldDropStopRouteID = entity.DropStopRouteID.HasValue ? entity.DropStopRouteID : null,
                    OldDropRoute = entity.DropStopRouteID.HasValue ? entity.Routes1.RouteCode + " - " + entity.Routes1.RouteDescription : null,
                    //end

                };

                routeStopMapDTO.RouteType = entity.IsOneWay == true ? new KeyValueDTO()
                {
                    Key = routeStopMapDTO.RouteTypeID.ToString(),
                    Value = routeStopMapDTO.RouteTypeName
                } : new KeyValueDTO();

                routeStopMapDTO.FeePeriod = new List<KeyValueDTO>();

                foreach (var feePeriods in entity.StudentRoutePeriodMaps)
                {
                    routeStopMapDTO.FeePeriod.Add(new KeyValueDTO()
                    {
                        Value = feePeriods.FeePeriod.Description,
                        Key = feePeriods.FeePeriodID.ToString(),
                    });
                }

                routeStopMapDTO.MonthlySplitDTO = new List<StudentRouteMonthlySplitDTO>();
                foreach (var monthlySplitDTO in entity.StudentRouteMonthlySplits)
                {
                    routeStopMapDTO.MonthlySplitDTO.Add(new StudentRouteMonthlySplitDTO()
                    {
                        FeePeriodID = monthlySplitDTO.FeePeriodID,
                        MonthID = monthlySplitDTO.MonthID,
                        Year = monthlySplitDTO.Year,
                        AcademicYearID = entity.AcademicYearID,
                        DropStopMapID = entity.DropStopMapID == null ? null : entity.DropStopMapID,
                        PickupStopMapID = entity.PickupStopMapID == null ? null : entity.PickupStopMapID,
                        StudentRouteStopMapID = entity.StudentRouteStopMapIID,
                        IsRowSelected = true,
                        IsCollected = monthlySplitDTO.IsCollected.HasValue ? monthlySplitDTO.IsCollected : false,
                        IsExcluded = monthlySplitDTO.IsExcluded.HasValue ? monthlySplitDTO.IsExcluded : false

                    });
                }

                return routeStopMapDTO;
            }
        }

        public StudentRouteStopMapDTO GetStudentTransportApplication(long TransportApplctnStudentMapIID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dTO = new StudentRouteStopMapDTO();

                var studentMap = dbContext.TransportApplctnStudentMaps
                    .Where(x => x.TransportApplctnStudentMapIID == TransportApplctnStudentMapIID)
                    .Include(x => x.Student)
                    .Include(x => x.AcademicYears)
                    .AsNoTracking()
                    .FirstOrDefault();

                var applications = dbContext.TransportApplications
                    .Where(a => a.TransportApplicationIID == studentMap.TransportApplicationID)
                    .AsNoTracking()
                    .FirstOrDefault();

                dTO.StudentID = studentMap.StudentID;
                dTO.Student = new KeyValueDTO() { Key = studentMap.Student.StudentIID.ToString(), Value = (string.IsNullOrEmpty(studentMap.Student.AdmissionNumber) ? " " : studentMap.Student.AdmissionNumber) + '-' + studentMap.Student.FirstName + ' ' + studentMap.Student.MiddleName + ' ' + studentMap.Student.LastName };
                dTO.AcademicYearID = studentMap.AcademicYearID;
                dTO.AcademicYear = studentMap.AcademicYearID != null ? new KeyValueDTO() { Key = studentMap.AcademicYears.AcademicYearID.ToString(), Value = (string.IsNullOrEmpty(studentMap.AcademicYears.AcademicYearCode) ? " " : studentMap.AcademicYears.Description + " " + '(' + studentMap.AcademicYears.AcademicYearCode + ')') } : null;
                dTO.ApplicationNumber = applications.ApplicationNumber;
                dTO.TransportApplctnStudentMapID = studentMap?.TransportApplctnStudentMapIID;
                dTO.DateFrom = studentMap.StartDate;
                dTO.SchoolID = studentMap?.SchoolID;
                dTO.FeePeriod = new List<KeyValueDTO>();
                dTO.MonthlySplitDTO = new List<StudentRouteMonthlySplitDTO>();

                return dTO;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as StudentRouteStopMapDTO;

            if (toDto.MonthlySplitDTO.Count() == 0)
            {
                throw new Exception("Please select a month from the Monthly Split.");
            }

            if (toDto.DateFrom > toDto.DateTo)
            {
                throw new Exception("DateFrom cannot be greater than DateTo. Please select a valid date range.");
            }

            var routeType_ToSchool = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ROUTE_TYPEID_SCHOOL");
            var routeType_ToHome = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ROUTE_TYPEID_HOME");

            if (toDto.StudentID == null)
            {
                throw new Exception("Please fill student!");
            }

            if (toDto.StudentRouteStopMapIID == 0 && toDto.PickupSeatMap.SeatAvailability == 0 && toDto.Termsandco == false)
            {
                throw new Exception("For pickup seat is not available!");
            }

            if (toDto.StudentRouteStopMapIID == 0 && toDto.DropSeatMap.SeatAvailability == 0 && toDto.Termsandco == false)
            {
                throw new Exception("For dropping seat is not available!");
            }

            if (toDto.IsOneWay == true && toDto.RouteTypeID == null)
            {
                throw new Exception("Please fill route type !");
            }

            if (toDto.IsActive == false && toDto.Remarks == null || toDto.IsActive == false && toDto.CancelDate == null)
            {
                throw new Exception("Please fill cancel date & remarks !");
            }

            if (toDto.IsActive == false && toDto.CancelDate.HasValue && toDto.Remarks == null)
            {
                throw new Exception("Please fill remark !");
            }

            if (toDto.IsActive == true && toDto.Termsandco == true && toDto.TransporStatusID == null || toDto.IsActive == true && toDto.Termsandco == true && toDto.TransporStatusID == 1)
            {
                throw new Exception(" please fill transport status !");
            }

            if (toDto.PickupStopMapID == null && toDto.IsOneWay == false || toDto.PickupStopMapID == null && toDto.RouteTypeID == byte.Parse(routeType_ToSchool))
            {
                throw new Exception(" please fill pickup stop name !");
            }

            if (toDto.DropStopMapID == null && toDto.IsOneWay == false || toDto.DropStopMapID == null && toDto.RouteTypeID == byte.Parse(routeType_ToHome))
            {
                throw new Exception(" please fill drop stop name !");
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var chekStudent = dbContext.StudentRouteStopMaps.Where(y => y.StudentID == toDto.StudentID && y.IsActive == true).AsNoTracking().ToList();
                DateTime date = DateTime.Now;

                if (toDto.StudentRouteStopMapIID == 0 && toDto.Approve == false && chekStudent.Any(dataCheck => dataCheck != null))
                {
                    throw new Exception("Student already exists!! if you want to update, please tick the Force assign box.");
                }
                else if (toDto.StudentRouteStopMapIID == 0 && toDto.Approve == true)
                {
                    if (chekStudent != null)
                    {
                        foreach (var data in chekStudent)
                        {
                            //auto inactive alreadyexist data and create new one
                            data.DateTo = date;
                            data.IsActive = false;
                            dbContext.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            dbContext.SaveChanges();
                        }
                    }
                }

                if (toDto.DropStopMapID != 0 && toDto.IsActive == true || toDto.PickupStopMapID != 0 && toDto.IsActive == true)
                {
                    var stopData = dbContext.StudentRouteStopMaps.Where(X => X.StudentRouteStopMapIID == toDto.StudentRouteStopMapIID).AsNoTracking().FirstOrDefault();
                    if (stopData != null)
                    {
                        if (stopData.DropStopMapID != toDto.DropStopMapID && toDto.DropSeatMap.SeatAvailability <= 0 && toDto.Termsandco == false || stopData.DropStopMapID == toDto.DropStopMapID && toDto.DropSeatMap.SeatAvailability <= 0 && stopData.IsActive == false && toDto.Termsandco == null)
                        {
                            throw new Exception("For Drop Seat is not available!");
                        }
                    }
                    if (stopData != null)
                    {
                        if (stopData.PickupStopMapID != toDto.PickupStopMapID && toDto.PickupSeatMap.SeatAvailability <= 0 && toDto.Termsandco == false || stopData.DropStopMapID == toDto.DropStopMapID && toDto.DropSeatMap.SeatAvailability <= 0 && stopData.IsActive == false && toDto.Termsandco == null)
                        {
                            throw new Exception("For Pickup Seat is not available!");
                        }
                    }
                }
            }

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var academicyearid = toDto.AcademicYearID;
                var transportFeeMasterID = dbContext.FeeMasters.Where(X => X.FeeType.FeeGroup.IsTransport == true).AsNoTracking()
                    .Select(Y => Y.FeeMasterID)
                    .FirstOrDefault();

                var studDetails = dbContext.Students.Where(a => a.StudentIID == toDto.StudentID).AsNoTracking().FirstOrDefault();
                var classID = studDetails.ClassID;
                var sectionID = studDetails.SectionID;

                var entity = new StudentRouteStopMap()
                {
                    StudentRouteStopMapIID = toDto.StudentRouteStopMapIID,
                    DateFrom = toDto.DateFrom,
                    DateTo = toDto.DateTo.HasValue ? toDto.DateTo : null,
                    CancelDate = toDto.IsActive == true ? null : toDto.CancelDate,
                    StudentID = toDto.StudentID,
                    AcademicYearID = toDto.AcademicYearID,
                    DropStopMapID = toDto.DropStopMapID == null ? null : toDto.DropStopMapID,
                    PickupStopMapID = toDto.PickupStopMapID == null ? null : toDto.PickupStopMapID,
                    PickupRouteID = toDto.PickupRouteID.HasValue ? toDto.PickupRouteID : null,
                    DropStopRouteID = toDto.DropStopRouteID.HasValue ? toDto.DropStopRouteID : null,
                    Termsandco = toDto.Termsandco,
                    TransportApplctnStudentMapID = toDto.TransportApplctnStudentMapID,
                    IsOneWay = toDto.IsOneWay,
                    IsActive = toDto.IsActive,
                    Remarks = toDto.Remarks,
                    TransportStatusID = toDto.IsActive == false ? null : toDto.Termsandco == false ? 1 : toDto.TransporStatusID,
                    SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                    //AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                    ClassID = classID,
                    SectionID = sectionID,
                    CreatedBy = toDto.StudentRouteStopMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.StudentRouteStopMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.StudentRouteStopMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.StudentRouteStopMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                    ////TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                };
                entity.StudentRouteMonthlySplits = new List<StudentRouteMonthlySplit>();
                //entity.StudentRouteMonthlySplits.Add(new StudentRouteMonthlySplit()
                //{
                //    StudentRouteStopMapID = entity.StudentRouteStopMapIID,
                //    PickupStopMapID = entity.PickupStopMapID,
                //    DropStopMapID = entity.DropStopMapID,
                //    //AcademicYearID=entity.
                //    Status = true,
                //    IsExcluded = false
                //});
                entity.StudentRoutePeriodMaps = new List<StudentRoutePeriodMap>();
                foreach (var feePrd in toDto.FeePeriod)
                {
                    entity.StudentRoutePeriodMaps.Add(new StudentRoutePeriodMap()
                    {

                        FeePeriodID = int.Parse(feePrd.Key),
                        StudentRouteStopMapID = entity.StudentRouteStopMapIID
                    });
                }



                entity.StudentRouteMonthlySplits = new List<StudentRouteMonthlySplit>();
                foreach (var monthlySplitDTO in toDto.MonthlySplitDTO)
                {
                    entity.StudentRouteMonthlySplits.Add(new StudentRouteMonthlySplit()
                    {

                        FeePeriodID = monthlySplitDTO.FeePeriodID,
                        MonthID = monthlySplitDTO.MonthID,
                        Year = monthlySplitDTO.Year,
                        AcademicYearID = toDto.AcademicYearID,
                        DropStopMapID = toDto.DropStopMapID == null ? null : toDto.DropStopMapID,
                        PickupStopMapID = toDto.PickupStopMapID == null ? null : toDto.PickupStopMapID,
                        StudentRouteStopMapID = entity.StudentRouteStopMapIID,
                        IsCollected = monthlySplitDTO.IsCollected.HasValue ? monthlySplitDTO.IsCollected : false,
                        IsExcluded = monthlySplitDTO.IsExcluded.HasValue ? monthlySplitDTO.IsExcluded : false

                    }); ;
                }

                dbContext.StudentRouteStopMaps.Add(entity);
                if (entity.StudentRouteStopMapIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    var mapEntity = dbContext.StudentRoutePeriodMaps.Where(X => X.StudentRouteStopMapID == toDto.StudentRouteStopMapIID).AsNoTracking().ToList();
                    if (mapEntity != null || mapEntity.Count > 0)
                    {
                        dbContext.StudentRoutePeriodMaps.RemoveRange(mapEntity);
                    }
                    var mapMonthlyEntity = dbContext.StudentRouteMonthlySplits.Where(X => X.StudentRouteStopMapID == toDto.StudentRouteStopMapIID).AsNoTracking().ToList();
                    if (mapMonthlyEntity != null || mapMonthlyEntity.Count > 0)
                    {
                        dbContext.StudentRouteMonthlySplits.RemoveRange(mapMonthlyEntity);
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                #region Mail sending to parents and class teacher with Transport details details
                if (entity.IsActive == true)
                {
                    //if (toDto.OldPickupStopMapID != entity.PickupStopMapID || toDto.OldPickupRouteID != entity.PickupRouteID
                    //    || toDto.OldDropStopMapID != entity.DropStopMapID || toDto.OldDropStopRouteID != entity.DropStopRouteID)
                    //{
                    var emaildata = new EmailNotificationDTO();

                    var studentDetails = dbContext.Students.Where(s => s.StudentIID == entity.StudentID)
                        .Include(i => i.Parent)
                        .Include(i => i.Class)
                        .Include(i => i.Section)
                        .Include(i => i.School)
                        .AsNoTracking().FirstOrDefault();

                    var classTeacher = studentDetails != null ? dbContext.ClassClassTeacherMaps
                        .Where(x => x.ClassID == studentDetails.ClassID && x.SectionID == studentDetails.SectionID && x.AcademicYearID == _context.AcademicYearID)
                        .Include(i => i.Employee)
                        .AsNoTracking().FirstOrDefault() : null;

                    var studentRouteIds = new List<long?>
                    {
                        entity.PickupRouteID,
                        entity.DropStopRouteID
                    };

                    var vehicleMap = dbContext.AssignVehicleMaps
                        .Where(x => x.IsActive == true && studentRouteIds.Any(y => y == x.RouteID))
                        .Include(i => i.Employee)
                        .Include(i => i.Routes1)
                        .OrderByDescending(o => o.AssignVehicleMapIID)
                        .AsNoTracking()
                        .FirstOrDefault();

                    string employeeName = null;
                    string employeeWorkphone = null;
                    if (vehicleMap != null && vehicleMap.Employee != null)
                    {
                        employeeName = vehicleMap.Employee.FirstName + " " + (string.IsNullOrEmpty(vehicleMap.Employee.MiddleName) ? null : vehicleMap.Employee.MiddleName + " ") + vehicleMap.Employee.LastName;
                        employeeWorkphone = vehicleMap.Routes1?.ContactNumber ?? "-";
                    }

                    var emailTemplate = EmailTemplateMapper.Mapper(_context).GetEmailTemplateDetails(EmailTemplates.StudentTransportMail.ToString());

                    var EmailIDs = new[]
                    {
                        new { ToEmailID = studentDetails.Parent?.GaurdianEmail },
                        new { ToEmailID = classTeacher?.Employee?.WorkEmail }
                            }.ToList();

                    foreach (var email in EmailIDs)
                    {
                        if (email.ToEmailID != null)
                        {
                            toDto.EmailID = email.ToEmailID;

                            String emailDetails = "";
                            String emailSub = "";

                            emailDetails = emailTemplate?.EmailTemplate;
                            //StudentDetails
                            emailDetails = emailDetails.Replace("{AdmissionNumber}", studentDetails.AdmissionNumber);
                            emailDetails = emailDetails.Replace("{StudentName}", studentDetails.FirstName + " " + studentDetails.MiddleName + " " + studentDetails.LastName);
                            emailDetails = emailDetails.Replace("{ClassName}", studentDetails.Class?.ClassDescription);
                            emailDetails = emailDetails.Replace("{SectionName}", studentDetails.Section?.SectionName);
                            emailDetails = emailDetails.Replace("{Remarks}", toDto.Remarks);

                            //TransportDetails
                            emailDetails = emailDetails.Replace("{PickUpRoute}", toDto.PickUpRoute?.Value);
                            emailDetails = emailDetails.Replace("{PickupStopMap}", toDto.PickupStopMap?.Value);
                            emailDetails = emailDetails.Replace("{DropRoute}", toDto.DropRoute?.Value);
                            emailDetails = emailDetails.Replace("{DropStop}", toDto.DropStopMap?.Value);

                            emailDetails = emailDetails.Replace("{EmployeeName}", employeeName);
                            emailDetails = emailDetails.Replace("{EmployeeWorkphone}", employeeWorkphone);

                            emailSub = "Automatic reply: Student Transport updation";

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
                                string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(toDto.EmailID, emailDetails);

                                var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

                                string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

                                if (emailDetails != "")
                                {
                                    if (hostDet.ToLower() == "live")
                                    {
                                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(toDto.EmailID, emailSub, mailMessage, EmailTypes.TransportCreation, mailParameters);
                                    }
                                    else
                                    {
                                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSub, mailMessage, EmailTypes.TransportCreation, mailParameters);
                                    }
                                }

                            }
                            catch { }
                        }
                    }
                }
                #endregion

                //For Change Transportation Status for Pending Staff / Student Seat
                var pendingID = new Domain.Setting.SettingBL(null).GetSettingValue<long>("TRANSPORT_STATUS_PENDING_ID");
                var confrimID = new Domain.Setting.SettingBL(null).GetSettingValue<long>("TRANSPORT_STATUS_CONFIRM_ID");

                //Transport Application Status Change to Allocated When Save 
                var transportApplicationID = new Domain.Setting.SettingBL(null).GetSettingValue<long>("TRANSPORT_APPLICATION_ALLOCATED_STATUS_ID");

                if (toDto.ApplicationNumber != null)
                {
                    var getSubtData = toDto.TransportApplctnStudentMapID.HasValue ?
                        dbContext.TransportApplctnStudentMaps
                        .Where(X => X.TransportApplctnStudentMapIID == toDto.TransportApplctnStudentMapID)
                        .AsNoTracking().FirstOrDefault() : null;

                    getSubtData.TransportApplcnStatusID = (byte?)transportApplicationID;
                    dbContext.Entry(getSubtData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();
                }

                if (entity.IsActive == false)
                {
                    var studTransport = dbContext.StudentRouteStopMaps
                        .Where(X => X.PickupRouteID == entity.PickupRouteID && X.TransportStatusID == pendingID || X.DropStopRouteID == entity.DropStopRouteID && X.TransportStatusID == pendingID)
                        .AsNoTracking().FirstOrDefault();

                    if (studTransport != null)
                    {
                        if (studTransport.DropStopRouteID == toDto.DropStopRouteID && toDto.DropSeatMap.SeatAvailability <= 0 && studTransport.IsActive == true || studTransport.PickupRouteID == toDto.PickupRouteID && toDto.PickupSeatMap.SeatAvailability <= 0 && studTransport.IsActive == true)
                        {
                            studTransport.TransportStatusID = confrimID;
                        }
                        dbContext.Entry(studTransport).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }

                    var staffTransport = dbContext.StaffRouteStopMaps
                        .Where(X => X.PickupRouteID == entity.PickupRouteID && X.TransportStatusID == pendingID || X.DropStopRouteID == entity.DropStopRouteID && X.TransportStatusID == pendingID)
                        .AsNoTracking().FirstOrDefault();

                    if (staffTransport != null)
                    {
                        if (staffTransport.DropStopRouteID == toDto.DropStopRouteID && toDto.DropSeatMap.SeatAvailability <= 0 && staffTransport.IsActive == true || staffTransport.PickupRouteID == toDto.PickupRouteID && toDto.PickupSeatMap.SeatAvailability <= 0 && staffTransport.IsActive == true)
                        {
                            staffTransport.TransportStatusID = confrimID;
                        }
                        dbContext.Entry(staffTransport).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    dbContext.SaveChanges();
                }

                string message = string.Empty;
                string _sFeePeriodIds = string.Empty;
                var _sFeePeriodlist = new List<int>();
                var nextFeePeriodID = 0;
                //var feePeriodDIds = 0;
                var CurrentFeePeriodID = 0;
                var feeDueExist = new FeeDueFeeTypeMap();
                if (toDto.FeePeriod != null && toDto.FeePeriod.Any())
                {
                    _sFeePeriodlist = toDto.FeePeriod.Select(w => int.Parse(w.Key)).ToList();
                }
                var feePeriodData = dbContext.FeePeriods.Where(x => _sFeePeriodlist.Contains(x.FeePeriodID)).AsNoTracking().ToList();
                var curerntDate = toDto.DateFrom.Value;


                var currentFeePeriod = feePeriodData.FirstOrDefault(x => (x.PeriodFrom.Date <= curerntDate.Date && x.PeriodTo.Date >= curerntDate.Date));
                if (currentFeePeriod != null)
                {
                    CurrentFeePeriodID = currentFeePeriod.FeePeriodID;
                    _sFeePeriodIds = CurrentFeePeriodID.ToString();
                    feeDueExist = dbContext.FeeDueFeeTypeMaps.Where(x => x.StudentFeeDue.StudentId == toDto.StudentID && x.FeeMasterID == transportFeeMasterID && x.FeePeriodID == CurrentFeePeriodID)
                                                             .Include(x => x.StudentFeeDue).AsNoTracking().FirstOrDefault();
                }
                if (currentFeePeriod != null && feeDueExist != null)
                {
                    string pattern = @"Term (\d+)";
                    Match match = Regex.Match(currentFeePeriod.Description, pattern);
                    int nextDigit = 0;
                    if (match.Success)
                    {
                        nextDigit = int.Parse(match.Groups[1].Value);
                    }

                    foreach (var feePeriod in feePeriodData)
                    {
                        if (feePeriod.Description.Contains($"Term {nextDigit + 1}"))
                        {
                            if (feePeriod.PeriodFrom.Month == curerntDate.AddMonths(1).Month) {
                                nextFeePeriodID = feePeriod.FeePeriodID;
                                _sFeePeriodIds = nextFeePeriodID.ToString();
                                break;
                            }
                        }
                    }

                }
                if (currentFeePeriod == null || nextFeePeriodID == 0)
                {
                    DateTime nextMonth = new DateTime(curerntDate.Year, curerntDate.Month, 1).AddMonths(1);
                    var feePeriodDIds = feePeriodData.Where(x => (x.PeriodFrom.Date <= nextMonth.Date && x.PeriodTo.Date >= nextMonth.Date)).Select(x => x.FeePeriodID.ToString()).FirstOrDefault();
                    if (feePeriodDIds != null && feePeriodDIds.Count() > 0 && toDto.FeePeriod.Any())
                    {
                        _sFeePeriodIds = string.Join(",", feePeriodDIds);
                    }
                }

                var feeDueTransportID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEE_DUE_FROM_TRANSPORT");

                if ((feeDueTransportID == "1" ? true : false) == true)
                {
                    using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetSchoolConnectionString()))
                    {
                        try { conn.Open(); }
                        catch (Exception ex)
                        {
                            message = ex.Message; return "0#" + message;
                        }

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
                            sqlCommand.Parameters["@STUDENTIDs"].Value = toDto.StudentID;

                            sqlCommand.Parameters.Add(new SqlParameter("@SECTIONIDs", SqlDbType.VarChar));
                            sqlCommand.Parameters["@SECTIONIDs"].Value = string.Empty;

                            sqlCommand.Parameters.Add(new SqlParameter("@LOGINID", SqlDbType.Int));
                            sqlCommand.Parameters["@LOGINID"].Value = _context.LoginID;

                            sqlCommand.Parameters.Add(new SqlParameter("@FEEPERIODIDs", SqlDbType.VarChar));
                            sqlCommand.Parameters["@FEEPERIODIDs"].Value = _sFeePeriodIds;

                            sqlCommand.Parameters.Add(new SqlParameter("@FEEMASTERIDs", SqlDbType.VarChar));
                            sqlCommand.Parameters["@FEEMASTERIDs"].Value = transportFeeMasterID;

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

                            try
                            {
                                // Run the stored procedure.
                                message = Convert.ToString(sqlCommand.ExecuteScalar() ?? string.Empty);

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
                }

                return ToDTOString(ToDTO(entity.StudentRouteStopMapIID));
            }
        }

        public StudentRouteStopMapDTO GetPickUpBusSeatAvailabilty(long RouteStopMapId, int academicYearID)
        {
            StudentRouteStopMapDTO routeMapDTO = new StudentRouteStopMapDTO();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                routeMapDTO = (from R in dbContext.Routes1
                               join RS in dbContext.RouteStopMaps on R.RouteID equals RS.RouteID
                               join RV in dbContext.RouteVehicleMaps on R.RouteID equals RV.RouteID
                               join V in dbContext.Vehicles on RV.VehicleID equals V.VehicleIID
                               where (R.RouteTypeID != 3 && RV.IsActive == true && V.IsActive == true && RS.RouteStopMapIID == RouteStopMapId)
                               select new StudentRouteStopMapDTO()
                               {
                                   PickupStopMapID = RouteStopMapId,
                                   PickUpRouteCode = RV.Routes1.RouteCode,
                                   PickupSeatMap = new SeatingAvailabilityDTO()
                                   {
                                       AllowSeatCapacity = V.AllowSeatingCapacity == null ? 0 : V.AllowSeatingCapacity,
                                       MaximumSeatCapacity = V.MaximumSeatingCapacity == null ? 0 : V.MaximumSeatingCapacity,
                                       SeatOccupied = dbContext.StudentRouteStopMaps.Count(i => i.RouteStopMap1.RouteID == R.RouteID && i.IsActive == true && i.AcademicYearID == academicYearID)
                                        +
                                       dbContext.StaffRouteStopMaps.Count(i => i.RouteStopMap1.RouteID == R.RouteID && i.IsActive == true && i.AcademicYearID == academicYearID),

                                   },

                               }).AsNoTracking().FirstOrDefault();

                if (routeMapDTO != null && routeMapDTO.PickupSeatMap != null && routeMapDTO.PickupSeatMap.AllowSeatCapacity != null)
                {
                    routeMapDTO.PickupSeatMap.SeatOccupied = routeMapDTO.PickupSeatMap.SeatOccupied == null ? 0 : routeMapDTO.PickupSeatMap.SeatOccupied.Value;
                    routeMapDTO.PickupSeatMap.SeatAvailability = routeMapDTO.PickupSeatMap.AllowSeatCapacity.Value - routeMapDTO.PickupSeatMap.SeatOccupied.Value;
                    routeMapDTO.PickupSeatMap.SeatAvailability = routeMapDTO.PickupSeatMap.SeatAvailability < 0 ? 0 : routeMapDTO.PickupSeatMap.SeatAvailability;
                }
                else
                {
                    new StudentRouteStopMapDTO()
                    {
                        PickupSeatMap = new SeatingAvailabilityDTO()
                        {
                            AllowSeatCapacity = 0,
                            MaximumSeatCapacity = 0,
                            SeatOccupied = 0,
                            SeatAvailability = 0
                        }
                    };

                }

                return routeMapDTO;
            }
        }

        public StudentRouteStopMapDTO GetDropBusSeatAvailabilty(long RouteStopMapId, int academicYearID)
        {
            StudentRouteStopMapDTO routeMapDTO = new StudentRouteStopMapDTO();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                routeMapDTO = (from R in dbContext.Routes1
                               join RS in dbContext.RouteStopMaps on R.RouteID equals RS.RouteID
                               join RV in dbContext.RouteVehicleMaps on R.RouteID equals RV.RouteID
                               join V in dbContext.Vehicles on RV.VehicleID equals V.VehicleIID
                               where (R.RouteTypeID != 1 && RV.IsActive == true && V.IsActive == true && RS.RouteStopMapIID == RouteStopMapId)

                               select new StudentRouteStopMapDTO()
                               {
                                   DropStopMapID = RouteStopMapId,
                                   DropRouteCode = V.VehicleCode,
                                   DropSeatMap = new SeatingAvailabilityDTO()
                                   {
                                       AllowSeatCapacity = V.AllowSeatingCapacity == null ? 0 : V.AllowSeatingCapacity,
                                       MaximumSeatCapacity = V.MaximumSeatingCapacity == null ? 0 : V.MaximumSeatingCapacity,
                                       SeatOccupied = dbContext.StudentRouteStopMaps.Count(i => i.RouteStopMap2.RouteID == R.RouteID && i.IsActive == true && i.AcademicYearID == academicYearID)
                                       +
                                       dbContext.StaffRouteStopMaps.Count(i => i.RouteStopMap2.RouteID == R.RouteID && i.IsActive == true && i.AcademicYearID == academicYearID),

                                   },

                               }).AsNoTracking().FirstOrDefault();

                if (routeMapDTO != null && routeMapDTO.DropSeatMap != null && routeMapDTO.DropSeatMap.AllowSeatCapacity != null)
                {
                    routeMapDTO.DropSeatMap.SeatOccupied = routeMapDTO.DropSeatMap.SeatOccupied == null ? 0 : routeMapDTO.DropSeatMap.SeatOccupied.Value;
                    routeMapDTO.DropSeatMap.SeatAvailability = routeMapDTO.DropSeatMap.AllowSeatCapacity.Value - routeMapDTO.DropSeatMap.SeatOccupied.Value;
                    routeMapDTO.DropSeatMap.SeatAvailability = routeMapDTO.DropSeatMap.SeatAvailability < 0 ? 0 : routeMapDTO.DropSeatMap.SeatAvailability;
                }
                else
                {
                    new StudentRouteStopMapDTO()
                    {
                        DropSeatMap = new SeatingAvailabilityDTO()
                        {
                            AllowSeatCapacity = 0,
                            MaximumSeatCapacity = 0,
                            SeatOccupied = 0,
                            SeatAvailability = 0
                        }
                    };
                }

                return routeMapDTO;
            }
        }

        public SeatingAvailabilityDTO GetPickUpBusSeatAvailabiltyforEdit(long RouteStopMapId, int academicYearID)
        {
            SeatingAvailabilityDTO routeMapDTO = new SeatingAvailabilityDTO();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                routeMapDTO = (from R in dbContext.Routes1
                               join RS in dbContext.RouteStopMaps on R.RouteID equals RS.RouteID
                               join RV in dbContext.RouteVehicleMaps on R.RouteID equals RV.RouteID
                               join V in dbContext.Vehicles on RV.VehicleID equals V.VehicleIID
                               where (R.RouteTypeID != 3 && RV.IsActive == true && V.IsActive == true && RS.RouteStopMapIID == RouteStopMapId)
                               select new SeatingAvailabilityDTO()
                               {
                                   VehicleCode = V.VehicleCode != null ? V.VehicleCode : null,
                                   AllowSeatCapacity = V.AllowSeatingCapacity == null ? 0 : V.AllowSeatingCapacity,
                                   MaximumSeatCapacity = V.MaximumSeatingCapacity == null ? 0 : V.MaximumSeatingCapacity,
                                   SeatOccupied = dbContext.StudentRouteStopMaps.Count(i => i.RouteStopMap1.RouteID == R.RouteID && i.IsActive == true && i.AcademicYearID == academicYearID)
                                   +
                                   dbContext.StaffRouteStopMaps.Count(i => i.RouteStopMap1.RouteID == R.RouteID && i.IsActive == true && i.AcademicYearID == academicYearID),

                               }).AsNoTracking().FirstOrDefault();

                if (routeMapDTO != null && routeMapDTO.AllowSeatCapacity != null)
                {
                    routeMapDTO.SeatOccupied = routeMapDTO.SeatOccupied == null ? 0 : routeMapDTO.SeatOccupied.Value;
                    routeMapDTO.SeatAvailability = (routeMapDTO.AllowSeatCapacity.Value - routeMapDTO.SeatOccupied.Value);
                }
                else
                {
                    new StudentRouteStopMapDTO()
                    {
                        PickupSeatMap = new SeatingAvailabilityDTO()
                        {
                            SeatOccupied = 0,
                            SeatAvailability = 0,
                            AllowSeatCapacity = 0,
                            MaximumSeatCapacity = 0,
                        }
                    };

                }

                return routeMapDTO;
            }
        }

        public SeatingAvailabilityDTO GetDropBusSeatAvailabiltyforEdit(long RouteStopMapId, int academicYearID)
        {
            SeatingAvailabilityDTO routeMapDTO = new SeatingAvailabilityDTO();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                routeMapDTO = (from R in dbContext.Routes1
                               join RS in dbContext.RouteStopMaps on R.RouteID equals RS.RouteID
                               join RV in dbContext.RouteVehicleMaps on R.RouteID equals RV.RouteID
                               join V in dbContext.Vehicles on RV.VehicleID equals V.VehicleIID
                               where (R.RouteTypeID != 1 && RV.IsActive == true && V.IsActive == true && RS.RouteStopMapIID == RouteStopMapId)

                               select new SeatingAvailabilityDTO()
                               {
                                   VehicleCode = V.VehicleCode != null ? V.VehicleCode : null,
                                   AllowSeatCapacity = V.AllowSeatingCapacity == null ? 0 : V.AllowSeatingCapacity,
                                   MaximumSeatCapacity = V.MaximumSeatingCapacity == null ? 0 : V.MaximumSeatingCapacity,
                                   SeatOccupied = dbContext.StudentRouteStopMaps.Count(i => i.RouteStopMap2.RouteID == R.RouteID && i.IsActive == true && i.AcademicYearID == academicYearID)
                                   +
                                   dbContext.StaffRouteStopMaps.Count(i => i.RouteStopMap2.RouteID == R.RouteID && i.IsActive == true && i.AcademicYearID == academicYearID),

                               }).AsNoTracking().FirstOrDefault();

                if (routeMapDTO != null && routeMapDTO.AllowSeatCapacity != null)
                {
                    routeMapDTO.SeatOccupied = routeMapDTO.SeatOccupied == null ? 0 : routeMapDTO.SeatOccupied.Value;
                    routeMapDTO.SeatAvailability = routeMapDTO.AllowSeatCapacity.Value - routeMapDTO.SeatOccupied.Value;
                }
                else
                {
                    new StudentRouteStopMapDTO()
                    {
                        DropSeatMap = new SeatingAvailabilityDTO()
                        {
                            SeatOccupied = 0,
                            SeatAvailability = 0,
                            AllowSeatCapacity = 0,
                            MaximumSeatCapacity = 0,

                        }
                    };

                }
                return routeMapDTO;
            }

        }

        private StudentRouteStopMapDTO RouteTypeDetail(StudentRouteStopMap routeStop)
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var routeTypeID = routeStop.DropStopMapID.HasValue ? 3 : routeStop.PickupStopMapID.HasValue ? 1 : 0;
                var dtos = new StudentRouteStopMapDTO();

                var type = dbContext.RouteTypes.Where(a => a.RouteTypeID == routeTypeID).AsNoTracking().FirstOrDefault();
                if (type != null)
                {
                    dtos.RouteTypeID = type.RouteTypeID;
                    dtos.RouteTypeName = type.Description;
                    //dtos.RouteType = type.RouteTypeID != 0 ? new KeyValueDTO()
                    //{
                    //    Key = type.RouteTypeID.ToString(),
                    //    Value = type.Description
                    //} : null ; 
                }

                return dtos;
            }
        }

        public List<KeyValueDTO> GetRouteStopsByRoute(int routeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var routeStopList = new List<KeyValueDTO>();
                var entities = dbContext.RouteStopMaps.Where(X => X.RouteID == routeID && X.IsActive == true)
                    .Include(i => i.Routes1)
                    .AsNoTracking()
                    .ToList();

                foreach (var stopname in entities)
                {
                    routeStopList.Add(new KeyValueDTO
                    {
                        Key = stopname.RouteStopMapIID.ToString(),
                        Value = stopname.Routes1.RouteCode + " - " + stopname.StopName,
                    });
                }

                return routeStopList;
            }
        }

        public List<RouteShiftingStudentMapDTO> GetStudentDatasFromRouteID(int routeId)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studentList = new List<RouteShiftingStudentMapDTO>();

                var getData = dbContext.StudentRouteStopMaps
                    .Where(x => x.PickupRouteID == routeId && x.IsActive == true && x.SchoolID == _context.SchoolID || x.DropStopRouteID == routeId && x.IsActive == true && x.SchoolID == _context.SchoolID)
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.RouteStopMap1)
                    .Include(i => i.RouteStopMap2)
                    .OrderByDescending(o => o.StudentRouteStopMapIID)
                    .AsNoTracking().ToList();

                foreach (var data in getData)
                {
                    if (!studentList.Any(x => x.StudentID == data.StudentID))
                    {
                        var getLogMapID = data.StudentRouteStopMapLogs.Where(x => x.StudentID == data.StudentID)
                            .OrderByDescending(o => o.StudentRouteStopMapLogIID).LastOrDefault();

                        studentList.Add(new RouteShiftingStudentMapDTO
                        {
                            StudentID = data.StudentID,
                            StudentRouteStopMapID = data.StudentRouteStopMapIID,
                            AdmissionNumber = data.Student.AdmissionNumber,
                            ClassID = data.ClassID,
                            SectionID = data.SectionID,
                            StudentRouteStopMapLogIID = getLogMapID != null ? getLogMapID.StudentRouteStopMapLogIID : 0,
                            Student = new KeyValueDTO()
                            {
                                Key = data.Student.StudentIID.ToString(),
                                Value = (string.IsNullOrEmpty(data.Student.AdmissionNumber) ? " " : data.Student.AdmissionNumber) + '-' + data.Student.FirstName + ' ' + data.Student.MiddleName + ' ' + data.Student.LastName
                            },
                            StudentName = data.Student.AdmissionNumber + " - " + data.Student.FirstName + " " + data.Student.MiddleName + " " + data.Student.LastName,
                            OldPickUpStop = data.RouteStopMap1 != null ? data.RouteStopMap1.StopName : null,
                            OldDropStop = data.RouteStopMap2 != null ? data.RouteStopMap2.StopName : null,
                            DateFromString = data.DateFrom.HasValue ? data.DateFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                            DateToString = data.DateTo.HasValue ? data.DateTo.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        });
                    }
                }

                return studentList;
            }
        }

        public List<StudentRouteStopMapDTO> GetStudentsDetailsByRouteStopID(long routeStopMapID)
        {
            var dtos = new List<StudentRouteStopMapDTO>();

            DateTime date = DateTime.Now;

            using (var dbContext = new dbEduegateSchoolContext())
            {
                //var pickupStopDetails = dbContext.StudentRouteStopMaps.Where(p => p.PickupStopMapID == routeStopMapID && p.IsActive == true && (p.DateFrom.Value.Day >= date.Day && p.DateFrom.Value.Month >= date.Month && p.DateFrom.Value.Year >= date.Year) && (p.DateTo.Value.Day <= date.Day && p.DateTo.Value.Month <= date.Month && p.DateTo.Value.Year <= date.Year)).ToList();

                var pickupStopDetails = dbContext.StudentRouteStopMaps.Where(p => p.PickupStopMapID == routeStopMapID && p.IsActive == true)
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.RouteStopMap1)
                    .Include(i => i.RouteStopMap2)
                    .AsNoTracking().ToList();

                var dropStopDetails = dbContext.StudentRouteStopMaps.Where(d => d.DropStopMapID == routeStopMapID && d.IsActive == true)
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.RouteStopMap1)
                    .Include(i => i.RouteStopMap2)
                    .AsNoTracking().ToList();

                if (pickupStopDetails.Count > 0)
                {
                    foreach (var pickupStop in pickupStopDetails)
                    {
                        dtos.Add(new StudentRouteStopMapDTO()
                        {
                            StudentRouteStopMapIID = pickupStop.StudentRouteStopMapIID,
                            StudentID = pickupStop.StudentID,
                            AdmissionNumber = pickupStop.StudentID.HasValue ? pickupStop.Student != null ? pickupStop.Student.AdmissionNumber : null : null,
                            StudentName = pickupStop.StudentID.HasValue ? pickupStop.Student != null ? pickupStop.Student.FirstName + " " + pickupStop.Student.MiddleName + " " + pickupStop.Student.LastName : null : null,
                            Student = pickupStop.StudentID.HasValue ? pickupStop.Student != null ? new KeyValueDTO()
                            {
                                Key = pickupStop.Student.StudentIID.ToString(),
                                Value = pickupStop.Student.AdmissionNumber + " - " + pickupStop.Student.FirstName + " " + pickupStop.Student.MiddleName + " " + pickupStop.Student.LastName
                            } : new KeyValueDTO() : new KeyValueDTO(),
                            IsOneWay = pickupStop.IsOneWay,
                            IsActive = pickupStop.IsActive,
                            SchoolID = pickupStop.SchoolID,
                            AcademicYearID = pickupStop.AcademicYearID,
                            Termsandco = pickupStop.Termsandco,
                            DateFrom = pickupStop.DateFrom,
                            DateTo = pickupStop.DateTo,
                            IsPickupStop = true,
                            IsDropStop = false,
                        });
                    }
                }

                if (dropStopDetails.Count > 0)
                {
                    foreach (var dropStop in dropStopDetails)
                    {
                        dtos.Add(new StudentRouteStopMapDTO()
                        {
                            StudentRouteStopMapIID = dropStop.StudentRouteStopMapIID,
                            StudentID = dropStop.StudentID,
                            AdmissionNumber = dropStop.StudentID.HasValue ? dropStop.Student != null ? dropStop.Student.AdmissionNumber : null : null,
                            StudentName = dropStop.StudentID.HasValue ? dropStop.Student != null ? dropStop.Student.FirstName + " " + dropStop.Student.MiddleName + " " + dropStop.Student.LastName : null : null,
                            Student = dropStop.StudentID.HasValue ? dropStop.Student != null ? new KeyValueDTO()
                            {
                                Key = dropStop.Student.StudentIID.ToString(),
                                Value = dropStop.Student.AdmissionNumber + " - " + dropStop.Student.FirstName + " " + dropStop.Student.MiddleName + " " + dropStop.Student.LastName
                            } : new KeyValueDTO() : new KeyValueDTO(),
                            IsOneWay = dropStop.IsOneWay,
                            IsActive = dropStop.IsActive,
                            SchoolID = dropStop.SchoolID,
                            AcademicYearID = dropStop.AcademicYearID,
                            Termsandco = dropStop.Termsandco,
                            DateFrom = dropStop.DateFrom,
                            DateTo = dropStop.DateTo,
                            IsPickupStop = false,
                            IsDropStop = true,
                        });
                    }
                }
            }

            return dtos;
        }

        public EventTransportAllocationMapDTO GetStudentTransportDetailsByStudentID(int studentID, string IsRouteType)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dto = new EventTransportAllocationMapDTO();
                var studentData = dbContext.StudentRouteStopMaps
                    .Where(x => x.StudentID == studentID && x.SchoolID == _context.SchoolID && x.IsActive == true &&
                        (IsRouteType == "both" || (IsRouteType == "Pick" ? x.PickupRouteID != null : x.DropStopRouteID != null)))
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.RouteStopMap1)
                    .Include(i => i.RouteStopMap2)
                    .Include(i => i.Routes11)
                    .Include(i => i.Routes1)
                    .OrderByDescending(o => o.StudentRouteStopMapIID)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (studentData != null)
                {
                    dto.StudentRouteStopMapID = studentData.StudentRouteStopMapIID;
                    dto.StudentID = studentData.StudentID;
                    dto.PickupStop = studentData.RouteStopMap1?.StopName;
                    dto.DropStop = studentData.RouteStopMap2?.StopName;
                    dto.PickUpRoute = studentData.Routes11?.RouteCode;
                    dto.DropRoute = studentData.Routes1?.RouteCode;
                    dto.DateFromString = studentData.DateFrom?.ToString(dateFormat, CultureInfo.InvariantCulture);
                    dto.DateToString = studentData.DateTo?.ToString(dateFormat, CultureInfo.InvariantCulture);
                    dto.ClassID = studentData.ClassID;
                    dto.SectionID = studentData.SectionID;
                    dto.ClassSection = studentData.Student.Class.ClassDescription + " - " + studentData.Student.Section.SectionName;
                }
                else
                {
                    dto.ClassSection = "No records found in " + IsRouteType;
                }

                return dto;
            }
        }

        public List<StudentTransportDetailDTO> GetStudentTransportDetailsOLD(long studentID)
        {
            var studentsTrasnports = new List<StudentTransportDetailDTO>();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat", 0, "dd/MM/yyyy");

            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
            using (SqlCommand cmd = new SqlCommand("[schools].[GET_STUDENT_TRANSPORT_DETAILS_BY_PARENT_LOGINID]", conn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@LoginID", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@LoginID"].Value = _context.LoginID;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@StudentID"].Value = studentID;

                DataSet dt = new DataSet();
                adapter.Fill(dt);
                DataTable dataTable = null;

                if (dt.Tables.Count > 0)
                {
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        dataTable = dt.Tables[0];
                    }
                }
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        String stringDateFrom = row.ItemArray[6].ToString();
                        String stringDateTo = row.ItemArray[7].ToString();

                        DateTime? DateFrom = string.IsNullOrEmpty(stringDateFrom) ? null : Convert.ToDateTime(stringDateFrom);
                        DateTime? DateTo = string.IsNullOrEmpty(stringDateTo) ? null : Convert.ToDateTime(stringDateTo);
                        StudentTransportDetailDTO studentTransport = new StudentTransportDetailDTO();
                        studentTransport.StudentID = Convert.ToInt64(row.ItemArray[0]);
                        studentTransport.Name = row.ItemArray[1].ToString();
                        //studentTransport.PickupStopMapID = Convert.ToInt64(row.ItemArray[2]);
                        studentTransport.PickupStopMapName = row.ItemArray[3].ToString();
                        //studentTransport.DropStopMapID = Convert.ToInt64(row.ItemArray[4]);
                        studentTransport.DropStopMapName = row.ItemArray[5].ToString();
                        studentTransport.IsOneWay = Convert.ToBoolean(row.ItemArray[8]);
                        studentTransport.DateFrom = DateFrom?.ToString("dd-MM-yyyy");
                        studentTransport.DateTo = DateTo?.ToString("dd-MM-yyyy");
                        studentTransport.ClassID = Convert.ToInt64(row.ItemArray[9]);
                        studentTransport.SectionID = Convert.ToInt64(row.ItemArray[10]);
                        studentTransport.PickupRouteCode = row.ItemArray[12].ToString();
                        studentTransport.PickupStopDriverName = row.ItemArray[13].ToString();
                        studentTransport.DropStopRouteCode = row.ItemArray[14].ToString();
                        studentTransport.DropStopDriverName = row.ItemArray[15].ToString();

                        studentTransport.PickupContactNo = row.ItemArray[16].ToString();
                        studentTransport.DropContactNo = row.ItemArray[17].ToString();
                        studentTransport.StudentRouteStopMapIID = Convert.ToInt64(row.ItemArray[18]);
                        studentsTrasnports.Add(studentTransport);
                    }
                }
                return studentsTrasnports;

            }
        }

        public List<StudentTransportDetailDTO> GetStudentTransportDetails(long studentID)
        {
            var studentsTrasnports = new List<StudentTransportDetailDTO>();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat", 0, "dd/MM/yyyy");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var transport = dbContext.StudentRouteStopMaps.Where(x => x.StudentID == studentID && x.IsActive == true)
                    .Include(x => x.Student)
                    .Include(x => x.Student).ThenInclude(c => c.Class)
                    .Include(x => x.Student).ThenInclude(s => s.Section)
                    .Include(x => x.RouteStopMap1)                                                                      //Pick Stop
                    .Include(x => x.RouteStopMap2)                                                                      //Drop Stop
                    .Include(x => x.Routes11)                                                                           //Pick Route
                    .Include(x => x.Routes1)                                                                            //Drop Route
                    .Include(x => x.Routes11).ThenInclude(y => y.AssignVehicleMaps).ThenInclude(ya => ya.Employee)      //Pickup Driver
                    .Include(x => x.Routes1).ThenInclude(z => z.AssignVehicleMaps).ThenInclude(za => za.Employee)       //Drop Driver

                    .OrderByDescending(x => x.StudentRouteStopMapIID)
                    .AsNoTracking()
                    .ToList();

                if (transport.Count > 0)
                {
                    studentsTrasnports = transport.Select(x => new StudentTransportDetailDTO()
                    {
                        StudentRouteStopMapIID = x.StudentRouteStopMapIID,

                        StudentID = (long)x.StudentID,
                        AdmissionNumber = x.Student?.AdmissionNumber,
                        ClassID = (long)x.Student.ClassID,
                        SectionID = (long)x.Student.SectionID,
                        Name = x.Student.FirstName + " " + x.Student.MiddleName + " " + x.Student.LastName,

                        DateFrom = x.DateFrom?.ToString("dd-MM-yyyy"),
                        DateTo = x.DateTo?.ToString("dd-MM-yyyy"),

                        IsOneWay = x.IsOneWay == true ? true : false,
                        PickupStopMapName = x.RouteStopMap1?.StopName,
                        DropStopMapName = x.RouteStopMap2?.StopName,
                        PickupRouteCode = x.Routes11?.RouteCode,
                        DropStopRouteCode = x.Routes1?.RouteCode,

                        PickupStopDriverName = x.Routes11?.AssignVehicleMaps?.FirstOrDefault().Employee.FirstName + " " + x.Routes11?.AssignVehicleMaps?.FirstOrDefault().Employee.MiddleName + " " + x.Routes11?.AssignVehicleMaps?.FirstOrDefault().Employee.LastName,
                        DropStopDriverName = x.Routes1?.AssignVehicleMaps?.FirstOrDefault().Employee.FirstName + " " + x.Routes1?.AssignVehicleMaps?.FirstOrDefault().Employee.MiddleName + " " + x.Routes1?.AssignVehicleMaps?.FirstOrDefault().Employee.LastName,
                        PickupContactNo = x.Routes1?.ContactNumber,
                        DropContactNo = x.Routes11?.ContactNumber,
                        SchoolID = x.SchoolID,
                        SchoolShortName = x.School?.SchoolShortName,
                        Class = x.Student?.Class?.ClassDescription,
                        Section = x.Student?.Section?.SectionName,

                    }).ToList();
                }
            }
            return studentsTrasnports;
        }

    }
}