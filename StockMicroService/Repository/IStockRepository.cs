using StockMicroService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMicroService.Repository
{
    public interface IStockRepository
    {
        List<Stock> Get(string code, DateTime startDate, DateTime endDate);

       Stock Create(Stock stock);

        void Delete(string code);
    }
}
