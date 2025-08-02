using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Enums.Accounting;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.Distributions;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class AssetCategoryDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long AssetCategoryID { get; set; }
        [DataMember]
        public string CategoryName { get; set; }
    }

        public class AssetCategoryMapper
        {
            public static AssetCategory ToEntity(AssetCategoryDTO dto)
            {
                if (dto != null)
                {
                    return new AssetCategory()
                    {
                        AssetCategoryID = dto.AssetCategoryID,
                        CategoryName = dto.CategoryName
                    };
                }
                else
                    return new AssetCategory();
            }


            public static AssetCategoryDTO ToDto(AssetCategory entity)
            {
                if (entity != null)
                {
                    return new AssetCategoryDTO()
                    {
                        AssetCategoryID = entity.AssetCategoryID,
                        CategoryName = entity.CategoryName,
                    };
                }
                else
                    return new AssetCategoryDTO();
            }

        }
    }


