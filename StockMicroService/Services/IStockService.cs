using StockMicroService.Models.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMicroService.Services
{
    public interface IStockService
    {
        StockResponse Get(string code, DateTime startDate, DateTime endDate);

        StockRequest Create(StockRequest stock);

        void Delete(string code);
    }
}
