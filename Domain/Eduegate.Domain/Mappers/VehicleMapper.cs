using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Mutual;


namespace Eduegate.Domain.Mappers
{
    public class VehicleMapper : IDTOEntityMapper<VehicleDTO, Vehicle>
    {
        private CallContext _context;

        public static VehicleMapper Mapper(CallContext context)
        {
            var mapper = new VehicleMapper();
            mapper._context = context;
            return mapper;
        }
        public VehicleDTO ToDTO(Vehicle entity)
        {
            if (entity != null)
            {
                return new VehicleDTO()
                {
                    VehicleID = entity.VehicleIID,
                    VehicleTypeID = entity.VehicleTypeID,
                    VehicleOwnershipTypeID = entity.VehicleOwnershipTypeID,
                    RegistrationName = entity.RegistrationName,
                    VehicleCode = entity.VehicleCode,
                    Description = entity.Description,
                    RegistrationNo = entity.RegistrationNo,
                    PurchaseDate = entity.PurchaseDate,
                    RegistrationExpire = entity.RegistrationExpire,
                    InsuranceExpire = entity.InsuranceExpire,
                    RigistrationCityID = entity.City.IsNotNull() ? entity.RigistrationCityID : null,
                    CityName = entity.City.IsNotNull() ? entity.City.CityName : null,
                    RigistrationCountryID = entity.Country.IsNotNull() ? entity.RigistrationCountryID : null,
                    CountryName = entity.Country.IsNotNull() ? entity.Country.CountryName : null,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                    CompanyID = entity.CompanyID != null? entity.CompanyID : (int)_context.CompanyID
                };
            }
            else return new VehicleDTO();
        }

        public Vehicle ToEntity(VehicleDTO dto)
        {
            if (dto != null)
            {
                return new Vehicle()
                {
                    VehicleIID = dto.VehicleID,
                    VehicleTypeID = dto.VehicleTypeID,
                    VehicleOwnershipTypeID = dto.VehicleOwnershipTypeID,
                    RegistrationName = dto.RegistrationName,
                    VehicleCode = dto.VehicleCode,
                    Description = dto.Description,
                    RegistrationNo = dto.RegistrationNo,
                    PurchaseDate = dto.PurchaseDate,
                    RegistrationExpire = dto.RegistrationExpire,
                    InsuranceExpire = dto.InsuranceExpire,
                    RigistrationCityID = dto.RigistrationCityID.IsNotNull() ? dto.RigistrationCityID : null,
                    RigistrationCountryID = dto.RigistrationCountryID.IsNotNull() ? dto.RigistrationCountryID : null,
                    CreatedBy = dto.VehicleID > 0 ? dto.CreatedBy : (int)_context.LoginID,
                    UpdatedBy = dto.VehicleID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = dto.VehicleID > 0 ? dto.CreatedDate : DateTime.Now,
                    UpdatedDate = dto.VehicleID > 0 ? DateTime.Now : dto.UpdatedDate,
                    //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                    CompanyID = dto.CompanyID != null ? dto.CompanyID : (int)_context.CompanyID
                };
            }
            else return new Vehicle();
        }
        public List<KeyValueDTO> GetVehicles(long loginID)
        {
            var vehicleKeyValuePairs = new List<KeyValueDTO>();

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
                                .Include(i => i.Vehicle)
                                .AsNoTracking()
                                .ToList();

                            if (vehicleMapDetails != null && vehicleMapDetails.Count > 0)
                            {
                                foreach (var vehicleMap in vehicleMapDetails)
                                {
                                    var vehicleDet = vehicleMap.Vehicle != null && vehicleMap.Vehicle.IsActive == true ? vehicleMap.Vehicle : null;


                                    if (vehicleDet != null)
                                    {
                                        // Constructing KeyValueDTO object
                                        vehicleKeyValuePairs.Add(new KeyValueDTO()
                                        {
                                            Key = vehicleDet.VehicleIID.ToString(),
                                            Value = vehicleDet.VehicleCode // You can change this to any other property you want to use as a value
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return vehicleKeyValuePairs;
        }



    }
}
