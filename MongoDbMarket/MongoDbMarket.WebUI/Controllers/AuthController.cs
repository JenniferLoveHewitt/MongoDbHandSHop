using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MongoDbMarket.Domain;
using MongoDbMarket.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MongoDbMarket.WebUI.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        IMarketRepository repository;
        private ApplicationUserManager _userManager;

        public AuthController() { }

        public AuthController(IMarketRepository repositoryParam, 
            ApplicationUserManager userManager)
        {
            UserManager = userManager;
            repository = repositoryParam;
        }

        //хранилище пользователей
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        //Auth/Index
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Auth/Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Auth/Register
        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Country = model.Country,
                Email = model.Email,
                Name = model.Name ,
                PhoneNumber = model.PhoneNumber,
                Skype = model.Skype      
            };

            var result = await UserManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await SignIn(user);
                return RedirectToAction("index", "home");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error);

            return View();
        }

        //
        // GET: /Auth/LogIn
        [HttpGet]
        public ActionResult LogIn(String returnUrl)
        {
            var model = new LogInModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        //
        // POST: /Auth/LogIn
        [HttpPost]
        public async Task<ActionResult> LogIn(LogInModel model)
        {
            if (!ModelState.IsValid)            
                return View();

            var user = await UserManager.FindAsync(model.Email, model.Password);

            if (user != null)
            {
                var identity = await UserManager.CreateIdentityAsync(
                    user, DefaultAuthenticationTypes.ApplicationCookie);

                GetAuthenticationManager().SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        //
        //Auth/LogOut
        public ActionResult LogOut()
        {
            GetAuthenticationManager().SignOut("ApplicationCookie");
            return RedirectToAction("index", "home");
        }

        private String GetRedirectUrl(String returnUrl)
        {
            if (String.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                return Url.Action("index", "home");            

            return returnUrl;
        }

        private IAuthenticationManager GetAuthenticationManager()
        {
            var ctx = Request.GetOwinContext();
            return ctx.Authentication;
        }

        private async Task SignIn(ApplicationUser user)
        {
            var identity = await UserManager.CreateIdentityAsync(user, 
                DefaultAuthenticationTypes.ApplicationCookie);

            GetAuthenticationManager().SignIn(identity);
        }
    }
}