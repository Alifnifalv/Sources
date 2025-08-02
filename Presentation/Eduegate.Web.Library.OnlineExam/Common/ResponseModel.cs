using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;

namespace Eduegate.Web.Library.OnlineExam.Common
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Response", "CRUDModel.ViewModel")]
    [DisplayName("Response")]
    public class ResponseModel : BaseMasterViewModel
    {
        public ResponseModel()
        {
        }

        // [Bind(Prefix = "some_prefix")]
        public string Message { set; get; }

        public object Data { set; get; }

        public bool isError { get; set; }

    }
}