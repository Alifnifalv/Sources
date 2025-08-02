using Eduegate.Domain.Entity.Models;
using System.Data.Entity;
using System;
using System.Linq;

namespace Eduegate.Domain.Repository.Frameworks
{
    public class FrameworkRepository
    {
        public ScreenMetadata GetScreenMetadata(long screenID, string languageCode = null)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var metaData = dbContext.ScreenMetadatas
                        .Include(b => b.ScreenLookupMaps)
                        .Include(b => b.ScreenFieldSettings)
                        .Include(b => b.View)
                        .Include(b => b.ScreenMetadataCultureDatas)
                        //.Include(b => b.ScreenFieldSettings.Select(a => a.ScreenField))
                        .FirstOrDefault(a => a.ScreenID == screenID);

                    if (metaData != null)
                    {
                        if (languageCode != null && !languageCode.ToLower().Contains("en"))
                        {
                            var cultureData = metaData.ScreenMetadataCultureDatas.FirstOrDefault(x => x.Culture.CultureCode.ToLower().StartsWith(languageCode.ToLower()));

                            if (cultureData != null)
                            {
                                metaData.ListButtonDisplayName = cultureData?.ListButtonDisplayName ?? metaData.ListButtonDisplayName;
                                metaData.DisplayName = cultureData?.DisplayName ?? metaData.DisplayName;
                            }
                        }

                        metaData.ScreenFieldSettings = dbContext.ScreenFieldSettings
                            .Include(a => a.ScreenField)
                            .Where(a => a.ScreenID == null || a.ScreenID == screenID).ToList();
                    }

                    return metaData;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
