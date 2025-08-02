using Lucene.Net.Index;
using Lucene.Net.Store;
using Eduegate.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain;

namespace Eduegate.Utilities.SearchIndexers
{
    public class FastSearch
    {
        private static string _luceneDir =
        Path.Combine(new Domain.Setting.SettingBL().GetSettingValue<string>("INDEXPATH"), "lucene_index");
        private static FSDirectory _directoryTemp;
        private static FSDirectory _directory
        {
            get
            {
                if (_directoryTemp == null) _directoryTemp = FSDirectory.Open(new DirectoryInfo(_luceneDir));
                if (IndexWriter.IsLocked(_directoryTemp)) IndexWriter.Unlock(_directoryTemp);
                var lockFilePath = Path.Combine(_luceneDir, "write.lock");
                if (File.Exists(lockFilePath)) File.Delete(lockFilePath);
                return _directoryTemp;
            }
        }

        public static void CreateIndex()
        {
           
        }
    }
}
