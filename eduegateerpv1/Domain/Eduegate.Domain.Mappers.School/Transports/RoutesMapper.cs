using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Transports;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Transports
{
    public class RoutesMapper : DTOEntityDynamicMapper
    {
        List<string> validationFields = new List<string>() { "RouteCode", "RouteDescription" };

        public static RoutesMapper Mapper(CallContext context)
        {
            var mapper = new RoutesMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<RoutesDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private RoutesDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var routeDto = new RoutesDTO();

                var entity = dbContext.Routes1.Where(x => x.RouteID == IID && x.AcademicYearID == _context.AcademicYearID && x.SchoolID == _context.SchoolID || x.RouteID == IID && x.SchoolID == null || x.RouteID == IID)
                    .Include(i => i.RouteStopMaps)
                    .Include(i => i.CostCenter)
                    .AsNoTracking()
                    .FirstOrDefault();

                var stopOrderBy = entity.RouteStopMaps != null && entity.RouteStopMaps.Count > 0 ? entity.RouteStopMaps.OrderBy(a => a.SequenceNo).ToList() : null;

                List<RouteStopFeeDTO> mapDto = new List<RouteStopFeeDTO>();
                if (stopOrderBy != null)
                {
                    foreach (var stp in stopOrderBy)
                    {
                        if (stp.RouteStopMapIID != 0)
                        {
                            mapDto.Add(new RouteStopFeeDTO()
                            {
                                RouteID = entity.RouteID,
                                StopName = stp.StopName,
                                RouteFareOneWay = stp.OneWayFee,
                                RouteStopMapIID = stp.RouteStopMapIID,
                                IsActive = stp.IsActive,
                                AcademicYearID = stp.AcademicYearID,
                                Latitude = stp.Latitude,
                                Longitude = stp.Longitude,
                                CreatedBy = stp.RouteStopMapIID == 0 ? (int)_context.LoginID : stp.Created,
                                UpdatedBy = stp.RouteStopMapIID > 0 ? (int)_context.LoginID : stp.Updated,
                                CreatedDate = stp.RouteStopMapIID == 0 ? DateTime.Now : stp.CreatedDate,
                                UpdatedDate = stp.RouteStopMapIID > 0 ? DateTime.Now : stp.UpdatedDate,
                                //TimeStamps = stp.TimeStamps == null ? null : Convert.ToBase64String(stp.TimeStamps),
                            });
                        }
                    }
                }

                routeDto = new RoutesDTO()
                {
                    RouteID = entity.RouteID,
                    RouteTypeID = entity.RouteTypeID,
                    RouteCode = entity.RouteCode,
                    ContactNumber = entity.ContactNumber,
                    RouteDescription = entity.RouteDescription,
                    RouteFareOneWay = entity.RouteFareOneWay,
                    IsActive = entity.IsActive,
                    Stops = mapDto,
                    CostCenter = entity.CostCenterID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.CostCenterID.ToString(),
                        Value = entity.CostCenter.CostCenterName
                    } : new KeyValueDTO(),
                    AcademicYearID = entity.AcademicYearID,
                    RouteGroupID = entity.RouteGroupID,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                    StopFees = mapDto,
                };
                return routeDto;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as RoutesDTO;
            var errorMessage = string.Empty;

            //validate first
            foreach (var field in validationFields)
            {
                var isValid = ValidateField(toDto, field);

                if (isValid.Key.Equals("true"))
                {
                    errorMessage = string.Concat(errorMessage, " ", isValid.Value, "<br>");
                }
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new Exception(errorMessage);
            }

            //convert the dto to entity and pass to the repository.
            var entity = new Routes1()
            {
                RouteID = toDto.RouteID,
                RouteTypeID = toDto.RouteTypeID,
                RouteCode = toDto.RouteCode,
                RouteDescription = toDto.RouteDescription,
                RouteFareOneWay = toDto.RouteFareOneWay,
                CostCenterID = toDto.CostCenterID,
                IsActive = toDto.IsActive,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = GetAcademicYearIDFromRouteGroup(toDto.RouteGroupID),
                ContactNumber = toDto.ContactNumber,
                RouteGroupID = toDto.RouteGroupID,
                CreatedBy = toDto.RouteID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.RouteID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.RouteID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.RouteID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var IIDs = toDto.StopFees
                  .Select(a => a.RouteStopMapIID).ToList();

                //delete maps
                var entities = dbContext.RouteStopMaps.Where(x =>
                    x.RouteID == entity.RouteID &&
                    !IIDs.Contains(x.RouteStopMapIID)).AsNoTracking().ToList();

                if (entities != null)
                    dbContext.RouteStopMaps.RemoveRange(entities);

                //delete/remove stop data from edit screen check
                if (toDto.StopFees.Count > 0 && toDto.RouteID != 0)
                {
                    foreach (var stpdto in entities)
                    {
                        var studTransport = dbContext.StudentRouteStopMaps.Where(x => x.DropStopMapID == stpdto.RouteStopMapIID || x.PickupStopMapID == stpdto.RouteStopMapIID).AsNoTracking().FirstOrDefault();
                        var staffTransport = dbContext.StaffRouteStopMaps.Where(x => x.DropStopMapID == stpdto.RouteStopMapIID || x.PickupStopMapID == stpdto.RouteStopMapIID).AsNoTracking().FirstOrDefault();

                        if (studTransport != null || staffTransport != null)
                        {
                            throw new Exception("The Stop is Already exist in Student/Staff Transport Screen. Please Check!");
                        }
                    }
                }

                if (toDto.StopFees.Count > 0)
                {
                    foreach (var stpdto in toDto.StopFees)
                    {
                        if (stpdto.RouteFareOneWay == null)
                        {
                            throw new Exception("Please Fill Selected Stop Fare");
                        }
                        entity.RouteStopMaps.Add(new RouteStopMap()
                        {
                            RouteID = entity.RouteID,
                            StopName = stpdto.StopName,
                            RouteStopMapIID = stpdto.RouteStopMapIID,
                            OneWayFee = stpdto.RouteFareOneWay,
                            IsActive = stpdto.IsActive,
                            Latitude = stpdto.Latitude,
                            Longitude = stpdto.Longitude,
                            AcademicYearID = entity.AcademicYearID,
                            Created = stpdto.RouteStopMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                            Updated = stpdto.RouteStopMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                            CreatedDate = stpdto.RouteStopMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                            UpdatedDate = stpdto.RouteStopMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                            //TimeStamps = stpdto.TimeStamps == null ? null : Convert.FromBase64String(stpdto.TimeStamps),
                        });

                    }
                }

                dbContext.Routes1.Update(entity);
                if (toDto.RouteID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    foreach (var map in entity.RouteStopMaps)
                    {

                        if (map.RouteStopMapIID != 0)
                        {
                            dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                if (toDto.RouteID != 0)
                {
                    using (var dbContext1 = new dbEduegateSchoolContext())
                    {
                        var getStopData = dbContext1.RouteStopMaps.Where(x => x.RouteID == toDto.RouteID).AsNoTracking().ToList();
                        foreach (var setNull in getStopData)
                        {
                            setNull.SequenceNo = null;
                            dbContext1.Entry(setNull).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            dbContext1.SaveChanges();
                            dbContext1.Entry(setNull).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                        }

                        foreach (var sequencMap in entity.RouteStopMaps)
                        {
                            var maxGroupID = getStopData.Max(a => a.SequenceNo);
                            sequencMap.SequenceNo = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                            dbContext1.Entry(sequencMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            dbContext1.SaveChanges();
                        }
                    }
                }

            }

            return GetEntity(entity.RouteID);
        }

        public int? GetAcademicYearIDFromRouteGroup(int? routeGroupID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.RouteGroups.Where(x => x.RouteGroupID == routeGroupID).AsNoTracking().FirstOrDefault();

                return entity.AcademicYearID;
            }
        }

        //public List<RoutesDTO> GetSubjectByClassID(int RouteID)
        //{
        //    using (var dbContext = new dbEduegateSchoolContext())
        //    {
        //        var routeList = new List<RoutesDTO>();

        //        var entities = dbContext.RouteStopMaps.Where(a => a.RouteID == RouteID)
        //            .Include(a => a.Routes1)
        //            .OrderBy(a => a.StopName).ToList();

        //        foreach (var map in entities)
        //        {
        //            routeList.Add(DTOTolist(map.RouteStopMapIID));
        //        }

        //        return routeList;
        //    }
        //}

        private RoutesDTO DTOTolist(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Routes1.Where(x => x.RouteID == IID)
                    .Include(a => a.RouteStopMaps)
                    .AsNoTracking()
                    .FirstOrDefault();

                //var stops = new List<KeyValueDTO>();
                //foreach (var stp in entity.RouteStopMaps)
                //{
                //    stops.Add(new KeyValueDTO()
                //    {
                //        Value = stp.StopName,
                //        Key = stp.RouteID.ToString()
                //    });
                //}

                return new RoutesDTO()
                {
                    RouteID = entity.RouteID,
                    RouteDescription = entity.RouteDescription,
                    RouteFareOneWay = entity.RouteFareOneWay,
                    CostCenterID = entity.CostCenterID,
                };
            }
        }

        public override KeyValueDTO ValidateField(BaseMasterDTO dto, string field)
        {
            var toDto = dto as RoutesDTO;
            var valueDTO = new KeyValueDTO();

            switch (field)
            {
                case "RouteCode":
                    if (!string.IsNullOrEmpty(toDto.RouteCode))
                    {
                        var hasDuplicated = IsrouteCodeDuplicated(toDto);
                        if (hasDuplicated)
                        {
                            valueDTO.Key = "true";
                            valueDTO.Value = "Route Code already exists, Please try with different Route Code.";
                        }
                        else
                        {
                            valueDTO.Key = "false";
                        }
                    }
                    else
                    {
                        valueDTO.Key = "false";
                    }
                    break;
                case "RouteDescription":
                    if (!string.IsNullOrEmpty(toDto.RouteDescription))
                    {
                        var hasDuplicated = IsrouteDescriptionDuplicated(toDto);
                        if (hasDuplicated)
                        {
                            valueDTO.Key = "true";
                            valueDTO.Value = "Route Description already exists, Please try with different Route Description.";
                        }
                        else
                        {
                            valueDTO.Key = "false";
                        }
                    }
                    else
                    {
                        valueDTO.Key = "false";
                    }
                    break;
            }
            return valueDTO;
        }

        public bool IsrouteCodeDuplicated(RoutesDTO routeDto)
        {
            using (var db = new dbEduegateSchoolContext())
            {
                List<Routes1> route;

                var routeGroupData = db.RouteGroups.Where(x => x.RouteGroupID == routeDto.RouteGroupID).AsNoTracking().FirstOrDefault();

                if (routeDto.RouteID == 0)
                {
                    route = db.Routes1.Where(x => x.RouteCode == routeDto.RouteCode && x.IsActive == true && x.RouteGroup.AcademicYearID == routeGroupData.AcademicYearID).AsNoTracking().ToList();
                }
                else
                {
                    route = db.Routes1.Where(x => x.RouteID != routeDto.RouteID && x.RouteCode == routeDto.RouteCode && x.IsActive == true && x.RouteGroup.AcademicYearID == routeGroupData.AcademicYearID).AsNoTracking().ToList();
                }

                if (route.Count() >= 1)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsrouteDescriptionDuplicated(RoutesDTO routeDto)
        {
            using (var db = new dbEduegateSchoolContext())
            {
                List<Routes1> route;

                var routeGroupData = db.RouteGroups.Where(x => x.RouteGroupID == routeDto.RouteGroupID).AsNoTracking().FirstOrDefault();

                if (routeDto.RouteID == 0)
                {
                    route = db.Routes1.Where(x => x.RouteDescription == routeDto.RouteDescription && x.IsActive == true && x.RouteGroup.AcademicYearID == routeGroupData.AcademicYearID).AsNoTracking().ToList();
                }
                else
                {
                    route = db.Routes1.Where(x => x.RouteID != routeDto.RouteID && x.RouteDescription == routeDto.RouteDescription && x.IsActive == true && x.RouteGroup.AcademicYearID == routeGroupData.AcademicYearID).AsNoTracking().ToList();
                }

                if (route.Count() >= 1)
                {
                    return true;
                }

                return false;
            }
        }

        public List<KeyValueDTO> GetRoutesByRouteGroupID(int? routeGroupID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var routeKeyValueDTO = new List<KeyValueDTO>();

                var routesDetails = dbContext.Routes1.Where(x => x.RouteGroupID == routeGroupID && x.IsActive == true).AsNoTracking().ToList();

                foreach (var route in routesDetails)
                {
                    routeKeyValueDTO.Add(new KeyValueDTO()
                    {
                        Key = route.RouteID.ToString(),
                        Value = route.RouteCode + " - " + route.RouteDescription
                    });
                }

                return routeKeyValueDTO;
            }
        }

        public List<KeyValueDTO> GetPickupStopMapsByRouteGroupID(int? routeGroupID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var stopsKeyValues = new List<KeyValueDTO>();

                var homeRouteType = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ROUTE_TYPEID_HOME");
                byte? homeRouteTypeID = byte.Parse(homeRouteType);

                var routeStopDetails = dbContext.RouteStopMaps
                    .Include(i => i.Routes1)
                    .Where(s => s.IsActive == true && s.Routes1.IsActive == true && s.Routes1.RouteTypeID != homeRouteTypeID &&
                    (s.Routes1.AssignVehicleMaps.Count > 0 ? s.Routes1.AssignVehicleMaps.Any(v => v.IsActive == true) : s.Routes1 != null) &&
                    s.Routes1.RouteGroupID == routeGroupID)
                    .AsNoTracking().ToList();

                foreach (var stop in routeStopDetails)
                {
                    stopsKeyValues.Add(new KeyValueDTO()
                    {
                        Key = stop.RouteStopMapIID.ToString(),
                        Value = stop.Routes1.RouteCode + " - " + stop.StopName
                    });
                }

                return stopsKeyValues;
            }
        }

        public List<KeyValueDTO> GetDropStopMapsByRouteGroupID(int? routeGroupID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var stopsKeyValues = new List<KeyValueDTO>();

                var schoolRouteType = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ROUTE_TYPEID_SCHOOL");
                byte? schoolRouteTypeID = byte.Parse(schoolRouteType);

                var routeStopDetails = dbContext.RouteStopMaps
                    .Include(i => i.Routes1)
                    .Where(s => s.IsActive == true && s.Routes1.IsActive == true && s.Routes1.RouteTypeID != schoolRouteTypeID &&
                    (s.Routes1.AssignVehicleMaps.Count > 0 ? s.Routes1.AssignVehicleMaps.Any(v => v.IsActive == true) : s.Routes1 != null) &&
                    s.Routes1.RouteGroupID == routeGroupID)
                    .AsNoTracking().ToList();

                foreach (var stop in routeStopDetails)
                {
                    stopsKeyValues.Add(new KeyValueDTO()
                    {
                        Key = stop.RouteStopMapIID.ToString(),
                        Value = stop.Routes1.RouteCode + " - " + stop.StopName
                    });
                }

                return stopsKeyValues;
            }
        }

    }
}