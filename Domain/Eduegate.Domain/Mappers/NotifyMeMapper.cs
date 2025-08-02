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
    public class NotifyMeMapper 
    {
        public static Notify ToEntity(NotifyMeDTO dto)
        {
            //Mapper.CreateMap<NotifyMeDTO,Notify>();

            //TODO: Map the DTO to Entity
            //return Mapper.Map<NotifyMeDTO, Notify>(dto);
            return null;
        }
    }
}
