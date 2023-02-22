using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialMP.Data.Repositories
{
    public class UnitOfWork
    {
        
        public DeviceRepository deviceRepository { get; private set; }

        public UnitOfWork()
        {

            
        }
        public async void CompleteAsync()
        {
           
        }
    }
}
