using Microsoft.AspNet.Identity.Owin;
using MongoDbMarket.Domain;
using MongoDbMarket.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
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
        public async Task<ActionResult> Index(ItemFilter filter)
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
            if (Id == String.Empty)
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

            //ViewData["Id"] = sUser.Id;
            //ViewData["UserName"] = sUser.UserName;
            //ViewData["Country"] = sUser.Country;
            //ViewData["Password"] = sUser.PasswordHash;

            await repository.AddItem(item);
            return RedirectToAction("Index");
        }

        //
        // GET: /Home/Edit
        [HttpGet]
        public async Task<ActionResult> Edit(String Id)
        {
            if (Id == null || Id == String.Empty)
                return HttpNotFound();

            return View(await repository.GetItem(Id));
        }

        //
        // POST: /Home/Edit
        [HttpPost]
        public async Task<ActionResult> Edit(Item item)
        {
            await repository.EditItem(item);
            return RedirectToAction("Index");
        }

        //добавить GET!!
        //
        // POST: /Home/Delete
        public async Task<ActionResult> Delete(String Id)
        {
            await repository.RemoveItem(Id);
            return RedirectToAction("Index");
        }

        //
        //Home/UserDetail
        public ActionResult UserDetail()
        {
            return View();
        }
    }
}