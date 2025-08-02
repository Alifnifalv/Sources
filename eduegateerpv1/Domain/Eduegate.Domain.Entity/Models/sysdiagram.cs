using Microsoft.EntityFrameworkCore;
using System;

namespace Eduegate.Domain.Entity.Models
{
    [Keyless]
    public partial class Sysdiagram
    {
        public string name { get; set; }
        public int principal_id { get; set; }
        public int diagram_id { get; set; }
        public Nullable<int> version { get; set; }
        public byte[] definition { get; set; }
    }
}