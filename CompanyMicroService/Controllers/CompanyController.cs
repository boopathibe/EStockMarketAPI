using CompanyMicroService.Models;
using CompanyMicroService.Services;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CompanyMicroService.Controllers
{
    [ApiController]
    [Route("api/v1.0/market/company")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IPublishEndpoint _publishEndpoint;
   
        public CompanyController(ICompanyService service, IPublishEndpoint publishEndpoint)
        {
            _companyService = service;
            _publishEndpoint = publishEndpoint;

        }
    
        [HttpGet, Authorize]
        [Route("getall")]
        public ActionResult<List<CompanyResponseModel>> Get()
        {
            try
            {
               var companies =  _companyService.GetAll();
               return Ok(companies);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        [HttpGet]
        [Route("info/{companycode}")]
        public ActionResult<CompanyResponseModel> GetByCode(string companycode)
        {
            try
            {
                var company = _companyService.GetByCode(companycode);

                if (company == null) 
                {
                    return NotFound();
                }
               
                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
 
        [HttpPost]
        [Route("register")]
        public ActionResult Post([FromBody] CompanyRequestModel request)
        {
            try
            {
                _companyService.Register(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
             
        [HttpDelete]
        [Route("delete/{companycode}")]
        public async Task<IActionResult> Delete(string companycode)
        {
            try
            {
               await _publishEndpoint.Publish(new CompanyDetailsQueue() { CompanyCode = companycode });

               _companyService.Delete(companycode);

                return Ok(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
    }
}
