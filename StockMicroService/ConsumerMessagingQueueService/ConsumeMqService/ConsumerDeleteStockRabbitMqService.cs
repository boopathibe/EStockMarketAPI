using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StockMicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace EStockCompanyMessagingQueue
{
    public class ConsumerDeleteStockRabbitMqService : IConsumerDeleteStockRabbitMqService
    {
        private IConnection _connection;
        private IStockService _stockService;
        private readonly IConfiguration _configuration;
        private readonly string _connectionUri;
        private readonly string _queueName;

        public ConsumerDeleteStockRabbitMqService(IConfiguration configuration, IStockService stockService)
        {
            _configuration = configuration;
            _stockService = stockService;

            var configSection = _configuration.GetSection("RabbitMq");
            _connectionUri = configSection.GetSection("ConnectionUri").Value;
            _queueName = configSection.GetSection("QueueName").Value;

            CreateConnection();
        }

        public void SendDeleteCompanyMessage(string deleteCompanyCode)
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, e) =>
                {
                    var body = e.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                };
                channel.BasicConsume("_queueName", true, consumer);
                ////_stockService.Delete(consumer);
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
