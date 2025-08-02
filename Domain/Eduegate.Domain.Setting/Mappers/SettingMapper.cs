using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Repository.Settings;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Enums.Synchronizer;
using Eduegate.Services.Contracts.Framework;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Services.Contracts.Settings;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Setting.Mappers
{
    public class SettingMapper : IDTOEntityMapper<SettingDTO, Eduegate.Domain.Entity.Setting.Models.Setting>
    {
        private CallContext _callContext;

        public static SettingMapper Mapper(CallContext _context = null)
        {
            var mapper = new SettingMapper();
            mapper._callContext = _context;
            return mapper;
        }

        public List<SettingDTO> ToDTO(List<Eduegate.Domain.Entity.Setting.Models.UserSetting> entities)
        {
            var dtos = new List<SettingDTO>();

            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public List<SettingDTO> ToDTO(List<Eduegate.Domain.Entity.Setting.Models.Setting> entities)
        {
            var dtos = new List<SettingDTO>();

            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public SettingDTO ToDTO(Eduegate.Domain.Entity.Setting.Models.Setting entity)
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

        public SettingDTO ToDTO(Eduegate.Domain.Entity.Setting.Models.UserSetting entity)
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

        public Eduegate.Domain.Entity.Setting.Models.Setting ToEntity(SettingDTO dto)
        {
            return new Eduegate.Domain.Entity.Setting.Models.Setting()
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

        public Eduegate.Domain.Entity.Setting.Models.UserSetting ToUserSettingEntity(SettingDTO dto, int companyID, long loginID)
        {
            return new Eduegate.Domain.Entity.Setting.Models.UserSetting()
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

        public List<ScreenFieldSettingDTO> ToDTO(List<Eduegate.Domain.Entity.Setting.Models.ScreenFieldSetting> entities)
        {
            var dtos = new List<ScreenFieldSettingDTO>();

            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public ScreenFieldSettingDTO ToDTO(Eduegate.Domain.Entity.Setting.Models.ScreenFieldSetting entity)
        {
            if (entity == null)
            {
                return new ScreenFieldSettingDTO();
            }

            return new ScreenFieldSettingDTO()
            {
                //DefaultFormat = entity.DefaultFormat,
                //DefaultValue = entity.DefaultValue,
                //Prefix = entity.Prefix,
                //TextTransformTypeId = entity.TextTransformTypeId,
                //SequenceID = entity.SequenceID,
                //ScreenFieldID = entity.ScreenFieldID,
                //ScreenID = entity.ScreenID.Value,
                //ScreenFieldSettingID = entity.ScreenFieldSettingID,
            };
        }

        public List<Eduegate.Domain.Entity.Setting.Models.ScreenFieldSetting> SaveToFieldSettingEntity(
            List<ScreenFieldSettingDTO> dtos, List<Eduegate.Domain.Entity.Setting.Models.ScreenFieldSetting> existingFields = null)
        {
            var settings = new List<Eduegate.Domain.Entity.Setting.Models.ScreenFieldSetting>();
            var reposistory = new SettingRepository();

            if (existingFields == null) //set emtpy array
            {
                existingFields = new List<Eduegate.Domain.Entity.Setting.Models.ScreenFieldSetting>();
            }

            foreach (var dto in dtos)
            {
                //var settingField = existingFields
                //    .Where(x => x.ScreenFieldName == dto.FieldName)
                //    .FirstOrDefault();

                settings.Add(new Eduegate.Domain.Entity.Setting.Models.ScreenFieldSetting()
                {
                    //Prefix = dto.Prefix,
                    //ScreenFieldID = settingField == null ? dto.ScreenFieldID : settingField.ScreenFieldID,
                    //ScreenFieldSettingID = settingField == null ? dto.ScreenFieldSettingID : settingField.ScreenFieldSettingID,
                    //DefaultValue = dto.DefaultValue,
                    //DefaultFormat = dto.DefaultFormat,
                    //ScreenID = dto.ScreenID,
                    //SequenceID = dto.SequenceID.HasValue ? dto.SequenceID : 1,
                    //TextTransformTypeId = dto.TextTransformTypeId,
                    //ScreenFieldName = dto.FieldName,
                });
            }

            return settings;
        }

        public ClassDTO GetClassDetailsByClassID(int classID)
        {
            var classDTO = new ClassDTO();

            using (var dbContext = new dbEduegateERPContext())
            {
                var classDet = dbContext.Classes.Where(x => x.ClassID == classID).AsNoTracking().FirstOrDefault();

                classDTO = new ClassDTO()
                {
                    ClassID = classDet.ClassID,
                    ClassDescription = classDet.ClassDescription,
                    Code = classDet.Code,
                    SchoolID = classDet.SchoolID,
                    ORDERNO = classDet.ORDERNO,
                };
            }

            return classDTO;
        }

        public SchoolsDTO GetSchoolDetailByID(byte schoolID)
        {
            var schoolDTO = new SchoolsDTO();

            using (var dbContext = new dbEduegateERPContext())
            {
                var schoolDet = dbContext.Schools.Where(x => x.SchoolID == schoolID).AsNoTracking().FirstOrDefault();

                schoolDTO = new SchoolsDTO()
                {
                    SchoolID = schoolDet.SchoolID,
                    SchoolCode = schoolDet.SchoolCode,
                    SchoolName = schoolDet.SchoolName,
                    Description = schoolDet.Description,
                    SchoolShortName = schoolDet.SchoolShortName,
                };
            }

            return schoolDTO;
        }

    }
}
