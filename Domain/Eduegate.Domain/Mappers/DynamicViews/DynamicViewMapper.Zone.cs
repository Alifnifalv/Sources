using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;

namespace Eduegate.Domain.Mappers.DynamicViews
{
    public partial class DynamicViewMapper
    {
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetZone(Zone zone)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (zone.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ZoneID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ZoneName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          zone.ZoneID, zone.ZoneName,zone.CreatedDate,zone.UpdatedDate                     
                        }
                });
            }


            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetZoneDeliveryTypes(List<DeliveryTypes1> ZoneDeliveryType, short IID)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (ZoneDeliveryType.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryTypeID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryTypeName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ZoneID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CountryID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CartTotalFrom" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CartTotalTo" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryCharge" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryChargePercentage" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsDeliveryAvailable" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedBy" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedBy" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TimeStamps" });

                foreach (var Type in ZoneDeliveryType)
                {
                    DeliveryTypeAllowedZoneMap dtzMap = new DeliveryTypeAllowedZoneMap();
                    dtzMap = new DistributionRepository().GetDeliveryTypeByAllowedZoneMaps(IID, Convert.ToInt32(Type.DeliveryTypeID));

                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                            dtzMap.IsNotNull() ? dtzMap.DeliveryTypeID : Type.DeliveryTypeID,
                            Type.DeliveryTypeName,
                            dtzMap.IsNotNull() ? dtzMap.ZoneID: IID,
                            dtzMap.IsNotNull() ? dtzMap.CountryID :1,
                            dtzMap.IsNotNull() ? dtzMap.CartTotalFrom : null,
                            dtzMap.IsNotNull() ? dtzMap.CartTotalTo : null,
                            dtzMap.IsNotNull() ? dtzMap.DeliveryCharge : null,
                            dtzMap.IsNotNull() ? dtzMap.DeliveryChargePercentage  : null,
                            dtzMap.IsNotNull() ? dtzMap.IsSelected : false,
                            dtzMap.IsNotNull() ? dtzMap.CreatedBy : null,
                            dtzMap.IsNotNull() ? dtzMap.CreatedDate : null,
                            dtzMap.IsNotNull() ? dtzMap.UpdatedBy : null,
                            dtzMap.IsNotNull() ? dtzMap.UpdatedDate : null,
                            //dtzMap.IsNotNull() ? Convert.ToBase64String(dtzMap.TimeStamps) : null,
                        }
                    });
                }
            }
            return searchDTO;
        }


        public Eduegate.Services.Contracts.Search.SearchResultDTO GetAreaDeliveryTypes(List<DeliveryTypes1> AreaDeliveryType, int IID)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO(); 

            if (AreaDeliveryType.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "AreaDeliveryTypeMapID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryTypeID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryTypeName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "AreaID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CartTotalFrom" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CartTotalTo" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryCharge" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryChargePercentage" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsDeliveryAvailable" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedBy" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedBy" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TimeStamps" });

                foreach (var Type in AreaDeliveryType)
                {
                    DeliveryTypeAllowedAreaMap dtaMap = new DeliveryTypeAllowedAreaMap();
                    dtaMap = new DistributionRepository().GetDeliveryTypeByAreaMaps(IID, Convert.ToInt32(Type.DeliveryTypeID));

                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                            dtaMap.IsNotNull()? dtaMap.AreaDeliveryTypeMapIID : 0 ,
                            dtaMap.IsNotNull() ? dtaMap.DeliveryTypeID : Type.DeliveryTypeID,
                            Type.DeliveryTypeName,
                            dtaMap.IsNotNull() ? dtaMap.AreaID: IID,
                            dtaMap.IsNotNull() ? dtaMap.CartTotalFrom : null,
                            dtaMap.IsNotNull() ? dtaMap.CartTotalTo : null,
                            dtaMap.IsNotNull() ? dtaMap.DeliveryCharge : null,
                            dtaMap.IsNotNull() ? dtaMap.DeliveryChargePercentage  : null,
                            dtaMap.IsNotNull() ? dtaMap.IsSelected : false,
                            dtaMap.IsNotNull() ? dtaMap.CreatedBy : null,
                            dtaMap.IsNotNull() ? dtaMap.CreatedDate : null,
                            dtaMap.IsNotNull() ? dtaMap.UpdatedBy : null,
                            dtaMap.IsNotNull() ? dtaMap.UpdatedDate : null,
                            //dtaMap.IsNotNull() ? Convert.ToBase64String(dtaMap.TimeStamps) : null,
                        }
                    });
                }
            }
            return searchDTO;
        }
    }
}
