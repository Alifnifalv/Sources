
using System;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Domain.Mappers
{
    class TransactionAllocationMapper : IDTOEntityMapper<TransactionAllocationDTO, TransactionAllocation>
    {
        public static TransactionAllocationMapper Mapper()
        {
            var mapper = new TransactionAllocationMapper();
            //mapper._context = context;
            return mapper;
        }

        public TransactionAllocationDTO ToDTO(TransactionAllocation entity)
        {
            if (entity != null)
            {
                return new TransactionAllocationDTO()
                {
                   
                    TransactionAllocationIID = entity.TransactionAllocationIID,
                    BranchID = Convert.ToInt32(entity.BranchID),
                    Quantity = entity.Quantity,
                    TrasactionDetailID = entity.TrasactionDetailID,
                    
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                    TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                };
            }
            else
            {
                return new TransactionAllocationDTO();
            }
        }

        public TransactionAllocation ToEntity(TransactionAllocationDTO dto)
        {
            if (dto != null)
            {
                return new TransactionAllocation()
                {
                    TransactionAllocationIID = dto.TransactionAllocationIID,
                    BranchID = dto.BranchID,
                    Quantity = dto.Quantity,
                    TrasactionDetailID = dto.TrasactionDetailID,

                   // CreatedBy = dto.TransactionAllocationIID > 0 ? dto.CreatedBy : Convert.ToInt32(_context.LoginID),
                    CreatedDate = dto.TransactionAllocationIID > 0 ? dto.CreatedDate : DateTime.Now,
                    //UpdatedBy = Convert.ToInt32(_context.LoginID),
                    UpdatedDate = DateTime.Now,
                    TimeStamps = string.IsNullOrEmpty(dto.TimeStamps) ? null : Convert.FromBase64String(dto.TimeStamps),
                };
            }
            else
            {
                return new TransactionAllocation();
            }
        }
    }
}
