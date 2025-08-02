using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain.Mappers
{
    public class DocumentTypeMapper : IDTOEntityMapper<DocumentTypeDTO, DocumentType>
    {
        private CallContext _context;

        public static DocumentTypeMapper Mapper(CallContext context)
        {
            var mapper = new DocumentTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public DocumentTypeDTO ToDTO(DocumentType entity)
        {
            if(entity == null)
            {
                return null;
            }

            return new DocumentTypeDTO()
            {
                UpdatedBy = entity.UpdatedBy.HasValue ? (int?) int.Parse(entity.UpdatedBy.ToString()) : null,
                CreatedBy = entity.CreatedBy.HasValue? (int?) int.Parse(entity.CreatedBy.ToString()) : null,
                //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                DocumentTypeID = entity.DocumentTypeID,
                ReferenceTypeID = entity.ReferenceTypeID,
                TransactionTypeName = entity.TransactionTypeName,
                TransactionNoPrefix = entity.TransactionNoPrefix,
                LastTransactionNo = entity.LastTransactionNo,
                TransactionSequenceType = entity.TransactionSequenceType,
                IgnoreInventoryCheck = entity.IgnoreInventoryCheck,
                TaxTamplateID = entity.TaxTemplateID,
                ApprovalWorkflow = entity.Workflow == null ? null : new Eduegate.Framework.Contracts.Common.KeyValueDTO()
                {
                     Key = entity.WorkflowID.ToString(),
                     Value = entity.Workflow.WokflowName,
                },
                System = entity.System
            };
        }

        public List<DocumentTypeDTO> ToDTO(List<DocumentType> entities)
        {
            return entities.Select(x => ToDTO(x)).ToList();
        }

        public DocumentType ToEntity(DocumentTypeDTO dto)
        {
            var entity = new DocumentType()
            {
                UpdatedDate = DateTime.Now,
                //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                UpdatedBy = int.Parse(_context.LoginID.ToString()),
                DocumentTypeID = dto.DocumentTypeID,
                ReferenceTypeID = dto.ReferenceTypeID,
                TransactionTypeName = dto.TransactionTypeName,
                TransactionNoPrefix = dto.TransactionNoPrefix,
                LastTransactionNo = dto.LastTransactionNo,
                System = dto.System,
                CompanyID = _context.CompanyID,
                TransactionSequenceType = dto.TransactionSequenceType,
                WorkflowID = string.IsNullOrEmpty(dto.ApprovalWorkflow.Key) ? (long?)null : long.Parse(dto.ApprovalWorkflow.Key),
                IgnoreInventoryCheck = dto.IgnoreInventoryCheck,
                TaxTemplateID = dto.TaxTamplateID,
            };

            if (!dto.CreatedBy.HasValue)
            {
                entity.CreatedBy = int.Parse(_context.LoginID.ToString());
                entity.CreatedDate = DateTime.Now;
            }

            return entity;
        }
    }
}
