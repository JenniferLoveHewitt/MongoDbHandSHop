﻿using MongoDB.Bson;
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

        [Display(Name = "Наименование товара")]
        public String Title { get; set; }

        [Display(Name = "Цена")]
        public Int32? Price { get; set; }

        [Display(Name = "Описание")]
        public String Description { get; set; }

        //ссылки на картинки в GridFS
        public List<String> Photos { get; set; }

        //ссылка на пользователя
        public ApplicationUser User { get; set; }

        [Display(Name = "Наличие товара")]
        public Boolean Availability { get; set; }

        [Display(Name = "Категория")]
        public String Category { get; set; }

        [Display(Name = "Подкатегория")]
        public String Subcategory { get; set; }

        [Display(Name = "Город")]
        public String City { get; set; }

        [Display(Name = "Метро")]
        public String Metro { get; set; }
    }
}
