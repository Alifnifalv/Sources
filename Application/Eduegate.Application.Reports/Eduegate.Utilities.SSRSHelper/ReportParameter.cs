using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eduegate.Utilities.SSRSHelper
{
    /// <summary>
    /// child of Report
    /// </summary>
    [Serializable()]
    public class ReportParameter
    {
        public ReportParameter()
        {
            Hidden = false;
        }

        #region Serialization Interface
        [System.Xml.Serialization.XmlAttribute]
        public string Name;
        public string DataType;
        public string Prompt;
        public DefaultValue DefaultValue { get; set; }
        public bool Hidden { get; set; }
        public bool MultiValue { get; set; }
        public ValidValues ValidValues { get; set; }
        #endregion
    }

    [Serializable()]
    public class DefaultValue
    {
        public Values Values { get; set; }
    }

    [Serializable()]
    public class Values : List<string>
    {
       
    }

    [Serializable()]
    public class ValidValues 
    {
        public List<ParameterValue> ParameterValues { get; set; }
        public DataSetReference DataSetReference { get; set; }
    }

    [Serializable()]
    public class DataSetReference
    {
        public string DataSetName { get; set; }
        public string ValueField { get; set; }
        public string LabelField { get; set; }
    }

    [Serializable()]
    public class ParameterValue
    {
        public string Value { get; set; }
        public string Label { get; set; }
    }
}