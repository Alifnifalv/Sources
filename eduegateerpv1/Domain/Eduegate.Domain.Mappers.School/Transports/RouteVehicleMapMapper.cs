using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Transports;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Domain.Mappers.School.Transports
{
    public class RouteVehicleMapMapper : DTOEntityDynamicMapper
    {
        public static RouteVehicleMapMapper Mapper(CallContext context)
        {
            var mapper = new RouteVehicleMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<RouteVehicleMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private RouteVehicleMapDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var mapList = dbContext.RouteVehicleMaps.Where(x => x.RouteVehicleMapIID == IID)
                    .Include(i => i.Routes1)
                    .Include(i => i.Vehicle)
                    .AsNoTracking()
                    .ToList();

                var routeMap = mapList.FirstOrDefault();

                var routes = new List<KeyValueDTO>();
                foreach (var Route in mapList)
                {
                    routes.Add(new KeyValueDTO()
                    {
                        Value = Route.Routes1.RouteDescription,
                        Key = Route.RouteID.ToString()
                    });
                }

                return new RouteVehicleMapDTO()
                {
                    RouteVehicleMapIID = routeMap.RouteVehicleMapIID,
                    DateFrom = routeMap.DateFrom,
                    DateTo = routeMap.DateTo,
                    Routes = routes,
                    VehicleID = routeMap.VehicleID,
                    Vehicle = new KeyValueDTO()
                    {
                        Key = routeMap.VehicleID.ToString(),
                        Value = routeMap.Vehicle.VehicleRegistrationNumber
                    },
                    IsActive = routeMap.IsActive,
                    AcademicYearID = routeMap.AcademicYearID,
                    RouteGroupID = routeMap.RouteID.HasValue ? routeMap.Routes1?.RouteGroupID : null,
                    SchoolID = routeMap.SchoolID,
                    CreatedBy = routeMap.CreatedBy,
                    UpdatedBy = routeMap.UpdatedBy,
                    CreatedDate = routeMap.CreatedDate,
                    UpdatedDate = routeMap.UpdatedDate,
                    //TimeStamps = Convert.ToBase64String(routeMap.TimeStamps),
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as RouteVehicleMapDTO;

            // Date Different Check
            if (toDto.DateFrom >= toDto.DateTo)
            {
                throw new Exception("Select Date Properlly!!");
            }

            if (toDto.VehicleID == 0 || toDto.VehicleID == null)
            {
                throw new Exception("Please Select Vehicle!");
            }

            if (toDto.Routes.Count == 0)
            {
                throw new Exception("Please Select Route!");
            }

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                Entity.School.Models.RouteVehicleMap routeMap = null;

                if (toDto.Routes.Count > 0)
                {
                    foreach (KeyValueDTO keyval in toDto.Routes)
                    {
                        var rentity = new Entity.School.Models.RouteVehicleMap()
                        {
                            RouteVehicleMapIID = toDto.RouteVehicleMapIID,
                            DateFrom = toDto.DateFrom,
                            DateTo = toDto.DateTo,
                            RouteID = int.Parse(keyval.Key),
                            VehicleID = toDto.VehicleID,
                            IsActive = toDto.IsActive,
                            SchoolID = toDto.SchoolID.HasValue ? toDto.SchoolID : (byte)_context.SchoolID,
                            AcademicYearID = GetAcademicYearIDFromRouteGroup(toDto.RouteGroupID),
                            CreatedBy = toDto.RouteVehicleMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                            UpdatedBy = toDto.RouteVehicleMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                            CreatedDate = toDto.RouteVehicleMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                            UpdatedDate = toDto.RouteVehicleMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                            //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                        };

                        routeMap = rentity;

                        if (toDto.RouteVehicleMapIID == 0)
                        {
                            //check Old same vehicle route data set to inactive
                            var existingMaps = dbContext.RouteVehicleMaps.Where(x => x.VehicleID == toDto.VehicleID && x.RouteID == routeMap.RouteID && x.IsActive == true).AsNoTracking().ToList();
                            if (existingMaps != null)
                            {
                                foreach (var map in existingMaps)
                                {
                                    map.IsActive = false;
                                    map.DateTo = DateTime.Now;

                                    dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                            }

                            dbContext.Entry(rentity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(rentity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }
                }
                dbContext.SaveChanges();

                return GetEntity(routeMap.RouteVehicleMapIID);

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

        public List<RouteVehicleMapDTO> GetRouteByVehicleID(int VehicleID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var routeVehicleList = new List<RouteVehicleMapDTO>();

                var entities = dbContext.RouteVehicleMaps.Where(a => a.VehicleID == VehicleID && a.IsActive == true)
                    .Include(i => i.Routes1)
                    .Include(i => i.Vehicle)
                    .OrderBy(a => a.Routes1.RouteCode)
                    .AsNoTracking().ToList();

                foreach (var routeSub in entities)
                {
                    routeVehicleList.Add(DTOTolist(routeSub.RouteVehicleMapIID));
                }

                return routeVehicleList;
            }
        }

        private RouteVehicleMapDTO DTOTolist(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.RouteVehicleMaps.Where(a => a.RouteVehicleMapIID == IID)
                    .Include(i => i.Routes1)
                    .Include(i => i.Vehicle)
                    .OrderBy(a => a.Routes1.RouteCode)
                    .AsNoTracking().FirstOrDefault();

                return new RouteVehicleMapDTO()
                {
                    RouteVehicleMapIID = entity.RouteVehicleMapIID,
                    Vehicle = new KeyValueDTO()
                    {
                        Key = entity.VehicleID.ToString(),
                        Value = entity.Vehicle.VehicleRegistrationNumber
                    },
                    DateFrom = entity.DateFrom,
                    DateTo = entity.DateTo,
                    RouteID = entity.RouteID,
                    IsActive = entity.IsActive
                };
            }
        }

        public List<RoutesDTO> GetRoutesByVehicleID(long vehicleID)
        {
            var dtos = new List<RoutesDTO>();
            var stopDtos = new List<RouteStopFeeDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                int? currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);

                var vehicleMapDetails = dbContext.RouteVehicleMaps
                    .Where(v => v.VehicleID == vehicleID && v.IsActive == true && v.Routes1.IsActive == true && v.Routes1.RouteGroup.IsActive == true &&
                    v.Routes1.RouteGroup.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                    .Include(i => i.Routes1).ThenInclude(i => i.RouteGroup).ThenInclude(i => i.AcademicYear)
                    .AsNoTracking().ToList();

                if (vehicleMapDetails.Count > 0)
                {
                    foreach (var vehicleMap in vehicleMapDetails)
                    {
                        var routeDetails = dbContext.Routes1.Where(b => b.RouteID == vehicleMap.RouteID && b.IsActive == true &&
                        b.RouteGroup.IsActive == true && b.RouteGroup.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                            .Include(i => i.RouteGroup).ThenInclude(i => i.AcademicYear)
                            .AsNoTracking().ToList();
                        if (routeDetails.Count > 0)
                        {
                            foreach (var route in routeDetails)
                            {
                                dtos.Add(new RoutesDTO()
                                {
                                    RouteID = route.RouteID,
                                    RouteCode = route.RouteCode,
                                    RouteDescription = route.RouteDescription,
                                    IsActive = route.IsActive,
                                });
                            }
                        }
                    }
                }
            }

            return dtos;
        }

        public List<RouteStopFeeDTO> GetRouteStopsByRouteID(long routeID)
        {
            var dtos = new List<RouteStopFeeDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var stopDetails = dbContext.RouteStopMaps.Where(s => s.RouteID == routeID && s.IsActive == true).OrderBy(x => x.SequenceNo).AsNoTracking().ToList();

                if (stopDetails.Count > 0)
                {
                    foreach (var stop in stopDetails)
                    {
                        dtos.Add(new RouteStopFeeDTO()
                        {
                            RouteStopMapIID = stop.RouteStopMapIID,
                            RouteID = stop.RouteID,
                            StopName = stop.StopName,
                            StopCode = stop.StopCode,
                            RouteFareOneWay = stop.OneWayFee,
                            RouteFareTwoWay = stop.TwoWayFee,
                            IsActive = stop.IsActive,
                            AcademicYearID = stop.AcademicYearID,
                        });
                    }
                }
            }

            return dtos;
        }

        public List<KeyValueDTO> GetVehiclesByRoute(int routeID)
        {
            var vehicles = new List<KeyValueDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var vehicleDetails = dbContext.RouteVehicleMaps.Where(s => s.RouteID == routeID && s.IsActive == true)
                    .Include(i => i.Vehicle)
                    .AsNoTracking().ToList();

                if (vehicleDetails.Count > 0)
                {
                    foreach (var veh in vehicleDetails)
                    {
                        vehicles.Add(new KeyValueDTO()
                        {
                            Key = veh.VehicleID.ToString(),
                            Value = veh.Vehicle.VehicleRegistrationNumber,
                        });
                    }
                }
            }

            return vehicles;
        }


        public List<VehicleDTO> GetVehicleDetailsWithRoutesAndStopsByEmployeeLoginID(long loginID)
        {
            var dtos = new List<VehicleDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                int? currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);

                var employeeDet = dbContext.Employees
                    .Where(e => e.LoginID == loginID && e.IsActive == true)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (employeeDet != null)
                {
                    var assignVehicleDetails = dbContext.AssignVehicleMaps
                        .Where(a => a.EmployeeID == employeeDet.EmployeeIID && a.IsActive == true &&
                                    a.Routes1.IsActive == true && a.Routes1.RouteGroup.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                        .AsNoTracking()
                        .ToList();

                    if (assignVehicleDetails.Count > 0)
                    {
                        foreach (var assign in assignVehicleDetails)
                        {
                            var vehicleMapDetails = dbContext.RouteVehicleMaps
                                .Where(v => v.RouteID == assign.RouteID && v.IsActive == true &&
                                            v.Routes1.IsActive == true && v.Routes1.RouteGroup.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                                .Include(i => i.Vehicle).ThenInclude(i => i.VehicleType)
                                .Include(i => i.Routes1).ThenInclude(i => i.RouteGroup).ThenInclude(i => i.AcademicYear)
                                .AsNoTracking()
                                .ToList();

                            if (vehicleMapDetails != null && vehicleMapDetails.Count > 0)
                            {
                                foreach (var vehicleMap in vehicleMapDetails)
                                {
                                    var vehicleDet = vehicleMap.Vehicle.IsActive == true ? vehicleMap.Vehicle : null;

                                    if (vehicleDet != null)
                                    {
                                        var routeDetails = dbContext.Routes1
                                            .Where(b => b.RouteID == vehicleMap.RouteID && b.IsActive == true &&
                                                        b.RouteGroup.IsActive == true && b.RouteGroup.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                                            .AsNoTracking()
                                            .ToList();

                                        var routesDto = new List<RoutesDTO>();

                                        foreach (var route in routeDetails)
                                        {
                                            var stopDtos = dbContext.RouteStopMaps
                                                .Where(s => s.RouteID == route.RouteID && s.IsActive == true)
                                                .OrderBy(x => x.SequenceNo)
                                                .AsNoTracking()
                                                .Select(stop => new RouteStopFeeDTO
                                                {
                                                    RouteStopMapIID = stop.RouteStopMapIID,
                                                    RouteID = stop.RouteID,
                                                    StopName = stop.StopName,
                                                    StopCode = stop.StopCode,
                                                    RouteFareOneWay = stop.OneWayFee,
                                                    RouteFareTwoWay = stop.TwoWayFee,
                                                    IsActive = stop.IsActive,
                                                    AcademicYearID = stop.AcademicYearID,
                                                }).ToList();

                                            routesDto.Add(new RoutesDTO()
                                            {
                                                RouteID = route.RouteID,
                                                RouteCode = route.RouteCode,
                                                RouteDescription = route.RouteDescription,
                                                IsActive = route.IsActive,
                                                Stops = stopDtos
                                            });
                                        }

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
                                            RoutesDetails = routesDto
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


    }
}