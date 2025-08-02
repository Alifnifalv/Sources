using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.SmartView;

namespace Eduegate.Domain
{
    public class SmartTreeViewBL
    {
        private CallContext _callContext;

        public SmartTreeViewBL(CallContext context)
        {
            _callContext = context;
        }

        public SmartTreeViewDTO GetSmartTreeView(SmartViewType type, long? parentID, string searchText)
        {
            var smartView = new SmartTreeViewDTO();

            switch (type)
            {
                case SmartViewType.Company:
                    break;
                case SmartViewType.Employee:
                    break;
                case SmartViewType.Customer:
                    break;
                case SmartViewType.Product:
                    smartView = GetProductTree(parentID);
                    break;
                case SmartViewType.Area:
                    smartView = GetAreas(parentID, searchText);
                    break;
            }

            return smartView;
        }

        public SmartTreeViewDTO GetAreas(long? parentID, string searchText)
        {
            var smartView = new SmartTreeViewDTO()
            {
                Node = new TreeNodeDTO()
                {
                    Caption = "Areas",
                    ID = -1,
                    SmartTreeNodeType = SmartTreeNodeType.Root
                },

                SmartViewType = SmartViewType.Area
            };

            var areas = new MutualRepository().GetAreaTrees(parentID, searchText);
            var filterdAreas = parentID.HasValue ? areas :
                areas.Where(x => x.ParentAreaID == null).OrderBy(x => x.ParentAreaID).ToList();

            foreach (var area in filterdAreas)
            {
                smartView.Node.Nodes.Add(new TreeNodeDTO()
                {
                    ID = area.AreaID,
                    Caption = area.AreaName,
                    Nodes = BuildTree(areas, area.AreaID)
                });
            }

            return smartView;
        }

        private List<TreeNodeDTO> BuildTree(List<AreaTreeSearch> areas, long areaID)
        {
            var nodes = new List<TreeNodeDTO>();

            foreach (var area in areas.Where(a => a.ParentAreaID == areaID))
            {
                nodes.Add(new TreeNodeDTO()
                {
                    ID = area.AreaID,
                    Caption = area.AreaName,
                    Nodes = BuildTree(areas, area.AreaID)
                });
            }

            return nodes;
        }

        public SmartTreeViewDTO GetProductTree(long? categoryID)
        {
            var smartView = new SmartTreeViewDTO()
            {
                Node = new TreeNodeDTO() { Caption = "Product View", ID = -1, SmartTreeNodeType = Services.Contracts.Enums.SmartTreeNodeType.Root },
                SmartViewType = Services.Contracts.Enums.SmartViewType.Product
            };
            FromProductEntity(new ProductDetailRepository().GetProductTree(categoryID).CategoryTree, smartView.Node);
            return smartView;
        }

        public void FromProductEntity(List<CategoryTree> categories, TreeNodeDTO smartView)
        {
            foreach (var cat in categories)
            {
                var node = new TreeNodeDTO()
                {
                    SmartTreeNodeType = Services.Contracts.Enums.SmartTreeNodeType.Category,
                    Caption = cat.CategoryName,
                    ID = cat.CategoryID,
                    Code = cat.CategoryCode,
                    Nodes = new List<TreeNodeDTO>(),
                };

                if (cat.Categories.Count > 0)
                {
                    FromProductEntity(cat.Categories, node);
                }

                foreach (var prd in cat.Products)
                {
                    node.Nodes.Add(new TreeNodeDTO()
                    {
                        SmartTreeNodeType = Services.Contracts.Enums.SmartTreeNodeType.Product,
                        Caption = prd.ProductName,
                        ID = prd.ProductID,
                        Code = prd.ProductCode
                    });
                }

                smartView.Nodes.Add(node);
            }
        }
    }
}