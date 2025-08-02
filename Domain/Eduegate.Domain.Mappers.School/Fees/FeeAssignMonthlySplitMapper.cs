using Newtonsoft.Json;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
namespace Eduegate.Domain.Mappers.School.Fees
{
    public  class FeeAssignMonthlySplitMapper : DTOEntityDynamicMapper
    {
        public static FeeAssignMonthlySplitMapper Mapper(CallContext context)
        {
            var mapper = new FeeAssignMonthlySplitMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FeeMasterDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

    }
}