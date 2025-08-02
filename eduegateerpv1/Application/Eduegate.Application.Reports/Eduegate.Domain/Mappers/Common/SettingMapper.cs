using System.Collections.Generic;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Settings;

namespace Eduegate.Domain.Mappers.Common
{
    public class SettingMapper : IDTOEntityMapper<SettingDTO, Setting>
    {
        private CallContext _callContext;

        public static SettingMapper Mapper(CallContext _context = null)
        {
            var mapper = new SettingMapper();
            mapper._callContext = _context;
            return mapper;
        }

        public List<SettingDTO> ToDTO(List<UserSetting> entities)
        {
            var dtos = new List<SettingDTO>();

            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public List<SettingDTO> ToDTO(List<Setting> entities)
        {
            var dtos = new List<SettingDTO>();

            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public SettingDTO ToDTO(Setting entity)
        {
            if (entity == null)
            {
                return new SettingDTO();
            }

            return new SettingDTO()
            {
                SettingCode = entity.SettingCode,
                SettingValue = entity.SettingValue,
                Description = entity.Description,
                CompanyID = entity.CompanyID,
                SiteID = entity.SiteID,
                GroupName = entity.GroupName,
                LookupTypeID = entity.LookupTypeID,
                ValueType = entity.ValueType
            };
        }

        public SettingDTO ToDTO(UserSetting entity)
        {
            return new SettingDTO()
            {
                SettingCode = entity.SettingCode,
                SettingValue = entity.SettingValue,
                Description = entity.Description,
                CompanyID = entity.CompanyID,
                SiteID = entity.SiteID,
                GroupName = entity.GroupName,
            };
        }

        public Setting ToEntity(SettingDTO dto)
        {
            return new Setting()
            {
                SettingCode = dto.SettingCode,
                SettingValue = dto.SettingValue,
                Description = dto.Description,
                CompanyID = _callContext.CompanyID.Value,
                SiteID = dto.SiteID,
                GroupName = dto.GroupName,
                LookupTypeID = dto.LookupTypeID,
                ValueType = dto.ValueType,
            };
        }

        public UserSetting ToUserSettingEntity(SettingDTO dto, int companyID, long loginID)
        {
            return new UserSetting()
            {
                SettingCode = dto.SettingCode,
                SettingValue = dto.SettingValue,
                Description = dto.Description,
                SiteID = dto.SiteID,
                GroupName = dto.GroupName,
                CompanyID = companyID,
                LoginID = loginID
            };
        }
    }
}
