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
    public class LeadTypesMapper : DTOEntityDynamicMapper
    {
        public static LeadTypesMapper Mapper(CallContext context)
        {
            var mapper = new LeadTypesMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LeadTypesDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private LeadTypesDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateCRMContext())
            {
                var entity = dbContext.LeadTypes.Where(X => X.LeadTypeID == IID)
                   .AsNoTracking()
                   .FirstOrDefault();

                var leadType = new LeadTypesDTO()
                {
                    LeadTypeName = entity.LeadTypeName,
                    LeadTypeID = entity.LeadTypeID,
                };
                leadType.LeadSequencePrefix = entity.LeadTypeID != 0 ? SequenceDetail(entity.LeadTypeID).LeadSequencePrefix : null;

                return leadType;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LeadTypesDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new LeadType()
            {
                LeadTypeID = toDto.LeadTypeID,
                LeadTypeName = toDto.LeadTypeName,
            };

            using (var dbContext = new dbEduegateCRMContext())
            {
                if (entity.LeadTypeID == 0)
                {
                    var maxGroupID = dbContext.LeadTypes.Max(a => (byte?)a.LeadTypeID);

                    entity.LeadTypeID = (byte)(maxGroupID == null ? 1 : byte.Parse(maxGroupID.ToString()) + 1);

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                //dbContext.LeadTypes.Add(entity);

                var sequence = dbContext.Sequences.Where(x => x.SequenceType == "Lead_" + entity.LeadTypeID.ToString()).OrderByDescending(o => o.SequenceID).AsNoTracking().FirstOrDefault();

                if (sequence != null)
                {
                    sequence.Prefix = toDto.LeadSequencePrefix.ToUpper();

                    dbContext.Entry(sequence).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                else
                {
                    sequence = new Sequence();
                    var maxGroupID = dbContext.Sequences.Max(a => (int?)a.SequenceID);

                    sequence.SequenceID = (byte)(maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1);
                    sequence.SequenceType = "Lead_" + entity.LeadTypeID.ToString();
                    sequence.Prefix = toDto.LeadSequencePrefix.ToUpper();
                    sequence.LastSequence = 0;

                    dbContext.Entry(sequence).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.LeadTypeID));
        }

        private LeadTypesDTO SequenceDetail(long? leadTypeID)
        {
            using (dbEduegateCRMContext dbContext = new dbEduegateCRMContext())
            {
                var dtos = new LeadTypesDTO();

                var sequence = dbContext.Sequences.Where(X => X.SequenceType == "Lead_" + leadTypeID.ToString())
                  .AsNoTracking()
                  .FirstOrDefault();

                if (sequence != null)
                {
                    dtos.LeadSequencePrefix = sequence.Prefix;
                }

                return dtos;
            }
        }

    }
}