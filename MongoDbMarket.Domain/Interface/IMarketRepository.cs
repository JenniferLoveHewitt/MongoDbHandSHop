using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbMarket.Domain
{
    public interface IMarketRepository
    {
        Task<ItemRepository> GetItemRepository(ItemFilter filter);
        Task<ItemRepository> GetItemRepository();
        Task<Item> GetItem(String Id);
        Task AddItem(Item item);
        Task EditItem(Item item);
        Task RemoveItem(String Id);
    }
}
