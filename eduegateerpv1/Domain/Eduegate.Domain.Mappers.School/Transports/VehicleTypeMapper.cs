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
    public class VehicleTypeMapper : DTOEntityDynamicMapper
    {
        List<string> validationFields = new List<string>() { "VehicleTypeName" };
        public static VehicleTypeMapper Mapper(CallContext context)
        {
            var mapper = new VehicleTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<VehicleTypeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private VehicleTypeDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.VehicleTypes.Where(x => x.VehicleTypeID == IID).AsNoTracking().FirstOrDefault();

                return new VehicleTypeDTO()
                {
                    VehicleTypeID = entity.VehicleTypeID,
                    VehicleTypeName = entity.VehicleTypeName,
                    Capacity = entity.Capacity,
                    Dimensions = entity.Dimensions,
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
            var toDto = dto as VehicleTypeDTO;
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
            //convert the dto to entity and pass to the repository.
            var entity = new VehicleType()
            {
                VehicleTypeID = toDto.VehicleTypeID,
                VehicleTypeName = toDto.VehicleTypeName,
                Capacity = toDto.Capacity,
                Dimensions = toDto.Dimensions,
                CreatedBy = toDto.VehicleTypeID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.VehicleTypeID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.VehicleTypeID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.VehicleTypeID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.VehicleTypeID == 0)
                {
                    var maxGroupID = dbContext.VehicleTypes.Max(a => (short?)a.VehicleTypeID);
                    entity.VehicleTypeID = Convert.ToByte(maxGroupID == null ? 1 : short.Parse(maxGroupID.ToString()) + 1);
                    dbContext.VehicleTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.VehicleTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.VehicleTypeID));
        }

        public override KeyValueDTO ValidateField(BaseMasterDTO dto, string field)
        {
            var toDto = dto as VehicleTypeDTO;
            var valueDTO = new KeyValueDTO();

            switch (field)
            {
                case "VehicleTypeName":
                    if (!string.IsNullOrEmpty(toDto.VehicleTypeName))
                    {
                        var hasDuplicated = IsvehicleTypeNameDuplicated(toDto.VehicleTypeName, toDto.VehicleTypeID);
                        if (hasDuplicated)
                        {
                            valueDTO.Key = "true";
                            valueDTO.Value = "VehicleTypeName already exists, Please try with different VehicleTypeName.";
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

        public bool IsvehicleTypeNameDuplicated(string VehicleTypeName, long VehicleTypeID)
        {
            using (var db = new dbEduegateSchoolContext())
            {
                List<VehicleType> vehicletype;

                if (VehicleTypeID == 0)
                {
                    vehicletype = db.VehicleTypes.Where(x => x.VehicleTypeName == VehicleTypeName).AsNoTracking().ToList();
                }
                else
                {
                    vehicletype = db.VehicleTypes.Where(x => x.VehicleTypeID != VehicleTypeID && x.VehicleTypeName == VehicleTypeName).AsNoTracking().ToList();
                }

                if (vehicletype.Count() >= 1)
                {
                    return true;
                }

                return false;
            }
        }

    }
}