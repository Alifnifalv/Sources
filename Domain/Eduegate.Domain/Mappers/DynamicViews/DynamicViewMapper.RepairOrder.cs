using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Oracle.Models;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers.DynamicViews
{
    public partial class DynamicViewMapper
    {
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetRepairOrder(AS_ROHEAD head)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (head.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SHOP" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "RODOCFYR" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ROTYPE" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "RONO" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DOCDATE" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CHASSISNO" });

                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CUSCODE" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BVEHTYPE" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PROMISED" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ACTUALDEL" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PRIORITY" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SPOTTER" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PAYCOMP" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SERADV" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "KMS" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "KMSOUT" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "FUEL" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "GPDOCFYR" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "GPASSNO" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "GPASSDATE" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "GPRTIND" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "FRSHOP" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "FRROFYR" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "FRROTYPE" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "FRRONO" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "INSRIND" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "INSRLAB" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "INSRPART" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "WARRIND" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "WARRLAB" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "WARRPART" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CUSTAPPR" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ADDIND" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ESTILABHRS" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ESTILABCOST" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ADDCHG" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "LABAMT" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "LABHRS" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          head.SHOP,head.RODOCFYR,head.ROTYPE,
                          head.RONO,head.DOCDATE,head.CHASSISNO
                          ,head.CUSCODE
                          ,head.BVEHTYPE
                          ,head.PROMISED
                          ,head.ACTUALDEL
                          ,head.PRIORITY
                          ,head.SPOTTER
                          ,head.PAYCOMP
                          ,head.SERADV
                          ,head.KMS
                          ,head.KMSOUT,
                          head.FUEL,
                          head.GPDOCFYR,
                          head.GPASSNO,
                          head.GPASSDATE,
                          head.GPRTIND,
                          head.FRSHOP,
                          head.FRROFYR,
                          head.FRROTYPE,
                          head.FRRONO,
                          head.INSRIND,
                          head.INSRLAB,
                          head.INSRPART,
                          head.WARRIND,
                          head.WARRLAB,
                          head.WARRPART,
                          head.CUSTAPPR,
                          head.ADDIND,
                          head.ESTILABHRS,
                          head.ESTILABCOST,
                          head.ADDCHG,
                          head.LABAMT,
                          head.LABHRS,

                        }
                });
            }


            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetRepairOrderCustomer(AS_ROHEAD head)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (head.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SHOP" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "RODOCFYR" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ROTYPE" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "RONO" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DOCDATE" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CHASSISNO" });

                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CUSCODE" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BVEHTYPE" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PROMISED" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ACTUALDEL" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PRIORITY" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SPOTTER" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PAYCOMP" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SERADV" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "KMS" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "KMSOUT" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "FUEL" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "GPDOCFYR" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "GPASSNO" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "GPASSDATE" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "GPRTIND" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "FRSHOP" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "FRROFYR" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "FRROTYPE" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "FRRONO" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "INSRIND" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "INSRLAB" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "INSRPART" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "WARRIND" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "WARRLAB" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "WARRPART" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CUSTAPPR" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ADDIND" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ESTILABHRS" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ESTILABCOST" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ADDCHG" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "LABAMT" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "LABHRS" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          head.SHOP,head.RODOCFYR,head.ROTYPE,
                          head.RONO,head.DOCDATE,head.CHASSISNO
                          ,head.CUSCODE
                          ,head.BVEHTYPE
                          ,head.PROMISED
                          ,head.ACTUALDEL
                          ,head.PRIORITY
                          ,head.SPOTTER
                          ,head.PAYCOMP
                          ,head.SERADV
                          ,head.KMS
                          ,head.KMSOUT,
                          head.FUEL,
                          head.GPDOCFYR,
                          head.GPASSNO,
                          head.GPASSDATE,
                          head.GPRTIND,
                          head.FRSHOP,
                          head.FRROFYR,
                          head.FRROTYPE,
                          head.FRRONO,
                          head.INSRIND,
                          head.INSRLAB,
                          head.INSRPART,
                          head.WARRIND,
                          head.WARRLAB,
                          head.WARRPART,
                          head.CUSTAPPR,
                          head.ADDIND,
                          head.ESTILABHRS,
                          head.ESTILABCOST,
                          head.ADDCHG,
                          head.LABAMT,
                          head.LABHRS,

                        }
                });
            }


            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetCustomer(AS_CUSTOMER_LOCL customer)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (customer.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CUSCODE" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CUSNAME" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "NMTITL" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "FSTNAM" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "MIDNAM" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "LSTNAM" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "INITAL" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "RESTEL1" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "RESTEL2" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "RESFAX" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "WRKTEL1" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "WRKTEL2" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "MOBILE" });

                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SEX" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "EMAIL" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CIVILID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CUSCAT" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          customer.CUSCODE,
                          customer.CUSNAME,
                          customer.NMTITL,
                          customer.FSTNAM,
                          customer.MIDNAM,
                          customer.LSTNAM,
                          customer.INITAL,
                          customer.RESTEL1,
                          customer.RESTEL2,
                          customer.RESFAX,
                          customer.WRKTEL1,
                          customer.WRKTEL2,
                          customer.MOBILE,
                          customer.SEX,
                          customer.EMAIL,
                          customer.CIVILID,
                          customer.CUSCAT
                        }
                });
            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO RepairOrderDetails(List<AS_RODETAIL> details)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (details.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SRLNO" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DOCDATE" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "STARTDATE" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ENTRY" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "LASTDATE" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ADDLREF" });

                foreach (var detail in details)
                {
                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          detail.SRLNO,
                          detail.DOCDATE,
                          detail.STARTDATE,
                          detail.ENTRY,
                          detail.LASTDATE,
                          detail.ADDLREF,
                        }
                    });
                }
            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO VehicleDetails(AS_VEHICLE vehicle)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (vehicle.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CHASSISNO" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ADESCRIPTION" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CROSHOP" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CROTYPE" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CURRONO" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CUSCODE" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CYLCODE" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DELDAT" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DESCRIPTION" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ENTRY" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "FIRSTOWNER" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "INVCOMP" });

                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "INVDAT" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "INVSHOP" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "KTNO" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "LABOUR" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "MATERIAL" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "OBSERVATION" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SECONDOWNER" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "STOCKNO" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "STATUS" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "VEH_TYPE" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "VEHMAKE" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "WARRANTYKM" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "WARRANTYMTH" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          vehicle.CHASSISNO,
                          vehicle.ADESCRIPTION,
                          vehicle.CROSHOP,
                          vehicle.CROTYPE,
                          vehicle.CURRONO,
                          vehicle.CUSCODE,
                           vehicle.CYLCODE,
                            vehicle.DELDAT,
                             vehicle.DESCRIPTION,
                              vehicle.ENGINENO,
                               vehicle.ENTRY,
                                vehicle.FIRSTOWNER,
                                 vehicle.INVCOMP,
                                  vehicle.INVDAT,
                                  vehicle.INVSHOP,
                                  vehicle.KTNO,
                                  vehicle.LABOUR,
                                  vehicle.LASTRONO,
                                  vehicle.MATERIAL, 
                                  vehicle.OBSERVATION,
                                  vehicle.REGISTRATION,
                                  vehicle.SECONDOWNER,
                                  vehicle.STOCKNO,
                                  vehicle.STATUS,
                                  vehicle.VEH_TYPE,
                                  vehicle.VEHMAKE,
                                  vehicle.WARRANTYKM,
                                  vehicle.WARRANTYMTH
                        }
                });
            }

            return searchDTO;
        }
    }
}
