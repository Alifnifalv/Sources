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
    public class CustomerSupportTicketMapper : IDTOEntityMapper<CustomerSupportTicketDTO, CustomerSupportTicket>
    {
        private CallContext _context;

        public static CustomerSupportTicketMapper Mapper(CallContext context)
        {
            var mapper = new CustomerSupportTicketMapper();
            mapper._context = context;
            return mapper;
        }

        public CustomerSupportTicketDTO ToDTO(CustomerSupportTicket entity)
        {
            throw new NotImplementedException();
        }

        public CustomerSupportTicket ToEntity(CustomerSupportTicketDTO dto)
        {
            var entity = new CustomerSupportTicket()
            {
                Comments = dto.Comments,
                CultureID = dto.CultureID,
                EmailID = dto.EmailID,
                IPAddress = dto.IPAddress,
                Name = dto.Name,
                Subject = dto.Subject,
                Telephone = dto.Telephone,
                TransactionNo = dto.TransactionNo,
                CreatedBy = _context.LoginID != null ? int.Parse(_context.LoginID.ToString()) : 0,
                CreatedDate = DateTime.Now,
                CompanyID = dto.CompanyID
            };

            if (!dto.CreatedBy.HasValue)
            {
                entity.CreatedBy = _context.LoginID != null ? int.Parse(_context.LoginID.ToString()) : 0;
                entity.CreatedDate = DateTime.Now;
                entity.CompanyID = _context.CompanyID;
            }

            return entity;
        }
    }
}
