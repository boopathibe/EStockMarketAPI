using MediatR;
using StockMicroService.Models.Stock;
using StockMicroService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockMicroService.Controllers.Query
{
    public class GetAllStocksQueryHandler : IRequestHandler<GetAllStocksQuery, StockResponse>
    {
        private readonly IStockService _stockService;

        public GetAllStocksQueryHandler(IStockService stockService)
        {
            _stockService = stockService;
        }

        public async Task<StockResponse> Handle(GetAllStocksQuery request, CancellationToken cancellationToken)
        {
            var products =  await _stockService.Get(request.CompanyCode, request.StartDate, request.EndDate);
            return products;
        }
    }
}
