using System;

namespace EStockCompanyMessagingQueue
{
    public interface IPublishDeleteStockRabbitMqService
    {
      void SendDeleteCompanyMessage(string deleteCompanyCode);
    }
}
