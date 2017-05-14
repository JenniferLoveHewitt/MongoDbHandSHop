using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDbMarket.WebUI.Controllers;
using MongoDbMarket.Domain;
using System.Web.Mvc;
using System.Threading.Tasks;
using MongoDbMarket.Domain.Identity;
using System.Web;

namespace MongoDbMarket.Tests
{
    [TestClass]
    class AuthControllerTests
    {
        IMarketRepository repository;
        ApplicationUserManager _userManager;

        [TestInitialize]
        public void Setup()
        {
            repository = new MarketRepository();
        }
    }
}
