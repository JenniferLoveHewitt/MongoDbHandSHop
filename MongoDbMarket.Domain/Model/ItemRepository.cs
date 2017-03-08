using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbMarket.Domain
{
    public class ItemRepository
    {
        public IEnumerable<Item> Items { get; set; }
        public Item Filter { get; set; }
    }
}
