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
    public class VehicleDetailsMapMapper : DTOEntityDynamicMapper
    {
        //List<string> validationFields = new List<string>() { "VehicleID" };
        public static VehicleDetailsMapMapper Mapper(CallContext context)
        {
            var mapper = new VehicleDetailsMapMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<VehicleDetailsMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }
        private VehicleDetailsMapDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.VehicleDetailMaps.Where(x => x.VehicleDetailMapIID == IID)
                    .Include(x => x.Vehicle)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new VehicleDetailsMapDTO()
                {
                    VehicleDetailMapIID = entity.VehicleDetailMapIID,
                    RegistrationDate = entity.RegistrationDate,
                    RegistrationExpiryDate = entity.RegistrationExpiryDate,
                    VehicleID = entity.VehicleID,
                    Vehicle = new KeyValueDTO()
                    {
                        Key = entity.VehicleID.ToString(),
                        Value = entity.Vehicle.VehicleRegistrationNumber
                    },
                    InsuranceIssueDate = entity.InsuranceIssueDate,
                    InsuranceExpiryDate = entity.InsuranceExpiryDate,
                    IsActive = entity.IsActive,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as VehicleDetailsMapDTO;
            if (toDto.VehicleID == 0 || toDto.VehicleID == null)
            {
                throw new Exception("Select Vehicle!!");
            }
            // Date Different Check
            if (toDto.RegistrationDate >= toDto.RegistrationExpiryDate)
            {
                throw new Exception("Select Date Properlly!!");
            }
            // Date Different Check
            if (toDto.InsuranceIssueDate >= toDto.InsuranceExpiryDate)
            {
                throw new Exception("Select Date Properlly!!");
            }

            var errorMessage = string.Empty;

            //validate first
            //foreach (var field in validationFields)
            //{
            //    var isValid = ValidateField(toDto, field);

            //    if (isValid.Key.Equals("true"))
            //    {
            //        errorMessage = string.Concat(errorMessage, "-", isValid.Value, "<br>");
            //    }
            //}
            //if (!string.IsNullOrEmpty(errorMessage))
            //{
            //    throw new Exception(errorMessage);
            //}
            //convert the dto to entity and pass to the repository.
            var entity = new VehicleDetailMap()
            {
                VehicleDetailMapIID = toDto.VehicleDetailMapIID,
                RegistrationDate = toDto.RegistrationDate,
                RegistrationExpiryDate = toDto.RegistrationExpiryDate,
                InsuranceIssueDate = toDto.InsuranceIssueDate,
                InsuranceExpiryDate = toDto.InsuranceExpiryDate,
                VehicleID = toDto.VehicleID,
                IsActive = toDto.IsActive,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.VehicleDetailMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.VehicleDetailMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.VehicleDetailMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.VehicleDetailMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };
            using (var dbContext = new dbEduegateSchoolContext())
            {
                // Get All Vehicles Maps By Vehicle ID to Make InActive
                var existingMaps = dbContext.VehicleDetailMaps.Where(x => x.VehicleID == entity.VehicleID && x.IsActive == true).AsNoTracking().ToList();

                foreach (var map in existingMaps)
                {
                    map.IsActive = false;
                    dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.VehicleDetailMaps.Add(entity);
                if (entity.VehicleDetailMapIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }
            //return ToDTOString(ToDTO(entity.VehicleDetailMapIID));
            return GetEntity(entity.VehicleDetailMapIID);
        }

        public override KeyValueDTO ValidateField(BaseMasterDTO dto, string field)
        {
            var toDto = dto as VehicleDetailsMapDTO;
            var valueDTO = new KeyValueDTO();

            switch (field)
            {
                case "VehicleID":
                    if (!string.IsNullOrEmpty(toDto.VehicleID.ToString()))
                    {
                        var hasDuplicated = IsvehicleDuplicated(toDto.VehicleID.ToString(), toDto.VehicleDetailMapIID);
                        if (hasDuplicated)
                        {
                            valueDTO.Key = "true";
                            valueDTO.Value = "Vehicle already exists, Please try with different Vehicle.";
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

        public bool IsvehicleDuplicated(string VehicleID, long VehicleDetailMapIID)
        {
            using (var db = new dbEduegateSchoolContext())
            {
                List<VehicleDetailMap> vehicle;

                if (VehicleDetailMapIID == 0)
                {
                    vehicle = db.VehicleDetailMaps.Where(x => x.VehicleID.ToString() == VehicleID).AsNoTracking().ToList();
                }
                else
                {
                    vehicle = db.VehicleDetailMaps.Where(x => x.VehicleDetailMapIID != VehicleDetailMapIID && x.VehicleID.ToString() == VehicleID).AsNoTracking().ToList();
                }

                if (vehicle.Count() >= 1)
                {
                    return true;
                }

                return false;
            }
        }

    }
}