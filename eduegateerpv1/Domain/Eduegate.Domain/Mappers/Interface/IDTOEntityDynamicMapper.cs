using Eduegate.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers.Interface
{
    public interface IDTOEntityDynamicMapper
    {
        BaseMasterDTO ToDTO(string entity);
        string SaveEntity(BaseMasterDTO dto);
    }
}
