using MediatR;
using StockMicroService.Models.Stock;
using System;

namespace StockMicroService.Controllers.Query
{
    public class GetAllStocksQuery : IRequest<StockResponse>
    {
        public string CompanyCode { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
