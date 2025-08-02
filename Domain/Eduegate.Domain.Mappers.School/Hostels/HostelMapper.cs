using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Hostel;
using Eduegate.Framework;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Hostels
{
    public class HostelMapper : DTOEntityDynamicMapper
    {   
        public static  HostelMapper Mapper(CallContext context)
        {
            var mapper = new  HostelMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<HostelDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private HostelDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Hostels.Where(x => x.HostelID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new HostelDTO()
                {
                    HostelID = entity.HostelID,
                    HostelName = entity.HostelName,
                    HostelTypeID = entity.HostelTypeID,
                    Address = entity.Address,
                    InTake = entity.InTake,
                    Description = entity.Description,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as HostelDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new Eduegate.Domain.Entity.School.Models.Hostel()
            {
                HostelID = toDto.HostelID,
                HostelName = toDto.HostelName,
                HostelTypeID = toDto.HostelTypeID,
                Address = toDto.Address,
                InTake = toDto.InTake,
                Description = toDto.Description,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                //var repository = new EntityRepository<Hostel, dbEduegateSchoolContext>(dbContext);

                //if (entity.HostelID == 0)
                //{
                //    var maxGroupID = repository.GetMaxID(a => a.HostelID);
                //    entity.HostelID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                //    entity = repository.Insert(entity);
                //}
                //else
                //{
                //    entity = repository.Update(entity);
                //}
            }

            return ToDTOString(ToDTO(entity.HostelID ));
        }       
    }
}