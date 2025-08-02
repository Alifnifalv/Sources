using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

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
            Nullable = false;
            AllowBlank = false;
        }

        [XmlAttribute]
        public string Name;

        public string DataType;

        public string Prompt;

        public DefaultValue DefaultValue { get; set; }

        public bool Hidden { get; set; }

        public bool MultiValue { get; set; }

        public bool Nullable { get; set; }

        public bool AllowBlank { get; set; }

        public ValidValues ValidValues { get; set; }
    }

    [Serializable()]
    public class DefaultValue
    {
        [XmlArray("Values")]
        [XmlArrayItem("Value")]
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