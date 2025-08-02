using Newtonsoft.Json;
using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.HR.Leaves;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.HR.Leaves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers.HR.Leaves
{
    public class LeaveBlocklistMapper: DTOEntityDynamicMapper
    {
        private CallContext _context;

        public static LeaveBlocklistMapper Mapper(CallContext context)
        {
            var mapper = new LeaveBlocklistMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LeaveBlockDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var repository = new EntiyRepository<LeaveBlockList, dbEduegateHRContext>(dbContext);
                var entity = repository.GetById(IID);

                return ToDTOString(new LeaveBlockDTO()
                {
                    
                });
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LeaveBlockDTO;

            //convert the dto to entity and pass to the repository.
            var entity = new LeaveBlockList()
            {
                
            };

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var repository = new EntiyRepository<LeaveBlockList, dbEduegateHRContext>(dbContext);

                if (entity.LeaveBlockListIID == 0)
                {
                    var maxGroupID = repository.GetMaxID(a => a.LeaveBlockListIID);
                    entity.LeaveBlockListIID = int.Parse(maxGroupID.ToString()) + 1;
                    entity = repository.Insert(entity);
                }
                else
                {
                    entity = repository.Update(entity);
                }
            }

            return ToDTOString(new LeaveBlockDTO()
            {
                
            });
        }

    }
}
