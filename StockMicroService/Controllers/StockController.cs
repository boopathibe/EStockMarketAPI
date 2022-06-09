using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMicroService.Controllers.Query;
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
        private readonly ISender _mediator;

        public StockController(IStockService stockService, ISender mediator)
        {
            _stockService = stockService;
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        public async Task<ActionResult> AddStock([FromBody] StockRequest request)
        {
            try
            {
                 await _stockService.Create(request);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex?.Message);
            }
        }

        [Route("get/{companyCode}/{startDate}/{endDate}")]
        [HttpGet]
        public async Task<ActionResult<StockResponse>> Get(string companyCode, DateTime startDate, DateTime endDate)
        {
            try
            {
                var request = new GetAllStocksQuery()
                {
                    CompanyCode = companyCode,
                    StartDate = startDate,
                    EndDate = endDate
                };

                var stockResponse = await _mediator.Send(request);

                return Ok(stockResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex?.Message);
            }
        }
    }
}
