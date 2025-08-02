using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Transports;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data;
using System.Globalization;

namespace Eduegate.Domain.Mappers.School.Transports
{
    public class StaffRouteStopMapMapper : DTOEntityDynamicMapper
    {
        List<string> validationFields = new List<string>() { "StaffID" };
        public static StaffRouteStopMapMapper Mapper(CallContext context)
        {
            var mapper = new StaffRouteStopMapMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StaffRouteStopMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private StaffRouteStopMapDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StaffRouteStopMaps.Where(x => x.StaffRouteStopMapIID == IID)
                    .Include(x => x.Employee)
                    .Include(x => x.AcademicYear)
                    .Include(x => x.RouteStopMap1).ThenInclude(i => i.Routes1)
                    .Include(x => x.RouteStopMap2).ThenInclude(i => i.Routes1)
                    .Include(x => x.Routes1)
                    .Include(x => x.Routes11)
                    .Include(x => x.TransportStatus)
                    .AsNoTracking()
                    .FirstOrDefault();

                var routeStopMapDTO = new StaffRouteStopMapDTO()
                {
                    StaffRouteStopMapIID = entity.StaffRouteStopMapIID,
                    DateFrom = entity.DateFrom,
                    DateTo = entity.DateTo,
                    StaffID = entity.StaffID,
                    StaffName = entity.Employee != null ? (string.IsNullOrEmpty(entity.Employee.EmployeeCode) ? " " : entity.Employee.EmployeeCode) + " - " + entity.Employee.FirstName + " " + entity.Employee.MiddleName + " " + entity.Employee.LastName : null,
                    AcademicYearID = entity.AcademicYearID,
                    Academicyear = entity.AcademicYearID != null ? new KeyValueDTO() { Key = entity.AcademicYear.AcademicYearID.ToString(), Value = (string.IsNullOrEmpty(entity.AcademicYear.AcademicYearCode) ? " " : entity.AcademicYear.Description + " " + '(' + entity.AcademicYear.AcademicYearCode + ')') } : null,
                    PickupStopMapID = entity.PickupStopMapID == null ? null : entity.PickupStopMapID,
                    PickupStopMap = entity.PickupStopMapID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.PickupStopMapID.ToString(),
                        Value = entity.RouteStopMap1.Routes1.RouteCode + '-' + entity.RouteStopMap1.StopName
                    } : null,
                    //PickUpRouteCode = entity == null || entity.RouteStopMap == null || entity.RouteStopMap.Routes1 == null || entity.RouteStopMap.Routes1.RouteCode == null ? null : entity.RouteStopMap.Routes1.RouteCode,

                    DropStopMapID = entity.DropStopMapID == null ? null : entity.DropStopMapID,
                    DropStopMap = entity.DropStopMapID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.DropStopMapID.ToString(),
                        Value = entity.RouteStopMap2.Routes1.RouteCode + '-' + entity.RouteStopMap2.StopName
                    } : null,
                    //DropRouteCode = entity == null || entity.RouteStopMap1 == null || entity.RouteStopMap1.Routes1 == null || entity.RouteStopMap1.Routes1.RouteCode == null ? null : entity.RouteStopMap1.Routes1.RouteCode,
                    PickupSeatMap = entity.PickupStopMapID.HasValue ? GetPickUpBusSeatAvailabiltyforEdits(entity.PickupStopMapID.Value, entity.AcademicYearID.Value) : null,
                    DropSeatMap = entity.DropStopMapID.HasValue ? GetDropBusSeatAvailabiltyforEdits(entity.DropStopMapID.Value, entity.AcademicYearID.Value) : null,
                    IsOneWay = entity.IsOneWay,
                    IsActive = entity.IsActive,
                    TermsAndConditions = entity.IsActive == false ? null : entity.TermsAndConditions,
                    CancelDate = entity.CancelDate,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    PickupRouteID = entity.PickupRouteID.HasValue ? entity.PickupRouteID : null,
                    PickUpRoute = entity.PickupRouteID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.PickupRouteID.ToString(),
                        Value = entity.Routes1.RouteCode + " - " + entity.Routes1.RouteDescription
                    } : null,
                    DropStopRouteID = entity.DropStopRouteID.HasValue ? entity.DropStopRouteID : null,
                    DropRoute = entity.DropStopRouteID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.DropStopRouteID.ToString(),
                        Value = entity.Routes11.RouteCode + " - " + entity.Routes11.RouteDescription
                    } : null,
                    RouteGroupID = entity.PickupRouteID.HasValue ? entity.Routes1?.RouteGroupID : entity.DropStopRouteID.HasValue ? entity.Routes11?.RouteGroupID : null,
                    TransporStatusID = entity.TermsAndConditions == false ? null : entity.TransportStatusID.HasValue ? entity.TransportStatusID : null,
                    TransporStatus = entity.TransportStatusID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.TransportStatusID.ToString(),
                        Value = entity.TransportStatus.StatusName
                    } : null,
                    ////TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                    RouteTypeID = entity.IsOneWay == true ? RouteTypeDetails(entity).RouteTypeID : null,
                    RouteTypeName = entity.IsOneWay == true ? RouteTypeDetails(entity).RouteTypeName : null,
                };

                routeStopMapDTO.RouteType = entity.IsOneWay == true ? new KeyValueDTO()
                {
                    Key = routeStopMapDTO.RouteTypeID.ToString(),
                    Value = routeStopMapDTO.RouteTypeName
                } : null;

                return routeStopMapDTO;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as StaffRouteStopMapDTO;
            // Date Different Check
            if (toDto.DateFrom >= toDto.DateTo)
            {
                throw new Exception("Select Date Properlly!!");
            }
            if (toDto.StaffID == null)
            {
                throw new Exception("please fill required fields!");
            }
            if (toDto.StaffRouteStopMapIID == 0 && toDto.PickupSeatMap.SeatAvailability == 0 && toDto.TermsAndConditions == false)
            {
                throw new Exception("For Pickup Seat is not available!");
            }
            if (toDto.StaffRouteStopMapIID == 0 && toDto.DropSeatMap.SeatAvailability == 0 && toDto.TermsAndConditions == false)
            {
                throw new Exception("For Dropping Seat is not available!");
            }

            if (toDto.IsOneWay == true && toDto.RouteTypeID == null)
            {
                throw new Exception(" please fill Route Type !");
            }

            if (toDto.IsActive == true && toDto.TermsAndConditions == true && toDto.TransporStatusID == null || toDto.IsActive == true && toDto.TermsAndConditions == true && toDto.TransporStatusID == 1)
            {
                throw new Exception(" please fill Transport Status !");
            }

            if (toDto.PickupStopMapID == null && toDto.IsOneWay == false || toDto.PickupStopMapID == null && toDto.RouteTypeID == 1)
            {
                throw new Exception(" please fill Pick Up Stop Name !");
            }

            if (toDto.DropStopMapID == null && toDto.IsOneWay == false || toDto.DropStopMapID == null && toDto.RouteTypeID == 2)
            {
                throw new Exception(" please fill Drop Stop Name !");
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (toDto.StaffRouteStopMapIID == 0 && toDto.Approve == false)
                {
                    var chekStaff = dbContext.StaffRouteStopMaps.Where(y => y.StaffID == toDto.StaffID && y.IsActive == true).AsNoTracking().FirstOrDefault();
                    if (chekStaff != null)
                    {
                        throw new Exception("Staff already exists!! if you want to update, please tick the Force assign box.");
                    }
                }
                if (toDto.StaffRouteStopMapIID == 0)
                {
                    var staffCheck = dbContext.StaffRouteStopMaps.Where(x => x.StaffID == toDto.StaffID && x.IsActive == true).AsNoTracking().ToList();

                    if (staffCheck != null)
                    {
                        foreach (var data in staffCheck)
                        {
                            //auto inactive alreadyexist data and create new one
                            data.IsActive = false;
                            data.DateTo = DateTime.Now;
                            dbContext.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            dbContext.SaveChanges();
                        }
                    }
                }

                if (toDto.DropStopMapID != 0 && toDto.IsActive == true || toDto.PickupStopMapID != 0 && toDto.IsActive == true)
                {
                    var stopData = dbContext.StaffRouteStopMaps.Where(X => X.StaffRouteStopMapIID == toDto.StaffRouteStopMapIID).AsNoTracking().FirstOrDefault();
                    if (stopData != null)
                    {
                        if (stopData.DropStopMapID != toDto.DropStopMapID && toDto.DropSeatMap.SeatAvailability <= 0 && toDto.TermsAndConditions == false || stopData.DropStopMapID == toDto.DropStopMapID && toDto.DropSeatMap.SeatAvailability <= 0 && stopData.IsActive == false && toDto.TermsAndConditions == null)
                        {
                            throw new Exception("For Drop Seat is not available!");
                        }
                    }
                    if (stopData != null)
                    {
                        if (stopData.PickupStopMapID != toDto.PickupStopMapID && toDto.PickupSeatMap.SeatAvailability <= 0 && toDto.TermsAndConditions == false || stopData.DropStopMapID == toDto.DropStopMapID && toDto.DropSeatMap.SeatAvailability <= 0 && stopData.IsActive == false && toDto.TermsAndConditions == null)
                        {
                            throw new Exception("For Pickup Seat is not available!");
                        }
                    }
                }
            }

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new StaffRouteStopMap()
                {
                    StaffRouteStopMapIID = toDto.StaffRouteStopMapIID,
                    DateFrom = toDto.DateFrom,
                    DateTo = toDto.DateTo,
                    StaffID = toDto.StaffID,
                    DropStopMapID = toDto.DropStopMapID,
                    PickupStopMapID = toDto.PickupStopMapID,
                    PickupRouteID = toDto.PickupRouteID.HasValue ? toDto.PickupRouteID : null,
                    DropStopRouteID = toDto.DropStopRouteID.HasValue ? toDto.DropStopRouteID : null,
                    IsOneWay = toDto.IsOneWay,
                    IsActive = toDto.IsActive,
                    TransportStatusID = toDto.IsActive == false ? null : toDto.TermsAndConditions == false ? 1 : toDto.TransporStatusID,
                    SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                    AcademicYearID = toDto.AcademicYearID,
                    TermsAndConditions = toDto.TermsAndConditions,
                    CancelDate = toDto.IsActive == true ? null : toDto.CancelDate,
                    CreatedBy = toDto.StaffRouteStopMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.StaffRouteStopMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.StaffRouteStopMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.StaffRouteStopMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                    ////TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                };

                dbContext.StaffRouteStopMaps.Add(entity);
                if (entity.StaffRouteStopMapIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                //For Change Transportation Status for Pending Staff / Student Seat
                var settingValue = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSPORT_STATUS_PENDING_ID");
                var pendingID = Convert.ToInt64(settingValue);
                var settingValue2 = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSPORT_STATUS_CONFIRM_ID");
                var confrimID = Convert.ToInt64(settingValue2);

                if (entity.IsActive == false)
                {
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
                    var studTransport = dbContext.StudentRouteStopMaps.Where(X => X.PickupRouteID == entity.PickupRouteID && X.TransportStatusID == pendingID || X.DropStopRouteID == entity.DropStopRouteID && X.TransportStatusID == pendingID).AsNoTracking().FirstOrDefault();
                    if (studTransport != null)
                    {
                        if (studTransport.DropStopRouteID == toDto.DropStopRouteID && toDto.DropSeatMap.SeatAvailability <= 0 && studTransport.IsActive == true || studTransport.PickupRouteID == toDto.PickupRouteID && toDto.PickupSeatMap.SeatAvailability <= 0 && studTransport.IsActive == true)
                        {
                            studTransport.TransportStatusID = confrimID;
                        }
                        dbContext.Entry(studTransport).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    dbContext.SaveChanges();
                }

                //return GetEntity(entity.AssignVehicleMapIID);
                return ToDTOString(ToDTO(entity.StaffRouteStopMapIID));
            }
        }

        public StaffRouteStopMapDTO GetPickUpBusSeatStaffAvailabilty(long RouteStopMapId, int academicYearID)
        {
            StaffRouteStopMapDTO routeMapDTO = new StaffRouteStopMapDTO();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                routeMapDTO = (from R in dbContext.Routes1
                               join RS in dbContext.RouteStopMaps on R.RouteID equals RS.RouteID
                               join RV in dbContext.RouteVehicleMaps on R.RouteID equals RV.RouteID
                               join V in dbContext.Vehicles on RV.VehicleID equals V.VehicleIID
                               where (R.RouteTypeID != 3 && RV.IsActive == true && V.IsActive == true && RS.RouteStopMapIID == RouteStopMapId)
                               select new StaffRouteStopMapDTO()
                               {
                                   PickupStopMapID = RouteStopMapId,
                                   PickUpRouteCode = RV.Routes1.RouteCode,
                                   PickupSeatMap = new SeatingAvailabilityDTO()
                                   {
                                       AllowSeatCapacity = V.AllowSeatingCapacity == null ? 0 : V.AllowSeatingCapacity,
                                       MaximumSeatCapacity = V.MaximumSeatingCapacity == null ? 0 : V.MaximumSeatingCapacity,
                                       SeatOccupied = dbContext.StaffRouteStopMaps.Count(i => i.RouteStopMap1.RouteID == R.RouteID && i.IsActive == true && i.AcademicYearID == academicYearID)
                                       +
                                       dbContext.StudentRouteStopMaps.Count(i => i.RouteStopMap1.RouteID == R.RouteID && i.IsActive == true && i.AcademicYearID == academicYearID),
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
                    new StaffRouteStopMapDTO()
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

        public StaffRouteStopMapDTO GetDropBusSeatStaffAvailabilty(long RouteStopMapId, int academicYearID)
        {
            StaffRouteStopMapDTO routeMapDTO = new StaffRouteStopMapDTO();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                routeMapDTO = (from R in dbContext.Routes1
                               join RS in dbContext.RouteStopMaps on R.RouteID equals RS.RouteID
                               join RV in dbContext.RouteVehicleMaps on R.RouteID equals RV.RouteID
                               join V in dbContext.Vehicles on RV.VehicleID equals V.VehicleIID
                               where (R.RouteTypeID != 1 && RV.IsActive == true && V.IsActive == true && RS.RouteStopMapIID == RouteStopMapId)

                               select new StaffRouteStopMapDTO()
                               {
                                   DropStopMapID = RouteStopMapId,
                                   DropRouteCode = V.VehicleCode,
                                   DropSeatMap = new SeatingAvailabilityDTO()
                                   {
                                       AllowSeatCapacity = V.AllowSeatingCapacity == null ? 0 : V.AllowSeatingCapacity,
                                       MaximumSeatCapacity = V.MaximumSeatingCapacity == null ? 0 : V.MaximumSeatingCapacity,
                                       SeatOccupied = dbContext.StaffRouteStopMaps.Count(i => i.RouteStopMap2.RouteID == R.RouteID && i.IsActive == true && i.AcademicYearID == academicYearID)
                                       +
                                       dbContext.StudentRouteStopMaps.Count(i => i.RouteStopMap2.RouteID == R.RouteID && i.IsActive == true && i.AcademicYearID == academicYearID),

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
                    new StaffRouteStopMapDTO()
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

        public SeatingAvailabilityDTO GetPickUpBusSeatAvailabiltyforEdits(long RouteStopMapId, int academicYearID)
        {
            SeatingAvailabilityDTO routeMapDTO = new SeatingAvailabilityDTO();
            //var sampleone = from srsm in dbContext.StudentRouteStopMaps
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
                                   SeatOccupied = dbContext.StaffRouteStopMaps.Count(i => i.RouteStopMap1.RouteID == R.RouteID && i.IsActive == true && i.AcademicYearID == academicYearID)
                                   +
                                   dbContext.StudentRouteStopMaps.Count(i => i.RouteStopMap1.RouteID == R.RouteID && i.IsActive == true && i.AcademicYearID == academicYearID),


                               }).AsNoTracking().FirstOrDefault();

                if (routeMapDTO != null && routeMapDTO.AllowSeatCapacity != null)
                {
                    routeMapDTO.SeatOccupied = routeMapDTO.SeatOccupied == null ? 0 : routeMapDTO.SeatOccupied.Value;
                    routeMapDTO.SeatAvailability = (routeMapDTO.AllowSeatCapacity.Value - routeMapDTO.SeatOccupied.Value);
                }
                else
                {
                    new StaffRouteStopMapDTO()
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

        public SeatingAvailabilityDTO GetDropBusSeatAvailabiltyforEdits(long RouteStopMapId, int academicYearID)
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
                                   SeatOccupied = dbContext.StaffRouteStopMaps.Count(i => i.RouteStopMap2.RouteID == R.RouteID && i.IsActive == true && i.AcademicYearID == academicYearID)
                                   +
                                   dbContext.StudentRouteStopMaps.Count(i => i.RouteStopMap2.RouteID == R.RouteID && i.IsActive == true && i.AcademicYearID == academicYearID),

                               }).AsNoTracking().FirstOrDefault();

                if (routeMapDTO != null && routeMapDTO.AllowSeatCapacity != null)
                {
                    routeMapDTO.SeatOccupied = routeMapDTO.SeatOccupied == null ? 0 : routeMapDTO.SeatOccupied.Value;
                    routeMapDTO.SeatAvailability = routeMapDTO.AllowSeatCapacity.Value - routeMapDTO.SeatOccupied.Value;
                }
                else
                {
                    new StaffRouteStopMapDTO()
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

        private StaffRouteStopMapDTO RouteTypeDetails(StaffRouteStopMap routeStop)
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var routeTypeID = routeStop.DropStopMapID.HasValue ? 3 : routeStop.PickupRouteID.HasValue ? 1 : 0;
                var dtos = new StaffRouteStopMapDTO();

                var type = dbContext.RouteTypes.Where(x => x.RouteTypeID == routeTypeID).AsNoTracking().FirstOrDefault();
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

        public List<RouteShiftingStaffMapDTO> GetStaffDatasFromRouteID(int routeId)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var staffList = new List<RouteShiftingStaffMapDTO>();

                var getData = dbContext.StaffRouteStopMaps
                    .Where(x => x.PickupRouteID == routeId && x.IsActive == true && x.SchoolID == _context.SchoolID || x.DropStopRouteID == routeId && x.IsActive == true && x.SchoolID == _context.SchoolID)
                    .Include(i => i.Employee)
                    .Include(i => i.RouteStopMap1)
                    .Include(i => i.RouteStopMap2)
                    .OrderByDescending(o => o.StaffRouteStopMapIID).AsNoTracking().ToList();

                foreach (var data in getData)
                {
                    if (!staffList.Any(x => x.StaffID == data.StaffID))
                    {
                        staffList.Add(new RouteShiftingStaffMapDTO
                        {
                            StaffID = data.StaffID,
                            StaffRouteStopMapID = 0,
                            EmployeeCode = data.Employee.EmployeeCode,
                            Staff = new KeyValueDTO()
                            {
                                Key = data.Employee.EmployeeIID.ToString(),
                                Value = (string.IsNullOrEmpty(data.Employee.EmployeeCode) ? " " : data.Employee.EmployeeCode) + '-' + data.Employee.FirstName + ' ' + data.Employee.MiddleName + ' ' + data.Employee.LastName
                            },
                            EmployeeName = data.Employee.EmployeeCode + " - " + data.Employee.FirstName + " " + data.Employee.MiddleName + " " + data.Employee.LastName,
                            OldPickUpStop = data.RouteStopMap1 != null ? data.RouteStopMap1.StopName : null,
                            OldDropStop = data.RouteStopMap2 != null ? data.RouteStopMap2.StopName : null,
                            DateFromString = data.DateFrom.HasValue ? data.DateFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                            DateToString = data.DateTo.HasValue ? data.DateTo.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        });
                    }
                }

                return staffList;
            }
        }

        public EventTransportAllocationMapDTO GetStaffTransportDetailsByStaffID(int staffID, string IsRouteType)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dto = new EventTransportAllocationMapDTO();
                var staffData = dbContext.StaffRouteStopMaps
                    .Where(x => x.StaffID == staffID && x.IsActive == true &&
                        (IsRouteType == "both" || (IsRouteType == "Pick" ? x.PickupRouteID != null : x.DropStopRouteID != null)))
                    .Include(i => i.RouteStopMap1)
                    .Include(i => i.RouteStopMap2)
                    .Include(i => i.Routes1)
                    .Include(i => i.Routes11)
                    .Include(i => i.Employee).ThenInclude(i => i.Designation)
                    .OrderByDescending(o => o.StaffRouteStopMapIID)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (staffData != null)
                {
                    dto.StaffRouteStopMapID = staffData.StaffRouteStopMapIID;
                    dto.StaffID = staffData.StaffID;
                    dto.PickupStop = staffData.RouteStopMap1?.StopName;
                    dto.DropStop = staffData.RouteStopMap2?.StopName;
                    dto.PickUpRoute = staffData.Routes1?.RouteCode;
                    dto.DropRoute = staffData.Routes11?.RouteCode;
                    dto.DateFromString = staffData.DateFrom?.ToString(dateFormat, CultureInfo.InvariantCulture);
                    dto.DateToString = staffData.DateTo?.ToString(dateFormat, CultureInfo.InvariantCulture);
                    dto.Designation = staffData.Employee?.Designation?.DesignationName;
                }
                else
                {
                    dto.Designation = "No records found in " + IsRouteType;
                }

                return dto;
            }
        }

    }
}