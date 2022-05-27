using System;

namespace EStockCompanyMessagingQueue
{
    public interface IConsumerDeleteStockRabbitMqService
    {
      void SendDeleteCompanyMessage(string deleteCompanyCode);
    }
}
