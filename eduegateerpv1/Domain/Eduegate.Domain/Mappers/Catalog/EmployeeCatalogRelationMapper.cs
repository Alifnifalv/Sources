using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Domain.Mappers.HR
{
    public class EmployeeCatalogRelationMapper
    {
        public static EmployeeCatalogRelationDTO ToDto(Entity.Models.EmployeeCatalogRelation entity)
        {
            if (entity != null)
            {
                return new EmployeeCatalogRelationDTO()
                {
                    EmployeeCatalogRelationsID = entity.EmployeeCatalogRelationsIID,
                    RelationTypeID = entity.RelationTypeID,
                    RelationID = entity.RelationID,
                    EmployeeID = entity.EmployeeID,
                };
            }
            else
                return new EmployeeCatalogRelationDTO();
        }


        public static Entity.Models.EmployeeCatalogRelation ToEntity(EmployeeCatalogRelationDTO dto)
        {
            if (dto != null)
            {
                return new Entity.Models.EmployeeCatalogRelation()
                {
                    EmployeeCatalogRelationsIID = dto.EmployeeCatalogRelationsID,
                    RelationTypeID = dto.RelationTypeID,
                    RelationID = dto.RelationID,
                    EmployeeID = dto.EmployeeID,
                    CreatedDate = dto.CreatedDate,
                    CreatedBy = dto.CreatedBy,
                };
            }
            else
                return new Entity.Models.EmployeeCatalogRelation();
        }
    }
}
