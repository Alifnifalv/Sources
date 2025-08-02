using System;
using System.Runtime.Remoting.Messaging;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain.Mappers
{
    public class CustomerGroupMapper : IDTOEntityMapper<CustomerGroupDTO, CustomerGroup>
    {
        private CallContext _context;
        public static CustomerGroupMapper Mapper(CallContext context)
        {
            var mapper = new CustomerGroupMapper();
            mapper._context = context;
            return mapper;
        }

        public CustomerGroupDTO ToDTO(CustomerGroup entity)
        {
            return new CustomerGroupDTO()
            {
                CustomerGroupIID = entity.CustomerGroupIID,
                GroupName = entity.GroupName,
                PointLimit = entity.PointLimit
            };
        }
        public CustomerGroup ToEntity(CustomerGroupDTO dto)
        {
            throw new NotImplementedException();
        }

    }
}