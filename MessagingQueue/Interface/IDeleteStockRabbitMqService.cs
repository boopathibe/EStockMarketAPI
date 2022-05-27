using System;

namespace EStockCompanyMessagingQueue
{
    public interface IDeleteStockRabbitMqService
    {
      void SendDeleteCompanyMessage(string deleteCompanyCode);
    }
}
