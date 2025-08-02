using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.School.Transports;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Transports
{
    public class VehicleMapper : DTOEntityDynamicMapper
    {
        List<string> validationFields = new List<string>() { "VehicleRegistrationNumber" };
        public static VehicleMapper Mapper(CallContext context)
        {
            var mapper = new VehicleMapper();
            mapper._context = context;
            return mapper;
        }



        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<VehicleDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private VehicleDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Vehicles.Where(x => x.VehicleIID == IID).AsNoTracking().FirstOrDefault();

                return new VehicleDTO()
                {
                    VehicleIID = entity.VehicleIID,
                    VehicleTypeID = entity.VehicleTypeID,
                    VehicleOwnershipTypeID = entity.VehicleOwnershipTypeID,
                    VehicleRegistrationNumber = entity.VehicleRegistrationNumber,
                    VehicleCode = entity.VehicleCode,
                    FleetCode = entity.FleetCode,
                    Description = entity.Description,
                    RegistrationNo = entity.RegistrationNo,
                    PurchaseDate = entity.PurchaseDate,
                    ModelName = entity.ModelName,
                    YearMade = entity.YearMade,
                    VehicleAge = entity.VehicleAge,
                    TransmissionID = entity.TransmissionID,
                    ManufactureName = entity.ManufactureName,
                    Color = entity.Color,
                    Power = entity.Power,
                    AllowSeatingCapacity = entity.AllowSeatingCapacity,
                    MaximumSeatingCapacity = entity.MaximumSeatingCapacity,
                    IsActive = entity.IsActive,
                    IsSecurityEnabled = entity.IsSecurityEnabled,
                    IsCameraEnabled = entity.IsCameraEnabled,
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
            var toDto = dto as VehicleDTO;
            var errorMessage = string.Empty;

            //validate first
            foreach (var field in validationFields)
            {
                var isValid = ValidateField(toDto, field);

                if (isValid.Key.Equals("true"))
                {
                    errorMessage = string.Concat(errorMessage, "-", isValid.Value, "<br>");
                }
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new Exception(errorMessage);
            }

            MutualRepository mutualRepository = new MutualRepository();
            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();

            if (toDto.VehicleIID == 0)
            {
                try
                {
                    sequence = mutualRepository.GetNextSequence("VehicleCode", null);
                }
                catch (Exception ex)
                {
                    throw new Exception("Please generate sequence with 'VehicleCode'");
                }
            }
            //convert the dto to entity and pass to the repository.
            var entity = new Vehicle()
            {
                VehicleIID = toDto.VehicleIID,
                VehicleTypeID = toDto.VehicleTypeID,
                VehicleOwnershipTypeID = toDto.VehicleOwnershipTypeID,
                VehicleRegistrationNumber = toDto.VehicleRegistrationNumber,
                VehicleCode = toDto.VehicleCode,
                FleetCode = toDto.VehicleIID == 0 ? sequence.Prefix + sequence.LastSequence : toDto.FleetCode,
                Description = toDto.Description,
                RegistrationNo = toDto.RegistrationNo,
                PurchaseDate = toDto.PurchaseDate,
                ModelName = toDto.ModelName,
                YearMade = toDto.YearMade,
                VehicleAge = toDto.VehicleAge,
                TransmissionID = toDto.TransmissionID,
                ManufactureName = toDto.ManufactureName,
                Color = toDto.Color,
                Power = toDto.Power,
                AllowSeatingCapacity = toDto.AllowSeatingCapacity,
                MaximumSeatingCapacity = toDto.MaximumSeatingCapacity,
                IsActive = toDto.IsActive,
                IsSecurityEnabled = toDto.IsSecurityEnabled,
                IsCameraEnabled = toDto.IsCameraEnabled,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.VehicleIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.VehicleIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.VehicleIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.VehicleIID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                dbContext.Vehicles.Add(entity);
                if (entity.VehicleIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }
            return GetEntity(entity.VehicleIID);
            //return ToDTOString(ToDTO(entity.VehicleIID));
        }

        public override KeyValueDTO ValidateField(BaseMasterDTO dto, string field)
        {
            var toDto = dto as VehicleDTO;
            var valueDTO = new KeyValueDTO();

            switch (field)
            {
                case "VehicleRegistrationNumber":
                    if (!string.IsNullOrEmpty(toDto.VehicleRegistrationNumber))
                    {
                        var hasDuplicated = IsvehicleRegistrationNumberDuplicated(toDto.VehicleRegistrationNumber, toDto.VehicleIID);
                        if (hasDuplicated)
                        {
                            valueDTO.Key = "true";
                            valueDTO.Value = "VehicleRegistrationNumber already exists, Please try with different VehicleRegistrationNumber.";
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

        public bool IsvehicleRegistrationNumberDuplicated(string VehicleRegistrationNumber, long VehicleIID)
        {
            using (var db = new dbEduegateSchoolContext())
            {
                List<Vehicle> vehicle;

                if (VehicleIID == 0)
                {
                    vehicle = db.Vehicles.Where(x => x.VehicleRegistrationNumber == VehicleRegistrationNumber && x.IsActive == true).AsNoTracking().ToList();
                }
                else
                {
                    vehicle = db.Vehicles.Where(x => x.VehicleIID != VehicleIID && x.VehicleRegistrationNumber == VehicleRegistrationNumber && x.IsActive == true).AsNoTracking().ToList();
                }

                if (vehicle.Count() >= 1)
                {
                    return true;
                }

                return false;
            }
        }

        public OperationResultDTO SubmitDriverVehicleTracking(DriverVehicleTrackingDTO trackingInfo)
        {
            var message = new OperationResultDTO();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                // Check if there's an existing entry for the same route, vehicle, and day
                var existingEntry = dbContext.VehicleTrackings.FirstOrDefault(t =>
                    t.EmployeeID == trackingInfo.EmployeeID &&
                    t.RouteID == trackingInfo.RouteID &&
                    t.VehicleID == trackingInfo.VehicleID &&
                    t.StartTime.HasValue &&
                    t.StartTime.Value.Date == DateTime.Today.Date); // Compare only the date part

                if (existingEntry != null)
                {
                    // Update existing entry
                    if (existingEntry.EndTime == null) // If it's the end of the journey
                    {
                        existingEntry.RouteEndKM = trackingInfo.RouteEndKM;
                        existingEntry.EndTime = DateTime.Now; // Save server time for EndTime
                    }
                    else // If it's the start of the journey
                    {
                        existingEntry.RouteStartKM = trackingInfo.RouteStartKM;
                        existingEntry.AttachmentID1 = trackingInfo.AttachmentID1;
                        existingEntry.AttachmentID2 = trackingInfo.AttachmentID2;
                        existingEntry.StartTime = DateTime.Now; // Save server time for StartTime
                    }

                    existingEntry.UpdatedDate = DateTime.Now;

                    try
                    {
                        dbContext.SaveChanges();

                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Success,
                            Message = "Updated successfully!"
                        };
                    }
                    catch (Exception ex)
                    {
                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Error,
                            Message = $"Failed to update! Error: {ex.Message}"
                        };
                    }
                }
                else
                {
                    // Insert new entry
                    var entity = new VehicleTracking()
                    {
                        VehicleID = trackingInfo.VehicleID,
                        RouteID = trackingInfo.RouteID,
                        EmployeeID = trackingInfo.EmployeeID,
                        RouteStartKM = trackingInfo.RouteStartKM,
                        RouteEndKM = trackingInfo.RouteEndKM,
                        AttachmentID1 = trackingInfo.AttachmentID1,
                        AttachmentID2 = trackingInfo.AttachmentID2,
                        StartTime = DateTime.Now, // Save server time for StartTime
                        EndTime = trackingInfo.EndTime, // Keep EndTime as is
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    };

                    dbContext.VehicleTrackings.Add(entity);

                    try
                    {
                        dbContext.SaveChanges();

                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Success,
                            Message = "Saved successfully!"
                        };
                    }
                    catch (Exception ex)
                    {
                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Error,
                            Message = $"Failed to save! Error: {ex.Message}"
                        };
                    }
                }
            }

            return message;
        }

    }
}