using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Interfaces;
using Eduegate.Framework;

namespace Eduegate.Domain.Mappers
{
    public class TransactionShipmentMapper : IDTOEntityMapper<ShipmentDetailDTO, TransactionShipment>
    {
        private CallContext _context;

        public static TransactionShipmentMapper Mapper(CallContext context)
        {
            var mapper = new TransactionShipmentMapper();
            mapper._context = context;
            return mapper;
        }

        public TransactionShipment ToEntity(ShipmentDetailDTO dto)
        {
            if (dto != null)
            {
                return new TransactionShipment()
                {
                    TransactionShipmentIID = dto.TransactionShipmentIID,
                    TransactionHeadID = dto.TransactionHeadID,
                    SupplierIDFrom = dto.SupplierIDFrom,
                    SupplierIDTo = dto.SupplierIDTo,
                    ShipmentReference = dto.ShipmentReference,
                    FreightCarrier = dto.FreightCareer,
                    ClearanceTypeID = dto.ClearanceTypeID,
                    AirWayBillNo = dto.AirWayBillNo,
                    FreightCharges = dto.FrieghtCharges,
                    BrokerCharges = dto.BrokerCharges,
                    AdditionalCharges = dto.AdditionalCharges,
                    Weight = dto.Weight,
                    NoOfBoxes = dto.NoOfBoxes,
                    BrokerAccount = dto.BrokerAccount,
                    Description = dto.Remarks,
                    CreatedBy = dto.CreatedBy,
                    UpdatedBy = dto.UpdatedBy,
                    CreatedDate = dto.CreatedDate,
                    UpdatedDate = dto.UpdatedDate,
                    //TimeStamps = dto.TimeStamps,
                };
                
            }

            else return new TransactionShipment();
        }

        public ShipmentDetailDTO ToDTO(TransactionShipment entity)
        {
            if (entity != null)
            {
                var shipment = new ShipmentDetailDTO()
                {
                    TransactionShipmentIID = entity.TransactionShipmentIID,
                    TransactionHeadID = entity.TransactionHeadID,
                    SupplierIDFrom = entity.SupplierIDFrom,
                    SupplierIDTo = entity.SupplierIDTo,
                    ShipmentReference = entity.ShipmentReference,
                    FreightCareer = entity.FreightCarrier,
                    ClearanceTypeID = entity.ClearanceTypeID,
                    AirWayBillNo = entity.AirWayBillNo,
                    FrieghtCharges = entity.FreightCharges,
                    BrokerCharges = entity.BrokerCharges,
                    AdditionalCharges = entity.AdditionalCharges,
                    Weight = entity.Weight,
                    NoOfBoxes = entity.NoOfBoxes,
                    BrokerAccount = entity.BrokerAccount,
                    Remarks = entity.Description,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = entity.TimeStamps,
                };

                return shipment;
            }

            else return new ShipmentDetailDTO();
        }
    }
}
