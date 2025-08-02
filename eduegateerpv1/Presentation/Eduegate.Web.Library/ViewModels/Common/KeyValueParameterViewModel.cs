using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Common
{
    public class KeyValueParameterViewModel
    {
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }

        public static List<KeyValueParameterViewModel> GetParameterCollection(object obj)
        {
            var parameters = new List<KeyValueParameterViewModel>();

            foreach(var property in obj.GetType().GetProperties())
            {
                if (property.MemberType == System.Reflection.MemberTypes.Property)
                {
                    parameters.Add(new KeyValueParameterViewModel() { ParameterName = property.Name, ParameterValue = property.GetValue(obj) != null ? property.GetValue(obj).ToString() : null });
                }
            }

            return parameters;
        }

        public static string TryGetValue(List<KeyValueParameterViewModel> parameter, string name, string defaultValue = null)
        {
            var parameterObject = parameter !=null ? parameter.FirstOrDefault(a => a.ParameterName.Equals(name)):null;

            if (parameterObject == null) return defaultValue;
            if(string.IsNullOrEmpty(parameterObject.ParameterValue)) return defaultValue;
            return parameterObject.ParameterValue;
        }

        public static T TryGetValue<T>(List<KeyValueParameterViewModel> parameter, string name, T defaultValue = default(T))
        {
             return (T)Convert.ChangeType(TryGetValue(parameter, name, defaultValue.ToString()), typeof(T));
        }
    }
}
