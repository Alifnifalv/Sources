using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.CustomerService;

namespace Eduegate.Web.Library.ViewModels.CustomerService
{
    public class RepairOrderViewModel : BaseMasterViewModel
    {
        public RepairOrderViewModel()
        {
            MasterViewModel = new RepairOrderMasterViewModel();
            //DetailViewModel = new List<RepairOrderDetailViewModel>();
        }

        public RepairOrderMasterViewModel MasterViewModel { get; set; }
        //public List<RepairOrderDetailViewModel> DetailViewModel { get; set; }

        public static RepairOrderViewModel ToVM(RepairOrderDTO dto)
        {
            var vm = new RepairOrderViewModel()
            {
                MasterViewModel = new RepairOrderMasterViewModel()
                {
                    ChasisNo = dto.CHASSISNO,
                    OrderDate = dto.DOCDATE.HasValue ? dto.DOCDATE.Value.ToString() : string.Empty,
                    RONO = dto.RONO.ToString(),
                    Customer = KeyValueViewModel.ToViewModel(dto.Customer),
                    OrderType = KeyValueViewModel.ToViewModel(dto.OrderType),
                    PhoneNumber = dto.PhoneNumber,
                    CivilId = dto.CivilID,
                    ROShop = KeyValueViewModel.ToViewModel(dto.Shops),
                    RegitrationNo = dto.KTNO,
                    VehicleDescription = dto.VehicleDescription,
                    RegitrationDate = dto.RegitrationDate,
                    BillVehicleType = dto.BillVehicleType,
                    WarrantyKMs = dto.WarrantyKMs,
                    LastServiceKMs = dto.LastServiceKMs,
                    Priority = dto.PRIORITY,
                },

            };

            vm.MasterViewModel.JobDetail.Details = new List<RepairOrderDetailViewModel>();

            foreach (var detail in dto.Details)
            {
                vm.MasterViewModel.JobDetail.Details.Add(new RepairOrderDetailViewModel()
                {
                    OperationGroup = new KeyValueViewModel() { Key = detail.OPGRPCODE.ToString(), Value = detail.OPGRPCODEDESCRIPTION.ToString() },
                    Operation = new KeyValueViewModel() { Key = detail.OPRNCODE.ToString(), Value = detail.OPRNCODEDESCRIPTION.ToString() },
                    Symptom = new KeyValueViewModel() { Key = detail.SYMCODE.ToString(), Value = detail.SYMCODEDESCRIPTION.ToString() }
                });
            }

            vm.MasterViewModel.Defect.DefectDetails = new List<DefectCodeViewModel>();
            foreach (var defect in dto.Defects)
            {
                vm.MasterViewModel.Defect.DefectDetails.Add(new DefectCodeViewModel()
                {
                    DefectSide = new KeyValueViewModel() { Key = defect.SIDE.ToString(), Value = defect.SIDEDESC },
                    DefectCode = new KeyValueViewModel() { Key = defect.DAMAGECODE.ToString(), Value = defect.DAMAMECODEDESC }
                });
            }

            return vm;
        }

        public static RepairOrderDTO ToDTO(RepairOrderViewModel vm)
        {
            //var dto = new RepairOrderDTO()
            //{

            //        CHASSISNO = vm.CHASSISNO,
            //        DOCDATE = vm.DOCDATE.HasValue ? vm.DOCDATE.Value.ToString() : string.Empty,
            //        RONO = dto.RONO.ToString(),
            //        Customer = KeyValueViewModel.ToViewModel(dto.Customer),
            //        PhoneNumber = dto.PhoneNumber,
            //        CivilId = dto.CivilID,
            //        ROShop = KeyValueViewModel.ToViewModel(dto.Shops),
            //        RegitrationNo = dto.KTNO,
            //        VehicleDescription = dto.VehicleDescription,
            //        RegitrationDate = dto.RegitrationDate,
            //        BillVehicleType = dto.BillVehicleType,
            //        WarrantyKMs = dto.WarrantyKMs,
            //        LastServiceKMs = dto.LastServiceKMs

            //    DetailViewModel = new List<RepairOrderDetailViewModel>()
            //};

            //foreach (var detail in dto.Details)
            //{
            //    vm.DetailViewModel.Add(new RepairOrderDetailViewModel()
            //    {
            //        OperationGroup = new KeyValueViewModel() { Key = detail.OPGRPCODE.ToString(), Value = detail.OPGRPCODEDESCRIPTION.ToString() },
            //        Operation = new KeyValueViewModel() { Key = detail.OPRNCODE.ToString(), Value = detail.OPRNCODEDESCRIPTION.ToString() },
            //        Symptom = new KeyValueViewModel() { Key = detail.SYMCODE.ToString(), Value = detail.SYMCODEDESCRIPTION.ToString() }
            //    });
            //}

            //return vm;
            var dto = new RepairOrderDTO()
            {
                CHASSISNO = vm.MasterViewModel.ChasisNo,
                DOCDATE = DateTime.Now,
                RONO = int.Parse(vm.MasterViewModel.RONO),
                Customer = KeyValueViewModel.ToDTO(vm.MasterViewModel.Customer),
                PhoneNumber = vm.MasterViewModel.PhoneNumber,
                CivilID = vm.MasterViewModel.CivilId,
                Shops = KeyValueViewModel.ToDTO(vm.MasterViewModel.ROShop),
                SHOP = short.Parse(vm.MasterViewModel.ROShop.Key),
                KTNO = vm.MasterViewModel.RegitrationNo,
                VehicleDescription = vm.MasterViewModel.VehicleDescription,
                RegitrationDate = vm.MasterViewModel.RegitrationDate,
                BillVehicleType = vm.MasterViewModel.BillVehicleType,
                WarrantyKMs = vm.MasterViewModel.WarrantyKMs,
                LastServiceKMs = vm.MasterViewModel.LastServiceKMs,
                CUSCODE = int.Parse(vm.MasterViewModel.Customer.Key),
                ROTYPE = short.Parse(vm.MasterViewModel.OrderType.Key),
                Details = new List<RepairDetailDTO>(),
                PRIORITY = vm.MasterViewModel.Priority,
                Defects = new List<RepairDefectDTO>()
            };
            //Mapper<RepairOrderViewModel, RepairOrderDTO>.CreateMap();
            //var mapper = Mapper<RepairOrderViewModel, RepairOrderDTO>.Map(vm);
            //mapper.Details = new List<RepairDetailDTO>();
            foreach (var detail in vm.MasterViewModel.JobDetail.Details)
            {
                var detailDTO = new RepairDetailDTO();
                detailDTO.OPGRPCODE = short.Parse(detail.OperationGroup.Key);
                detailDTO.OPGRPCODEDESCRIPTION = detail.OperationGroup.Value;
                detailDTO.OPRNCODE = short.Parse(detail.Operation.Key);
                detailDTO.OPRNCODEDESCRIPTION = detail.Operation.Value;
                detailDTO.SYMCODE = short.Parse(detail.Symptom.Key);
                detailDTO.SYMCODEDESCRIPTION = detail.Symptom.Value;
                dto.Details.Add(detailDTO);
            }

            foreach (var defect in vm.MasterViewModel.Defect.DefectDetails)
            {
                var defectDTO = new RepairDefectDTO();
                defectDTO.SIDE = defect.DefectSide.Key;
                defectDTO.DAMAGECODE = defect.DefectCode.Key;
                dto.Defects.Add(defectDTO);
            }
            return dto;

        }
    }
}
