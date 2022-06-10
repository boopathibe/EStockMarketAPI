using StockMicroService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMicroService.Repository
{
    public interface IStockRepository
    {
        Task<List<Stock>> Get(string code, DateTime startDate, DateTime endDate);

       Task<Stock> Create(Stock stock);

        Task Delete(string code);
    }
}
