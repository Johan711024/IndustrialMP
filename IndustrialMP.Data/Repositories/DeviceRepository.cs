using IndustrialMP.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialMP.Data.Repositories
{
    public class DeviceRepository
    {

        public DeviceRepository()
        {

        }

        public void Add(Item device)
        {

        }

        public void AnyAsync(string title)
        {

        }



        public async void GetAllAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException($"'{nameof(title)}' cannot be null or whitespace.", nameof(title));
            }


        }




        public async void GetAsync(string title, int id)
        {

        }

        public void Remove(Item game)
        {

        }

        public void Update(Item game)
        {

        }
    }
}
