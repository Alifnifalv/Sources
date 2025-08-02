using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Commons;
using System.Collections.Generic;

namespace Eduegate.Notification.Email.ViewModels
{
    public class KeyValueParameterViewModel
    {
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }
        public object ParameterObject { get; set; }

        public static List<KeyValueParameterViewModel> GetParameterCollection(object obj)
        {
            var parameters = new List<KeyValueParameterViewModel>();

            foreach(var property in obj.GetType().GetProperties())
            {
                if (property.MemberType == System.Reflection.MemberTypes.Property)
                {
                    // we don't need to add AdditionalParameters we have to add property of AdditionalParameters
                    if (property.Name != "AdditionalParameters")
                    {
                        parameters.Add(new KeyValueParameterViewModel() { 
                            ParameterName = property.Name, 
                            ParameterValue = property.GetValue(obj) != null ? property.GetValue(obj).ToString() : null });
                    }
                }
            }

            return parameters;
        }

        public static List<KeyValueParameterViewModel> ToParameterVM(List<KeyValueParameterDTO> listObject)
        {
            var parameters = new List<KeyValueParameterViewModel>();

            if (listObject != null)
            {
                foreach (var obj in listObject)
                {
                    parameters.Add(new KeyValueParameterViewModel() { 
                        ParameterName = obj.ParameterName, 
                        ParameterValue = obj.ParameterValue.IsNotNullOrEmpty() ? obj.ParameterValue : null,
                        ParameterObject = obj.ParameterObject
                    });
                }
            }

            return parameters;
        }
    }
}
