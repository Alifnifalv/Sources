using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Logging;
using Eduegate.Domain.Entity.Logging.Models;

namespace Eduegate.Domain.Mappers.Logging
{
    public class CatalogLoggerMapper : IDTOEntityMapper<CatalogLoggerDTO, CatalogLogger>
    {
        public static CatalogLoggerMapper Mapper()
        {
            var mapper = new CatalogLoggerMapper();
            return mapper;
        }

        public CatalogLoggerDTO ToDTO(CatalogLogger entity)
        {
            return null;
        }

        public CatalogLogger ToEntity(CatalogLoggerDTO dto)
        {
            CatalogLogger catalogLogger = new CatalogLogger()
            {
                ProductSKUMapID = dto.ProductSKUMapID,
                OperationTypeID = dto.OperationTypeID,
                LogValue = dto.LogValue,
                SolrCore = dto.SolrCore,
                CreatedDate = DateTime.Now
            };
            return catalogLogger;
        }
    }
}
