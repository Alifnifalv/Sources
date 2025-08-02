using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Salon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Appointments
{
    public class AppointmentViewModel : BaseMasterViewModel
    {
        public static AppointmentViewModel FromDTO(AppointmentDTO dto)
        {
            Mapper<AppointmentDTO, AppointmentViewModel>.CreateMap();
            return Mapper<AppointmentDTO, AppointmentViewModel>.Map(dto);
        }

        public static AppointmentDTO ToDTO(AppointmentViewModel vm)
        {
            Mapper<AppointmentViewModel, AppointmentDTO>.CreateMap();
            var mapper = Mapper<AppointmentViewModel, AppointmentDTO>.Map(vm);
            return mapper;
        }
    }
}
