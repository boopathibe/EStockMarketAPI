using StockMicroService.Models.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMicroService.Services
{
    public interface IStockService
    {
        Task<StockResponse> Get(string code, DateTime startDate, DateTime endDate);

        Task<StockRequest> Create(StockRequest stock);

        Task Delete(string code);
    }
}
