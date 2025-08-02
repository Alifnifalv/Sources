using Eduegate.Domain.Mappers.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain.Mappers
{
    public class DTOEntityDynamicMapper : IDTOEntityDynamicMapper
    {
        public virtual BaseMasterDTO ToDTO(string entity)
        {
            throw new NotImplementedException();
        }

        public virtual string ToDTOString(BaseMasterDTO dto)
        {
            throw new NotImplementedException();
        }

        public virtual string GetEntity(long IID)
        {
            throw new NotImplementedException();
        }

        public virtual string SaveEntity(BaseMasterDTO dto)
        {
            throw new NotImplementedException();
        }

        public static DTOEntityDynamicMapper GetMapper(string fullName)
        {
            Type type = Type.GetType(fullName, true);
            return (DTOEntityDynamicMapper)Activator.CreateInstance(type);
        }
    }
}
