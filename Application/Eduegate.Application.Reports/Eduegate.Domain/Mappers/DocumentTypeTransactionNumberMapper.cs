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
    public class DocumentTypeTransactionNumberMapper : IDTOEntityMapper<DocumentTypeTransactionNumberDTO, DocumentTypeTransactionNumber>
    {
        private CallContext _context;

        public static DocumentTypeTransactionNumberMapper Mapper(CallContext context)
        {
            var mapper = new DocumentTypeTransactionNumberMapper();
            mapper._context = context;
            return mapper;
        }

        public DocumentTypeTransactionNumberDTO ToDTO(DocumentTypeTransactionNumber entity)
        {
            return new DocumentTypeTransactionNumberDTO()
            {
                
                DocumentTypeID = entity.DocumentTypeID,
                Month = entity.Month,
                Year=entity.Year,
                LastTransactionNo = entity.LastTransactionNo==null?0: (long)entity.LastTransactionNo
            };
        }

        public List<DocumentTypeTransactionNumberDTO> ToDTO(List<DocumentTypeTransactionNumber> entities)
        {
            return entities.Select(x => ToDTO(x)).ToList();
        }

        public DocumentTypeTransactionNumber ToEntity(DocumentTypeTransactionNumberDTO dto)
        {
            var entity = new DocumentTypeTransactionNumber()
            {              
                DocumentTypeID = dto.DocumentTypeID,               
                LastTransactionNo = dto.LastTransactionNo,
                Month = dto.Month.Value,
                Year=dto.Year.Value
            };

            return entity;
        }

        public List<DocumentTypeTransactionNumber> ToEntity(List<DocumentTypeTransactionNumberDTO> dtos)
        {
            return dtos.Where(y=> y.Month.HasValue && y.Year.HasValue).Select(x => ToEntity(x)).ToList();
        }
    }
}
