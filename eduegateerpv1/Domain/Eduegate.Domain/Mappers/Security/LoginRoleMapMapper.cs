using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Admin;

namespace Eduegate.Domain.Mappers.Security
{
    public class LoginRoleMapMapper : IDTOEntityMapper<UserRoleDTO, Eduegate.Domain.Entity.Models.LoginRoleMap>
    {
        private CallContext _context;

        public static LoginRoleMapMapper Mapper(CallContext context)
        {
            var mapper = new LoginRoleMapMapper();
            mapper._context = context;
            return mapper;
        }

        public List<UserRoleDTO> ToDTO(List<Entity.Models.LoginRoleMap> entities)
        {
            var roles = new List<UserRoleDTO>();

            foreach (var entity in entities)
            {
                roles.Add(ToDTO(entity));
            }

            return roles;
        }

        public UserRoleDTO ToDTO(Entity.Models.LoginRoleMap entity)
        {
            return new UserRoleDTO() {
                 RoleID = entity.RoleID.Value,
                 RoleName = entity.Role.RoleName
            };
        }

        public Entity.Models.LoginRoleMap ToEntity(UserRoleDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
