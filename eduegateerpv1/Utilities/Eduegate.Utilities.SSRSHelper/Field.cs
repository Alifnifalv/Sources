using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eduegate.Utilities.SSRSHelper
{
    /// <summary>
    /// child of DataSet
    /// </summary>
    [Serializable()]
    public class Field
    {
        #region Serialization Interface
        [System.Xml.Serialization.XmlAttribute]
        public string Name;
        public string DataField;
        public string TypeName;
        #endregion
    }
}