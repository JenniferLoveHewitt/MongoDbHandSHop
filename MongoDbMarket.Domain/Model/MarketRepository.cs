using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace MongoDbMarket.Domain
{
    public class MarketRepository : IMarketRepository
    {
        private String connectionString = "mongodb://localhost";
        private MongoClient Client { get; set; }
        private IMongoDatabase Database { get; set; }
        private IMongoCollection<Item> ItemCollection { get; set; }
        private ItemRepository Repository { get; set; }
        public MarketRepository()
        {
            Client = new MongoClient(connectionString);
            Database = Client.GetDatabase("HandShopV3");

            ItemCollection = Database.GetCollection<Item>("Item");
        }

        public async Task<ItemRepository> GetItemRepository(ItemFilter filter)
        {
            if(filter.Title == null)
                Repository = new ItemRepository
                {
                    Items = await ItemCollection.Find(new BsonDocument()).ToListAsync(),
                    Filter = null
                };
            else
                Repository = new ItemRepository
                {
                    Items = await ItemCollection.Find(Builders<Item>.Filter.Eq("Title", filter.Title)).ToListAsync(),
                    Filter = new Item { Title = filter.Title }
                };

            return Repository;
        }

        public async Task<ItemRepository> GetItemRepository()
        {
            Repository = new ItemRepository
            {
                Items = await ItemCollection.Find(new BsonDocument()).ToListAsync(),
                Filter = null
            };

            return Repository;
        }

        public async Task<Item> GetItem(String Id)
        {
            return await ItemCollection.Find(Builders<Item>.Filter.Eq("ItemId", Id)).FirstOrDefaultAsync();
        }

        public async Task AddItem(Item item)
        {
            await ItemCollection.InsertOneAsync(item);
        }

        public async Task EditItem(Item item)
        {
            await ItemCollection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(item.ItemId)), item);
        }

        public async Task RemoveItem(String Id)
        {
            await ItemCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(Id)));
        }

        public IMongoDatabase GetDatabase()
        {
            return Database;
        }
    }
}
