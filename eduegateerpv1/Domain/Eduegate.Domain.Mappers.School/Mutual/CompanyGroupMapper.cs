using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Framework;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Mutual
{
    public class CompanyGroupMapper : DTOEntityDynamicMapper
    {   
        public static  CompanyGroupMapper Mapper(CallContext context)
        {
            var mapper = new  CompanyGroupMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<CompanyGroupDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private CompanyGroupDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.CompanyGroups.Where(a => a.CompanyGroupID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new CompanyGroupDTO()
                {
                    CompanyGroupID = entity.CompanyGroupID,
                    GroupName = entity.GroupName,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as CompanyGroupDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new CompanyGroup()
            {
                CompanyGroupID = toDto.CompanyGroupID,
                GroupName = toDto.GroupName,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
              
                if (entity.CompanyGroupID == 0)
                {                    
                    var maxGroupID = dbContext.CompanyGroups.Max(a => (int?)a.CompanyGroupID);
                    entity.CompanyGroupID = (byte)(maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1);
                    dbContext.CompanyGroups.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.CompanyGroups.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.CompanyGroupID ));
        }       
    }
}