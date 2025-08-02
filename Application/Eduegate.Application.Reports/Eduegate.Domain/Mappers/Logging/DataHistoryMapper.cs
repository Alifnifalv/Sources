using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Logging;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Domain.Mappers.Logging
{
    public class DataHistoryMapper : IDTOEntityMapper<DataHistoryEntityDTO, DataHistoryEntity>
    {

        private CallContext _context;

        public static DataHistoryMapper Mapper(CallContext context)
        {
            var mapper = new DataHistoryMapper();
            mapper._context = context;
            return mapper;
        }

        public DataHistoryEntityDTO ToDTO(DataHistoryEntity entity)
        {
            var dto = new DataHistoryEntityDTO();

            if (entity.IsNotNull())
            {
                entity.DataHistoryEntityID = dto.DataHistoryEntityID;
                entity.Name = dto.Name;
                entity.Description = dto.Description;
                entity.TableName = dto.TableName;
                entity.DBName = dto.DBName;
                entity.SchemaName = dto.SchemaName;
                entity.LoggerTableName = dto.LoggerTableName;
                entity.LoggerDBName = dto.LoggerDBName;
                entity.LoggerSchemaName = dto.LoggerSchemaName;
            }

            return dto;
        }

        public DataHistoryEntity ToEntity(DataHistoryEntityDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
