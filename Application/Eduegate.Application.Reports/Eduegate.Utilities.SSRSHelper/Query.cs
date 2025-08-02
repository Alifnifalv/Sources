using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace Eduegate.Utilities.SSRSHelper
{
    /// <summary>
    /// child of DataSet
    /// </summary>
    [Serializable()]
    public class Query
    {
        #region Serialization Interface
        public string DataSourceName;
        public List<QueryParameter> QueryParameters = new List<QueryParameter>();
        public string CommandText;
        public CommandType CommandType =CommandType.Text ;
        #endregion
    }
}