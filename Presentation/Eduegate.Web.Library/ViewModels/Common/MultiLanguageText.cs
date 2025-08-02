using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Web.Library.ViewModels.Common
{
    public class MultiLanguageText
    {
        private string _text;

        public MultiLanguageText()
        {

        }

        public MultiLanguageText(List<CultureDataInfoViewModel> datas)
        {
            CultureDatas = datas;
        }

        public override string ToString()
        {
            return Text;
        }

        //TODO: Need to check if this is required
        //[AllowHtml]
        public string Text
        {
            get
            {
                if (string.IsNullOrEmpty(_text))
                {
                    return _text;
                }
                else
                {
                    if (CultureDatas != null && CultureDatas.Count > 1)
                        return CultureDatas[0].CultureValue;
                    else return _text;
                }
            }

            set
            {
                _text = value;
            }
        }

        //TODO: Need to check if this is required
        //[AllowHtml]
        public List<CultureDataInfoViewModel> CultureDatas { get; set; }

        public string GetValueByCultureID(byte cutlureID)
        {
            var value = CultureDatas!=null? CultureDatas.FirstOrDefault(a => a.CultureID == cutlureID):null;
            return value == null ? null : value.CultureValue;
        }

        public string GetTimeStampByCultureID(byte cutlureID)
        {
            var value = CultureDatas!=null? CultureDatas.FirstOrDefault(a => a.CultureID == cutlureID):null;
            return value == null ? null : value.TimeStamps;
        }

        public void SetValueByCultureID(CultureDataInfoViewModel vm, string dataValue, string timeStamps)
        {
            var value = CultureDatas.FirstOrDefault(a => a.CultureID == vm.CultureID);
            vm.CultureValue = dataValue;
            vm.TimeStamps = timeStamps;

            if (value == null)
            {
                CultureDatas.Add(vm);
            }
            else
            {
                value.CultureValue = dataValue;
                value.TimeStamps = timeStamps;
            }
        }
    }
}
