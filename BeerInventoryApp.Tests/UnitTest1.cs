using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeerInventoryApp.Services;

namespace BeerInventoryApp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var genericClient = new GenericRestClient("http://a.com");
            var genericRequest = new GenericRestRequest("api");

            genericClient.Post(genericRequest);
        }
    }
}
