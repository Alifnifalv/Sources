using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Transports
{
    public class VehicleOwnershipTypeMapper : DTOEntityDynamicMapper
    {
        List<string> validationFields = new List<string>() { "OwnershipTypeName" };

        public static VehicleOwnershipTypeMapper Mapper(CallContext context)
        {
            var mapper = new VehicleOwnershipTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<VehicleOwnershipTypeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private VehicleOwnershipTypeDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.VehicleOwnershipTypes.Where(x => x.VehicleOwnershipTypeID == IID).AsNoTracking().FirstOrDefault();

                return new VehicleOwnershipTypeDTO()
                {
                    VehicleOwnershipTypeID = entity.VehicleOwnershipTypeID,
                    OwnershipTypeName = entity.OwnershipTypeName,
                    Description = entity.Description,
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
            var toDto = dto as VehicleOwnershipTypeDTO;
            //validate first
            var errorMessage = string.Empty;
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
            //convert the dto to entity and pass to the repository.
            var entity = new VehicleOwnershipType()
            {
                VehicleOwnershipTypeID = toDto.VehicleOwnershipTypeID,
                OwnershipTypeName = toDto.OwnershipTypeName,
                Description = toDto.Description,
                CreatedBy = toDto.VehicleOwnershipTypeID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.VehicleOwnershipTypeID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.VehicleOwnershipTypeID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.VehicleOwnershipTypeID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.VehicleOwnershipTypeID == 0)
                {
                    var maxGroupID = dbContext.VehicleOwnershipTypes.Max(a => (short?)a.VehicleOwnershipTypeID);
                    entity.VehicleOwnershipTypeID = Convert.ToByte(maxGroupID == null ? 1 : short.Parse(maxGroupID.ToString()) + 1);
                    dbContext.VehicleOwnershipTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.VehicleOwnershipTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.VehicleOwnershipTypeID));
        }

        public override KeyValueDTO ValidateField(BaseMasterDTO dto, string field)
        {
            var toDto = dto as VehicleOwnershipTypeDTO;
            var valueDTO = new KeyValueDTO();

            switch (field)
            {
                case "OwnershipTypeName":
                    if (!string.IsNullOrEmpty(toDto.OwnershipTypeName))
                    {
                        var hasDuplicated = IsvehicleTypeNameDuplicated(toDto.OwnershipTypeName, toDto.VehicleOwnershipTypeID);
                        if (hasDuplicated)
                        {
                            valueDTO.Key = "true";
                            valueDTO.Value = "VehicleTOwnershipTypeNameypeName already exists, Please try with different OwnershipTypeName.";
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

        public bool IsvehicleTypeNameDuplicated(string OwnershipTypeName, long VehicleOwnershipTypeID)
        {
            using (var db = new dbEduegateSchoolContext())
            {
                List<VehicleOwnershipType> vehicleOwner;

                if (VehicleOwnershipTypeID == 0)
                {
                    vehicleOwner = db.VehicleOwnershipTypes.Where(x => x.OwnershipTypeName == OwnershipTypeName).AsNoTracking().ToList();
                }
                else
                {
                    vehicleOwner = db.VehicleOwnershipTypes.Where(x => x.VehicleOwnershipTypeID != VehicleOwnershipTypeID && x.OwnershipTypeName == OwnershipTypeName).AsNoTracking().ToList();
                }

                if (vehicleOwner.Count() >= 1)
                {
                    return true;
                }

                return false;
            }
        }

    }
}