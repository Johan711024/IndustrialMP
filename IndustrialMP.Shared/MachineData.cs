using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialMP.Shared
{
    public class MachineData
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("n");        
        public string? Description { get; set; }
    }
}
