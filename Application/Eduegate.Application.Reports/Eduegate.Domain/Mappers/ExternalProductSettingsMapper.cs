using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Domain.Mappers
{
    public class ExternalProductSettingsMapper : IDTOEntityMapper<ExternalProductSettingsDTO, CustomerProductReference>
    {
        public ExternalProductSettingsDTO ToDTO(CustomerProductReference entity)
        {
            if (entity != null)
            {
                return new ExternalProductSettingsDTO() {
                    
                };
            }
            else
                return new ExternalProductSettingsDTO();
        }

        public CustomerProductReference ToEntity(ExternalProductSettingsDTO dto)
        {
            if (dto != null)
            {
                return new CustomerProductReference()
                {

                };
            }
            else
                return new CustomerProductReference();
        }
    }
}
