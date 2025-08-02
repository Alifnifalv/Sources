using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using System;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
using System.Collections.Generic;
using System.Security.Cryptography;
using Eduegate.Domain.Entity.Lms.Models;
using Eduegate.Services.Contracts.Lms;

namespace Eduegate.Domain.Mappers.Lms.Lms
{
    public class LmsSlotRemarkMapMapper : DTOEntityDynamicMapper
    {
        public static LmsSlotRemarkMapMapper Mapper(CallContext context)
        {
            var mapper = new LmsSlotRemarkMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LmsSlotRemarkMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private LmsSlotRemarkMapDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateLmsContext())
            {
                var entity = dbContext.SignupSlotRemarkMaps.Where(x => x.SignupSlotRemarkMapIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private LmsSlotRemarkMapDTO ToDTO(SignupSlotRemarkMap entity)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            var groupDTO = new LmsSlotRemarkMapDTO()
            {
                SignupSlotRemarkMapIID = entity.SignupSlotRemarkMapIID,
                SignupSlotAllocationMapID = entity.SignupSlotAllocationMapID,
                SignupSlotMapID = entity.SignupSlotMapID,
                SignupID = entity.SignupID,
                TeacherRemarks = entity.TeacherRemarks,
                ParentRemarks = entity.ParentRemarks,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
            };

            return groupDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LmsSlotRemarkMapDTO;

            using (var dbContext = new dbEduegateLmsContext())
            {
                var entity = new SignupSlotRemarkMap()
                {
                    SignupSlotRemarkMapIID = toDto.SignupSlotRemarkMapIID,
                    SignupSlotAllocationMapID = toDto.SignupSlotAllocationMapID,
                    SignupSlotMapID = toDto.SignupSlotMapID,
                    SignupID = toDto.SignupID,
                    TeacherRemarks = toDto.TeacherRemarks,
                    ParentRemarks = toDto.ParentRemarks,
                    CreatedBy = toDto.SignupSlotRemarkMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.SignupSlotRemarkMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.SignupSlotRemarkMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.SignupSlotRemarkMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                };

                var signupSlotRemarkMapID = SaveSlotMapEntry(entity);

                return ToDTOString(ToDTO(signupSlotRemarkMapID));
            }
        }

        public long SaveSlotMapEntry(SignupSlotRemarkMap entity)
        {
            using (var dbContext = new dbEduegateLmsContext())
            {
                dbContext.SignupSlotRemarkMaps.Add(entity);
                if (entity.SignupSlotRemarkMapIID == 0)
                {
                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = EntityState.Modified;
                }
                dbContext.SaveChanges();

                return entity.SignupSlotRemarkMapIID;
            }
        }

        public string SaveSignupSlotRemarkMap(LmsSlotRemarkMapDTO slotRemarkMap)
        {
            string returnMessage = null;

            var entity = new SignupSlotRemarkMap()
            {
                SignupSlotRemarkMapIID = slotRemarkMap.SignupSlotRemarkMapIID,
                SignupSlotAllocationMapID = slotRemarkMap.SignupSlotAllocationMapID,
                SignupSlotMapID = slotRemarkMap.SignupSlotMapID,
                SignupID = slotRemarkMap.SignupID,
                TeacherRemarks = slotRemarkMap.TeacherRemarks,
                ParentRemarks = slotRemarkMap.ParentRemarks,
                CreatedBy = slotRemarkMap.SignupSlotRemarkMapIID == 0 ? (int)_context.LoginID : slotRemarkMap.CreatedBy,
                UpdatedBy = slotRemarkMap.SignupSlotRemarkMapIID > 0 ? (int)_context.LoginID : slotRemarkMap.UpdatedBy,
                CreatedDate = slotRemarkMap.SignupSlotRemarkMapIID == 0 ? DateTime.Now : slotRemarkMap.CreatedDate,
                UpdatedDate = slotRemarkMap.SignupSlotRemarkMapIID > 0 ? DateTime.Now : slotRemarkMap.UpdatedDate,
            };

            var signupSlotRemarkMapID = SaveSlotMapEntry(entity);

            if (signupSlotRemarkMapID != 0)
            {
                returnMessage = "Saved successfully!";
            }
            else
            {
                returnMessage = null;
            }

            return returnMessage;
        }

        public LmsSlotRemarkMapDTO GetSlotDetailsByAllocationID(long? slotAllocationMapID)
        {
            using (var dbContext = new dbEduegateLmsContext())
            {
                var slotRemarksMapDTO = new LmsSlotRemarkMapDTO();

                var entity = dbContext.SignupSlotRemarkMaps.Where(x => x.SignupSlotAllocationMapID == slotAllocationMapID)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (entity != null)
                {
                    slotRemarksMapDTO = ToDTO(entity);
                }

                return slotRemarksMapDTO;
            }
        }

    }
}