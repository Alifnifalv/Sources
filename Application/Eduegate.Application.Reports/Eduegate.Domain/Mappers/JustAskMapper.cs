using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Supports;

namespace Eduegate.Domain.Mappers
{
    public class JustAskMapper : IDTOEntityMapper<JustAskDTO, CustomerJustAsk>
    {
        private CallContext _context;

        public static JustAskMapper Mapper(CallContext context)
        {
            var mapper = new JustAskMapper();
            mapper._context = context;
            return mapper;
        }
        public JustAskDTO ToDTO(CustomerJustAsk entity)
        {
            throw new NotImplementedException();
        }

        public CustomerJustAsk ToEntity(JustAskDTO dto)
        {
            var entity = new CustomerJustAsk()
            {
                CreatedBy = _context.LoginID != null ? int.Parse(_context.LoginID.ToString()) : 0,
                CreatedDate = DateTime.Now,
                CultureID  = dto.CultureID,
                Description = dto.Description,
                EmailID = dto.EmailID,
                IPAddress = dto.IPAddress,
                Name = dto.Name,
                Telephone = dto.Telephone,
            };

            if (!dto.CreatedBy.HasValue)
            {
                entity.CreatedBy = _context.LoginID != null ? int.Parse(_context.LoginID.ToString()) : 0;
                entity.CreatedDate = DateTime.Now;
            }

            return entity;
        }
    }
}
