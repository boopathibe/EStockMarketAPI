using StockMicroService.Entities;
using StockMicroService.Models.Stock;
using StockMicroService.Repository;
using System;
using System.Linq;

namespace StockMicroService.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;

        public StockService(IStockRepository repository)
        {
            this._stockRepository = repository;
        }

        public StockRequest Create(StockRequest stockRequest)
        {
            var stock = new Stock()
            {
                CompanyCode = stockRequest.CompanyCode,
                StockPrice = stockRequest.StockPrice,                
                CreatedAt = DateTime.UtcNow
            };

            this._stockRepository.Create(stock);
            return stockRequest;
        }

        public StockResponse Get(string code, DateTime startDate, DateTime endDate)
        {
            var stocks = this._stockRepository.Get(code, startDate, endDate);
            return new StockResponse()
            {
                Stocks = stocks.Select( x=> new StockDetails()
                {
                    CompanyCode = x.CompanyCode,
                    StockPrice = x.StockPrice,
                    StockDate = x.CreatedAt.ToShortDateString(),
                    StockTime = x.CreatedAt.ToShortTimeString()
                }).ToList(),
                AvgPrice = stocks.Average(x => x.StockPrice),
                MaxPrice = stocks.Max(x => x.StockPrice),
                MinPrice = stocks.Min(x => x.StockPrice)
            };
        }

        public void Delete(string code)
        {
            _stockRepository.Delete(code);
        }     
    }
}
