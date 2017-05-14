using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDbMarket.WebUI.Controllers;
using MongoDbMarket.Domain;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace MongoDbMarket.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        private IMarketRepository repository;
        private HomeController controller;

        [TestInitialize]
        public void Setup()
        {
            repository = new MarketRepository();
            controller = new HomeController(repository);
        }

        [TestMethod]
        public async Task TestDetailWithNullParameter()
        {
            var result = await controller.Detail(null) as ViewResult;

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task TestDetailWithStringEmptyParameter()
        {
            var result = await controller.Detail("") as ViewResult;

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task TestDetailWithNon24hexValueParameter()
        {
            var result = await controller.Detail("some") as ViewResult;

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task TestDetailWithCorrectParameter()
        {
            var result = await controller.Detail("58ce5d209e73c5282cd2caa6") as ViewResult;

            Assert.IsNotNull(result.ViewName);
        }

        [TestMethod]
        public async Task TestIndexWithNullParameter()
        {
            var result = await controller.Index(new ItemFilter()) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TestIndexWithNotNullParameter()
        {
            var filter = new ItemFilter(){ Title = "abcabc" };
            var result = await controller.Index(filter) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestAdd()
        {
            var result = controller.Add() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TestEditWithNon24hexValueParameter()
        {
            var result = await controller.Edit("some") as ViewResult;

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task TestEditWithStringEmptyParameter()
        {
            var result = await controller.Edit("") as ViewResult;

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task TestEditWith24hexParameter()
        {
            var result = await controller.Edit("58ce5d209e73c5282cd2caa6") as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TestDeleteWithNullParameter()
        {
            var result = await controller.Delete(null) as ViewResult;

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task TestDeleteWithNon24hexValueParameter()
        {
            var result = await controller.Delete("non24hex") as ViewResult;

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task TestDeleteWithStringEmptyParameter()
        {
            var result = await controller.Edit("") as ViewResult;

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task TestDeleteWith24hexParameter()
        {
            var result = await controller.Edit("58ce5d209e73c5282cd2caa6") as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TestListWithNullParameter()
        {
            var result = await controller.List(new ItemFilter()) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TestListWithNotNullParameter()
        {
            var filter = new ItemFilter() { Title = "abcabc" };
            var result = await controller.List(filter) as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
