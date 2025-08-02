using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.SmartView;
using System.Collections.Generic;

namespace Eduegate.Web.Library.ViewModels.Frameworks
{
    public class SmartTreeViewViewModel
    {
        public SmartTreeViewViewModel()
        {
            Nodes = new List<SmartTreeViewViewModel>();
        }

        public SmartViewType SmartViewType { get; set; }
        public long NodeID { get; set; }
        public string NodeName { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Ledger { get; set; }
        public List<SmartTreeViewViewModel> Nodes { get; set; }

        public static SmartTreeViewViewModel ToVM(SmartTreeViewDTO dto)
        {
            var vm = new SmartTreeViewViewModel();

            foreach(var node in dto.Node.Nodes)
            {
                vm.Nodes.Add(new SmartTreeViewViewModel() { NodeID = node.ID, NodeName = node.Caption, Nodes = BuildTree(node.Nodes), Amount =  node.Amount, Ledger = node.Ledger});
            }

            return vm;
        }

        private static List<SmartTreeViewViewModel> BuildTree(List<TreeNodeDTO> treeView)
        {
            var nodes = new List<SmartTreeViewViewModel>();

            foreach (var node in treeView)
            {
                nodes.Add(new SmartTreeViewViewModel()
                {
                    NodeID = node.ID,
                    NodeName = node.Caption,
                    Nodes = node.Nodes == null? null : BuildTree(node.Nodes)
                });
            }

            return nodes;
        }

        public static SmartTreeViewDTO ToDTO(SmartTreeViewViewModel vm)
        {
            var dto = new SmartTreeViewDTO();
            return dto;
        }
    }
}
