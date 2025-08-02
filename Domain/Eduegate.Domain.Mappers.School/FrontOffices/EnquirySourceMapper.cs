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
    public class EnquirySourceMapper : DTOEntityDynamicMapper
    {   
        public static  EnquirySourceMapper Mapper(CallContext context)
        {
            var mapper = new  EnquirySourceMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<EnquirySourceDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private EnquirySourceDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.EnquirySources.Where(x => x.EnquirySourceID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new EnquirySourceDTO()
                {
                    EnquirySourceID = entity.EnquirySourceID,
                    SourceName = entity.SourceName,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as EnquirySourceDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new EnquirySource()
            {
                EnquirySourceID = toDto.EnquirySourceID,
                SourceName = toDto.SourceName,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                //var repository = new EntityRepository<EnquirySource, dbEduegateSchoolContext>(dbContext);

                //if (entity.EnquirySourceID == 0)
                //{
                //    var maxGroupID = repository.GetMaxID(a => a.EnquirySourceID);
                //    entity.EnquirySourceID = Convert.ToByte(maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1);
                //    entity = repository.Insert(entity);
                //}
                //else
                //{
                //    entity = repository.Update(entity);
                //}
            }

            return ToDTOString(ToDTO(entity.EnquirySourceID ));
        }       
    }
}