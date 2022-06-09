using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using StockMicroService.Controllers;
using StockMicroService.Controllers.Query;
using StockMicroService.Models.Stock;
using StockMicroService.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockMicroService.Test.Controllers
{
    [TestFixture]
    public class StockControllerTest
    {
        private StockController controller;
        private Mock<IStockService> _stockService;
        private Mock<ISender> _meditor;

        [SetUp]
        public void Setup()
        {
            _stockService = new Mock<IStockService>();
            _meditor = new Mock<ISender>();
            controller = new StockController(_stockService.Object, _meditor.Object);

        }

        [Test]
        public async Task AddStock_Test()
        {
            // Arrange
            var stockRequest = new StockRequest()
            {
                CompanyCode = "infy",
                 StockPrice = 1000
            };

            _stockService.Setup(x => x.Create(It.IsAny<StockRequest>())).Returns( Task.FromResult(stockRequest));

            // Act
            var response = await controller.AddStock(stockRequest) as OkResult;

            Assert.AreEqual(response.StatusCode, (int)HttpStatusCode.OK);
        }

        [Test]
        public async Task GetStock_TestAsync()
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

            _meditor.Setup(x => x.Send(It.IsAny<GetAllStocksQuery>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(stockResponse));

            // Act
            var response = await controller.Get("infy", DateTime.UtcNow, DateTime.UtcNow.AddDays(1));
            var responseResult = response.Result as OkObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(responseResult.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreEqual((responseResult.Value as StockResponse).Stocks.Count, 1);
        }
    }
}
