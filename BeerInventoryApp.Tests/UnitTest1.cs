using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeerInventoryApp.Services;

namespace BeerInventoryApp.Tests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void Test2()
        {
            IAzureSearchApi searchAPi = RestService.For<IAzureSearchApi>(AzureSearchApi.ApiUrl);

            searchAPi.Search(AzureSearchApi.BeerIndex, "");
        }
    }
}
