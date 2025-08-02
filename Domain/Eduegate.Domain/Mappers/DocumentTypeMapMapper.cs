using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain.Mappers
{
    public class DocumentTypeMapMapper : IDTOEntityMapper<DocumentTypeTypeDTO, DocumentTypeType>
    {
        private CallContext _context;

        public static DocumentTypeMapMapper Mapper(CallContext context)
        {
            var mapper = new DocumentTypeMapMapper();
            mapper._context = context;
            return mapper;
        }

        public List<DocumentTypeTypeDTO> ToDTO(List<DocumentTypeType> entities)
        {
            return entities.Select(x => ToDTO(x)).ToList();
        }

        public DocumentTypeType ToEntity(DocumentTypeTypeDTO dto)
        {
            var entity = new DocumentTypeType()
            {
                DocumentTypeID = dto.DocumentTypeID,
                DocumentTypeMapID = dto.DocumentTypeMapID,
                DocumentTypeTypeMapIID = dto.DocumentTypeTypeMapIID,
                UpdatedDate = DateTime.Now,
                //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                UpdatedBy = int.Parse(_context.LoginID.ToString()),
            };
            if (!dto.CreatedBy.HasValue)
            {
                entity.CreatedBy = int.Parse(_context.LoginID.ToString());
                entity.CreatedDate = DateTime.Now;
            }

            return entity;
        }

        public List<DocumentTypeType> ToEntity(List<DocumentTypeTypeDTO> dtos)
        {
            var Documentmap = new List<DocumentTypeType>();

            foreach (var dto in dtos)
            {
                Documentmap.Add(ToEntity(dto));
            }

            return Documentmap;
        }

        public DocumentTypeTypeDTO ToDTO(DocumentTypeType entity)
        {
            return new DocumentTypeTypeDTO()
            {
                DocumentTypeTypeMapIID = entity.DocumentTypeTypeMapIID,
                DocumentTypeID = entity.DocumentTypeID,
                DocumentTypeMapID = entity.DocumentTypeMapID,
                DocumentTypeMapName = new MetadataRepository().GetDocumentTypeName(entity.DocumentTypeMapID.IsNotNull() ? (int)entity.DocumentTypeMapID : default(int)),
                UpdatedBy = entity.UpdatedBy.HasValue ? (int?)int.Parse(entity.UpdatedBy.ToString()) : null,
                CreatedBy = entity.CreatedBy.HasValue ? (int?)int.Parse(entity.CreatedBy.ToString()) : null,
                //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
            };
        }
    }
}

