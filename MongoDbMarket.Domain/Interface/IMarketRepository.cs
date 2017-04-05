using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.GridFS;

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
        Task UploadStream(System.IO.Stream stream, String fileName);
        Task UploadBytes(Byte[] source, String fileName);
        String ImageId { get; }
        Task<IEnumerable<GridFSFileInfo>> GetImages(IEnumerable<String> fileId);
        Task<List<String>> GetImagesId(String Id);
    }
}
