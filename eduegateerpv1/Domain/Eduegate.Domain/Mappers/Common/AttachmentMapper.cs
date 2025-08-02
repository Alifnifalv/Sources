using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Mutual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers.Common
{
    public class AttachmentsMapper 
    {
        public static AttachmentsMapper Mapper()
        {
            var mapper = new AttachmentsMapper();
            return mapper;
        }

        public AttachmentDTO ToDTO(Attachment entity)
        {
            throw new NotImplementedException();
        }

        public Attachment ToEntity(AttachmentDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
