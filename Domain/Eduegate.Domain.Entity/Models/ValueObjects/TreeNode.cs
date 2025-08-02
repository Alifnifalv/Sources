using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.ValueObjects
{
    public class TreeNode
    {
        public TreeNode()
        {
            ChildNodes = new List<TreeNode>();
        }

        [Key]
        public int NodeID { get; set; }
        public string NodeName { get; set; }
        public string NodeType { get; set; }

        public List<TreeNode> ChildNodes { get; set; }
    }
}
