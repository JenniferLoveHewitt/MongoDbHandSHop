using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbMarket.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbMarket.Domain
{
    public class Item
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public String ItemId { get; set; }

        [Display(Name = "Наименование")]
        public String Title { get; set; }

        [Display(Name = "Цена")]
        public Int32? Price { get; set; }

        [Display(Name = "Описание")]
        public String Description { get; set; }

        public Byte[] Photos { get; set; }

        //ссылка на пользователя
        public ApplicationUser User { get; set; }

        [Display(Name = "Наличие товара")]
        public Boolean Availability { get; set; }

        public String Category { get; set; }

        public String Subcategory { get; set; }
    }
}
