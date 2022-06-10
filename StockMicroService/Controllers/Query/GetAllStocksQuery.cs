using MediatR;
using StockMicroService.Models.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMicroService.Controllers.Query
{
    public class GetAllStocksQuery : IRequest<StockResponse>
    {
        public string CompanyCode { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
