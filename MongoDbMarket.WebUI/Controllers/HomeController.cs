﻿using Microsoft.AspNet.Identity.Owin;
using MongoDbMarket.Domain;
using MongoDbMarket.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using MongoDB.Bson;

namespace MongoDbMarket.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IMarketRepository repository;

        public HomeController(IMarketRepository repositoryParam)
        {
            repository = repositoryParam;
        }

        //контекст пользователей
        public ApplicationIdentityContext IdentityContext
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationIdentityContext>();
            }
        }

        //
        //Home/index
        public async Task<ActionResult> Admin(ItemFilter filter)
        {
            if(filter != null)
                return View(await repository.GetItemRepository(filter));
            else
                return View(await repository.GetItemRepository());
        }

        //
        //Home/Detail
        public async Task<ActionResult> Detail(String Id)
        {
            if (Id == String.Empty || Id == null || Id.Length != 24)
                return HttpNotFound();

            var model = await repository.GetItem(Id);
            return View(model);
        }

        //
        // GET: /Home/Add
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        //
        // POST: /Home/Add
        [HttpPost]
        public async Task<ActionResult> Add(Item item)
        {
            var sUser = await IdentityContext.Users.Find(Builders<ApplicationUser>.
                Filter.Eq("UserName", User.Identity.Name)).FirstAsync(); 
            item.User = sUser;

            var ImageIdList = new List<String>();

            for (int i = 1; i <= 3; i++)
            {
                if (Request.Files["file" + i].FileName != null &&
                Request.Files["file" + i].FileName != String.Empty)
                {
                    await repository.UploadStream(Request.Files["file" + i].InputStream,
                        Request.Files["file" + i].FileName);

                    ImageIdList.Add(repository.ImageId);
                }
            }

            item.Photos = ImageIdList;

            await repository.AddItem(item);
            return RedirectToAction("List");
        }

        //
        // GET: /Home/Edit
        [HttpGet]
        public async Task<ActionResult> Edit(String Id)
        {
            if (Id == null || Id == String.Empty || Id.Length != 24)
                return HttpNotFound();

            return View(await repository.GetItem(Id));
        }

        //
        // POST: /Home/Edit
        [HttpPost]
        public async Task<ActionResult> Edit(Item item)
        {
            await repository.EditItem(item);
            return RedirectToAction("List");
        }

        //добавить POST!!
        //
        // GET: /Home/Delete
        public async Task<ActionResult> Delete(String Id)
        {
            if (Id == null || Id == String.Empty || Id.Length != 24)
                return HttpNotFound();

            await repository.RemoveItem(Id);
            return RedirectToAction("List");
        }

        //
        //Home/List
        public async Task<ActionResult> List(ItemFilter filter)
        {
            if (filter != null)
                return View(await repository.GetItemRepository(filter));
            else
                return View(await repository.GetItemRepository());
        }

        //
        //Home/UserDetail
        public async Task<ActionResult> UserDetail(String Id)
        {
            if (Id == null || Id == String.Empty || Id.Length != 24)
                return HttpNotFound();

            var sUser = await IdentityContext.Users.Find(Builders<ApplicationUser>.
                Filter.Eq("Id", Id)).FirstAsync();

            ViewBag.Items = await repository.GetItemByUserId(Id);

            return View(sUser);
        }

        private async Task<ActionResult> Indexxx(String Id, List<String> ImagesId)
        {
            if (Id == null || Id == String.Empty)
                return HttpNotFound();

            ImagesId = await repository.GetImagesId(Id);

            var images = await repository.GetImages(ImagesId);

            return View(images);
        }
    }
}