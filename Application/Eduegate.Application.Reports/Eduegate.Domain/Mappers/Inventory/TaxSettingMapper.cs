using Newtonsoft.Json;
using Eduegate.Domain.Entity.Models.Inventory;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Inventory;
using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Mappers.Inventory
{
    public class TaxSettingMapper : DTOEntityDynamicMapper
    {
        public static TaxSettingMapper Mapper(CallContext context)
        {
            var mapper = new TaxSettingMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<TaxSettingDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            dto = dto as TaxSettingDTO;
            return JsonConvert.SerializeObject(dto);
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as TaxSettingDTO;
            var entity = ToEntity(toDto);
            var TaxTemplateID = new ReferenceDataRepository().SaveTaxTemplate(entity);
            return GetEntity(entity.TaxTemplateID);
        }

        public override string GetEntity(long IID)
        {
            var repository = new ReferenceDataRepository();
            var entity = repository.GetTaxTemplates(int.Parse(IID.ToString()));
            var dto = ToDTO(entity);
            return ToDTOString(dto);
        }

        public TaxSettingDTO ToDTO(TaxTemplate entity)
        {
            if (entity != null)
            {
                var dto = new TaxSettingDTO()
                {
                    TaxTemplateID = entity.TaxTemplateID,
                    IsActive = entity.IsActive,
                    IsDefault = entity.IsDefault,
                    TemplateName = entity.TemplateName,
                    HasTaxInclusive = entity.HasTaxInclusive,
                    TemplateItems = new System.Collections.Generic.List<TaxTemplateItemsDTO>()
                };

                foreach(var item in entity.TaxTemplateItems)
                {
                    dto.TemplateItems.Add(new TaxTemplateItemsDTO()
                    {
                        AccountID = item.AccountID,
                        Amount = item.Amount,
                        Account = item.AccountID.HasValue ? new Eduegate.Framework.Contracts.Common.KeyValueDTO()
                        {
                            Key = item.Account.AccountID.ToString(),
                            Value = item.Account.AccountName
                        } : null,
                        TaxTemplateID = item.TaxTemplateID,
                        TaxTemplateItemID = item.TaxTemplateItemID,
                        TaxTypeID = item.TaxTypeID,
                        Percentage = item.Percentage,
                        HasTaxInclusive = item.HasTaxInclusive
                    });
                }

                return dto;
            }
            else return new TaxSettingDTO();
        }

        public TaxTemplate ToEntity(TaxSettingDTO TaxTemplate)
        {
            var entity = new TaxTemplate();
            var repository = new ReferenceDataRepository();

            entity.TaxTemplateID = TaxTemplate.TaxTemplateID;
            entity.TemplateName = TaxTemplate.TemplateName;
            entity.IsActive = TaxTemplate.IsActive;
            entity.IsDefault = TaxTemplate.IsDefault;
            entity.HasTaxInclusive = TaxTemplate.HasTaxInclusive;
            entity.TaxTemplateItems = new List<TaxTemplateItem>();

            foreach (var item in TaxTemplate.TemplateItems)
            {
                if (!item.TaxTypeID.HasValue) continue;

                entity.TaxTemplateItems.Add(new TaxTemplateItem()
                {
                    AccountID = item.Account != null && !string.IsNullOrEmpty(item.Account.Key) ?  int.Parse(item.Account.Key) : (int?)null,
                    Amount = item.Amount,
                    Percentage = item.Percentage,
                    TaxTemplateID = item.TaxTemplateID,
                    TaxTemplateItemID = item.TaxTemplateItemID,
                    TaxTypeID = item.TaxTypeID,
                    HasTaxInclusive= TaxTemplate.HasTaxInclusive // now UI is setting from the parent.
                });
            }

            return entity;
        }
    }
}
