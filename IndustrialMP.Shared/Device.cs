using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialMP.Shared
{
    public class Device
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("n");
        public string Name { get; set; } = string.Empty;      
        public bool Status { get; set; }
        public ICollection<DeviceData> DeviceDatas { get; set; } = new List<DeviceData>();
    }
}
