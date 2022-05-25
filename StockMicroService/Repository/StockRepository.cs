using MongoDB.Driver;
using StockMicroService.Entities;
using StockMicroService.Models.DatabaseConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMicroService.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly IMongoCollection<Stock> _stockRepository;
        public StockRepository(IStockDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _stockRepository = database.GetCollection<Stock>(settings.StockCollectionName);
        }

        public Stock Create(Stock stock)
        {
            this._stockRepository.InsertOne(stock);
            return stock;
        }

        public List<Stock> Get(string code, DateTime startDate, DateTime endDate)
        {
           var stocks = this._stockRepository.Find(x => x.CompanyCode == code && x.CreatedAt >= startDate && x.CreatedAt <= endDate).ToList();
            return stocks;
        }

        public void Delete(string code)
        {
            throw new NotImplementedException();
        }
    }
}
