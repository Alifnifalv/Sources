using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Services.Contracts.School.Transports;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Transports
{
    public class RouteGroupMapper : DTOEntityDynamicMapper
    {
        public static RouteGroupMapper Mapper(CallContext context)
        {
            var mapper = new RouteGroupMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<RouteGroupDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }
        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private RouteGroupDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.RouteGroups
                    .Include(i => i.AcademicYear)
                    .AsNoTracking()
                    .FirstOrDefault(X => X.RouteGroupID == IID);

                var dto = new RouteGroupDTO()
                {
                    RouteGroupID = entity.RouteGroupID,
                    Description = entity.Description,
                    SchoolID = entity.SchoolID,
                    AcademicYearID = entity.AcademicYearID,
                    AcademicYear = entity.AcademicYearID.HasValue ? new KeyValueDTO() { Key = entity.AcademicYearID.ToString(), Value = entity.AcademicYear?.Description + " (" + entity.AcademicYear?.AcademicYearCode?.ToString() + ")" } : new KeyValueDTO(),
                    IsActive = entity.IsActive,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                };

                return dto;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as RouteGroupDTO;
            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (toDto.IsActive == true)
                {
                    var groupData = dbContext.RouteGroups.Where(g => g.SchoolID == toDto.SchoolID && g.AcademicYearID == toDto.AcademicYearID && g.IsActive == true)
                        .AsNoTracking()
                        .FirstOrDefault();

                    if (groupData != null && groupData.RouteGroupID != toDto.RouteGroupID)
                    {
                        throw new Exception("Active group already exist in same academic year!");
                    }
                }

                //convert the dto to entity and pass to the repository.
                var entity = new RouteGroup()
                {
                    RouteGroupID = toDto.RouteGroupID,
                    Description = toDto.Description,
                    SchoolID = toDto.SchoolID,
                    AcademicYearID = toDto.AcademicYearID,
                    IsActive = toDto.IsActive,
                    CreatedBy = toDto.RouteGroupID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.RouteGroupID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.RouteGroupID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.RouteGroupID > 0 ? DateTime.Now : dto.UpdatedDate,
                };

                dbContext.RouteGroups.Add(entity);

                if (entity.RouteGroupID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.RouteGroupID));
            }
        }

        public override bool DeleteCRUDData(long screenID, long IID)
        {
            return base.DeleteCRUDData(screenID, IID);
        }

        public override long Clone(long screenID, long IID)
        {
            var data = CopyAndInsertRelatedTableDatas(IID);
            return data;
        }

        public long CopyAndInsertRelatedTableDatas(long groupID)
        {
            var routeGroupEntity = new RouteGroup();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                routeGroupEntity = GetGroupData(groupID);

                dbContext.RouteGroups.Add(routeGroupEntity);

                dbContext.Entry(routeGroupEntity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                dbContext.SaveChanges();
            }

            return routeGroupEntity != null ? routeGroupEntity.RouteGroupID : 0;
        }

        #region Bind tables data
        public RouteGroup GetGroupData(long groupID)
        {
            var routeGroup = new RouteGroup();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var routeGroupData = dbContext.RouteGroups
                    .Include(i => i.Routes1).ThenInclude(i => i.RouteStopMaps)
                    .Include(i => i.Routes1).ThenInclude(i => i.RouteVehicleMaps)
                    .Include(i => i.Routes1).ThenInclude(i => i.AssignVehicleMaps)
                    .AsNoTracking()
                    .FirstOrDefault(g => g.RouteGroupID == groupID);

                if (routeGroupData != null)
                {
                    //Get route details under the group
                    var routeList = GetRoutesData(routeGroupData);

                    routeGroup = new RouteGroup()
                    {
                        Description = routeGroupData.Description,
                        SchoolID = routeGroupData.SchoolID,
                        AcademicYearID = routeGroupData.AcademicYearID,
                        IsActive = false,
                        CreatedBy = Convert.ToInt32(_context.LoginID),
                        CreatedDate = DateTime.Now,
                        Routes1 = routeList,
                    };
                }
            }

            return routeGroup;
        }

        public List<Routes1> GetRoutesData(RouteGroup groupData)
        {
            var routes = new List<Routes1>();

            if (groupData.Routes1 != null && groupData.Routes1.Count > 0)
            {
                var filterActiveRoutes = groupData.Routes1.Where(r => r.IsActive == true).ToList();

                if (filterActiveRoutes != null && filterActiveRoutes.Count > 0)
                {
                    foreach (var route in filterActiveRoutes)
                    {
                        //Stop details
                        var routeStopMapList = GetRouteStopMapsData(route);

                        //Vehicle map for routes
                        var routeVehicleMapList = GetRouteVehicleMapsData(route);

                        //Driver or attendar map
                        var assignVehicleMapList = GetDriverAttendarVehicleMapsData(route);

                        routes.Add(new Routes1()
                        {
                            RouteCode = route.RouteCode,
                            RouteDescription = route.RouteDescription,
                            RouteTypeID = route.RouteTypeID,
                            RouteFareOneWay = route.RouteFareOneWay,
                            RouteFareTwoWay = route.RouteFareTwoWay,
                            SchoolID = route.SchoolID,
                            AcademicYearID = route.AcademicYearID,
                            IsActive = route.IsActive,
                            CostCenterID = route.CostCenterID,
                            Landmark = route.Landmark,
                            ContactNumber = route.ContactNumber,
                            CreatedBy = Convert.ToInt32(_context.LoginID),
                            CreatedDate = DateTime.Now,
                            RouteStopMaps = routeStopMapList,
                            RouteVehicleMaps = routeVehicleMapList,
                            AssignVehicleMaps = assignVehicleMapList,
                        });

                    }
                }
            }

            return routes;
        }

        public List<RouteStopMap> GetRouteStopMapsData(Routes1 route)
        {
            var routeStopMaps = new List<RouteStopMap>();

            if (route.RouteStopMaps != null && route.RouteStopMaps.Count > 0)
            {
                var filterActiveStops = route.RouteStopMaps.Where(map => map.IsActive == true).ToList();

                foreach (var stop in filterActiveStops)
                {
                    routeStopMaps.Add(new RouteStopMap()
                    {
                        StopName = stop.StopName,
                        OneWayFee = stop.OneWayFee,
                        TwoWayFee = stop.TwoWayFee,
                        AcademicYearID = stop.AcademicYearID,
                        IsActive = stop.IsActive,
                        SequenceNo = stop.SequenceNo,
                        Created = Convert.ToInt32(_context.LoginID),
                        CreatedDate = DateTime.Now,
                    });
                }
            }

            return routeStopMaps;
        }

        public List<RouteVehicleMap> GetRouteVehicleMapsData(Routes1 route)
        {
            var routeVehicleMaps = new List<RouteVehicleMap>();

            if (route.RouteVehicleMaps != null && route.RouteVehicleMaps.Count > 0)
            {
                var filterActiveVehicles = route.RouteVehicleMaps.Where(map => map.IsActive == true).ToList();

                foreach (var routeVehicle in filterActiveVehicles)
                {
                    routeVehicleMaps.Add(new RouteVehicleMap()
                    {
                        VehicleID = routeVehicle.VehicleID,
                        DateFrom = routeVehicle.DateFrom,
                        DateTo = routeVehicle.DateTo,
                        SchoolID = routeVehicle.SchoolID,
                        AcademicYearID = routeVehicle.AcademicYearID,
                        IsActive = routeVehicle.IsActive,
                        CreatedBy = Convert.ToInt32(_context.LoginID),
                        CreatedDate = DateTime.Now,
                    });
                }
            }

            return routeVehicleMaps;
        }

        public List<AssignVehicleMap> GetDriverAttendarVehicleMapsData(Routes1 route)
        {
            var assignVehicleMaps = new List<AssignVehicleMap>();

            if (route.AssignVehicleMaps != null && route.AssignVehicleMaps.Count > 0)
            {
                var filterActiveVehicleMaps = route.AssignVehicleMaps.Where(map => map.IsActive == true).ToList();

                foreach (var vehicleMap in filterActiveVehicleMaps)
                {
                    //Bind attendar details
                    var vehicleAttendantMapList = GetAttendantMapsData(vehicleMap);

                    assignVehicleMaps.Add(new AssignVehicleMap()
                    {
                        DateFrom = vehicleMap.DateFrom,
                        DateTo = vehicleMap.DateTo,
                        EmployeeID = vehicleMap.EmployeeID,
                        VehicleID = vehicleMap.VehicleID,
                        Notes = vehicleMap.Notes,
                        SchoolID = vehicleMap.SchoolID,
                        AcademicYearID = vehicleMap.AcademicYearID,
                        IsActive = vehicleMap.IsActive,
                        CreatedBy = Convert.ToInt32(_context.LoginID),
                        CreatedDate = DateTime.Now,
                        AssignVehicleAttendantMaps = vehicleAttendantMapList,
                    });
                }
            }

            return assignVehicleMaps;
        }

        public List<AssignVehicleAttendantMap> GetAttendantMapsData(AssignVehicleMap vehicleMap)
        {
            var vehicleAttendantMaps = new List<AssignVehicleAttendantMap>();

            if (vehicleMap.AssignVehicleAttendantMaps != null && vehicleMap.AssignVehicleAttendantMaps.Count > 0)
            {
                foreach (var attendant in vehicleMap.AssignVehicleAttendantMaps)
                {
                    vehicleAttendantMaps.Add(new AssignVehicleAttendantMap()
                    {
                        EmployeeID = attendant.EmployeeID,
                        CreatedBy = Convert.ToInt32(_context.LoginID),
                        CreatedDate = DateTime.Now,
                    });
                }
            }

            return vehicleAttendantMaps;
        }

        #endregion

        public AcademicYearDTO GetAcademicYearDataByGroupID(int? routeGroupID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var acdemicYearDTO = new AcademicYearDTO();

                var routesGroup = dbContext.RouteGroups.Where(x => x.RouteGroupID == routeGroupID)
                    .Include(i => i.AcademicYear)
                    .AsNoTracking().FirstOrDefault();

                if (routesGroup != null)
                {
                    var academicData = routesGroup.AcademicYearID.HasValue ? routesGroup.AcademicYear ?? null : null;

                    if (academicData != null)
                    {
                        acdemicYearDTO = new AcademicYearDTO()
                        {
                            AcademicYearID = academicData.AcademicYearID,
                            ORDERNO = academicData.ORDERNO,
                            AcademicYearCode = academicData.AcademicYearCode,
                            Description = academicData.Description,
                            StartDate = academicData.StartDate,
                            EndDate = academicData.EndDate,
                            SchoolID = academicData.SchoolID,
                            AcademicYearStatusID = academicData.AcademicYearStatusID,
                            IsActive = academicData.IsActive
                        };
                    }
                }

                return acdemicYearDTO;
            }
        }

    }
}