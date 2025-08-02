using Eduegate.Services.Contracts.Salon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Eduegate.Services.Contracts.Appointments
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAppointmentService" in both code and config file together.
    [ServiceContract]
    public interface IAppointmentService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAppointments")]
        List<AppointmentDTO> GetAppointments();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAppointment?appointmentID={appointmentID}")]
        AppointmentDTO GetAppointment(long appointmentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAppointments?loginID={loginID}")]
        List<AppointmentDTO> GetAppointmentsByLoginID(long loginID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
             RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveAppointments")]
        List<AppointmentDTO> SaveAppointments(List<AppointmentDTO> appointments);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveAppointments")]
        AppointmentDTO SaveAppointments(AppointmentDTO appointment);
    }
}
