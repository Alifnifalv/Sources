using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Accounts;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Accounts;
using Eduegate.Services.Contracts.Accounts.Accounting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers.Accounts
{
    public class ExpenditureAllocationMapper : DTOEntityDynamicMapper
    {
        public static ExpenditureAllocationMapper Mapper(CallContext context)
        {
            var mapper = new ExpenditureAllocationMapper();
            mapper._context = context;
            return mapper;
        }

        public List<TransactionAllocationHeadDTO> ToDTO(List<TransactionAllocationHead> entities)
        {
            var dtos = new List<TransactionAllocationHeadDTO>();
            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }
            return dtos;
        }

        public TransactionAllocationHeadDTO ToDTO(TransactionAllocationHead entity)
        {
            return new TransactionAllocationHeadDTO()
            {
                TransactionAllocationIID = entity.TransactionAllocationIID,
                TransactionDate = entity.TransactionDate,
            };
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<TransactionAllocationHeadDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto as TransactionAllocationHeadDTO);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private TransactionAllocationHeadDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entity = dbContext.TransactionAllocationHeads.Where(a => a.TransactionAllocationIID == IID)
                    .Include(i => i.TransactionAllocationDetails)
                    .Include(i => i.TransactionAllocationDetails).ThenInclude(x => x.Account)
                    .Include(i => i.TransactionAllocationDetails).ThenInclude(x => x.CostCenter)
                    .AsNoTracking()
                    .FirstOrDefault();

                var dto = new TransactionAllocationHeadDTO()
                {
                    TransactionAllocationIID = entity.TransactionAllocationIID,
                    TransactionDate = entity.TransactionDate,                   
                };

                dto.TransactionAllocationDetailDTO = new List<TransactionAllocationDetailDTO>();
                foreach (var map in entity.TransactionAllocationDetails)
                {
                    dto.TransactionAllocationDetailDTO.Add(new TransactionAllocationDetailDTO()
                    {
                        TransactionAllocationDetailIID = map.TransactionAllocationDetailIID,
                        AccountTransactionHeadID = map.AccountTransactionHeadID,
                        Amount = map.Amount,
                        TransactionAllocationID = map.TransactionAllocationID,
                        Remarks = map.Remarks,
                        Account = map.AccountID.HasValue ? new KeyValueDTO()
                        {
                            Key = map.AccountID.ToString(),
                            Value = map.Account.AccountName
                        } : new KeyValueDTO(),
                        CostCenter = map.CostCenterID.HasValue ? new KeyValueDTO()
                        {
                            Key = map.CostCenterID.ToString(),
                            Value = map.CostCenter.CostCenterName
                        } : new KeyValueDTO(),
                    });
                }

                return dto;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as TransactionAllocationHeadDTO;
            //convert the dto to entity and pass to the repository.

            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                var entity = new TransactionAllocationHead()
                {
                    TransactionAllocationIID = toDto.TransactionAllocationIID,
                    TransactionDate = toDto.TransactionDate.HasValue ? toDto.TransactionDate: (DateTime)DateTime.Now,
                   
                    CreatedBy = toDto.TransactionAllocationIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.TransactionAllocationIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.TransactionAllocationIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.TransactionAllocationIID > 0 ? DateTime.Now : dto.UpdatedDate,
                    ////TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                };

                entity.TransactionAllocationDetails = new List<TransactionAllocationDetail>();
                foreach (var dat in toDto.TransactionAllocationDetailDTO)
                {
                    entity.TransactionAllocationDetails.Add(new TransactionAllocationDetail()
                    {
                        TransactionAllocationDetailIID = dat.TransactionAllocationDetailIID,
                        AccountTransactionHeadID = dat.AccountTransactionHeadID,
                        Amount = dat.Amount,
                        TransactionAllocationID = dat.TransactionAllocationID,
                        Remarks = dat.Remarks,
                        AccountID= dat.AccountID ,
                        CostCenterID =dat.CostCenterID
                    });
                }

                var IIDs = toDto.TransactionAllocationDetailDTO
                        .Select(a => a.TransactionAllocationDetailIID).ToList();

                //delete maps
                var entities = dbContext.TransactionAllocationDetails.Where(x =>
                    x.TransactionAllocationID == entity.TransactionAllocationIID &&
                    !IIDs.Contains(x.TransactionAllocationDetailIID)).AsNoTracking().ToList();

                if (entities.IsNotNull() && entities.Count()>0)
                    dbContext.TransactionAllocationDetails.RemoveRange(entities);


                if (entity.TransactionAllocationIID == 0)
                {                 

                    dbContext.TransactionAllocationHeads.Add(entity);

                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    foreach (var map in entity.TransactionAllocationDetails)
                    {
                        if (map.TransactionAllocationDetailIID == 0)
                        {
                            dbContext.Entry(map).State = EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(map).State = EntityState.Modified;
                        }
                    }

                    dbContext.Entry(entity).State = EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.TransactionAllocationIID));
            }
        }

    }
}