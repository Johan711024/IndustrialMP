using IndustrialMP.HTTPTriggerFunc.Entities;
using IndustrialMP.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialMP.HTTPTriggerFunc.Extensions
{
    public static class Mapper
    {
        public static ItemTableEntity ToTableEntity(this Item item)
        {
            return new ItemTableEntity
            {
                Online = item.Online,
                Text = item.Text,
                RowKey = item.Id
            };
        }

        public static Item ToItem(this ItemTableEntity itemTable)
        {
            return new Item
            {
                Id = itemTable.RowKey,
                Text = itemTable.Text,
                Online = itemTable.Online
            };
        }
    }
}
