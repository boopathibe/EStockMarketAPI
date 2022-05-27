using EStockCompanyMessagingQueue;
using StockMicroService.Entities;
using StockMicroService.Models.Stock;
using StockMicroService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace StockMicroService.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private IConnection _connection;
        private readonly IConfiguration _configuration;
        private readonly string _connectionUri;
        private readonly string _queueName;

        public StockService(IStockRepository repository,IConfiguration configuration)
        {
            this._stockRepository = repository;

            _configuration = configuration;

            var configSection = _configuration.GetSection("RabbitMq");

            _connectionUri = configSection.GetSection("ConnectionUri").Value;
            _queueName = configSection.GetSection("QueueName").Value;

            CreateConnection();
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
            if (ConnectionExists())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, e) => {
                        var body = e.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                    };
                    channel.BasicConsume("_queueName", true, consumer);
                    _stockRepository.Delete(code);
                }
            }
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    Uri = new Uri(_connectionUri)
                };

                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }

            CreateConnection();

            return _connection != null;
        }
    }
}
