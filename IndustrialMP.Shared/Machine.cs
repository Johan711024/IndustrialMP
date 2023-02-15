using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialMP.Shared
{
    public class Machine
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("n");
        public string Name { get; set; } = string.Empty;      
        public bool Status { get; set; }
        public ICollection<MachineData> MachineDatas { get; set; } = new List<MachineData>();
    }
}
