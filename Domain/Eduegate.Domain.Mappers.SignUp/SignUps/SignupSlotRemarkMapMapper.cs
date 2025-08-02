using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using System;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Eduegate.Domain.Entity.SignUp;
using Eduegate.Services.Contracts.SignUp.SignUps;
using Eduegate.Domain.Entity.SignUp.Models;
using System.Globalization;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Eduegate.Domain.Mappers.SignUp.SignUps
{
    public class SignupSlotRemarkMapMapper : DTOEntityDynamicMapper
    {
        public static SignupSlotRemarkMapMapper Mapper(CallContext context)
        {
            var mapper = new SignupSlotRemarkMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SignupSlotRemarkMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private SignupSlotRemarkMapDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSignUpContext())
            {
                var entity = dbContext.SignupSlotRemarkMaps.Where(x => x.SignupSlotRemarkMapIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private SignupSlotRemarkMapDTO ToDTO(SignupSlotRemarkMap entity)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            var groupDTO = new SignupSlotRemarkMapDTO()
            {
                SignupSlotRemarkMapIID = entity.SignupSlotRemarkMapIID,
                SignupSlotAllocationMapID = entity.SignupSlotAllocationMapID,
                SignupSlotMapID = entity.SignupSlotMapID,
                SignupID = entity.SignupID,
                TeacherRemarks = entity.TeacherRemarks,
                ParentRemarks = entity.ParentRemarks,
                RemarkEnteredDate = entity.RemarkEnteredDate,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
            };

            return groupDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SignupSlotRemarkMapDTO;

            var entity = new SignupSlotRemarkMap()
            {
                SignupSlotRemarkMapIID = toDto.SignupSlotRemarkMapIID,
                SignupSlotAllocationMapID = toDto.SignupSlotAllocationMapID,
                SignupSlotMapID = toDto.SignupSlotMapID,
                SignupID = toDto.SignupID,
                TeacherRemarks = toDto.TeacherRemarks,
                ParentRemarks = toDto.ParentRemarks,
                RemarkEnteredDate = toDto.RemarkEnteredDate,
                CreatedBy = toDto.SignupSlotRemarkMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.SignupSlotRemarkMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.SignupSlotRemarkMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.SignupSlotRemarkMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            var signupSlotRemarkMapID = SaveSlotMapEntry(entity);

            return ToDTOString(ToDTO(signupSlotRemarkMapID));
        }

        public long SaveSlotMapEntry(SignupSlotRemarkMap entity)
        {
            using (var dbContext = new dbEduegateSignUpContext())
            {
                if (entity.SignupSlotRemarkMapIID == 0)
                {
                    entity.RemarkEnteredDate = DateTime.Now;
                }
                else
                {
                    if (!entity.RemarkEnteredDate.HasValue)
                    {
                        var slotData = dbContext.SignupSlotRemarkMaps
                            .Where(x => x.SignupSlotRemarkMapIID == entity.SignupSlotRemarkMapIID)
                            .AsNoTracking().FirstOrDefault();

                        entity.RemarkEnteredDate = slotData != null && slotData.RemarkEnteredDate.HasValue ? slotData.RemarkEnteredDate.Value : DateTime.Now;
                    }
                }

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

        public string SaveSignupSlotRemarkMap(SignupSlotRemarkMapDTO slotRemarkMap)
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
                RemarkEnteredDate = slotRemarkMap.RemarkEnteredDate,
                CreatedBy = slotRemarkMap.SignupSlotRemarkMapIID == 0 ? (int)_context.LoginID : slotRemarkMap.CreatedBy,
                UpdatedBy = slotRemarkMap.SignupSlotRemarkMapIID > 0 ? (int)_context.LoginID : slotRemarkMap.UpdatedBy,
                CreatedDate = slotRemarkMap.SignupSlotRemarkMapIID == 0 ? DateTime.Now : slotRemarkMap.CreatedDate,
                UpdatedDate = slotRemarkMap.SignupSlotRemarkMapIID > 0 ? DateTime.Now : slotRemarkMap.UpdatedDate,
            };

            var signupSlotRemarkMapID = SaveSlotMapEntry(entity);

            if (signupSlotRemarkMapID > 0)
            {
                var slotStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_CLOSED", 4);

                SignUpMapper.Mapper(_context).UpdateSignupSlotMapStatus(slotRemarkMap.SignupSlotMapID.Value, slotStatusID);

                returnMessage = "Saved successfully!";
            }
            else
            {
                returnMessage = null;
            }

            return returnMessage;
        }

        public SignupSlotRemarkMapDTO GetSlotDetailsByAllocationID(long? slotAllocationMapID)
        {
            using (var dbContext = new dbEduegateSignUpContext())
            {
                var slotRemarksMapDTO = new SignupSlotRemarkMapDTO();

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