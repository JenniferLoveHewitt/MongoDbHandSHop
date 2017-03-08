using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbMarket.Domain
{
    public class User
    {
        public String Login { get; set; }
        public String Password { get; set; }
        public String Email { get; set; }
        public DateTime Birthday { get; set; }
        public Int32 Rating { get; set; }
        public String Phone { get; set; }
        public String Skype { get; set; }
        public String Name { get; set; }
    }
}
