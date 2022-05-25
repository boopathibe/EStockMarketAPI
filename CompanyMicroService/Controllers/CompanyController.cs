using CompanyMicroService.Models;
using CompanyMicroService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public CompanyController(ICompanyService service)
        {
            _companyService = service;

        }
    
        [HttpGet]
        [Route("getall")]
        public ActionResult <List<CompanyResponse>> Get()
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
        public ActionResult<CompanyResponse> Get(string companycode)
        {
            try
            {
                var company = _companyService.GetByCode(companycode);
                
                if(company == null)
                return NotFound();

                return Ok(company);


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

 
        [HttpPost]
        [Route("register")]
        public ActionResult Post([FromBody] CompanyRequest request)
        {
            try
            {
                _companyService.Register(request);
                return Ok(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

     
        [HttpDelete]
        [Route("delete/{companycode}")]
        public ActionResult Delete(string companycode)
        {
            try
            {
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
