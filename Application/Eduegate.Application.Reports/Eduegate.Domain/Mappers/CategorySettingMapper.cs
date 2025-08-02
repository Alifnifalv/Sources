using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain.Mappers
{
    public class CategorySettingMapper : IDTOEntityMapper<CategorySettingDTO, CategorySetting>
    {

        private CallContext _context;

        public static CategorySettingMapper Mapper(CallContext context)
        {
            var mapper = new CategorySettingMapper();
            mapper._context = context;
            return mapper;
        }

        public CategorySettingDTO ToDTO(CategorySetting entity)
        {
            return null;
        }

        public CategorySetting ToEntity(CategorySettingDTO dto)
        {
            return null;
        }

        public  List<CategorySettingDTO> ToDTOList(List<CategorySetting> entity)
        {
            var dtoList = new List<CategorySettingDTO>();
            foreach (var item in entity)
            {
                dtoList.Add(new CategorySettingDTO() {
                    CategorySettingsID = item.CategorySettingsID,
                    CategoryID = item.CategoryID,
                    SettingCode = item.SettingCode,
                    SettingValue = item.SettingValue,
                    UIControlTypeID = item.UIControlTypeID,
                    LookUpID = item.LookUpID,
                    CreatedBy = item.CreatedBy.HasValue?(int)item.CreatedBy.Value:default(int),
                    CreatedDate = item.CreatedDate,
                    UpdatedBy = item.UpdatedBy.HasValue ? (int)item.UpdatedBy.Value : default(int),
                    UpdatedDate = item.UpdatedDate,
                    Description = item.Description
                });
            }
            return dtoList;
        }
    }
}
