using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;
using Eduegate.Framework;

namespace Eduegate.Domain.Mappers
{
    public class DataFeedMapper
    {
        private CallContext _context;

        public static DataFeedMapper Mapper(CallContext context)
        {
            var mapper = new DataFeedMapper();
            mapper._context = context;
            return mapper;
        }

        public ProductInventoryDTO ToProductInventoryDTO(ProductInventory entity)
        {
            throw new NotImplementedException();
        }

        public ProductInventory ToProductInventoryEntity(ProductInventoryDTO dto)
        {
            ProductInventory productInventory = new ProductInventory()
            {
                ProductSKUMapID = dto.ProductSKUMapID,
                Batch = (long)dto.Batch,
                BranchID = (long)dto.BranchID,
                Quantity = dto.Quantity,
                UpdatedBy = (int)_context.LoginID,
                UpdatedDate = DateTime.Now,
            };
            return productInventory;
        }

        public DataFeedTypeDTO ToDataFeedTypeDTO(DataFeedType dataFeedType)
        {
            DataFeedTypeDTO dataFeedTypeDTO = new DataFeedTypeDTO()
            {
                DataFeedTypeID = dataFeedType.DataFeedTypeID,
                Name = dataFeedType.Name,
                TemplateName = dataFeedType.TemplateName
            };
            return dataFeedTypeDTO;
        }

        public DataFeedLog ToDataFeedLogEntity(DataFeedLogDTO dto)
        {
            DataFeedLog dataFeedLog = new DataFeedLog();

            dataFeedLog.DataFeedStatusID = (short)dto.DataFeedStatusID;
            dataFeedLog.DataFeedTypeID = (int)dto.DataFeedTypeID;
            dataFeedLog.FileName = dto.FileName;
            dataFeedLog.CompanyID = dto.CompanyID != null ? dto.CompanyID : _context.CompanyID;

            if (dto.DataFeedLogID == default(long))
            {
                dataFeedLog.CreatedBy = (int)_context.LoginID;
                dataFeedLog.CreatedDate = DateTime.Now;
            }
            else
            {
                dataFeedLog.DataFeedLogIID = dto.DataFeedLogID;
                dataFeedLog.UpdatedBy = (int)_context.LoginID;
                dataFeedLog.UpdatedDate = DateTime.Now;
            }

            return dataFeedLog;
        }

        public DataFeedLogDTO ToDataFeedLogDTO(DataFeedLog dataFeedLog)
        {
            if(dataFeedLog == null) return null;
            dataFeedLog.DataFeedStatusID = dataFeedLog.DataFeedStatusID.HasValue ? dataFeedLog.DataFeedStatusID : 1;
            dataFeedLog.DataFeedTypeID = dataFeedLog.DataFeedTypeID.HasValue ? dataFeedLog.DataFeedTypeID : 1;
            DataFeedLogDTO dataFeedLogDTO = new DataFeedLogDTO()
            {
                DataFeedLogID = dataFeedLog.DataFeedLogIID,
                DataFeedStatusID = (Services.Contracts.Enums.DataFeedStatus)dataFeedLog.DataFeedStatusID,
                DataFeedTypeID = dataFeedLog.DataFeedTypeID.HasValue ? dataFeedLog.DataFeedTypeID.Value : 1,
                DataFeedType = (Services.Contracts.Enums.DataFeedTypes)dataFeedLog.DataFeedTypeID,
                FileName = dataFeedLog.FileName,
                CreatedBy = dataFeedLog.CreatedBy,
                DataFeedTypeName = dataFeedLog.DataFeedTypeID.HasValue ? dataFeedLog.DataFeedType?.Name : string.Empty,
                DataFeedStatusName = dataFeedLog.DataFeedStatusID.HasValue ? dataFeedLog.DataFeedStatus?.StatusName : string.Empty,
                CompanyID = dataFeedLog.CompanyID
            };
            return dataFeedLogDTO;
        }
    }
}
