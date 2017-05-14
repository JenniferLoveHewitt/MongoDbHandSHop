using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using System.IO;

namespace MongoDbMarket.Domain
{
    public class MarketRepository : IMarketRepository
    {
        private String connectionString = "mongodb://localhost";
        private MongoClient Client { get; set; }
        private IMongoDatabase Database { get; set; }
        private IMongoCollection<Item> ItemCollection { get; set; }
        private ItemRepository Repository { get; set; }
        private GridFSBucket GridFs { get; set; }
        public MarketRepository()
        {
            Client = new MongoClient(connectionString);
            Database = Client.GetDatabase("HandShopV3");
            GridFs = new GridFSBucket(Database);

            ItemCollection = Database.GetCollection<Item>("Item");
        }

        private Boolean disposed = false;
        private String _imageId;

        public String ImageId
        {
            get
            {
                return _imageId;
            }
        }

        public async Task<ItemRepository> GetItemRepository(ItemFilter filter)
        {
            if (filter.Title == null)
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

        public async Task UploadStream(Stream stream, String fileName)
        {
            _imageId = ObjectId.GenerateNewId().ToString();

            await GridFs.UploadFromStreamAsync(fileName, stream, 
                new GridFSUploadOptions
            {
                Metadata = new BsonDocument
                    {
                        { "FileID", ImageId }
                    }
            });
        }

        public async Task UploadBytes(Byte[] source, String fileName)
        {
            _imageId = ObjectId.GenerateNewId().ToString();

            await GridFs.UploadFromBytesAsync(fileName, source, 
                new GridFSUploadOptions
            {
                Metadata = new BsonDocument
                    {
                        { "FileID", ImageId }
                    }
            });
        }

        public String GetCurrentImageId()
        {
            return ImageId;
        }

        public async Task<IEnumerable<GridFSFileInfo>> GetImages(IEnumerable<String> ImagesId)
        {
            var imagesList = new List<GridFSFileInfo>();

            foreach (var item in ImagesId)
                imagesList.AddRange(await GridFs.Find(Builders<GridFSFileInfo>.
                    Filter.Eq(x => x.Metadata["FileID"], item)).ToListAsync());

            return imagesList;
        }

        public async Task<List<String>> GetImagesId(String Id)
        {
            var sItem = await ItemCollection.Find(Builders<Item>.Filter.Eq("ItemId", Id)).FirstOrDefaultAsync();

            return sItem.Photos;
        }

        public async Task<IEnumerable<Item>> GetItemByUserId(string Id)
        {
            return await ItemCollection.Find(Builders<Item>.Filter.Eq("User.Id", Id)).ToListAsync();
        }
    }
}
