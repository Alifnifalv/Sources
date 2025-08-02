using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Oracle.Models;
using Eduegate.Domain.Repository.Oracle;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain.Mappers.EmploymentService
{
    public class EmploymentDocumentMapper : IDTOEntityMapper<DocumentFileDTO, HR_EMP_REQ_DOCUMENTS>
    {
        private CallContext _context;
        public static EmploymentDocumentMapper Mapper(CallContext context)
        {
            var mapper = new EmploymentDocumentMapper();
            mapper._context = context;
            return mapper;
        }

         public HR_EMP_REQ_DOCUMENTS ToEntity(DocumentFileDTO dto)
        {

            return new HR_EMP_REQ_DOCUMENTS()
            {
                FILEUPLOADTYPEID = dto.DocumentFileIID,
                FILENAME = dto.FileName,
            };
        }

         public DocumentFileDTO ToDTO(HR_EMP_REQ_DOCUMENTS entity)
        {
            var fileUploadType = new EmploymentServiceRepository().GetDocType(long.Parse(entity.FILEUPLOADTYPEID.ToString()));
            return new DocumentFileDTO()
            {
                FileName = entity.FILENAME,
                DocumentFileIID = long.Parse(entity.FILEUPLOADTYPEID.ToString()),
                Description = fileUploadType.FILEUPLOADTYPENAME
            };
        }

        public List<DocumentFileDTO> ToDTOList(List<HR_EMP_REQ_DOCUMENTS> entitylist)
        {

            var list = new List<DocumentFileDTO>();
            foreach (var entity in entitylist)
            {
                list.Add(ToDTO(entity));
            }
            return list;
        }
    }
}
