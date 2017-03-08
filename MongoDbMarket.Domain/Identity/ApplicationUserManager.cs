using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbMarket.Domain.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationIdentityContext>().Users));

            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            //Пароль должен соответствовать след. требованиям
            manager.PasswordValidator = new PasswordValidator
            {
                //длина пароля
                RequiredLength = 6,
                //наличие символа, отличного от цифры или буквы
                RequireNonLetterOrDigit = false,
                //наличие цифры
                RequireDigit = false,
                //нижний регист
                RequireLowercase = true,
                //верхний регистр
                RequireUppercase = false,
            };

            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            return manager;
        }
    }
}
