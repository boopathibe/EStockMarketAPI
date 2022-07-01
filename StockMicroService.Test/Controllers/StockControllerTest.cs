using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using StockMicroService.Controllers;
using StockMicroService.Models.Stock;
using StockMicroService.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace StockMicroService.Test.Controllers
{
    [TestFixture]
    public class StockControllerTest
    {
        private StockController controller;
        private Mock<IStockService> _stockService;

        [SetUp]
        public void Setup()
        {
            _stockService = new Mock<IStockService>();
            controller = new StockController(_stockService.Object);

        }

        [Test]
        public void AddStock_Test()
        {
            // Arrange
            var stockRequest = new StockRequest()
            {
                CompanyCode = "infy",
                 StockPrice = 1000
            };

            _stockService.Setup(x => x.Create(It.IsAny<StockRequest>())).Returns(stockRequest);

            // Act
            var response = controller.AddStock(stockRequest) as OkResult;

            Assert.AreEqual(response.StatusCode, (int)HttpStatusCode.OK);
        }

        [Test]
        public void GetStock_Test()
        {
            // Arrange
            var stocks = new List<StockDetails>();
            var stockDetails = new StockDetails()
            {
                CompanyCode = "infy",
                StockPrice = 1000, 
                StockDate = DateTime.UtcNow.ToShortDateString(), 
                StockTime = DateTime.UtcNow.ToShortTimeString()
            };

            stocks.Add(stockDetails);

            var stockResponse = new StockResponse()
            {
                Stocks = stocks,
                AvgPrice = 1500,
                MaxPrice = 2000,
                MinPrice = 1000
            };

            _stockService.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(stockResponse);

            // Act
            var response = controller.Get("infy", DateTime.UtcNow, DateTime.UtcNow.AddDays(1)).Result as ObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreEqual((response.Value as StockResponse).Stocks.Count, 1);
        }
    }
}
