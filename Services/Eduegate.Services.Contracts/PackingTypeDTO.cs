using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class PackingTypeDTO
    {
        [DataMember]
        public long PackingTypeID { get; set; }
        [DataMember]
        public string PackingTypeName { get; set; }
    }

        public class PackingTypeMapper
        {
            public static PackingType ToEntity(PackingTypeDTO dto)
            {
                if (dto != null)
                {
                    return new PackingType()
                    {
                        PackingTypeIID = Convert.ToInt16(dto.PackingTypeID),
                        PackingType1 = dto.PackingTypeName,
                   
                    };
                }
                else
                    return new PackingType();
            }


            public static PackingTypeDTO ToDto(PackingType entity)
            {
                if (entity != null)
                {
                    return new PackingTypeDTO()
                    {
                        PackingTypeID = entity.PackingTypeIID,
                        PackingTypeName = entity.PackingType1,
                       
                    };
                }
                else
                    return new PackingTypeDTO();
            }

        }
    }
