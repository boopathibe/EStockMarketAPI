using MassTransit;
using RabbitMq.Models;
using StockMicroService.Models;
using StockMicroService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMicroService.Consumers
{
    public class StockConsumer : IConsumer<CompanyDetailsQueue>
    {
        IStockService _service;
        public StockConsumer(IStockService service)
        {
            _service = service;
        }
        public async Task Consume(ConsumeContext<CompanyDetailsQueue> context)
        {
            var data = context.Message;
           await _service.Delete(data.CompanyCode);
        }
    }
}
