using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMicroService.Models.Stock;
using StockMicroService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StockMicroService.Controllers
{
    [Route("api/v1.0/market/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [Route("add")]
        [HttpPost]
        public ActionResult AddStock([FromBody] StockRequest request)
        {
            try
            {
                 _stockService.Create(request);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex?.Message);
            }
        }

        [Route("get/{companyCode}/{startDate}/{endDate}")]
        [HttpGet]
        public ActionResult<StockResponse> GetStocksbyCompanyCodeAsync(string companyCode, DateTime startDate, DateTime endDate)
        {
            try
            {
                var stockResponse = _stockService.Get(companyCode, startDate, endDate);

                return Ok(stockResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex?.Message);
            }
        }
    }
}
