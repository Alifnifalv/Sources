using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Models.Mutual;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Schedulers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.Mutual
{
    public class SchoolGeoLocationLogMapper : DTOEntityDynamicMapper
    {
        public static SchoolGeoLocationLogMapper Mapper(CallContext context)
        {
            var mapper = new SchoolGeoLocationLogMapper();
            mapper._context = context;
            return mapper;
        }

        public List<SchoolGeoLocationDTO> GetGeoSchoolSettingBySchoolID(long schoolID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var getSchoolGeo = dbContext.SchoolGeoMaps.Where(s => s.SchoolID == schoolID).AsNoTracking().ToList();
                var dtos = new List<SchoolGeoLocationDTO>();
                foreach (var datas in getSchoolGeo)
                {
                    dtos.Add(new SchoolGeoLocationDTO()
                    {
                        SchoolGeoMapIID = datas.SchoolGeoMapIID,
                        SchoolID = datas.SchoolID,
                        Latitude = datas.Latitude,
                        Longitude = datas.Longitude,
                    });
                }
                return dtos;
            }
        }

        public void SaveGeoSchoolSetting(List<SchoolGeoLocationDTO> dtos)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                foreach (var dto in dtos)
                {
                    var entity = new SchoolGeoMap()
                    {
                        Latitude = dto.Latitude,
                        Longitude = dto.Longitude,
                        SchoolID = dto.SchoolID,
                    };
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    dbContext.SaveChanges();
                }
            }
        }

        public void ClearGeoSchoolSettingBySchoolID(int schoolID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var getSchoolGeo = dbContext.SchoolGeoMaps.Where(s => s.SchoolID == schoolID).AsNoTracking().ToList();

                if (getSchoolGeo != null || getSchoolGeo.Count > 0)
                {
                    dbContext.SchoolGeoMaps.RemoveRange(getSchoolGeo);
                    dbContext.SaveChanges();
                }
            }
        }


    }
}