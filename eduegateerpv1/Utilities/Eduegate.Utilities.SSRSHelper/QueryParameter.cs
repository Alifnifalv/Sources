using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eduegate.Utilities.SSRSHelper
{
    /// <summary>
    /// child of Query
    /// </summary>
    [Serializable()]
    public class QueryParameter
    {
        #region Serialization Interface
        [System.Xml.Serialization.XmlAttribute]
        public string Name;
        public string DataType;
        public string Value;
        #endregion
    }
}