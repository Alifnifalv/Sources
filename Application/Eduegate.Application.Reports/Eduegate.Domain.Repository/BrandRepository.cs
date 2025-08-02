using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Domain.Repository
{
    public class BrandRepository
    {
        public Brand GetBrand(long brandID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var brand = dbContext.Brands.Where(x => x.BrandIID == brandID).FirstOrDefault();
                dbContext.Entry(brand).Collection(a => a.BrandTagMaps).Load();
                dbContext.Entry(brand).Collection(a => a.BrandImageMaps).Load();

                foreach (var tag in brand.BrandTagMaps)
                {
                    dbContext.Entry(tag).Reference(a => a.BrandTag).Load();
                }

                return brand;
            }
        }

        public List<BrandTag> GetBrandTags()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.BrandTags.ToList();
            }
        }

        public Brand SaveBrand(Brand brandDetails, CallContext callContext)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                dbContext.Brands.Add(brandDetails);

                if (brandDetails.BrandIID == 0)
                    dbContext.Entry(brandDetails).State = EntityState.Added;
                else
                {
                    dbContext.Entry(brandDetails).State = EntityState.Modified;
                    dbContext.BrandTagMaps.RemoveRange(dbContext.BrandTagMaps.Where(a => a.BrandID == brandDetails.BrandIID));
                    dbContext.BrandImageMaps.RemoveRange(dbContext.BrandImageMaps.Where(a => a.BrandID == brandDetails.BrandIID));
                }

                foreach (var tag in brandDetails.BrandTagMaps)
                {
                    dbContext.Entry(tag).State = EntityState.Added;

                    if (tag.BrandTagID == null || tag.BrandTagID == 0)
                    {
                        dbContext.Entry(tag.BrandTag).State = EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(tag.BrandTag).State = EntityState.Modified;
                    }
                }

                foreach (var imageMap in brandDetails.BrandImageMaps)
                {
                    dbContext.Entry(imageMap).State = System.Data.Entity.EntityState.Added;
                }

                dbContext.SaveChanges();
                brandDetails = GetBrand(brandDetails.BrandIID);
            }

            return brandDetails;
        }

        public List<BrandStatus> GetBrandStatusList()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.BrandStatuses.OrderBy(a=> a.StatusName).ToList();
            }
        }

        public List<Brand> GetAllBrands()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Brands.ToList();
            }
        }

        public bool BrandNameAvailibility(string brandName,long brandIID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Brands.Any(x => x.BrandName == brandName && x.BrandIID!= brandIID);
            }
        }

        public List<Brand> GetCategoryBrandsbyTag(string tagName)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var brands = (from brand in dbContext.Brands
                                  join tagMap in dbContext.BrandTagMaps on brand.BrandIID equals tagMap.BrandID
                                  join tag in dbContext.BrandTags on tagMap.BrandTagID equals tag.BrandTagIID
                                  where tag.TagName.ToUpper().Equals(tagName.ToUpper())
                                  select brand).ToList();
                return brands;
            }
        }
    }
}
