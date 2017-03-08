using AspNet.Identity.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbMarket.Domain.Identity
{
    public class ApplicationIdentityContext : IDisposable
    {
        public IMongoCollection<IdentityRole> Roles { get; set; }
        public IMongoCollection<ApplicationUser> Users { get; set; }
        private static IMongoDatabase Database { get; set; }
        private static MarketRepository reposiotry { get; set; }

        public static ApplicationIdentityContext Create()
        {
            reposiotry = new MarketRepository();
            Database = reposiotry.GetDatabase();

            var users = Database.GetCollection<ApplicationUser>("users");
            var roles = Database.GetCollection<IdentityRole>("roles");

            return new ApplicationIdentityContext(users, roles);
        }

        private ApplicationIdentityContext(IMongoCollection<ApplicationUser> users, 
            IMongoCollection<IdentityRole> roles)
        {
            Users = users;
            Roles = roles;
        }

        public async Task<List<IdentityRole>> AllRolesAsync()
        {
            return await Roles.Find(r => true).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            return await Users.Find(new BsonDocument()).ToListAsync();
        }

        public void Dispose() { }
    }
}
