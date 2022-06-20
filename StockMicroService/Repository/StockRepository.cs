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

        public async Task<Stock> Create(Stock stock)
        {
            await this._stockRepository.InsertOneAsync(stock);
            return stock;
        }

        public async Task<List<Stock>> Get(string code, DateTime startDate, DateTime endDate)
        {
            var stocks = await this._stockRepository.FindAsync(x => x.CompanyCode == code);
            var stocksList = stocks.ToList().Where(x => x.CreatedAt.Date >= startDate.Date && x.CreatedAt.Date <= endDate.Date).ToList();
            return stocksList;
        }

        public async Task Delete(string code)
        {
            await this._stockRepository.DeleteManyAsync(x => x.CompanyCode == code);
        }
    }
}
