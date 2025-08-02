using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.FrontOffices;
using Eduegate.Framework;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.FrontOffices
{
    public class EnquiryReferenceTypeMapper : DTOEntityDynamicMapper
    {   
        public static  EnquiryReferenceTypeMapper Mapper(CallContext context)
        {
            var mapper = new  EnquiryReferenceTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<EnquiryReferenceTypeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private EnquiryReferenceTypeDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.EnquiryReferenceTypes.Where(x => x.EnquiryReferenceTypeID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new EnquiryReferenceTypeDTO()
                {
                    EnquiryReferenceTypeID = entity.EnquiryReferenceTypeID,
                    ReferenceName = entity.ReferenceName,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as EnquiryReferenceTypeDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new EnquiryReferenceType()
            {
                EnquiryReferenceTypeID = toDto.EnquiryReferenceTypeID,
                ReferenceName = toDto.ReferenceName,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                //var repository = new EntityRepository<EnquiryReferenceType, dbEduegateSchoolContext>(dbContext);

                //if (entity.EnquiryReferenceTypeID == 0)
                //{
                //    var maxGroupID = repository.GetMaxID(a => a.EnquiryReferenceTypeID);
                //    entity.EnquiryReferenceTypeID = Convert.ToByte(maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1);
                //    entity = repository.Insert(entity);
                //}
                //else
                //{
                //    entity = repository.Update(entity);
                //}
            }

            return ToDTOString(ToDTO(entity.EnquiryReferenceTypeID ));
        }       
    }
}




