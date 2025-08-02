using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using System;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models.Mutual;

namespace Eduegate.Domain.Mappers.Mutual
{
    public class DocumentTypeSettingsMapper : DTOEntityDynamicMapper
    {
        public static DocumentTypeSettingsMapper Mapper(CallContext context)
        {
            var mapper = new DocumentTypeSettingsMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<DocumentTypeSettingsDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private DocumentTypeSettingsDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.DocumentTypeSettings.Where(x => x.DocumentTypeSettingID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private DocumentTypeSettingsDTO ToDTO(DocumentTypeSetting entity)
        {
            var settingDTO = new DocumentTypeSettingsDTO()
            {
                DocumentTypeSettingID = entity.DocumentTypeSettingID,
                DocumentTypeID = entity.DocumentTypeID,
                SettingCode = entity.SettingCode,
                SettingValue = entity.SettingValue,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
            };

            return settingDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as DocumentTypeSettingsDTO;

            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = new DocumentTypeSetting()
                {
                    DocumentTypeSettingID = toDto.DocumentTypeSettingID,
                    DocumentTypeID = toDto.DocumentTypeID,
                    SettingCode = toDto.SettingCode,
                    SettingValue = toDto.SettingValue,
                    CreatedBy = toDto.DocumentTypeSettingID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.DocumentTypeSettingID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.DocumentTypeSettingID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.DocumentTypeSettingID > 0 ? DateTime.Now : dto.UpdatedDate,
                };

                dbContext.DocumentTypeSettings.Add(entity);
                if (entity.DocumentTypeSettingID == 0)
                {
                    var maxGroupID = dbContext.DocumentTypeSettings.Max(a => (int?)a.DocumentTypeSettingID);
                    entity.DocumentTypeSettingID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;

                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.DocumentTypeSettingID));
            }
        }

        public List<DocumentTypeSettingsDTO> GetDocumentTypeSettingsByTypeID(int documentTypeID)
        {
            var dtos = new List<DocumentTypeSettingsDTO>();

            using (var dbContext = new dbEduegateERPContext())
            {
                var entities = dbContext.DocumentTypeSettings.Where(x => x.DocumentTypeID == documentTypeID)
                    .AsNoTracking()
                    .ToList();

                foreach (var entity in entities)
                {
                    dtos.Add(ToDTO(entity));
                }
            }

            return dtos;
        }

    }
}