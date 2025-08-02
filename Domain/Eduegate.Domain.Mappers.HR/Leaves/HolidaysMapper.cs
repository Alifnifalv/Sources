using Newtonsoft.Json;
using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.HR.Leaves;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.HR.Leaves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class HolidaysMapper : DTOEntityDynamicMapper
    {   
        private CallContext _context;

        public static  HolidaysMapper Mapper(CallContext context)
        {
            var mapper = new  HolidaysMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<HolidaysDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var repository = new EntiyRepository<Holidays, dbEduegateHRContext>(dbContext);
                var entity = repository.GetById(IID);

                return ToDTOString(new HolidaysDTO()
                {
                HolidayIID = entity. HolidayIID,
                HolidayListID = entity. HolidayListID,
                HolidayDate = entity. HolidayDate,
                Description = entity. Description,
                CreatedBy = entity. CreatedBy,
                UpdatedBy = entity. UpdatedBy,
                CreatedDate = entity. CreatedDate,
                UpdatedDate = entity. UpdatedDate,
                TimeStamps = entity. TimeStamps,
  
                });
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as HolidaysDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new Holidays()
            {
                HolidayIID = toDto. HolidayIID,
                HolidayListID = toDto. HolidayListID,
                HolidayDate = toDto. HolidayDate,
                Description = toDto. Description,
                CreatedBy = toDto. CreatedBy,
                UpdatedBy = toDto. UpdatedBy,
                CreatedDate = toDto. CreatedDate,
                UpdatedDate = toDto. UpdatedDate,
                TimeStamps = toDto. TimeStamps,
                       
            };

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var repository = new EntiyRepository<Holidays, dbEduegateHRContext>(dbContext);

                if (entity.LeaveApplicationIID == 0)
                {
                    var maxGroupID = repository.GetMaxID(a => a.HolidaysIID);
                    entity.HolidaysIID = int.Parse(maxGroupID.ToString()) + 1;
                    entity = repository.Insert(entity);
                }
                else
                {
                    entity = repository.Update(entity);
                }
            }

            return ToDTOString(new HolidaysDTO()
            {
                HolidayIID = entity. HolidayIID,
                HolidayListID = entity. HolidayListID,
                HolidayDate = entity. HolidayDate,
                Description = entity. Description,
                CreatedBy = entity. CreatedBy,
                UpdatedBy = entity. UpdatedBy,
                CreatedDate = entity. CreatedDate,
                UpdatedDate = entity. UpdatedDate,
                TimeStamps = entity. TimeStamps,
                   
            });
        }       
    }
}




