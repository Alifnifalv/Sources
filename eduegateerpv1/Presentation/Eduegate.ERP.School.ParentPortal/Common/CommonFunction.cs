using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eduegate.ERP.School.ParentPortal.Common
{
    public class CommonFunction
    {
        public string ConvertInvalideDate(String arg)
        {
            string DateValue = ""; var returnDate="";
            try
            {
                if (arg != null && arg != "")
                {
                    List<int> spl = arg.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Select(w => Convert.ToInt32(w)).ToList<int>();

                    DateTime _stestD = DateTime.Now;
                    if (spl.Any() && spl.Count == 3)
                    {
                        _stestD = new DateTime(spl[2], spl[1], spl[0]);
                        DateValue = _stestD.ToString();
                    }

                    //DateTime.TryParse(Model.PassportNoIssueString + " 00:00:00",  out _stestD);

                    returnDate = Convert.ToDateTime(DateValue).ToString("dd/MMM/yyyy");
                }
            }
            catch  { }
                
            return returnDate;
        }
    }
}