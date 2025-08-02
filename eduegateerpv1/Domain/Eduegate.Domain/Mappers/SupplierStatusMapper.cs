using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Domain.Mappers
{
    public class SupplierStatusMapper : IDTOEntityMapper<SupplierStatusDTO, SupplierStatus>
    {
        public static SupplierStatusMapper Mapper { get { return new SupplierStatusMapper(); } }

        public SupplierStatusDTO ToDTO(SupplierStatus entity)
        {
            if (entity != null)
            {
                return new SupplierStatusDTO()
                {
                    SupplierStatusID = entity.SupplierStatusID,
                    StatusName = entity.StatusName,
                };
            }
            else
            {
                return new SupplierStatusDTO();
            }
        }


        public SupplierStatus ToEntity(SupplierStatusDTO dto)
        {
            if (dto != null)
            {
                return new SupplierStatus()
                {
                    SupplierStatusID = dto.SupplierStatusID,
                    StatusName = dto.StatusName,
                };
            }
            else
            {
                return new SupplierStatus();
            }
        }


    }
}
