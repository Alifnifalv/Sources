using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository.Recruitment;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Security;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Jobs;
using EntityGenerator.Core.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.HR.Employment
{
    public class CareerLoginMapper : DTOEntityDynamicMapper
    {
        public static CareerLoginMapper Mapper(CallContext context)
        {
            var mapper = new CareerLoginMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<RegisterUserDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.RecruitmentLogins
                    .FirstOrDefault(x => x.LoginID == IID);

                var dto = new RegisterUserDTO();

                dto.LoginID = entity.LoginID;
                dto.UserID = entity.UserID;
                dto.UserName = entity.UserName;
                dto.Password = entity.Password;
                dto.EmailID = entity.EmailID;
                dto.IsActive = entity.IsActive;

                dto.CreatedBy = entity.CreatedBy;
                dto.CreatedDate = entity.CreatedDate;
                dto.UpdatedBy = entity.UpdatedBy;
                dto.UpdatedDate = entity.UpdatedDate;

                dto.Password = entity.Password;
                dto.PasswordSalt = entity.PasswordSalt;

                dto.OTP = entity.OTP;

                return ToDTOString(dto);
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as RegisterUserDTO;

            using (var dbContext = new dbEduegateERPContext())
            {

                var roleID = Convert.ToInt32(new Domain.Setting.SettingBL(null).GetSettingValue<string>("Recruitment_Portal_RoleID").ToString());

                var PasswordSalt = PasswordHash.CreateHash(toDto.Password);
                //encryt the value to save in the DB as Password
                var Password = StringCipher.Encrypt(toDto.Password, PasswordSalt);

                var entity = new RecruitmentLogin();

                entity = new RecruitmentLogin()
                {
                    LoginID = (long)toDto.LoginID,
                    UserID = toDto.UserID,
                    UserName = toDto.UserName,
                    EmailID = toDto.EmailID,
                    Password = toDto.PasswordUpdate == true ? Password : toDto.Password,
                    PasswordSalt = toDto.PasswordUpdate == true ? PasswordSalt : toDto.PasswordSalt,
                    RoleID = roleID,
                    OTP = toDto.OTP,
                    IsActive = toDto.IsActive == true ? true : false,
                    UpdatedBy = (int?)(toDto.LoginID != null ? _context.LoginID : toDto.UpdatedBy),
                    UpdatedDate = toDto.LoginID != null ? DateTime.Now : toDto.UpdatedDate,
                    CreatedBy = (int?)(toDto.LoginID == null ? _context.LoginID : toDto.CreatedBy),
                    CreatedDate = toDto.LoginID == null ? DateTime.Now : toDto.CreatedDate,
                };

                dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();

                return GetEntity(entity.LoginID);
            }
        }

    }
}