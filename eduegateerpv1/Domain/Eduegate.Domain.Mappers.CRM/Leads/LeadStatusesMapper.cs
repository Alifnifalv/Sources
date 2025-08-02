using Newtonsoft.Json;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Leads;
using Eduegate.Domain.Entity.CRM.Models;
using Eduegate.Domain.Entity.CRM;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.CRM.Leads
{
    public class LeadStatusesMapper : DTOEntityDynamicMapper
    {
        public static LeadStatusesMapper Mapper(CallContext context)
        {
            var mapper = new LeadStatusesMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LeadStatusesDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private LeadStatusesDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateCRMContext())
            {
                var entity = dbContext.LeadStatuses.Where(X => X.LeadStatusID == IID)
                   .AsNoTracking()
                   .FirstOrDefault();

                return new LeadStatusesDTO()
                {
                    LeadStatusName = entity.LeadStatusName,
                    LeadStatusID = entity.LeadStatusID,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LeadStatusesDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new LeadStatus()
            {
                LeadStatusID = toDto.LeadStatusID,
                LeadStatusName = toDto.LeadStatusName,
            };

            using (var dbContext = new dbEduegateCRMContext())
            {
                if (entity.LeadStatusID == 0)
                {
                    var maxGroupID = dbContext.LeadStatuses.Max(a => (byte?)a.LeadStatusID);
                    entity.LeadStatusID = (byte)(maxGroupID == null ? 1 : byte.Parse(maxGroupID.ToString()) + 1);
                    dbContext.LeadStatuses.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.LeadStatuses.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.LeadStatusID));
        }

    }
}