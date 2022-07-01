using System;

namespace EStockCompanyMessagingQueue
{
    public interface IDeleteStockRabbitMqService1
    {
      void SendDeleteCompanyMessage(string deleteCompanyCode);
    }
}
