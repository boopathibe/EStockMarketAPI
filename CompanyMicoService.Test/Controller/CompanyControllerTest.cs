using CompanyMicroService.Controllers;
using CompanyMicroService.Models;
using CompanyMicroService.Services;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;

namespace CompanyMicoService.Test
{
    public class CompanyControllerTest
    {
        private CompanyController controller;
        private Mock<ICompanyService> _companyService;
        private Mock<IPublishEndpoint> _publishEndpoint;

        [SetUp]
        public void Setup()
        {
            _companyService = new Mock<ICompanyService>();
            _publishEndpoint = new Mock<IPublishEndpoint>();
            controller = new CompanyController(_companyService.Object, _publishEndpoint.Object);
        }

        [Test]
        public void AddCompany_Test()
        {
            // Arrange
            var companyRequest = new CompanyRequest()
            {
              Code = "Comp1",
              CeoName = "Ceo",
              Name = "Company",
              Exchange = new string[] { "NSE", "BSE"},
              TurnOver = 1000000m,
              Website = "www.ccc.com"
            };

            _companyService.Setup(x => x.Register(It.IsAny<CompanyRequest>())).Returns(companyRequest);

            // Act
            var response = controller.Post(companyRequest) as OkResult;

            Assert.AreEqual(response.StatusCode, (int)HttpStatusCode.OK);
        }

        [Test]
        public void GetAll_Company_Test()
        {
            // Arrange
            var company = new List<CompanyResponse>();
            var companyDetails = new CompanyResponse
            {
                Code = "Comp1",
                CeoName = "Ceo",
                Name = "Company",
                Exchange = new string[] { "NSE", "BSE" },
                TurnOver = 1000000m,
                Website = "www.ccc.com"
            };

            company.Add(companyDetails);

            _companyService.Setup(x => x.GetAll()).Returns(company);

            // Act
            var response = controller.Get().Result as OkObjectResult;

            //Assert
            Assert.IsNotNull(response);            
            Assert.AreEqual(response.StatusCode, (int)HttpStatusCode.OK);            
        }

        [Test]
        public void GetByCompanyCode_Test()
        {
            // Arrange
            var companyDetails = new CompanyResponse
            {
                Code = "Comp1",
                CeoName = "Ceo",
                Name = "Company",
                Exchange = new string[] { "NSE", "BSE" },
                TurnOver = 1000000m,
                Website = "www.ccc.com"
            };

            _companyService.Setup(x => x.GetByCode("Comp1")).Returns(companyDetails);

            // Act
            var response = controller.GetByCode("Comp1").Result as OkObjectResult;

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, (int)HttpStatusCode.OK);
        }

        [Test]
        public void Delete_Company_Test()
        {
            // Arrange
            var companyCode = "Comp1";

            _companyService.Setup(x => x.Delete(companyCode));

            // Act
            var response = controller.Delete("Comp1").Result as OkObjectResult;

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, (int)HttpStatusCode.OK);
        }
    }
}